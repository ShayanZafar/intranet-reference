using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Administrator_manageUsers : System.Web.UI.Page
{
   protected string[] dbData;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rolesDDL.DataSource = Roles.GetAllRoles();
           
            //rolesDDL.Items.Add("All Users");
            rolesDDL.DataBind();
            rolesDDL.Items.Insert(0, "All Users");
        }
        

        int i = 0;
        MembershipUserCollection users = Membership.GetAllUsers();
        dbData = new string[users.Count];
        foreach (MembershipUser item in users)
        {
            dbData[i++] = item.UserName;
        }

           
         

    }

}