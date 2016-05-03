namespace {{project:namespace}}.Dal
{
	#region Reference
	
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
    
    using NFramework.DBTool.QueryTool.Mssql;
    
    using {{project:namespace}}.IDal;
    
	#endregion
    
	/// <summary>
	/// Mssql Dal创建器
	/// </summary>
	public class DalFactory : MssqlDalFactoryBase, IDalFactory
	{
		#region Public Methods
        {{loop:table}}
        /// <summary>
        /// 创建{{table:name}}数据操作接口
		/// </summary>
        /// <returns>返回{{table:name}}数据操作接口</returns>
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
		/// <param name="connectionString">数据库连接字符串</param>
        public DalFactory(string currConnectionString)
            : base(currConnectionString)
        {
            
        }
        
		#endregion
    }
}