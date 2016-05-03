namespace {{proejct:namespace}}.IDal 
{
    #region Reference

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    #endregion

	/// <summary>
    /// 数据存储管理器
    /// </summary>
    public class DalManager
    {
        #region Fields & Properties

        /// <summary>
        /// Dal创建器
        /// </summary>
        private static IDalFactory dalFactory;
        /// <summary>
        /// Dal创建器
        /// </summary>
        public static IDalFactory DalFactory
        {
            get
            {
                return dalFactory;
            }
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// 加载Dal创建器
        /// </summary>
        /// <param name="dalFactory">Dal创建器</param>
        public static void Load(IDalFactory dalFactory)
        {
            DalManager.dalFactory = dalFactory;
        }

        #endregion
    }

}