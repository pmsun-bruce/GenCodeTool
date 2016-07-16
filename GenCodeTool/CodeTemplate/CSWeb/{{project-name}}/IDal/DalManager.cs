namespace {{project:namespace}}.IDal 
{
	#region Reference
	
	using System;
	using System.Collections;
	using System.Collections.Generic;
    using System.Data;
	using System.Linq;
	using System.Text;

	#endregion

    /// <summary>
    /// 数据操作管理类，存储了具体数据的Dal工厂类
    /// </summary>
    public class DalManager
    {
        #region Fields & Properties

        /// <summary>
        /// Dal工厂类
        /// </summary>
        private static IDalFactory dalFactory;
        /// <summary>
        /// Dal工厂类，只读属性
        /// </summary>
        public static IDalFactory DalFactory
        {
            get
            {
                return DalManager.dalFactory;
            }
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// 加载Dal工厂
        /// </summary>
        /// <param name="dalFactory">实际运用的Dal工厂类</param>
        public static void Load(IDalFactory dalFactory)
        {
            DalManager.dalFactory = dalFactory;
        }

        #endregion
    }
}