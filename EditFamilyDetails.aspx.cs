using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Security.Principal;
using System.Web.UI.HtmlControls;

/*
 * This class provides functionalities to View existing family details
 * Edit Family details
 * Delete and Existign family
 * Add memebers to Family
 */

public partial class EditFamilyDetails : System.Web.UI.Page
{
    /*Class variables */
    SqlCommand com;
    SqlDataReader reader;
    string conString = ConfigurationManager.ConnectionStrings["basilicaConnectionString"].ToString();

    /*
     * void OnInit(EventArgs e)
     * Initialization Event handler
     */
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        initPage();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void ImageButtonAdd_Click(object sender, ImageClickEventArgs e)
    {
        LabelInsertMessage.Text = "";
        PanelViewFamilyDetails.Visible = false;
        PanelPersonDetails.Visible = true;
        PanelEditFamilyDetails.Visible = false;
    }
    protected void ImageButtonEdit_Click(object sender, ImageClickEventArgs e)
    {
        LabelUpdateMessage.Text = "";
        PanelViewFamilyDetails.Visible = false;
        PanelPersonDetails.Visible = false;
        PanelEditFamilyDetails.Visible = true;
    }
    protected void ImageButtonView_Click(object sender, ImageClickEventArgs e)
    {
        using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
        {
            con.Open();
            populateFamilyDetails(con);
            con.Close();
        }
        PanelViewFamilyDetails.Visible = true;
        PanelPersonDetails.Visible = false; 
        PanelEditFamilyDetails.Visible = false;
    }
    protected void DropDownUnits_SelectedIndexChanged(object sender, EventArgs e)
    {
         Debug.WriteLine("Drop down changed");
         using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
         {
             
             con.Open();
             populateFamiliesList(con);
             con.Close();
         }
    }
    protected void ButtonUpdateFamily_Click(object sender, EventArgs e)
    {
        if (DropDownFamily.SelectedItem == null)
        {
            LabelUpdateMessage.Text = "You should select a family";
            return;
        }
        int familyId = Int16.Parse(DropDownFamily.SelectedItem.Value);
        String familyName = TextFamilyName.Text.Trim();
        String unitName;
        if (DropDownUnitNames.SelectedItem != null)
        {
            unitName = DropDownUnitNames.SelectedItem.Text.Trim();
        }
        else
        {
            unitName = "";
        }
        String address = TextFamilyAddress.Text.Trim();
        String phone = TextPhone.Text.Trim();


        if (string.IsNullOrEmpty(unitName))
        {
            LabelUpdateMessage.Text = "You should enter a unit";
            return;
        }
        if (string.IsNullOrEmpty(familyName))
        {
            LabelUpdateMessage.Text = "You should enter a family name";
            return;
        }

        using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
        {
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            com = new SqlCommand("UPDATE FAMILY SET FAMILY_NAME=@familyName WHERE FAMILY_ID=@familyId", con, trans);

            com.Parameters.Add("@familyId", SqlDbType.Int).Value = familyId;
            com.Parameters.Add("@familyName", SqlDbType.NVarChar).Value = familyName;
            int result = com.ExecuteNonQuery();
            if (result == 1)
            {
                com = new SqlCommand("UPDATE FAMILY_DETAILS SET UNIT_NAME=@unitName,ADDRESS=@address,PHONE=@phone WHERE FAMILY_ID=@familyId", con, trans);
                com.Parameters.Add("@familyId", SqlDbType.Int).Value = familyId;
                com.Parameters.Add("@unitName", SqlDbType.NVarChar).Value = unitName;
                com.Parameters.Add("@address", SqlDbType.NVarChar).Value = address;
                com.Parameters.Add("@phone", SqlDbType.NVarChar).Value = phone;
                result = com.ExecuteNonQuery();
                if (result == 1)
                {
                    LabelUpdateMessage.Text = "Successfully entered the data";
                    trans.Commit();
                }
                else
                {
                    LabelUpdateMessage.Text = "Failed to entered the data";
                    trans.Rollback();
                }
            }
            else
            {
                LabelUpdateMessage.Text = "Failed to entered the data";
                trans.Rollback();
            }
        }
    }

