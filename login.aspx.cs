using System;
using System.Web.Security;
public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // after 30 seconds redirection to main website
        //Response.AddHeader("REFRESH", "120;URL=http://petomaccallum.com");
    }
    protected void Login1_LoggingIn(object sender, System.Web.UI.WebControls.LoginCancelEventArgs e)
    {
       // MembershipUser user = Membership.GetUser(Login1.UserName);
      
        
        //if (user.IsOnline)
        //{
        //    FormsAuthentication.SignOut();
        //    e.Cancel = true;
        //    
       
        //}
        //else
        //{
        //    e.Cancel = false;
        //}
    }
}