using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Administrator_manageRoles : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rolesLB.DataSource = Roles.GetAllRoles();
            rolesLB.DataBind();
        }
    }
    protected void addbtn_Click(object sender, EventArgs e)
    {
        try
        {
            Roles.CreateRole(newRoleTB.Text.Trim());
            statusLBL.ForeColor = System.Drawing.Color.Green;
            statusLBL.Text = "User Created Successfully";
            // rebind to display updated data
            Server.Transfer("~/Administrator/manageRoles.aspx");
        }
        catch (Exception ex)
        {
            statusLBL.ForeColor = System.Drawing.Color.Red;
            statusLBL.Text = "Error: Role was not created due to: " + ex.Message;
            
        }
    }
}