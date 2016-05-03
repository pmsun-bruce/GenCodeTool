using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFramework.GenCodeTool.Entity;
using NFramework.GenCodeTool.Handler;
using System.Data;
using System.Text.RegularExpressions;
using NFramework.GenCodeTool.CodeInfoGetter;
using NFramework.GenCodeTool.DBInfoGetter;

namespace GenCodeTool.Test
{
    /// <summary>
    /// GenCodeToolTest 的摘要说明
    /// </summary>
    [TestClass]
    public class GenCodeToolTest
    {
        public GenCodeToolTest()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性:
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            ProjectInfo projectInfo = new ProjectInfo();
            projectInfo.TemplatePath = @"E:\Projects\Framework\SourceCode\Master\GenCodeTool\CodeTemplate\CSWeb";
            projectInfo.GenTargetPath = @"E:\TestGenCode";
            projectInfo.Name = "TestProject";
            projectInfo.Namespace = "NFramework.TestProject";
            projectInfo.DisplayName = "测试生成";
            projectInfo.CodeInfoGetter = new CSCodeInfoGetter();
            projectInfo.DBInfoGetter = new MssqlDBInfoGetter();
            projectInfo.IsClearTargetFolder = true;

            TableInfo tableInfo = null;
            ColumnInfo colInfo = null;

            #region Company

            tableInfo = new TableInfo();
            tableInfo.Name = "Company";
            tableInfo.Comment = "公司";
            TableInfo comTableInfo = tableInfo;

            colInfo = new ColumnInfo();
            colInfo.Name = "Name";
            colInfo.DbType = DbType.String;
            colInfo.SqlType = "VarChar";
            colInfo.DalType = projectInfo.DBInfoGetter.ToDalType(colInfo.SqlType, colInfo.Precision, colInfo.Scale);
            colInfo.CodeType = projectInfo.CodeInfoGetter.ToCodeType(colInfo.DbType);
            colInfo.MaxLength = 100;
            colInfo.IsNullable = false;
            colInfo.Comment = "名称";
            colInfo.CurrTable = tableInfo;
            colInfo.IsGenSearchCondition = true;
            colInfo.IsGenInput = true;
            colInfo.IsGenSearchResult = true;
            colInfo.IsUnique = false;
            tableInfo.AddColumn(colInfo);

            colInfo = new ColumnInfo();
            colInfo.Name = "Code";
            colInfo.DbType = DbType.String;
            colInfo.SqlType = "VarChar";
            colInfo.DalType = projectInfo.DBInfoGetter.ToDalType(colInfo.SqlType, colInfo.Precision, colInfo.Scale);
            colInfo.CodeType = projectInfo.CodeInfoGetter.ToCodeType(colInfo.DbType);
            colInfo.MaxLength = 10;
            colInfo.IsNullable = false;
            colInfo.Comment = "编号";
            colInfo.CurrTable = tableInfo;
            colInfo.IsGenSearchCondition = true;
            colInfo.IsGenInput = true;
            colInfo.IsGenSearchResult = true;
            colInfo.IsUnique = true;
            tableInfo.AddColumn(colInfo);

            colInfo = new ColumnInfo();
            colInfo.Name = "CompanyId";
            colInfo.DbType = DbType.String;
            colInfo.SqlType = "VarChar";
            colInfo.DalType = projectInfo.DBInfoGetter.ToDalType(colInfo.SqlType, colInfo.Precision, colInfo.Scale);
            colInfo.CodeType = projectInfo.CodeInfoGetter.ToCodeType(colInfo.DbType);
            colInfo.MaxLength = 40;
            colInfo.IsNullable = false;
            colInfo.IsPK = true;
            colInfo.Comment = "ID";
            colInfo.CurrTable = tableInfo;
            colInfo.IsGenSearchCondition = false;
            colInfo.IsGenInput = false;
            colInfo.IsGenSearchResult = false;
            colInfo.IsUnique = true;
            tableInfo.AddColumn(colInfo);

            colInfo = new ColumnInfo();
            colInfo.Name = "CreateTime";
            colInfo.DbType = DbType.DateTime;
            colInfo.SqlType = "DateTime";
            colInfo.DalType = projectInfo.DBInfoGetter.ToDalType(colInfo.SqlType, colInfo.Precision, colInfo.Scale);
            colInfo.CodeType = projectInfo.CodeInfoGetter.ToCodeType(colInfo.DbType);
            colInfo.MaxLength = 12;
            colInfo.IsNullable = false;
            colInfo.IsPK = false;
            colInfo.Comment = "创建时间";
            colInfo.CurrTable = tableInfo;
            colInfo.IsGenSearchCondition = false;
            colInfo.IsGenInput = false;
            colInfo.IsGenSearchResult = false;
            colInfo.IsUnique = false;
            tableInfo.AddColumn(colInfo);

            colInfo = new ColumnInfo();
            colInfo.Name = "Status";
            colInfo.DbType = DbType.Int32;
            colInfo.SqlType = "Int";
            colInfo.DalType = projectInfo.DBInfoGetter.ToDalType(colInfo.SqlType, colInfo.Precision, colInfo.Scale);
            colInfo.CodeType = projectInfo.CodeInfoGetter.ToCodeType(colInfo.DbType);
            colInfo.MaxLength = 8;
            colInfo.DefaultValue = "((1))";
            colInfo.IsNullable = false;
            colInfo.IsPK = false;
            colInfo.Comment = "Status";
            colInfo.CurrTable = tableInfo;
            colInfo.IsGenSearchCondition = true;
            colInfo.IsGenInput = false;
            colInfo.IsGenSearchResult = true;
            colInfo.IsUnique = false;
            tableInfo.AddColumn(colInfo);

