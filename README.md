## Table of contents
- [Introduce](#introduce)
- [Quick start](#quick-start)
- [Documentation](#documentation)
- [Lisence](#lisence)

## Introduce

Generate the project code according to the database table structure.  
If the template is good, the generated code can run, and complete the basic function for CRUD.

## Quick start

- Build the project, and run GenCodeTool.exe.
- Connect DB.
- Choose program language type.
- Choose template.
- Fill project information.
- Then you can generate all code.

## Documentation
**Code Template**  
Template files must put in CodeTemplate folder, a group template files must put in one folder. One folder one type. You can choose the type in program.

**Multi-database support**  
Now support MS SQL Server and Mysql. Want to suport another DB, you can build a class that inherits from the IDbInfoGetter interface, and regist it in 'DbInfoGetterFactory'.

**Multi-code support**  
Now support C#. If you want to support Java or another language, you can build a class that inherits from the ICodeInfoGetter interface,  and regist it in 'CodeInfoGetterFactory'.

More detail information, please read help document.

## Lisence
