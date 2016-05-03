namespace {{proejct:namespace}}.IDal
{
    #region Reference
    
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    #endregion
    
    public interface IDalFactory : NFramework.DBTool.Common.IDalFactoryBase
    {{{loop:table}}
        /// <summary>
        /// 创建一个{{table:comment}}数据操作接口
        /// </summary>
        /// <returns>返回{{table:comment}}数据操作接口</returns>
        I{{table:name}}Dal Create{{table:name}}Dal();
        {{/loop:table}}
    }
}
