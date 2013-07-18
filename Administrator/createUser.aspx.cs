using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using petomaccallumModel;

public partial class Administrator_createUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            AdminManager am = new AdminManager();

            branchDDL.DataSource = am.getBranches();
            branchDDL.DataBind();

            roleDDL.DataSource = Roles.GetAllRoles();
            roleDDL.DataBind();

        }
    }
    protected void createBtn_Click(object sender, EventArgs e)
    {
        try
        {
            User user = new User();
            AdminManager am = new AdminManager();
            user.FirstName = fNameTB.Text.Trim();
            user.LastName = lNameTB.Text.Trim();
            user.MiddleName = mNameTB.Text.Trim();
            user.empNo = empNoTB.Text;
            user.branch = branchDDL.SelectedValue;
            user.Role = roleDDL.SelectedValue;
            user.username = userTB.Text.Trim();
            user.password = passwordTB.Text.Trim();

            Membership.CreateUser(user.username, user.password);
            Roles.AddUserToRole(user.username, user.Role);
            //Roles.AddUserToRole(user.username, "Member");

            // add user to manager role if they are in upper management
            if (user.Role == "Director" || user.Role == "Executive" || user.Role == "Associate")
            {
                Roles.AddUserToRole(user.username, "Manager");
            }
            am.createUser(user);
            statusLBL.ForeColor = System.Drawing.Color.Green;
            statusLBL.Text = "User Created Successfully";
        }
        catch (Exception ex)
        {
            statusLBL.ForeColor = System.Drawing.Color.Red;
            statusLBL.Text = "Error User was not created due to: " + ex.Message;
            
        }
    }
}