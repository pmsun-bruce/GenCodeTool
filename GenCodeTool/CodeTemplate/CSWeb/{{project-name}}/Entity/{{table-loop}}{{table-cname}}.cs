namespace {{project:namespace}}.Entity 
{
	#region Reference
	
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
    using System.Runtime.Serialization;
	using System.Text;
	
	#endregion

	/// <summary>
	/// {{table:comment}}
	/// </summary>
    [Serializable]
    [DataContract(IsReference=true)]
    public class {{table:cname}} : NFramework.DBTool.Common.IRVersion
    {
		#region Fields & Properties
        {{loop:col}}
        /// <summary>
        /// {{col:comment}}
        /// </summary>
        [DataMember]
        public {{col:codetype}} {{col:pname}}
        {
            get;
            set;
        }
        {{/loop:col}}
{{loop:fk}}
        /// <summary>
        /// {{fk:table:comment}}
        /// </summary>
        [DataMember]
        public {{fk:table:cname}} Curr{{fk:table:cname}}
        {
            get;
            set;
        }
        {{/loop:fk}}{{rlast}}\n\n{{/rlast}}
{{loop:rk}}
        /// <summary>
        /// {{rk:table:comment}}
        /// </summary>
        [DataMember]
        public IList<{{rk:table:cname}}> {{rk:table:cname}}List
        {
            get;
            set;
        }
        {{/loop:rk}}{{rlast}}\n\n{{/rlast}}
        #endregion
        
        #region Public Constructors
        
        /// <summary>
        /// {{table:comment}}
        /// </summary>
        public {{table:cname}}()
        {{{loop:col|ignparam:pk,createtime}}
            this.{{col:pname}} = {{col:defaultvalue}};{{/loop:col}}
        }
        
        /// <summary>
        /// {{table:comment}}
        /// </summary>{{loop:col}}
        /// <param name="{{col:lfpname}}">{{col:comment}}</param>{{/loop:col}}
        public {{table:cname}}({{loop:col|rlast:,|rlast: }}{{col:codetype}} {{col:lfpname}}, {{/loop:col}})
        {{{loop:col}}
            this.{{col:pname}} = {{col:lfpname}};{{/loop:col}}
        }
        
        #endregion
    }
}