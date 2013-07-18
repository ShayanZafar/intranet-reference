using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// for stringbuilder
using System.IO;
// thumbnail
using System.Drawing;

using System.Text;
using petomaccallumModel;
/// <summary>
/// Summary description for FileManager
/// </summary>
public class FileManager
{
    System.Web.HttpApplication _context;
    string uploadPath;
    public FileManager()
    {
        _context = System.Web.HttpContext.Current.ApplicationInstance;
        
    }

    /// <summary>
    ///  FILE IO
    /// </summary>

    public string[] fileNames
    {
        get { return Directory.GetFiles(uploadPath, "*.pdf"); }
    }

    public bool uploadImageFile(ref System.Web.UI.WebControls.FileUpload fu)
    {
        bool result = false;
        string fn = fu.FileName;
        string uploadedFile = uploadPath + @"\" + fn;
        try
        {
            if (fu.HasFile)
            {
                fu.SaveAs(uploadedFile);
                // jpg to png conversion
                System.Drawing.Image img = System.Drawing.Image.FromFile(uploadedFile);
                img.Save(uploadedFile.Replace(".txt", "pdf"), System.Drawing.Imaging.ImageFormat.Png);
                result = true;
            }

        }
        catch (Exception)
        {
            result = false;
            throw;
        }
        _context.Session["fileName"] = fu.FileName;

        return result;
    }
    public bool uploadFile(ref System.Web.UI.WebControls.FileUpload fu, string role)
    {
        bool result = false;

        try
        {
            if (fu.HasFile && fu.FileContent.Length != 0)
            {
               string url = _context.Server.MapPath("~/" + role + "/Reports");
               string uploadedFile = url + @"\" + fu.FileName;
               fu.SaveAs(uploadedFile);

            }

        }
        catch (Exception)
        {
            result = false;
            throw;
        }

        return result;
    }

    public bool CopyFile(string fName, string uName)
    {
        try
        {
            string source = Path.Combine(uploadPath, fName);
            string fileContent = File.ReadAllText(source);
            string dest = Path.Combine(uploadPath, fName + ".ext");
            StreamWriter write = File.CreateText(dest);
            write.Write(fileContent);
            write.Close();
            return true;
        }
        catch (Exception )
        {
            return false;
        }
    }

    public string getFileContents(string fName)
    {
        // combine the file paths for source and destination
        string source = Path.Combine(uploadPath, fName);
        //read file content from the source file
        return File.ReadAllText(source);
    }
    public string searchFileContents(string fName, string s)
    {
        string result = "";
        string line;
        int count = 0;
        string fc = this.getFileContents(fName);

        StreamReader reader = new StreamReader(uploadPath + fName);

        while ((line = reader.ReadLine()) != null)
        {
            if (line.Contains(s))
            {
                result += "\n" + line;
                count++;
            }
        }
        result += "\n" + "  " + count + "Occurances of:" + s + " in this File: " + fName;
        return result;
    }

    public void deleteFile(string fName)
    {
        string source = Path.Combine(uploadPath, fName);
        File.Delete(source);

    }

    /// <summary>
    /// saves Document Instance to the database under the Documents table
    /// </summary>
    /// <param name="d"></param>
    public void createDocument(Document d)
    {
        using (var context = new PetoEntities())
        {
            context.Documents.AddObject(d);
            context.SaveChanges();
        }

    }



