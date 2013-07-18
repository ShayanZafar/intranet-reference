using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using petomaccallumModel;

/// <summary>
/// Summary description for ReportManager
/// </summary>
public class ReportManager
{
    public ReportManager(){}

    /// <summary>
    /// gets all of the Reports in the database sorted by role
    /// </summary>
    /// <returns></returns>
    public List<Report> getReports(string sort,string category)
    {
        using (var context = new PetoEntities())
        {
            if (category == "All Reports")
            {
                switch (sort)
                {
                    case "Role":
                        return (from x in context.Reports

                                orderby x.Role
                                select x).ToList();

                    case "Extension":
                        return (from x in context.Reports

                                orderby x.Extension
                                select x).ToList();

                    case "DateCreated":
                        return (from x in context.Reports

                                orderby x.DateCreated
                                select x).ToList();

                    case "Size":
                        return (from x in context.Reports

                                orderby x.Size
                                select x).ToList();
                    case "ReportDate":
                        return (from x in context.Reports

                                orderby x.ReportDate
                                select x).ToList();
                    case "Category":
                        return (from x in context.Reports

                                orderby x.Category
                                select x).ToList();
                    case "Type":
                        return (from x in context.Reports

                                orderby x.Type
                                select x).ToList();
                    default:
                        return (from x in context.Reports
                                orderby x.DateCreated
                                select x).ToList();
                }
            }
            else if (category == "Project Profile")
            {
                switch (sort)
                {
                    case "Role":
                        return (from x in context.Reports
                                where x.Category == "Project Profile"
                                orderby x.Role
                                select x).ToList();

                    case "Extension":
                        return (from x in context.Reports
                                where x.Category == "Project Profile"
                                orderby x.Extension
                                select x).ToList();

                    case "DateCreated":
                        return (from x in context.Reports
                                where x.Category == "Project Profile"
                                orderby x.DateCreated
                                select x).ToList();

                    case "Size":
                        return (from x in context.Reports
                                where x.Category == "Project Profile"
                                orderby x.Size
                                select x).ToList();
                    case "ReportDate":
                        return (from x in context.Reports
                                where x.Category == "Project Profile"
                                orderby x.ReportDate
                                select x).ToList();
                    case "Category":
                        return (from x in context.Reports
                                where x.Category == "Project Profile"
                                orderby x.Category
                                select x).ToList();
                    case "Type":
                        return (from x in context.Reports
                                where x.Category == "Project Profile"
                                orderby x.Type
                                select x).ToList();
                    default:
                        return (from x in context.Reports
                                where x.Category == "Project Profile"
                                orderby x.Name
                                select x).ToList();
                }
            }
            else
            {


                // break it up into a switch for determining which column to sort
                switch (sort)
                {
                    case "Role":
                        return (from x in context.Reports
                                where x.Category == category
                                orderby x.Role
                                select x).ToList();

                    case "Extension":
                        return (from x in context.Reports
                                where x.Category == category
                                orderby x.Extension
                                select x).ToList();

                    case "DateCreated":
                        return (from x in context.Reports
                                where x.Category == category
                                orderby x.DateCreated
                                select x).ToList();

                    case "Size":
                        return (from x in context.Reports
                                where x.Category == category
                                orderby x.Size
                                select x).ToList();
                    case "ReportDate":
                        return (from x in context.Reports
                                where x.Category == category
                                orderby x.ReportDate
                                select x).ToList();
                    case "Category":
                        return (from x in context.Reports
                                where x.Category == category
                                orderby x.Category
                                select x).ToList();
                    case "Type":
                        return (from x in context.Reports
                                where x.Category == category
                                orderby x.Type
                                select x).ToList();
                    default:
                        return (from x in context.Reports
                                where x.Category == category
                                orderby x.DateCreated
                                select x).ToList();

                }
            }
        }
    }

