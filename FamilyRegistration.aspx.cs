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
using System.Diagnostics;

public partial class FamilyRegistration : System.Web.UI.Page
{
    /*Class variables*/
    SqlCommand com;
    SqlDataReader reader;
    string conString = ConfigurationManager.ConnectionStrings["basilicaConnectionString"].ToString();

    /*
     * protected override void OnInit(EventArgs e)
     * The Event handler that gets invoked automatically on the envent of page creation
     * @Overrides the base class method OnInit
     * @params: EventArgs e is the set of arguments that is passed on teh creation of teh page
     * purpose: It loads the drop down of the Unit names with the set of unit names
     */
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        string conString = ConfigurationManager.ConnectionStrings["basilicaConnectionString"].ToString();

        using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
        {
            con.Open();
            loadUnitNames(con);
            con.Close();
        }
    }

    /*
     * void Page_Load(object sender, EventArgs e)
     * Event handler that gets invoked automatically in the event of loading the WebPage
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     */
    protected void Page_Load(object sender, EventArgs e)
    {       
         if (string.IsNullOrEmpty(TextFamilyName.Text))
         {
             TextFamilyName.Focus();
         }
         else
         {
             this.DropDownUnitName.Focus();
         }
    }

    /*
     * void Submit_Click(object sender, EventArgs e)
     * Event handler that gets invoked automatically when Submit button is clicked
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * This method registers a new family. Along with creating a new family it also creates
     * An application user for that family
     */
    protected void Submit_Click(object sender, EventArgs e)
    {
        int parishId = 1;
        String familyName = TextFamilyName.Text.Trim();
        String unitName;
        if (TextUnitName.Visible == true)
        {
            unitName = TextUnitName.Text;
        }
        else if (DropDownUnitName.SelectedItem != null)
        {
            unitName = DropDownUnitName.SelectedItem.Text.Trim();
        }
        else
        {
            unitName = "";
        }
        String address = TextAddress.Text.Trim();
        String phone = TextPhone.Text.Trim();
        String username = TextUserName.Text.Trim();
        int noMembers = 0;
        MembershipUser user = Membership.GetUser(username);

        //Folllowing lines perform the validation of the input data
        if (string.IsNullOrEmpty(unitName))
        {
            LabelMessage.Text = "You should enter a unit";
            return;
        }
        if (string.IsNullOrEmpty(familyName))
        {
            LabelMessage.Text = "You should enter a family name";
            return;
        }
        else if(string.IsNullOrEmpty(username)){
            LabelAvailability.Text = "UserName connot be empty";
            return;
        }
        else if (user != null)
        {
            LabelAvailability.Text = "Not Available";
            return;
        }

         
         using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
         {
             con.Open();
             SqlTransaction trans = con.BeginTransaction();
             com = new SqlCommand("INSERT INTO FAMILY(PARISH_ID,FAMILY_NAME,NO_OF_MEMBERS) VALUES (@parishId,@familyName,@noMembers)", con, trans);
             com.Parameters.Add("@parishId", SqlDbType.Int).Value = parishId;
             com.Parameters.Add("@familyName", SqlDbType.NVarChar).Value = familyName;
             com.Parameters.Add("@noMembers", SqlDbType.Int).Value = noMembers;
             int result = com.ExecuteNonQuery();
             if (result == 1)
             {
                 com = new SqlCommand("select max(FAMILY_ID) ID from FAMILY", con, trans);
                 reader = com.ExecuteReader();
                 if (reader.Read())
                 {
                     int familyId = Int16.Parse(reader["ID"].ToString());
                     reader.Close();
                     com = new SqlCommand("INSERT INTO FAMILY_DETAILS(FAMILY_ID,ADDRESS,UNIT_NAME,PHONE) VALUES (@familyId,@address,@unitName,@phone)", con, trans);
                     com.Parameters.Add("@familyId", SqlDbType.Int).Value = familyId;
                     com.Parameters.Add("@address", SqlDbType.NVarChar).Value = address;
                     com.Parameters.Add("@unitName", SqlDbType.NVarChar).Value = unitName;
                     com.Parameters.Add("@phone", SqlDbType.NVarChar).Value = phone;
                     com.ExecuteNonQuery();
                     
                     string defaultPassword = "Password_123";
                     MembershipCreateStatus m;
                     user = Membership.CreateUser(username, defaultPassword, "user@basilica.com", "a", "a", true, out m);
                     Roles.AddUserToRole(user.UserName, "Parishioner");
                     com = new SqlCommand("INSERT INTO USERS(PARISH_ID,FAMILY_ID,USER_NAME) VALUES(@parishid,@familyid,@username)", con,trans);
                     com.Parameters.Add("@parishid", SqlDbType.Int).Value = parishId;
                     com.Parameters.Add("@familyid", SqlDbType.Int).Value = familyId;
                     com.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                     com.ExecuteNonQuery();
                     
                     trans.Commit();
                     LabelMessage.Text = "Successfully entered the data";
                     TextFamilyName.Text = "";
                     TextUnitName.Text = "";
                     TextAddress.Text = "";
                     TextPhone.Text = "";
                     LabelAvailability.Text = "";
                     TextUserName.Text = "";
                     loadUnitNames(con);
                 }
                 else
                 {
                     LabelMessage.Text = "Failed to entered the data";
                     trans.Rollback();
                 }
                 con.Close();
             }
             else
             {
                 LabelMessage.Text = "Failed to entered the data";
                 trans.Rollback();
             }

         }
    }

    /*
     * void ButtonAvailability_Click(object sender, EventArgs e)
     * Event handler that gets invoked when the 'Check Availability' button is clicked
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * This method checks whether the username is available or already choosen (hence not available)
     */
    protected void ButtonCheckAvailablity_Click(object sender, EventArgs e)
    {
        string username = TextUserName.Text.Trim();
        MembershipUser user = Membership.GetUser(username);
        if(string.IsNullOrEmpty(username)){
            LabelAvailability.Text = "UserName connot be empty";
        }
        else if (user == null)
        {
            LabelAvailability.Text = "Available";
        }
        else
        {
            LabelAvailability.Text = "Not Available";
        }
    }

    /*
     * void ButtonNewUnit_Click(object sender, EventArgs e)
     * Event handler that gets invoked when the 'New Unit' button is clicked
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * purpose: Show the Text box to receive the new Unit's name from the user
     */
    protected void ButtonNewUnit_Click(object sender, EventArgs e)
    {
        if (TextUnitName.Visible == false)
        {
            TextUnitName.Visible = true;
            DropDownUnitName.Visible = false;
            ButtonNewUnit.Text = "Select Existing Unit";
        }
        else
        {
            TextUnitName.Visible = false;
            DropDownUnitName.Visible = true;
            ButtonNewUnit.Text = "Add New Unit";

        }
    }
    protected void TextFamilyName_Change(object sender, EventArgs e)
    {
        TextUserName.Text = TextFamilyName.Text;
        string username = TextUserName.Text.Trim();
        MembershipUser user = Membership.GetUser(username);
        if (string.IsNullOrEmpty(username))
        {
            LabelAvailability.Text = "UserName connot be empty";
        }
        else if (user == null)
        {
            LabelAvailability.Text = "Available";
        }
        else
        {
            LabelAvailability.Text = "Not Available";
        }
    }


    /*
     * void loadUnitNames(System.Data.SqlClient.SqlConnection con)
     * @params: SqlConnection con - is an open conncection object to SQL server
     * @return: Returns nothing
     * purpose: Loads the drop down for units with the names of existing users
     */
    protected void loadUnitNames(System.Data.SqlClient.SqlConnection con)
    {
        com = new SqlCommand("SELECT DISTINCT UNIT_NAME UNIT FROM FAMILY_DETAILS", con);
        reader = com.ExecuteReader();
        DropDownUnitName.Items.Clear();
        while (reader.Read())
        {
            DropDownUnitName.Items.Add(reader["UNIT"].ToString());
        }
    }

}
