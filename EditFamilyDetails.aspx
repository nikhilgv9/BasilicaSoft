<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditFamilyDetails.aspx.cs" Inherits="EditFamilyDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="padding-left:40%; height:100px">
        <div style ="float :left ">
            <img width="70px" height="70px" alt="" src="images/famlylogo.png" />
        </div>
        <div style ="float :left; padding-top:10px ">
        <asp:Label ID="LabelHeading" runat="server" CssClass="h2" Font-Names="Times New Roman"></asp:Label>
        </div>
    </div>
    <asp:Panel ID="PanelEditFamily" runat="server" CssClass="bubble">

        <table style="width:100%;">
            <tr>
                <td>
                    <asp:Label ID="LabelUnit" runat="server" Text="Select Unit: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownUnits" runat="server"
                        OnSelectedIndexChanged="DropDownUnits_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="LabelFamily" runat="server" Text="Select Family: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownFamily" runat="server"
                        OnSelectedIndexChanged="DropDownFamily_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <asp:Panel ID="PanelButtons" runat="server">
            <table style="width: 100%; text-align:center">
                <tr>
                    <td><asp:ImageButton ToolTip="Add Person" ID="ImageButtonAdd" runat="server" Height="44px" Width="48px"
                            ImageUrl="~/images/add_person.png" OnClick="ImageButtonAdd_Click"  /></td>
                    <td><asp:ImageButton ToolTip="Edit Family" ID="ImageButtonEdit" Height="44px" Width="48px" runat="server" 
                            ImageUrl="~/images/edit.png" OnClick="ImageButtonEdit_Click" /></td>
                    <td> <asp:ImageButton ToolTip="View" ID="ImageButtonView" runat="server" Height="44px" Width="48px" 
                            ImageUrl="~/images/view.png" OnClick="ImageButtonView_Click" /></td>
                    <td> <asp:ImageButton ToolTip="Delete" ID="ImageButtonDelete" runat="server" Height="44px" Width="48px" 
                            ImageUrl="~/images/DeleteRed.png" OnClick="ImageButtonDelete_Click" /></td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <asp:Panel ID="PanelPersonDetails" runat="server" Width="100%" Visible="false">
            <table style="width: 100%;" cellpadding="5px">
                <tr>
                    <td style="width: 236px">Name*</td>
                    <td>
                        <asp:TextBox ID="TextPersonName" runat="server" Width="289px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 236px">Sex*</td>
                    <td>
                        <asp:DropDownList ID="DropDownPersonSex" runat="server">
                        <asp:ListItem>Male</asp:ListItem>
                        <asp:ListItem>Female</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 236px">Date of Birth*</td>
                    <td>
                        <asp:DropDownList ID="DropDownDOBYr" runat="server"
                            OnSelectedIndexChanged="DropDownDOBYr_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:DropDownList ID="DropDownDOBMonth" runat="server"
                            OnSelectedIndexChanged="DropDownDOBMonth_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:DropDownList ID="DropDownDOBDay" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 236px">Occupation</td>
                    <td>
                        <asp:TextBox ID="TextPersonOccupation" runat="server" Width="289px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 236px">Date of Baptism</td>
                    <td>
                        <asp:DropDownList ID="DropDownBtsmYr" runat="server" 
                            OnSelectedIndexChanged="DropDownBtsmYr_SelectedIndexChanged" 
                            AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:DropDownList ID="DropDownBtsmMonth" runat="server" AutoPostBack="True" 
                            OnSelectedIndexChanged="DropDownBtsmMonth_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="DropDownBtsmDay" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 236px">Marital Status*</td>
                    <td>
                        <asp:DropDownList ID="DropDownMaritalStatus" runat="server">
                        <asp:ListItem>Single</asp:ListItem>
                        <asp:ListItem>Married</asp:ListItem>
                        <asp:ListItem>Widow</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 236px">&nbsp;</td>
                    <td>&nbsp;<asp:Label ID="LabelInsertMessage" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 236px">&nbsp;</td>
                    <td>
                        <asp:Button ID="ButtonAddPerson" runat="server" 
                            Text="Add Person to Family" OnClick="ButtonAddPerson_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>

         <asp:Panel ID="PanelEditFamilyDetails" runat="server" Width="100%" Visible="false">
             <table style="width: 100%;">
                 <tr>
                     <td style="width: 244px">Family Name*</td>
                     <td>
                         <asp:TextBox ID="TextFamilyName" runat="server" Width="289px"></asp:TextBox>
                     </td>
                 </tr>
                 <tr>
                     <td style="width: 244px">Unit Name*</td>
                     <td>
                         <asp:DropDownList ID="DropDownUnitNames" runat="server">
                         </asp:DropDownList>
                     </td>
                 </tr>
                 <tr>
                     <td style="width: 244px">Address</td>
                     <td>
                         <asp:TextBox ID="TextFamilyAddress" runat="server" Height="80px" TextMode="MultiLine" 
                             Width="290px"></asp:TextBox>
                     </td>
                 </tr>
                 <tr>
                     <td style="width: 244px">Phone</td>
                     <td>
                         <asp:TextBox ID="TextPhone" runat="server" Width="289px"></asp:TextBox>
                     </td>
                 </tr>
                 <tr>
                     <td style="width: 244px">&nbsp;</td>
                     <td>
                         <asp:Label ID="LabelUpdateMessage" runat="server" Text=""></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td style="width: 244px">&nbsp;</td>
                     <td>
                         <asp:Button ID="ButtonUpdateFamily" runat="server" Text="Update Family" 
                             OnClick="ButtonUpdateFamily_Click"  />
                     </td>
                 </tr>
             </table>
          </asp:Panel>


         <asp:Panel ID="PanelViewFamilyDetails" runat="server" Width="100%" Visible="true">
             <table style="width: 100%;">
                 <tr>
                     <td style="width: 119px; text-align: right;">&nbsp;</td>
                     <td style="width: 225px; text-align: left;">Family Name</td>
                     <td>
                         <asp:Label ID="LabelFamilyName" runat="server" style="margin-left: 0px" 
                             Width="289px"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td style="width: 119px; text-align: right;">&nbsp;</td>
                     <td style="width: 225px; text-align: left;">Unit Name</td>
                     <td>
                         <asp:Label ID="LabelUnitName" runat="server" Width="289px"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td style="width: 119px; vertical-align: top; text-align: right;">&nbsp;</td>
                     <td style="width: 225px; vertical-align: top; text-align: left;">Address</td>
                     <td>
                         <asp:TextBox ID="LabelAddress" runat="server" CssClass="readOnlyTextArea" 
                             Height="80px" TextMode="MultiLine" Width="290px"></asp:TextBox>
                     </td>
                 </tr>
                 <tr>
                     <td style="width: 119px; text-align: right;">&nbsp;</td>
                     <td style="width: 225px; text-align: left;">Phone</td>
                     <td>
                         <asp:Label ID="LabelPhone" runat="server" Width="289px"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td style="width: 119px; text-align: right;">&nbsp;</td>
                     <td style="width: 225px; text-align: left;">Number of Members</td>
                     <td>
                         <asp:Label ID="LabelNoMembers" runat="server" Width="289px"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td style="width: 119px; text-align: right;">&nbsp;</td>
                     <td style="width: 225px; text-align: left;">Members</td>
                     <td>
                         <asp:Panel ID="PanelFamilyMembers" runat="server">
                         </asp:Panel>
                     </td>
                 </tr>
             </table>
          </asp:Panel>
    </asp:Panel>
</asp:Content>

