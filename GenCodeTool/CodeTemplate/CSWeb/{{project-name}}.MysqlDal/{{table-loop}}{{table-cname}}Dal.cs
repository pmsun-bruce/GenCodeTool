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
    
    using MySql.Data.MySqlClient;

    using NFramework.DBTool.Common;
    using NFramework.DBTool.QueryTool;
    using NFramework.DBTool.QueryTool.Mysql;
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
    public class {{table:cname}}Dal : MysqlDalBase, I{{table:cname}}Dal
    {
        /// <summary>
        /// 添加一个{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}">{{table:comment}}</param>
        public void Add({{table:cname}} {{table:lfcname}})
        {
            this.Add({{table:lfcname}}, null);
        }

        /// <summary>
        /// 添加一个{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}">{{table:comment}}</param>
        /// <param name="tran">中间数据库事务对象</param>
        public void Add({{table:cname}} {{table:lfcname}}, ICTransaction tran)
        {
            {{table:cname}} cloneObj = ObjectFactory.Clone<{{table:cname}}>({{table:lfcname}});
            StringBuilder query = new StringBuilder();
            query.AppendLine(@"INSERT INTO ");
            query.AppendLine(@"  `{{table:name}}` (");{{loop:col|rfirst:,: |}}
            query.AppendLine(@"    ,`{{col:name}}` ");{{/loop:col}}
            query.AppendLine(@") ");
            query.AppendLine(@"VALUES (");{{loop:col|rfirst:,: |}}
            query.AppendLine(@"    ,@{{col:pname}} ");{{/loop:col}}
            query.AppendLine(@"); ");
            
            cloneObj.{{pk:col:pname}} = KeyGenerator.GenNewGuidKey();{{if:col:exist|CreateTime|}}
            cloneObj.CreateTime = DateTime.Now;{{/if:col:exist}}{{if:col:exist|UpdateTime|}}
            cloneObj.UpdateTime = DateTime.Now;{{/if:col:exist}}{{if:col:exist|RVersion|}}
            cloneObj.RVersion = 1;{{/if:col:exist}}

            MySqlParameter[] paramCollection = new MySqlParameter[{{col:lcount:3}}];{{loop:col}}
            paramCollection[{{col:lindex}}] = new MySqlParameter("@{{col:pname}}", {{col:daltype}}{{if:col:string}}, {{col:maxlen}}{{/if:col:string}});{{/loop:col}}
            {{loop:col}}
            paramCollection[{{col:lindex}}].Value = cloneObj.{{col:pname}};{{/loop:col}}
            
            try
            {
                int effectCount = 0;

                if (tran != null)
                {
                    effectCount = MySqlHelper.ExecuteNonQuery((MySqlConnection)tran.Connection, query.ToString(), paramCollection);
                }
                else
                {
                    effectCount = MySqlHelper.ExecuteNonQuery(this.CurrentConnectionString, query.ToString(), paramCollection);
                }

                if (effectCount == 0)
                {
                    throw new ResponseException((int)ResultCode.NoDataInsert, {{table:cname}}Resource.ErrorNoAdd);
                }

                {{table:lfcname}}.{{pk:col:pname}} = cloneObj.{{pk:col:pname}};{{if:col:exist|CreateTime|}}
                {{table:lfcname}}.CreateTime = cloneObj.CreateTime;{{/if:col:exist}}{{if:col:exist|UpdateTime|}}
                {{table:lfcname}}.UpdateTime = cloneObj.UpdateTime;{{/if:col:exist}}{{if:col:exist|RVersion|}}
                {{table:lfcname}}.RVersion = cloneObj.RVersion;{{/if:col:exist}}
            }
            catch (MySqlException sex)
            {
                switch (sex.Number)
                {
                    case 1452:
                        throw new ResponseException((int)ResultCode.FKError, {{table:cname}}Resource.ErrorFK, sex);
                    case 1062:
                        throw new ResponseException((int)ResultCode.UQError, {{table:cname}}Resource.ErrorUQ, sex);
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
        /// <param name="{{table:lfcname}}List">{{table:comment}}列表</param>
        public void Add(IList<{{table:cname}}> {{table:lfcname}}List)
        {
            ICTransaction tran = DalManager.DalFactory.BeginTransaction();

            try
            {
                this.Add({{table:lfcname}}List, tran);
                tran.Commit();
            }
            catch (ResponseException rex)
            {
                tran.Rollback();
                throw new ResponseException(rex.ResultCode, rex.Message, rex);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 批量添加{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}List">{{table:comment}}</param>
        /// <param name="tran">中间数据库事务对象</param>
        public void Add(IList<{{table:cname}}> {{table:lfcname}}List, ICTransaction tran)
        {
            if ({{table:lfcname}}List == null)
            {
                return;
            }

            IList<{{table:cname}}> cloneList = ObjectFactory.Clone<IList<{{table:cname}}>>({{table:lfcname}}List);

            foreach ({{table:cname}} {{table:lfcname}} in cloneList)
            {
                this.Add({{table:lfcname}}, tran);
            }

            for (int i = 0; i < {{table:lfcname}}List.Count; i++)
            {
                {{table:lfcname}}List[i].{{pk:col:pname}}  = cloneList[i].{{pk:col:pname}};{{if:col:exist|CreateTime|}}
                {{table:lfcname}}List[i].CreateTime = cloneList[i].CreateTime;{{/if:col:exist}}{{if:col:exist|UpdateTime|}}
                {{table:lfcname}}List[i].UpdateTime = cloneList[i].UpdateTime;{{/if:col:exist}}{{if:col:exist|RVersion|}}
                {{table:lfcname}}List[i].RVersion = cloneList[i].RVersion;{{/if:col:exist}}
            }
        }

        /// <summary>
        /// 查找指定条件的{{table:comment}}的数量
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <returns>返回查询到的数量</returns>
        public long Count({{table:cname}}Searcher {{table:lfcname}}Searcher)
        {
            return this.Count({{table:lfcname}}Searcher, null);
        }

        /// <summary>
        /// 查找指定条件的{{table:comment}}的数量
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回查询到的数量</returns>
        public long Count({{table:cname}}Searcher {{table:lfcname}}Searcher, ICTransaction tran)
        {
            object count = 0;
            long result = 0;
            {{table:cname}}Searcher querySearcher = null;
            MysqlQueryParser queryParser = new MysqlQueryParser();
            StringBuilder query = new StringBuilder();
            query.AppendLine(@"SELECT ");
            query.AppendLine(@"  COUNT(A.`{{pk:col:name}}`) ");
            query.AppendLine(@"FROM ");
            query.AppendLine(@"  `{{table:name}}` A ");

            if ({{table:lfcname}}Searcher != null)
            {
                querySearcher = ({{table:cname}}Searcher){{table:lfcname}}Searcher.Clone();
                querySearcher.TableName = "A";
                {{loop:fk}}
                if (querySearcher.Curr{{fk:table:cname}} != null)
                {
                    querySearcher.Curr{{fk:table:cname}}.TableName = "T{{fk:col:id}}";
                    query.AppendLine(@"LEFT JOIN ");
                    query.AppendLine(@"  `{{fk:table:name}}` T{{fk:col:id}} ON(A.`{{fk:col:name}}` = T{{fk:col:id}}.`{{fk:pk:col:name}}`) ");
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
                count = MySqlHelper.ExecuteScalar((MySqlConnection)tran.Connection, query.ToString(), queryParser.ParamCollection.ToArray());
            }
            else
            {
                count = MySqlHelper.ExecuteScalar(this.CurrentConnectionString, query.ToString(), queryParser.ParamCollection.ToArray());
            }

            return long.TryParse(count.ToString(), out result) ? result : 0;
        }

        /// <summary>
        /// 删除指定PK的{{table:comment}}
        /// </summary>
        /// <param name="{{pk:col:lfpname}}">{{table:comment}}的PK值</param>
        public void Delete({{pk:col:codetype}} {{pk:col:lfpname}})
        {
            this.Delete({{pk:col:lfpname}}, null);
        }

        /// <summary>
        /// 删除指定PK的{{table:comment}}
        /// </summary>
        /// <param name="{{pk:col:lfpname}}">{{table:comment}}的PK值</param>
        /// <param name="tran">中间数据库事务对象</param>
        public void Delete({{pk:col:codetype}} {{pk:col:lfpname}}, ICTransaction tran)
        {
            {{table:cname}}Searcher querySearcher = new {{table:cname}}Searcher();
            querySearcher.{{pk:col:pname}}.Equal({{pk:col:lfpname}});
            this.Delete(querySearcher, tran);
        }

        /// <summary>
        /// 删除指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        public void Delete({{table:cname}}Searcher {{table:lfcname}}Searcher)
        {
            this.Delete({{table:lfcname}}Searcher, null);
        }

        /// <summary>
        /// 删除指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        public void Delete({{table:cname}}Searcher {{table:lfcname}}Searcher, ICTransaction tran)
        {
            {{table:cname}}Searcher querySearcher = null;
            MysqlQueryParser queryParser = new MysqlQueryParser();
            StringBuilder query = new StringBuilder();
            query.AppendLine(@"DELETE FROM ");
            query.AppendLine(@"  `{{table:name}}` ");
            query.AppendLine(@"WHERE ");
            query.AppendLine(@"   `{{pk:col:name}}` IN (");
            query.AppendLine(@"      SELECT ");
            query.AppendLine(@"         * ");
            query.AppendLine(@"      FROM ");
            query.AppendLine(@"         (SELECT ");
            query.AppendLine(@"             A.`{{pk:col:name}}` ");
            query.AppendLine(@"          FROM ");
            query.AppendLine(@"             `{{table:name}}` A ");

            if ({{table:lfcname}}Searcher != null)
            {
                querySearcher = ({{table:cname}}Searcher){{table:lfcname}}Searcher.Clone();
                querySearcher.TableName = "A";
                {{loop:fk}}
                if (querySearcher.Curr{{fk:table:cname}} != null)
                {
                    querySearcher.Curr{{fk:table:cname}}.TableName = "T{{fk:col:id}}";
                    query.AppendLine(@"LEFT JOIN ");
                    query.AppendLine(@"  `{{fk:table:name}}` T{{fk:col:id}} ON(A.`{{fk:col:name}}` = T{{fk:col:id}}.`{{fk:pk:col:name}}`) ");
                }
{{/loop:fk}}
            }
            
            queryParser.SearcherParse(querySearcher);

            if (!string.IsNullOrEmpty(queryParser.ConditionString))
            {
                query.AppendLine(@"WHERE ");
                query.AppendLine(@"   " + queryParser.ConditionString);
            }
    
            query.AppendLine(@") AS A1); ");
            
            int effectCount = 0;

            try
            {
                if (tran != null)
                {
                    effectCount = MySqlHelper.ExecuteNonQuery((MySqlConnection)tran.Connection, query.ToString(), queryParser.ParamCollection.ToArray());
                }
                else
                {
                    effectCount = MySqlHelper.ExecuteNonQuery(this.CurrentConnectionString, query.ToString(), queryParser.ParamCollection.ToArray());
                }
            }
            catch (MySqlException sex)
            {
                switch (sex.Number)
                {
                    case 1452:
                        throw new ResponseException((int)ResultCode.FKError, {{table:cname}}Resource.ErrorFK, sex);
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
        /// <param name="{{pk:col:lfpname}}">{{table:comment}}的PK值</param>
        /// <returns>找到返回{{table:comment}}对象，未找到则返回null</returns>
        public {{table:cname}} FindSingle({{pk:col:codetype}} {{pk:col:lfpname}})
        {
            return this.FindSingle({{pk:col:lfpname}}, null);
        }

        /// <summary>
        /// 查找指定PK的{{table:comment}}
        /// </summary>
        /// <param name="{{pk:col:lfpname}}">{{table:comment}}的PK值</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>找到返回{{table:comment}}对象，未找到则返回null</returns>
        public {{table:cname}} FindSingle({{pk:col:codetype}} {{pk:col:lfpname}}, ICTransaction tran)
        {
            {{table:cname}}Searcher querySearcher = new {{table:cname}}Searcher();
            querySearcher.{{pk:col:pname}}.Equal({{pk:col:lfpname}});
            IList<{{table:cname}}> resultList = this.FindList(querySearcher, tran);
            return (resultList == null || resultList.Count == 0) ? null : resultList[0];
        }

        /// <summary>
        /// 查找指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <returns>找到返回{{table:comment}}对象列表，未找到则返回null</returns>
        public IList<{{table:cname}}> FindList({{table:cname}}Searcher {{table:lfcname}}Searcher)
        {
            PageList<{{table:cname}}> pageList = this.FindList({{table:lfcname}}Searcher, null, null);
            return (pageList == null || pageList.TotalCount == 0) ? null : pageList.RecordList;
        }

        /// <summary>
        /// 查找指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>找到返回{{table:comment}}对象列表，未找到则返回null</returns>
        public IList<{{table:cname}}> FindList({{table:cname}}Searcher {{table:lfcname}}Searcher, ICTransaction tran)
        {
            PageList<{{table:cname}}> pageList = this.FindList({{table:lfcname}}Searcher, null, tran);
            return (pageList == null || pageList.TotalCount == 0) ? null : pageList.RecordList;
        }

        /// <summary>
        /// 查找指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回分页结果对象，通过分页结果对象中的TotalCount判断是否有查找到的记录</returns>
        public PageList<{{table:cname}}> FindList({{table:cname}}Searcher {{table:lfcname}}Searcher, Pager pager)
        {
            return this.FindList({{table:lfcname}}Searcher, pager, null);
        }

        /// <summary>
        /// 查找指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回分页结果对象，通过分页结果对象中的TotalCount判断是否有查找到的记录</returns>
        public PageList<{{table:cname}}> FindList({{table:cname}}Searcher {{table:lfcname}}Searcher, Pager pager, ICTransaction tran)
        {
            PageList<{{table:cname}}> resultList = new PageList<{{table:cname}}>();
            PageDataTable pageDataTable = this.FindDataTable({{table:lfcname}}Searcher, pager, tran);
            {{table:cname}} ele = null;

            if (pageDataTable != null)
            {
                resultList.PageIndex = pageDataTable.PageIndex;
                resultList.TotalCount = pageDataTable.TotalCount;

                if (pageDataTable.RecordList != null && pageDataTable.RecordList.Rows.Count > 0)
                {
                    foreach (DataRow aRow in pageDataTable.RecordList.Rows)
                    {
                        ele = new {{table:cname}}();
                        {{loop:col}}
                        if (!(aRow["{{col:name}}"] is DBNull))
                        {
                            ele.{{col:pname}} = {{col:convert}}aRow["{{col:name}}"]{{/col:convert}};
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
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <returns>找到记录则返回DataTable，如果未找到则返回null</returns>
        public DataTable FindDataTable({{table:cname}}Searcher {{table:lfcname}}Searcher)
        {
            PageDataTable pageDataTable = this.FindDataTable({{table:lfcname}}Searcher, null, null);
            return (pageDataTable == null || pageDataTable.TotalCount == 0) ? null : pageDataTable.RecordList;
        }

        /// <summary>
        /// 查找指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>找到记录则返回DataTable，如果未找到则返回null</returns>
        public DataTable FindDataTable({{table:cname}}Searcher {{table:lfcname}}Searcher, ICTransaction tran)
        {
            PageDataTable pageDataTable = this.FindDataTable({{table:lfcname}}Searcher, null, tran);
            return (pageDataTable == null || pageDataTable.TotalCount == 0) ? null : pageDataTable.RecordList;
        }

        /// <summary>
        /// 查找指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回分页结果对象，通过分页结果对象中的TotalCount判断是否有查找到的记录</returns>
        public PageDataTable FindDataTable({{table:cname}}Searcher {{table:lfcname}}Searcher, Pager pager)
        {
            return this.FindDataTable({{table:lfcname}}Searcher, pager, null);
        }

        /// <summary>
        /// 查找指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回分页结果对象，通过分页结果对象中的TotalCount判断是否有查找到的记录</returns>
        public PageDataTable FindDataTable({{table:cname}}Searcher {{table:lfcname}}Searcher, Pager pager, ICTransaction tran)
        {
            {{table:cname}}Searcher querySearcher = null;
            MysqlQueryParser queryParser = new MysqlQueryParser();
            PageDataTable pDataTable = new PageDataTable();
            DataSet resultSet = null;
            StringBuilder joinQuery = new StringBuilder();
            StringBuilder conditionQuery = new StringBuilder();
            StringBuilder sortQuery = new StringBuilder();

            if ({{table:lfcname}}Searcher != null)
            {
                querySearcher = ({{table:cname}}Searcher){{table:lfcname}}Searcher.Clone();
                querySearcher.TableName = "A";
                {{loop:fk}}
                if (querySearcher.Curr{{fk:table:cname}} != null)
                {
                    querySearcher.Curr{{fk:table:cname}}.TableName = "T{{fk:col:id}}";
                    joinQuery.AppendLine(@"LEFT JOIN ");
                    joinQuery.AppendLine(@"  `{{fk:table:name}}` T{{fk:col:id}} ON(A.`{{fk:col:name}}` = T{{fk:col:id}}.`{{fk:pk:col:name}}`) ");
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
            query.AppendLine(@"   ,`{{col:name}}` ");{{/loop:col}}
            query.AppendLine(@"FROM ");
            query.AppendLine(@"   `{{table:name}}` A ");
            query.AppendLine(joinQuery.ToString());
            query.AppendLine(conditionQuery.ToString());
            query.AppendLine(sortQuery.ToString());
            
            if (pager != null && pager.PageSize != 0 && pager.StartRecord >= 0)
            {
                query.AppendLine(@" LIMIT " + pager.StartRecord.ToString() + "," + pager.PageSize + " ");
            }

            query.AppendLine(@"; ");
            
            if (tran != null)
            {
                resultSet = MySqlHelper.ExecuteDataset((MySqlConnection)tran.Connection, query.ToString(), queryParser.ParamCollection.ToArray());
            }
            else
            {
                resultSet = MySqlHelper.ExecuteDataset(this.CurrentConnectionString, query.ToString(), queryParser.ParamCollection.ToArray());
            }

            if (resultSet != null)
            {
                pDataTable.RecordList = resultSet.Tables[0];
                pDataTable.TotalCount = pDataTable.RecordList.Rows.Count;

                if (pager != null)
                {
                    pDataTable.PageIndex = pager.CurrentPage;
                    pDataTable.TotalCount = this.Count({{table:lfcname}}Searcher, tran);
                }
            }

            return pDataTable;
        }

        /// <summary>
        /// 更新{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}">{{table:comment}}</param>
        public void Update({{table:cname}} {{table:lfcname}})
        {
            this.Update({{table:lfcname}}, null);
        }

        /// <summary>
        /// 更新{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}">{{table:comment}}</param>
        /// <param name="tran">中间数据库事务对象</param>
        public void Update({{table:cname}} {{table:lfcname}}, ICTransaction tran)
        {
            {{table:cname}} cloneObj = ObjectFactory.Clone<{{table:cname}}>({{table:lfcname}});
            {{table:cname}} old{{table:cname}} = this.FindSingle(cloneObj.{{table:cname}}Id, tran);

            if (cloneObj.RVersion != old{{table:cname}}.RVersion)
            {
                throw new ResponseException((int)ResultCode.VersionChanged, {{table:cname}}Resource.ErrorVersionChanged);
            }
            
            int updateColCount = 0;
            StringBuilder query = new StringBuilder();
            query.AppendLine(@"UPDATE ");
            query.AppendLine(@"   `{{table:name}}` ");
            query.AppendLine(@"SET ");
            query.AppendLine(@"   `{{pk:col:name}}` = `{{pk:col:name}}` ");
            {{loop:col|ignparam:pk,CreaterId,CreateTime,UpdateTime,RVersion|}}{{if:col:string}}
            if ((!string.IsNullOrEmpty(cloneObj.{{col:pname}}) && !cloneObj.{{col:pname}}.Equals(old{{table:cname}}.{{col:pname}}))
                || (!string.IsNullOrEmpty(old{{table:cname}}.{{col:pname}}) && !old{{table:cname}}.{{col:pname}}.Equals(cloneObj.{{col:pname}})))
            {
                updateColCount++;
                query.AppendLine(@"  ,`{{col:name}}` = @{{col:pname}} ");
            }
            {{/if:col:string}}{{if:col:number}}
            if (cloneObj.{{col:pname}} != old{{table:cname}}.{{col:pname}})
            {
                updateColCount++;
                query.AppendLine(@"  ,`{{col:name}}` = @{{col:pname}} ");
            }
            {{/if:col:number}}{{if:col:int}}
            if (cloneObj.{{col:pname}} != old{{table:cname}}.{{col:pname}})
            {
                updateColCount++;
                query.AppendLine(@"  ,`{{col:name}}` = @{{col:pname}} ");
            }
            {{/if:col:int}}{{/loop:col}}{{if:col:exist|UpdateTime|}}
            query.AppendLine(@"  ,`UpdateTime` = @UpdateTime ");{{/if:col:exist}}{{if:col:exist|RVersion|}}
            query.AppendLine(@"  ,`RVersion` = @RVersion ");{{/if:col:exist}}
            query.AppendLine(@"WHERE ");
            query.AppendLine(@"   `{{pk:col:name}}` = @{{pk:col:pname}} ");

            if (updateColCount == 0)
            {
                return;
            }
            
            {{if:col:exist|UpdateTime|}}
            cloneObj.UpdateTime = DateTime.Now;{{/if:col:exist}}{{if:col:exist|RVersion|}}
            cloneObj.RVersion++;{{/if:col:exist}}
            MySqlParameter[] paramCollection = new MySqlParameter[{{col:lcount:8}}];{{loop:col|ignparam:CreaterId,CreateTime|}}
            paramCollection[{{col:lindex}}] = new MySqlParameter("@{{col:pname}}", {{col:daltype}}{{if:col:string}}, {{col:maxlen}}{{/if:col:string}});{{/loop:col}}
            
            {{loop:col|ignparam:CreaterId,CreateTime|}}
            paramCollection[{{col:lindex}}].Value = cloneObj.{{col:pname}};{{/loop:col}}
            
            try
            {
                int effectCount = 0;

                if (tran != null)
                {
                    effectCount = MySqlHelper.ExecuteNonQuery((MySqlConnection)tran.Connection, query.ToString(), paramCollection);
                }
                else
                {
                    effectCount = MySqlHelper.ExecuteNonQuery(this.CurrentConnectionString, query.ToString(), paramCollection);
                }

                // 抛出一个异常
                if (effectCount == 0)
                {
                    throw new ResponseException((int)ResultCode.NoDataUpdate, {{table:cname}}Resource.ErrorNoUpdate);
                }
                {{if:col:exist|UpdateTime|}}
                {{table:lfcname}}.UpdateTime = cloneObj.UpdateTime;{{/if:col:exist}}{{if:col:exist|RVersion|}}
                {{table:lfcname}}.RVersion = cloneObj.RVersion;{{/if:col:exist}}
            }
            catch (MySqlException sex)
            {
                switch (sex.Number)
                {
                    case 1452:
                        throw new ResponseException((int)ResultCode.FKError, {{table:cname}}Resource.ErrorFK, sex);
                    case 1062:
                        throw new ResponseException((int)ResultCode.UQError, {{table:cname}}Resource.ErrorUQ, sex);
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
        /// <param name="{{table:lfcname}}">{{table:comment}}</param>
        public void Update(IList<{{table:cname}}> {{table:lfcname}}List)
        {
            ICTransaction tran = DalManager.DalFactory.BeginTransaction();

            try
            {
                this.Update({{table:lfcname}}List, tran);
                tran.Commit();
            }
            catch (ResponseException rex)
            {
                tran.Rollback();
                throw new ResponseException(rex.ResultCode, rex.Message, rex);
            }
            catch(Exception ex)
            {
                tran.Rollback();
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 批量更新{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}">{{table:comment}}</param>
        /// <param name="tran">中间数据库事务对象</param>
        public void Update(IList<{{table:cname}}> {{table:lfcname}}List, ICTransaction tran)
        {
            if ({{table:lfcname}}List == null)
            {
                return;
            }

            IList<{{table:cname}}> cloneList = ObjectFactory.Clone<IList<{{table:cname}}>>({{table:lfcname}}List);

            foreach ({{table:cname}} {{table:lfcname}} in cloneList)
            {
                this.Update({{table:lfcname}}, tran);
            }
            {{if:section}}
            for (int i = 0; i < {{table:lfcname}}List.Count; i++)
            {
                {{if:col:exist|UpdateTime|}}{{table:lfcname}}List[i].UpdateTime = cloneList[i].UpdateTime;{{/if:col:exist}}{{if:col:exist|RVersion|}}
                {{table:lfcname}}List[i].RVersion = cloneList[i].RVersion;{{/if:col:exist}}
            }{{/if:section}}
        }

        /// <summary>
        /// 获取查找{{table:comment}} PK的SQL语句，用于其他SQL的子查询或拼接使用
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="paramCollection">返回当前查询对象中所有的条件对象集合</param>
        /// <return>返回拼接后的SQL语句</return>
        public string GetPKSQLCommand({{table:cname}}Searcher {{table:lfcname}}Searcher, out DBParamCollection<DbParameter> paramCollection)
        {
            {{table:cname}}Searcher querySearcher = null;
            MysqlQueryParser queryParser = new MysqlQueryParser();
            StringBuilder joinQuery = new StringBuilder();
            StringBuilder conditionQuery = new StringBuilder();

            if ({{table:lfcname}}Searcher != null)
            {
                querySearcher = ({{table:cname}}Searcher){{table:lfcname}}Searcher.Clone();
                querySearcher.TableName = "A";
                {{loop:fk}}
                if (querySearcher.Curr{{fk:table:cname}} != null)
                {
                    querySearcher.Curr{{fk:table:cname}}.TableName = "T{{fk:col:id}}";
                    joinQuery.AppendLine(@"LEFT JOIN ");
                    joinQuery.AppendLine(@"  `{{fk:table:name}}` T{{fk:col:id}} ON(A.`{{fk:col:name}}` = T{{fk:col:id}}.`{{fk:pk:col:name}}`) ");
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
            query.AppendLine(@"SELECT ");
            query.AppendLine(@"   `{{pk:col:name}}` ");
            query.AppendLine(@"FROM ");
            query.AppendLine(@"   `{{table:name}}` A ");
            query.AppendLine(joinQuery.ToString());
            query.AppendLine(conditionQuery.ToString());
            paramCollection = new DBParamCollection<DbParameter>();
            DbParameter cloneParam = null;

            foreach (MySqlParameter mParam in queryParser.ParamCollection)
            {
                cloneParam = ObjectFactory.Clone<DbParameter>(mParam);
                paramCollection.Add(cloneParam);
            }
            
            return query.ToString();
        } 
        
        #region Public Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dalFactory">传入当前指定的DalFactory对象</param>
        public {{table:cname}}Dal(MysqlDalFactoryBase dalFactory)
            : base(dalFactory)
        {

        }

        #endregion
    }
}