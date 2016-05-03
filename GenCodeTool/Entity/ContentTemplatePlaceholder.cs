namespace NFramework.GenCodeTool.Entity
{
    #region Reference

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    #endregion

    /// <summary>
    /// 内容占位符
    /// </summary>
    public class ContentTemplatePlaceholder
    {
        #region Project Info

        /// <summary>
        /// 项目名称占位符
        /// </summary>
        public const string ProjectName = "{{project:name}}";
        /// <summary>
        /// 全大写项目名称占位符
        /// </summary>
        public const string ProjectNameUp = "{{project:uname}}"; 
        /// <summary>
        /// 全小写项目名称占位符
        /// </summary>
        public const string ProjectNameLow = "{{project:lname}}"; 
        /// <summary>
        /// 首字母小写项目名称占位符
        /// </summary>
        public const string ProjectNameLowFirst = "{{project:lfname}}"; 
        /// <summary>
        /// 项目命名空间占位符
        /// </summary>
        public const string ProjectNamespace = "{{project:namespace}}";
        /// <summary>
        /// 项目显示名称占位符
        /// </summary>
        public const string ProjectDisplayName = "{{project:displayname}}"; 

        #endregion

        #region Table Info

        /// <summary>
        /// 表循环占位符
        /// </summary>
        public const string TableLoopSection = "{{loop:table}}{0}{{/loop:table}}";
        /// <summary>
        /// 表序号占位符，即给当前表分配的排序号
        /// </summary>
        public const string TableIndex = "{{table:index}}"; 
        /// <summary>
        /// 表名称占位符
        /// </summary>
        public const string TableName = "{{table:name}}"; 
        /// <summary>
        /// 全大写表名称占位符
        /// </summary>
        public const string TableNameUp = "{{table:uname}}"; 
        /// <summary>
        /// 全小写表名称占位符
        /// </summary>
        public const string TableNameLow = "{{table:lname}}"; 
        /// <summary>
        /// 首字母小写表名称占位符
        /// </summary>
        public const string TableNameLowFirst = "{{table:lfname}}";
        /// <summary>
        /// 表备注占位符
        /// </summary>
        public const string TableComment = "{{table:comment}}";

        #endregion

        #region Column Info

        /// <summary>
        /// 字段循环占位符
        /// </summary>
        public const string ColumnLoopSection = "{{loop:col}}{0}{{/loop:col}}";
        /// <summary>
        /// 字段ID占位符，即字段在数据库中的排序号
        /// </summary>
        public const string ColumnId = "{{col:id}}";
        /// <summary>
        /// 字段名称占位符
        /// </summary>
        public const string ColumnName = "{{col:name}}";
        /// <summary>
        /// 全大写字段名称占位符
        /// </summary>
        public const string ColumnNameUp = "{{col:uname}}";
        /// <summary>
        /// 全小写字段名称占位符
        /// </summary>
        public const string ColumnNameLow = "{{col:lname}}";
        /// <summary>
        /// 首字母小写字段名称占位符
        /// </summary>
        public const string ColumnNameLowFirst = "{{col:lfname}}";
        /// <summary>
        /// 字段备注占位符
        /// </summary>
        public const string ColumnComment = "{{col:comment}}";
        /// <summary>
        /// 字段默认值占位符
        /// </summary>
        public const string ColumnDefaultValue = "{{col:defaultvalue}}";
        /// <summary>
        /// 字段有效位占位符
        /// </summary>
        public const string ColumnPrecision = "{{col:precision}}";
        /// <summary>
        /// 字段有效小数位占位符
        /// </summary>
        public const string ColumnScale = "{{col:scale}}";
        /// <summary>
        /// 字段最大值占位符
        /// </summary>
        public const string ColumnMax = "{{col:max}}";
        /// <summary>
        /// 字段最小值占位符
        /// </summary>
        public const string ColumnMin = "{{col:min}}";
        /// <summary>
        /// 字段最大长度占位符
        /// </summary>
        public const string ColumnMaxLength = "{{col:maxlen}}";
        /// <summary>
        /// 字段在程序中的类型占位符
        /// </summary>
        public const string ColumnCodeType = "{{col:codetype}}";
        /// <summary>
        /// 字段的数据库类型占位符
        /// </summary>
        public const string ColumnSqlType = "{{col:sqltype}}";
        /// <summary>
        /// 字段在Dal中使用的数据类型占位符
        /// </summary>
        public const string ColumnDalType = "{{col:daltype}}";
        /// <summary>
        /// 字段的外键名称占位符
        /// </summary>
        public const string ColumnFKName = "{{col:fkname}}";
        /// <summary>
        /// 字段类型装换占位符
        /// </summary>
        public const string ColumnConvert = "{{col:convert}}{0}{{/col:convert}}";
        /// <summary>
        /// 字段转字符串占位符
        /// </summary>
        public const string ColumnToString = "{{col:tostring}}{0}{{/col:tostring}}";

        #endregion

        #region PK Info

        /// <summary>
        /// 循环主键占位符
        /// </summary>
        public const string PKLoopSection = "{{loop:pk}}{0}{{/loop:pk}}";
        /// <summary>
        /// 主键ID占位符，即主键字段在数据库中的排序号
        /// </summary>
        public const string PKId = "{{pk:col:id}}";
        /// <summary>
        /// 主键字段名称占位符
        /// </summary>
        public const string PKName = "{{pk:col:name}}";
        /// <summary>
        /// 全小写主键字段名称占位符
        /// </summary>
        public const string PKNameLow = "{{pk:col:lname}}";
        /// <summary>
        /// 全大写主键字段名称占位符
        /// </summary>
        public const string PKNameUp = "{{pk:col:uname}}";
        /// <summary>
        /// 首字母小写主键字段名称占位符
        /// </summary>
        public const string PKNameLowFirst = "{{pk:col:lfname}}";
        /// <summary>
        /// 主键字段备注占位符
        /// </summary>
        public const string PKComment = "{{pk:col:comment}}";
        /// <summary>
        /// 主键默认值占位符
        /// </summary>
        public const string PKDefaultValue = "{{pk:col:defaultvalue}}";
        /// <summary>
        /// 主键有效位数占位符
        /// </summary>
        public const string PKPrecision = "{{pk:col:precision}}";
        /// <summary>
        /// 主键有效小数位占位符
        /// </summary>
        public const string PKScale = "{{pk:col:scale}}";
        /// <summary>
        /// 主键最大值占位符
        /// </summary>
        public const string PKMax = "{{pk:col:max}}";
        /// <summary>
        /// 主键最小值占位符
        /// </summary>
        public const string PKMin = "{{pk:col:min}}";
        /// <summary>
        /// 主键最大长度占位符
        /// </summary>
        public const string PKMaxLength = "{{pk:col:maxlen}}";
        /// <summary>
        /// 主键在程序中的数据类型占位符
        /// </summary>
        public const string PKCodeType = "{{pk:col:codetype}}";
        /// <summary>
        /// 主键在数据库中的数据类型占位符
        /// </summary>
        public const string PKSqlType = "{{pk:col:sqltype}}";
        /// <summary>
        /// 主键在Dal中使用的数据类型占位符
        /// </summary>
        public const string PKDalType = "{{col:daltype}}";
        /// <summary>
        /// 主键类型转换占位符
        /// </summary>
        public const string PKConvert = "{{pk:col:convert}}{0}{{/pk:col:convert}}";
        /// <summary>
        /// 主键值转字符串占位符
        /// </summary>
        public const string PKToString = "{{pk:col:tostring}}{0}{{/pk:col:tostring}}";

        #endregion

        #region FK Info

        /// <summary>
        /// 循环外键占位符
        /// </summary>
        public const string FKLoopSection = "{{loop:fk}}{0}{{/loop:fk}}";
        /// <summary>
        /// 外键ID占位符，即主键字段在数据库中的排序号
        /// </summary>
        public const string FKId = "{{fk:col:id}}";
        /// <summary>
        /// 外键表名称占位符
        /// </summary>
        public const string FKTableName = "{{fk:table:name}}";
        /// <summary>
        /// 全大写外键表名称占位符
        /// </summary>
        public const string FKTableNameUp = "{{fk:table:uname}}";
        /// <summary>
        /// 全小写外键表名称占位符
        /// </summary>
        public const string FKTableNameLow = "{{fk:table:lname}}";
        /// <summary>
        /// 首字母小写外键表名称占位符
        /// </summary>
        public const string FKTableNameLowFirst = "{{fk:table:lfname}}";
        /// <summary>
        /// 外键表备注占位符
        /// </summary>
        public const string FKTableComment = "{{fk:table:comment}}";
        /// <summary>
        /// 外键字段名称占位符
        /// </summary>
        public const string FKName = "{{fk:col:name}}";
        /// <summary>
        /// 全小写外键字段名称占位符
        /// </summary>
        public const string FKNameLow = "{{fk:col:lname}}";
        /// <summary>
        /// 全大写外键字段名称占位符
        /// </summary>
        public const string FKNameUp = "{{fk:col:uname}}";
        /// <summary>
        /// 首字母小写外键字段名称占位符
        /// </summary>
        public const string FKNameLowFirst = "{{fk:col:lfname}}";
        /// <summary>
        /// 外键字段备注占位符
        /// </summary>
        public const string FKComment = "{{fk:col:comment}}";
        /// <summary>
        /// 外键字段默认值占位符
        /// </summary>
        public const string FKDefaultValue = "{{fk:col:defaultvalue}}";
        /// <summary>
        /// 外键字段有效位数占位符
        /// </summary>
        public const string FKPrecision = "{{fk:col:precision}}";
        /// <summary>
        /// 外键字段有效小数位数占位符
        /// </summary>
        public const string FKScale = "{{fk:col:scale}}";
        /// <summary>
        /// 外键字段最大值占位符
        /// </summary>
        public const string FKMax = "{{fk:col:max}}";
        /// <summary>
        /// 外键字段最小值占位符
        /// </summary>
        public const string FKMin = "{{fk:col:min}}";
        /// <summary>
        /// 外键字段最大长度占位符
        /// </summary>
        public const string FKMaxLength = "{{fk:col:maxlen}}";
        /// <summary>
        /// 外键字段在程序中的数据类型占位符
        /// </summary>
        public const string FKCodeType = "{{fk:col:codetype}}";
        /// <summary>
        /// 外键字段数据库中的数据类型占位符
        /// </summary>
        public const string FKSqlType = "{{fk:col:sqltype}}";
        /// <summary>
        /// 外键在Dal中使用的数据类型占位符
        /// </summary>
        public const string FKDalType = "{{fk:col:daltype}}";
        /// <summary>
        /// 外键字段数据类型转换占位符
        /// </summary>
        public const string FKConvert = "{{fk:col:convert}}{0}{{/fk:col:convert}}";
        /// <summary>
        /// 外键字段转成字符串占位符
        /// </summary>
        public const string FKToString = "{{fk:col:tostring}}{0}{{/fk:col:tostring}}";
        /// <summary>
        /// 外键关联的主表的主键字段名称占位符
        /// </summary>
        public const string FKPKName = "{{fk:pk:col:name}}";
        /// <summary>
        /// 全小写外键关联的主表的主键字段名称占位符
        /// </summary>
        public const string FKPKNameLow = "{{fk:pk:col:lname}}";
        /// <summary>
        /// 全大写外键关联的主表的主键字段名称占位符
        /// </summary>
        public const string FKPKNameUp = "{{fk:pk:col:uname}}";
        /// <summary>
        /// 首字母小写外键关联的主表的主键字段名称占位符
        /// </summary>
        public const string FKPKNameLowFirst = "{{fk:pk:col:lfname}}";
        /// <summary>
        /// 外键关联的主表的主键字段备注占位符
        /// </summary>
        public const string FKPKComment = "{{fk:pk:col:comment}}";
        /// <summary>
        /// 外键关联的主表的主键字段默认值占位符
        /// </summary>
        public const string FKPKDefaultValue = "{{fk:pk:col:defaultvalue}}";
        /// <summary>
        /// 外键关联的主表的主键字段有效位数占位符
        /// </summary>
        public const string FKPKPrecision = "{{fk:pk:col:precision}}";
        /// <summary>
        /// 外键关联的主表的主键字段有效小数位占位符
        /// </summary>
        public const string FKPKScale = "{{fk:pk:col:scale}}";
        /// <summary>
        /// 外键关联的主表的主键字段最大值占位符
        /// </summary>
        public const string FKPKMax = "{{fk:pk:col:max}}";
        /// <summary>
        /// 外键关联的主表的主键字段最小值占位符
        /// </summary>
        public const string FKPKMin = "{{fk:pk:col:min}}";
        /// <summary>
        /// 外键关联的主表的主键字段最大长度占位符
        /// </summary>
        public const string FKPKMaxLength = "{{fk:pk:col:maxlen}}";
        /// <summary>
        /// 外键关联的主表的主键字段程序数据类型占位符
        /// </summary>
        public const string FKPKCodeType = "{{fk:pk:col:codetype}}";
        /// <summary>
        /// 外键关联的主表的主键字段数据库类型占位符
        /// </summary>
        public const string FKPKSqlType = "{{fk:pk:col:sqltype}}";
        /// <summary>
        /// 外键关联的主表的主键字段在DAL中使用的数据类型占位符
        /// </summary>
        public const string FKPKDalType = "{{fk:pk:col:daltype}}";
        /// <summary>
        /// 外键关联的主表的主键字段数据类型转换占位符
        /// </summary>
        public const string FKPKConvert = "{{fk:pk:col:convert}}{0}{{/fk:pk:col:convert}}";
        /// <summary>
        /// 外键关联的主表的主键字段转字符串占位符
        /// </summary>
        public const string FKPKToString = "{{fk:pk:col:tostring}}{0}{{/fk:pk:col:tostring}}";

        #endregion

        #region RK Info

        /// <summary>
        /// 循环所有关联字段占位符
        /// </summary>
        public const string RKLoopSection = "{{loop:rk}}{0}{{/loop:rk}}";
        /// <summary>
        /// 关联字段序号占位符
        /// </summary>
        public const string RKId = "{{rk:col:id}}";
        /// <summary>
        /// 关联字段所在表的表名称占位符
        /// </summary>
        public const string RKTableName = "{{rk:table:name}}";
        /// <summary>
        /// 全大写关联字段所在表的表名称占位符
        /// </summary>
        public const string RKTableNameUp = "{{rk:table:uname}}";
        /// <summary>
        /// 全小写关联字段所在表的表名称占位符
        /// </summary>
        public const string RKTableNameLow = "{{rk:table:lname}}";
        /// <summary>
        /// 首字母小写关联字段所在表的表名称占位符
        /// </summary>
        public const string RKTableNameLowFirst = "{{rk:table:lfname}}";
        /// <summary>
        /// 关联字段所在表的表备注占位符
        /// </summary>
        public const string RKTableComment = "{{rk:table:comment}}";
        /// <summary>
        /// 关联字段名称占位符
        /// </summary>
        public const string RKName = "{{rk:col:name}}";
        /// <summary>
        /// 全小写关联字段名称占位符
        /// </summary>
        public const string RKNameLow = "{{rk:col:lname}}";
        /// <summary>
        /// 全大写关联字段名称占位符
        /// </summary>
        public const string RKNameUp = "{{rk:col:uname}}";
        /// <summary>
        /// 首字母小写关联字段名称占位符
        /// </summary>
        public const string RKNameLowFirst = "{{rk:col:lfname}}";
        /// <summary>
        /// 关联字段备注占位符
        /// </summary>
        public const string RKComment = "{{rk:col:comment}}";
        /// <summary>
        /// 关联字段默认值占位符
        /// </summary>
        public const string RKDefaultValue = "{{rk:col:defaultvalue}}";
        /// <summary>
        /// 关联字段有效位数占位符
        /// </summary>
        public const string RKPrecision = "{{rk:col:precision}}";
        /// <summary>
        /// 关联字段有效小数位数占位符
        /// </summary>
        public const string RKScale = "{{rk:col:scale}}";
        /// <summary>
        /// 关联字段最大值占位符
        /// </summary>
        public const string RKMax = "{{rk:col:max}}";
        /// <summary>
        /// 关联字段最小值占位符
        /// </summary>
        public const string RKMin = "{{rk:col:min}}";
        /// <summary>
        /// 关联字段最大长度占位符
        /// </summary>
        public const string RKMaxLength = "{{rk:col:maxlen}}";
        /// <summary>
        /// 关联字段在程序中的数据类型占位符
        /// </summary>
        public const string RKCodeType = "{{rk:col:codetype}}";
        /// <summary>
        /// 关联字段在数据库中的数据类型占位符
        /// </summary>
        public const string RKSqlType = "{{rk:col:sqltype}}";
        /// <summary>
        /// 关联字段在Dal中的数据类型占位符
        /// </summary>
        public const string RKDalType = "{{rk:col:daltype}}";
        /// <summary>
        /// 关联字段数据类型转换占位符
        /// </summary>
        public const string RKConvert = "{{rk:col:convert}}{0}{{/rk:col:convert}}";
        /// <summary>
        /// 关联字段转字符串占位符
        /// </summary>
        public const string RKToString = "{{rk:col:tostring}}{0}{{/rk:col:tostring}}";
        /// <summary>
        /// 关联字段所在表的主键字段名称占位符
        /// </summary>
        public const string RKPKName = "{{rk:pk:col:name}}";
        /// <summary>
        /// 全小写关联字段所在表的主键字段名称占位符
        /// </summary>
        public const string RKPKNameLow = "{{rk:pk:col:lname}}";
        /// <summary>
        /// 全大写关联字段所在表的主键字段名称占位符
        /// </summary>
        public const string RKPKNameUp = "{{rk:pk:col:uname}}";
        /// <summary>
        /// 首字母小写关联字段所在表的主键字段名称占位符
        /// </summary>
        public const string RKPKNameLowFirst = "{{rk:pk:col:lfname}}";
        /// <summary>
        /// 关联字段所在表的主键字段备注占位符
        /// </summary>
        public const string RKPKComment = "{{rk:pk:col:comment}}";
        /// <summary>
        /// 关联字段所在表的主键字段默认值占位符
        /// </summary>
        public const string RKPKDefaultValue = "{{rk:pk:col:defaultvalue}}";
        /// <summary>
        /// 关联字段所在表的主键字段有效位数占位符
        /// </summary>
        public const string RKPKPrecision = "{{rk:pk:col:precision}}";
        /// <summary>
        /// 关联字段所在表的主键字段有效小数位数占位符
        /// </summary>
        public const string RKPKScale = "{{rk:pk:col:scale}}";
        /// <summary>
        /// 关联字段所在表的主键字段最大值占位符
        /// </summary>
        public const string RKPKMax = "{{rk:pk:col:max}}";
        /// <summary>
        /// 关联字段所在表的主键字段最小值占位符
        /// </summary>
        public const string RKPKMin = "{{rk:pk:col:min}}";
        /// <summary>
        /// 关联字段所在表的主键字段最大长度占位符
        /// </summary>
        public const string RKPKMaxLength = "{{rk:pk:col:maxlen}}";
        /// <summary>
        /// 关联字段所在表的主键字段在程序中的数据类型占位符
        /// </summary>
        public const string RKPKCodeType = "{{rk:pk:col:codetype}}";
        /// <summary>
        /// 关联字段所在表的主键字段在数据库中的数据类型占位符
        /// </summary>
        public const string RKPKSqlType = "{{rk:pk:col:sqltype}}";
        /// <summary>
        /// 关联字段所在表的主键字段在DAL中的数据类型占位符
        /// </summary>
        public const string RKPKDalType = "{{fk:pk:col:daltype}}";
        /// <summary>
        /// 关联字段所在表的主键字段数据类型转换占位符
        /// </summary>
        public const string RKPKConvert = "{{rk:pk:col:convert}}{0}{{/rk:pk:col:convert}}";
        /// <summary>
        /// 关联字段所在表的主键字段转字符串占位符
        /// </summary>
        public const string RKPKToString = "{{rk:pk:col:tostring}}{0}{{/rk:pk:col:tostring}}";

        #endregion

        #region IF Session 判断性占位符

        /// <summary>
        /// 判断性占位符，如果该占位符内部有其他判断性占位符，如果都为false时，整段将被替换为空字符串
        /// </summary>
        public const string IFSection = "{{if:section}}{0}{{/if:section}}";

        #region Table IF

        /// <summary>
        /// 判断性循环表占位符，如果该占位符内部有其他判断性占位符，如果都为false时，整段将被替换为空字符串
        /// </summary>
        public const string TableLoopIFSection = "{{if:loop:table}}{0}{{/if:loop:table}}";

        #region PK IF Section

        /// <summary>
        /// 判断是否是组合主键，如果是组合主键则生成
        /// </summary>
        public const string PKMIFSection = "{{if:pk:many}}{0}{{/if:pk:many}}";
        /// <summary>
        /// 判断是否是单主键，如果是单主键则生成
        /// </summary>
        public const string PKSIFSection = "{{if:pk:one}}{0}{{/if:pk:one}}";

        #endregion

        #region FK IF

        /// <summary>
        /// 判断是否是为多个外键，如果是多个外键则生成
        /// </summary>
        public const string FKMIFSection = "{{if:fk:many}}{0}{{/if:fk:many}}";
        /// <summary>
        /// 判断是否是单外键，如果是单外键则生成
        /// </summary>
        public const string FKSIFSection = "{{if:fk:one}}{0}{{/if:fk:one}}";

        #endregion

        #region RK IF

        /// <summary>
        /// 判断是否为多个关联字段，如果是则生成
        /// </summary>
        public const string RKMIFSection = "{{if:rk:many}}{0}{{/if:rk:many}}";
        /// <summary>
        /// 判断是否为单个关联字段，如果是则生成
        /// </summary>
        public const string RKSIFSection = "{{if:fk:one}}{0}{{/if:fk:one}}";

        #endregion

        #region Column IF

        /// <summary>
        /// 判断性循环字段，如果该占位符内部有其他判断性占位符，如果都为false时，整段将被替换为空字符串
        /// </summary>
        public const string ColumnLoopIFSection = "{{if:loop:col}}{0}{{/if:loop:col}}";

        #region Validate IF Section

        /// <summary>
        /// 判断字段是否为必填
        /// </summary>
        public const string ColumnRequiredIFSection = "{{if:col:required}}{0}{{/if:col:required}}";
        /// <summary>
        /// 判断字段是否判断最大长度
        /// </summary>
        public const string ColumnMaxLenIFSection = "{{if:col:maxlen}}{0}{{/if:col:maxlen}}";
        /// <summary>
        /// 判断字段是否判断最小长度
        /// </summary>
        public const string ColumnMinLenIFSection = "{{if:col:minlen}}{0}{{/if:col:minlen}}";
        /// <summary>
        /// 判断字段是否判断最大值
        /// </summary>
        public const string ColumnMaxIFSection = "{{if:col:max}}{0}{{/if:col:max}}";
        /// <summary>
        /// 判断字段是否判断最小值
        /// </summary>
        public const string ColumnMinIFSection = "{{if:col:min}}{0}{{/if:col:min}}";
        /// <summary>
        /// 判断字段是否判断唯一项
        /// </summary>
        public const string ColumnUniqueIFSection = "{{if:col:unique}}{0}{{/if:col:unique}}";

        #endregion

        #region Data Type IF Section

        /// <summary>
        /// 判断字段是否为字符串类型
        /// </summary>
        public const string ColumnStringIFSection = "{{if:col:string}}{0}{{/if:col:string}}";
        /// <summary>
        /// 判断字段是否为整形类型
        /// </summary>
        public const string ColumnIntegerIFSection = "{{if:col:int}}{0}{{/if:col:int}}";
        /// <summary>
        /// 判断字段是否为数字类型
        /// </summary>
        public const string ColumnNumberIFSection = "{{if:col:number}}{0}{{/if:col:number}}";
        /// <summary>
        /// 判断字段是否为日期时间类型
        /// </summary>
        public const string ColumnDateTimeIFSection = "{{if:col:datetime}}{0}{{/if:col:datetime}}";

        #endregion

        #endregion

        #endregion

        #endregion

        #region Function Placeholder 功能性占位符

        /// <summary>
        /// 从占位符位置向下，替换第一个匹配项
        /// </summary>
        public const string ReplaceFirstSection = "{{rfirst}}{0}{{/rfirst}}";
        /// <summary>
        /// 从占位符位置向上，替换最后一个匹配项
        /// </summary>
        public const string ReplaceLastSection = "{{rlast}}{0}{{/rlast}}";

        #endregion
    }
}
