namespace NFramework.GenCodeTool.DBInfoGetter
{
    #region Reference

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;

    using NFramework.GenCodeTool.Entity;
    using NFramework.GenCodeTool.Resources.Lan;
    using NFramework.ExceptionTool;
    using MySql.Data.MySqlClient;

    #endregion

    /// <summary>
    /// Mysql SERVER对应的数据库信息获取器
    /// </summary>
    public class MysqlDBInfoGetter : IDBInfoGetter
    {
        #region Fields & Properties

        /// <summary>
        /// 获取器名称：Mysql Use DbType
        /// </summary>
        public string GetterName
        {
            get { return "Mysql Use DbType"; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 往表中填充字段信息
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="tableInfo">需要填充的表信息对象</param>
        public void FillColumnInfo(string connectionString, TableInfo tableInfo)
        {
            MySqlConnection dbConn = new MySqlConnection(connectionString);
            dbConn.Open();
            string currentSchema = dbConn.Database;
            dbConn.Close();
            dbConn.Dispose();
            dbConn = null;

            StringBuilder colQuery = new StringBuilder();
            colQuery.AppendLine(@"SELECT ");
            colQuery.AppendLine(@"    C.*, ");
            colQuery.AppendLine(@"    PKTBL.PK, ");
            colQuery.AppendLine(@"    UKTBL.UK, ");
            colQuery.AppendLine(@"    FKTBL.FK, ");
            colQuery.AppendLine(@"    FKTBL.FK_NAME, ");
            colQuery.AppendLine(@"    FKTBL.REF_SCHEMA, ");
            colQuery.AppendLine(@"    FKTBL.REF_TABLE_NAME, ");
            colQuery.AppendLine(@"    FKTBL.REF_COLUMN_NAME ");
            colQuery.AppendLine(@"FROM ");
            colQuery.AppendLine(@"    `information_schema`.`columns` AS C ");
            colQuery.AppendLine(@"LEFT JOIN ");
            colQuery.AppendLine(@"    (SELECT ");
            colQuery.AppendLine(@"        k.`table_schema`, ");
            colQuery.AppendLine(@"        k.`table_name`, ");
            colQuery.AppendLine(@"        k.`column_name`, ");
            colQuery.AppendLine(@"        tc.constraint_name, ");
            colQuery.AppendLine(@"        tc.constraint_type AS PK ");
            colQuery.AppendLine(@"    FROM ");
            colQuery.AppendLine(@"        `information_schema`.KEY_COLUMN_USAGE AS k ");
            colQuery.AppendLine(@"    INNER JOIN `information_schema`.TABLE_CONSTRAINTS AS tc ON k.CONSTRAINT_NAME = tc.CONSTRAINT_NAME ");
            colQuery.AppendLine(@"                                                           AND k.`table_name` = tc.`table_name` ");
            colQuery.AppendLine(@"                                                           AND k.table_schema = tc.table_schema ");
            colQuery.AppendLine(@"                                                           AND tc.CONSTRAINT_TYPE = 'PRIMARY KEY' ");
            colQuery.AppendLine(@"    ) PKTBL ON PKTBL.`table_schema` = C.`table_schema` ");
            colQuery.AppendLine(@"            AND PKTBL.`table_name` = C.`table_name` ");
            colQuery.AppendLine(@"            AND PKTBL.`column_name` = C.`column_name` ");
            colQuery.AppendLine(@"LEFT JOIN ");
            colQuery.AppendLine(@"    (SELECT DISTINCT ");
            colQuery.AppendLine(@"        k.`table_schema`, ");
            colQuery.AppendLine(@"        k.`table_name`, ");
            colQuery.AppendLine(@"        k.`column_name`, ");
            colQuery.AppendLine(@"        tc.constraint_type AS UK ");
            colQuery.AppendLine(@"    FROM");
            colQuery.AppendLine(@"        `information_schema`.KEY_COLUMN_USAGE AS k ");
            colQuery.AppendLine(@"    INNER JOIN `information_schema`.TABLE_CONSTRAINTS AS tc ON k.CONSTRAINT_NAME = tc.CONSTRAINT_NAME ");
            colQuery.AppendLine(@"                                                           AND k.`table_name` = tc.`table_name` ");
            colQuery.AppendLine(@"                                                           AND k.table_schema = tc.table_schema ");
            colQuery.AppendLine(@"                                                           AND tc.CONSTRAINT_TYPE = 'UNIQUE' ");
            colQuery.AppendLine(@"    ) UKTBL ON UKTBL.`table_schema` = C.`table_schema` ");
            colQuery.AppendLine(@"            AND UKTBL.`table_name` = C.`table_name` ");
            colQuery.AppendLine(@"            AND UKTBL.`column_name` = C.`column_name` ");
            colQuery.AppendLine(@"LEFT JOIN ");
            colQuery.AppendLine(@"    (SELECT ");
            colQuery.AppendLine(@"        k.`table_schema`, ");
            colQuery.AppendLine(@"        k.`table_name`,");
            colQuery.AppendLine(@"        k.`column_name`, ");
            colQuery.AppendLine(@"        tc.constraint_type AS FK, ");
            colQuery.AppendLine(@"        tc.constraint_name AS FK_NAME, ");
            colQuery.AppendLine(@"        k.referenced_table_schema AS REF_SCHEMA, ");
            colQuery.AppendLine(@"        k.referenced_table_name AS REF_TABLE_NAME, ");
            colQuery.AppendLine(@"        k.referenced_column_name AS REF_COLUMN_NAME ");
            colQuery.AppendLine(@"    FROM");
            colQuery.AppendLine(@"        `information_schema`.KEY_COLUMN_USAGE AS k ");
            colQuery.AppendLine(@"    INNER JOIN `information_schema`.TABLE_CONSTRAINTS AS tc ON k.CONSTRAINT_NAME = tc.CONSTRAINT_NAME ");
            colQuery.AppendLine(@"                                                           AND k.`table_name` = tc.`table_name` ");
            colQuery.AppendLine(@"                                                           AND k.table_schema = tc.table_schema ");
            colQuery.AppendLine(@"                                                           AND tc.CONSTRAINT_TYPE = 'FOREIGN KEY' ");
            colQuery.AppendLine(@"    ) FKTBL ON FKTBL.`table_schema` = C.`table_schema` ");
            colQuery.AppendLine(@"            AND FKTBL.`table_name` = C.`table_name` ");
            colQuery.AppendLine(@"            AND FKTBL.`column_name` = C.`column_name` ");
            colQuery.AppendLine(@"WHERE");
            colQuery.AppendLine(@"    C.TABLE_SCHEMA = @TableSchema ");
            colQuery.AppendLine(@"AND ");
            colQuery.AppendLine(@"    C.TABLE_NAME = @TableName ");
            colQuery.AppendLine(@"ORDER BY ");
            colQuery.AppendLine(@"    C.ORDINAL_POSITION ");

            MySqlParameter[] paramCollection = new MySqlParameter[2];
            paramCollection[0] = new MySqlParameter("TableSchema", MySqlDbType.String);
            paramCollection[1] = new MySqlParameter("TableName", MySqlDbType.String);

            paramCollection[0].Value = currentSchema;
            paramCollection[1].Value = tableInfo.Name;
            
            ColumnInfo colInfo = null;
            int tmpInt = 0;

            try
            {
                DataSet colDs = MySqlHelper.ExecuteDataset(connectionString, colQuery.ToString(), paramCollection);

                if (colDs != null && colDs.Tables.Count > 0 && colDs.Tables[0].Rows.Count > 0)
                {
                    DataTable colTbl = colDs.Tables[0];

                    foreach(DataRow dRow in colTbl.Rows)
                    {
                        colInfo = new ColumnInfo();
                        colInfo.Name = dRow["COLUMN_NAME"].ToString();
                        colInfo.SqlType = dRow["DATA_TYPE"].ToString().ToLower();
                        colInfo.MaxLength = !int.TryParse(dRow["CHARACTER_MAXIMUM_LENGTH"].ToString(), out tmpInt) ? 0 : tmpInt;
                        colInfo.IsNullable = "YES".Equals(dRow["IS_NULLABLE"].ToString()) ? true : false;
                        colInfo.ColId = Convert.ToInt32(dRow["ORDINAL_POSITION"]);
                        colInfo.Comment = dRow["COLUMN_COMMENT"].ToString();
                        colInfo.Precision = !int.TryParse(dRow["NUMERIC_PRECISION"].ToString(), out tmpInt) ? 0 : tmpInt;
                        colInfo.Scale = !int.TryParse(dRow["NUMERIC_SCALE"].ToString(), out tmpInt) ? 0 : tmpInt;
                        colInfo.DefaultValue = dRow["COLUMN_DEFAULT"].ToString();
                        colInfo.IsUnique = dRow["UK"] is DBNull ? false : true;
                        colInfo.IsPK = dRow["PK"] is DBNull ? false : true;
                        colInfo.IsFK = dRow["FK"] is DBNull ? false : true;

                        if (colInfo.IsFK)
                        {
                            colInfo.FKName = dRow["FK_NAME"].ToString();
                            colInfo.FKColumnName = dRow["REF_COLUMN_NAME"].ToString();
                            colInfo.FKTableName = dRow["REF_TABLE_NAME"].ToString();
                        }

                        colInfo.DbType = ToDbType(colInfo.SqlType, colInfo.Precision, colInfo.Scale);
                        colInfo.DalType = ToDalType(colInfo.SqlType, colInfo.Precision, colInfo.Scale);
                        tableInfo.AddColumn(colInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ResponseException(GenCodeToolResource.Error_GetColumn, ex);
            }
        }

        /// <summary>
        /// 往表中填充字段信息
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="tableInfoList">需要填充的表信息对象集合</param>
        public void FillColumnInfo(string connectionString, IList<TableInfo> tableInfoList)
        {
            if (tableInfoList == null || tableInfoList.Count == 0)
            {
                return;
            }

            foreach (TableInfo tableInfo in tableInfoList)
            {
                FillColumnInfo(connectionString, tableInfo);
            }
            
        }

        /// <summary>
        /// 获得指定表的表信息对象
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="tableName">数据库表名</param>
        /// <returns>返回表信息对象,如果没有该表，则返回null</returns>
        public TableInfo GetTableInfo(string connectionString, string tableName)
        {
            TableInfo result = new TableInfo();
            MySqlConnection dbConn = new MySqlConnection(connectionString);
            dbConn.Open();
            string currentSchema = dbConn.Database;
            dbConn.Close();
            dbConn.Dispose();
            dbConn = null;

            StringBuilder tblQuery = new StringBuilder();
            tblQuery.AppendLine(@"SELECT ");
            tblQuery.AppendLine(@"  * ");
            tblQuery.AppendLine(@"FROM ");
            tblQuery.AppendLine(@"  `information_schema`.`tables` ");
            tblQuery.AppendLine(@"WHERE ");
            tblQuery.AppendLine(@"  table_schema=@TableSchema ");
            tblQuery.AppendLine(@"AND ");
            tblQuery.AppendLine(@"  table_name=@TableName ");

            MySqlParameter[] paramCollection = new MySqlParameter[2];
            paramCollection[0] = new MySqlParameter("TableSchema", MySqlDbType.String, 100);
            paramCollection[1] = new MySqlParameter("TableName", MySqlDbType.String, 100);
            
            paramCollection[0].Value = currentSchema;
            paramCollection[1].Value = tableName;
            
            try
            {
                DataSet tblDs = MySqlHelper.ExecuteDataset(connectionString, tblQuery.ToString(), paramCollection);

                if (tblDs != null && tblDs.Tables.Count > 0 && tblDs.Tables[0].Rows.Count > 0)
                {
                    DataTable tblInfoTbl = tblDs.Tables[0];
                    DataRow dRow = tblInfoTbl.Rows[0];
                    result = new TableInfo();
                    result.Name = dRow["TABLE_NAME"].ToString();
                    result.Comment = dRow["TABLE_COMMENT"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new ResponseException(GenCodeToolResource.Error_GetTable, ex);
            }

            return result;
        }

        /// <summary>
        /// 获取指定数据库中所有表的信息对象集合
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns>返回表信息对象集合</returns>
        public IList<TableInfo> GetTableInfoList(string connectionString)
        {
            IList<TableInfo> resultList = new List<TableInfo>();
            MySqlConnection dbConn = new MySqlConnection(connectionString);
            dbConn.Open();
            string currentSchema = dbConn.Database;
            dbConn.Close();
            dbConn.Dispose();
            dbConn = null;

            StringBuilder tblQuery = new StringBuilder();
            tblQuery.AppendLine(@"SELECT ");
            tblQuery.AppendLine(@"  * ");
            tblQuery.AppendLine(@"FROM ");
            tblQuery.AppendLine(@"  `information_schema`.`tables` ");
            tblQuery.AppendLine(@"WHERE ");
            tblQuery.AppendLine(@"  table_schema=@TableSchema ");
            tblQuery.AppendLine(@"ORDER BY ");
            tblQuery.AppendLine(@"  table_name ASC");

            MySqlParameter[] paramCollection = new MySqlParameter[1];
            paramCollection[0] = new MySqlParameter("TableSchema", MySqlDbType.String, 100);

            paramCollection[0].Value = currentSchema;

            TableInfo tableInfo = null;

            try
            {
                DataSet tblDs = MySqlHelper.ExecuteDataset(connectionString, tblQuery.ToString(), paramCollection);

                if (tblDs != null && tblDs.Tables.Count > 0 && tblDs.Tables[0].Rows.Count > 0)
                {
                    DataTable tblInfoTbl = tblDs.Tables[0];

                    foreach (DataRow aRow in tblInfoTbl.Rows)
                    {
                        tableInfo = new TableInfo();
                        tableInfo.Name = aRow["TABLE_NAME"].ToString();
                        tableInfo.Comment = aRow["TABLE_COMMENT"].ToString();
                        resultList.Add(tableInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ResponseException(GenCodeToolResource.Error_GetTable, ex);
            }

            return resultList;
        }

        /// <summary>
        /// 将数据库类型转为Dal/Dao中所要用到的类型，如：DbType，SqlDbType，OleDbType等中对应的类型
        /// </summary>
        /// <param name="sqlType">数据库中的数据类型</param>
        /// <param name="precision">有效位数</param>
        /// <param name="scale">有效小数位数</param>
        /// <returns>返回类型的字符串</returns>
        public string ToDalType(string sqlType, int precision, int scale)
        {
            string typePre = "MySqlDbType.";
            
            switch (sqlType)
            {
                case MysqlSqlType.Char:
                    return typePre + "String";
                case MysqlSqlType.VarChar:
                    return typePre + "VarChar";
                case MysqlSqlType.NVarChar:
                    return typePre + "VarString";
                case MysqlSqlType.Text:
                    return typePre + "Text";
                case MysqlSqlType.LongText:
                    return typePre + "VarChar";
                case MysqlSqlType.Int:
                    return typePre + "Int32";
                case MysqlSqlType.BigInt:
                    return typePre + "Int64";
                case MysqlSqlType.Decimal:
                    return typePre + "Decimal";
                case MysqlSqlType.Float:
                    return typePre + "Float";
                case MysqlSqlType.Double:
                    return typePre + "Double";
                case MysqlSqlType.Date:
                    return typePre + "Date";
                case MysqlSqlType.DateTime:
                    return typePre + "DateTime";
                default:
                    return typePre + "VarString";
            }
        }

        /// <summary>
        /// 将数据库类型转为对应的通用的DbType
        /// </summary>
        /// <param name="sqlType">数据库中的数据类型</param>
        /// <param name="precision">有效位数</param>
        /// <param name="scale">有效小数位数</param>
        /// <returns>返回对应的DbType的枚举值</returns>
        public DbType ToDbType(string sqlType, int precision, int scale)
        {
            switch (sqlType)
            {
                case MysqlSqlType.Char:
                    return DbType.StringFixedLength;
                case MysqlSqlType.VarChar:
                case MysqlSqlType.NVarChar:
                case MysqlSqlType.Text:
                case MysqlSqlType.LongText:
                    return DbType.String;
                case MysqlSqlType.Int:
                    return DbType.Int32;
                case MysqlSqlType.BigInt:
                    return DbType.Int64;
                case MysqlSqlType.Decimal:
                case MysqlSqlType.Float:
                case MysqlSqlType.Double:
                    return DbType.Decimal;
                case MysqlSqlType.Date:
                case MysqlSqlType.DateTime:
                    return DbType.DateTime;
                default:
                    return DbType.String;
            }
        }

        #endregion
    }
}