    protected void ButtonAddPerson_Click(object sender, EventArgs e)
    {
        //Code to create a new person
        int dobYr = Int16.Parse(DropDownDOBYr.SelectedItem.Text);
        int dobMonth = Int16.Parse(DropDownDOBMonth.SelectedItem.Text);
        int dobDay = Int16.Parse(DropDownDOBDay.SelectedItem.Text);
        DateTime dob = new DateTime(dobYr, dobMonth, dobDay);

        int btsmYr = Int16.Parse(DropDownBtsmYr.SelectedItem.Text);
        int btsmMonth = Int16.Parse(DropDownBtsmMonth.SelectedItem.Text);
        int btsmDay = Int16.Parse(DropDownBtsmDay.SelectedItem.Text);
        DateTime btsmDate = new DateTime(btsmYr, btsmMonth, btsmDay);
        int familyId=0;
        if (DropDownFamily.SelectedItem != null)
        {
            familyId = Int16.Parse(DropDownFamily.SelectedItem.Value);
        }
        string sex = DropDownPersonSex.SelectedItem.Text;
        string maritalStatus = DropDownMaritalStatus.SelectedItem.Text;
        string name = TextPersonName.Text;
        string occupation = TextPersonOccupation.Text;
        if (familyId == 0)
        {
            LabelInsertMessage.Text = "Select a family first";
            return;
        }
        if (string.IsNullOrEmpty(name))
        {
            LabelInsertMessage.Text = "Person should have a name";
            return;
        }
        if (string.IsNullOrEmpty(sex))
        {
            LabelInsertMessage.Text = "Select the sex";
            return;        
        }

        using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
        {
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            com = new SqlCommand("INSERT INTO PERSONS (FAMILY_ID,PERSON_NAME,SEX,DOB) VALUES (@familyId,@personName,@sex,@dob)", con, trans);
            com.Parameters.Add("@familyId", SqlDbType.Int).Value = familyId;
            com.Parameters.Add("@personName", SqlDbType.NVarChar).Value = name;
            com.Parameters.Add("@sex", SqlDbType.NVarChar).Value = sex;
            com.Parameters.Add("@dob", SqlDbType.DateTime).Value = dob;
            int result = com.ExecuteNonQuery();
            if (result == 1)
            {
                com = new SqlCommand("select max(PERSON_ID) ID from PERSONS", con, trans);
                reader = com.ExecuteReader();
                if (reader.Read())
                {
                    int personId = Int16.Parse(reader["ID"].ToString());
                    reader.Close();
                    com = new SqlCommand("INSERT INTO PERSON_DETAILS (PERSON_ID,OCCUPATION,BAPTISM_DATE,MARITAL_STATUS) VALUES (@personId,@occupation,@baptismDate,@maritalStatus)", con, trans);
                    com.Parameters.Add("@personId", SqlDbType.Int).Value = personId;
                    com.Parameters.Add("@occupation", SqlDbType.NVarChar).Value = occupation;
                    com.Parameters.Add("@baptismDate", SqlDbType.DateTime).Value = btsmDate;
                    com.Parameters.Add("@maritalStatus", SqlDbType.NVarChar).Value = maritalStatus;
                    result = com.ExecuteNonQuery();
                    if (result == 1)
                    {
                        com = new SqlCommand("SELECT COUNT(*) NO FROM PERSONS WHERE FAMILY_ID=@familyId", con, trans);
                        com.Parameters.Add("@familyId", SqlDbType.Int).Value = familyId;
                        reader = com.ExecuteReader();
                        if (reader.Read())
                        {
                            int count = Int16.Parse(reader["NO"].ToString());
                            reader.Close();
                            com = new SqlCommand("UPDATE FAMILY SET NO_OF_MEMBERS=@count WHERE FAMILY_ID=@familyId", con, trans);
                            com.Parameters.Add("@familyId", SqlDbType.Int).Value = familyId;
                            com.Parameters.Add("@count", SqlDbType.Int).Value = count;
                            result = com.ExecuteNonQuery();
                            if (result == 1)
                            {
                                LabelInsertMessage.Text = "Successfully added new person";
                                trans.Commit();
                                TextPersonName.Text = "";
                                TextPersonOccupation.Text = "";
                            }
                            else
                            {
                                LabelInsertMessage.Text = "Failed to insert new person";
                                trans.Rollback();
                            }
                        } 
                    }
                    else
                    {
                        LabelInsertMessage.Text = "Failed to insert new person";
                        trans.Rollback();
                    }
                }
            }
            else
            {
                LabelInsertMessage.Text = "Failed to insert new person";
                trans.Rollback();
            }
        }
    }

