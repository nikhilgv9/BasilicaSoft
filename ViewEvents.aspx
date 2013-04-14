<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ViewEvents.aspx.cs" Inherits="ViewEvents" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:Panel CssClass="fullScreenDiv" Visible="false" ID="PanelImage" runat="server">
<div>
    <div >
    <div style="float:right">
        <asp:ImageButton Width="30" Height="30" ID="ImageButtonClose" ImageUrl="images/DeleteRed.png" runat="server" OnClick="ImageButtonClose_Click" />
    </div>
    </div>
    <div class="imagePanel">
        <asp:Image Width="900" ID="Image1" runat="server" />
    </div> 
</div>
</asp:Panel>

<div style="padding-left:30%; height:100px">
        <div style ="float :left ">
            <img height="100" alt="" src="images/Events_png.png" style="width: 176px" />
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
                        <asp:Label ID="LabelName" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LabelDate" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                <td ></td>
                    <td style="width: 681px"> 
                    <asp:Label ID="LabelPlace" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            
            <asp:Table CellPadding="3" ID="TableImages" runat="server" GridLines="Both">
            </asp:Table>
</div>   
</asp:Content>

