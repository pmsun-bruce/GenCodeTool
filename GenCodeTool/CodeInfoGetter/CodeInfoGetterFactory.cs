namespace NFramework.GenCodeTool.CodeInfoGetter
{
    #region Reference

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    #endregion

    /// <summary>
    /// 代码信息获取器工厂
    /// </summary>
    public class CodeInfoGetterFactory
    {
        #region Fields & Properties

        /// <summary>
        /// 代码信息获取器池，所有可使用的代码信息获取器在这里注册
        /// </summary>
        public static IList<ICodeInfoGetter> CodeInfoGetterPool
        {
            get;
            private set;
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// 获取指定名称的代码信息获取器
        /// </summary>
        /// <param name="getterName">代码信息获取器的名称</param>
        /// <returns>返回代码信息获取器，如果没有返回null</returns>
        public static ICodeInfoGetter GetCodeInfoGetter(string getterName)
        {
            ICodeInfoGetter codeInfoGetter = CodeInfoGetterPool.FirstOrDefault<ICodeInfoGetter>(g => g.GetterName.Equals(getterName));
            return codeInfoGetter;
        }

        #endregion

        #region Static Constructors

        /// <summary>
        /// 静态构造函数，注册所有的代码信息获取器
        /// </summary>
        static CodeInfoGetterFactory()
        {
            CodeInfoGetterPool = new List<ICodeInfoGetter>();

            ICodeInfoGetter csCodeInfoGetter = new CSCodeInfoGetter();
            CodeInfoGetterPool.Add(csCodeInfoGetter);

            //ICodeInfoGetter javaCodeInfoGetter = new JavaCodeInfoGetter();
            //CodeInfoGetterPool.Add(javaCodeInfoGetter);
        }

        #endregion
    }
}