    /// <summary>
    /// gets all of the documents in the database sorted by role
    /// </summary>
    /// <returns></returns>
    public List<Document> getDocuments(string sort)
    {
        using (var context = new PetoEntities())
        {
            // break it up into a switch for determining which column to sort
            switch (sort)
            {
                case "Role":
                    return (from x in context.Documents
                            orderby x.Role
                            select x).ToList();
                    
                case "extension":
                    return (from x in context.Documents
                            orderby x.extension
                            select x).ToList();
                    
                case "DateCreated":
                     return (from x in context.Documents
                    orderby x.DateCreated
                    select x).ToList();
                    
                case "size":
                    return (from x in context.Documents
                            orderby x.size
                            select x).ToList();
                default:
                       return (from x in context.Documents
                    orderby x.DateCreated
                    select x).ToList();

            }
           
        }
    }
    /// <summary>
    /// get documents for role with sorting
    /// </summary>
    /// <param name="role"></param>
    /// <param name="sort"></param>
    /// <returns></returns>
    public List<Document> getDocumentsForRole(string role, string sort) {
        using (var context = new PetoEntities())
        {
            // break it up into a switch for determining which column to sort
            switch (sort)
            {
                case "Role":
                    return (from x in context.Documents
                            where x.Role == role
                            orderby x.Role
                            select x).ToList();

                case "extension":
                    return (from x in context.Documents
                            where x.Role == role
                            orderby x.extension
                            select x).ToList();

                case "DateCreated":
                    return (from x in context.Documents
                            where x.Role == role
                            orderby x.DateCreated
                            select x).ToList();

                case "size":
                    return (from x in context.Documents
                            where x.Role == role
                            orderby x.size
                            select x).ToList();
                default:
                    return (from x in context.Documents
                            where x.Role == role
                            orderby x.DateCreated
                            select x).ToList();

            }

        }
    
    }

    /// <summary>
    /// get all the document category names
    /// </summary>
    /// <returns></returns>
    public List<string> getCategories() {
        using (var context = new PetoEntities())
        {
            return (from i in context.DocumentCategories
                   orderby i.name
                   select i.name).ToList();
        }
    }
    /// <summary>
    /// delete a document
    /// </summary>
    /// <param name="id"></param>
    public void deleteDocument(int DocumentId) {
        using (var context = new PetoEntities())
        {
            Document d = context.Documents.FirstOrDefault(x => x.DocumentId == DocumentId);

            if (d != null)
            {
                context.Documents.DeleteObject(d);
                context.SaveChanges();
            }
        }
    }
    /// <summary>
    /// get the Document for it's ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Document documentForID(int id) {
        using (var context = new PetoEntities())
        {
            return context.Documents.FirstOrDefault(x => x.DocumentId == id);
        }
    }
    /// <summary>
    /// get the document for its' filename
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Document documentForName(string name) {
        using (var context = new PetoEntities())
        {
            return context.Documents.FirstOrDefault(x => x.name == name);
        }
    }


    /// <summary>
    /// generates a thumbnail
    /// </summary>
    /// <param name="Original"></param>
    /// <param name="Length"></param>
    /// <returns></returns>
    protected Byte[] GenerateThumbnail(byte[] Original, out int Length, int width)
    {
        // Create an image from the original digital content
        // using a "byte array to memory stream" technique
        MemoryStream msOriginal = new MemoryStream(Original);
        System.Drawing.Image imOriginal = new Bitmap(msOriginal);

        // If the image size is less than 120 pixels wide, just return it
        // It's small enough that it doesn't need thumbnailing
        if (imOriginal.Width < width)
        {
            Length = Original.Length;
            return Original;
        }
        else
        {
            // Scale it to be 120 pixels wide

            // Get the original width to height ratio
            decimal imageRatio = (decimal)imOriginal.Width / (decimal)imOriginal.Height;
            // Assume we want a thumbnail that has a 120 pixel width
            int myThumbWidth = width;
            // Calculate the new height
            int myThumbHeight = Convert.ToInt32(width / imageRatio);

            // Now, create the thumbnail image
            Bitmap tn = new Bitmap(myThumbWidth, myThumbHeight);
            // Create a graphics object to enable high-quality rendering
            Graphics g = Graphics.FromImage(tn);
            // Configure the settings of the object
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            // Make the thumbnail
            g.DrawImage
                (imOriginal, new Rectangle(0, 0, myThumbWidth, myThumbHeight),
                0, 0, imOriginal.Width, imOriginal.Height, GraphicsUnit.Pixel);
            // Release the resources
            g.Dispose();

            // Return the thumbnail using a "memory stream to byte array" technique
            MemoryStream msThumbnail = new MemoryStream();
            tn.Save(msThumbnail, System.Drawing.Imaging.ImageFormat.Png);
            Length = Convert.ToInt32(msThumbnail.Length);
            return msThumbnail.GetBuffer();
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
            return (from x in context.Documents
                    select x.name).Distinct().ToArray();
        }
    }
    
}