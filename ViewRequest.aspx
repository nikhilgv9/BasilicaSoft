<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ViewRequest.aspx.cs" Inherits="ViewRequest" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="padding-left:30%; height:100px">
        <div style ="float :left ">
            <img width="70" height="70" alt="" src="images/viewrequest.png" />
        </div>
        <div style ="float :left; padding-top:10px ">
           <h2 style="font-family: 'Times New Roman';">View Your Request</h2>
        </div>
    </div>
    <div class="bubble">
        <asp:Table CssClass="aspTable" ID="Table1" runat="server">
        <asp:TableHeaderRow Height="40px" BackColor="AliceBlue" ForeColor="#6161A7">
            <asp:TableHeaderCell>Family Name</asp:TableHeaderCell>
            <asp:TableHeaderCell>Resource</asp:TableHeaderCell>
            <asp:TableHeaderCell>Date</asp:TableHeaderCell>
            <asp:TableHeaderCell>Status</asp:TableHeaderCell>
        </asp:TableHeaderRow>
        </asp:Table>
    </div> 
</asp:Content>

