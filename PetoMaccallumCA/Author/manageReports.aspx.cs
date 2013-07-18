using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using petomaccallumModel;
using System.IO;
using System.Web.Security;
using System.Text.RegularExpressions;

public partial class Author_manageReports : System.Web.UI.Page
{
                    // page scope variable to access in markup
   protected string[] dbData;
  
    protected void Page_Load(object sender, EventArgs e)
    {
        ReportManager rm = new ReportManager();
        dbData = rm.getData();

        if (!IsPostBack) {
            categoriesDDL.DataSource = rm.getCategories();
            categoriesDDL.DataBind();
            categoriesDDL.Items.Insert(0, "All Reports");
        }
    }
}