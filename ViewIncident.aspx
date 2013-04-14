<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ViewIncident.aspx.cs" Inherits="ViewIncident" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div style="padding-left:30%; height:100px">
        <div style ="float :left ">
            <img width="100" height="100" alt="" src="images/incidents_png.png" />
        </div>
</div> 
<div style="height: 40px;">
    <div style="float:right">
        <asp:LinkButton ID="LinkButtonNext" runat="server" OnClick="LinkButtonNext_Click">Next&lt;</asp:LinkButton>
        <asp:Label ID="LabelRowId" runat="server" Text="1"></asp:Label>
        <asp:LinkButton ID="LinkButtonPrevious" runat="server" OnClick="LinkButtonPrevious_Click">&gt;Previous</asp:LinkButton>
    </div>
</div>
<div class="bubble">
            <table width="100%" cellpadding="5";>
            <tr>
            <td ></td>
                    <td style="width:681px">
                        Incident
                    </td>
                    <td>
                    <asp:Label ID="LabelName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                <td ></td>
                    <td style="width: 681px"> 
                    Date
                    </td>
                    <td>
                    <asp:Label ID="LabelDate" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                <td ></td>
                    <td style="width: 681px"> 
                    Family
                    </td>
                    <td>
                    <asp:Label ID="LabelFamily" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
     <div style="margin:0 auto; width: 400px">
    <asp:Image ID="Image1" runat="server" Width="400" />
    </div>
</div> 
</asp:Content>

