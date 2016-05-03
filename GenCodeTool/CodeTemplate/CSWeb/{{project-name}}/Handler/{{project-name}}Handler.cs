namespace {{project:namespace}}.Handler
{
    #region Reference
    
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Resources;
    using System.Text;
    using System.Xml;
    
    using NFramework.DBTool.Common;
    using NFramework.DBTool.QueryTool;
    using NFramework.LogTool.SysLog;
    
    using {{project:namespace}}.Entity;
    using {{project:namespace}}.IDal;
    using {{project:namespace}}.Searcher;
    
    #endregion
    
    /// <summary>
    /// {{project:displayname}}业务操作类
    /// </summary>
    public class {{project:name}}Handler
    {
        #region Fields & Properties
        
        /// <summary>
        /// 锁对象，用于新建修改时锁定代码段
        /// </summary>
        private static object lockKey = new object();
        
        /// <summary>
        /// 系统日志记录器
        /// </summary>
        private static ILogWriter logWriter;
        /// <summary>
        /// 系统日志记录器
        /// </summary>
        protected static ILogWriter LogWriter
        {
            get
            {
                if (logWriter == null)
                {
                    logWriter = SysLogManager.GetLogWriter("{{project:name}}Handler Log");
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
        public static I{{table:name}}Dal {{table:name}}Dal
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
        {{loop:table}}
        #region {{table:comment}}业务逻辑
        
        #region Public Methods
        
        /// <summary>
        /// 添加{{table:comment}}数据
        /// </summary>
        /// <param name="{{table:lfname}}">需要保存的数据对象</param>
        public static void Add{{table:name}}({{table:name}} {{table:lfname}})
        {
            Add{{table:name}}({{table:lfname}}, null);
        }
        
        /// <summary>
        /// 添加{{table:comment}}数据
        /// </summary>
        /// <param name="{{table:lfname}}">需要保存的数据对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        public static void Add{{table:name}}({{table:name}} {{table:lfname}}, ICTransaction tran)
        {
            Save{{table:name}}({{table:lfname}}, 0, tran);
        }
        
        /// <summary>
        /// 修改{{table:comment}}数据
        /// </summary>
        /// <param name="{{table:lfname}}">需要保存的数据对象</param>
        public static void Update{{table:name}}({{table:name}} {{table:lfname}})
        {
            ICTransaction tran = DalManager.DalFactory.BeginTransaction();

            try
            {
                Update{{table:name}}({{table:lfname}}, tran);
                tran.Commit();
            }
            catch
            {
                tran.RollBack();
                throw;
            }
        }
        
        /// <summary>
        /// 修改{{table:comment}}数据
        /// </summary>
        /// <param name="{{table:lfname}}">需要保存的数据对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        public static void Update{{table:name}}({{table:name}} {{table:lfname}}, ICTransaction tran)
        {
            Save{{table:name}}({{table:lfname}}, 1, tran);
        }
        
        /// <summary>
        /// 删除{{table:comment}}数据
        /// </summary>
        /// <param name="id">需要删除的数据的ID</param>
        public static void Delete{{table:name}}(string id)
        {
            ICTransaction tran = DalManager.DalFactory.BeginTransaction();

            try
            {
                {{table:name}}Searcher {{table:lfname}}Searcher = new {{table:name}}Searcher();
                {{table:lfname}}Searcher.{{table:name}}Id.AddCondition(ConditionFactory.Equal(id));
                Delete{{table:name}}({{table:lfname}}Searcher);
                tran.Commit();
            }
            catch
            {
                tran.RollBack();
                throw;
            }
        }
        
        /// <summary>
        /// 删除{{table:comment}}数据
        /// </summary>
        /// <param name="id">需要删除的数据的ID</param>
        /// <param name="tran">中间数据库事务对象</param>
        public static void Delete{{table:name}}(string id, ICTransaction tran)
        {
            {{table:name}}Searcher {{table:lfname}}Searcher = new {{table:name}}Searcher();
            {{table:lfname}}Searcher.{{table:name}}Id.AddCondition(ConditionFactory.Equal(id));
            Delete{{table:name}}({{table:lfname}}Searcher, tran);
        }
        
        /// <summary>
        /// 删除{{table:comment}}数据
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        public static void Delete{{table:name}}({{table:name}}Searcher {{table:lfname}}Searcher)
        {
            ICTransaction tran = DalManager.DalFactory.BeginTransaction();

            try
            {
                Delete{{table:name}}({{table:lfname}}Searcher, tran);
                tran.Commit();
            }
            catch
            {
                tran.RollBack();
                throw;
            }
        }
        
        /// <summary>
        /// 删除{{table:comment}}数据
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        public static void Delete{{table:name}}({{table:name}}Searcher {{table:lfname}}Searcher, ICTransaction tran)
        {{{loop:rk}}
            {{rk:table:name}}Searcher {{rk:table:lfname}}Searcher = new {{rk:table:name}}Searcher();
            {{rk:table:lfname}}Searcher.Curr{{table:name}} = {{table:lfname}}Searcher;
            {{rk:table:name}}Dal.Delete({{rk:table:lfname}}Searcher, tran){{/loop:rk}};
            {{table:name}}Dal.Delete({{table:lfname}}Searcher, tran);
        }
        
        /// <summary>
        /// 获取单个{{table:comment}}数据对象
        /// </summary>
        /// <param name="id">需要获得的数据的ID</param>
        /// <returns>找到返回对象，未找到则返回null</returns>
        public static {{table:name}} Get{{table:name}}ById(string id)
        {
            return Get{{table:name}}ById(id, null);
        }
        
        /// <summary>
        /// 获取单个{{table:comment}}数据对象
        /// </summary>
        /// <param name="id">需要获得的数据的ID</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>找到返回对象，未找到则返回null</returns>
        public static {{table:name}} Get{{table:name}}ById(string id, ICTransaction tran)
        {
            return {{table:name}}Dal.FindSingle(id, tran);
        }
        
        /// <summary>
        /// 获取{{table:comment}}数据数量
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <returns>返回记录数量</returns>
        public static long Count{{table:name}}({{table:name}}Searcher {{table:lfname}}Searcher)
        {
            return Count{{table:name}}({{table:lfname}}Searcher, null);
        }
        
        /// <summary>
        /// 获取{{table:comment}}数据数量
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回记录数量</returns>
        public static long Count{{table:name}}({{table:name}}Searcher {{table:lfname}}Searcher, ICTransaction tran)
        {
            return {{table:name}}Dal.Count({{table:lfname}}Searcher, tran);
        }
        
        /// <summary>
        /// 获取所有{{table:comment}}数据对象列表
        /// </summary>
        /// <returns>找到则返回对象列表，未找到则返回null</returns>
        public static IList<{{table:name}}> Get{{table:name}}List()
        {
            {{table:name}}Searcher {{table:lfname}}Searcher = new {{table:name}}Searcher();
            {{table:lfname}}Searcher.CreateTime.SortOrder = SortOrder.Asc;
            return Get{{table:name}}List({{table:lfname}}Searcher, (ICTransaction)null);
        }
        
        /// <summary>
        /// 获取所有{{table:comment}}数据对象列表
        /// </summary>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>找到则返回对象列表，未找到则返回null</returns>
        public static IList<{{table:name}}> Get{{table:name}}List(ICTransaction tran)
        {
            {{table:name}}Searcher {{table:lfname}}Searcher = new {{table:name}}Searcher();
            {{table:lfname}}Searcher.CreateTime.SortOrder = SortOrder.Asc;
            return Get{{table:name}}List({{table:lfname}}Searcher, tran);
        }
        
        /// <summary>
        /// 获取{{table:comment}}数据对象列表
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <returns>找到则返回对象列表，未找到则返回null</returns>
        public static IList<{{table:name}}> Get{{table:name}}List({{table:name}}Searcher {{table:lfname}}Searcher)
        {
            return Get{{table:name}}List({{table:lfname}}Searcher, (ICTransaction)null);
        }
        
        /// <summary>
        /// 获取{{table:comment}}数据对象列表
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>找到则返回对象列表，未找到则返回null</returns>
        public static IList<{{table:name}}> Get{{table:name}}List({{table:name}}Searcher {{table:lfname}}Searcher, ICTransaction tran)
        {
            return {{table:name}}Dal.FindList({{table:lfname}}Searcher, tran);
        }
        
        /// <summary>
        /// 获取{{table:comment}}分页数据对象列表
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>找到则返回对象列表，未找到则返回null</returns>
        public static PageList<{{table:name}}> Get{{table:name}}List({{table:name}}Searcher {{table:lfname}}Searcher, Pager pager)
        {
            return Get{{table:name}}List({{table:lfname}}Searcher, pager, null);
        }
        
        /// <summary>
        /// 获取{{table:comment}}分页数据对象列表
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>找到则返回对象列表，未找到则返回null</returns>
        public static PageList<{{table:name}}> Get{{table:name}}List({{table:name}}Searcher {{table:lfname}}Searcher, Pager pager, ICTransaction tran)
        {
            return {{table:name}}Dal.FindList({{table:lfname}}Searcher, pager, tran);
        }
        
        /// <summary>
        /// 获取所有{{table:comment}}数据表
        /// </summary>
        /// <returns>找到则返回对象列表，未找到则返回null</returns>
        public static DataTable Get{{table:name}}DataTable()
        {
            {{table:name}}Searcher {{table:lfname}}Searcher = new {{table:name}}Searcher();
            {{table:lfname}}Searcher.CreateTime.SortOrder = SortOrder.Asc;
            return Get{{table:name}}DataTable({{table:lfname}}Searcher, (ICTransaction)null);
        }
        
        /// <summary>
        /// 获取所有{{table:comment}}数据表
        /// </summary>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>找到则返回对象列表，未找到则返回null</returns>
        public static DataTable Get{{table:name}}DataTable(ICTransaction tran)
        {
            {{table:name}}Searcher {{table:lfname}}Searcher = new {{table:name}}Searcher();
            {{table:lfname}}Searcher.CreateTime.SortOrder = SortOrder.Asc;
            return Get{{table:name}}DataTable({{table:lfname}}Searcher, tran);
        }
        
        /// <summary>
        /// 获取指定条件的{{table:comment}}数据表
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <returns>找到则返回对象列表，未找到则返回null</returns>
        public static DataTable Get{{table:name}}DataTable({{table:name}}Searcher {{table:lfname}}Searcher)
        {
            return Get{{table:name}}DataTable({{table:lfname}}Searcher, (ICTransaction)null);
        }
        
        /// <summary>
        /// 获取指定条件的{{table:comment}}数据表
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>找到则返回对象列表，未找到则返回null</returns>
        public static DataTable Get{{table:name}}DataTable({{table:name}}Searcher {{table:lfname}}Searcher, ICTransaction tran)
        {
            return {{table:name}}Dal.FindDataTable({{table:lfname}}Searcher, tran);
        }
        
        /// <summary>
        /// 获取指定条件的{{table:comment}}分页数据表
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>找到则返回对象列表，未找到则返回null</returns>
        public static PageDataTable Get{{table:name}}DataTable({{table:name}}Searcher {{table:lfname}}Searcher, Pager pager)
        {
            return Get{{table:name}}DataTable({{table:lfname}}Searcher, pager, null);
        }
        
        /// <summary>
        /// 获取指定条件的{{table:comment}}分页数据表
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>找到则返回对象列表，未找到则返回null</returns>
        public static PageDataTable Get{{table:name}}DataTable({{table:name}}Searcher {{table:lfname}}Searcher, Pager pager, ICTransaction tran)
        {
            return {{table:name}}Dal.FindDataTable({{table:lfname}}Searcher, pager, tran);
        }
        
        #endregion
        
        #region Private Methods
        
        /// <summary>
        /// 保存{{table:comment}}记录，并锁定，以防止并发
        /// </summary>
        /// <param name="{{table:lfname}}">需要保存的数据对象</param>
        /// <param name="saveType">保存类型：0 为新建， 1 为修改</param>
        /// <param name="tran">中间数据库事务对象</param>
        private static void SaveEmailQueue({{table:name}} {{table:lfname}}, int saveType, ICTransaction tran)
        {
            lock (lockKey)
            {
                if (saveType == 0)
                {
                    {{table:name}}Dal.Add({{table:lfname}}, tran);
                }
                else
                {
                    {{table:name}}Dal.Update({{table:lfname}}, tran);
                }
            }
        }
        
        #endregion
        
        #endregion
        {{/loop:table}}
    }
}
