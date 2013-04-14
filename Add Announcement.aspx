<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Add Announcement.aspx.cs" Inherits="AddAnnouncement" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="padding-left:30%; height:100px">
        <div style ="float :left ">
            <img width="70" height="70" alt="" src="images/announcements.png" />
        </div>
        <div style ="float :left "><h2 style="font-family: 'Times New Roman';"> Add Announcements</h2></div>
    </div>
    <asp:Panel ID="PanelAnnouncements" runat="server" CssClass="bubble">
        <span class="h4">Choose Catagory</span>
        <br />
        <table width="100%">
            <tr>
                <td style="text-align: center">
                    <asp:ImageButton ToolTip="Add Prayer Info" ID="ImageButtonPrayer" 
                        runat="server" Height="100px" Width="100px" 
                        ImageUrl="images/holy_mass.png" OnClick="ImageButtonPrayer_Click" 
                     />
                </td>
                <td style="text-align: center">
                    <asp:ImageButton ToolTip =" Add Meetings Info" ID="ImageButtonMeeting" 
                        runat="server" Height="100px" Width="100px" ImageUrl="images/meeting-hi.png" 
                        OnClick="ImageButtonMeeting_Click" />
                </td>
                <td style="text-align: center">
                    <asp:ImageButton ToolTip =" Add Cathesium info" ID="ImageButtonCatheism" 
                        runat="server" Height="100px" Width="100px" ImageUrl="images/catheism.png" 
                        OnClick="ImageButtonCatheism_Click" />
                </td>
                <td style="text-align: center">
                    <asp:ImageButton ToolTip ="Add Other info " ID="ImageButtonOther" 
                        runat="server" Height="100px" Width="100px" ImageUrl="images/others.png" 
                        OnClick="ImageButtonOther_Click"/>
                </td>
            </tr>
        </table>
        <br />
        <asp:Panel ID="PanelAnnouncementDetails" runat="server" Width="100%" Visible="false">

            <table style="width:100%;">
                <tr>
                    <td style="width: 233px">Type*</td>
                    <td>
                        <asp:TextBox ID="TextType" runat="server" ReadOnly="True" Width="215px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 233px">Title*</td>
                    <td>
                        <asp:TextBox ID="TextTitle" runat="server" Width="390px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 233px">Message</td>
                    <td>
                        <asp:TextBox ID="TextMessage" runat="server" Height="110px" TextMode="MultiLine" 
                            Width="389px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 233px">Valid for next</td>
                    <td>
                        <asp:TextBox ID="TextValidity" runat="server" Width="72px"></asp:TextBox>
                        &nbsp;days
                        <asp:RangeValidator ID="RangeValidator1" runat="server"  MinimumValue="0" MaximumValue="1000" ControlToValidate="TextValidity"
                            ErrorMessage="Should be an integer less than 1000" Type="Integer"></asp:RangeValidator>
                    </td>

                </tr>
                <tr>
                    <td style="width: 233px">&nbsp;</td>
                    <td>
                        <asp:Label ID="LabelMessage" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 233px">&nbsp;</td>
                    <td>
                        <asp:Button ID="ButtonPost" runat="server" Text="Post" 
                            OnClick="ButtonPost_Click" />
                    </td>
                </tr>
            </table>

        </asp:Panel>
    </asp:Panel>
</asp:Content>