            colInfo = new ColumnInfo();
            colInfo.Name = "Col2";
            colInfo.DbType = DbType.String;
            colInfo.SqlType = "VarChar";
            colInfo.DalType = projectInfo.DBInfoGetter.ToDalType(colInfo.SqlType, colInfo.Precision, colInfo.Scale);
            colInfo.CodeType = projectInfo.CodeInfoGetter.ToCodeType(colInfo.DbType);
            colInfo.MaxLength = 50;
            colInfo.DefaultValue = "22";
            colInfo.IsNullable = true;
            colInfo.IsPK = false;
            colInfo.Comment = "Col2的字段";
            colInfo.CurrTable = tableInfo;
            colInfo.IsGenSearchCondition = true;
            colInfo.IsGenInput = false;
            colInfo.IsGenSearchResult = true;
            colInfo.IsUnique = false;
            tableInfo.AddColumn(colInfo);
            
            projectInfo.AddTableInfo(tableInfo);

            #endregion

            #region Department

            tableInfo = new TableInfo();
            tableInfo.Name = "Department";
            tableInfo.Comment = "部门";

            colInfo = new ColumnInfo();
            colInfo.Name = "Name";
            colInfo.DbType = DbType.String;
            colInfo.SqlType = "VarChar";
            colInfo.DalType = projectInfo.DBInfoGetter.ToDalType(colInfo.SqlType, colInfo.Precision, colInfo.Scale);
            colInfo.CodeType = projectInfo.CodeInfoGetter.ToCodeType(colInfo.DbType);
            colInfo.MaxLength = 100;
            colInfo.IsNullable = false;
            colInfo.Comment = "名称";
            colInfo.CurrTable = tableInfo;
            colInfo.IsGenSearchCondition = true;
            colInfo.IsGenInput = true;
            colInfo.IsGenSearchResult = true;
            colInfo.IsUnique = false;
            tableInfo.AddColumn(colInfo);

            colInfo = new ColumnInfo();
            colInfo.Name = "Code";
            colInfo.DbType = DbType.String;
            colInfo.SqlType = "VarChar";
            colInfo.DalType = projectInfo.DBInfoGetter.ToDalType(colInfo.SqlType, colInfo.Precision, colInfo.Scale);
            colInfo.CodeType = projectInfo.CodeInfoGetter.ToCodeType(colInfo.DbType);
            colInfo.MaxLength = 10;
            colInfo.IsNullable = false;
            colInfo.Comment = "编号";
            colInfo.CurrTable = tableInfo;
            colInfo.IsGenSearchCondition = true;
            colInfo.IsGenInput = true;
            colInfo.IsGenSearchResult = true;
            colInfo.IsUnique = true;
            tableInfo.AddColumn(colInfo);

            colInfo = new ColumnInfo();
            colInfo.Name = "DepartmentId";
            colInfo.DbType = DbType.String;
            colInfo.SqlType = "VarChar";
            colInfo.DalType = projectInfo.DBInfoGetter.ToDalType(colInfo.SqlType, colInfo.Precision, colInfo.Scale);
            colInfo.CodeType = projectInfo.CodeInfoGetter.ToCodeType(colInfo.DbType);
            colInfo.MaxLength = 40;
            colInfo.IsNullable = false;
            colInfo.IsPK = true;
            colInfo.Comment = "ID";
            colInfo.CurrTable = tableInfo;
            colInfo.IsGenSearchCondition = false;
            colInfo.IsGenInput = false;
            colInfo.IsGenSearchResult = false;
            colInfo.IsUnique = true;
            tableInfo.AddColumn(colInfo);

            colInfo = new ColumnInfo();
            colInfo.Name = "CompanyId";
            colInfo.DbType = DbType.String;
            colInfo.SqlType = "VarChar";
            colInfo.DalType = projectInfo.DBInfoGetter.ToDalType(colInfo.SqlType, colInfo.Precision, colInfo.Scale);
            colInfo.CodeType = projectInfo.CodeInfoGetter.ToCodeType(colInfo.DbType);
            colInfo.MaxLength = 40;
            colInfo.IsNullable = false;
            colInfo.IsPK = false;
            colInfo.IsFK = true;
            colInfo.FKName = "FK_Company_Department";
            colInfo.Comment = "所属公司ID";
            colInfo.CurrTable = tableInfo;
            colInfo.IsGenSearchCondition = false;
            colInfo.IsGenInput = false;
            colInfo.IsGenSearchResult = false;
            colInfo.IsUnique = false;
            colInfo.FKColumn = comTableInfo.PKList[0];
            tableInfo.AddColumn(colInfo);

            projectInfo.AddTableInfo(tableInfo);

            #endregion

            #region Position

            tableInfo = new TableInfo();
            tableInfo.Name = "Position";
            projectInfo.AddTableInfo(tableInfo);

            #endregion

            #region Employee

            tableInfo = new TableInfo();
            tableInfo.Name = "Employee";
            projectInfo.AddTableInfo(tableInfo);

            #endregion

            GenCodeHandler.GenCode(projectInfo);
        }
    }
}
