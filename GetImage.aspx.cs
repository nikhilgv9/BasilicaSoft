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
 * GetImage.aspx.cs
 * This page is never directly accessed by the User.
 * The response of this page is of MIME type 'image/jpeg'
 * It displays the requested image to this page as the request parameter imageId
 * The image is retrieved from table EVENT_ALBUM
 */
public partial class GetImage : System.Web.UI.Page
{

    SqlCommand com;
    SqlDataReader reader;
    string conString = ConfigurationManager.ConnectionStrings["basilicaConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        bool incident = false;
        if (Request["imageId"] == null) return;
        if (Request["incident"] != null)
        {
            incident = true;
        }
        String image=Request["imageId"].ToString();
        if (string.IsNullOrEmpty(image))
        {
            return;
        }
        int imageId = Int16.Parse(image);
        Response.ContentType = "image/jpeg";


        using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
        {
            if (!incident)
            {
                com = new SqlCommand("SELECT * FROM EVENT_ALBUM WHERE ALBUM_ENTRY_ID=@ImageId", con);
                con.Open();
                com.Parameters.Add("@ImageId", SqlDbType.Int).Value = imageId;
                reader = com.ExecuteReader();
                if (reader.Read())
                {
                    byte[] content = (byte[])reader["IMAGE_DATA"];
                    if (content.Length == 0)
                    {
                        return;
                    }
                    Response.OutputStream.Write(content, 0, content.Length);
                }
                reader.Close();
            }
            else
            {

                com = new SqlCommand("SELECT * FROM INCIDENTS WHERE INCIDENT_ID=@ImageId", con);
                con.Open();
                com.Parameters.Add("@ImageId", SqlDbType.Int).Value = imageId;
                reader = com.ExecuteReader();
                if (reader.Read())
                {
                    byte[] content = (byte[])reader["IMAGE_DATA"];
                    if (content.Length == 0)
                    {
                        return;
                    }
                    Response.OutputStream.Write(content, 0, content.Length);
                }
                reader.Close();

            }
        }
    }
}
