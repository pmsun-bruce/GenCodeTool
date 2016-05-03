namespace {{project:namespace}}
{
	#region Reference
	
	using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
	 
	#endregion

	/// <summary>
	/// {{table:comment}}实体类
    /// </summary>
    [DataContract]
    [Serializable]
    public class {{table:name}}
    {
        #region Fields & Properties
        {{loop:col}}
        /// <summary>
        /// {{col:comment}}
        /// </summary>
        [DataMember]
        public {{col:codetype}} {{col:name}}
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
        public {{fk:table:name}} Curr{{fk:table:name}}
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
        public IList<{{rk:table:name}}> {{rk:table:name}}List
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
        public {{table:name}}()
        {{{loop:col}}
            this.{{col:name}} = {{col:defaultvalue}};{{/loop:col}}
        }
        
        /// <summary>
        /// {{table:comment}}
        /// </summary>{{loop:col}}
        /// <param name="{{col:lfname}}">{{col:comment}}</param>{{/loop:col}}
        public {{table:name}}({{loop:col|rlast:,|rlast: }}{{col:codetype}} {{col:lfname}}, {{/loop:col}})
        {{{loop:col}}
            this.{{col:name}} = {{col:lfname}};{{/loop:col}}
        }
        
        #endregion

    }
}