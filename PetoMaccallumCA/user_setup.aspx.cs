using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_setup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        

        // create system roles

        //System.Web.Security.Roles.CreateRole("Executive");
        //System.Web.Security.Roles.CreateRole("Administrator");
        //System.Web.Security.Roles.CreateRole("Director");
        //System.Web.Security.Roles.CreateRole("Associate");
        //System.Web.Security.Roles.CreateRole("Member");
        //System.Web.Security.Roles.CreateRole("Manager");


        //// exec user
        //System.Web.Security.Membership.CreateUser("ExecUser", "password!!");
        //// director user
        //System.Web.Security.Membership.CreateUser("DirectorUser", "password!!");
        //// Associate User
        //System.Web.Security.Membership.CreateUser("AssocUser", "password!!");
        //System.Web.Security.Membership.CreateUser("SpecialUser", "password!!");
        //// Administrator User
        //System.Web.Security.Membership.CreateUser("AdminUser", "password!!");
        //// Member User
        //System.Web.Security.Membership.CreateUser("MemberUser", "password!!");
        
        ////// user to role
        //System.Web.Security.Roles.AddUserToRole("DirectorUser", "Director");
        //System.Web.Security.Roles.AddUserToRole("MemberUser", "Member");
        //System.Web.Security.Roles.AddUserToRole("AssocUser", "Associate");
        //System.Web.Security.Roles.AddUserToRole("SpecialUser", "Special");
        //System.Web.Security.Roles.AddUserToRole("AdminUser", "Administrator");
        //System.Web.Security.Roles.AddUserToRole("ExecUser", "Executive");

        //System.Web.Security.Roles.CreateRole("Author");
        //System.Web.Security.Membership.CreateUser("AuthorUser", "password!!");
        //System.Web.Security.Roles.AddUserToRole("AuthorUser", "Author");

        System.Web.Security.Roles.AddUserToRole("AssocUser", "Manager");
        System.Web.Security.Roles.AddUserToRole("SpecialUser", "Manager");
        System.Web.Security.Roles.AddUserToRole("AdminUser", "Manager");
        System.Web.Security.Roles.AddUserToRole("ExecUser", "Manager");


        Response.Write("Roles Successfully Created!");        
   


      
    }
}