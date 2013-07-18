using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using petomaccallumModel;
using System.Data.SqlClient;

public partial class Member_searchDocuments : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FileManager fm = new FileManager();

            GridView1.DataSource = fm.getDocuments("");
            GridView1.DataBind(); 
        }

    }
    protected void searchBtn_Click(object sender, EventArgs e)
    {
        if (searchTB.Text != string.Empty)
        {
            List<Document> searchResults = new List<Document>();

            //SqlConnection conn = new SqlConnection("data source=208.124.173.18;initial catalog=petomaccallum.ca;user id=sa;password=PMLsa1234;multipleactiveresultsets=True");
            SqlConnection conn = new SqlConnection("data source=User;initial catalog=petomaccallum.ca;user id=sa;password=PMLtest;multipleactiveresultsets=True");
            conn.Open();

            SqlCommand stmt = conn.CreateCommand();

            //get projnum, name, client
            if (searchTB.Text.Contains(' '))
            {
                stmt.CommandText = "SELECT DocumentId FROM Documents WHERE FREETEXT(fileContent,@searchText)";
            }
            else {
                stmt.CommandText = "SELECT DocumentId FROM Documents WHERE CONTAINS(fileContent,@searchText)"; 
            }

            stmt.Parameters.AddWithValue("@searchText", searchTB.Text);
            TimesheetManager tm = new TimesheetManager();

            SqlDataReader projectReader = stmt.ExecuteReader();
            using (var context = new PetoEntities())
            {
                if (projectReader.HasRows)
                {
                    while (projectReader.Read())
                    {
                        int id = projectReader.GetInt32(0);
                        searchResults.Add(context.Documents.FirstOrDefault(x => x.DocumentId == id));
                    }
                }
                GridView1.DataSource = searchResults;
                GridView1.DataBind();
            }
        }
        else {
        
             FileManager fm = new FileManager();

            GridView1.DataSource = fm.getDocuments("");
            GridView1.DataBind(); 
        }
    }
}