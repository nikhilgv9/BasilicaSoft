<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateRequest.aspx.cs" Inherits="CreateRequest" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="padding-left:30%; height:100px">
        <div style ="float :left ">
            <img width="70" height="70" alt="" src="images/request1.png" />
        </div>
        <div style ="float :left; padding-top:10px ">
           <h2 style="font-family: 'Times New Roman';">Request Resources</h2>
        </div>
    </div>
    <div class="bubble">
    <span class="h4" >Choose Category</span>
        <table width="100%" style=" text-align:center;">
            <tr>
                <td>
                    <asp:ImageButton ID="Image1" ToolTip ="Create Physical Resources" runat="server" ImageUrl="images/parish-hall.png" Width="136px" OnClick="Image1_Click" />
                    
                </td>
                <td>
                    
                    <asp:ImageButton ID="Image2" ToolTip ="Create New Date" runat="server" ImageUrl="images/calendar-icon.png" Width="136px" OnClick="Image2_Click" />
                </td>
            </tr>
        </table>
        <table width="100%" cellpadding="5";>
        <tr>
                <td>
                    Resource Type*
                </td>
                <td>
                    <asp:TextBox ID="TextRequest" runat="server" Height="24px" Width="232px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td> Select Resource*</td>
                <td>
                    <asp:DropDownList ID="DropDownResource" runat="server">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    Booking Date*</td>
                <td>
                    <asp:DropDownList ID="DropDownYear" runat="server" OnSelectedIndexChanged="DropDownYear_SelectedIndexChanged" AutoPostBack ="true">
                    </asp:DropDownList>
                    <asp:DropDownList ID="DropDownMonth" runat="server" OnSelectedIndexChanged="DropDownMonth_SelectedIndexChanged" AutoPostBack ="true">
                    </asp:DropDownList>
                    <asp:DropDownList ID="DropDownDay" runat="server">
                    </asp:DropDownList>
                    <asp:Button ID="ButtonAvailability" runat="server" Text="Check Availability" OnClick="ButtonAvailability_Click" /></td>
            </tr>
            <tr>
                <td style="height: 18px">
                    Description</td>
                <td style="height: 18px">
                    <asp:TextBox ID="TextDescription" runat="server" TextMode="MultiLine" Height="112px" Width="400px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Label ID="LabelMessage" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="ButtonRequest" runat="server" Text="Request" OnClick="ButtonRequest_Click" />
                </td>
            </tr>
        </table>
        
        
    </div>
</asp:Content>

