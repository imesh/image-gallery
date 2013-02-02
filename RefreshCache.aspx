<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/imagegallery.master" CodeFile="RefreshCache.aspx.cs" Inherits="RefreshCache" Theme="Default" %>
<%@ Register TagPrefix="uc" TagName="CacheManager" Src="~/UserControls/CacheManager.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
<uc:CacheManager runat="server"></uc:CacheManager>
</asp:Content>
