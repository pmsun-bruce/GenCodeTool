namespace NFramework.GenCodeTool.CodeInfoGetter
{
    #region Reference
    
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;

    #endregion

    /// <summary>
    /// 指定程序语言的信息获取接口
    /// </summary>
    public interface ICodeInfoGetter
    {
        /// <summary>
        /// 获取器名称
        /// </summary>
        string GetterName { get; }
        /// <summary>
        /// 根据程序中的数据类型，包装指定内容的数据类型转换字符串。
        /// </summary>
        /// <param name="content">需要包装的内容</param>
        /// <param name="codeType">程序数据类型</param>
        /// <returns>返回包装后的代码字符串</returns>
        string GetConvertString(string content, string codeType);
        /// <summary>
        /// 根据指定的DbType，包装指定内容的默认值字符串。
        /// </summary>
        /// <param name="defaultValue">需要包装的默认值内容</param>
        /// <param name="dbType">通用的DbType类型</param>
        /// <returns>返回包装后的代码字符串</returns>
        string GetDefaultValueString(string defaultValue, DbType dbType);
        /// <summary>
        /// 根据程序中的数据类型，包装指定内容的ToString代码字符串。
        /// </summary>
        /// <param name="content">需要包装的内容</param>
        /// <param name="codeType">程序数据类型</param>
        /// <returns>返回包装后的代码字符串</returns>
        string GetToString(string content, string codeType);
        /// <summary>
        /// 将指定DbType转为程序中的对应的数据类型
        /// </summary>
        /// <param name="dbType">通用的DbType类型</param>
        /// <returns>返回转换成的程序中的数据类型字符串</returns>
        string ToCodeType(DbType dbType);
    }
}
