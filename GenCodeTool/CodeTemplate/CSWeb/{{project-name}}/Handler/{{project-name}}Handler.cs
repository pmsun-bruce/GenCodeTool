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
        private static I{{table:name}}Dal {{table:lfname}}Dal;
        /// <summary>
        /// {{table:comment}}表的数据操作类
        /// </summary>
        private static I{{table:name}}Dal {{table:name}}Dal
        {
            get
            {
                if ({{table:lfname}}Dal == null)
                {
                    {{table:lfname}}Dal = DalManager.DalFactory.Create{{table:name}}Dal();
                }

                return {{table:lfname}}Dal;
            }
        }
		{{/loop:table}}
        #endregion

        #region Public
		
        #endregion

        #endregion
		{{loop:table}}
        #region {{table:name}} Methods

        #region Validate Data{{if:section}}
		{{if:loop:col|ignparam:pk,fk,rk,CreateTime,UpdateTime,CreaterId,UpdatorId|}}{{if:col:unique}}
        /// <summary>
        /// 检查是否有重复的{{col:comment}}
        /// </summary>
        /// <param name="{{col:lfname}}">{{col:comment}}</param>
        /// <returns>如存在返回False，不存在返回True</returns>
        public static bool CheckSame{{table:name}}{{col:name}}({{col:codetype}} {{col:lfname}})
        {
            return CheckSame{{table:name}}{{col:name}}({{col:lfname}}, null, null);
        }

        /// <summary>
        /// 检查是否有重复的{{col:comment}}
        /// </summary>
        /// <param name="{{col:lfname}}">{{col:comment}}</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>如存在返回False，不存在返回True</returns>
        public static bool CheckSame{{table:name}}{{col:name}}({{col:codetype}} {{col:lfname}}, ICTransaction tran)
        {
            return CheckSame{{table:name}}{{col:name}}({{col:lfname}}, null, tran);
        }

        /// <summary>
        /// 检查是否有重复的{{col:comment}}，排除指定键值的记录
        /// 修改时使用
        /// </summary>
        /// <param name="{{col:lfname}}">{{col:comment}}</param>
        /// <param name="{{pk:col:lfname}}">需要排除的键值</param>
        /// <returns>如存在返回False，不存在返回True</returns>
        public static bool CheckSame{{table:name}}{{col:name}}({{col:codetype}} {{col:lfname}}, {{pk:col:codetype}} {{pk:col:lfname}})
        {
            return CheckSame{{table:name}}{{col:name}}({{table:lfname}}{{col:name}}, {{pk:col:lfname}}, null);
        }

        /// <summary>
        /// 检查是否有重复的{{col:comment}}，排除指定键值的记录
        /// 修改时使用
        /// </summary>
        /// <param name="{{col:lfname}}">{{col:comment}}</param>
        /// <param name="{{pk:col:lfname}}">需要排除的键值</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>如存在返回False，不存在返回True</returns>
        public static bool CheckSame{{table:name}}{{col:name}}({{col:codetype}} {{col:lfname}}, {{pk:col:codetype}} {{pk:col:lfname}}, ICTransaction tran)
        {
            if (string.IsNullOrWhiteSpace({{col:lfname}}))
            {
                return false;
            }

            {{table:name}}Searcher {{table:lfname}}Searcher = new {{table:name}}Searcher();
            {{table:lfname}}Searcher.{{col:name}}.AddCondition(ConditionFactory.Equal({{col:lfname}}));

            if (!string.IsNullOrWhiteSpace({{pk:col:lfname}}))
            {
                {{table:lfname}}Searcher.{{pk:col:name}}.AddCondition(ConditionFactory.NotEqual({{pk:col:lfname}}));
            }

            long count = Count{{table:name}}({{table:lfname}}Searcher, tran);

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
        /// <param name="{{table:lfname}}">{{table:comment}}</param>
        public static void Validate{{table:name}}Data({{table:name}} {{table:lfname}})
        {
            Validate{{table:name}}Data({{table:lfname}}, null);
        }

        /// <summary>
        /// {{table:comment}}数据验证
        /// </summary>
        /// <param name="{{table:lfname}}">{{table:comment}}</param>
        /// <param name="tran">中间数据库事务对象</param>
        public static void Validate{{table:name}}Data({{table:name}} {{table:lfname}}, ICTransaction tran)
        {
            Validator validator = new Validator();{{if:section}}
            {{if:loop:col|ignparam:pk,fk,rk,CreateTime,UpdateTime,CreaterId,UpdatorId|}}
            // {{col:comment}} Check{{if:col:required}}
			validator.RuleList.Add(ValidateRuleFactory.Required({{table:lfname}}.{{col:name}}, string.Format(ValidateMessageResource.Required, {{table:name}}Resource.{{col:name}})));
			{{/if:col:required}}{{if:col:max}}
			validator.RuleList.Add(ValidateRuleFactory.Max({{table:lfname}}.{{col:name}}, {{col:max}}, string.Format(ValidateMessageResource.Max, {{table:name}}Resource.{{col:name}}, {{col:max}})));
			{{/if:col:max}}{{if:col:maxlen}}
			validator.RuleList.Add(ValidateRuleFactory.MaxLen({{table:lfname}}.{{col:name}}, {{col:maxlen}}, string.Format(ValidateMessageResource.MaxLen, {{table:name}}Resource.{{col:name}}, {{col:maxlen}})));{{/if:col:maxlen}}{{if:col:min}}
			validator.RuleList.Add(ValidateRuleFactory.Max({{table:lfname}}.{{col:name}}, {{col:min}}, string.Format(ValidateMessageResource.Min, {{table:name}}Resource.{{col:name}}, {{col:min}})));{{/if:col:min}}
			{{if:col:unique}}
			if(string.IsNullOrWhiteSpace({{table:lfname}}.{{pk:col:name}}))
            {
                validator.RuleList.Add(ValidateRuleFactory.Customized(CheckSame{{table:name}}{{col:name}}({{table:lfname}}.{{col:name}}, tran), string.Format(ValidateMessageResource.SameValue, {{table:name}}Resource.{{col:name}})));
            }
            else
            {
                validator.RuleList.Add(ValidateRuleFactory.Customized(CheckSame{{table:name}}{{col:name}}({{table:lfname}}.{{col:name}}, {{table:lfname}}.{{pk:col:name}}, tran), string.Format(ValidateMessageResource.SameValue, {{table:name}}Resource.{{col:name}})));
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
        /// <param name="{{table:lfname}}">{{table:comment}}对象</param>
        public static void Add{{table:name}}({{table:name}} {{table:lfname}})
        {
            ICTransaction tran = DalManager.DalFactory.BeginTransaction();

            try
            {
                Add{{table:name}}({{table:lfname}}, tran);
                tran.Commit();
            }
            catch(ResponseException rex)
            {
                tran.RollBack();
                throw new ResponseException(rex.Message, rex); 
            }
            catch(Exception ex)
            {
                tran.RollBack();
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 添加{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfname}}">{{table:comment}}对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        public static void Add{{table:name}}({{table:name}} {{table:lfname}}, ICTransaction tran)
        {
            lock (lockKey)
            {
                Validate{{table:name}}Data({{table:lfname}}, tran);
                {{table:name}}Dal.Add({{table:lfname}}, tran);
            }
        }

        /// <summary>
        /// 更新{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfname}}">{{table:comment}}对象</param>
        public static void Update{{table:name}}({{table:name}} {{table:lfname}})
        {
            ICTransaction tran = DalManager.DalFactory.BeginTransaction();

            try
            {
                Update{{table:name}}({{table:lfname}}, tran);
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
        /// 更新{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfname}}">{{table:comment}}对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        public static void Update{{table:name}}({{table:name}} {{table:lfname}}, ICTransaction tran)
        {
            lock (lockKey)
            {
                Validate{{table:name}}Data({{table:lfname}}, tran);
                {{table:name}}Dal.Update({{table:lfname}}, tran);
            }
        }

        /// <summary>
        /// 删除{{table:comment}}
        /// </summary>
        /// <param name="{{pk:col:lfname}}">{{pk:col:comment}}</param>
        public static void Delete{{table:name}}ByPK({{pk:col:codetype}} {{pk:col:lfname}})
        {
            ICTransaction tran = DalManager.DalFactory.BeginTransaction();

            try
            {
                Delete{{table:name}}ByPK({{pk:col:lfname}}, tran);
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
        /// 删除{{table:comment}}
        /// </summary>
        /// <param name="{{pk:col:lfname}}">{{pk:col:comment}}</param>
        /// <param name="tran">中间数据库事务对象</param>
        public static void Delete{{table:name}}ByPK({{pk:col:codetype}} {{pk:col:lfname}}, ICTransaction tran)
        {
            lock (lockKey)
            {
                {{table:name}}Dal.Delete({{pk:col:lfname}}, tran);
            }
        }

        /// <summary>
        /// 删除{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">{{table:comment}}查询对象</param>
        public static void Delete{{table:name}}BySearcher({{table:name}}Searcher {{table:lfname}}Searcher)
        {
            ICTransaction tran = DalManager.DalFactory.BeginTransaction();

            try
            {
                Delete{{table:name}}BySearcher({{table:lfname}}Searcher, tran);
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
        /// 删除{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        public static void Delete{{table:name}}BySearcher({{table:name}}Searcher {{table:lfname}}Searcher, ICTransaction tran)
        {
            lock (lockKey)
            {
                {{table:name}}Dal.Delete({{table:lfname}}Searcher, tran);
            }
        }

        /// <summary>
        /// 根据PK获得{{table:comment}}
        /// </summary>
        /// <param name="{{pk:col:lfname}}">{{table:comment}}PK</param>
        /// <returns>返回{{table:comment}}对象，如未找到返回null</returns>
        public static {{table:name}} Get{{table:name}}ByPK({{pk:col:codetype}} {{pk:col:lfname}})
        {
            return Get{{table:name}}ByPK({{pk:col:lfname}}, null);
        }

        /// <summary>
        /// 根据PK获得{{table:comment}}
        /// </summary>
        /// <param name="{{pk:col:lfname}}">{{table:comment}}PK</param>
        /// <param name="tran">数据库事务对象</param>
        /// <returns>返回资源类型对象，如未找到返回null</returns>
        public static {{table:name}} Get{{table:name}}ById({{pk:col:codetype}} {{pk:col:lfname}}, ICTransaction tran)
        {
            return {{table:name}}Dal.FindSingle({{pk:col:lfname}}, tran);
        }

        /// <summary>
        /// 获取所有{{table:comment}}的列表
        /// </summary>
        /// <returns>返回{{table:comment}}列表，如未找到则返回null</returns>
        public static IList<{{table:name}}> Get{{table:name}}List()
        {
            return Get{{table:name}}List((ICTransaction)null);
        }

        /// <summary>
        /// 获取所有{{table:comment}}的列表
        /// </summary>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回{{table:comment}}列表，如未找到则返回null</returns>
        public static IList<{{table:name}}> Get{{table:name}}List(ICTransaction tran)
        {
            {{table:name}}Searcher {{table:lfname}}Searcher = new {{table:name}}Searcher();
            //{{table:lfname}}Searcher.DisplayName.SortOrder = SortOrder.Asc;
            return Get{{table:name}}List({{table:lfname}}Searcher, tran);
        }

        /// <summary>
        /// 根据指定条件查找{{table:comment}}列表
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">{{table:comment}}查询对象</param>
        /// <returns>返回{{table:comment}}列表，如未找到则返回null</returns>
        public static IList<{{table:name}}> Get{{table:name}}List({{table:name}}Searcher {{table:lfname}}Searcher)
        {
            return Get{{table:name}}List({{table:lfname}}Searcher, (ICTransaction)null);
        }

        /// <summary>
        /// 根据指定条件查找{{table:comment}}列表
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回{{table:comment}}列表，如未找到则返回null</returns>
        public static IList<{{table:name}}> Get{{table:name}}List({{table:name}}Searcher {{table:lfname}}Searcher, ICTransaction tran)
        {
            return {{table:name}}Dal.FindList({{table:lfname}}Searcher, tran);
        }

        /// <summary>
        /// 根据指定条件查找{{table:comment}}列表
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回分页结果对象</returns>
        public static PageList<{{table:name}}> Get{{table:name}}List({{table:name}}Searcher {{table:lfname}}Searcher, Pager pager)
        {
            return Get{{table:name}}List({{table:lfname}}Searcher, pager, null);
        }

        /// <summary>
        /// 根据指定条件查找{{table:comment}}列表
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回分页结果对象</returns>
        public static PageList<{{table:name}}> Get{{table:name}}List({{table:name}}Searcher {{table:name}}({{table:lfname}}Searcher, Pager pager, ICTransaction tran)
        {
            return {{table:name}}Dal.FindList({{table:lfname}}Searcher, pager, tran);
        }

        /// <summary>
        /// 获取所有{{table:comment}}数量
        /// </summary>
        /// <returns>返回{{table:comment}}数量</returns>
        public static long Count{{table:name}}()
        {
            return Count{{table:name}}((ICTransaction)null);
        }

        /// <summary>
        /// 获取所有{{table:comment}}数量
        /// </summary>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回{{table:comment}}数量</returns>
        public static long Count{{table:name}}(ICTransaction tran)
        {
            {{table:name}}Searcher {{table:name}}({{table:lfname}}Searcher = new {{table:name}}Searcher();
            return Count{{table:name}}({{table:lfname}}Searcher, tran);
        }

        /// <summary>
        /// 获取指定条件的{{table:comment}}数量
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">{{table:comment}}查询对象</param>
        /// <returns>返回{{table:comment}}数量</returns>
        public static long Count{{table:name}}({{table:name}}Searcher {{table:lfname}}Searcher)
        {
            return Count{{table:name}}({{table:lfname}}Searcher, null);
        }

        /// <summary>
        /// 获取指定条件的{{table:comment}}数量
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回{{table:comment}}数量</returns>
        public static long Count{{table:name}}({{table:name}}Searcher {{table:lfname}}Searcher, ICTransaction tran)
        {
            return {{table:name}}Dal.Count({{table:lfname}}Searcher, tran);
        }
		
        #endregion{{/loop:table}}

        #endregion
    }
}