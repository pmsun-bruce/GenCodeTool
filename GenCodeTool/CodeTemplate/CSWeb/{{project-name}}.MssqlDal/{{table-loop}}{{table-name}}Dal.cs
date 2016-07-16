namespace {{project:namespace}}.Dal 
{
	#region Reference

    using System;
    using System.Collections;
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
    using NFramework.ObjectTool;

    using {{project:namespace}}.IDal;
    using {{project:namespace}}.Entity;
    using {{project:namespace}}.Globalization;
    using {{project:namespace}}.Searcher;

	#endregion

    /// <summary>
    /// {{table:comment}}数据操作类
    /// </summary>
    public class {{table:name}}Dal : MssqlDalBase, I{{table:name}}Dal
    {
        /// <summary>
        /// 添加一个{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfname}}">{{table:comment}}</param>
        public void Add({{table:name}} {{table:lfname}})
        {
            this.Add({{table:lfname}}, null);
        }

        /// <summary>
        /// 添加一个{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfname}}">{{table:comment}}</param>
        /// <param name="tran">中间数据库事务对象</param>
        public void Add({{table:name}} {{table:lfname}}, ICTransaction tran)
        {
            {{table:name}} cloneObj = ObjectFactory.Clone<{{table:name}}>({{table:lfname}});
            StringBuilder query = new StringBuilder();
            query.AppendLine(@"INSERT INTO ");
            query.AppendLine(@"  {{table:name}} (");{{loop:col|rfirst:,: |}}
            query.AppendLine(@"    ,{{col:name}} ");{{/loop:col}}
            query.AppendLine(@") ");
            query.AppendLine(@"VALUES (");{{loop:col|rfirst:,: |}}
            query.AppendLine(@"    ,@{{col:name}} ");{{/loop:col}}
            query.AppendLine(@"); ");
            
            cloneObj.{{pk:col:name}} = KeyGenerator.GenNewGuidKey();{{if:col:exist|CreateTime|}}
            cloneObj.CreateTime = DateTime.Now;{{/if:col:exist}}{{if:col:exist|UpdateTime|}}
            cloneObj.UpdateTime = DateTime.Now;{{/if:col:exist}}{{if:col:exist|RVersion|}}
            cloneObj.RVersion = 1;{{/if:col:exist}}

            DBParamCollection paramCollection = new DBParamCollection();{{loop:col}}
            paramCollection.Add(new DBParam("@{{col:name}}", cloneObj.{{col:name}}, {{col:daltype}}{{if:col:string}}, {{col:maxlen}}{{/if:col:string}}{{if:col:number}}, {{col:precision}}, {{col:scale}}{{/if:col:number}}));{{/loop:col}}

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
                    throw new ResponseException((int)ResultCode.NoDataInsert, {{table:name}}Resource.ErrorNoAdd);
                }

                {{table:lfname}}.{{pk:col:name}} = cloneObj.{{pk:col:name}};
                {{table:lfname}}.CreateTime = cloneObj.CreateTime;
                {{table:lfname}}.UpdateTime = cloneObj.UpdateTime;
                {{table:lfname}}.RVersion = cloneObj.RVersion;
            }
            catch (SqlException sex)
            {
                switch (sex.Number)
                {
                    case 547:
                        throw new ResponseException((int)ResultCode.FKError, {{table:name}}Resource.ErrorFK, sex);
                    case 2627:
                        throw new ResponseException((int)ResultCode.UQError, {{table:name}}Resource.ErrorUQ, sex);
                    default:
                        throw new Exception(sex.Message, sex);
                }

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 批量添加{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfname}}List">{{table:comment}}列表</param>
        public void Add(IList<{{table:name}}> {{table:lfname}}List)
        {
            ICTransaction tran = DalManager.DalFactory.BeginTransaction();

            try
            {
                this.Add({{table:lfname}}List, tran);
                tran.Commit();
            }
            catch (ResponseException rex)
            {
                tran.RollBack();
                throw new ResponseException(rex.ResultCode, rex.Message, rex);
            }
            catch (Exception ex)
            {
                tran.RollBack();
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 批量添加{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfname}}List">{{table:comment}}</param>
        /// <param name="tran">中间数据库事务对象</param>
        public void Add(IList<{{table:name}}> {{table:lfname}}List, ICTransaction tran)
        {
            if ({{table:lfname}}List == null)
            {
                return;
            }

            IList<{{table:name}}> cloneList = ObjectFactory.Clone<IList<{{table:name}}>>({{table:lfname}}List);

            foreach ({{table:name}} {{table:lfname}} in cloneList)
            {
                this.Add({{table:lfname}}, tran);
            }

            for (int i = 0; i < {{table:lfname}}List.Count; i++)
            {
                {{table:lfname}}List[i].{{pk:col:name}}  = cloneList[i].{{pk:col:name}} ;
                {{table:lfname}}List[i].CreateTime = cloneList[i].CreateTime;
                {{table:lfname}}List[i].UpdateTime = cloneList[i].UpdateTime;
                {{table:lfname}}List[i].RVersion = cloneList[i].RVersion;
            }
        }

        /// <summary>
        /// 查找指定条件的{{table:comment}}的数量
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">{{table:comment}}查询对象</param>
        /// <returns>返回查询到的数量</returns>
        public long Count({{table:name}}Searcher {{table:lfname}}Searcher)
        {
            return this.Count({{table:lfname}}Searcher, null);
        }

        /// <summary>
        /// 查找指定条件的{{table:comment}}的数量
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回查询到的数量</returns>
        public long Count({{table:name}}Searcher {{table:lfname}}Searcher, ICTransaction tran)
        {
            object count = 0;
            long result = 0;
            {{table:name}}Searcher querySearcher = null;
            MssqlQueryParser queryParser = new MssqlQueryParser();
            StringBuilder query = new StringBuilder();
            query.AppendLine(@"SELECT ");
            query.AppendLine(@"  COUNT({{pk:col:name}}) ");
            query.AppendLine(@"FROM ");
            query.AppendLine(@"  {{table:name}} A ");

            if ({{table:lfname}}Searcher != null)
            {
                querySearcher = ({{table:name}}Searcher){{table:lfname}}Searcher.Clone();
                querySearcher.TableName = "A";
                {{loop:fk}}
                if (querySearcher.Curr{{fk:table:name}} != null)
                {
                    querySearcher.Curr{{fk:table:name}}.TableName = "T{{fk:col:id}}";
                    query.AppendLine(@"LEFT JOIN ");
                    query.AppendLine(@"  {{fk:table:name}} T{{fk:col:id}} ON(A.{{fk:col:name}} = T{{fk:col:id}}.{{fk:pk:col:name}}) ");
                }
{{/loop:fk}}
            }

            queryParser.SearcherParse(querySearcher);

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
        /// 删除指定PK的{{table:comment}}
        /// </summary>
        /// <param name="{{pk:col:lfname}}">{{table:comment}}的PK值</param>
        public void Delete({{pk:col:codetype}} {{pk:col:lfname}})
        {
            this.Delete({{pk:col:lfname}}, null);
        }

        /// <summary>
        /// 删除指定PK的{{table:comment}}
        /// </summary>
        /// <param name="{{pk:col:lfname}}">{{table:comment}}的PK值</param>
        /// <param name="tran">中间数据库事务对象</param>
        public void Delete({{pk:col:codetype}} {{pk:col:lfname}}, ICTransaction tran)
        {
            {{table:name}}Searcher querySearcher = new {{table:name}}Searcher();
            querySearcher.{{pk:col:name}}.AddCondition(ConditionFactory.Equal({{pk:col:lfname}}));
            this.Delete(querySearcher, tran);
        }

        /// <summary>
        /// 删除指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">{{table:comment}}查询对象</param>
        public void Delete({{table:name}}Searcher {{table:lfname}}Searcher)
        {
            this.Delete({{table:lfname}}Searcher, null);
        }

        /// <summary>
        /// 删除指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        public void Delete({{table:name}}Searcher {{table:lfname}}Searcher, ICTransaction tran)
        {
            {{table:name}}Searcher querySearcher = null;
            MssqlQueryParser queryParser = new MssqlQueryParser();
            StringBuilder query = new StringBuilder();
            query.AppendLine(@"DELETE FROM ");
            query.AppendLine(@"  {{table:name}} ");

            if ({{table:lfname}}Searcher != null)
            {
                querySearcher = ({{table:name}}Searcher){{table:lfname}}Searcher.Clone();
                {{loop:fk}}
                if (querySearcher.Curr{{fk:table:name}} != null)
                {
                    querySearcher.Curr{{fk:table:name}}.TableName = "T{{fk:col:id}}";
                    query.AppendLine(@"LEFT JOIN ");
                    query.AppendLine(@"  {{fk:table:name}} T{{fk:col:id}} ON(A.{{fk:col:name}} = T{{fk:col:id}}.{{fk:pk:col:name}}) ");
                }
{{/loop:fk}}
            }
            
            queryParser.SearcherParse(querySearcher);

            if (!string.IsNullOrEmpty(queryParser.ConditionString))
            {
                query.AppendLine(@"WHERE ");
                query.AppendLine(@"   " + queryParser.ConditionString);
            }

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
                        throw new ResponseException((int)ResultCode.FKError, {{table:name}}Resource.ErrorFK, sex);
                    default:
                        throw new Exception(sex.Message, sex);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 查找指定PK的{{table:comment}}
        /// </summary>
        /// <param name="{{pk:col:lfname}}">{{table:comment}}的PK值</param>
        /// <returns>找到返回{{table:comment}}对象，未找到则返回null</returns>
        public {{table:name}} FindSingle({{pk:col:codetype}} {{pk:col:lfname}})
        {
            return this.FindSingle({{pk:col:lfname}}, null);
        }

        /// <summary>
        /// 查找指定PK的{{table:comment}}
        /// </summary>
        /// <param name="{{pk:col:lfname}}">{{table:comment}}的PK值</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>找到返回{{table:comment}}对象，未找到则返回null</returns>
        public {{table:name}} FindSingle({{pk:col:codetype}} {{pk:col:lfname}}, ICTransaction tran)
        {
            {{table:name}}Searcher querySearcher = new {{table:name}}Searcher();
            querySearcher.{{pk:col:name}}.AddCondition(ConditionFactory.Equal({{pk:col:lfname}}));
            IList<{{table:name}}> resultList = this.FindList(querySearcher, tran);
            return (resultList == null || resultList.Count == 0) ? null : resultList[0];
        }

        /// <summary>
        /// 查找指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">{{table:comment}}查询对象</param>
        /// <returns>找到返回{{table:comment}}对象列表，未找到则返回null</returns>
        public IList<{{table:name}}> FindList({{table:name}}Searcher {{table:lfname}}Searcher)
        {
            PageList<{{table:name}}> pageList = this.FindList({{table:lfname}}Searcher, null, null);
            return (pageList == null || pageList.TotalCount == 0) ? null : pageList.RecordList;
        }

        /// <summary>
        /// 查找指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>找到返回{{table:comment}}对象列表，未找到则返回null</returns>
        public IList<{{table:name}}> FindList({{table:name}}Searcher {{table:lfname}}Searcher, ICTransaction tran)
        {
            PageList<{{table:name}}> pageList = this.FindList({{table:lfname}}Searcher, null, tran);
            return (pageList == null || pageList.TotalCount == 0) ? null : pageList.RecordList;
        }

        /// <summary>
        /// 查找指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回分页结果对象，通过分页结果对象中的TotalCount判断是否有查找到的记录</returns>
        public PageList<{{table:name}}> FindList({{table:name}}Searcher {{table:lfname}}Searcher, Pager pager)
        {
            return this.FindList({{table:lfname}}Searcher, pager, null);
        }

        /// <summary>
        /// 查找指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回分页结果对象，通过分页结果对象中的TotalCount判断是否有查找到的记录</returns>
        public PageList<{{table:name}}> FindList({{table:name}}Searcher {{table:lfname}}Searcher, Pager pager, ICTransaction tran)
        {
            PageList<{{table:name}}> resultList = new PageList<{{table:name}}>();
            PageDataTable pageDataTable = this.FindDataTable({{table:lfname}}Searcher, pager, tran);
            {{table:name}} ele = null;

            if (pageDataTable != null)
            {
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
        /// 查找指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">{{table:comment}}查询对象</param>
        /// <returns>找到记录则返回DataTable，如果未找到则返回null</returns>
        public DataTable FindDataTable({{table:name}}Searcher {{table:lfname}}Searcher)
        {
            PageDataTable pageDataTable = this.FindDataTable({{table:lfname}}Searcher, null, null);
            return (pageDataTable == null || pageDataTable.TotalCount == 0) ? null : pageDataTable.RecordList;
        }

        /// <summary>
        /// 查找指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>找到记录则返回DataTable，如果未找到则返回null</returns>
        public DataTable FindDataTable({{table:name}}Searcher {{table:lfname}}Searcher, ICTransaction tran)
        {
            PageDataTable pageDataTable = this.FindDataTable({{table:lfname}}Searcher, null, tran);
            return (pageDataTable == null || pageDataTable.TotalCount == 0) ? null : pageDataTable.RecordList;
        }

        /// <summary>
        /// 查找指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回分页结果对象，通过分页结果对象中的TotalCount判断是否有查找到的记录</returns>
        public PageDataTable FindDataTable({{table:name}}Searcher {{table:lfname}}Searcher, Pager pager)
        {
            return this.FindDataTable({{table:lfname}}Searcher, pager, null);
        }

        /// <summary>
        /// 查找指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回分页结果对象，通过分页结果对象中的TotalCount判断是否有查找到的记录</returns>
        public PageDataTable FindDataTable({{table:name}}Searcher {{table:lfname}}Searcher, Pager pager, ICTransaction tran)
        {
            {{table:name}}Searcher querySearcher = null;
            MssqlQueryParser queryParser = new MssqlQueryParser();
            PageDataTable pDataTable = new PageDataTable();
            DataSet resultSet = null;
            StringBuilder joinQuery = new StringBuilder();
            StringBuilder conditionQuery = new StringBuilder();
            StringBuilder sortQuery = new StringBuilder();

            if ({{table:lfname}}Searcher != null)
            {
                querySearcher = ({{table:name}}Searcher){{table:lfname}}Searcher.Clone();
                querySearcher.TableName = "A";
                {{loop:fk}}
                if (querySearcher.Curr{{fk:table:name}} != null)
                {
                    querySearcher.Curr{{fk:table:name}}.TableName = "T{{fk:col:id}}";
                    joinQuery.AppendLine(@"LEFT JOIN ");
                    joinQuery.AppendLine(@"  {{fk:table:name}} T{{fk:col:id}} ON(A.{{fk:col:name}} = T{{fk:col:id}}.{{fk:pk:col:name}}) ");
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
            query.AppendLine(@"SELECT ");{{loop:col|rfirst:,: |}}
            query.AppendLine(@"   ,{{col:name}} ");{{/loop:col}}
            query.AppendLine(@"FROM ");
            query.AppendLine(@"   {{table:name}} A ");
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
        /// 更新{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfname}}">{{table:comment}}</param>
        public void Update({{table:name}} {{table:lfname}})
        {
            this.Update({{table:lfname}}, null);
        }

        /// <summary>
        /// 更新{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfname}}">{{table:comment}}</param>
        /// <param name="tran">中间数据库事务对象</param>
        public void Update({{table:name}} {{table:lfname}}, ICTransaction tran)
        {
            {{table:name}} cloneObj = ObjectFactory.Clone<{{table:name}}>({{table:lfname}});
            {{table:name}} old{{table:name}} = this.FindSingle(cloneObj.{{table:name}}Id, tran);

            if (cloneObj.RVersion != old{{table:name}}.RVersion)
            {
                throw new ResponseException((int)ResultCode.VersionChanged, {{table:name}}Resource.ErrorVersionChanged);
            }
            
            int updateColCount = 0;
            StringBuilder query = new StringBuilder();
            query.AppendLine(@"UPDATE ");
            query.AppendLine(@"   {{table:name}} ");
            query.AppendLine(@"SET ");
            query.AppendLine(@"   {{pk:col:name}} = {{pk:col:name}} ");
            {{loop:col|ignparam:pk,CreaterId,CreateTime,UpdateTime,RVersion|}}{{if:col:string}}
            if ((!string.IsNullOrEmpty(cloneObj.{{col:name}}) && !cloneObj.{{col:name}}.Equals(old{{table:name}}.{{col:name}}))
                || (!string.IsNullOrEmpty(old{{table:name}}.{{col:name}}) && !old{{table:name}}.{{col:name}}.Equals(cloneObj.{{col:name}})))
            {
                updateColCount++;
                query.AppendLine(@"  ,{{col:name}} = @{{col:name}} ");
            }
            {{/if:col:string}}{{if:col:number}}
            if (cloneObj.{{col:name}} != old{{table:name}}.{{col:name}})
            {
                updateColCount++;
                query.AppendLine(@"  ,{{col:name}} = @{{col:name}} ");
            }
            {{/if:col:number}}{{/loop:col}}
            query.AppendLine(@"  ,UpdateTime = @UpdateTime ");
            query.AppendLine(@"  ,RVersion = @RVersion ");
            query.AppendLine(@"WHERE ");
            query.AppendLine(@"   {{pk:col:name}} = @{{pk:col:name}} ");

            if (updateColCount == 0)
            {
                return;
            }

            cloneObj.UpdateTime = DateTime.Now;
            cloneObj.RVersion++;
            DBParamCollection paramCollection = new DBParamCollection();
            paramCollection.Add(new DBParam("@{{pk:col:name}}", cloneObj.{{pk:col:name}}, {{pk:col:daltype}}{{if:col:string}}, {{pk:col:maxlen}}{{/if:col:string}}));{{loop:col|ignparam:pk,CreaterId,CreateTime|}}
            paramCollection.Add(new DBParam("@{{col:name}}", cloneObj.{{col:name}}, {{col:daltype}}{{if:col:string}}, {{col:maxlen}}{{/if:col:string}}{{if:col:number}}, {{col:precision}}, {{col:scale}}{{/if:col:number}}));{{/loop:col}}
            
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
                    throw new ResponseException((int)ResultCode.NoDataUpdate, {{table:name}}Resource.ErrorNoUpdate);
                }

                {{table:lfname}}.UpdateTime = cloneObj.UpdateTime;
                {{table:lfname}}.RVersion = cloneObj.RVersion;
            }
            catch (SqlException sex)
            {
                switch (sex.Number)
                {
                    case 547:
                        throw new ResponseException((int)ResultCode.FKError, {{table:name}}Resource.ErrorFK, sex);
                    case 2627:
                        throw new ResponseException((int)ResultCode.UQError, {{table:name}}Resource.ErrorUQ, sex);
                    default:
                        throw new Exception(sex.Message, sex);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 批量更新{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfname}}">{{table:comment}}</param>
        public void Update(IList<{{table:name}}> {{table:lfname}}List)
        {
            ICTransaction tran = DalManager.DalFactory.BeginTransaction();

            try
            {
                this.Update({{table:lfname}}List, tran);
                tran.Commit();
            }
            catch (ResponseException rex)
            {
                tran.RollBack();
                throw new ResponseException(rex.ResultCode, rex.Message, rex);
            }
            catch(Exception ex)
            {
                tran.RollBack();
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 批量更新{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfname}}">{{table:comment}}</param>
        /// <param name="tran">中间数据库事务对象</param>
        public void Update(IList<{{table:name}}> {{table:lfname}}List, ICTransaction tran)
        {
            if ({{table:lfname}}List == null)
            {
                return;
            }

            IList<{{table:name}}> cloneList = ObjectFactory.Clone<IList<{{table:name}}>>({{table:lfname}}List);

            foreach ({{table:name}} {{table:lfname}} in cloneList)
            {
                this.Update({{table:lfname}}, tran);
            }

            for (int i = 0; i < {{table:lfname}}List.Count; i++)
            {
                {{table:lfname}}List[i].UpdateTime = cloneList[i].UpdateTime;
                {{table:lfname}}List[i].RVersion = cloneList[i].RVersion;
            }
        }

        /// <summary>
        /// 获取查找{{table:comment}} PK的SQL语句，用于其他SQL的子查询或拼接使用
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="paramCollection">返回当前查询对象中所有的条件对象集合</param>
        /// <return>返回拼接后的SQL语句</return>
        public string GetPKSQLCommand({{table:name}}Searcher {{table:lfname}}Searcher, out DBParamCollection paramCollection)
        {
            {{table:name}}Searcher querySearcher = null;
            MssqlQueryParser queryParser = new MssqlQueryParser();
            StringBuilder joinQuery = new StringBuilder();
            StringBuilder conditionQuery = new StringBuilder();

            if ({{table:lfname}}Searcher != null)
            {
                querySearcher = ({{table:name}}Searcher){{table:lfname}}Searcher.Clone();
                querySearcher.TableName = "A";
                {{loop:fk}}
                if (querySearcher.Curr{{fk:table:name}} != null)
                {
                    querySearcher.Curr{{fk:table:name}}.TableName = "T{{fk:col:id}}";
                    joinQuery.AppendLine(@"LEFT JOIN ");
                    joinQuery.AppendLine(@"  {{fk:table:name}} T{{fk:col:id}} ON(A.{{fk:col:name}} = T{{fk:col:id}}.{{fk:pk:col:name}}) ");
                }
{{/loop:fk}}
            }

            queryParser.SearcherParse(querySearcher);

            if (!string.IsNullOrEmpty(queryParser.ConditionString))
            {
                conditionQuery.AppendLine(@"WHERE ");
                conditionQuery.AppendLine(@"   " + queryParser.ConditionString + " ");
            }

            StringBuilder query = new StringBuilder();
            query.AppendLine(@"SELECT ");{{loop:col|rfirst:,: |}}
            query.AppendLine(@"   ,{{col:name}} ");{{/loop:col}}
            query.AppendLine(@"FROM ");
            query.AppendLine(@"   {{table:name}} A ");
            query.AppendLine(joinQuery.ToString());
            query.AppendLine(conditionQuery.ToString());
            paramCollection = queryParser.ParamCollection;
            return query.ToString();
        } 
        
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