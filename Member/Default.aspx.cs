using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using petomaccallumModel;
public partial class Member_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // get id from query string
        int id = Convert.ToInt32(Request.QueryString["id"]);
        string type = Request.QueryString["type"];

        if (type == "doc")
        {
            // get document for id
            FileManager fm = new FileManager();
            Document d = fm.documentForID(id);
            Response.Write(Request.QueryString["id"]);

            Response.Clear();
            Response.Buffer = true;

            Response.ContentType = d.MIMEType;
            Response.BinaryWrite(d.fileContent);
        }
        else {
            ReportManager rm = new ReportManager();
           Report r = rm.ReportForID(id);
            Response.Write(Request.QueryString["id"]);

            Response.Clear();
            Response.Buffer = true;

            Response.ContentType = r.MIMEType;
            Response.BinaryWrite(r.FileContent);
        }
        Response.Flush();
        Response.End();
    }
}