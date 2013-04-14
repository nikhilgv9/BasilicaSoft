<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FamilyRegistration.aspx.cs" Inherits="FamilyRegistration" Title="Register Family" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="padding-left:30%; height:100px">
        <div style ="float :left ">
            <img width="70px" height="70px" alt="" src="images/famlylogo.png" />
        </div>
        <div style ="float :left "><h2 style="font-family: 'Times New Roman';">Enter Family Details</h2></div>
    </div>
    <div class="bubble">
        <table style="width:100%;" cellspacing="5px">
            <tr>
                <td>Family Name*</td>
                <td>
                    <asp:TextBox ID="TextFamilyName" runat="server" Width="220px" OnTextChanged="TextFamilyName_Change" AutoPostBack="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>User Name*</td>
                <td>
                    <div style="float:left"><asp:TextBox ID="TextUserName" runat="server" 
                            Width="222px"></asp:TextBox></div>
                    <div style="width:18px; float:left"></div>
                    <div style="float:left"><asp:Button ID="ButtonCheckAvailablity" runat="server" 
                            Text="Check Availablity" OnClick="ButtonCheckAvailablity_Click" /></div>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="LabelAvailability" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Unit Name*</td>
                <td>
                    <div style="float:left">
                        <asp:TextBox ID="TextUnitName" runat="server" Width="243px" Visible="false"></asp:TextBox>
                        <asp:DropDownList ID="DropDownUnitName" runat="server"  Width="273px" AutoPostBack="True"></asp:DropDownList>
                    </div>
                    <div style="width:18px; float:left"></div>
                    <div style="width:18px; float:left">
                        <asp:Button ID="ButtonNewUnit" runat="server" Text="New Unit" OnClick="ButtonNewUnit_Click" />
                    </div>
                     
                </td>
            </tr>
            <tr>
                <td>Address</td>
                <td>
                    <asp:TextBox ID="TextAddress" runat="server" Height="87px" TextMode="MultiLine" 
                        Width="400px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Phone</td>
                <td>
                    <asp:TextBox ID="TextPhone" runat="server" Width="400px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="Submit" runat="server" Text="Submit" Width="92px" 
                        OnClick="Submit_Click" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="LabelMessage" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </asp:Content>

