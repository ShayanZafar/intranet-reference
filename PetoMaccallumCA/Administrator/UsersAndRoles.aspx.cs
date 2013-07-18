using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
public partial class Administrator_UsersAndRoles : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            rolesDDL.DataSource = Roles.GetAllRoles();
            rolesDDL.DataBind();

            managerDDL.DataSource = Roles.GetUsersInRole("Branch Manager");
            managerDDL.DataBind();
        }
    }
    protected void addBtn_Click(object sender, EventArgs e)
    {
        try
        {
            Roles.AddUserToRole(userDDL.SelectedValue,rolesDDL.SelectedValue);
            statusLBL.ForeColor = System.Drawing.Color.Green;
            statusLBL.Text = "User: " + userDDL.SelectedValue + " Successfully added to: " + rolesDDL.SelectedValue;
        }
        catch (System.Exception ex)
        {
            statusLBL.ForeColor = System.Drawing.Color.Red;
            statusLBL.Text = "Error: User was not added to role due to " + ex.Message;
        }
    }
    protected void removeBtn_Click(object sender, EventArgs e)
    {
        try
        {
            Roles.RemoveUserFromRole(userDDL.SelectedValue,userRolesLB.SelectedValue);
            statusLBL.ForeColor = System.Drawing.Color.Green;
            statusLBL.Text = "User: " + userDDL.SelectedValue + " is no longer a " + userRolesLB.SelectedValue;
        }
        catch (System.Exception ex)
        {
            statusLBL.ForeColor = System.Drawing.Color.Red;
            statusLBL.Text = "Error: Role could not be removed due to " + ex.Message;
        }
    }
    protected void managerBtn_Click(object sender, EventArgs e)
    {
        try
        {
            AdminManager am = new AdminManager();
            am.updateEmployee(userDDL.SelectedValue, "", "", managerDDL.SelectedValue);
            statusLBL.Text = "User: " + userDDL.SelectedValue + " is now Managed By " + managerDDL.SelectedValue;
        }
        catch (Exception)
        {
            statusLBL.Text = "User is Already Managed By Someone. Or something went wrong.";
            
        }
    }
}