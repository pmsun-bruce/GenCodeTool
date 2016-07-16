namespace NFramework.GenCodeTool.Handler
{
    #region Reference

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    using log4net;
    using NFramework.ExceptionTool;
    using NFramework.ObjectTool;

    using NFramework.GenCodeTool.CodeInfoGetter;
    using NFramework.GenCodeTool.DBInfoGetter;
    using NFramework.GenCodeTool.Entity;
    using NFramework.GenCodeTool.Resources.Lan;

    #endregion

    /// <summary>
    /// 代码生成操作
    /// </summary>
    public class GenCodeHandler
    {
        #region Fields & Properties

        /// <summary>
        /// 日志记录器
        /// </summary>
        private static ILog logWriter;
        /// <summary>
        /// 日志记录器
        /// </summary>
        private static ILog LogWriter
        {
            get
            {
                if (logWriter == null)
                {
                    logWriter = LogManager.GetLogger("Gen Code Logger");
                }

                return logWriter;
            }
        }

        /// <summary>
        /// 生成模板池，从程序下的CodeTemplate目录中读取所有模板，以模板文件夹名称为名称
        /// </summary>
        private static Dictionary<string, string> codeTemplatePool;
        /// <summary>
        /// 生成模板池，从程序下的CodeTemplate目录中读取所有模板，以模板文件夹名称为名称
        /// </summary>
        public static Dictionary<string, string> CodeTemplatePool
        {
            get
            {
                if(codeTemplatePool == null)
                {
                    codeTemplatePool = new Dictionary<string, string>();
                    string tmplRootFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"CodeTemplate");

                    if (!Directory.Exists(tmplRootFolder))
                    {
                        return codeTemplatePool;
                    }

                    string[] templateArr = Directory.GetDirectories(tmplRootFolder);
                    string tmplName = string.Empty;

                    foreach (string template in templateArr)
                    {
                        tmplName = template.Substring(template.LastIndexOf(@"\") + 1);

                        if (!codeTemplatePool.ContainsKey(tmplName))
                        {
                            codeTemplatePool.Add(tmplName, template);
                        }
                    }
                }

                return codeTemplatePool;
            }
        }

        /// <summary>
        /// 已保存的生成配置池，从程序目录下的Resources\Config目录中读取
        /// </summary>
        private static IList<GenSetting> genSettingPool;
        /// <summary>
        /// 已保存的生成配置池，从程序目录下的Resources\Config目录中读取
        /// </summary>
        public static IList<GenSetting> GenSettingPool
        {
            get
            {
                if (genSettingPool == null)
                {
                    genSettingPool = new List<GenSetting>();
                    GenSetting genSetting = null;
                    string genSettingFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\Config");

                    if (!Directory.Exists(genSettingFolder))
                    {
                        return null;
                    }

                    string[] genSettingFileArr = Directory.GetFiles(genSettingFolder);
                    string xmlStr = string.Empty;

                    foreach (string genSettingFile in genSettingFileArr)
                    {
                        try
                        {
                            xmlStr = File.ReadAllText(genSettingFile);
                            genSetting = ObjectFactory.DeserializeFromXML<GenSetting>(xmlStr);
                            genSettingPool.Add(genSetting);
                        }
                        catch (Exception ex)
                        {
                            LogWriter.Error(ex.Message, ex);
                        }
                    }
                }

                return genSettingPool;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 生成前进行验证，各种必要信息是否正确
        /// </summary>
        /// <param name="projectInfo">项目信息对象</param>
        /// <returns>如果验证不通过，则返回报错信息；如果通过，则返回空字符串</returns>
        public static string ValidatePreGen(ProjectInfo projectInfo)
        {
            string errMsg = string.Empty;

            if (projectInfo == null)
            {
                errMsg = GenCodeToolResource.Error_NullProjectInfo;
                return errMsg;
            }

            if (string.IsNullOrEmpty(projectInfo.Name))
            {
                errMsg += GenCodeToolResource.Error_NoProjectName + System.Environment.NewLine;
            }

            if (string.IsNullOrEmpty(projectInfo.Namespace))
            {
                errMsg += GenCodeToolResource.Error_NoNamespace + System.Environment.NewLine;
            }

            if (string.IsNullOrEmpty(projectInfo.ConnectionString))
            {
                errMsg += GenCodeToolResource.Error_NullDBGetter + System.Environment.NewLine;
            }

            if (projectInfo.GenTableInfoList == null || projectInfo.GenTableInfoList.Count == 0)
            {
                errMsg += GenCodeToolResource.Error_NoGenTable + System.Environment.NewLine;
            }

            if (projectInfo.DBInfoGetter == null)
            {
                errMsg += GenCodeToolResource.Error_NullDBGetter + System.Environment.NewLine;
            }

            if (projectInfo.CodeInfoGetter == null)
            {
                errMsg += GenCodeToolResource.Error_NullCodeGetter + System.Environment.NewLine;
            }

            if (string.IsNullOrEmpty(projectInfo.TemplatePath) || !Directory.Exists(projectInfo.TemplatePath))
            {
                errMsg += GenCodeToolResource.Error_NoTemplatePath + System.Environment.NewLine;
            }

            if (string.IsNullOrEmpty(projectInfo.GenTargetPath))
            {
                errMsg += GenCodeToolResource.Error_NoTargetPath + System.Environment.NewLine;
            }

            return errMsg;
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="projectInfo">项目信息对象</param>
        public static void GenCode(ProjectInfo projectInfo)
        {
            string errMsg = ValidatePreGen(projectInfo);

            if (!string.IsNullOrWhiteSpace(errMsg))
            {
                throw new ResponseException(errMsg);
            }

            // 整理表信息，会自动加载没有被选择，但是有关联的表信息对象，但是不会生成关联表的物理文件
            InitGenTableInfo(projectInfo);

            if (projectInfo.IsClearTargetFolder)
            {
                ClearTargetFolder(projectInfo.GenTargetPath);
            }

            RecursionGen(projectInfo.TemplatePath, projectInfo.GenTargetPath, projectInfo);
        }

        /// <summary>
        /// 根据数据库连接和指定的数据库信息获取器读取所有表信息对象集合
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="dbInfoGetterName">指定的数据库信息获取器</param>
        /// <returns>返回所有表信息对象集合</returns>
        public static IList<TableInfo> GetTableInfoList(string connectionString, IDBInfoGetter dbInfoGetter)
        {
            IList<TableInfo> tableInfoList = dbInfoGetter.GetTableInfoList(connectionString);
            return tableInfoList;
        }

        /// <summary>
        /// 填充表信息对象中的字段信息
        /// </summary>
        /// <param name="dbInfoGetter">数据库连接字符串</param>
        /// <param name="dbInfoGetter">指定的数据库信息获取器</param>
        /// <param name="tableInfo">需要填充的表信息对象</param>
        public static void FillColumnInfoList(string connectionString, IDBInfoGetter dbInfoGetter, ICodeInfoGetter codeInfoGetter, TableInfo tableInfo)
        {
            if (tableInfo.ColumnList != null && tableInfo.ColumnList.Count > 0)
            {
                return;
            }

            dbInfoGetter.FillColumnInfo(connectionString, tableInfo);

            foreach (ColumnInfo columnInfo in tableInfo.ColumnList)
            {
                columnInfo.DalType = dbInfoGetter.ToDalType(columnInfo.SqlType, columnInfo.Precision, columnInfo.Scale);
                columnInfo.CodeType = codeInfoGetter.ToCodeType(columnInfo.DbType);
            }
        }

        /// <summary>
        /// 保存生成配置
        /// </summary>
        /// <param name="genSetting">需要保存的配置对象</param>
        public static void SaveGenSetting(GenSetting genSetting)
        {
            string genSettingFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\Config\");

            if (!Directory.Exists(genSettingFolderPath))
            {
                Directory.CreateDirectory(genSettingFolderPath);
            }

            string genSettingFilePath = Path.Combine(genSettingFolderPath, genSetting.SettingName + ".xml");
            string xmlStr = ObjectFactory.SerializeToXML<GenSetting>(genSetting);
            File.WriteAllText(genSettingFilePath, xmlStr, Encoding.UTF8);
        }

        /// <summary>
        /// 递归计算需要生成的文件的总数量
        /// </summary>
        /// <param name="templateFolderPath">模板所处目录</param>
        /// <param name="genTableCount">需要生成的表的个数</param>
        public static int CalculateGenCount(string templateFolderPath, int genTableCount)
        {
            int genCount = 0;
            int childCount = 0;
            string[] templateDirectories = Directory.GetDirectories(templateFolderPath);
            GenType folderType = FolderFileTemplatePlaceholder.CheckGenType(templateFolderPath, true);

            if (templateDirectories.Length > 0)
            {
                string targetPath = string.Empty;

                foreach (string templatePath in templateDirectories)
                {
                    childCount = CalculateGenCount(templatePath, genTableCount);

                    if (FolderFileTemplatePlaceholder.CheckGenType(templatePath, true) == GenType.TableLoopFolder)
                    {
                        genCount += childCount * genTableCount;
                    }
                    else
                    {
                        genCount += childCount;
                    }
                }
            }

            string[] templateFiles = Directory.GetFiles(templateFolderPath);

            if (templateFiles.Length > 0)
            {
                foreach (string filePath in templateFiles)
                {
                    if (FolderFileTemplatePlaceholder.CheckGenType(filePath, false) == GenType.TableLoopFile && folderType != GenType.TableLoopFolder)
                    {
                        genCount += genTableCount;
                    }
                    else
                    {
                        genCount++;
                    }
                }
            }

            return genCount;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 整理表信息，会自动加载没有被选择，但是有关联的表信息对象，但是不会生成关联表的物理文件
        /// </summary>
        /// <param name="projectInfo">项目信息对象</param>
        private static void InitGenTableInfo(ProjectInfo projectInfo)
        {
            TableInfo fkTableInfo = null;
            ColumnInfo fkColumnInfo = null;

            foreach (TableInfo tableInfo in projectInfo.GenTableInfoList)
            {
                if (tableInfo.ColumnList.Count == 0)
                {
                    FillColumnInfoList(projectInfo.ConnectionString, projectInfo.DBInfoGetter, projectInfo.CodeInfoGetter, tableInfo);
                }

                if (tableInfo.FKList.Count == 0)
                {
                    continue;
                }

                foreach (ColumnInfo columnInfo in tableInfo.FKList)
                {
                    fkTableInfo = projectInfo.AllDBTableInfoList.FirstOrDefault<TableInfo>(t => t.Name.Equals(columnInfo.FKTableName));

                    if (fkTableInfo == null)
                    {
                        fkTableInfo = projectInfo.DBInfoGetter.GetTableInfo(projectInfo.ConnectionString, columnInfo.FKTableName);

                        if (fkTableInfo == null)
                        {
                            columnInfo.IsFK = false;
                            tableInfo.FKList.Remove(columnInfo);
                            continue;
                        }

                        fkTableInfo.IsGen = false;
                        fkTableInfo.IsGenUI = false;
                    }
                    else
                    {
                        if (fkTableInfo.CurrProjectInfo == null)
                        {
                            fkTableInfo.CurrProjectInfo = projectInfo;
                        }
                    }

                    if(fkTableInfo.ColumnList.Count == 0)
                    {
                        FillColumnInfoList(projectInfo.ConnectionString, projectInfo.DBInfoGetter, projectInfo.CodeInfoGetter, fkTableInfo);
                    }

                    fkColumnInfo = fkTableInfo.ColumnList.FirstOrDefault<ColumnInfo>(c => columnInfo.FKColumnName.Equals(c.Name));

                    if (fkColumnInfo == null)
                    {
                        columnInfo.IsFK = false;
                        tableInfo.FKList.Remove(columnInfo);
                        continue;
                    }

                    columnInfo.FKColumn = fkColumnInfo;
                    fkTableInfo.RKList.Add(columnInfo);
                }
            }
        }

        #region 物理目录或文件操作

        /// <summary>
        /// 清空目标目录中的所有内容
        /// </summary>
        /// <param name="targetRootFolderPath">目标生成目录</param>
        private static void ClearTargetFolder(string targetRootFolderPath)
        {
            try
            {
                Directory.Delete(targetRootFolderPath, true);
            }
            catch (Exception ex)
            {
                LogWriter.Error(ex.Message, ex);
            }

            if (!Directory.Exists(targetRootFolderPath))
            {
                Directory.CreateDirectory(targetRootFolderPath);
            }
        }

        /// <summary>
        /// 根据表信息进行递归生成
        /// </summary>
        /// <param name="templateFolderPath">需要生成的模板目录路径</param>
        /// <param name="targetParentFolderPath">生成的目录所要保存的父目标目录</param>
        /// <param name="tableInfo">需要生成的表信息对象</param>
        private static void RecursionGen(string templateFolderPath, string targetParentFolderPath, TableInfo tableInfo)
        {
            string[] templateDirectories = Directory.GetDirectories(templateFolderPath);

            if (templateDirectories.Length > 0)
            {
                string targetPath = string.Empty;

                foreach (string templatePath in templateDirectories)
                {
                    targetPath = GenTargetPath(templatePath, targetParentFolderPath);
                    targetPath = ReplaceProjectFFPlaceholder(targetPath, tableInfo.CurrProjectInfo);
                    targetPath = ReplaceTableFFPlaceholder(targetPath, tableInfo);

                    if (!Directory.Exists(targetPath))
                    {
                        Directory.CreateDirectory(targetPath);
                    }

                    RecursionGen(templatePath, targetPath, tableInfo);
                }
            }

            string[] templateFiles = Directory.GetFiles(templateFolderPath);

            if (templateFiles.Length > 0)
            {
                foreach (string filePath in templateFiles)
                {
                    GenProjectFile(filePath, targetParentFolderPath, tableInfo);
                }
            }
        }

        /// <summary>
        /// 根据表信息进行递归生成
        /// </summary>
        /// <param name="templateFolderPath">需要生成的模板目录路径</param>
        /// <param name="targetParentFolderPath">生成的目录所要保存的父目标目录</param>
        /// <param name="projectInfo">需要生成的项目信息对象</param>
        private static void RecursionGen(string templateFolderPath, string targetParentFolderPath, ProjectInfo projectInfo)
        {
            string[] templateDirectories = Directory.GetDirectories(templateFolderPath);
            
            if (templateDirectories.Length > 0)
            {
                string targetPath = string.Empty;

                foreach (string templatePath in templateDirectories)
                {
                    if (FolderFileTemplatePlaceholder.CheckGenType(templatePath, true) == GenType.TableLoopFolder)
                    {
                        GenLoopTableFolder(templatePath, targetParentFolderPath, projectInfo);
                    }
                    else
                    {
                        targetPath = GenTargetPath(templatePath, targetParentFolderPath);
                        targetPath = ReplaceProjectFFPlaceholder(targetPath, projectInfo);

                        if (!Directory.Exists(targetPath))
                        {
                            Directory.CreateDirectory(targetPath);
                        }

                        RecursionGen(templatePath, targetPath, projectInfo);
                    }
                }
            }

            string[] templateFiles = Directory.GetFiles(templateFolderPath);

            if (templateFiles.Length > 0)
            {
                foreach (string filePath in templateFiles)
                {
                    if (FolderFileTemplatePlaceholder.CheckGenType(filePath, false) == GenType.TableLoopFile)
                    {
                        GenLoopTableFile(filePath, targetParentFolderPath, projectInfo);
                    }
                    else
                    {
                        GenProjectFile(filePath, targetParentFolderPath, projectInfo);
                    }
                }
            }
        }

        /// <summary>
        /// 循环所有表生成目录
        /// </summary>
        /// <param name="templateFolderPath">需要生成的模板目录路径</param>
        /// <param name="targetParentFolderPath">生成的目录所要保存的父目标目录</param>
        /// <param name="projectInfo">需要生成的项目信息对象</param>
        private static void GenLoopTableFolder(string templateFolderPath, string targetParentFolderPath, ProjectInfo projectInfo)
        {
            string targetFolderPath = GenTargetPath(templateFolderPath, targetParentFolderPath);
            string resultPath = string.Empty;

            foreach (TableInfo tableInfo in projectInfo.GenTableInfoList)
            {
                resultPath = ReplaceProjectFFPlaceholder(targetFolderPath, projectInfo);
                resultPath = ReplaceTableFFPlaceholder(resultPath, tableInfo);

                if (!Directory.Exists(resultPath))
                {
                    Directory.CreateDirectory(resultPath);
                }

                RecursionGen(templateFolderPath, resultPath, tableInfo);
            }
        }

        /// <summary>
        /// 生成项目文件
        /// </summary>
        /// <param name="templateFilePath">需要生成的模板文件路径</param>
        /// <param name="targetFolderPath">生成文件需要保存的目标目录</param>
        /// <param name="projectInfo">需要生成的项目信息对象</param>
        private static void GenProjectFile(string templateFilePath, string targetFolderPath, ProjectInfo projectInfo)
        {
            string resultPath = GenTargetPath(templateFilePath, targetFolderPath);
            resultPath = ReplaceProjectFFPlaceholder(resultPath, projectInfo);

            File.Copy(templateFilePath, resultPath, true);
            ReplaceFileContent(resultPath, projectInfo);
        }

        /// <summary>
        /// 生成项目文件
        /// </summary>
        /// <param name="templateFilePath">需要生成的模板文件路径</param>
        /// <param name="targetFolderPath">生成文件需要保存的目标目录</param>
        /// <param name="tableInfo">需要生成的表信息对象</param>
        private static void GenProjectFile(string templateFilePath, string targetFolderPath, TableInfo tableInfo)
        {
            if (!tableInfo.IsGen)
            {
                return;
            }

            string resultPath = GenTargetPath(templateFilePath, targetFolderPath);
            resultPath = ReplaceProjectFFPlaceholder(resultPath, tableInfo.CurrProjectInfo);
            resultPath = ReplaceTableFFPlaceholder(resultPath, tableInfo);

            File.Copy(templateFilePath, resultPath, true);
            ReplaceFileContent(resultPath, tableInfo);
        }

        /// <summary>
        /// 循环所有表生成文件
        /// </summary>
        /// <param name="templateFilePath">需要生成的模板文件路径</param>
        /// <param name="targetParentFolderPath">生成文件需要保存的目标目录</param>
        /// <param name="projectInfo">需要生成的项目信息对象</param>
        private static void GenLoopTableFile(string templateFilePath, string targetParentFolderPath, ProjectInfo projectInfo)
        {
            string targetFilePath = GenTargetPath(templateFilePath, targetParentFolderPath);
            string resultPath = string.Empty;

            foreach (TableInfo tableInfo in projectInfo.GenTableInfoList)
            {
                if (!tableInfo.IsGen)
                {
                    return;
                }
                
                resultPath = ReplaceProjectFFPlaceholder(targetFilePath, projectInfo);
                resultPath = ReplaceTableFFPlaceholder(resultPath, tableInfo);

                File.Copy(templateFilePath, resultPath, true);
                ReplaceFileContent(resultPath, tableInfo);
            }
        }

        /// <summary>
        /// 替换文件中的占位符
        /// </summary>
        /// <param name="filePath">生成后的文件路径</param>
        /// <param name="projectInfo">需要生成的项目信息对象</param>
        private static void ReplaceFileContent(string filePath, ProjectInfo projectInfo)
        {
            string fileContent = File.ReadAllText(filePath);
            fileContent = ReplaceProjectContentPlaceholder(fileContent, projectInfo);
            fileContent = ReplaceTableLoopContent(fileContent, projectInfo);
            fileContent = ReplaceFunctionSectionContent(fileContent);
            File.WriteAllText(filePath, fileContent, Encoding.UTF8);
            projectInfo.CurrGenFileList.Add(filePath);
        }

        /// <summary>
        /// 替换文件中的占位符
        /// </summary>
        /// <param name="filePath">生成后的文件路径</param>
        /// <param name="tableInfo">需要生成的表信息对象</param>
        private static void ReplaceFileContent(string filePath, TableInfo tableInfo)
        {
            string fileContent = File.ReadAllText(filePath);
            fileContent = ReplaceProjectContentPlaceholder(fileContent, tableInfo.CurrProjectInfo);
            fileContent = ReplaceTableContentPlaceholder(fileContent, tableInfo);
            fileContent = ReplaceColumnExistIFSectionContent(fileContent, tableInfo);
            fileContent = ReplaceIFSectionContent(fileContent, tableInfo);
            fileContent = ReplaceColumnLoopContent(fileContent, tableInfo);
            fileContent = ReplacePKLoopContent(fileContent, tableInfo);
            ReplaceTableIFSectionContent(fileContent, tableInfo, out fileContent);

            if (tableInfo.PKList.Count > 0)
            {
                fileContent = ReplacePKContentPlaceholder(fileContent, tableInfo.PKList[0]);
                ReplacePKColumnIFSectionContent(fileContent, tableInfo.PKList[0], out fileContent);
            }

            fileContent = ReplaceFKLoopContent(fileContent, tableInfo);
            fileContent = ReplaceRKLoopContent(fileContent, tableInfo);
            fileContent = ReplaceFunctionSectionContent(fileContent);
            File.WriteAllText(filePath, fileContent, Encoding.UTF8);
            tableInfo.CurrProjectInfo.CurrGenFileList.Add(filePath);
        }

        #endregion

        #region 占位符操作

        #region Loop Section

        /// <summary>
        /// 循环所有表替换内容
        /// </summary>
        /// <param name="content">需要替换的内容</param>
        /// <param name="projectInfo">项目信息对象</param>
        /// <returns>返回替换后的内容</returns>
        private static string ReplaceTableLoopContent(string content, ProjectInfo projectInfo)
        {
            string sectionContent = content;
            Regex regex = GetRegex(ContentTemplatePlaceholder.TableLoopSection);
            MatchCollection matchCollection = regex.Matches(sectionContent);
            string matchString = string.Empty;
            string contentString = string.Empty;
            string tmpString = string.Empty;

            foreach (Match match in matchCollection)
            {
                contentString = string.Empty;
                matchString = match.Value;
                matchString = RemovePlaceholder(ContentTemplatePlaceholder.TableLoopSection, matchString);

                foreach(TableInfo tableInfo in projectInfo.GenTableInfoList)
                {
                    if (CheckIgnoreParamForPlaceholder(match.Value, tableInfo))
                    {
                        continue;
                    }

                    tmpString = ReplaceTableContentPlaceholder(matchString, tableInfo);
                    tmpString = ReplaceColumnLoopContent(tmpString, tableInfo);
                    tmpString = ReplacePKLoopContent(tmpString, tableInfo);
                    tmpString = ReplaceColumnExistIFSectionContent(tmpString, tableInfo);
                    tmpString = ReplaceIFSectionContent(tmpString, tableInfo);
                    ReplaceTableIFSectionContent(tmpString, tableInfo, out tmpString);

                    if(tableInfo.PKList.Count > 0)
                    {
                        tmpString = ReplacePKContentPlaceholder(tmpString, tableInfo.PKList[0]);
                        ReplacePKColumnIFSectionContent(tmpString, tableInfo.PKList[0], out tmpString);
                    }

                    tmpString = ReplaceFKLoopContent(tmpString, tableInfo);
                    tmpString = ReplaceRKLoopContent(tmpString, tableInfo);
                    contentString += tmpString;
                }

                contentString = ReplaceParamForPlaceholder(match.Value, contentString);
                sectionContent = sectionContent.Replace(match.Value, contentString);
            }

            return sectionContent;
        }

        /// <summary>
        /// 循环所有主键替换内容
        /// </summary>
        /// <param name="content">需要替换的内容</param>
        /// <param name="tableInfo">表信息对象</param>
        /// <returns>返回替换后的内容</returns>
        private static string ReplacePKLoopContent(string content, TableInfo tableInfo)
        {
            string sectionContent = content;
            Regex regex = GetRegex(ContentTemplatePlaceholder.PKLoopSection);
            MatchCollection matchCollection = regex.Matches(sectionContent);
            string matchString = string.Empty;
            string contentString = string.Empty;
            string tmpString = string.Empty;

            foreach (Match match in matchCollection)
            {
                contentString = string.Empty;
                matchString = match.Value;
                matchString = RemovePlaceholder(ContentTemplatePlaceholder.PKLoopSection, matchString);

                foreach (ColumnInfo columnInfo in tableInfo.PKList)
                {
                    tmpString = ReplacePKContentPlaceholder(matchString, columnInfo);
                    ReplaceColumnIFSectionContent(tmpString, columnInfo, out tmpString);
                    contentString += tmpString;
                }

                contentString = ReplaceParamForPlaceholder(match.Value, contentString);
                sectionContent = sectionContent.Replace(match.Value, contentString);
            }

            return sectionContent;
        }

        /// <summary>
        /// 循环所有外键替换内容
        /// </summary>
        /// <param name="content">需要替换的内容</param>
        /// <param name="tableInfo">表信息对象</param>
        /// <returns>返回替换后的内容</returns>
        private static string ReplaceFKLoopContent(string content, TableInfo tableInfo)
        {
            string sectionContent = content;
            Regex regex = GetRegex(ContentTemplatePlaceholder.FKLoopSection);
            MatchCollection matchCollection = regex.Matches(sectionContent);
            string matchString = string.Empty;
            string contentString = string.Empty;
            string tmpString = string.Empty;

            foreach (Match match in matchCollection)
            {
                contentString = string.Empty;
                matchString = match.Value;
                matchString = RemovePlaceholder(ContentTemplatePlaceholder.FKLoopSection, matchString);

                foreach (ColumnInfo columnInfo in tableInfo.FKList)
                {
                    tmpString = ReplaceFKContentPlaceholder(matchString, columnInfo);
                    ReplaceColumnIFSectionContent(tmpString, columnInfo, out tmpString);
                    tmpString = ReplacePKContentPlaceholder(tmpString, columnInfo.FKColumn);

                    contentString += tmpString;
                }

                contentString = ReplaceParamForPlaceholder(match.Value, contentString);
                sectionContent = sectionContent.Replace(match.Value, contentString);
            }

            return sectionContent;
        }

        /// <summary>
        /// 循环所有关联键替换内容
        /// </summary>
        /// <param name="content">需要替换的内容</param>
        /// <param name="tableInfo">表信息对象</param>
        /// <returns>返回替换后的内容</returns>
        private static string ReplaceRKLoopContent(string content, TableInfo tableInfo)
        {
            string sectionContent = content;
            Regex regex = GetRegex(ContentTemplatePlaceholder.RKLoopSection);
            MatchCollection matchCollection = regex.Matches(sectionContent);
            string matchString = string.Empty;
            string contentString = string.Empty;
            string tmpString = string.Empty;

            foreach (Match match in matchCollection)
            {
                contentString = string.Empty;
                matchString = match.Value;
                matchString = RemovePlaceholder(ContentTemplatePlaceholder.RKLoopSection, matchString);

                foreach (ColumnInfo columnInfo in tableInfo.RKList)
                {
                    tmpString = ReplaceRKContentPlaceholder(matchString, columnInfo);
                    ReplaceColumnIFSectionContent(tmpString, columnInfo, out tmpString);

                    if (columnInfo.CurrTable.PKList.Count > 0)
                    {
                        tmpString = ReplacePKContentPlaceholder(tmpString, columnInfo.CurrTable.PKList[0]);
                    }

                    tmpString = ReplaceFKContentPlaceholder(tmpString, columnInfo);
                    contentString += tmpString;
                }

                contentString = ReplaceParamForPlaceholder(match.Value, contentString);
                sectionContent = sectionContent.Replace(match.Value, contentString);
            }

            return sectionContent;
        }

        /// <summary>
        /// 循环所有字段替换内容
        /// </summary>
        /// <param name="content">需要替换的内容</param>
        /// <param name="tableInfo">表信息对象</param>
        /// <returns>返回替换后的内容</returns>
        private static string ReplaceColumnLoopContent(string content, TableInfo tableInfo)
        {
            string sectionContent = content;
            Regex regex = GetRegex(ContentTemplatePlaceholder.ColumnLoopSection);
            MatchCollection matchCollection = regex.Matches(sectionContent);
            string matchString = string.Empty;
            string contentString = string.Empty;
            string tmpString = string.Empty;

            foreach (Match match in matchCollection)
            {
                contentString = string.Empty;
                matchString = match.Value;
                matchString = RemovePlaceholder(ContentTemplatePlaceholder.ColumnLoopSection, matchString);

                foreach (ColumnInfo columnInfo in tableInfo.ColumnList)
                {
                    if (CheckIgnoreParamForPlaceholder(match.Value, columnInfo))
                    {
                        continue;
                    }

                    tmpString = ReplaceColumnContentPlaceholder(matchString, columnInfo);
                    ReplaceColumnIFSectionContent(tmpString, columnInfo, out tmpString);
                    contentString += tmpString;
                }

                contentString = ReplaceParamForPlaceholder(match.Value, contentString);
                sectionContent = sectionContent.Replace(match.Value, contentString);
            }

            return sectionContent;
        }

        #endregion

        #region IF Section

        /// <summary>
        /// 判断占位符替换，如果内部没有判断占位符或内部的判断占位符为返回的内容都为空字符串，则整段内容返回空字符串
        /// </summary>
        /// <param name="content">需要替换的内容</param>
        /// <param name="tableInfo">表信息对象</param>
        /// <returns>返回替换后的内容</returns>
        private static string ReplaceIFSectionContent(string content, TableInfo tableInfo)
        {
            string contentString = content;
            Regex regex = GetRegex(ContentTemplatePlaceholder.IFSection);
            MatchCollection matchCollection = regex.Matches(contentString);
            string matchString = string.Empty;
            string tmpString = string.Empty;
            int count = 0;

            foreach (Match match in matchCollection)
            {
                matchString = match.Value;
                matchString = RemovePlaceholder(ContentTemplatePlaceholder.IFSection, matchString);
                count = ReplaceColumnLoopIFSectionContent(matchString, tableInfo, out tmpString);

                if (count == 0)
                {
                    contentString = contentString.Replace(match.Value, string.Empty);
                }
                else
                {
                    contentString = contentString.Replace(match.Value, tmpString);
                }
            }

            return contentString;
        }

        /// <summary>
        /// 循环所有字段，进行判断占位符替换
        /// </summary>
        /// <param name="content">需要替换的内容</param>
        /// <param name="tableInfo">表信息对象</param>
        /// <param name="resultString">输出替换后的内容</param>
        /// <returns>返回有内容的判断占位符个数，如果为0则说明该内容中的所有判断占位符都没能返回内容</returns>
        private static int ReplaceColumnLoopIFSectionContent(string content, TableInfo tableInfo, out string resultString)
        {
            resultString = content;
            Regex regex = GetRegex(ContentTemplatePlaceholder.ColumnLoopIFSection);
            MatchCollection matchCollection = regex.Matches(resultString);
            string matchString = string.Empty;
            string contentString = string.Empty;
            string tmpString = string.Empty;
            string tmpResultString = string.Empty;
            int count = 0;
            int hasContentCount = 0;

            foreach (Match match in matchCollection)
            {
                contentString = string.Empty;
                matchString = match.Value;
                matchString = RemovePlaceholder(ContentTemplatePlaceholder.ColumnLoopIFSection, matchString);

                foreach (ColumnInfo columnInfo in tableInfo.ColumnList)
                {
                    if(CheckIgnoreParamForPlaceholder(match.Value, columnInfo))
                    {
                        continue;
                    }

                    tmpString = ReplaceColumnContentPlaceholder(matchString, columnInfo);
                    count += ReplaceColumnIFSectionContent(tmpString, columnInfo, out tmpResultString);
                    tmpResultString = ReplaceFunctionSectionContent(tmpResultString);
                    contentString += tmpResultString;
                }
                
                if (count == 0)
                {
                    resultString = resultString.Replace(match.Value, string.Empty);
                }
                else
                {
                    contentString = ReplaceParamForPlaceholder(match.Value, contentString);
                    resultString = resultString.Replace(match.Value, contentString);
                    hasContentCount++;
                }
            }

            return hasContentCount;
        }

        /// <summary>
        /// 主键判断占位符替换
        /// </summary>
        /// <param name="content">需要替换的内容</param>
        /// <param name="columnInfo">字段信息对象</param>
        /// <param name="resultString">输出替换后的内容</param>
        /// <returns>返回有内容的判断占位符个数，如果为0则说明该内容中的所有判断占位符都没能返回内容</returns>
        private static int ReplacePKColumnIFSectionContent(string content, ColumnInfo columnInfo, out string resultString)
        {
            content = content.Replace(":pk:", ":");
            return ReplaceColumnIFSectionContent(content, columnInfo, out resultString);
        }

        /// <summary>
        /// 字段判断占位符替换，判断内容中是否有必填，最大，最小值，字符串，数字等
        /// </summary>
        /// <param name="content">需要替换的内容</param>
        /// <param name="columnInfo">字段信息对象</param>
        /// <param name="resultString">输出替换后的内容</param>
        /// <returns>返回有内容的判断占位符个数，如果为0则说明该内容中的所有判断占位符都没能返回内容</returns>
        private static int ReplaceColumnIFSectionContent(string content, ColumnInfo columnInfo, out string resultString)
        {
            string contentString = content;
            int hasContentCount = 0;

            // Required
            hasContentCount += ReplaceConditionIFSectionContent(ContentTemplatePlaceholder.ColumnRequiredIFSection, contentString, !columnInfo.IsNullable, out contentString);
            // Max Length
            hasContentCount += ReplaceConditionIFSectionContent(ContentTemplatePlaceholder.ColumnMaxLenIFSection, contentString, columnInfo.IsMaxLen, out contentString);
            // Max
            hasContentCount += ReplaceConditionIFSectionContent(ContentTemplatePlaceholder.ColumnMaxIFSection, contentString, columnInfo.IsMax, out contentString);
            // Min
            hasContentCount += ReplaceConditionIFSectionContent(ContentTemplatePlaceholder.ColumnMinIFSection, contentString, columnInfo.IsMin, out contentString);
            // Unique
            hasContentCount += ReplaceConditionIFSectionContent(ContentTemplatePlaceholder.ColumnUniqueIFSection, contentString, columnInfo.IsUnique, out contentString);

            // String
            hasContentCount += ReplaceConditionIFSectionContent(ContentTemplatePlaceholder.ColumnStringIFSection, contentString, columnInfo.IsString, out contentString);
            // Number
            hasContentCount += ReplaceConditionIFSectionContent(ContentTemplatePlaceholder.ColumnNumberIFSection, contentString, columnInfo.IsNumber, out contentString);
            // Integer
            hasContentCount += ReplaceConditionIFSectionContent(ContentTemplatePlaceholder.ColumnIntegerIFSection, contentString, columnInfo.IsInteger, out contentString);
            // DateTime
            hasContentCount += ReplaceConditionIFSectionContent(ContentTemplatePlaceholder.ColumnDateTimeIFSection, contentString, columnInfo.IsDateTime, out contentString);

            resultString = contentString;
            return hasContentCount;
        }

        /// <summary>
        /// 表判断占位符替换
        /// </summary>
        /// <param name="content">需要替换的内容</param>
        /// <param name="tableInfo">表信息对象</param>
        /// <param name="resultString">输出替换后的内容</param>
        /// <returns>返回有内容的判断占位符个数，如果为0则说明该内容中的所有判断占位符都没能返回内容</returns>
        private static int ReplaceTableIFSectionContent(string content, TableInfo tableInfo, out string resultString)
        {
            string contentString = content;
            int hasContentCount = 0;

            // Many PK
            hasContentCount += ReplaceConditionIFSectionContent(ContentTemplatePlaceholder.PKMIFSection, contentString, (tableInfo.PKList.Count > 1), out contentString);
            // Single PK
            hasContentCount += ReplaceConditionIFSectionContent(ContentTemplatePlaceholder.PKSIFSection, contentString, (tableInfo.PKList.Count == 1), out contentString);
            // Many FK
            hasContentCount += ReplaceConditionIFSectionContent(ContentTemplatePlaceholder.FKMIFSection, contentString, (tableInfo.FKList.Count > 1), out contentString);
            // Single FK
            hasContentCount += ReplaceConditionIFSectionContent(ContentTemplatePlaceholder.FKSIFSection, contentString, (tableInfo.FKList.Count == 1), out contentString);
            // Many RK
            hasContentCount += ReplaceConditionIFSectionContent(ContentTemplatePlaceholder.RKMIFSection, contentString, (tableInfo.RKList.Count > 1), out contentString);
            // Single RK
            hasContentCount += ReplaceConditionIFSectionContent(ContentTemplatePlaceholder.RKSIFSection, contentString, (tableInfo.RKList.Count == 1), out contentString);

            resultString = contentString;
            return hasContentCount;
        }

        /// <summary>
        /// 判断占位符替换
        /// </summary>
        /// <param name="contentTemplatePlaceholder">需要查找替换的占位符</param>
        /// <param name="content">需要替换的内容</param>
        /// <param name="isGen">是否生成：true为生成，false为不生成</param>
        /// <param name="resultString">输出替换后的内容</param>
        /// <returns>返回有内容的判断占位符个数，如果为0则说明该内容中的所有判断占位符都没能返回内容</returns>
        private static int ReplaceConditionIFSectionContent(string contentTemplatePlaceholder, string content, bool isGen, out string resultString)
        {
            resultString = content;
            Regex regex = GetRegex(contentTemplatePlaceholder);
            MatchCollection matchCollection = regex.Matches(resultString);
            string matchString = string.Empty;
            string contentString = string.Empty;
            int hasContentCount = 0;
            
            foreach (Match match in matchCollection)
            {
                if (isGen)
                {
                    matchString = match.Value;
                    matchString = RemovePlaceholder(contentTemplatePlaceholder, matchString);
                    resultString = resultString.Replace(match.Value, matchString);
                    hasContentCount++;
                }
                else
                {
                    resultString = resultString.Replace(match.Value, string.Empty);
                }
            }

            return hasContentCount;
        }

        /// <summary>
        /// 当特定字段存在时才生成内容
        /// </summary>
        /// <param name="content">需要替换的内容</param>
        /// <param name="tableInfo">表信息对象</param>
        /// <returns>返回替换后的内容</returns>
        private static string ReplaceColumnExistIFSectionContent(string content, TableInfo tableInfo)
        {
            string contentString = content;
            Regex regex = GetRegex(ContentTemplatePlaceholder.ColumnExistIFSection);
            MatchCollection matchCollection = regex.Matches(contentString);
            string matchString = string.Empty;
            string tagHead = string.Empty;
            string[] paramArr = null;
            bool isExist = false;

            foreach (Match match in matchCollection)
            {
                matchString = match.Value;
                tagHead = matchString.Substring(0, matchString.IndexOf("}}") + 2);
                tagHead = tagHead.Replace("}}", "").Replace("{{", "");
                paramArr = tagHead.Split('|');

                if(paramArr.Length <= 1)
                {
                    isExist = false;
                }
                else
                {
                    isExist = (tableInfo.ColumnList.Count(c => c.NameLow.Equals(paramArr[1].ToLower())) > 0);
                }

                matchString = RemovePlaceholder(ContentTemplatePlaceholder.ColumnExistIFSection, matchString);

                if (!isExist)
                {
                    contentString = contentString.Replace(match.Value, string.Empty);
                }
                else
                {
                    contentString = contentString.Replace(match.Value, matchString);
                }
            }

            return contentString;
        }

        #endregion

        #region Function Section

        /// <summary>
        /// 功能性占位符替换：如替换第一或最后一个指定字符串为其他的字符串
        /// </summary>
        /// <param name="content">需要替换的内容</param>
        /// <returns>返回替换后的内容</returns>
        private static string ReplaceFunctionSectionContent(string content)
        {
            Regex regex = null;
            Match match = null;
            string matchString = string.Empty;
            string contentString = content;
            int startPosition = 0;
            string[] paramArr = null;
            string exStr = string.Empty;
            string replaceStr = string.Empty;
            int matchCount = 0;

            regex = GetRegex(ContentTemplatePlaceholder.ReplaceFirstSection);
            match = regex.Match(contentString);

            if (match.Success)
            {
                contentString = regex.Replace(contentString, string.Empty, 1);
                replaceStr = string.Empty;
                matchString = match.Value;
                matchString = RemovePlaceholder(ContentTemplatePlaceholder.ReplaceFirstSection, matchString);
                paramArr = matchString.Split('|');

                if (paramArr.Length > 1)
                {
                    replaceStr = paramArr[1];
                }

                exStr = paramArr[0];
                exStr = exStr.Replace(@"\n", System.Environment.NewLine);
                startPosition = contentString.Substring(match.Index).IndexOf(exStr);

                if (startPosition > -1)
                {
                    contentString = contentString.Substring(0, startPosition) + replaceStr + contentString.Substring(startPosition + exStr.Length);
                }

                matchCount++;
            }

            regex = GetRegex(ContentTemplatePlaceholder.ReplaceLastSection);
            match = regex.Match(contentString);

            if (match.Success)
            {
                contentString = regex.Replace(contentString, string.Empty, 1);
                replaceStr = string.Empty;
                matchString = match.Value;
                matchString = RemovePlaceholder(ContentTemplatePlaceholder.ReplaceLastSection, matchString);
                
                paramArr = matchString.Split('|');

                if (paramArr.Length > 1)
                {
                    replaceStr = paramArr[1];
                }

                exStr = paramArr[0];
                exStr = exStr.Replace(@"\n", System.Environment.NewLine);
                startPosition = contentString.Substring(0, match.Index).LastIndexOf(exStr);

                if (startPosition > -1)
                {
                    contentString = contentString.Substring(0, startPosition) + replaceStr + contentString.Substring(startPosition + matchString.Length);
                }

                matchCount++;
            }

            if (matchCount > 0)
            {
                contentString = ReplaceFunctionSectionContent(contentString);
            }

            return contentString;
        }

        #endregion

        #region Placeholder Param

        /// <summary>
        /// 检测当前字段信息是否为排除项
        /// </summary>
        /// <param name="matchString">正则匹配出来的字符串</param>
        /// <param name="columnInfo">字段信息对象</param>
        /// <returns>如果是排除内容则返回true，如果不是则返回false</returns>
        private static bool CheckIgnoreParamForPlaceholder(string matchString, ColumnInfo columnInfo)
        {
            if (string.IsNullOrWhiteSpace(matchString) || columnInfo == null)
            {
                return false;
            }

            bool isIgnore = false;
            Regex regex = null;
            Match match = null;
            string[] paramArr = null;
            string tmpParam = string.Empty;
            string tagParamStr = string.Empty;
            string tagHead = matchString.Substring(0, matchString.IndexOf("}}") + 2);
            tagHead = tagHead.Replace("}}", "|}}").Replace("|", "||");
            regex = new Regex(@"\|ignparam\:(.)*?\|");
            match = regex.Match(tagHead);

            if (match.Success)
            {
                if (match.Value.IndexOf(":") > -1)
                {
                    tagParamStr = match.Value.Replace("|", string.Empty).Split(':')[1];

                    if (string.IsNullOrWhiteSpace(tagParamStr))
                    {
                        return false;
                    }
                }

                paramArr = tagParamStr.Split(',');

                foreach (string param in paramArr)
                {
                    tmpParam = param.ToLower().Trim();

                    if ("pk".Equals(tmpParam) && columnInfo.IsPK)
                    {
                        isIgnore = true;
                        continue;
                    }

                    if ("fk".Equals(tmpParam) && columnInfo.IsFK)
                    {
                        isIgnore = true;
                        continue;
                    }

                    if (columnInfo.NameLow.Equals(tmpParam))
                    {
                        isIgnore = true;
                        continue;
                    }
                }
            }

            return isIgnore;
        }

        /// <summary>
        /// 检测当前表信息是否为排除项
        /// </summary>
        /// <param name="matchString">正则匹配出来的字符串</param>
        /// <param name="tableInfo">表信息对象</param>
        /// <returns>返回替换后的内容</returns>
        private static bool CheckIgnoreParamForPlaceholder(string matchString, TableInfo tableInfo)
        {
            if (string.IsNullOrWhiteSpace(matchString) || tableInfo == null)
            {
                return false;
            }

            bool isIgnore = false;
            Regex regex = null;
            Match match = null;
            string[] paramArr = null;
            string tmpParam = string.Empty;
            string tagParamStr = string.Empty;
            string tagHead = matchString.Substring(0, matchString.IndexOf("}}") + 1);
            tagHead = tagHead.Replace("}}", "|}}").Replace("|", "||");
            regex = new Regex(@"\|ignparam\:(.)*?\|");
            match = regex.Match(tagHead);

            if (match.Success)
            {
                if (match.Value.IndexOf(":") > -1)
                {
                    tagParamStr = match.Value.Split(':')[1];

                    if (string.IsNullOrWhiteSpace(tagParamStr))
                    {
                        return false;
                    }
                }

                paramArr = tagParamStr.Split(',');

                foreach (string param in paramArr)
                {
                    tmpParam = param.ToLower().Trim();

                    if ("pk".Equals(tmpParam))
                    {
                        isIgnore = true;
                        continue;
                    }

                    if ("fk".Equals(tmpParam))
                    {
                        isIgnore = true;
                        continue;
                    }

                    if ("rk".Equals(tmpParam))
                    {
                        isIgnore = true;
                        continue;
                    }

                    if (tableInfo.NameLow.Equals(tmpParam))
                    {
                        isIgnore = true;
                        continue;
                    }
                }
            }

            return isIgnore;
        }
        
        /// <summary>
        /// 针对标签中的参数进行内容替换
        /// </summary>
        /// <param name="matchString">正则匹配出来的字符串</param>
        /// <param name="content">需要替换的内容</param>
        /// <returns>返回替换后的内容</returns>
        private static string ReplaceParamForPlaceholder(string matchString, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return content;
            }

            string resultString = content;
            Regex regex = null;
            MatchCollection matchCollection = null;
            int startPosition = 0;
            string exStr = string.Empty;
            string replaceStr = string.Empty;
            string[] paramArr = null; 
            string tagHead = matchString.Substring(0, matchString.IndexOf("}}") + 2);
            tagHead = tagHead.Replace("}}", "|}}").Replace("|", "||");
            regex = new Regex(@"\|rfirst\:(.)*?\|");
            matchCollection = regex.Matches(tagHead);
            
            foreach (Match match in matchCollection)
            {
                paramArr = match.Value.Replace("|", "").Split(':');

                if (paramArr.Length < 2)
                {
                    continue;
                }

                exStr = paramArr[1];
                exStr = exStr.Replace(@"\n", System.Environment.NewLine);
                startPosition = resultString.IndexOf(exStr);

                if (startPosition > -1)
                {
                    if (paramArr.Length > 2)
                    {
                        replaceStr = paramArr[2];
                    }

                    resultString = resultString.Substring(0, startPosition) + replaceStr + resultString.Substring(startPosition + paramArr[1].Length);
                }
            }

            regex = new Regex(@"\|rlast\:(.)*?\|");
            matchCollection = regex.Matches(tagHead);

            foreach (Match match in matchCollection)
            {
                paramArr = match.Value.Replace("|", "").Split(':');

                if (paramArr.Length < 2)
                {
                    continue;
                }

                exStr = paramArr[1];
                exStr = exStr.Replace(@"\n", System.Environment.NewLine);
                startPosition = resultString.LastIndexOf(exStr);

                if (startPosition > -1)
                {
                    if (paramArr.Length > 2)
                    {
                        replaceStr = paramArr[2];
                    }

                    resultString = resultString.Substring(0, startPosition) + replaceStr + resultString.Substring(startPosition + paramArr[1].Length);
                }
            }

            return resultString;
        }

        #endregion

        #region Placeholder Replace

        /// <summary>
        /// 替换项目在物理目录和文件名称上的占位符内容
        /// </summary>
        /// <param name="ffPath">目录或文件目录路径</param>
        /// <param name="projectInfo">项目信息对象</param>
        /// <returns>返回替换后的内容</returns>
        private static string ReplaceProjectFFPlaceholder(string ffPath, ProjectInfo projectInfo)
        {
            string targetFolderPath = ffPath.Replace(FolderFileTemplatePlaceholder.ProjectName, projectInfo.Name)
                                            .Replace(FolderFileTemplatePlaceholder.ProjectNameLow, projectInfo.NameLow)
                                            .Replace(FolderFileTemplatePlaceholder.ProjectNameUp, projectInfo.NameUp)
                                            .Replace(FolderFileTemplatePlaceholder.ProjectNameLowFirst, projectInfo.NameLowFirst)
                                            .Replace(FolderFileTemplatePlaceholder.ProjectNamespace, projectInfo.Namespace)
                                            .Replace(FolderFileTemplatePlaceholder.ProjectDisplayName, projectInfo.DisplayName);

            return targetFolderPath;
        }

        /// <summary>
        /// 替换表在物理目录和文件名称上的占位符内容
        /// </summary>
        /// <param name="ffPath">目录或文件目录路径</param>
        /// <param name="tableInfo">表信息对象</param>
        /// <returns>返回替换后的内容</returns>
        private static string ReplaceTableFFPlaceholder(string ffPath, TableInfo tableInfo)
        {
            string targetFolderPath = ffPath.Replace(FolderFileTemplatePlaceholder.TableName, tableInfo.Name)
                                            .Replace(FolderFileTemplatePlaceholder.TableNameLow, tableInfo.NameLow)
                                            .Replace(FolderFileTemplatePlaceholder.TableNameUp, tableInfo.NameUp)
                                            .Replace(FolderFileTemplatePlaceholder.TableNameLowFirst, tableInfo.NameLowFirst)
                                            .Replace(FolderFileTemplatePlaceholder.TableLoop, string.Empty);

            return targetFolderPath;
        }

        /// <summary>
        /// 替换项目占位符内容
        /// </summary>
        /// <param name="content">需要替换的内容</param>
        /// <param name="projectInfo">项目信息对象</param>
        /// <returns>返回替换后的内容</returns>
        private static string ReplaceProjectContentPlaceholder(string content, ProjectInfo projectInfo)
        {
            string result = content.Replace(ContentTemplatePlaceholder.ProjectName, projectInfo.Name)
                                   .Replace(ContentTemplatePlaceholder.ProjectNameLow, projectInfo.NameLow)
                                   .Replace(ContentTemplatePlaceholder.ProjectNameUp, projectInfo.NameUp)
                                   .Replace(ContentTemplatePlaceholder.ProjectNameLowFirst, projectInfo.NameLowFirst)
                                   .Replace(ContentTemplatePlaceholder.ProjectNamespace, projectInfo.Namespace)
                                   .Replace(ContentTemplatePlaceholder.ProjectDisplayName, projectInfo.DisplayName)
                                   .Replace(ContentTemplatePlaceholder.ProjectReference, projectInfo.ReferenceRootFolder);

            return result;
        }

        /// <summary>
        /// 替换表占位符内容
        /// </summary>
        /// <param name="content">需要替换的内容</param>
        /// <param name="tableInfo">表信息对象</param>
        /// <returns>返回替换后的内容</returns>
        private static string ReplaceTableContentPlaceholder(string content, TableInfo tableInfo)
        {
            string result = content.Replace(ContentTemplatePlaceholder.TableName, tableInfo.Name)
                                   .Replace(ContentTemplatePlaceholder.TableNameLow, tableInfo.NameLow)
                                   .Replace(ContentTemplatePlaceholder.TableNameUp, tableInfo.NameUp)
                                   .Replace(ContentTemplatePlaceholder.TableNameLowFirst, tableInfo.NameLowFirst)
                                   .Replace(ContentTemplatePlaceholder.TableComment, tableInfo.Comment);

            return result;
        }

        /// <summary>
        /// 替换字段占位符内容
        /// </summary>
        /// <param name="content">需要替换的内容</param>
        /// <param name="columnInfo">字段信息对象</param>
        /// <returns>返回替换后的内容</returns>
        private static string ReplaceColumnContentPlaceholder(string content, ColumnInfo columnInfo)
        {
            string result = content.Replace(ContentTemplatePlaceholder.ColumnName, columnInfo.Name)
                                   .Replace(ContentTemplatePlaceholder.ColumnNameLow, columnInfo.NameLow)
                                   .Replace(ContentTemplatePlaceholder.ColumnNameUp, columnInfo.NameUp)
                                   .Replace(ContentTemplatePlaceholder.ColumnNameLowFirst, columnInfo.NameLowFirst)
                                   .Replace(ContentTemplatePlaceholder.ColumnComment, columnInfo.Comment)
                                   .Replace(ContentTemplatePlaceholder.ColumnPrecision, columnInfo.Precision.ToString())
                                   .Replace(ContentTemplatePlaceholder.ColumnScale, columnInfo.Scale.ToString())
                                   .Replace(ContentTemplatePlaceholder.ColumnCodeType, columnInfo.CodeType)
                                   .Replace(ContentTemplatePlaceholder.ColumnSqlType, columnInfo.SqlType)
                                   .Replace(ContentTemplatePlaceholder.ColumnDalType, columnInfo.DalType)
                                   .Replace(ContentTemplatePlaceholder.ColumnMaxLength, columnInfo.MaxLength.ToString())
                                   .Replace(ContentTemplatePlaceholder.ColumnMax, columnInfo.MaxValue)
                                   .Replace(ContentTemplatePlaceholder.ColumnMin, columnInfo.MinValue)
                                   .Replace(ContentTemplatePlaceholder.ColumnFKName, columnInfo.FKName)
                                   .Replace(ContentTemplatePlaceholder.ColumnId, columnInfo.ColId.ToString())
                                   .Replace(ContentTemplatePlaceholder.ColumnDefaultValue, columnInfo.CurrTable.CurrProjectInfo.CodeInfoGetter.GetDefaultValueString(columnInfo.DefaultValue, columnInfo.DbType));

            Regex regex = null;
            MatchCollection matchCollection = null; 
            string matchString = string.Empty;

            #region Convert To Code Type

            regex = GetRegex(ContentTemplatePlaceholder.ColumnConvert);
            matchCollection = regex.Matches(result);
            
            foreach (Match match in matchCollection)
            {
                matchString = RemovePlaceholder(ContentTemplatePlaceholder.ColumnConvert, match.Value);
                result = result.Replace(match.Value, columnInfo.CurrTable.CurrProjectInfo.CodeInfoGetter.GetConvertString(matchString, columnInfo.CodeType));
            }

            #endregion

            #region To String

            regex = GetRegex(ContentTemplatePlaceholder.ColumnToString);
            matchCollection = regex.Matches(result);

            foreach (Match match in matchCollection)
            {
                matchString = RemovePlaceholder(ContentTemplatePlaceholder.ColumnToString, match.Value);
                result = result.Replace(match.Value, columnInfo.CurrTable.CurrProjectInfo.CodeInfoGetter.GetToString(matchString, columnInfo.CodeType));
            }

            #endregion

            return result;
        }

        /// <summary>
        /// 替换主键占位符内容
        /// </summary>
        /// <param name="content">需要替换的内容</param>
        /// <param name="pkColumnInfo">主键信息对象</param>
        /// <returns>返回替换后的内容</returns>
        private static string ReplacePKContentPlaceholder(string content, ColumnInfo pkColumnInfo)
        {
            string result = content.Replace("{{pk:", "{{");
            result = ReplaceColumnContentPlaceholder(result, pkColumnInfo);
            return result;
        }

        /// <summary>
        /// 替换外键占位符内容
        /// </summary>
        /// <param name="content">需要替换的内容</param>
        /// <param name="fkColumnInfo">外键信息对象</param>
        /// <returns>返回替换后的内容</returns>
        private static string ReplaceFKContentPlaceholder(string content, ColumnInfo fkColumnInfo)
        {
            string result = content.Replace("{{fk:", "{{");
            result = ReplaceTableContentPlaceholder(result, fkColumnInfo.FKColumn.CurrTable);
            result = ReplaceColumnContentPlaceholder(result, fkColumnInfo);
            return result;
        }

        /// <summary>
        /// 替换关联键占位符内容
        /// </summary>
        /// <param name="content">需要替换的内容</param>
        /// <param name="rkColumnInfo">关联键信息对象</param>
        /// <returns>返回替换后的内容</returns>
        private static string ReplaceRKContentPlaceholder(string content, ColumnInfo rkColumnInfo)
        {
            string result = content.Replace("{{rk:", "{{");
            result = ReplaceTableContentPlaceholder(result, rkColumnInfo.CurrTable);
            result = ReplaceColumnContentPlaceholder(result, rkColumnInfo);
            return result;
        }

        #endregion

        #region Utils

        /// <summary>
        /// 根据模板文件或目录的路径层次生成目标文件或目录的
        /// </summary>
        /// <param name="templatePath">模板文件或目录路径</param>
        /// <param name="targetPath">需要生成文件或目录的父目录路径</param>
        /// <returns>返回需要生成的路径字符串</returns>
        private static string GenTargetPath(string templatePath, string targetPath)
        {
            string tmpPath = templatePath.Replace("/", @"\");

            if (tmpPath.LastIndexOf(@"\") + 1 == tmpPath.Length)
            {
                tmpPath = tmpPath.Substring(0, tmpPath.LastIndexOf(@"\"));
            }

            string name = templatePath.Substring(tmpPath.LastIndexOf(@"\") + 1);
            string resultPath = Path.Combine(targetPath, name);
            return resultPath;
        }

        /// <summary>
        /// 返回双标签（区域型）匹配正则对象
        /// </summary>
        /// <param name="placeholder">需要匹配的占位符</param>
        /// <returns>返回正则表达式对象</returns>
        private static Regex GetRegex(string placeholder)
        {
            string regexStr = placeholder.Replace("{0}", @"(.|\s)*?").Replace("{", @"\{").Replace("}", @"\}").Replace(":", @"\:").Replace("/", @"\/");
            regexStr = regexStr + "|" + regexStr.Insert(regexStr.IndexOf(@"\}"), @"\|(.)*?");
            Regex regex = new Regex(regexStr, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            return regex;
        }

        /// <summary>
        /// 替换单标签占位符
        /// </summary>
        /// <param name="placeholder">需要替换的占位符</param>
        /// <param name="content">需要查找占位符的内容</param>
        /// <returns>返回替换后的内容</returns>
        private static string RemovePlaceholder(string placeholder, string content)
        {
            string[] placeholderEleArray = placeholder.Replace("{0}", "|").Split('|');
            string regexStr = placeholderEleArray[0].Insert(placeholderEleArray[0].IndexOf("}"), "(.)*?");
            regexStr = regexStr.Replace("{", @"\{").Replace("}", @"\}").Replace(":", @"\:").Replace("/", @"\/");
            Regex regex = new Regex(regexStr, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            string replaceString = content;
            Match match = regex.Match(replaceString);
            replaceString = replaceString.Substring(match.Index + match.Value.Length);

            if (placeholderEleArray.Length > 1 && !string.IsNullOrWhiteSpace(replaceString))
            {
                replaceString = replaceString.Substring(0, replaceString.LastIndexOf(placeholderEleArray[1]));
            }
            
            return replaceString;
        }

        #endregion

        #endregion

        #endregion
    }
}
