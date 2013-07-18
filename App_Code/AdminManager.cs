using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using petomaccallumModel;
/// <summary>
/// Summary description for AdminManager
/// </summary>
public class AdminManager
{
    public AdminManager() { }
    /// <summary>
    /// gets all users on the system
    /// </summary>
    /// <returns></returns>
    public MembershipUserCollection getUsers() {
  
        return Membership.GetAllUsers();
    }
    /// <summary>
    /// get users for role
    /// </summary>
    /// <param name="role"></param>
    ///// <returns></returns>
    public MembershipUser[] getUsersForRole(string role)
    {
        // get all users, else get only the users for the selected role
        if (role == "All Users")
        {
            MembershipUserCollection users = Membership.GetAllUsers();
            MembershipUser[] m = new MembershipUser[users.Count];
            // convert collection to array
            users.CopyTo(m,0);
            return m;
        }
        else
        {
            string[] users = Roles.GetUsersInRole(role);
            MembershipUser[] m = new MembershipUser[users.Length];
            
            // get the MemberShip user instance for each username returned for role
            //for (int i = 0; i < users.Length; i++)
            //{
            //    m[i++] = Membership.GetUser(users[i]);
            //}
            int i = 0;
            foreach (string s in users)
            {
                m[i++] = Membership.GetUser(s);
            }
            return m;

        }
    }
    /// <summary>
    /// get branches
    /// </summary>
    /// <returns></returns>
    public List<string> getBranches() {
        using (var context = new PetoEntities())
        {
            return (from i in context.Branches
                   select i.name).ToList();
        }
    }
    /// <summary>
    /// creates a user and saves it to the Users table in the database
    /// </summary>
    /// <param name="user"></param>
    public void createUser(User user) {
        using (var context = new PetoEntities())
        {
            context.Users.AddObject(user);
            context.SaveChanges();
        }
    }
    /// <summary>
    /// delete a membership user
    /// </summary>
    /// <param name="user"></param>
    public void deleteUser(string UserName) {
        try
        {
            // delete the user from the API (asp_Users) table
            Membership.DeleteUser(UserName, true);
            using (var context = new PetoEntities())
            {
                User user = context.Users.FirstOrDefault(x=> x.username == UserName);
                // if a user is returned with the specified username delete that record from the Users table
                if (user != null) {
                    context.Users.DeleteObject(user);
                    context.SaveChanges();
                
                }
            }

        }
        catch (Exception )
        {
            
            throw;
        }
    }

    /// <summary>
    /// update a membership user
    /// </summary>
    /// <param name="user"></param>
    public void updateUser(Boolean IsApproved, string UserName) {
        try
        {
            // get user that matches record
            MembershipUser user = Membership.GetUser(UserName);
            
            
            // update fields
           
            user.IsApproved = IsApproved;
           
           
            Membership.UpdateUser(user);

        }
        catch (Exception ex)
        {
            
            throw;
        }
    }

    public List<string> rolesForUser(string username) {
        return Roles.GetRolesForUser(username).ToList();
    }
    /// <summary>
    /// update a employee user
    /// </summary>
    /// <param name="username"></param>
    /// <param name="fName"></param>
    /// <param name="mName"></param>
    /// <param name="lName"></param>
    /// <param name="branch"></param>
    /// <param name="manager"></param>
    public void updateEmployee(string username, string status, string branch, string manager) {
        using (var context = new PetoEntities())
        {
            User u = context.Users.FirstOrDefault(x => x.username == username);
            if (u != null) {

                if (branch != string.Empty)
                {
                    u.branch = branch; 
                }
                if (manager != string.Empty)
                {
                    u.ManagedBy = manager; 
                }
                if (u.Status != string.Empty)
                {
                    u.Status = status; 
                }
                context.SaveChanges();
            }
        }
        
    
    }

   
}