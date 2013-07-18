using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using petomaccallumModel;

public partial class Administrator_manageDocuments : System.Web.UI.Page
{
    // page scope variable to access in markup
    protected string[] dbData;

    protected void Page_Load(object sender, EventArgs e)
    {
        FileManager fm = new FileManager();
        dbData = fm.getData();
    }

    protected void searchBtn_Click(object sender, EventArgs e)
    {
        if (searchTB.Text != string.Empty)
        {
            List<Document> searchResults = new List<Document>();

            SqlConnection conn = new SqlConnection("data source=USER;initial catalog=petomaccallum.ca;user id=sa;password=PMLtest;multipleactiveresultsets=True");

            conn.Open();

            SqlCommand stmt = conn.CreateCommand();

            //get projnum, name, client
            stmt.CommandText = "SELECT DocumentId FROM Documents WHERE CONTAINS(fileContent,@searchText)";
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
            }
            //  update the gridview
   
        }
    }
}