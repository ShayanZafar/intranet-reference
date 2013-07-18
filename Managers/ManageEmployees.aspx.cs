using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Managers_ManageEmployees : System.Web.UI.Page
{
    protected string[] dbData;
    protected void Page_Load(object sender, EventArgs e)
    {
        TimesheetManager tm = new TimesheetManager();
        dbData = tm.getBranches();
        if (!IsPostBack) {
            datesDDL.DataSource = tm.getDates();
            datesDDL.DataBind();
        }
    }
}