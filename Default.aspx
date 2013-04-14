<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Login</title>
     <link href="scripts/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">

<div id="logo-wrap">
<div id="logo">
	<h1><a href="#">Basilica Information Management System </a></h1>
</div>
</div>

<!-- start header -->
<div id="header">
	<div id="menu">
		<ul>
			<li><asp:LinkButton ID="LinkLogin" runat="server" CssClass="current_page_item" 
                    OnClick="LinkLogin_Click">Login</asp:LinkButton></li>
			<li><asp:LinkButton ID="LinkAbout" runat="server" OnClick="LinkAbout_Click">About</asp:LinkButton></li>
            <li><asp:LinkButton ID="LinkContact" runat="server" OnClick="LinkContact_Click">Contact</asp:LinkButton></li>
		</ul>
	</div>
</div>
<!-- end header -->
<!-- start page -->
<div id="wrapper">
<div id="wrapper-btm">
<div id="page">
	<!-- start content -->
	<div id="content">
		<div id="banner">
		</div>
		
		<div class="post">
            <asp:Panel ID="PanelLogin" runat="server">
			    <h1 class="title">Login to see your account </h1>
			    <div class="entry">
				    <p><img src="images/EasterBible.gif" alt="" width="140" height="125" class="left" />This is BasilicaSoft, a Parish information management system <a href="#/"></a> To get your loing credentails please see the contact details</p>
			    </div>
             </asp:Panel>
            <asp:Panel ID="PanelContact" runat="server" Visible="false">
			    <h1 class="title">contact us </h1>
			    <div class="entry">
				    <p><img src="images/EasterBible.gif" alt="" width="140" height="125" class="left" />St Mary's church Elthuruth, Phone: 04872362545</p>
			    </div>
             </asp:Panel>
            <asp:Panel ID="PanelAbout" runat="server" Visible="false">
			    <h1 class="title">about this software </h1>
			    <div class="entry">
				    <p><img src="images/EasterBible.gif" alt="" width="140" height="125" class="left" />This is BasilicaSoft, a Parish information management system <a href="#/"></a> To get your loing credentails please see the contact details</p>
			    </div>
             </asp:Panel>
		</div>
	</div>
	<!-- end content -->
	<!-- start sidebar -->
	<div id="sidebar">
		<ul>
			<li id="search">
				<h2>Login</h2>
					<asp:Login ID="Login1" runat="server"
                        OnLoggedIn="Login1_LoggedIn" 
                        BorderStyle="None" ForeColor="White" PasswordLabelText="Password" 
                        RememberMeText="Remember me" UserNameLabelText="Username" 
                        Font-Names="Microsoft Sans Serif" Font-Size="Small">
                    </asp:Login>
			</li>
		</ul>
	</div>
	<!-- end sidebar -->
	<div style="clear: both;">&nbsp;</div>
</div>
<!-- end page -->
</div>
</div>
<!-- start footer -->
<div id="footer">
	<div id="footer-wrap">
	<p id="legal">(c) 2013 . Design by <a href="http://www.freecsstemplates.org/">Anupa T J</a>.</p>
	</div>
</div>
<!-- end footer -->
</form>
</body>
</html>
