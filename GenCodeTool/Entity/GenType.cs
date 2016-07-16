namespace NFramework.GenCodeTool.Entity
{
    #region Reference

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    #endregion

    /// <summary>
    /// 生成类型
    /// </summary>
    public enum GenType : int
    {
        /// <summary>
        /// 项目文件夹
        /// </summary>
        ProjectFolder = 1,
        /// <summary>
        /// 循环表，即该模板每张表都要生成一个文件夹
        /// </summary>
        TableLoopFolder = 2,
        /// <summary>
        /// 表文件夹
        /// </summary>
        TableFolder = 3,
        /// <summary>
        /// 项目文件
        /// </summary>
        ProjectFile = 4,
        /// <summary>
        /// 循环表，即该模板每张表都要生成一个文件
        /// </summary>
        TableLoopFile = 5,
        /// <summary>
        /// 表文件
        /// </summary>
        TableFile = 6
    }
}
