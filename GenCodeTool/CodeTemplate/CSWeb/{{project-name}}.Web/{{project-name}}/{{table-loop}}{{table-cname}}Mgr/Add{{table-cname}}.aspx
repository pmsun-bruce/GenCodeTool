<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add{{table:cname}}.aspx.cs" Inherits="{{project:namespace}}.Web.{{project:name}}.Add{{table:cname}}" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Resource/JS/{{table:cname}}Mgr/Add{{table:cname}}.js"></script>
</head>
<body>
    <form id="Add{{table:cname}}Frm" runat="server">
        <table>
        {{loop:col|ignparam:pk,fk,CreateTime,UpdateTime,CreaterId,UpdatorId}}
        <tr>
            <td>
                <%={{table:cname}}Resource.{{col:pname}} %>
            </td>
            <td>{{if:col:string}}
                <input type="text" id="{{col:pname}}Txt" name="{{col:pname}}Txt" value="" nt="string" />{{/if:col:string}}{{if:col:datetime}}
                <input type="text" id="{{col:pname}}Txt" name="{{col:pname}}Txt" value="" nt="date" />{{/if:col:datetime}}{{if:col:int}}
                <input type="text" id="{{col:pname}}Txt" name="{{col:pname}}Txt" value="" nt="int" />{{/if:col:int}}{{if:col:number}}
                <input type="text" id="{{col:pname}}Txt" name="{{col:pname}}Txt" value="" nt="number" />{{/if:col:number}}
            </td>
        </tr>{{/loop:col}}
        {{loop:fk}}
        <tr>
            <td>
                <%={{fk:table:cname}}Resource.{{fk:table:cname}} %>
            </td>
            <td>
                <select id="{{fk:table:cname}}Sel" name="{{fk:table:cname}}Sel"></select>
            </td>
        </tr>
        {{/loop:fk}}
        </table>
    </form>
</body>
</html>
