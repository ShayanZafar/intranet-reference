using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Managers_viewProjectProfiles : System.Web.UI.Page
{
    protected string[] dbData;
    protected void Page_Load(object sender, EventArgs e)
    {
        ReportManager rm = new ReportManager();
        dbData = rm.getData();
    }
}