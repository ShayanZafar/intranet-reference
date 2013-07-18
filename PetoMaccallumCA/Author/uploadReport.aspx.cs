using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using petomaccallumModel;
using System.Text.RegularExpressions;
using System.IO;
public partial class Author_uploadReport : System.Web.UI.Page
{
    protected string msg;

    protected void Page_Load(object sender, EventArgs e)
    {
        ReportManager rm = new ReportManager();
        if (!IsPostBack)
        {
            //populate the drop down lists
            permissionDDL.DataSource = Roles.GetAllRoles();
            permissionDDL.DataBind();

            categoryDDL.DataSource = rm.getCategories();
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
                    ReportManager rm = new ReportManager();
                    // create new instance of a Report
                    Report r = new Report();
                    // verifies if the selected category and selected permissions match for the filename
                     msg = rm.reportUploadContextValidator(fu.FileName, categoryDDL.SelectedValue, permissionDDL.SelectedValue);

                    if (msg != "")
                    {
                       string script = "alert('" + msg + "');";
                        ClientScript.RegisterStartupScript(this.GetType(), "warning", "warn();", true);
                        //ClientScript.RegisterStartupScript(this.GetType(), "warning", script, true);
                    }
                    else
                    {
                        // matches MMM-YYYY i.e May-2012
                        string dateExp = "\\b([A-Z]{1}[a-z]{2})-(\\d{4})\\b";
                        bool isValid = false;

                        // extract "MMM-YYYY" from Financial Statements MMM-YYYY, if wrong format, display an error
                        if (Regex.IsMatch(fu.FileName, dateExp))
                        {
                            r.ReportDate = Regex.Match(fu.FileName, dateExp).Value;
                            isValid = true;
                        }
                        else
                        {
                            if (categoryDDL.SelectedValue == "Financial Statements")
                            {
                                statusLbl.ForeColor = System.Drawing.Color.Red;
                                statusLbl.Text = "Error: File in Incorrect Format. Correct Format: (<Name> <Date: MMM-YYYY> <(type)>) ";
                            }
                            else {
                                isValid = true;
                                r.ReportDate = DateTime.Now.ToString();
                            }
                        }
                        int start = 0;

                        // for files that have brackets enclosing file type information in the filename
                        if (fu.FileName.Contains('(') && fu.FileName.Contains(')'))
                        {
                            // extract the text within brackets get the start and end indexes
                            start = fu.FileName.IndexOf("(") + 1;
                            int end = fu.FileName.IndexOf(")");
                            int length = end - start;

                            r.Type = fu.FileName.Substring(start, length);
                        }

                        if (isValid)
                        {
                            // populate the Report instance

                            // truncate the brackets and the extension from the name if the file has brackets
                            if (start > 0)
                            {
                                r.Name = fu.FileName.Substring(0, start - 1);
                            }
                            else
                            {
                                r.Name = fu.FileName;
                            }
                            // convert bytes to kilobytes
                            r.Size = fu.PostedFile.ContentLength/ 1000;
                            r.MIMEType = fu.PostedFile.ContentType;
                            r.DateCreated = DateTime.Now;
                            r.DateModified = DateTime.Now;
                            r.FileContent = fu.FileBytes;
                            r.Extension = Path.GetExtension(fu.PostedFile.FileName).Substring(1);
                            r.Role = permissionDDL.SelectedItem.Value;
                            r.Category = categoryDDL.SelectedValue;

                            // save changes to the database

                            rm.createReport(r);
                            // show success
                            statusLbl.ForeColor = System.Drawing.Color.Green;
                            statusLbl.Text = "File Successfully Uploaded";

                        }
                    }
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