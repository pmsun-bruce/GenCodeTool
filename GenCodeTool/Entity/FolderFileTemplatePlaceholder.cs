namespace NFramework.GenCodeTool.Entity
{
    #region Reference

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    #endregion

    /// <summary>
    /// 物理目录和文件占位符
    /// </summary>
    public class FolderFileTemplatePlaceholder
    {
        #region Project Info

        /// <summary>
        /// 项目名称
        /// </summary>
        public const string ProjectName = "{{project-name}}";
        /// <summary>
        /// 全小写项目名称
        /// </summary>
        public const string ProjectNameLow = "{{project-lname}}";
        /// <summary>
        /// 全大写项目名称
        /// </summary>
        public const string ProjectNameUp = "{{project-uname}}";
        /// <summary>
        /// 首字母小写项目名称
        /// </summary>
        public const string ProjectNameLowFirst = "{{project-lfname}}";
        /// <summary>
        /// 项目命名空间
        /// </summary>
        public const string ProjectNamespace = "{{project-namespace}}";
        /// <summary>
        /// 项目的显示名称，即说明
        /// </summary>
        public const string ProjectDisplayName = "{{project-displayname}}";

        #endregion

        #region Table Info

        /// <summary>
        /// 表循环，即循环所有需要生成的表信息对象
        /// </summary>
        public const string TableLoop = "{{table-loop}}";
        /// <summary>
        /// 表名称
        /// </summary>
        public const string TableName = "{{table-name}}";
        /// <summary>
        /// 类名称
        /// </summary>
        public const string ClassName = "{{table-cname}}";
        /// <summary>
        /// 全小写表名称
        /// </summary>
        public const string ClassNameLow = "{{table-lcname}}";
        /// <summary>
        /// 全大写表名称
        /// </summary>
        public const string ClassNameUp = "{{table-ucname}}";
        /// <summary>
        /// 首字母小写的表名称
        /// </summary>
        public const string ClassNameLowFirst = "{{table-lfcname}}";

        #endregion

        #region Public Static Methods

        /// <summary>
        /// 根据路径判断生成的物理目录或文件类型
        /// </summary>
        /// <param name="path">所要生成的模板目录或文件路径</param>
        /// <param name="isFolder">需要生成的是否为目录，true为目录，false为文件</param>
        /// <returns>返回生成类型</returns>
        public static GenType CheckGenType(string path, bool isFolder)
        {
            Regex loopTableReg = new Regex(@"\{\{table\-loop\}\}");
            Regex tableReg = new Regex(@"\{\{table\-(.)*?\}\}");

            if (isFolder)
            {
                if (loopTableReg.IsMatch(path))
                {
                    return GenType.TableLoopFolder;
                }

                if (tableReg.IsMatch(path))
                {
                    return GenType.TableFolder;
                }

                return GenType.ProjectFolder;
            }
            else
            {
                if (loopTableReg.IsMatch(path))
                {
                    return GenType.TableLoopFile;
                }

                if (tableReg.IsMatch(path))
                {
                    return GenType.TableFile;
                }

                return GenType.ProjectFile;
            }
        }

        #endregion
    }
}
