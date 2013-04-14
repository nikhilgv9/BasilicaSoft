<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddIncidents.aspx.cs" Inherits="AddIncidents" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="padding-left:30%; height:100px">   
        <div style="height:100px">
        <div style ="float :left ">
            <img width="70" height="70" alt="" src="images/incident1.png" />
        </div>
        <div style ="float :left; padding-top:10px ">
           <h2 style="font-family: 'Times New Roman';">Add Incidents</h2>
        </div>
    </div>
</div>
    
        <asp:Panel ID="Panel1" runat="server">
        <span class="h4">Choose Catagory</span>
        <br />
        <table width="100%">
            <tr>
                <td style="text-align: center">
                    <asp:ImageButton ToolTip="Death" ID="ImageButtonDeath" 
                        runat="server" Height="100px" Width="100px" 
                        ImageUrl="images/Funeral 02.jpg" OnClick="ImageButtonDeath_Click" 
                     />
                </td>
                <td style="text-align: center">
                    <asp:ImageButton ToolTip =" Wedding" ID="ImageButtonWedding" 
                        runat="server" Height="100px" Width="100px" ImageUrl="images/famly 2.jpg" OnClick="ImageButtonWedding_Click" />
                </td>
                <td style="text-align: center">
                    <asp:ImageButton ToolTip =" Birth" ID="ImageButtonBirth" 
                        runat="server" Height="100px" Width="100px" ImageUrl="images/cake.jpg" OnClick="ImageButtonBirth_Click" />
                </td>
            </tr>
        </table>
        <br />
        
        
        <div class="bubble">
            <table width="100%" cellpadding="5";>
            <tr>
                    <td> Incident</td>
                    <td>
                        <asp:TextBox ID="TextIncidentType" Text="Death" runat="server" Height="24px" Width="232px" ReadOnly ="true" ></asp:TextBox>
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
                        <asp:Label ID="LabelMessage" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="ButtonAddIncident" runat="server" Text="Add" OnClick="ButtonAddIncident_Click" />
                        </td>
                </tr>
            </table>
            </div>
        </asp:Panel>
     
  
</asp:Content>



