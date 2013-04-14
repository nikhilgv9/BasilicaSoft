<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Announcements.aspx.cs" Inherits="Announcements" Title="Announcements" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="padding-left:30%; height:100px">
        <div style ="float :left ">
            <img width="70" height="70" alt="" src="images/announcements.png" />
        </div>
        <div style ="float :left "><h2 style="font-family: 'Times New Roman';">Announcements</h2></div>
    </div>
    <asp:Panel ID="PanelAnnouncements" runat="server" CssClass="bubble">
        <table width="100%">
            <tr>
                <td style="text-align: center">
                    <asp:ImageButton ID="ImageButtonPrayers" runat="server" Height="100px" Width="100px" ImageUrl="images/holy_mass.png" OnClick="ImageButtonPrayers_Click" />
                </td>
                <td style="text-align: center">
                    <asp:ImageButton ID="ImageButtonMeetings" runat="server" Height="100px" Width="100px" ImageUrl="images/meeting-hi.png" OnClick="ImageButtonMeetings_Click" />
                </td>
                <td style="text-align: center">
                    <asp:ImageButton ID="ImageButtonCatheism" runat="server" Height="100px" Width="100px" ImageUrl="images/catheism.png" OnClick="ImageButtonCatheism_Click"/>
                </td>
                <td style="text-align: center">
                    <asp:ImageButton ID="ImageButtonOthers" runat="server" Height="100px" Width="100px" ImageUrl="images/others.png" OnClick="ImageButtonOthers_Click"/>
                </td>
            </tr>
        </table>
        <br />
         <asp:Label ID="LabelType" runat="server" Font-Size="Large"></asp:Label>
         <!--spacer --><div style="height:10px"></div>
        <asp:Panel ID="PanelAnnouncementDetails" runat="server" Width="100%">
        </asp:Panel>
    </asp:Panel>
</asp:Content>

