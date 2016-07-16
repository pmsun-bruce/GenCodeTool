namespace NFramework.GenCodeTool.CodeInfoGetter
{
    #region Reference

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data;

    #endregion

    /// <summary>
    /// C# 代码相关信息
    /// </summary>
    public class CSCodeInfoGetter : ICodeInfoGetter
    {
        #region Fields & Properties

        /// <summary>
        /// 获取器名称：CS Getter
        /// </summary>
        public string GetterName
        {
            get { return "CS Getter"; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 根据C#程序中的数据类型，包装指定内容的数据类型转换字符串。
        /// </summary>
        /// <param name="content">需要包装的内容</param>
        /// <param name="codeType">程序数据类型</param>
        /// <returns>返回包装后的代码字符串</returns>
        public string GetConvertString(string content, string codeType)
        {
            string convertString = "{0}";
            
            switch (codeType)
            {
                case CSCodeType.String:
                    convertString = "{0}.ToString()";
                    break;
                case CSCodeType.Time:
                case CSCodeType.DateTime:
                    convertString = "Convert.ToDateTime({0})";
                    break;
                case CSCodeType.Short:
                    convertString = "Convert.ToInt16({0})";
                    break;
                case CSCodeType.Int:
                    convertString = "Convert.ToInt32({0})";
                    break;
                case CSCodeType.Long:
                    convertString = "Convert.ToInt64({0})";
                    break;
                case CSCodeType.Decimal:
                    convertString = "Convert.ToDecimal({0})";
                    break;
                case CSCodeType.Float:
                    convertString = "Convert.ToSingle({0})";
                    break;
                case CSCodeType.Double:
                    convertString = "Convert.ToDouble({0})";
                    break;
                case CSCodeType.Boolean:
                    convertString = "Convert.ToBoolean({0})";
                    break;
                case CSCodeType.Byte:
                    convertString = "Convert.ToByte({0})";
                    break;
                default:
                    convertString = "{0}.ToString()";
                    break;
                
            }

            return string.Format(convertString, content);
        }

        /// <summary>
        /// 根据指定的DbType，包装指定内容的默认值为C#代码中的字符串。
        /// </summary>
        /// <param name="defaultValue">需要包装的默认值内容</param>
        /// <param name="dbType">通用的DbType类型</param>
        /// <returns>返回包装后的代码字符串</returns>
        public string GetDefaultValueString(string defaultValue, DbType dbType)
        {
            string defaultString = (defaultValue == null ? string.Empty : defaultValue.Replace("(", "").Replace(")", ""));

            switch (dbType)
            {
                case DbType.String:
                    defaultString = "\"" + defaultString + "\"";
                    break;
                case DbType.Int16:
                case DbType.Int32:
                case DbType.Int64:
                case DbType.Currency:
                case DbType.Decimal:
                case DbType.Double:
                case DbType.Single:
                case DbType.UInt16:
                case DbType.UInt32:
                case DbType.UInt64:
                    if (string.IsNullOrEmpty(defaultString))
                    {
                        defaultString = "0";
                    }
                    break;
                case DbType.DateTime:
                    if (string.IsNullOrEmpty(defaultString))
                    {
                        defaultString = "Convert.ToDateTime(\"1753-01-01\")";
                    }
                    else
                    {
                        defaultString = "Convert.ToDateTime(\"" + defaultValue + "\")";
                    }

                    break;
            }

            return defaultString;
        }

        /// <summary>
        /// 根据C#程序中的数据类型，包装指定内容的ToString代码字符串。
        /// </summary>
        /// <param name="content">需要包装的内容</param>
        /// <param name="codeType">程序数据类型</param>
        /// <returns>返回包装后的代码字符串</returns>
        public string GetToString(string content, string codeType)
        {
            string toString = string.Empty;

            switch (codeType)
            {
                case CSCodeType.DateTime:
                    toString = "{0}.ToString(\"yyyy-MM-dd HH:mm:ss\")";
                    break;
                default:
                    toString = "{0}.ToString()";
                    break;
            }

            return string.Format(toString, content);
        }

        /// <summary>
        /// 将指定DbType转为C#程序中的对应的数据类型
        /// </summary>
        /// <param name="dbType">通用的DbType类型</param>
        /// <returns>返回转换成的程序中的数据类型字符串</returns>
        public string ToCodeType(DbType dbType)
        {
            switch (dbType)
            {
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                case DbType.String:
                case DbType.StringFixedLength:
                    return CSCodeType.String;
                case DbType.Boolean:
                    return CSCodeType.Boolean;
                case DbType.Byte:
                    return CSCodeType.Byte;
                case DbType.Currency:
                case DbType.Decimal:
                    return CSCodeType.Decimal;
                case DbType.Double:
                    return CSCodeType.Double;
                case DbType.Int16:
                case DbType.UInt16:
                    return CSCodeType.Short;
                case DbType.Int32:
                case DbType.UInt32:
                    return CSCodeType.Int;
                case DbType.Int64:
                case DbType.UInt64:
                    return CSCodeType.Long;
                case DbType.Single:
                    return CSCodeType.Float;
                case DbType.Time:
                case DbType.DateTime:
                    return CSCodeType.DateTime;
                default:
                    return CSCodeType.Object;
            }
        }

        #endregion
    }
}
