using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Diagnostics;

/* MasterPage.master.cs
 * This is the C# class corresponding to the master page which 
 * acts as a template for all the pages in this project (Other than Login page)
 * So it contains method common to all pages like verification of login page, 
 * Display of profile pictures according to user
 * And displaying appropriate welcome message according to the user
 * 
 */

public partial class MasterPage : System.Web.UI.MasterPage
{
    /*
     * Page_Load(object sender, EventArgs e)
     * Event method that gets invoked automatically in the event of loading the WebPage
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     */
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckLogin();  
    }

    /*
     * LogoutButton_Click(object sender, EventArgs e)
     * Event method that gets invoked when the logout button is pressed
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * This method performs the necessary tasks to signout the user from the application
     */
    protected void LogoutButton_Click(object sender, EventArgs e)
    {
        //Signout from the application
        FormsAuthentication.SignOut();
        //Destroy the session
        Session.Abandon();
        //Redirect back to login page
        FormsAuthentication.RedirectToLoginPage();
    }


    /*
     * CheckLogin()
     * This method is usually invoked by Page_Load() to check the type of user logged in
     * And it displays the user depended UI components like, the profile picture, the welcome message etc
     * @params: none
     * @return: none
     */
    protected void CheckLogin()
    {
        IPrincipal p = HttpContext.Current.User;

        if (p.IsInRole("Admin")) //If the USER is of ADMIN Role
        {
            Label1.Text = "Welcome Administrator";
            Image1.ImageUrl = "images/admin pic.png";
        }
        else if (p.IsInRole("Parishioner")) //If the USER is of Parishioner Role
        {
            Label1.Text = "Welcome Parishioner";
            Image1.ImageUrl = "images/Users.png";
        }
        else if (p.IsInRole("Trustee")) //If the USER is of Trustee Role
        {
            Label1.Text = "Welcome Trustee";
            Image1.ImageUrl = "images/clerk.png";
        }
    }
}
