<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditPerson.aspx.cs" Inherits="EditPerson" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="padding-left:40%; height:100px">
        <div style ="float :left ">
            <img width="70" height="70" alt="" src="images/edit-person1.png" />
        </div>
        <div style ="float :left; padding-top:10px ">
        <asp:Label ID="LabelHeading" runat="server" CssClass="h2" Font-Names="Times New Roman" Text ="Edit Person"></asp:Label>
        </div>
    </div>
     <asp:Panel ID="PanelEditPerson" runat="server" CssClass="bubble">
    <table style="width:100%;">
            <tr>
                <td>
                    Select Unit:
                </td>
                <td>
                    <asp:DropDownList ID="DropDownUnits" runat="server"
                       AutoPostBack="True" OnSelectedIndexChanged="DropDownUnits_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    Select Family:
                </td>
                <td>
                    <asp:DropDownList ID="DropDownFamily" runat="server"
                         AutoPostBack="True" OnSelectedIndexChanged="DropDownFamily_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    Select Person: 
                </td>
                <td>
                    <asp:DropDownList ID="DropDownPerson" runat="server"
                         AutoPostBack="True" OnSelectedIndexChanged="DropDownPerson_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    <asp:Panel ID="PanelButtons" runat="server">
            <table style="width: 100%; text-align:center">
                <tr>
                    <td> <asp:ImageButton ToolTip="View" ID="ImageButtonView" runat="server" 
                            Height="44px" Width="48px" 
                            ImageUrl="~/images/view.png" OnClick="ImageButtonView_Click" /></td>
                    <td><asp:ImageButton ToolTip="Edit Person" ID="ImageButtonEdit" Height="44px" 
                            Width="48px" runat="server" 
                            ImageUrl="~/images/edit.png" OnClick="ImageButtonEdit_Click"  /></td>
                    <td> <asp:ImageButton ToolTip="Delete" ID="ImageButtonDelete" runat="server" Height="44px" Width="48px" 
                            ImageUrl="~/images/DeleteRed.png"  /></td>
                </tr>
            </table>
        </asp:Panel>
         <asp:Panel ID="PanelViewPerson" runat="server" Visible ="true">
             <table style="width:100%">
                 <tr>
                     <td style="height: 13px; width: 124px;">
                         &nbsp;</td>
                     <td style="height: 13px; width: 237px;">Person Name</td>
                     <td style="height: 13px">
                         <asp:Label ID="LabelName" runat="server" Text=""></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td style="width: 124px">
                         &nbsp;</td>
                     <td style="width: 237px">Family</td>
                     <td>
                         <asp:Label ID="LabelFamily" runat="server"></asp:Label></td>
                 </tr>
                 <tr>
                     <td style="width: 124px">
                         &nbsp;</td>
                     <td style="width: 237px">Sex</td>
                     <td>
                         <asp:Label ID="LabelSex" runat="server"></asp:Label></td>
                 </tr>
                 <tr>
                     <td style="width: 124px">
                         &nbsp;</td>
                     <td style="width: 237px">Date Of Birth</td>
                     <td>
                         <asp:Label ID="LabelDob" runat="server"></asp:Label></td>
                 </tr>
                 <tr>
                     <td style="width: 124px">
                         &nbsp;</td>
                     <td style="width: 237px">Occupation</td>
                     <td>
                         <asp:Label ID="LabelOccupation" runat="server"></asp:Label></td>
                 </tr>
                 <tr>
                     <td style="width: 124px">
                         &nbsp;</td>
                     <td style="width: 237px">Baptism Date</td>
                     <td>
                         <asp:Label ID="LabelBaptism" runat="server"></asp:Label></td>
                 </tr>
                 <tr>
                     <td style="width: 124px">
                         &nbsp;</td>
                     <td style="width: 237px">Marital Status</td>
                     <td>
                         <asp:Label ID="LabelMarital" runat="server"></asp:Label></td>
                 </tr>
             </table>
         </asp:Panel> 
         <asp:Panel ID="PanelEditPersonDetails" runat="server" Visible ="false" >
             <table style="width:100%;">
                 <tr>
                     <td style="width: 181px">Person Name*</td>
                     <td>
                         <asp:TextBox ID="TextName" runat="server" Width="263px"></asp:TextBox>
                     </td>
                 </tr>
                 <tr>
                     <td style="width: 181px">Unit*</td>
                     <td>
                         <asp:DropDownList ID="DropUnitName" runat="server" 
                             onselectedindexchanged="DropUnitName_SelectedIndexChanged" 
                             AutoPostBack="True">
                         </asp:DropDownList>
                     </td>
                 </tr>
                 <tr>
                     <td style="width: 181px">Family*</td>
                     <td>
                         <asp:DropDownList ID="DropDownFamilyName" runat="server">
                         </asp:DropDownList>
                     </td>
                 </tr>
                 <tr>
                     <td style="width: 181px">Sex*</td>
                     <td>
                         <asp:DropDownList ID="DropDownSex" runat="server">
                             <asp:ListItem>Male</asp:ListItem>
                             <asp:ListItem>Female</asp:ListItem>
                         </asp:DropDownList>
                     </td>
                 </tr>
                 <tr>
                     <td style="width: 181px">Date of Birth</td>
                     <td>
                         <asp:DropDownList ID="DropDOBYr" runat="server">
                         </asp:DropDownList>
                         <asp:DropDownList ID="DropDOBMnth" runat="server">
                         </asp:DropDownList>
                         <asp:DropDownList ID="DropDOBDay" runat="server">
                         </asp:DropDownList>
                     </td>
                 </tr>
                 <tr>
                     <td style="width: 181px">Occupation</td>
                     <td>
                         <asp:TextBox ID="TextOccupation" runat="server" Width="263px"></asp:TextBox>
                     </td>
                 </tr>
                 <tr>
                     <td style="width: 181px">Baptism Date</td>
                     <td>
                         <asp:DropDownList ID="DropDownBtsmYr" runat="server">
                         </asp:DropDownList>
                         <asp:DropDownList ID="DropDownBtsmMnth" runat="server">
                         </asp:DropDownList>
                         <asp:DropDownList ID="DropDownBtsmDay" runat="server">
                         </asp:DropDownList>
                     </td>
                 </tr>
                 <tr>
                     <td style="width: 181px">Marital Status*</td>
                     <td>
                         <asp:DropDownList ID="DropDownMaritalStatus" runat="server">
                             <asp:ListItem>Single</asp:ListItem>
                             <asp:ListItem>Married</asp:ListItem>
                             <asp:ListItem>Widow</asp:ListItem>
                         </asp:DropDownList>
                     </td>
                 </tr>
                 <tr>
                     <td style="width: 181px">&nbsp;</td>
                     <td>&nbsp;</td>
                 </tr>
                 <tr>
                     <td style="width: 181px">&nbsp;</td>
                     <td>
                         <asp:Button ID="Button1" runat="server" Text="Update" />
                     </td>
                 </tr>
             </table>
         </asp:Panel> 
    </asp:Panel>
</asp:Content>

