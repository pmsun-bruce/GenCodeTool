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
        /// 全小写表名称
        /// </summary>
        private string nameLow;
        /// <summary>
        /// 全小写表名称
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
        /// 全大写表名称
        /// </summary>
        private string nameUp;
        /// <summary>
        /// 全大写表名称
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
        /// 首字母小写的表名称
        /// </summary>
        private string nameLowFirst;
        /// <summary>
        /// 首字母小写的表名称
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