    protected void ImageButtonDelete_Click(object sender, ImageClickEventArgs e)
    {
        //Code to delete family
        //It includes deletign of memebrs of family, deletion of the application user etc..
        int familyId = 0;
        if (DropDownFamily.SelectedItem != null)
        {
            familyId = Int16.Parse(DropDownFamily.SelectedItem.Value);
        }
        if (familyId == 0)
        {
            MessageBox.Show("Select a family first");
            return;
        }
        using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
        {
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            com = new SqlCommand("delete PERSON_DETAILS WHERE PERSON_ID IN (SELECT PERSON_ID FROM PERSONS WHERE FAMILY_ID=@familyId)", con, trans);
            com.Parameters.Add("@familyId", SqlDbType.Int).Value = familyId;
            int result = com.ExecuteNonQuery();
            com = new SqlCommand("delete FROM PERSONS WHERE FAMILY_ID=@familyId", con, trans);
            com.Parameters.Add("@familyId", SqlDbType.Int).Value = familyId;
            result = com.ExecuteNonQuery();
            com = new SqlCommand("delete FROM family_details WHERE FAMILY_ID=@familyId", con, trans);
            com.Parameters.Add("@familyId", SqlDbType.Int).Value = familyId;
            result = com.ExecuteNonQuery();
            com = new SqlCommand("delete FROM family WHERE FAMILY_ID=@familyId", con, trans);
            com.Parameters.Add("@familyId", SqlDbType.Int).Value = familyId;
            result = com.ExecuteNonQuery();
            trans.Commit();
            initPage();
            MessageBox.Show("Successfully Deleted Family");
        } 

    }

