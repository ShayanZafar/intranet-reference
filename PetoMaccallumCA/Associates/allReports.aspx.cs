using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Executives_allReports : System.Web.UI.Page
{
    // page scope variable to access in markup
    protected string[] dbData;
    protected void Page_Load(object sender, EventArgs e)
    {

            ReportManager rm = new ReportManager();
            dbData = rm.getData("Associate");


    }
}