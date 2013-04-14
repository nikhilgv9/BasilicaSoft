using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;

/* Default.aspx.cs
 * This is the C# class corresponding to the Login page of this project.
 * Default.aspx acts as the Index page for the application
 * It contains code segments to login and redirect users
 * The login check is performed with the help of security features provided by ASP .NET so,
 * So no code wont be found in this class to check the username and password validity
 */

public partial class _Default : System.Web.UI.Page 
{
    /*
     * void Page_Load(object sender, EventArgs e)
     * Event method that gets invoked automatically in the event of loading the WebPage
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * This method redirects the user to Home page in case he is already logged in and trying 
     * to access login page again
     */
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Context.User.Identity.Name))
        {
            Response.Redirect("Home.aspx");
        }
    }



    /*
     * void Login1_LoggedIn(object sender, EventArgs e)
     * Event method that gets invoked when the login button is pressed and the login is successfuly
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * This method redirects the user the previously requested page or home page after a successful login
     * to access login page again
     */
    protected void Login1_LoggedIn(object sender, EventArgs e)
    {
        string userName=this.Login1.UserName;
        string redirectUrl = FormsAuthentication.GetRedirectUrl(userName, false);
        if (redirectUrl.Equals(FormsAuthentication.LoginUrl))
        {
            redirectUrl = "Home.aspx";
        }
        Response.Redirect(redirectUrl);
    }


    /*
     * void LinkAbout_Click(object sender, EventArgs e)
     * Event method that gets invoked when the About Tab is pressed on UI
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * Purpose: Displays the contents of About Tab
     */
    protected void LinkAbout_Click(object sender, EventArgs e)
    {
        PanelContact.Visible = false;
        PanelAbout.Visible = true;
        PanelLogin.Visible = false;
        LinkAbout.CssClass = "current_page_item";
        LinkLogin.CssClass = "";
        LinkContact.CssClass = "";
    }


    /*
     * void LinkContact_Click(object sender, EventArgs e)
     * Event method that gets invoked when the Contact Tab is pressed on UI
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * Purpose: Displays the contents of Contact Tab
     */
    protected void LinkContact_Click(object sender, EventArgs e)
    {
        PanelContact.Visible = true;
        PanelAbout.Visible = false;
        PanelLogin.Visible = false;
        LinkAbout.CssClass = "";
        LinkLogin.CssClass = "";
        LinkContact.CssClass = "current_page_item";
    }


    /*
     * void LinkLogin_Click(object sender, EventArgs e)
     * Event method that gets invoked when the Login Tab is pressed on UI
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * Purpose: Displays the contents of Login Tab
     */
    protected void LinkLogin_Click(object sender, EventArgs e)
    {
        PanelContact.Visible = false;
        PanelAbout.Visible = false;
        PanelLogin.Visible = true;
        LinkAbout.CssClass = "";
        LinkLogin.CssClass = "current_page_item";
        LinkContact.CssClass = "";
    }
}
