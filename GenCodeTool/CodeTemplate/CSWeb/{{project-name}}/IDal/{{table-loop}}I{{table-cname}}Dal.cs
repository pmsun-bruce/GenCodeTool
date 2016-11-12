namespace {{project:namespace}}.IDal 
{
	#region Reference

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;

    using NFramework.DBTool.Common;
    using NFramework.DBTool.QueryTool;

    using {{project:namespace}}.Entity;
    using {{project:namespace}}.Searcher;
	 
	#endregion

	/// <summary>
	/// {{table:comment}}数据接口
	/// </summary>
    public interface I{{table:cname}}Dal
    {
        /// <summary>
        /// 添加{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}">{{table:comment}}对象</param>
        void Add({{table:cname}} {{table:lfcname}});
        /// <summary>
        /// 添加{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}">{{table:comment}}对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        void Add({{table:cname}} {{table:lfcname}}, ICTransaction tran);
        /// <summary>
        /// 批量添加{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}List">{{table:comment}}对象列表</param>
        void Add(IList<{{table:cname}}> {{table:lfcname}}List);
        /// <summary>
        /// 批量添加{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}List">{{table:comment}}对象列表</param>
        /// <param name="tran">中间数据库事务对象</param>
        void Add(IList<{{table:cname}}> {{table:lfcname}}List, ICTransaction tran);
        /// <summary>
        /// 根据指定条件查找{{table:comment}}数量
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        long Count({{table:cname}}Searcher {{table:lfcname}}Searcher);
        /// <summary>
        /// 根据指定条件查找{{table:comment}}数量
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        long Count({{table:cname}}Searcher {{table:lfcname}}Searcher, ICTransaction tran);
        /// <summary>
        /// 根据PK删除{{table:comment}}
        /// </summary>
        /// <param name="{{pk:col:lfpname}}">{{pk:col:comment}}</param>
        void Delete({{pk:col:codetype}} {{pk:col:lfpname}});
        /// <summary>
        /// 根据PK删除{{table:comment}}
        /// </summary>
        /// <param name="{{pk:col:lfpname}}">{{pk:col:comment}}</param>
        /// <param name="tran">中间数据库事务对象</param>
        void Delete({{pk:col:codetype}} {{pk:col:lfpname}}, ICTransaction tran);
        /// <summary>
        /// 删除指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        void Delete({{table:cname}}Searcher {{table:lfcname}}Searcher);
        /// <summary>
        /// 删除指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        void Delete({{table:cname}}Searcher {{table:lfcname}}Searcher, ICTransaction tran);
        /// <summary>
        /// 根据PK查找{{table:comment}}
        /// </summary>
        /// <param name="{{pk:col:lfpname}}">{{pk:col:comment}}</param>
        /// <returns>返回{{table:comment}}对象，如未找到则返回null</returns>
        {{table:cname}} FindSingle({{pk:col:codetype}} {{pk:col:lfpname}});
        /// <summary>
        /// 根据PK查找{{table:comment}}
        /// </summary>
        /// <param name="{{pk:col:lfpname}}">{{pk:col:comment}}</param>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回{{table:comment}}对象，如未找到则返回null</returns>
        {{table:cname}} FindSingle({{pk:col:codetype}} {{pk:col:lfpname}}, ICTransaction tran);
        /// <summary>
        /// 查找指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <returns>返回{{table:comment}}对象列表，如未找到则返回null</returns>
        IList<{{table:cname}}> FindList({{table:cname}}Searcher {{table:lfcname}}Searcher);
        /// <summary>
        /// 查找指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回{{table:comment}}对象列表，如未找到则返回null</returns>
        IList<{{table:cname}}> FindList({{table:cname}}Searcher {{table:lfcname}}Searcher, ICTransaction tran);
        /// <summary>
        /// 查找指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回分页结果对象</returns>
        PageList<{{table:cname}}> FindList({{table:cname}}Searcher {{table:lfcname}}Searcher, Pager pager);
        /// <summary>
        /// 查找指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回分页结果对象</returns>
        PageList<{{table:cname}}> FindList({{table:cname}}Searcher {{table:lfcname}}Searcher, Pager pager, ICTransaction tran);
        /// <summary>
        /// 查找指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <returns>返回一个DataTable对象</returns>
        DataTable FindDataTable({{table:cname}}Searcher {{table:lfcname}}Searcher);
        /// <summary>
        /// 查找指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回一个DataTable对象</returns>
        DataTable FindDataTable({{table:cname}}Searcher {{table:lfcname}}Searcher, ICTransaction tran);
        /// <summary>
        /// 查找指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的DataTable数据</returns>
        PageDataTable FindDataTable({{table:cname}}Searcher {{table:lfcname}}Searcher, Pager pager);
        /// <summary>
        /// 查找指定条件的{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="pager">分页对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回查找到的分页集合，包括按条件可查询到的所有记录数和当前分页的DataTable数据</returns>
        PageDataTable FindDataTable({{table:cname}}Searcher {{table:lfcname}}Searcher, Pager pager, ICTransaction tran);
        /// <summary>
        /// 更新{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}">{{table:comment}}</param>
        void Update({{table:cname}} {{table:lfcname}});
        /// <summary>
        /// 更新{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}">{{table:comment}}</param>
        /// <param name="tran">中间数据库事务对象</param>
        void Update({{table:cname}} {{table:lfcname}}, ICTransaction tran);
        /// <summary>
        /// 批量更新{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}List">{{table:comment}}列表</param>
        void Update(IList<{{table:cname}}> {{table:lfcname}}List);
        /// <summary>
        /// 批量更新{{table:comment}}
        /// </summary>
        /// <param name="{{table:lfcname}}List">{{table:comment}}列表</param>
        /// <param name="tran">中间数据库事务对象</param>
        void Update(IList<{{table:cname}}> {{table:lfcname}}List, ICTransaction tran);
        /// <summary>
        /// 获取查找{{table:comment}} PK的SQL语句，用于其他SQL的子查询或拼接使用
        /// </summary>
        /// <param name="{{table:lfcname}}Searcher">{{table:comment}}查询对象</param>
        /// <param name="paramCollection">返回当前查询对象中所有的条件对象集合</param>
        /// <return>返回拼接后的SQL语句</return>
        string GetPKSQLCommand({{table:cname}}Searcher {{table:lfcname}}Searcher, out DBParamCollection paramCollection);
    }
}