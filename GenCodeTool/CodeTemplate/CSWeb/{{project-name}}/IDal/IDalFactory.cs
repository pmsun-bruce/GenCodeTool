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

	#endregion

	/// <summary>
	/// 数据操作对象创建者
	/// </summary>
    public interface IDalFactory : NFramework.DBTool.Common.IDalFactoryBase
    {
		{{loop:table}}
        /// <summary>
        /// 创建{{table:comment}}数据操作类
        /// </summary>
        I{{table:cname}}Dal Create{{table:cname}}Dal();{{/loop:table}}
    }
}