    /// <summary>
    /// get all the report category names
    /// </summary>
    /// <returns></returns>
    public List<string> getCategories()
    {
        using (var context = new PetoEntities())
        {
            return (from i in context.ReportCategories
                    orderby i.name
                    select i.name).ToList();
        }
    }
    /// <summary>
    /// saves Report Instance to the database under the Reports table
    /// </summary>
    /// <param name="d"></param>
    public void createReport(Report r)
    {
        using (var context = new PetoEntities())
        {
            context.Reports.AddObject(r);
            context.SaveChanges();
        }

    }
    /// <summary>
    /// delete a Report
    /// </summary>
    /// <param name="id"></param>
    public void deleteReport(int ReportId)
    {
        using (var context = new PetoEntities())
        {
            Report d = context.Reports.FirstOrDefault(x => x.ReportId == ReportId);

            if (d != null)
            {
                context.Reports.DeleteObject(d);
                context.SaveChanges();
            }
        }
    }
    /// <summary>
    /// get the Report for it's ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Report ReportForID(int id)
    {
        using (var context = new PetoEntities())
        {
            return context.Reports.FirstOrDefault(x => x.ReportId == id);
        }
    }
    /// <summary>
    /// get the Report for its' filename
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Report ReportForName(string name)
    {
        using (var context = new PetoEntities())
        {
            return context.Reports.FirstOrDefault(x => x.Name == name);
        }
    }
    /// <summary>
    /// verifies if the selected category and selected permissions match for the filename
    /// </summary>
    /// <param name="name"></param>
    /// <param name="category"></param>
    /// <param name="permission"></param>
    /// <returns></returns>
    public string reportUploadContextValidator(string name, string category, string permission) {
        Boolean result = false;

        switch (category)
        {
            case "Financial Statements":
                if (permission == "Executive" && !name.Contains("fullset"))
                {
                    return "This File is not meant to be visible to " + permission;
                }
                else if (permission == "Director" && !name.Contains("Directors"))
                {
                    return "This File is not meant to be visible to " + permission;
                }
                else if (permission == "Associate" && !name.Contains("Associates"))
                {
                    return "This File is not meant to be visible to " + permission;
                }
                else {
                    return "";
                }
               
            default:
                break;
        }

        return "";
    }
    /// <summary>
    /// monthYear is in format similar to: "Apr-2012"
    /// </summary>
    /// <param name="monthYear"></param>
    /// <returns></returns>
    public Report getMonthlyReport(string monthYear, string type, string role) {
        using (var context = new PetoEntities())
        {
            return context.Reports.FirstOrDefault(x=> x.ReportDate == monthYear && x.Role == role && x.Type == type);
        }
    }
    /// <summary>
    /// get reports for a specific role, sorted on specified columns
    /// </summary>
    /// <param name="role"></param>
    /// <param name="sort"></param>
    /// <returns></returns>
    public List<Report> getReportsForRole(string role, string sort){
        using(var context = new PetoEntities())
	    {
            // break it up into a switch for determining which column to sort
            switch (sort)
            {
                case "Role":
                    return (from x in context.Reports
                            orderby x.Role
                            where x.Role == role
                            select x).ToList();

                case "Extension":
                    return (from x in context.Reports
                            orderby x.Extension
                            where x.Role == role 
                            select x).ToList();

                case "DateCreated":
                    return (from x in context.Reports
                            orderby x.DateCreated
                            where x.Role == role 
                            select x).ToList();

                case "Size":
                    return (from x in context.Reports
                            orderby x.Size
                            where x.Role == role 
                            select x).ToList();
                case "ReportDate":
                    return (from x in context.Reports
                            orderby x.ReportDate
                            where x.Role == role 
                            select x).ToList();
                case "Type":
                    return (from x in context.Reports
                            orderby x.ReportDate
                            where x.Role == role 
                            select x).ToList();
                case "Category":
                    return (from x in context.Reports
                            orderby x.Category
                            where x.Role == role
                            select x).ToList();
                default:
                    return (from x in context.Reports
                            orderby x.DateCreated
                            where x.Role == role 
                            select x).ToList();

            }

	    }
    }
    /// <summary>
    /// This method is used for getting reports for upper level managers (Executive, Director and Associate)
    /// get reports under the category with sorting under the specified columns and the reports for the logged in upper management account
    /// also display the reports under the shared 'Manager' role for all of these upper level managers based on category
    /// </summary>
    /// <param name="Category"></param>
    /// <param name="sort"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    public List<Report> reportsForCategory(string Category, string sort, string role) {
        using (var context = new PetoEntities())
        {
            // break it up into a switch for determining which column to sort
            switch (sort)
            {
                case "Category":
                    return (from x in context.Reports
                            orderby x.Category
                            where (x.Category == Category && x.Role == role) || (x.Role == "Manager" && x.Category == Category)
                            select x).ToList();

                case "Extension":
                    return (from x in context.Reports
                            orderby x.Extension
                            where (x.Category == Category && x.Role == role) || (x.Role == "Manager" && x.Category == Category)
                            select x).ToList();

                case "DateCreated":
                    return (from x in context.Reports
                            orderby x.DateCreated
                            where (x.Category == Category && x.Role == role) || (x.Role == "Manager" && x.Category == Category)
                            select x).ToList();

                case "Size":
                    return (from x in context.Reports
                            orderby x.Size
                            where (x.Category == Category && x.Role == role) || (x.Role == "Manager" && x.Category == Category)
                            select x).ToList();
                case "ReportDate":
                    return (from x in context.Reports
                            orderby x.ReportDate
                            where (x.Category == Category && x.Role == role) || (x.Role == "Manager" && x.Category == Category)
                            select x).ToList();
                case "Type":
                    return (from x in context.Reports
                            orderby x.ReportDate
                            where (x.Category == Category && x.Role == role) || (x.Role == "Manager" && x.Category == Category)
                            select x).ToList();
                case "Role":
                    return (from x in context.Reports
                            orderby x.Role
                            where (x.Category == Category && x.Role == role) || (x.Role == "Manager" && x.Category == Category)
                            select x).ToList();
                default:

                        return (from x in context.Reports
                                orderby x.DateCreated
                                where (x.Category == Category && x.Role == role) || (x.Role == "Manager" && x.Category == Category)
                                select x).ToList();
                    
            }
                   
        }
    }

    /// <summary>
    /// This method gets search data for predictive search datasource based on role
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public string[] getData(string role)
    {
        using (var context = new PetoEntities())
        {
            // get an array of names which will be in format: [<Name> MMM-YYYY]
            return (from x in context.Reports
                    where x.Role == role
                    select x.Name).Distinct().ToArray();
        }
    }

    /// <summary>
    /// This method gets search data for predictive search datasource based on role
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public string[] getData()
    {
        using (var context = new PetoEntities())
        {
            // get an array of names which will be in format: [<Name> MMM-YYYY]
            return (from x in context.Reports
                    select x.Name).Distinct().ToArray();
        }
    }
}