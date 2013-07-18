using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using petomaccallumModel;
using System.Web.Security;

public partial class ui_mp_site : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Label1.ForeColor = System.Drawing.Color.Navy;
        //Label1.Text = String.Format("{0:f}", DateTime.Now);
  
    }
    protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
    {
        MembershipUser user = Membership.GetUser(false);
        FormsAuthentication.SignOut();
        user.LastActivityDate = DateTime.UtcNow.AddMinutes(-(Membership.UserIsOnlineTimeWindow + 1));
        Membership.UpdateUser(user);
    }
}
