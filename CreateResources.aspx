<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateResources.aspx.cs" Inherits="CreateResources" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="padding-left:30%; height:100px">
        <div style ="float :left ">
            <img width="70" height="70" alt="" src="images/create-resources.png" />
        </div>
        <div style ="float :left; padding-top:10px ">
           <h2 style="font-family: 'Times New Roman';">Create Resources</h2>
        </div>
</div>
    <div class="bubble">
    <span class="h4" >Choose Category</span>
        <table width="100%" style=" text-align:center;">
            <tr>
                <td>
                    <asp:ImageButton ID="Image1" ToolTip ="Create Physical Resources" runat="server" ImageUrl="images/two_storied_house.png" Width="136px" OnClick="Image1_Click" />
                    
                </td>
                <td>
                    
                    <asp:ImageButton ID="Image2" ToolTip ="Create New Date" runat="server" ImageUrl="images/bookingdate.png" Width="136px" OnClick="Image2_Click" />
                </td>
            </tr>
        </table>
        <table width="100%" cellpadding="5";>
        <tr>
                <td>
                    Resource Type*
                </td>
                <td>
                    <asp:TextBox ID="TextType" runat="server" Width="232px" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td> Resource Name*</td>
                <td>
                    <asp:TextBox ID="TextResourceName" runat="server"></asp:TextBox>
                 </td>
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
                    <asp:Label ID="LabelMessage" runat="server" ></asp:Label></td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="ButtonCreate" runat="server" Text="Create" OnClick="ButtonCreate_Click" />
                </td>
            </tr>
        </table>
        
        
    </div>
</asp:Content>

