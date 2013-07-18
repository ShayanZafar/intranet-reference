using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using petomaccallumModel;

public partial class Member_profile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TimesheetManager tm = new TimesheetManager();
       using(var context = new PetoEntities())
	{
           string name = Membership.GetUser().UserName;
           User user = context.Users.FirstOrDefault(x => x.username == name);
           HiddenField1.Value = user.UserId.ToString();
	} 
        
    }
}