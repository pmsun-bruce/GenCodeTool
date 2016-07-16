namespace NFramework.GenCodeTool.DBInfoGetter
{
    #region Reference

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;

    using NFramework.GenCodeTool.Entity;

    #endregion

    /// <summary>
    /// 数据库信息获取接口
    /// </summary>
    public interface IDBInfoGetter
    {
        #region Properties

        /// <summary>
        /// 获取器名称
        /// </summary>
        string GetterName { get; }

        #endregion

        #region Methods

        /// <summary>
        /// 往表中填充字段信息
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="tableInfo">需要填充的表信息对象</param>
        void FillColumnInfo(string connectionString, TableInfo tableInfo);
        /// <summary>
        /// 往表中填充字段信息
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="tableInfoList">需要填充的表信息对象集合</param>
        void FillColumnInfo(string connectionString, IList<TableInfo> tableInfoList);
        /// <summary>
        /// 获得指定表的表信息对象
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="tableName">数据库表名</param>
        /// <returns>返回表信息对象,如果没有该表，则返回null</returns>
        TableInfo GetTableInfo(string connectionString, string tableName);
        /// <summary>
        /// 获取指定数据库中所有表的信息对象集合
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns>返回表信息对象集合</returns>
        IList<TableInfo> GetTableInfoList(string connectionString);
        /// <summary>
        /// 将数据库类型转为Dal/Dao中所要用到的类型，如：DbType，SqlDbType，OleDbType等中对应的类型
        /// </summary>
        /// <param name="sqlType">数据库中的数据类型</param>
        /// <param name="precision">有效位数</param>
        /// <param name="scale">有效小数位数</param>
        /// <returns>返回类型的字符串</returns>
        string ToDalType(string sqlType, int precision, int scale);
        /// <summary>
        /// 将数据库类型转为对应的通用的DbType
        /// </summary>
        /// <param name="sqlType">数据库中的数据类型</param>
        /// <param name="precision">有效位数</param>
        /// <param name="scale">有效小数位数</param>
        /// <returns>返回对应的DbType的枚举值</returns>
        DbType ToDbType(string sqlType, int precision, int scale);

        #endregion
    }
}
