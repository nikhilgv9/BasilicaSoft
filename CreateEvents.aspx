<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateEvents.aspx.cs" Inherits="CreateEvents" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="padding-left:30%; height:100px">   
        <div style="height:100px">
        <div style ="float :left ">
            <img alt="" src="images/Event_png.png" style="width: 96px; height: 56px" />
        </div>
        <div style ="float :left; padding-top:10px ">
           <h2 style="font-family: 'Times New Roman';">Create Event</h2>
        </div>
    </div>
</div>
    
        <asp:Panel ID="Panel1" runat="server">
        <div class="bubble">
            <table width="100%" cellpadding="5";>
            <tr>
                    <td>
                        Name of the Event*
                    </td>
                    <td>
                        <asp:TextBox ID="TextName" runat="server" Height="24px" Width="232px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Place</td>
                    <td>
                        <asp:TextBox ID="TextPlace" runat="server" Height="24px" Width="232px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Date</td>
                    <td>
                        <asp:DropDownList ID="DropDownYear" runat="server" OnSelectedIndexChanged="DropDownYear_SelectedIndexChanged" AutoPostBack ="true">
                        </asp:DropDownList>
                        <asp:DropDownList ID="DropDownMonth" runat="server" OnSelectedIndexChanged="DropDownMonth_SelectedIndexChanged" AutoPostBack ="true">
                        </asp:DropDownList>
                        <asp:DropDownList ID="DropDownDay" runat="server">
                        </asp:DropDownList>
                     </td>
                </tr>
                <tr>
                    <td style="height: 18px">
                        Description</td>
                    <td style="height: 18px">
                        <asp:TextBox ID="TextDescription" runat="server" TextMode="MultiLine" Height="40px" Width="400px"></asp:TextBox></td>
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
                        <asp:Button ID="ButtonAddPhotos" runat="server" Text="Add Photos" OnClick="ButtonAddPhotos_Click"  />
                    </td>
                </tr>
            </table>
            </div>
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server" Visible="false">
        <div class="bubble">
            <table>
            <tr>
                <td>Event Name
                </td>
                <td>
                    <asp:Label ID="LabelEventName" runat="server" Text=""></asp:Label>
                </td>
               
            </tr>
            <tr>
                <td>Choose Photo
                </td>
                <td>
                    <asp:FileUpload ID="FileUploadImage" runat="server" />
                </td> 
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Label ID="LabelMessage2" runat="server" Text=""></asp:Label></td>
               
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="ButtonAddImage" runat="server" Text="Add Image" OnClick="ButtonAddImage_Click" />
                    <asp:Button ID="ButtonCreateNewEvent" runat="server" Text="Create a New Event" OnClick="ButtonCreateNewEvent_Click" />
                </td>
               
            </tr>
        </table>
        </div>
        </asp:Panel>
  
</asp:Content>

