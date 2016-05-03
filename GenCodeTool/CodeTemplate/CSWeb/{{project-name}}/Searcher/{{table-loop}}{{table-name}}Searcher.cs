namespace {{project:namespace}}.Searcher 
{
	#region Reference
	
	using System;
	using System.Collections;
    using System.Collections.Generic;
    using System.Data;
	using System.Linq;
	using System.Text;
    
    using NFramework.DBTool.QueryTool;
	
	#endregion
    
	/// <summary>
	/// {{table:comment}}查询对象
    /// </summary>
    [DataContract]
    [Serializable]
    public class {{table:name}}Searcher : Searcher
    {
        #region Fields & Properties
        {{loop:col}}
        /// <summary>
        /// {{col:comment}}查询条件
        /// </summary>
        private SearchColumn {{col:lfname}};
        /// <summary>
        /// {{col:comment}}查询条件
        /// </summary>
        [DataMember]
        public SearchColumn {{col:name}}
        {
            get
            {
                if ({{col:lfname}} == null)
                {
                    {{col:lfname}} = new SearchColumn(this, "{{col:name}}");
                    this.ConditionColumnList.Add({{col:lfname}});
                }

                return {{col:lfname}};
            }
        }
        {{/loop:col}}
{{loop:fk}}
        /// <summary>
        /// {{fktable:comment}}查询对象
        /// </summary>
        private {{fk:table:name}}Searcher curr{{fk:table:name}};
        /// <summary>
        /// {{fktable:comment}}查询对象
        /// </summary>
        [DataMember]
        public {{fk:table:name}}Searcher Curr{{fk:table:name}}
        {
            get
            {
                return curr{{fk:table:name}};
            }
            set
            {
                if (this.RelationSearcherList.Contains(curr{{fk:table:name}}))
                {
                    this.RelationSearcherList.Remove(curr{{fk:table:name}});
                }

                curr{{fk:table:name}} = value;

                if (!this.RelationSearcherList.Contains(curr{{fk:table:name}}))
                {
                    this.RelationSearcherList.Add(curr{{fk:table:name}});
                }
            }
        }
        {{/loop:fk}}{{rlast}}\n\n{{/rlast}}
        #endregion
        
        #region Public Constructors
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public {{table:name}}Searcher()
        {
            this.TableName = "{{table:name}}";
        }
        
        #endregion

    }
}