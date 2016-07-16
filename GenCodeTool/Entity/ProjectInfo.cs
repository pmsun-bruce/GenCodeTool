namespace NFramework.GenCodeTool.Entity
{
    #region Reference

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NFramework.GenCodeTool.CodeInfoGetter;
    using NFramework.GenCodeTool.DBInfoGetter;

    #endregion

    /// <summary>
    /// 项目信息
    /// </summary>
    public class ProjectInfo
    {
        #region Fields & Properties

        /// <summary>
        /// 项目名称，用于项目的物理文件，目录等的生成
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 全小写项目名称
        /// </summary>
        private string nameLow;
        /// <summary>
        /// 全小写项目名称
        /// </summary>
        public string NameLow
        {
            get
            {
                if (string.IsNullOrWhiteSpace(nameLow) && !string.IsNullOrWhiteSpace(Name))
                {
                    nameLow = Name.ToLower();
                }

                return nameLow;
            }
        }

        /// <summary>
        /// 全大写项目名称
        /// </summary>
        private string nameUp;
        /// <summary>
        /// 全大写项目名称
        /// </summary>
        public string NameUp
        {
            get
            {
                if (string.IsNullOrWhiteSpace(nameUp) && !string.IsNullOrWhiteSpace(Name))
                {
                    nameUp = Name.ToUpper();
                }

                return nameUp;
            }
        }

        /// <summary>
        /// 首字母小写的项目名称
        /// </summary>
        private string nameLowFirst;
        /// <summary>
        /// 首字母小写的项目名称
        /// </summary>
        public string NameLowFirst
        {
            get
            {
                if (string.IsNullOrWhiteSpace(nameLowFirst) && !string.IsNullOrWhiteSpace(Name))
                {
                    if (Name.Length > 1)
                    {
                        nameLowFirst = Name.Substring(0, 1).ToLower() + Name.Substring(1);
                    }
                    else
                    {
                        nameLowFirst = Name.ToLower();
                    }
                }

                return nameLowFirst;
            }
        }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// 项目命名空间
        /// </summary>
        public string Namespace
        {
            get;
            set;
        }

        /// <summary>
        /// 项目显示名称，可以使用中文等，用于项目的描述说明
        /// </summary>
        public string DisplayName
        {
            get;
            set;
        }

        /// <summary>
        /// 项目引用的根目录路径
        /// </summary>
        public string ReferenceRootFolder
        {
            get;
            set;
        }

        /// <summary>
        /// 所要生成的模板目录路径
        /// </summary>
        public string TemplatePath
        {
            get;
            set;
        }

        /// <summary>
        /// 生成后文件所要存放的目录路径
        /// </summary>
        public string GenTargetPath
        {
            get;
            set;
        }

        /// <summary>
        /// 是否在生成前删除生成目录中的所有文件，默认为True
        /// </summary>
        public bool IsClearTargetFolder
        {
            get;
            set;
        }

        /// <summary>
        /// 所选择的程序语言对应的转换器
        /// </summary>
        public ICodeInfoGetter CodeInfoGetter
        {
            get;
            set;
        }

        /// <summary>
        /// 所选择的数据库类型对应的转换器
        /// </summary>
        public IDBInfoGetter DBInfoGetter
        {
            get;
            set;
        }

        /// <summary>
        /// 用户选择的需要生成的表
        /// </summary>
        public IList<TableInfo> GenTableInfoList
        {
            get;
            private set;
        }

        /// <summary>
        /// 所有数据库表
        /// </summary>
        public IList<TableInfo> AllDBTableInfoList
        {
            get;
            set;
        }

        /// <summary>
        /// 最大生成数量
        /// </summary>
        public int MaxGenCount
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public IList<string> CurrGenFileList
        {
            get;
            private set;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 添加需要进行生成的表信息对象
        /// </summary>
        /// <param name="tableInfo">表信息对象</param>
        public void AddTableInfo(TableInfo tableInfo)
        {
            tableInfo.CurrProjectInfo = this;
            this.GenTableInfoList.Add(tableInfo);
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public ProjectInfo()
        {
            this.IsClearTargetFolder = false;
            this.GenTableInfoList = new List<TableInfo>();
            this.CurrGenFileList = new List<string>();
        }

        #endregion
    }
}
