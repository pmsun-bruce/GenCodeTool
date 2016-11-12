namespace NFramework.GenCodeTool.Entity
{
    #region Reference

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    #endregion

    /// <summary>
    /// 表信息对象
    /// </summary>
    public class TableInfo
    {
        #region Fields & Properties

        /// <summary>
        /// 表名称，实际数据库中表的名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 类名称，初始和表名相同
        /// </summary>
        public string className;

        /// <summary>
        /// 类名称，初始和表名相同
        /// </summary>
        public string ClassName
        {
            get
            {
                if(string.IsNullOrWhiteSpace(className))
                {
                    className = Name;
                }

                return className;
            }

            set
            {
                className = value;
            }
        }

        /// <summary>
        /// 全小写类名称
        /// </summary>
        private string classNameLow;
        /// <summary>
        /// 全小写类名称
        /// </summary>
        public string ClassNameLow
        {
            get
            {
                if (string.IsNullOrWhiteSpace(classNameLow) && !string.IsNullOrWhiteSpace(ClassName))
                {
                    classNameLow = ClassName.ToLower();
                }

                return classNameLow;
            }
        }

        /// <summary>
        /// 全大写表名称
        /// </summary>
        private string classNameUp;
        /// <summary>
        /// 全大写表名称
        /// </summary>
        public string ClassNameUp
        {
            get
            {
                if (string.IsNullOrWhiteSpace(classNameUp) && !string.IsNullOrWhiteSpace(ClassName))
                {
                    classNameUp = ClassName.ToUpper();
                }

                return classNameUp;
            }
        }

        /// <summary>
        /// 首字母小写的表名称
        /// </summary>
        private string classNameLowFirst;
        /// <summary>
        /// 首字母小写的表名称
        /// </summary>
        public string ClassNameLowFirst
        {
            get
            {
                if (string.IsNullOrWhiteSpace(classNameLowFirst) && !string.IsNullOrWhiteSpace(ClassName))
                {
                    if (ClassName.Length > 1)
                    {
                        classNameLowFirst = ClassName.Substring(0, 1).ToLower() + ClassName.Substring(1);
                    }
                    else
                    {
                        classNameLowFirst = ClassName.ToLower();
                    }
                }

                return classNameLowFirst;
            }
        }

        /// <summary>
        /// 当前表所处命名空间
        /// </summary>
        public string Namespace
        {
            get;
            set;
        }

        /// <summary>
        /// 表的备注，用于描述表用途，可以作为表界面显示名称
        /// </summary>
        public string Comment
        {
            get;
            set;
        }

        /// <summary>
        /// 当前表主键字段信息对象集合
        /// </summary>
        public IList<ColumnInfo> PKList
        {
            get;
            private set;
        }

        /// <summary>
        /// 当前表的外键字段信息对象集合
        /// </summary>
        public IList<ColumnInfo> FKList
        {
            get;
            private set;
        }

        /// <summary>
        /// 关联表字段信息对象集合，即其他表用了当前表主键作为外键的表中对应的字段信息对象
        /// </summary>
        public IList<ColumnInfo> RKList
        {
            get;
            private set;
        }

        /// <summary>
        /// 当前表的所有字段信息对象
        /// </summary>
        public IList<ColumnInfo> ColumnList
        {
            get;
            private set;
        }

        /// <summary>
        /// 判断当前表是否用于生成物理文件；如果为false，则不根据模板生成物理文件，但是可以用作关联生成
        /// </summary>
        public bool IsGen
        {
            get;
            set;
        }

        /// <summary>
        /// 判断是否生成界面
        /// </summary>
        public bool IsGenUI
        {
            get;
            set;
        }

        public int LoopIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 项目信息对象
        /// </summary>
        public ProjectInfo CurrProjectInfo
        {
            get;
            set;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 将字段信息对象添加到表信息对象中，并根据字段是否为主键或外键，分配到主键和外键集合中
        /// </summary>
        /// <param name="columnInfo">字段信息对象</param>
        public void AddColumn(ColumnInfo columnInfo)
        {
            columnInfo.CurrTable = this;
            ColumnList.Add(columnInfo);

            if (columnInfo.IsPK)
            {
                this.PKList.Add(columnInfo);
            }

            if (columnInfo.IsFK)
            {
                this.FKList.Add(columnInfo);
            }
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public TableInfo()
        {
            this.ColumnList = new List<ColumnInfo>();
            this.RKList = new List<ColumnInfo>();
            this.FKList = new List<ColumnInfo>();
            this.PKList = new List<ColumnInfo>();
            this.IsGen = false;
            this.IsGenUI = true;
        }

        #endregion
    }
}
