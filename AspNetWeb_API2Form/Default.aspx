<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AspNetWeb_API2Form._Default" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript" src="Scripts/jquery-3.4.1.js"></script>
    <script type="text/javascript" src="Scripts/Userdefine/indexload.js"></script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>ASP.NET</h1>
        <p class="lead">ASP.NET是一个免费的web框架，用于使用HTML、CSS和JavaScript构建效果极佳的网站和web应用程序。</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>
    <div class="jumbotron">
        <h2>产品列表</h2>
        <table class="table">
            <thead>
                <tr>
                    <th>名称</th>
                    <th>价格</th>
                </tr>
            </thead>
            <tbody id="products"></tbody>
        </table>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>开始学习</h2>
            <p>
                ASP.NET WebAPI是一个能够使访问HTTP服务变得容易框架，它可以构建广泛的客户端，
                包括浏览器和移动设备。ASP.NET Web API 是构建RESTful应用程序的理想平台
            </p>
            <p><a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301870">详细信息 &raquo;</a></p>
        </div>
        <div class="col-md-4">
            <h2>NuGet库管理工具</h2>
            <p>NuGet是一个免费的Visual Studio扩展，可以轻松地添加、删除和更新Visual Studio项目中的库和工具。</p>
            <p><a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301871">详细信息 &raquo;</a></p>
        </div>
        <div class="col-md-4">
            <h2>Web服务器</h2>
            <p>你可以很容易地找到一家为你的应用程序提供正确的功能和价格组合的网络托管公司.</p>
            <p><a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301872">详细信息 &raquo;</a></p>
        </div>
    </div>

</asp:Content>