    protected void DropDownFamily_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
        {
            con.Open();
            populateFamilyDetails(con);
            con.Close();
        }
    }


    protected void DropDownDOBYr_SelectedIndexChanged(object sender, EventArgs e)
    {
        populeDays(DropDownDOBYr, DropDownDOBMonth, DropDownDOBDay);
    }
    protected void DropDownDOBMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        populeDays(DropDownDOBYr, DropDownDOBMonth, DropDownDOBDay);
    }
    protected void DropDownBtsmYr_SelectedIndexChanged(object sender, EventArgs e)
    {
        populeDays(DropDownBtsmYr, DropDownBtsmMonth, DropDownBtsmDay);
    }
    protected void DropDownBtsmMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        populeDays(DropDownBtsmYr, DropDownBtsmMonth, DropDownBtsmDay);
    }

    protected void populateFamiliesList(System.Data.SqlClient.SqlConnection con)
    {
        if (DropDownUnits.SelectedItem != null)
        {
            string unitName = DropDownUnits.SelectedItem.Text;
            com = new SqlCommand("SELECT DISTINCT FAMILY_NAME,FAMILY_ID FROM FAMILY_VIEW WHERE UNIT_NAME=@unitName", con);
            com.Parameters.Add("@unitName", SqlDbType.VarChar).Value = unitName;
            reader = com.ExecuteReader();
            DropDownFamily.Items.Clear();
            while (reader.Read())
            {
                ListItem item = new ListItem(reader["FAMILY_NAME"].ToString(), reader["FAMILY_ID"].ToString());
                DropDownFamily.Items.Add(item);
            }
            reader.Close();

            populateFamilyDetails(con);
        }
    }

    protected void populateFamilyDetails(System.Data.SqlClient.SqlConnection con)
    {
        PanelFamilyMembers.Controls.Clear();
        LabelFamilyName.Text = "";
        LabelUnitName.Text = "";
        LabelAddress.Text = "";
        LabelPhone.Text = "";
        LabelNoMembers.Text = "";

        TextFamilyName.Text = "";
        TextFamilyAddress.Text = "";
        TextPhone.Text = "";
        if (DropDownFamily.SelectedItem != null)
        {
            int familyId = Int16.Parse(DropDownFamily.SelectedItem.Value);
            com = new SqlCommand("SELECT * FROM FAMILY_VIEW WHERE FAMILY_ID=@familyId", con);
            com.Parameters.Add("@familyId", SqlDbType.Int).Value = familyId;
            reader = com.ExecuteReader();
            if (reader.Read())
            {
                LabelFamilyName.Text = reader["FAMILY_NAME"].ToString();
                LabelUnitName.Text = reader["UNIT_NAME"].ToString();
                LabelAddress.Text = reader["ADDRESS"].ToString();
                LabelPhone.Text = reader["PHONE"].ToString();
                LabelNoMembers.Text=reader["NO_OF_MEMBERS"].ToString();

                TextFamilyName.Text = reader["FAMILY_NAME"].ToString();
                string selectedUnit=reader["UNIT_NAME"].ToString();
                DropDownUnitNames.Text = selectedUnit;
                TextFamilyAddress.Text = reader["ADDRESS"].ToString();
                TextPhone.Text=reader["PHONE"].ToString();
            }
            reader.Close();

            com = new SqlCommand("SELECT * FROM PERSONS WHERE FAMILY_ID=@familyId", con);
            com.Parameters.Add("@familyId", SqlDbType.Int).Value = familyId;
            reader = com.ExecuteReader();

            LiteralControl unorderedList = new LiteralControl();
            unorderedList.Text += @"<ul style='padding-left: 2px;'>";
            while (reader.Read())
            {
                string person = reader["PERSON_NAME"].ToString();
                unorderedList.Text += @"<li><a href='#'><span>" + person + "</span></a></li>";
            }
            unorderedList.Text += @"</ul>";
            PanelFamilyMembers.Controls.Add(unorderedList);
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
    protected void populateDateControls(DropDownList yr,DropDownList month, DropDownList day)
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
     * void initPage()
     * Essesntial methos that intializes the page.
     * Usually it is invoked by OnInit() event handler
     * 
     */
    protected void initPage()
    {
        using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
        {
            con.Open();
            SqlCommand com = new SqlCommand("SELECT DISTINCT UNIT_NAME FROM FAMILY_DETAILS", con);
            SqlDataReader reader = com.ExecuteReader();
            DropDownUnits.Items.Clear();
            while (reader.Read())
            {
                DropDownUnits.Items.Add(reader["UNIT_NAME"].ToString());
                DropDownUnitNames.Items.Add(reader["UNIT_NAME"].ToString());
            }
            reader.Close();
            populateFamiliesList(con);
            con.Close();
        }

        populateDateControls(DropDownDOBYr, DropDownDOBMonth, DropDownDOBDay);
        populateDateControls(DropDownBtsmYr, DropDownBtsmMonth, DropDownBtsmDay);
        string type = "";
        if (Request["type"] != null)
        {
            type = Request["type"];
        }
        IPrincipal p = HttpContext.Current.User;
        if (p.IsInRole("Admin") || p.IsInRole("Trustee"))
        {
            if (string.Equals(type, "view"))
            {
                PanelButtons.Visible = false;
                LabelHeading.Text = "View Family";
            }
            else
            {
                PanelButtons.Visible = true;
                LabelHeading.Text = "Edit Family";
            }
        }
        else
        {
            PanelButtons.Visible = false;
            LabelHeading.Text = "View Family";
        }
    }
  
}