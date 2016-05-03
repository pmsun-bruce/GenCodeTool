namespace NFramework.GenCodeTool.Entity
{
    #region Reference

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;

    #endregion

    /// <summary>
    /// 字段信息对象
    /// </summary>
    public class ColumnInfo
    {
        #region Fields & Properties

        /// <summary>
        /// 字段名称，数据库中真实的字段名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 全小写字段名称
        /// </summary>
        private string nameLow;
        /// <summary>
        /// 全小写字段名称
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
        /// 全大写字段名称
        /// </summary>
        private string nameUp;
        /// <summary>
        /// 全大写字段名称
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
        /// 首字母小写的字段名称
        /// </summary>
        private string nameLowFirst;
        /// <summary>
        /// 首字母小写的字段名称
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
        /// 字段备注，用于描述字段，可以用于界面的字段名称显示
        /// </summary>
        public string Comment
        {
            get;
            set;
        }

        /// <summary>
        /// 当前所在表的信息对象
        /// </summary>
        public TableInfo CurrTable
        {
            get;
            set;
        }

        /// <summary>
        /// 字段ID，即字段的排序号
        /// </summary>
        public int ColId
        {
            get;
            set;
        }

        /// <summary>
        /// 是否为主键字段
        /// </summary>
        public bool IsPK
        {
            get;
            set;
        }
        
        /// <summary>
        /// 是否为外键字段
        /// </summary>
        public bool IsFK
        {
            get;
            set;
        }

        /// <summary>
        /// 数据库中的外键名称，如：FK_Table1_Table2
        /// </summary>
        public string FKName
        {
            get;
            set;
        }

        /// <summary>
        /// 所对应的主表名称
        /// </summary>
        public string FKTableName
        {
            get;
            set;
        }

        /// <summary>
        /// 所对应的主表中的主键字段名称
        /// </summary>
        public string FKColumnName
        {
            get;
            set;
        }

        /// <summary>
        /// 外键字段信息对象
        /// </summary>
        public ColumnInfo FKColumn
        {
            get;
            set;
        }

        /// <summary>
        /// 是否为自增长
        /// </summary>
        public bool IsAutoNum
        {
            get;
            set;
        }

        /// <summary>
        /// 字段对应的DbType
        /// </summary>
        public DbType DbType
        {
            get;
            set;
        }

        /// <summary>
        /// 字段对应的数据库类型字符串
        /// </summary>
        public string SqlType
        {
            get;
            set;
        }

        /// <summary>
        /// 字段对应的Dal中所使用的类型字符串
        /// </summary>
        public string DalType
        {
            get;
            set;
        }

        /// <summary>
        /// 所选语言对应的数据类型，如选的是C#，则为C#对应类型
        /// </summary>
        public string CodeType
        {
            get;
            set;
        }

        /// <summary>
        /// 字段的最大长度，如果为0，则为无长度限制
        /// </summary>
        public int MaxLength
        {
            get;
            set;
        }

        /// <summary>
        /// 字段有效位数，字段为数字类型时有用
        /// </summary>
        public int Precision
        {
            get;
            set;
        }

        /// <summary>
        /// 有效小数位，字段为数字类型时有用
        /// </summary>
        public int Scale
        {
            get;
            set;
        }

        /// <summary>
        /// 字段的默认值
        /// </summary>
        public string DefaultValue
        {
            get;
            set;
        }

        /// <summary>
        /// 字段是否可为null
        /// </summary>
        public bool IsNullable
        {
            get;
            set;
        }

        /// <summary>
        /// 字段是否有唯一性约束
        /// </summary>
        public bool IsUnique
        {
            get;
            set;
        }

        /// <summary>
        /// 字段是否为字符串类型
        /// </summary>
        private bool isString;
        /// <summary>
        /// 字段是否为字符串类型
        /// </summary>
        public bool IsString
        {
            get
            {
                isString = false;

                if (this.DbType == DbType.AnsiString
                    || this.DbType == DbType.AnsiStringFixedLength
                    || this.DbType == DbType.String
                    || this.DbType == DbType.StringFixedLength)
                {
                    isString = true;
                }

                return isString;
            }
        }

        /// <summary>
        /// 是否为数字类型
        /// </summary>
        private bool isNumber;
        /// <summary>
        /// 是否为数字类型
        /// </summary>
        public bool IsNumber
        {
            get
            {
                isNumber = false;

                if (this.DbType == DbType.Currency
                    || this.DbType == DbType.Decimal
                    || this.DbType == DbType.Single
                    || this.DbType == DbType.Double
                    || this.DbType == DbType.VarNumeric)
                {
                    isNumber = true;
                }

                return isNumber;
            }
        }

        /// <summary>
        /// 是否为整形
        /// </summary>
        private bool isInteger;
        /// <summary>
        /// 是否为整形
        /// </summary>
        public bool IsInteger
        {
            get
            {
                isInteger = false;

                if (this.DbType == DbType.Int16
                    || this.DbType == DbType.Int32
                    || this.DbType == DbType.Int64
                    || this.DbType == DbType.UInt16
                    || this.DbType == DbType.UInt32
                    || this.DbType == DbType.UInt64)
                {
                    isInteger = true;
                }

                return isInteger;
            }
        }

        /// <summary>
        /// 是否为日期时间类型
        /// </summary>
        private bool isDateTime;
        /// <summary>
        /// 是否为日期时间类型
        /// </summary>
        public bool IsDateTime
        {
            get
            {
                isDateTime = false;

                if (this.DbType == DbType.Date
                    || this.DbType == DbType.DateTime
                    || this.DbType == DbType.DateTime2
                    || this.DbType == DbType.DateTimeOffset)
                {
                    isDateTime = true;
                }

                return isDateTime;
            }
        }

        /// <summary>
        /// 是否可以判断最大值
        /// </summary>
        private bool isMax;
        /// <summary>
        /// 是否可以判断最大值
        /// </summary>
        public bool IsMax
        {
            get
            {
                isMax = false;

                if (this.DbType == DbType.Currency
                    || this.DbType == DbType.Decimal
                    || this.DbType == DbType.Single
                    || this.DbType == DbType.Double
                    || this.DbType == DbType.Int16
                    || this.DbType == DbType.Int32
                    || this.DbType == DbType.Int64
                    || this.DbType == DbType.UInt16
                    || this.DbType == DbType.UInt32
                    || this.DbType == DbType.UInt64
                    || this.DbType == DbType.VarNumeric)
                {
                    isMax = true;
                }

                return isMax;
            }
        }

        /// <summary>
        /// 是否可以判断最小值
        /// </summary>
        private bool isMin;
        /// <summary>
        /// 是否可以判断最小值
        /// </summary>
        public bool IsMin
        {
            get
            {
                isMin = false;

                if (this.DbType == DbType.Currency
                    || this.DbType == DbType.Decimal
                    || this.DbType == DbType.Single
                    || this.DbType == DbType.Double  
                    || this.DbType == DbType.Int16
                    || this.DbType == DbType.Int32
                    || this.DbType == DbType.Int64 
                    || this.DbType == DbType.UInt16 
                    || this.DbType == DbType.UInt32 
                    || this.DbType == DbType.UInt64
                    || this.DbType == DbType.VarNumeric
                    || this.DbType == DbType.Date
                    || this.DbType == DbType.DateTime
                    || this.DbType == DbType.DateTime2)
                {
                    isMin = true;
                }

                return isMin;
            }
        }

        /// <summary>
        /// 最大值
        /// </summary>
        private string maxValue;
        /// <summary>
        /// 最大值
        /// </summary>
        public string MaxValue
        {
            get
            {
                if (string.IsNullOrWhiteSpace(maxValue))
                {
                    switch (this.DbType)
                    {
                        case DbType.Int16:
                            maxValue = short.MaxValue.ToString();
                            break;
                        case DbType.Int32:
                            maxValue = int.MaxValue.ToString();
                            break;
                        case DbType.Int64:
                            maxValue = long.MaxValue.ToString();
                            break;
                        case DbType.Decimal:
                            maxValue = decimal.MaxValue.ToString();
                            break;
                        case DbType.Single:
                            maxValue = float.MaxValue.ToString();
                            break;
                        case DbType.Double:
                            maxValue = double.MaxValue.ToString();
                            break;
                        case DbType.DateTime:
                            maxValue = DateTime.MaxValue.ToString("yyyy-MM-dd HH:mm:ss");
                            break;
                        case DbType.Byte:
                            maxValue = byte.MaxValue.ToString();
                            break;
                        default:
                            maxValue = string.Empty;
                            break;
                    }
                }

                return maxValue;
            }
            set
            {
                maxValue = value;
            }
        }

        /// <summary>
        /// 最小值
        /// </summary>
        public string minValue;
        /// <summary>
        /// 最小值
        /// </summary>
        public string MinValue
        {
            get
            {
                if (string.IsNullOrWhiteSpace(minValue))
                {
                    switch (this.DbType)
                    {
                        case DbType.Int16:
                            maxValue = short.MinValue.ToString();
                            break;
                        case DbType.Int32:
                            maxValue = int.MinValue.ToString();
                            break;
                        case DbType.Int64:
                            maxValue = long.MinValue.ToString();
                            break;
                        case DbType.Decimal:
                            maxValue = decimal.MinValue.ToString();
                            break;
                        case DbType.Single:
                            maxValue = float.MinValue.ToString();
                            break;
                        case DbType.Double:
                            maxValue = double.MinValue.ToString();
                            break;
                        case DbType.DateTime:
                            maxValue = "1753-01-01";
                            break;
                        case DbType.Byte:
                            maxValue = byte.MinValue.ToString();
                            break;
                        default:
                            maxValue = string.Empty;
                            break;
                    }
                }

                return minValue;
            }
            set
            {
                minValue = value;
            }
        }

        /// <summary>
        /// 是否可以判断长度
        /// </summary>
        private bool isMaxLen;
        /// <summary>
        /// 是否可以判断长度
        /// </summary>
        public bool IsMaxLen
        {
            get
            {
                isMaxLen = false;

                if (this.IsString && this.MaxLength > 0)
                {
                    isMaxLen = true;
                }

                return isMaxLen;
            }
        }

        /// <summary>
        /// 是否在查询界面上作为查询结果的一列
        /// </summary>
        public bool IsGenSearchResult
        {
            get;
            set;
        }

        /// <summary>
        /// 是否在查询界面上作为查询条件
        /// </summary>
        public bool IsGenSearchCondition
        {
            get;
            set;
        }

        /// <summary>
        /// 是否在输入界面上生成
        /// </summary>
        public bool IsGenInput
        {
            get;
            set;
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public ColumnInfo()
        {
            this.IsAutoNum = false;
            this.IsFK = false;
            this.IsGenSearchResult = true;
            this.IsGenSearchCondition = true;
            this.IsGenInput = true;
            this.IsNullable = true;
            this.IsPK = false;
            this.IsUnique = false;
            this.DefaultValue = string.Empty;
            this.MaxLength = 0;
            this.Precision = 0;
            this.Scale = 0;
        }

        #endregion
    }
}
