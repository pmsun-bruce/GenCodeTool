namespace {{project:namespace}}.Dal
{
    #region Reference
    
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    
    using NFramework.DBTool.Common;
    using NFramework.DBTool.QueryTool;
    using NFramework.DBTool.QueryTool.Mssql;
    using NFramework.ExceptionTool;
    
    using {{project:namespace}}.Entity;
    using {{project:namespace}}.Globalization;
    using {{project:namespace}}.IDal;
    using {{project:namespace}}.Searcher;
    
    #endregion
    
    public class {{table:name}}Dal : MssqlDalBase, I{{table:name}}Dal
    {
        #region Public Methods
        
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="{{table:lfname}}">实体对象</param>
        /// <returns>新建成功返回true，新建失败返回false</returns>
        public void Add({{table:name}} {{table:lfname}})
        {
            this.Add({{table:lfname}}, null);
        }
        
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="{{table:lfname}}">实体对象</param>
        /// <param name="tran">数据库事务对象</param>
        /// <returns>新建成功返回true，新建失败返回false</returns>
        public void Add({{table:name}} {{table:lfname}}, ICTransaction tran)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine(@"INSERT INTO ");
            query.AppendLine(@"  [{{table:name}}] (");{{loop:col|rfirst:,: }}
            query.AppendLine(@"    ,[{{col:name}}] ");{{/loop:col}}
            query.AppendLine(@") ");
            query.AppendLine(@"VALUES ( ");{{loop:col|rfirst:,: }}
            query.AppendLine(@"    ,@{{col:name}} ");{{/loop:col}}
            query.AppendLine(@")");

            DBParamCollection paramCollection = new DBParamCollection();{{loop:col}}
            paramCollection.Add(new DBParam("@{{col:name}}", {{table:lfname}}.{{col:name}}, {{col:daltype}}{{if:col:maxlen}}, {{col:maxlen}}{{/if:col:maxlen}}));{{/loop:col}}

            try
            {
                int effectCount = 0;

                if (tran != null)
                {
                    DbTransaction dbTran = ((MssqlTransaction)tran).CurrentTransaction;
                    effectCount = MssqlHelper.ExecuteNonQuery(dbTran, CommandType.Text, query.ToString(), paramCollection);
                }
                else
                {
                    effectCount = MssqlHelper.ExecuteNonQuery(this.CurrentConnectionString, CommandType.Text, query.ToString(), paramCollection);
                }

                if (effectCount == 0)
                {
                    throw new ResponseException((int)ResultCode.NoDataInsert, {{table:name}}Resource.Error_AddFaild);
                }
            }
            catch (SqlException sex)
            {
                switch (sex.Number)
                {
                    case 547:
                        throw new ResponseException((int)ResultCode.FKError, {{table:name}}Resource.Error_AddFK);
                    case 2627:
                        throw new ResponseException((int)ResultCode.UQError, {{table:name}}Resource.Error_AddUQ);
                    default:
                        throw sex;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 统计记录数
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <returns>返回记录数，如果没有返回0</returns>
        public long Count({{table:name}}Searcher {{table:lfname}}Searcher)
        {
            return this.Count({{table:lfname}}Searcher, null);
        }
		
        /// <summary>
        /// 统计记录数
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <param name="tran">数据库事务对象</param>
        /// <returns>返回记录数，如果没有返回0</returns>
        public long Count({{table:name}}Searcher {{table:lfname}}Searcher, ICTransaction tran)
        {
            object count = 0;
            long result = 0;
            {{table:name}}Searcher querySearcher = null;
            MssqlQueryParser queryParser = new MssqlQueryParser();{{if:fk:many}}
            StringBuilder joinQuery = new StringBuilder();{{/if:fk:many}}
            StringBuilder query = new StringBuilder();
            query.AppendLine(@"SELECT ");
            query.AppendLine(@"  COUNT({{pk:col:name}}) ");
            query.AppendLine(@"FROM ");
            query.AppendLine(@"  [{{table:name}}] ");
            
            if ({{table:lfname}}Searcher != null)
            {
                querySearcher = ({{table:name}}Searcher){{table:lfname}}Searcher.Clone();
                querySearcher.TableName = "[{{table:name}}]";
                {{loop:fk}}
                if(querySearcher.Curr{{fk:table:name}} != null)
                {
                    querySearcher.Curr{{fk:table:name}}.TableName = "[{{fk:table:name}}]";
                    joinQuery.AppendLine(@"    LEFT JOIN ");
                    joinQuery.AppendLine(@"      [{{fk:table:name}}] ON([{{fk:table:name}}].[{{fk:pk:col:name}}] = [{{table:name}}].[{{fk:col:name}}]) ");
                }
                {{/loop:fk}}
            }
            
            queryParser.SearcherParse(querySearcher);
            query.AppendLine(joinQuery.ToString());
            
            if (!string.IsNullOrEmpty(queryParser.ConditionString))
            {
                query.AppendLine(@"WHERE ");
                query.AppendLine(@"   " + queryParser.ConditionString);
            }
            
            if (tran != null)
            {
                DbTransaction dbTran = ((MssqlTransaction)tran).CurrentTransaction;
                count = MssqlHelper.ExecuteScalar(dbTran, CommandType.Text, query.ToString(), queryParser.ParamCollection);

            }
            else
            {
                count = MssqlHelper.ExecuteScalar(this.CurrentConnectionString, CommandType.Text, query.ToString(), queryParser.ParamCollection);
            }
            
            return long.TryParse(count.ToString(), out result) ? result : 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Delete(string id)
        {
            this.Delete(id, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tran">中间事务对象</param>
        public void Delete(string id, ICTransaction tran)
        {
            {{table:name}}Searcher querySearcher = new {{table:name}}Searcher();
            querySearcher.{{pk:col:name}}.AddCondition(ConditionFactory.Equal(id));
            this.Delete(querySearcher, tran);
        }
        
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <returns>删除成功返回true，删除失败返回false</returns>
        public void Delete({{table:name}}Searcher {{table:lfname}}Searcher)
        {
            this.Delete({{table:lfname}}Searcher, null);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <param name="tran">数据库事务对象</param>
        /// <returns>删除成功返回true，删除失败返回false</returns>
        public void Delete({{table:name}}Searcher {{table:lfname}}Searcher, ICTransaction tran)
        {
            {{table:name}}Searcher querySearcher = null;
            MssqlQueryParser queryParser = new MssqlQueryParser();
            StringBuilder joinQuery = new StringBuilder();
            StringBuilder query = new StringBuilder();
            query.AppendLine(@"DELETE FROM ");
            query.AppendLine(@"  [{{table:name}}] ");
            query.AppendLine(@"WHERE ");
            query.AppendLine(@"  [{{pk:col:name}}] IN ( ");
            query.AppendLine(@"    SELECT ");
            query.AppendLine(@"      [{{pk:col:name}}] ");
            query.AppendLine(@"    FROM ");
            query.AppendLine(@"      [{{table:name}}] ");

            if ({{table:lfname}}Searcher != null)
            {
                querySearcher = ({{table:name}}Searcher){{table:lfname}}Searcher.Clone();
                querySearcher.TableName = "[{{table:name}}]";
                {{loop:fk}}
                if(querySearcher.Curr{{fk:table:name}} != null)
                {
                    querySearcher.Curr{{fk:table:name}}.TableName = "[{{fk:table:name}}]";
                    joinQuery.AppendLine(@"    LEFT JOIN ");
                    joinQuery.AppendLine(@"      [{{fk:table:name}}] ON([{{fk:table:name}}].[{{fk:pk:col:name}}] = [{{table:name}}].[{{fk:col:name}}]) ");
                }
                {{/loop:fk}}
            }

            queryParser.SearcherParse(querySearcher);
            query.AppendLine(joinQuery.ToString());

            if (!string.IsNullOrEmpty(queryParser.ConditionString))
            {
                query.AppendLine(@"WHERE ");
                query.AppendLine(@"   " + queryParser.ConditionString);
            }

            query.AppendLine(@"); ");

            int effectCount = 0;

            try
            {
                if (tran != null)
                {
                    DbTransaction dbTran = ((MssqlTransaction)tran).CurrentTransaction;
                    effectCount = MssqlHelper.ExecuteNonQuery(dbTran, CommandType.Text, query.ToString(), queryParser.ParamCollection);
                }
                else
                {
                    effectCount = MssqlHelper.ExecuteNonQuery(this.CurrentConnectionString, CommandType.Text, query.ToString(), queryParser.ParamCollection);
                }
            }
            catch (SqlException sex)
            {
                switch (sex.Number)
                {
                    case 547:
                        throw new ResponseException((int)ResultCode.FKError, {{table:name}}Resource.Error_DeleteFK);
                    default:
                        throw sex;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回实例对象集合，如未找到则返回null</returns>
        public {{table:name}} FindSingle(string id)
        {
            return this.FindSingle(id, null);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tran">数据库事务对象</param>
        /// <returns>返回实例对象集合，如未找到则返回null</returns>
        public {{table:name}} FindSingle(string id, ICTransaction tran)
        {
            {{table:name}}Searcher querySearcher = new {{table:name}}Searcher();
            querySearcher.{{pk:col:name}}.AddCondition(ConditionFactory.Equal(id));
            IList<{{table:name}}> resultList = this.FindList(querySearcher, tran);
            return (resultList == null || resultList.Count == 0) ? null : resultList[0];
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <returns>返回实例对象集合，如未找到则返回null</returns>
        public IList<{{table:name}}> FindList({{table:name}}Searcher {{table:lfname}}Searcher)
        {
            PageList<{{table:name}}> pageList = this.FindList({{table:lfname}}Searcher, null, null);
            return pageList == null ? null : pageList.RecordList;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <param name="tran">数据库事务对象</param>
        /// <returns>返回实例对象集合，如未找到则返回null</returns>
        public IList<{{table:name}}> FindList({{table:name}}Searcher {{table:lfname}}Searcher, ICTransaction tran)
        {
            PageList<{{table:name}}> pageList = this.FindList({{table:lfname}}Searcher, null, tran);
            return pageList == null ? null : pageList.RecordList;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <param name="pager">查询分页对象</param>
        /// <returns>返回实例对象集合，如未找到则返回null</returns>
        public PageList<{{table:name}}> FindList({{table:name}}Searcher {{table:lfname}}Searcher, Pager pager)
        {
            return this.FindList({{table:lfname}}Searcher, pager, null);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <param name="pager">查询分页对象</param>
        /// <param name="tran">数据库事务对象</param>
        /// <returns>返回实例对象集合，如未找到则返回null</returns>
        public PageList<{{table:name}}> FindList({{table:name}}Searcher {{table:lfname}}Searcher, Pager pager, ICTransaction tran)
        {
            PageList<{{table:name}}> resultList = new PageList<{{table:name}}>();
            PageDataTable pageDataTable = this.FindDataTable({{table:lfname}}Searcher, pager, tran);
            {{table:name}} ele = null;
            
            if (pageDataTable != null)
            {
                resultList = new PageList<{{table:name}}>();
                resultList.PageIndex = pageDataTable.PageIndex;
                resultList.TotalCount = pageDataTable.TotalCount;

                if (pageDataTable.RecordList != null && pageDataTable.RecordList.Rows.Count > 0)
                {
                    foreach (DataRow aRow in pageDataTable.RecordList.Rows)
                    {
                        ele = new {{table:name}}();
                        {{loop:col}}
                        if (!(aRow["{{col:name}}"] is DBNull))
                        {
                            ele.{{col:name}} = {{col:convert}}aRow["{{col:name}}"]{{/col:convert}};
                        }
                        {{/loop:col}}
                        resultList.RecordList.Add(ele);
                    }
                }
            }

            return resultList;
        }
        
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <returns>返回实例对象集合，如未找到则返回null</returns>
        public DataTable FindDataTable({{table:name}}Searcher {{table:lfname}}Searcher)
        {
            PageDataTable pageDataTable = this.FindDataTable({{table:lfname}}Searcher, null, null);
            return pageDataTable == null ? null : pageDataTable.RecordList;
        }
        
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <param name="tran">数据库事务对象</param>
        /// <returns>返回实例对象集合，如未找到则返回null</returns>
        public DataTable FindDataTable({{table:name}}Searcher {{table:lfname}}Searcher, ICTransaction tran)
        {
            PageDataTable pageDataTable = this.FindDataTable({{table:lfname}}Searcher, null, tran);
            return pageDataTable == null ? null : pageDataTable.RecordList;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <param name="pager">查询分页对象</param>
        /// <returns>返回实例对象集合，如未找到则返回null</returns>
        public PageDataTable FindDataTable({{table:name}}Searcher {{table:lfname}}Searcher, Pager pager)
        {
            return this.FindDataTable({{table:lfname}}Searcher, pager, null);
        }
        
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <param name="pager">查询分页对象</param>
        /// <param name="tran">数据库事务对象</param>
        /// <returns>返回实例对象集合，如未找到则返回null</returns>
        public PageDataTable FindDataTable({{table:name}}Searcher {{table:lfname}}Searcher, Pager pager, ICTransaction tran)
        {
            {{table:name}}Searcher querySearcher = null;
            MssqlQueryParser queryParser = new MssqlQueryParser();
            PageDataTable pDataTable = new PageDataTable();
            DataSet resultSet = null;
            StringBuilder conditionQuery = new StringBuilder();
            StringBuilder joinQuery = new StringBuilder();
            StringBuilder sortQuery = new StringBuilder();

            if ({{table:lfname}}Searcher != null)
            {
                querySearcher = ({{table:name}}Searcher){{table:lfname}}Searcher.Clone();
                querySearcher.TableName = "{{table:name}}";
                {{loop:fk}}
                if(querySearcher.Curr{{fk:table:name}} != null)
                {
                    querySearcher.Curr{{fk:table:name}}.TableName = "[{{fk:table:name}}]";
                    joinQuery.AppendLine(@"    LEFT JOIN ");
                    joinQuery.AppendLine(@"      [{{fk:table:name}}] ON([{{fk:table:name}}].[{{fk:pk:col:name}}] = [{{table:name}}].[{{fk:col:name}}]) ");
                }
                {{/loop:fk}}
            }

            queryParser.SearcherParse(querySearcher);

            if (!string.IsNullOrEmpty(queryParser.ConditionString))
            {
                conditionQuery.AppendLine(@"WHERE ");
                conditionQuery.AppendLine(@"   " + queryParser.ConditionString + " ");
            }

            if (!string.IsNullOrEmpty(queryParser.SortString))
            {
                sortQuery.AppendLine(@"ORDER BY ");
                sortQuery.AppendLine(@"   " + queryParser.SortString + " ");
            }

            StringBuilder query = new StringBuilder();
            query.AppendLine(@"SELECT ");{{loop:col|rfirst:,: }}
            query.AppendLine(@"  ,[{{col:name}}] ");{{/loop:col}}
            query.AppendLine(@"FROM ");
            query.AppendLine(@"   [{{table:name}}] ");
            query.AppendLine(joinQuery.ToString());
            query.AppendLine(conditionQuery.ToString());
            query.AppendLine(sortQuery.ToString());
            query.AppendLine(@"; ");

            if (tran != null)
            {
                DbTransaction dbTran = ((MssqlTransaction)tran).CurrentTransaction;

                if (pager != null)
                {
                    resultSet = MssqlHelper.ExecuteDataSet(dbTran, CommandType.Text, query.ToString(), pager, "{{table:name}}", queryParser.ParamCollection);
                }
                else
                {
                    resultSet = MssqlHelper.ExecuteDataSet(dbTran, CommandType.Text, query.ToString(), queryParser.ParamCollection);
                }
            }
            else
            {
                if (pager != null)
                {
                    resultSet = MssqlHelper.ExecuteDataSet(this.CurrentConnectionString, CommandType.Text, query.ToString(), pager, "{{table:name}}", queryParser.ParamCollection);
                }
                else
                {
                    resultSet = MssqlHelper.ExecuteDataSet(this.CurrentConnectionString, CommandType.Text, query.ToString(), queryParser.ParamCollection);
                }
            }

            if (resultSet != null)
            {
                pDataTable.RecordList = resultSet.Tables[0];
                pDataTable.TotalCount = pDataTable.RecordList.Rows.Count;

                if (pager != null)
                {
                    pDataTable.PageIndex = pager.CurrentPage;
                    pDataTable.TotalCount = this.Count({{table:lfname}}Searcher, tran);
                }
            }

            return pDataTable;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="{{table:lfname}}">更新的实例对象</param>
        /// <returns>更新成功返回true，更新失败返回false</returns>
        public void Update({{table:name}} {{table:lfname}})
        {
            this.Update({{table:lfname}}, null);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="{{table:lfname}}">更新的实例对象</param>
        /// <param name="tran">数据库事务对象</param>
        /// <returns>更新成功返回true，更新失败返回false</returns>
        public void Update({{table:name}} {{table:lfname}}, ICTransaction tran)
        {
            {{table:name}} old{{table:name}} = this.FindSingle({{table:lfname}}.{{pk:col:name}}, tran);
            
            int updateColCount = 0;
            StringBuilder query = new StringBuilder();
            query.AppendLine(@"UPDATE  ");
            query.AppendLine(@"   [{{table:name}}] ");
            query.AppendLine(@"SET ");
            query.AppendLine(@"   [{{pk:col:name}}] = [{{pk:col:name}}] ");
            {{loop:col}}{{if:col:string}}
            if ((!string.IsNullOrEmpty({{table:lfname}}.{{col:name}}) && !{{table:lfname}}.{{col:name}}.Equals(old{{table:name}}.{{col:name}}))
                || (!string.IsNullOrEmpty(old{{table:name}}.{{col:name}}) && !old{{table:name}}.{{col:name}}.Equals({{table:lfname}}.{{col:name}})))
            {
                updateColCount++;
                query.AppendLine(@"  ,[{{col:name}}] = @{{col:name}} ");
            }
            {{/if:col:string}}{{if:col:number}}
            if ({{table:lfname}}.{{col:name}} != old{{table:name}}.{{col:name}})
            {
                updateColCount++;
                query.AppendLine(@"  ,[{{col:name}}] = @{{col:name}} ");
            }
            {{/if:col:number}}{{if:col:int}}
            if ({{table:lfname}}.{{col:name}} != old{{table:name}}.{{col:name}})
            {
                updateColCount++;
                query.AppendLine(@"  ,[{{col:name}}] = @{{col:name}} ");
            }
            {{/if:col:int}}{{if:col:datetime}}
            if (({{table:lfname}}.{{col:name}} != null && {{table:lfname}}.{{col:name}}.CompareTo(old{{table:name}}.{{col:name}}) != 0)
                || (old{{table:name}}.{{col:name}} != null && old{{table:name}}.{{col:name}}.CompareTo({{table:lfname}}.{{col:name}}) != 0))
            {
                updateColCount++;
                query.AppendLine(@"  ,[{{col:name}}] = @{{col:name}} ");
            }
            {{/if:col:datetime}}{{/loop:col}}
            query.AppendLine(@"WHERE ");
            query.AppendLine(@"   [{{pk:col:name}}] = @Old{{pk:col:name}} ");

            if (updateColCount == 0)
            {
                return;
            }
            
            DBParamCollection paramCollection = new DBParamCollection();
            paramCollection.Add(new DBParam("@Old{{pk:col:name}}", old{{table:name}}.{{pk:col:name}}, {{pk:col:daltype}}{{if:pk:col:maxlen}}, {{pk:col:maxlen}}{{/if:pk:col:maxlen}}));{{loop:col}}
            paramCollection.Add(new DBParam("@{{col:name}}", {{table:lfname}}.{{col:name}}, {{col:daltype}}{{if:col:maxlen}}, {{col:maxlen}}{{/if:col:maxlen}}));{{/loop:col}}

            try
            {
                int effectCount = 0;

                if ({{table:lfname}} != null)
                {
                    if (tran != null)
                    {
                        DbTransaction dbTran = ((MssqlTransaction)tran).CurrentTransaction;
                        effectCount = MssqlHelper.ExecuteNonQuery(dbTran, CommandType.Text, query.ToString(), paramCollection);
                    }
                    else
                    {
                        effectCount = MssqlHelper.ExecuteNonQuery(this.CurrentConnectionString, CommandType.Text, query.ToString(), paramCollection);
                    }
                }
                
                // 抛出一个异常
                if (effectCount == 0)
                {
                    throw new ResponseException((int)ResultCode.NoDataUpdate, {{table:name}}Resource.Error_UpdateFaild);
                }
            }
            catch (SqlException sex)
            {
                switch (sex.Number)
                {
                    case 547:
                        throw new ResponseException((int)ResultCode.FKError, {{table:name}}Resource.Error_UpdateFK);
                    case 2627:
                        throw new ResponseException((int)ResultCode.UQError, {{table:name}}Resource.Error_UpdateUQ);
                    default:
                        throw sex;
                }
            }
            catch
            {
                throw;
            }
        }
        
        #endregion

        #region Public Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dalFactory">传入当前指定的DalFactory对象</param>
        public {{table:name}}Dal(MssqlDalFactoryBase dalFactory)
            : base(dalFactory)
        {

        }

        #endregion
    }
}