<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" Title="Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		    
<div style="padding-left:30%; height:100px">
<div style ="float :left ">
<img width="70" height="70" alt="" src="images/annoncement1.png" />
</div>
<div style ="float :left; padding-top:10px ">
           <h2 style="font-family: 'Times New Roman';">Notification</h2>
</div>
</div>

    <asp:Panel ID="PanelAnnouncementNotification" CssClass="bubble" runat="server">
    </asp:Panel>
</asp:Content>

