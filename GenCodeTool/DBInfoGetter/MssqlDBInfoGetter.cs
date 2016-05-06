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

    #endregion

    /// <summary>
    /// MS SQL SERVER对应的数据库信息获取器
    /// </summary>
    public class MssqlDBInfoGetter : IDBInfoGetter
    {
        #region Fields & Properties

        /// <summary>
        /// 获取器名称：MS SQL Use DbType
        /// </summary>
        public string GetterName
        {
            get { return "MS SQL Use DbType"; }
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
            SqlConnection sqlConn = new SqlConnection(connectionString);

            try
            {
                sqlConn.Open();
            }
            catch (Exception ex)
            {
                throw new ResponseException(GenCodeToolResource.Error_OpenDB, ex);
            }

            StringBuilder colQuery = new StringBuilder();
            colQuery.AppendLine(@"SELECT ");
            colQuery.AppendLine(@"    A.colorder, ");
            colQuery.AppendLine(@"    A.name AS columnname, ");
            colQuery.AppendLine(@"    B.name AS typename, ");
            colQuery.AppendLine(@"    A.length, ");
            colQuery.AppendLine(@"    A.isnullable,");
            colQuery.AppendLine(@"    A.colid, ");
            colQuery.AppendLine(@"    D.value AS comment,");
            colQuery.AppendLine(@"    A.prec,");
            colQuery.AppendLine(@"    A.scale,");
            colQuery.AppendLine(@"    G.text AS initialvalue,");
            colQuery.AppendLine(@"    isunique = CASE WHEN EXISTS(SELECT ");
            colQuery.AppendLine(@"                                    1 ");
            colQuery.AppendLine(@"                                FROM ");
            colQuery.AppendLine(@"                                    sys.indexes U1 ");
            colQuery.AppendLine(@"                                LEFT JOIN ");
            colQuery.AppendLine(@"                                    sys.index_columns U2 ON(U1.object_id = U2.object_id AND U1.index_id = u2.index_id) ");
            colQuery.AppendLine(@"                                WHERE ");
            colQuery.AppendLine(@"                                    U1.object_id = A.id ");
            colQuery.AppendLine(@"                                AND ");
            colQuery.AppendLine(@"                                    U2.column_id = A.colid ");
            colQuery.AppendLine(@"                                AND ");
            colQuery.AppendLine(@"                                    U1.is_unique = 1) ");
            colQuery.AppendLine(@"               THEN ");
            colQuery.AppendLine(@"                1 ");
            colQuery.AppendLine(@"               ELSE ");
            colQuery.AppendLine(@"                0 ");
            colQuery.AppendLine(@"               END,");
            colQuery.AppendLine(@"    ispkey = CASE WHEN EXISTS(SELECT ");
            colQuery.AppendLine(@"                                1 ");
            colQuery.AppendLine(@"                              FROM ");
            colQuery.AppendLine(@"                                sysobjects ");
            colQuery.AppendLine(@"                              WHERE ");
            colQuery.AppendLine(@"                                xtype='PK' ");
            colQuery.AppendLine(@"                              AND ");
            colQuery.AppendLine(@"                                parent_obj=A.id ");
            colQuery.AppendLine(@"                              AND ");
            colQuery.AppendLine(@"                                name IN (SELECT ");
            colQuery.AppendLine(@"                                            name ");
            colQuery.AppendLine(@"                                         FROM ");
            colQuery.AppendLine(@"                                            sysindexes ");
            colQuery.AppendLine(@"                                         WHERE ");
            colQuery.AppendLine(@"                                            indid IN(SELECT ");
            colQuery.AppendLine(@"                                                        indid ");
            colQuery.AppendLine(@"                                                     FROM ");
            colQuery.AppendLine(@"                                                        sysindexkeys ");
            colQuery.AppendLine(@"                                                     WHERE ");
            colQuery.AppendLine(@"                                                        id=A.id ");
            colQuery.AppendLine(@"                                                     AND ");
            colQuery.AppendLine(@"                                                        colid=A.colid))) ");
            colQuery.AppendLine(@"             THEN ");
            colQuery.AppendLine(@"                1 ");
            colQuery.AppendLine(@"             ELSE ");
            colQuery.AppendLine(@"                0 ");
            colQuery.AppendLine(@"             END,");
            colQuery.AppendLine(@"    isfkey = CASE WHEN E.fkey IS NOT NULL THEN 1 ELSE 0 END,");
            colQuery.AppendLine(@"    I.name AS fkname,");
            colQuery.AppendLine(@"    H.name AS fkcolumn,");
            colQuery.AppendLine(@"    F.name AS fktable");
            colQuery.AppendLine(@"FROM ");
            colQuery.AppendLine(@"    syscolumns A ");
            colQuery.AppendLine(@"LEFT JOIN ");
            colQuery.AppendLine(@"    systypes B ON(A.xtype = B.xtype) ");
            colQuery.AppendLine(@"LEFT JOIN ");
            colQuery.AppendLine(@"    sysobjects C ON(C.id = A.id AND C.xtype = 'U' AND C.name <> 'dtproperties')  ");
            colQuery.AppendLine(@"LEFT JOIN ");
            colQuery.AppendLine(@"    sys.extended_properties D ON(D.minor_id = A.colid AND D.major_id = A.id) ");
            colQuery.AppendLine(@"LEFT JOIN ");
            colQuery.AppendLine(@"    sysforeignkeys E ON(E.fkeyid = A.id and E.fkey = A.colid) ");
            colQuery.AppendLine(@"LEFT JOIN ");
            colQuery.AppendLine(@"    sysobjects F ON(E.rkeyid = F.id) ");
            colQuery.AppendLine(@"LEFT JOIN ");
            colQuery.AppendLine(@"    syscomments G ON(A.cdefault = G.id) ");
            colQuery.AppendLine(@"LEFT JOIN ");
            colQuery.AppendLine(@"    syscolumns H ON(E.rkeyid = H.id and E.rkey = H.colid) ");
            colQuery.AppendLine(@"LEFT JOIN ");
            colQuery.AppendLine(@"    sysobjects I ON(I.id = E.constid) ");
            colQuery.AppendLine(@"WHERE ");
            colQuery.AppendLine(@"    A.id = (SELECT ");
            colQuery.AppendLine(@"                id ");
            colQuery.AppendLine(@"            FROM ");
            colQuery.AppendLine(@"                sysobjects ");
            colQuery.AppendLine(@"            WHERE ");
            colQuery.AppendLine(@"                id = OBJECT_ID(@TableName)) ");
            colQuery.AppendLine(@"            AND ");
            colQuery.AppendLine(@"                B.name <> 'sysname' ");
            colQuery.AppendLine(@"ORDER BY ");
            colQuery.AppendLine(@"    A.colorder ASC");
            
            SqlCommand command = new SqlCommand(colQuery.ToString(), sqlConn);
            SqlDataReader dataReader = null;
            ColumnInfo colInfo = null;
            int tmpInt = 0;

            try
            {
                command.Parameters.Add(new SqlParameter("@TableName", tableInfo.Name));
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    colInfo = new ColumnInfo();
                    colInfo.Name = dataReader[1].ToString();
                    colInfo.SqlType = dataReader[2].ToString().ToLower();
                    colInfo.MaxLength = !int.TryParse(dataReader[3].ToString(), out tmpInt) ? 0 : tmpInt;
                    colInfo.IsNullable = "1".Equals(dataReader[4].ToString()) ? true : false;
                    colInfo.ColId = int.Parse(dataReader[5].ToString());
                    colInfo.Comment = dataReader[6].ToString();
                    colInfo.Precision = !int.TryParse(dataReader[7].ToString(), out tmpInt) ? 0 : tmpInt;
                    colInfo.Scale = !int.TryParse(dataReader[8].ToString(), out tmpInt) ? 0 : tmpInt;
                    colInfo.DefaultValue = dataReader[9].ToString();
                    colInfo.IsUnique = "1".Equals(dataReader[10].ToString()) ? true : false;
                    colInfo.IsPK = "1".Equals(dataReader[11].ToString()) ? true : false;
                    colInfo.IsFK = "1".Equals(dataReader[12].ToString()) ? true : false;

                    if (colInfo.IsFK)
                    {
                        colInfo.FKName = dataReader[13].ToString();
                        colInfo.FKColumnName = dataReader[14].ToString();
                        colInfo.FKTableName = dataReader[15].ToString();
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
                sqlConn.Close();
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
            SqlConnection sqlConn = new SqlConnection(connectionString);

            try
            {
                sqlConn.Open();
            }
            catch (Exception ex)
            {
                throw new ResponseException(GenCodeToolResource.Error_OpenDB, ex);
            }

            StringBuilder tblQuery = new StringBuilder();
            tblQuery.AppendLine(@"SELECT ");
            tblQuery.AppendLine(@"    A.name, B.value AS comment ");
            tblQuery.AppendLine(@"FROM ");
            tblQuery.AppendLine(@"    sysobjects A ");
            tblQuery.AppendLine(@"LEFT JOIN ");
            tblQuery.AppendLine(@"    sys.extended_properties B ON(A.id = B.major_id AND B.minor_id = 0 AND B.name = 'MS_Description')");
            tblQuery.AppendLine(@"WHERE ");
            tblQuery.AppendLine(@"    type='U' ");
            tblQuery.AppendLine(@"  AND ");
            tblQuery.AppendLine(@"    A.name=@TableName ");
            tblQuery.AppendLine(@"ORDER BY ");
            tblQuery.AppendLine(@"    name ASC");

            SqlCommand command = new SqlCommand(tblQuery.ToString(), sqlConn);
            SqlDataReader dataReader = null;

            try
            {
                command.Parameters.Add(new SqlParameter("@TableName", tableName));
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
                sqlConn.Close();
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
            SqlConnection sqlConn = new SqlConnection(connectionString);

            try
            {
                sqlConn.Open();
            }
            catch (Exception ex)
            {
                throw new ResponseException(GenCodeToolResource.Error_OpenDB, ex);
            }

            StringBuilder tblQuery = new StringBuilder();
            tblQuery.AppendLine(@"SELECT ");
            tblQuery.AppendLine(@"    A.name, B.value AS comment ");
            tblQuery.AppendLine(@"FROM ");
            tblQuery.AppendLine(@"    sysobjects A ");
            tblQuery.AppendLine(@"LEFT JOIN ");
            tblQuery.AppendLine(@"    sys.extended_properties B ON(A.id = B.major_id AND B.minor_id = 0 AND B.name = 'MS_Description')");
            tblQuery.AppendLine(@"WHERE ");
            tblQuery.AppendLine(@"    type='U' ");
            tblQuery.AppendLine(@"ORDER BY ");
            tblQuery.AppendLine(@"    name ASC");

            SqlCommand command = new SqlCommand(tblQuery.ToString(), sqlConn);
            SqlDataReader dataReader = null;
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
                sqlConn.Close();
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
            switch (sqlType)
            {
                case MssqlSqlType.Char:
                    return DbType.AnsiStringFixedLength;
                case MssqlSqlType.NChar:
                    return DbType.StringFixedLength;
                case MssqlSqlType.NText:
                case MssqlSqlType.NVarChar:
                    return DbType.String;
                case MssqlSqlType.Text:
                case MssqlSqlType.VarChar:
                    return DbType.AnsiString;
                case MssqlSqlType.Int:
                    return DbType.Int32;
                case MssqlSqlType.BigInt:
                    return DbType.Int64;
                case MssqlSqlType.Decimal:
                case MssqlSqlType.Money:
                    return DbType.Decimal;
                case MssqlSqlType.DateTime:
                case MssqlSqlType.DateTime2:
                case MssqlSqlType.DateTimeOffset:
                    return DbType.DateTime;
                default:
                    return DbType.String;
            }
        }

        #endregion
    }
}
