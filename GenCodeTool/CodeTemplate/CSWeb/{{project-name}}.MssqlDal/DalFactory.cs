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

    using {{project:namespace}}.IDal;

	#endregion

	/// <summary>
	/// 数据操作接口创建者类
	/// </summary>
    public class DalFactory : MssqlDalFactoryBase, {{project:namespace}}.IDal.IDalFactory
    {
        #region Public Methods
        {{loop:table}}
        /// <summary>
        /// 创建{{table:comment}}数据操作类
        /// </summary>
        /// <returns>返回{{table:comment}}数据操作类</returns>
        public I{{table:name}}Dal Create{{table:name}}Dal()
        {
            return new {{table:name}}Dal(this);
        }
        {{/loop:table}}
        #endregion

        #region Public Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="currConnectionString">数据库连接字符串</param>
        public DalFactory(string currConnectionString)
            : base(currConnectionString)
        {

        }

        #endregion

    }
}