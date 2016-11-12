namespace {{project:namespace}}.Searcher 
{
	#region Reference

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    using NFramework.DBTool.QueryTool;
	 
	#endregion

	/// <summary>
	/// {{table:comment}}
    /// </summary>
    [Serializable]
    [DataContract(IsReference=true)]
	public class {{table:cname}}Searcher : NFramework.DBTool.QueryTool.Searcher
    {
        #region Fields & Properties
		{{loop:col}}
        /// <summary>
        /// {{col:comment}}查询条件
        /// </summary>
        private SearchColumn {{col:lfpname}};
        /// <summary>
        /// {{col:comment}}查询条件
        /// </summary>
        [DataMember]
        public SearchColumn {{col:pname}}
        {
            get
            {
                if ({{col:lfpname}} == null)
                {
                    {{col:lfpname}} = new SearchColumn(this, "{{col:pname}}", {{col:daltype}});
                    this.ConditionColumnList.Add({{col:lfpname}});
                }

                return {{col:lfpname}};
            }
        }
        {{/loop:col}}
{{loop:fk}}
        /// <summary>
        /// {{fktable:comment}}查询对象
        /// </summary>
        private {{fk:table:cname}}Searcher curr{{fk:table:cname}};
        /// <summary>
        /// {{fktable:comment}}查询对象
        /// </summary>
        [DataMember]
        public {{fk:table:cname}}Searcher Curr{{fk:table:cname}}
        {
            get
            {
                return curr{{fk:table:cname}};
            }
            set
            {
                if (this.RelationSearcherList.Contains(curr{{fk:table:cname}}))
                {
                    this.RelationSearcherList.Remove(curr{{fk:table:cname}});
                }

                curr{{fk:table:cname}} = value;
                
                if (!this.RelationSearcherList.Contains(curr{{fk:table:cname}}))
                {
                    this.RelationSearcherList.Add(curr{{fk:table:cname}});
                }
            }
        }
        {{/loop:fk}}
        #endregion
        
        #region Public Constructors
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public {{table:cname}}Searcher() : base("{{table:cname}}")
        {
        }
        
        #endregion
    }
}