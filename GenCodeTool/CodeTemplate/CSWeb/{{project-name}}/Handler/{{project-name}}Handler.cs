namespace {{project:namespace}}.Handler
{
    #region Reference

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;

    using NFramework.DBTool.Common;
    using NFramework.DBTool.QueryTool;
    using NFramework.ExceptionTool;
    using NFramework.LogTool.SysLog;
    using NFramework.ObjectTool;
    using NFramework.ProjectKey;
    using NFramework.SecurityTool;
    using NFramework.SecurityTool.Web;
    using NFramework.ValidationTool;
    using NFramework.ValidationTool.Globalization;

    using {{project:namespace}}.Entity;
    using {{project:namespace}}.Globalization;
    using {{project:namespace}}.IDal;
    using {{project:namespace}}.Searcher;

    #endregion

    /// <summary>
    /// {{project:displayname}}业务操作类
    /// </summary>
    public class {{project:name}}Handler
    {
        #region Fields & Properties

        #region Private

        /// <summary>
        /// 锁对象
        /// </summary>
        private static object lockKey = new object();

        /// <summary>
        /// 系统日志记录器
        /// </summary>
        private static ILogWriter logWriter;
        /// <summary>
        /// 系统日志记录器
        /// </summary>
        private static ILogWriter LogWriter
        {
            get
            {
                if (logWriter == null)
                {
                    logWriter = SysLogManager.GetLogWriter("{{project:name}}Handler");
                }

                return logWriter;
            }
        }
		
		{{loop:table}}
        /// <summary>
        /// {{table:comment}}表的数据操作类
        /// </summary>
        private static I{{table:cname}}Dal {{table:lfcname}}Dal;
        /// <summary>
        /// {{table:comment}}表的数据操作类
        /// </summary>
        private static I{{table:cname}}Dal {{table:cname}}Dal
        {
            get
            {
                if ({{table:lfcname}}Dal == null)
                {
                    {{table:lfcname}}Dal = DalManager.DalFactory.Create{{table:cname}}Dal();
                }

                return {{table:lfcname}}Dal;
            }
        }
		{{/loop:table}}
        #endregion

        #region Public
		
        #endregion

        #endregion
		{{loop:table}}
        #region {{table:cname}} Methods

        #region Validate Data{{if:section}}
		{{if:loop:col|ignparam:pk,fk,rk,CreateTime,UpdateTime,CreaterId,UpdatorId,RVersion,Status|}}{{if:col:unique}}
        /// <summary>
        /// 检查是否有重复的{{col:comment}}
        /// </summary>
        /// <param name="{{col:lfpname}}">{{col:comment}}</param>
        /// <returns>如存在返回False，不存在返回True</returns>
        public static bool CheckSame{{table:cname}}{{col:pname}}({{col:codetype}} {{col:lfpname}})
        {
            return CheckSame{{table:cname}}{{col:pname}}({{col:lfpname}}, null, null);
        }

        /// <summary>
        /// 检查是否有重复的{{col:comment}}
        /// </summary>
        /// <param name="{{col:lfpname}}">{{col:comment}}</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>如存在返回False，不存在返回True</returns>
        public static bool CheckSame{{table:cname}}{{col:pname}}({{col:codetype}} {{col:lfpname}}, ICTransaction tran)
        {
            return CheckSame{{table:cname}}{{col:pname}}({{col:lfpname}}, null, tran);
        }

        /// <summary>
        /// 检查是否有重复的{{col:comment}}，排除指定键值的记录
        /// 修改时使用
        /// </summary>
        /// <param name="{{col:lfpname}}">{{col:comment}}</param>
        /// <param name="{{pk:col:lfpname}}">需要排除的键值</param>
        /// <returns>如存在返回False，不存在返回True</returns>
        public static bool CheckSame{{table:cname}}{{col:pname}}({{col:codetype}} {{col:lfpname}}, {{pk:col:codetype}} {{pk:col:lfpname}})
        {
            return CheckSame{{table:cname}}{{col:pname}}({{col:lfpname}}, {{pk:col:lfpname}}, null);
        }

        /// <summary>
        /// 检查是否有重复的{{col:comment}}，排除指定键值的记录
        /// 修改时使用
        /// </summary>
        /// <param name="{{col:lfpname}}">{{col:comment}}</param>
        /// <param name="{{pk:col:lfpname}}">需要排除的键值</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>如存在返回False，不存在返回True</returns>
        public static bool CheckSame{{table:cname}}{{col:pname}}({{col:codetype}} {{col:lfpname}}, {{pk:col:codetype}} {{pk:col:lfpname}}, ICTransaction tran)
        {
            if (string.IsNullOrWhiteSpace({{col:lfpname}}))
            {
                return false;
            }

            {{table:cname}}Searcher {{table:lfcname}}Searcher = new {{table:cname}}Searcher();
            {{table:lfcname}}Searcher.{{col:pname}}.Equal({{col:lfpname}});

            if (!string.IsNullOrWhiteSpace({{pk:col:lfpname}}))
            {
                {{table:lfcname}}Searcher.{{pk:col:pname}}.NotEqual({{pk:col:lfpname}});
            }

            long count = Count{{table:cname}}({{table:lfcname}}Searcher, tran);

            if (count > 0)
            {
                return false;
            }

            return true;
        }
		{{/if:col:unique}}{{/if:loop:col}}{{/if:section}}
        /// <summary>
        /// {{table:comment}}数据验证
        /// </summary>
        /// <param name="{{table:lfcname}}">{{table:comment}}</param>
        public static void Validate{{table:cname}}Data({{table:cname}} {{table:lfcname}})
        {
            Validate{{table:cname}}Data({{table:lfcname}}, null);
        }

        /// <summary>
        /// {{table:comment}}数据验证
        /// </summary>
        /// <param name="{{table:lfcname}}">{{table:comment}}</param>
        /// <param name="tran">中间数据库事务对象</param>
        public static void Validate{{table:cname}}Data({{table:cname}} {{table:lfcname}}, ICTransaction tran)
        {
            Validator validator = new Validator();{{if:section}}
            {{if:loop:col|ignparam:pk,fk,rk,CreateTime,UpdateTime,CreaterId,UpdatorId|}}
            // {{col:comment}} Check{{if:col:required}}
			validator.RuleList.Add(ValidateRuleFactory.Required({{col:tostring}}{{table:lfcname}}.{{col:pname}}{{/col:tostring}}, string.Format(ValidateMessageResource.Required, {{table:cname}}Resource.{{col:pname}})));{{/if:col:required}}{{if:col:max}}
			validator.RuleList.Add(ValidateRuleFactory.Max({{col:tostring}}{{table:lfcname}}.{{col:pname}}{{/col:tostring}}, {{if:col:number}}(double){{/if:col:number}}{{col:max}}, string.Format(ValidateMessageResource.Max, {{table:cname}}Resource.{{col:pname}}, {{col:max}})));
			{{/if:col:max}}{{if:col:maxlen}}
			validator.RuleList.Add(ValidateRuleFactory.MaxLen({{table:lfcname}}.{{col:pname}}, {{col:maxlen}}, string.Format(ValidateMessageResource.MaxLen, {{table:cname}}Resource.{{col:pname}}, {{col:maxlen}})));{{/if:col:maxlen}}
			{{if:col:unique}}
			if(string.IsNullOrWhiteSpace({{table:lfcname}}.{{pk:col:pname}}))
            {
                validator.RuleList.Add(ValidateRuleFactory.Customized(CheckSame{{table:cname}}{{col:pname}}({{table:lfcname}}.{{col:pname}}, tran), string.Format(ValidateMessageResource.SameValue, {{table:cname}}Resource.{{col:pname}})));
            }
            else
            {
                validator.RuleList.Add(ValidateRuleFactory.Customized(CheckSame{{table:cname}}{{col:pname}}({{table:lfcname}}.{{col:pname}}, {{table:lfcname}}.{{pk:col:pname}}, tran), string.Format(ValidateMessageResource.SameValue, {{table:cname}}Resource.{{col:pname}})));
            }
            {{/if:col:unique}}{{/if:loop:col}}{{/if:section}}
            ValidResult vResult = validator.Valid();

            if(!vResult.IsPass)
            {
                throw new ResponseException((int)ResultCode.ValidError, vResult.ErrorMessage);
            }
        }

        #endregion

        /// <summary>
        /// 添加{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}">{{table:comment}}对象</param>
        public static void Add{{table:cname}}({{table:cname}} {{table:lfcname}})
        {
            ICTransaction tran = DalManager.DalFactory.BeginTransaction();

            try
            {
                Add{{table:cname}}({{table:lfcname}}, tran);
                tran.Commit();
            }
            catch(ResponseException rex)
            {
                tran.Rollback();
                throw new ResponseException(rex.Message, rex); 
            }
            catch(Exception ex)
            {
                tran.Rollback();
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 添加{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}">{{table:comment}}对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        public static void Add{{table:cname}}({{table:cname}} {{table:lfcname}}, ICTransaction tran)
        {
            lock (lockKey)
            {
                Validate{{table:cname}}Data({{table:lfcname}}, tran);
                {{table:cname}}Dal.Add({{table:lfcname}}, tran);
            }
        }

        /// <summary>
        /// 更新{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}">{{table:comment}}对象</param>
        public static void Update{{table:cname}}({{table:cname}} {{table:lfcname}})
        {
            ICTransaction tran = DalManager.DalFactory.BeginTransaction();

            try
            {
                Update{{table:cname}}({{table:lfcname}}, tran);
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
        /// 更新{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}">{{table:comment}}对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        public static void Update{{table:cname}}({{table:cname}} {{table:lfcname}}, ICTransaction tran)
        {
            lock (lockKey)
            {
                Validate{{table:cname}}Data({{table:lfcname}}, tran);
                {{table:cname}}Dal.Update({{table:lfcname}}, tran);
            }
        }

        /// <summary>
        /// 删除{{table:comment}}
        /// </summary>
        /// <param name="{{pk:col:lfpname}}">{{pk:col:comment}}</param>
        public static void Delete{{table:cname}}ByPK({{pk:col:codetype}} {{pk:col:lfpname}})
        {
            ICTransaction tran = DalManager.DalFactory.BeginTransaction();

            try
            {
                Delete{{table:cname}}ByPK({{pk:col:lfpname}}, tran);
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
        /// 删除{{table:comment}}
        /// </summary>
        /// <param name="{{pk:col:lfpname}}">{{pk:col:comment}}</param>
        /// <param name="tran">中间数据库事务对象</param>
        public static void Delete{{table:cname}}ByPK({{pk:col:codetype}} {{pk:col:lfpname}}, ICTransaction tran)
        {
            lock (lockKey)
            {
                {{table:cname}}Dal.Delete({{pk:col:lfpname}}, tran);
            }
        }

        /// <summary>
        /// 删除{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        public static void Delete{{table:cname}}BySearcher({{table:cname}}Searcher {{table:lfcname}}Searcher)
        {
            ICTransaction tran = DalManager.DalFactory.BeginTransaction();

            try
            {
                Delete{{table:cname}}BySearcher({{table:lfcname}}Searcher, tran);
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
        /// 删除{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        public static void Delete{{table:cname}}BySearcher({{table:cname}}Searcher {{table:lfcname}}Searcher, ICTransaction tran)
        {
            lock (lockKey)
            {
                {{table:cname}}Dal.Delete({{table:lfcname}}Searcher, tran);
            }
        }

        /// <summary>
        /// 根据PK获得{{table:comment}}
        /// </summary>
        /// <param name="{{pk:col:lfpname}}">{{table:comment}}PK</param>
        /// <returns>返回{{table:comment}}对象，如未找到返回null</returns>
        public static {{table:cname}} Get{{table:cname}}ByPK({{pk:col:codetype}} {{pk:col:lfpname}})
        {
            return Get{{table:cname}}ByPK({{pk:col:lfpname}}, null);
        }

        /// <summary>
        /// 根据PK获得{{table:comment}}
        /// </summary>
        /// <param name="{{pk:col:lfpname}}">{{table:comment}}PK</param>
        /// <param name="tran">数据库事务对象</param>
        /// <returns>返回资源类型对象，如未找到返回null</returns>
        public static {{table:cname}} Get{{table:cname}}ByPK({{pk:col:codetype}} {{pk:col:lfpname}}, ICTransaction tran)
        {
            return {{table:cname}}Dal.FindSingle({{pk:col:lfpname}}, tran);
        }

        /// <summary>
        /// 获取所有{{table:comment}}的列表
        /// </summary>
        /// <returns>返回{{table:comment}}列表，如未找到则返回null</returns>
        public static IList<{{table:cname}}> Get{{table:cname}}List()
        {
            return Get{{table:cname}}List((ICTransaction)null);
        }

        /// <summary>
        /// 获取所有{{table:comment}}的列表
        /// </summary>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回{{table:comment}}列表，如未找到则返回null</returns>
        public static IList<{{table:cname}}> Get{{table:cname}}List(ICTransaction tran)
        {
            {{table:cname}}Searcher {{table:lfcname}}Searcher = new {{table:cname}}Searcher();
            //{{table:lfcname}}Searcher.DisplayName.SortOrder = SortOrder.Asc;
            return Get{{table:cname}}List({{table:lfcname}}Searcher, tran);
        }

        /// <summary>
        /// 根据指定条件查找{{table:comment}}列表
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <returns>返回{{table:comment}}列表，如未找到则返回null</returns>
        public static IList<{{table:cname}}> Get{{table:cname}}List({{table:cname}}Searcher {{table:lfcname}}Searcher)
        {
            return Get{{table:cname}}List({{table:lfcname}}Searcher, (ICTransaction)null);
        }

        /// <summary>
        /// 根据指定条件查找{{table:comment}}列表
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回{{table:comment}}列表，如未找到则返回null</returns>
        public static IList<{{table:cname}}> Get{{table:cname}}List({{table:cname}}Searcher {{table:lfcname}}Searcher, ICTransaction tran)
        {
            return {{table:cname}}Dal.FindList({{table:lfcname}}Searcher, tran);
        }

        /// <summary>
        /// 根据指定条件查找{{table:comment}}列表
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回分页结果对象</returns>
        public static PageList<{{table:cname}}> Get{{table:cname}}List({{table:cname}}Searcher {{table:lfcname}}Searcher, Pager pager)
        {
            return Get{{table:cname}}List({{table:lfcname}}Searcher, pager, null);
        }

        /// <summary>
        /// 根据指定条件查找{{table:comment}}列表
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回分页结果对象</returns>
        public static PageList<{{table:cname}}> Get{{table:cname}}List({{table:cname}}Searcher {{table:lfcname}}Searcher, Pager pager, ICTransaction tran)
        {
            return {{table:cname}}Dal.FindList({{table:lfcname}}Searcher, pager, tran);
        }

        /// <summary>
        /// 获取所有{{table:comment}}数量
        /// </summary>
        /// <returns>返回{{table:comment}}数量</returns>
        public static long Count{{table:cname}}()
        {
            return Count{{table:cname}}((ICTransaction)null);
        }

        /// <summary>
        /// 获取所有{{table:comment}}数量
        /// </summary>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回{{table:comment}}数量</returns>
        public static long Count{{table:cname}}(ICTransaction tran)
        {
            {{table:cname}}Searcher {{table:lfcname}}Searcher = new {{table:cname}}Searcher();
            return Count{{table:cname}}({{table:lfcname}}Searcher, tran);
        }

        /// <summary>
        /// 获取指定条件的{{table:comment}}数量
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <returns>返回{{table:comment}}数量</returns>
        public static long Count{{table:cname}}({{table:cname}}Searcher {{table:lfcname}}Searcher)
        {
            return Count{{table:cname}}({{table:lfcname}}Searcher, null);
        }

        /// <summary>
        /// 获取指定条件的{{table:comment}}数量
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回{{table:comment}}数量</returns>
        public static long Count{{table:cname}}({{table:cname}}Searcher {{table:lfcname}}Searcher, ICTransaction tran)
        {
            return {{table:cname}}Dal.Count({{table:lfcname}}Searcher, tran);
        }
		
        #endregion
        {{/loop:table}}
    }
}