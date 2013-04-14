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

/*
 * EditPerson.aspx.cs
 * This class corresponds to the user interface Edit Person. Which enables users to see existing persons
 * Edit and delete persons for administrators.
 * 
 */

public partial class EditPerson : System.Web.UI.Page
{
    /*Class variables */
    SqlCommand com;
    SqlDataReader reader;
    string conString = ConfigurationManager.ConnectionStrings["basilicaConnectionString"].ToString();

    /*
     * override void OnInit(EventArgs e)
     * Page initialization event handler.
     * This method is used to load the drop downs having unit names family names person name etc.
     */
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
        {
            com = new SqlCommand("SELECT DISTINCT UNIT_NAME FROM FAMILY_DETAILS", con);
            con.Open();
            reader = com.ExecuteReader();
            while (reader.Read())
            {
                String s = reader["UNIT_NAME"].ToString();
                DropUnitName.Items.Add(s);
                DropDownUnits.Items.Add(s);
            }
            reader.Close();
            populateFamily(con, DropDownUnits, DropDownFamily, DropDownPerson);
            populateFamily(con, DropUnitName, DropDownFamilyName,null);
            populateDateControls(DropDOBYr, DropDOBMnth, DropDOBDay);
            populateDateControls(DropDownBtsmYr, DropDownBtsmMnth, DropDownBtsmDay);
            con.Close();
        }
    }
    //Nothin is being done in page load. All the task is performed on page initialization
    protected void Page_Load(object sender, EventArgs e)
    {    
    }
    /*
     *  void ImageButtonEdit_Click(object sender, ImageClickEventArgs e)
     *  Even handler
     *  It displays EditPerson details mage
     */
    //Method that handles the even on clicking Edit button
    protected void ImageButtonEdit_Click(object sender, ImageClickEventArgs e)
    {
        PanelViewPerson.Visible = false;
        PanelEditPersonDetails.Visible = true;
    }


    /*
     * void ImageButtonView_Click(object sender, ImageClickEventArgs e)
     *  Even handler
     *  It displays View Person details mage
     */
    protected void ImageButtonView_Click(object sender, ImageClickEventArgs e)
    {
        PanelViewPerson.Visible = true;
        PanelEditPersonDetails.Visible = false;
    }

    protected void DropDownFamily_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(conString);
        con.Open();
        populatePerson(con,DropDownFamily,DropDownPerson);
        con.Close();
    }
    protected void DropDownUnits_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(conString);
        con.Open();
        populateFamily(con, DropDownUnits, DropDownFamily, DropDownPerson);
        con.Close();
    }

    protected void DropDownPerson_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(conString);
        con.Open();
        loadPersonDetail(con);
        con.Close();
    }
    
    protected void populateFamily(SqlConnection con, DropDownList unitList, DropDownList familyList, DropDownList personList)
    {
        com = new SqlCommand("SELECT FAMILY_NAME  FROM FAMILY_view where UNIT_NAME=@unitname", con);
        familyList.Items.Clear();
        if (unitList.SelectedItem == null)
        {
            return;
        }
        String selectedUnit = unitList.SelectedItem.Value;
        com.Parameters.Add("@unitname", SqlDbType.VarChar).Value = selectedUnit;
        reader = com.ExecuteReader();
        
        while (reader.Read())
        {
            String s = reader["FAMILY_NAME"].ToString();
            familyList.Items.Add(s);
        }
        reader.Close();
        if (personList != null)
        {
            populatePerson(con, familyList, personList);
        }
 
    }
    protected void populatePerson(SqlConnection con, DropDownList familyList, DropDownList personList)
    {
        com = new SqlCommand("SELECT PERSON_NAME, PERSON_ID FROM PERSON_VIEW where FAMILY_NAME=@familyname", con);
        personList.Items.Clear();
        if (familyList.SelectedItem == null)
        {
            return;
        }
        String selectedFamily = familyList.SelectedItem.Value;
        com.Parameters.Add("@familyname", SqlDbType.VarChar).Value = selectedFamily;
        reader = com.ExecuteReader();
        while (reader.Read())
        {
            String s = reader["PERSON_NAME"].ToString();
            String v = reader["PERSON_ID"].ToString();
            personList.Items.Add(new ListItem(s, v));
        }
        reader.Close();
        loadPersonDetail(con);

    }
    /*
     * void loadPersonDetail(SqlConnection con)
     * @params SqlConnection con: An open SQL Connection object to SQL server
     * @return: none
     * Load all the relevant data of the selected person to view/edit
     */
    protected void loadPersonDetail(SqlConnection con)
    {
        com = new SqlCommand("SELECT *  FROM PERSON_VIEW where PERSON_ID=@personid", con);
        if (DropDownPerson.SelectedItem == null)
        {
            LabelName.Text = "";
            TextName.Text = "";
            LabelFamily.Text = "";
            LabelSex.Text = "";
            LabelDob.Text = "";
            LabelOccupation.Text = "";
            TextOccupation.Text = "";
            LabelMarital.Text = "";
            LabelBaptism.Text = "";
            return;
        }
        String v = DropDownPerson.SelectedItem.Value;
        com.Parameters.Add("@personid", SqlDbType.Int).Value =Int16.Parse(v);
        reader = com.ExecuteReader();
        using (System.Data.SqlClient.SqlConnection con2 = new System.Data.SqlClient.SqlConnection(conString))
        {
            con2.Open();
            if (reader.Read())
            {
                LabelName.Text = reader["PERSON_NAME"].ToString();
                TextName.Text = reader["PERSON_NAME"].ToString();
                LabelFamily.Text = reader["FAMILY_NAME"].ToString();
                LabelSex.Text = reader["SEX"].ToString();
                DropDownSex.Text = reader["SEX"].ToString();
                if (reader["DOB"] != null)
                {
                    LabelDob.Text = ((DateTime)reader["DOB"]).ToShortDateString();
                }
                else
                {
                    LabelDob.Text = "";
                }
                DropUnitName.Text = reader["UNIT_NAME"].ToString();
                LabelOccupation.Text = reader["OCCUPATION"].ToString();
                TextOccupation.Text = reader["OCCUPATION"].ToString();
                LabelMarital.Text = reader["MARITAL_STATUS"].ToString();
                DropDownMaritalStatus.Text = reader["MARITAL_STATUS"].ToString();
                if (reader["BAPTISM_DATE"] != null)
                {
                    LabelBaptism.Text = ((DateTime)reader["BAPTISM_DATE"]).ToShortDateString();
                }
                else
                {
                    LabelBaptism.Text = "";
                }
            }
            reader.Close();
        }
    }


    /*
    * void populateDateControls(DropDownList yr, DropDownList month, DropDownList day)
    * @params: DropDownList yr
    * @params: DropDownList month
    * @params: DropDownList day
    * @reutn: none
    * This method takes the UI drop down conponents for yearr month and day, and populate the drop downs with values for selection
    * This method delegates control to populateDays() for polulating days
    */
    protected void populateDateControls(DropDownList yr, DropDownList month, DropDownList day)
    {
        int startYr = DateTime.Now.Year;
        for (int i = 0; i < 150; i++)
        {
            yr.Items.Add("" + (startYr - i));
        }
        month.Items.Clear();
        for (int i = 1; i <= 12; i++)
        {
            month.Items.Add("" + i);
        }
        populeDays(yr, month, day);
    }

    /*
     * void populeDays(DropDownList yr, DropDownList month, DropDownList day)
     * @params: DropDownList yr
     * @params: DropDownList month
     * @params: DropDownList day
     * @reutn: none
     * Populates th day drop down with the  integers upto number of datys in teh month depending on the
     * selected year and month
     */
    protected void populeDays(DropDownList yr, DropDownList month, DropDownList day)
    {
        int daysInMonth = DateTime.DaysInMonth(Int16.Parse(yr.SelectedItem.Text), Int16.Parse(month.SelectedItem.Text));
        day.Items.Clear();
        for (int i = 1; i <= daysInMonth; i++)
        {
            day.Items.Add("" + i);
        }
    }

    /*
     * Event method to be invoked on selecting a different Unit for a person in Edit mode
     * It populates the family name drop down with set of all family names
     */
    protected void DropUnitName_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(conString);
        con.Open();
        populateFamily(con, DropUnitName, DropDownFamilyName,null);
        con.Close();
    }
}
