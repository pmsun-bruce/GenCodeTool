namespace NFramework.GenCodeTool.DBInfoGetter
{
    #region Reference

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    #endregion

    /// <summary>
    /// 数据库信息获取器工厂
    /// </summary>
    public class DBInfoGetterFactory
    {
        #region Fields & Properties

        /// <summary>
        /// 数据库信息获取器池，所有可使用的数据库信息获取器在这里注册
        /// </summary>
        public static IList<IDBInfoGetter> DBInfoGetterPool
        {
            get;
            private set;
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// 获取指定名称的数据库信息获取器
        /// </summary>
        /// <param name="getterName">数据库信息获取器的名称</param>
        /// <returns>返回数据库信息获取器，如果没有返回null</returns>
        public static IDBInfoGetter GetDBInfoGetter(string getterName)
        {
            IDBInfoGetter dbInfoGetter = DBInfoGetterPool.FirstOrDefault<IDBInfoGetter>(g => g.GetterName.Equals(getterName));
            return dbInfoGetter;
        }

        #endregion

        #region Static Constructors

        /// <summary>
        /// 静态构造函数，注册所有的数据库信息获取器
        /// </summary>
        static DBInfoGetterFactory()
        {
            DBInfoGetterPool = new List<IDBInfoGetter>();
            // MS SQL Server
            IDBInfoGetter mssqlDbInfoGetter = new MssqlDBInfoGetter();
            DBInfoGetterPool.Add(mssqlDbInfoGetter);
            // Oracle
            IDBInfoGetter oracleDbInfoGetter = new OracleDBInfoGetter();
            DBInfoGetterPool.Add(oracleDbInfoGetter);
            // Mysql
            //IDBInfoGetter mySqlDbInfoGetter = new MySqlDBInfoGetter();
            //DBInfoGetterPool.Add(mySqlDbInfoGetter);
        }

        #endregion
    }
}
