using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using petomaccallumModel;
using System.IO;
using System.Web.Security;
public partial class Author_uploadDocument : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FileManager fm = new FileManager();
        if (!IsPostBack)
        {
            //populate the drop down lists
            permissionDDL.DataSource = Roles.GetAllRoles();
            permissionDDL.DataBind();

            categoryDDL.DataSource = fm.getCategories();
            categoryDDL.DataBind();

        }
    }
    protected void uploadBtn_Click(object sender, EventArgs e)
    {
        if (fu.HasFile && fu.FileContent.Length != 0)
        {

            using (var context = new PetoEntities())
            {
                try
                {
                    // create new instance of a document
                    Document d = new Document();

                    // populate this instance
                    d.name = fu.FileName;
                    // divide by 1000 to convert bytes to kilobytes
                    d.size = fu.PostedFile.ContentLength / 1000;
                    d.MIMEType = fu.PostedFile.ContentType;
                    d.DateCreated = DateTime.Now;
                    d.DateModified = DateTime.Now;
                    d.fileContent = fu.FileBytes;
                    d.extension = Path.GetExtension(fu.PostedFile.FileName).Substring(1);
                    d.Role = permissionDDL.SelectedItem.Value;
                    // d.Category = typeDDL.SelectedValue;

                    // save changes to the database
                    FileManager fm = new FileManager();
                    fm.createDocument(d);
                    // show success
                    statusLbl.ForeColor = System.Drawing.Color.Green;
                    statusLbl.Text = "File Successfully Uploaded";

                }
                catch (Exception ex)
                {
                    // shoe error message
                    statusLbl.ForeColor = System.Drawing.Color.Red;
                    statusLbl.Text = "Error: File was not uploaded due to: " + ex.Message;
                    throw;
                }
            }
        }
        else
        {
            statusLbl.ForeColor = System.Drawing.Color.Red;
            statusLbl.Text = "Error: No File Chosen";
        }
    }
 
}