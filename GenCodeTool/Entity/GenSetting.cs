namespace NFramework.GenCodeTool.Entity
{
    #region Reference

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    #endregion

    /// <summary>
    /// 生成配置对象，用于加载和保存生成配置，可序列化
    /// </summary>
    [Serializable]
    public class GenSetting
    {
        /// <summary>
        /// 配置名称
        /// </summary>
        public string SettingName
        {
            get;
            set;
        }

        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        public string ConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public string DBType
        {
            get;
            set;
        }
        
        /// <summary>
        /// 程序类型
        /// </summary>
        public string CodeType
        {
            get;
            set;
        }

        /// <summary>
        /// 命名空间
        /// </summary>
        public string Namespace
        {
            get;
            set;
        }

        /// <summary>
        /// 其他库引用路径
        /// </summary>
        public string ReferenceRootFolder
        {
            get;
            set;
        }

        /// <summary>
        /// 生成模板文件夹路径
        /// </summary>
        public string TemplatePath
        {
            get;
            set;
        }

        /// <summary>
        /// 目标文件夹路径
        /// </summary>
        public string GenTargetPath
        {
            get;
            set;
        }

        /// <summary>
        /// 是否生成前清空目标文件夹
        /// </summary>
        public bool IsClearTargetFolder
        {
            get;
            set;
        }
    }
}
