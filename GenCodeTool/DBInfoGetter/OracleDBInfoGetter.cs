namespace NFramework.GenCodeTool.DBInfoGetter
{
    #region Reference

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.Linq;
    using System.Text;

    using NFramework.ExceptionTool;

    using NFramework.GenCodeTool.Entity;
    using NFramework.GenCodeTool.Resources.Lan;

    #endregion

    /// <summary>
    /// Oracle对应的数据库信息获取器
    /// </summary>
    public class OracleDBInfoGetter : IDBInfoGetter
    {
        #region Fields & Properties

        /// <summary>
        /// 获取器名称：Oracle Use DbType
        /// </summary>
        public string GetterName
        {
            get { return "Oracle Use DbType"; }
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
            OleDbConnection oleDbConn = new OleDbConnection(connectionString);

            try
            {
                oleDbConn.Open();
            }
            catch (Exception ex)
            {
                throw new ResponseException(GenCodeToolResource.Error_OpenDB, ex);
            }

            StringBuilder colQuery = new StringBuilder();
            colQuery.AppendLine(@"SELECT ");
            colQuery.AppendLine(@"  t.table_name, ");
            colQuery.AppendLine(@"  t.column_name, ");
            colQuery.AppendLine(@"  t.data_type, ");
            colQuery.AppendLine(@"  t.data_length, ");
            colQuery.AppendLine(@"  t.nullable, ");
            colQuery.AppendLine(@"  t.column_id, ");
            colQuery.AppendLine(@"  c.comments, ");
            colQuery.AppendLine(@"  t.DATA_PRECISION, ");
            colQuery.AppendLine(@"  t.DATA_SCALE, ");
            colQuery.AppendLine(@"  (SELECT CASE WHEN n.column_name is not null AND n.constraint_type = 'P' THEN 'true' ELSE 'false' END FROM DUAL) ispk, ");
            colQuery.AppendLine(@"  (SELECT CASE WHEN n.column_name is not null AND n.constraint_type = 'R' THEN 'true' ELSE 'false' END FROM DUAL) isfk, ");
            colQuery.AppendLine(@"  n.fk_name, ");
            colQuery.AppendLine(@"  n.fk_col_name, ");
            colQuery.AppendLine(@"  n.fk_table_name ");
            colQuery.AppendLine(@"FROM ");
            colQuery.AppendLine(@"  user_tab_cols t ");
            colQuery.AppendLine(@"LEFT JOIN  ");
            colQuery.AppendLine(@"  user_col_comments c ON (c.table_name=t.table_name AND c.column_name=t.column_name) ");
            colQuery.AppendLine(@"LEFT JOIN ");
            colQuery.AppendLine(@"  (SELECT ");
            colQuery.AppendLine(@"     col1.column_name, ");
            colQuery.AppendLine(@"     col1.table_name, ");
            colQuery.AppendLine(@"     con1.constraint_type, ");
            colQuery.AppendLine(@"     con1.constraint_name, ");
            colQuery.AppendLine(@"     con1.r_constraint_name AS fk_name, ");
            colQuery.AppendLine(@"     col2.column_name AS fk_col_name, ");
            colQuery.AppendLine(@"     col2.table_name AS fk_table_name ");
            colQuery.AppendLine(@"   FROM ");
            colQuery.AppendLine(@"     user_cons_columns col1 ");
            colQuery.AppendLine(@"   LEFT JOIN ");
            colQuery.AppendLine(@"     user_constraints con1 ON(col1.table_name = con1.table_name AND col1.constraint_name = con1.constraint_name) ");
            colQuery.AppendLine(@"   LEFT JOIN ");
            colQuery.AppendLine(@"     user_cons_columns col2 ON(col2.constraint_name = con1.r_constraint_name) ");
            colQuery.AppendLine(@"   LEFT JOIN ");
            colQuery.AppendLine(@"     user_constraints con2 ON(col2.table_name = con2.table_name AND con2.constraint_name = con1.r_constraint_name) ");
            colQuery.AppendLine(@"   WHERE ");
            colQuery.AppendLine(@"       LOWER(col1.table_name)='m_cust_edit_detail' ");
            colQuery.AppendLine(@"     AND ");
            colQuery.AppendLine(@"       con1.constraint_type != 'C') n ON (n.column_name = t.column_name) ");
            colQuery.AppendLine(@"WHERE ");
            colQuery.AppendLine(@"    LOWER(t.table_name)='" + tableInfo.Name + @"' ");
            colQuery.AppendLine(@"  AND ");
            colQuery.AppendLine(@"    t.hidden_column='NO' ");
            colQuery.AppendLine(@"ORDER BY ");
            colQuery.AppendLine(@"  t.column_id ");

            OleDbCommand command = new OleDbCommand(colQuery.ToString(), oleDbConn);
            OleDbDataReader dataReader = null;
            ColumnInfo colInfo = null;
            int tmpInt = 0;

            try
            {
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    colInfo = new ColumnInfo();
                    colInfo.Name = dataReader[1].ToString();
                    colInfo.SqlType = dataReader[2].ToString().ToLower();
                    colInfo.MaxLength = !int.TryParse(dataReader[3].ToString(), out tmpInt) ? 0 : tmpInt;
                    colInfo.IsNullable = "Y".Equals(dataReader[4].ToString()) ? true : false;
                    colInfo.ColId = int.Parse(dataReader[5].ToString()); 
                    colInfo.Comment = dataReader[6].ToString();
                    colInfo.Precision = !int.TryParse(dataReader[7].ToString(), out tmpInt) ? 0 : tmpInt;
                    colInfo.Scale = !int.TryParse(dataReader[8].ToString(), out tmpInt) ? 0 : tmpInt;
                    colInfo.IsPK = bool.Parse(dataReader[9].ToString());
                    colInfo.IsFK = bool.Parse(dataReader[10].ToString());

                    if (colInfo.IsFK)
                    {
                        colInfo.FKName = dataReader[11].ToString();
                        colInfo.FKColumnName = dataReader[12].ToString();
                        colInfo.FKTableName = dataReader[13].ToString();
                    }

                    colInfo.DbType = ToDbType(colInfo.SqlType, colInfo.Precision, colInfo.Scale);
                    colInfo.DalType = ToDalType(colInfo.SqlType, colInfo.Precision, colInfo.Scale);
                    tableInfo.AddColumn(colInfo);
                }
            }
            catch (Exception ex)
            {
                throw new ResponseException(GenCodeToolResource.Error_GetColumn, ex);
            }
            finally
            {
                dataReader.Close();
                oleDbConn.Close();
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
            OleDbConnection oleDbConn = new OleDbConnection(connectionString);

            try
            {
                oleDbConn.Open();
            }
            catch (Exception ex)
            {
                throw new ResponseException(GenCodeToolResource.Error_OpenDB, ex);
            }

            StringBuilder tblQuery = new StringBuilder();
            tblQuery.AppendLine(@"SELECT ");
            tblQuery.AppendLine(@"  a.TABLE_NAME, ");
            tblQuery.AppendLine(@"  b.COMMENTS ");
            tblQuery.AppendLine(@"FROM ");
            tblQuery.AppendLine(@"  user_tables a, ");
            tblQuery.AppendLine(@"  user_tab_comments b ");
            tblQuery.AppendLine(@"WHERE ");
            tblQuery.AppendLine(@"  a.TABLE_NAME = '" + tableName + "' ");
            tblQuery.AppendLine(@"; ");

            OleDbCommand command = new OleDbCommand(tblQuery.ToString(), oleDbConn);
            OleDbDataReader dataReader = null;

            try
            {
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    result = new TableInfo();
                    result.Name = dataReader[0].ToString();
                    result.Comment = dataReader[1].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new ResponseException(GenCodeToolResource.Error_GetTable, ex);
            }
            finally
            {
                dataReader.Close();
                oleDbConn.Close();
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
            OleDbConnection oleDbConn = new OleDbConnection(connectionString);

            try
            {
                oleDbConn.Open();
            }
            catch (Exception ex)
            {
                throw new ResponseException(GenCodeToolResource.Error_OpenDB, ex);
            }

            StringBuilder tblQuery = new StringBuilder();
            tblQuery.AppendLine(@"SELECT ");
            tblQuery.AppendLine(@"  a.TABLE_NAME, ");
            tblQuery.AppendLine(@"  b.COMMENTS ");
            tblQuery.AppendLine(@"FROM ");
            tblQuery.AppendLine(@"  user_tables a, ");
            tblQuery.AppendLine(@"  user_tab_comments b ");
            tblQuery.AppendLine(@"WHERE ");
            tblQuery.AppendLine(@"  a.TABLE_NAME = b.TABLE_NAME ");
            tblQuery.AppendLine(@"ORDER BY ");
            tblQuery.AppendLine(@"  a.TABLE_NAME ");
            tblQuery.AppendLine(@"; ");

            OleDbCommand command = new OleDbCommand(tblQuery.ToString(), oleDbConn);
            OleDbDataReader dataReader = null;
            TableInfo tableInfo = null;

            try
            {
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    tableInfo = new TableInfo();
                    tableInfo.Name = dataReader[0].ToString();
                    tableInfo.Comment = dataReader[1].ToString();
                    resultList.Add(tableInfo);
                }
            }
            catch (Exception ex)
            {
                throw new ResponseException(GenCodeToolResource.Error_GetTable, ex);
            }
            finally
            {
                dataReader.Close();
                oleDbConn.Close();
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
            DbType dbType = ToDbType(sqlType, precision, scale);
            return "DbType." + dbType.ToString();
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
            switch (sqlType.ToLower())
            {
                case OracleSqlType.Char:
                    return DbType.AnsiStringFixedLength;
                case OracleSqlType.NChar:
                    return DbType.StringFixedLength;
                case OracleSqlType.NClob:
                case OracleSqlType.NVarChar2:
                    return DbType.String;
                case OracleSqlType.Clob:
                case OracleSqlType.VarChar2:
                case OracleSqlType.Long:
                    return DbType.AnsiString;
                case OracleSqlType.Blob:
                case OracleSqlType.LongRaw:
                    return DbType.Object;
                case OracleSqlType.Number:
                    if (scale == 0)
                    {
                        return DbType.Int64;
                    }
                    else
                    {
                        if (precision <= 28)
                        {
                            return DbType.Decimal;
                        }

                        return DbType.Double;
                    }
                case OracleSqlType.BinaryFloat:
                    return DbType.Single;
                case OracleSqlType.BinaryDouble:
                    return DbType.Double;
                case OracleSqlType.Date:
                case OracleSqlType.TimeStamp:
                    return DbType.DateTime;
                default:
                    return DbType.String;
            }
        }

        #endregion
    }
}
