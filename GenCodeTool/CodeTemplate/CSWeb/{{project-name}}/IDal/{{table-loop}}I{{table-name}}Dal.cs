namespace {{proejct:namespace}}.IDal
{
    #region Reference

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;

    using NFramework.DBTool.Common;

    using {{proejct:namespace}}.Entity;
    using {{proejct:namespace}}.Searcher;

    #endregion

    public interface I{{table:name}}Dal : NFramework.DBTool.Common.IDalBase
    {
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="{{table:lfname}}">实体对象</param>
        /// <returns>新建成功返回true，新建失败返回false</returns>
        void Add({{table:name}} {{table:lfname}});
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="{{table:lfname}}">实体对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>新建成功返回true，新建失败返回false</returns>
        void Add({{table:name}} {{table:lfname}}, ICTransaction tran);
        /// <summary>
        /// 统计记录数
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <returns>返回记录数，如果没有返回0</returns>
        long Count({{table:name}}Searcher {{table:lfname}}Searcher);
        /// <summary>
        /// 统计记录数
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回记录数，如果没有返回0</returns>
        long Count({{table:name}}Searcher {{table:lfname}}Searcher, ICTransaction tran);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <returns>删除成功返回true，删除失败返回false</returns>
        void Delete({{table:name}}Searcher {{table:lfname}}Searcher);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>删除成功返回true，删除失败返回false</returns>
        void Delete({{table:name}}Searcher {{table:lfname}}Searcher, ICTransaction tran);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="localCode"></param>
        /// <returns>返回实例对象集合，如未找到则返回null</returns>
        {{table:name}} FindSingle(string localCode);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="localCode"></param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回实例对象集合，如未找到则返回null</returns>
        {{table:name}} FindSingle(string localCode, ICTransaction tran);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <returns>返回实例对象集合，如未找到则返回null</returns>
        IList<{{table:name}}> FindList({{table:name}}Searcher {{table:lfname}}Searcher);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回实例对象集合，如未找到则返回null</returns>
        IList<{{table:name}}> FindList({{table:name}}Searcher {{table:lfname}}Searcher, ICTransaction tran);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <param name="pager">查询分页对象</param>
        /// <returns>返回实例对象集合，如未找到则返回null</returns>
        PageList<{{table:name}}> FindList({{table:name}}Searcher {{table:lfname}}Searcher, Pager pager);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <param name="pager">查询分页对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回实例对象集合，如未找到则返回null</returns>
        PageList<{{table:name}}> FindList({{table:name}}Searcher {{table:lfname}}Searcher, Pager pager, ICTransaction tran);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <returns>返回实例对象集合，如未找到则返回null</returns>
        DataTable FindDataTable({{table:name}}Searcher {{table:lfname}}Searcher);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回实例对象集合，如未找到则返回null</returns>
        DataTable FindDataTable({{table:name}}Searcher {{table:lfname}}Searcher, ICTransaction tran);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <param name="pager">查询分页对象</param>
        /// <returns>返回实例对象集合，如未找到则返回null</returns>
        PageDataTable FindDataTable({{table:name}}Searcher {{table:lfname}}Searcher, Pager pager);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="{{table:lfname}}Searcher">查询对象</param>
        /// <param name="pager">查询分页对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>返回实例对象集合，如未找到则返回null</returns>
        PageDataTable FindDataTable({{table:name}}Searcher {{table:lfname}}Searcher, Pager pager, ICTransaction tran);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="{{table:lfname}}">更新的实例对象</param>
        /// <returns>更新成功返回true，更新失败返回false</returns>
        void Update({{table:name}} {{table:lfname}});
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="{{table:lfname}}">更新的实例对象</param>
        /// <param name="tran">中间数据库事务对象</param>
        /// <returns>更新成功返回true，更新失败返回false</returns>
        void Update({{table:name}} {{table:lfname}}, ICTransaction tran);
    }
}
