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

public partial class CreateResources : System.Web.UI.Page
{
    string conString = ConfigurationManager.ConnectionStrings["basilicaConnectionString"].ToString();
    //SqlDataReader reader;
    SqlCommand com;

    /*
     * protected override void OnInit(EventArgs e)
     * The Event handler that gets invoked automatically on the envent of page creation
     * @Overrides teh base class method
     * @params: EventArgs e is the set of arguments that is passed on teh creation of teh page
     * purpose: it sets the resource type to 'Physical Property'
     */

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        TextType.Text = "Physical Property";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //We are doing nothing in the page load
    }

    /*
     * void ButtonCreate_Click(object sender, EventArgs e)
     * Event handler that gets invoked when the Request UI Button is clicked
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * This method creates a new parish resource, that can be requested by the parishnor later
     */
    protected void ButtonCreate_Click(object sender, EventArgs e)
    {
        String title = TextResourceName.Text;
        String type = TextType.Text;
        String description = TextDescription.Text;
        int parishid = 1;

        //Perform basic validations
        if (String.IsNullOrEmpty(title))
        {
            LabelMessage.Text = "Resorce name cannot be empty";
            return;
        }
        if (String.IsNullOrEmpty(type))
        {
            LabelMessage.Text = "Choose a resource type";
            return;
        }
        using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
        {
            com = new SqlCommand("INSERT INTO PARISH_RESOURCES (PARISH_ID,TITLE,RESOURCE_DESCRIPTION,RESOURCE_TYPE) VALUES(@parishid,@title,@description,@type)", con);
            com.Parameters.Add("@type", SqlDbType.VarChar).Value = type;
            com.Parameters.Add("@parishid", SqlDbType.Int).Value = parishid ;
            com.Parameters.Add("@title", SqlDbType.VarChar).Value = title ;
            com.Parameters.Add("@description", SqlDbType.VarChar).Value = description ;
            con.Open();
            int result= com.ExecuteNonQuery();
            if (result == 1)
            {
                LabelMessage.Text = "Successfully created";
                TextDescription.Text = "";
                TextResourceName.Text = "";
                

            }
            else
            {
                LabelMessage.Text = "Failed to create Resource";
            }
            con.Close();
        }
    }

    /*
     * void Image1_Click(object sender, ImageClickEventArgs e)
     * Event handler that gets invoked when the 'Physical Property' UI image Button is clicked
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * Changes the resource type to 'Physical Property'
     */
    protected void Image1_Click(object sender, ImageClickEventArgs e)
    {
        TextType.Text = "Physical Property";
    }

    /*
     *void Image2_Click(object sender, ImageClickEventArgs e)
     * Event handler that gets invoked when the 'Date' UI ImageButton is clicked
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * Changes the resource type to 'Date'
     */
    protected void Image2_Click(object sender, ImageClickEventArgs e)
    {
        TextType.Text = "Date";
    }
}

