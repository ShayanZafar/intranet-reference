using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using petomaccallumModel;
using System.Web.Security;
public partial class Managers_viewTimesheets : System.Web.UI.Page
{
    protected string[] dbData;

    protected void Page_Load(object sender, EventArgs e)
    {
        TimesheetManager tm = new TimesheetManager();
        dbData = tm.getData();

        if (!IsPostBack)
        {
            statusDDL.DataSource = tm.getTimesheetStatus();
            statusDDL.DataBind();

            branchesDDL.DataSource = tm.getBranches();
            string username = Membership.GetUser().UserName;
            User user = tm.getEmployeeForId(username);
            //branchesDDL.SelectedValue = user.branch;
            branchesDDL.DataBind();
            datesDDL.DataSource = tm.getDates();
            datesDDL.DataBind();

            // check query strings for form data to be populated
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["reject"] == "reject")
                {
                    TimeSheet ts = tm.getTimesheetForID(Convert.ToInt32(Request.QueryString["time"]));
                    if (ts != null)
                    {
                        tm.updateTimeSheet(ts.TimeSheetId, ts.TotalHours.Value, ts.TotalDistance.Value, ts.TotalTruck.Value, ts.TotalExpenses.Value, "Rejected", ts.EmployeeComments, ts.ManagerComments);
                        statusDDL.SelectedValue = "Rejected";
                        statusDDL.DataBind();
                        GridView1.DataBind();
                    }
                }
                else if (Request.QueryString["sync"] == "sync")
                {
                    // create a connection to the database
                    SqlConnection conn = new SqlConnection("data source=USER;initial catalog=TIMESHEET_SYNC;user id=sa;password=PMLtest;multipleactiveresultsets=True");

                    int id = Convert.ToInt32(Request.QueryString["time"]);
                    petomaccallumModel.TimeSheet t = tm.getTimesheetForID(id);
                    if (t != null)
                    {
                        conn.Open();
                        SqlCommand tsStmt = conn.CreateCommand();
                        tsStmt.CommandText =
                        "INSERT INTO TimeSheet(TimeSheetId,TotalHours,TotalDistance, TotalExpenses, EmpNo, WeekEnding, ApprovedBy,EmployeeName, ManagerComments,EmployeeComments) VALUES(@TimeSheetId,@TotalHours, @TotalDistance, @TotalExpenses, @EmpNo, @WeekEnding, @ApprovedBy,@EmployeeName, @ManagerComments, @EmployeeComments)";

                        tsStmt.Parameters.AddWithValue("@TimeSheetId", t.TimeSheetId);
                        tsStmt.Parameters.AddWithValue("@TotalHours", t.TotalHours);
                        tsStmt.Parameters.AddWithValue("@TotalDistance", t.TotalDistance);
                        tsStmt.Parameters.AddWithValue("@TotalExpenses", t.TotalExpenses);
                        // get emp no
                        User emp = tm.getEmployeeForId(t.EmployeeId);

                        tsStmt.Parameters.AddWithValue("@EmpNo", emp.empNo);
                        tsStmt.Parameters.AddWithValue("@WeekEnding", t.WeekEnding);
                        tsStmt.Parameters.AddWithValue("@ApprovedBy", t.ApprovedBy);
                        tsStmt.Parameters.AddWithValue("@EmployeeName", t.EmployeeName);
                        tsStmt.Parameters.AddWithValue("@ManagerComments", t.ManagerComments);

                        tsStmt.Parameters.AddWithValue("@EmployeeComments", t.EmployeeComments);

                        tsStmt.ExecuteNonQuery();
                        tsStmt.Dispose();

                        // get manager chargeable data and map it
                        List<ManagerChargeable> mData = tm.getManagerChargeableForID(id);

                        if (mData != null)
                        {
                            // TODO: map the managerChargeable to the local db
                            try
                            {

                                // for each ManagerChargeable row for the timesheet, insert to temporary database
                                foreach (ManagerChargeable item in mData)
                                {
                                    SqlCommand stmt = conn.CreateCommand();
                                    //get projnum, name, client

                                    //get projnum, name, client
                                    stmt.CommandText =
                                    "INSERT INTO Chargeable(TimeSheetId,Day,PayRollHours,PayRollTravelDistance,PayRollAccomodation,PayRollMisc,BillingHours,BillingTravelDistance,BillingAccomodation,BillingMisc,ProjectNo,Classification,Activity,Remarks,BillingTruckDistance)" +
                                    "VALUES(@TimeSheetId,@Day,@PayRollHours,@PayRollTravelDistance, @PayRollAccomodation ,@PayRollMisc, @BillingHours, @BillingTravelDistance, @BillingAccomodation, @BillingMisc, @ProjectNo, @Classification, @Activity, @Remarks,@BillingTruckDistance)";
                                    stmt.Parameters.AddWithValue("@TimeSheetId", item.TimeSheetId);
                                    stmt.Parameters.AddWithValue("@Day", item.Day);
                                    // payroll
                                    if (item.PayRollHours.HasValue)
                                    {
                                        stmt.Parameters.AddWithValue("@PayRollHours", item.PayRollHours.Value);
                                    }
                                    else
                                    {
                                        stmt.Parameters.AddWithValue("@PayRollHours", 0);
                                    }
                                    if (item.PayRollTravelDistance.HasValue)
                                    {
                                        stmt.Parameters.AddWithValue("@PayRollTravelDistance", item.PayRollTravelDistance.Value);
                                    }
                                    else
                                    {
                                        stmt.Parameters.AddWithValue("@PayRollTravelDistance", 0);
                                    }
                                    if (item.PayRollAccomodation.HasValue)
                                    {
                                        stmt.Parameters.AddWithValue("@PayRollAccomodation", item.PayRollAccomodation.Value);
                                    }
                                    else
                                    {
                                        stmt.Parameters.AddWithValue("@PayRollAccomodation", 0);
                                    }
                                    if (item.PayRollMisc.HasValue)
                                    {
                                        stmt.Parameters.AddWithValue("@PayRollMisc", item.PayRollMisc.Value);
                                    }
                                    else
                                    {
                                        stmt.Parameters.AddWithValue("@PayRollMisc", 0);
                                    }

                                    // billing
                                    if (item.BillingHours.HasValue)
                                    {
                                        stmt.Parameters.AddWithValue("@BillingHours", item.BillingHours.Value);
                                    }
                                    if (item.BillingTravelDistance.HasValue)
                                    {
                                        stmt.Parameters.AddWithValue("@BillingTravelDistance", item.BillingTravelDistance.Value);
                                    }
                                    else
                                    {
                                        stmt.Parameters.AddWithValue("@BillingTravelDistance", 0);
                                    }

                                    if (item.BillingAccomodation.HasValue)
                                    {
                                        stmt.Parameters.AddWithValue("@BillingAccomodation", item.BillingAccomodation.Value);
                                    }
                                    else
                                    {
                                        stmt.Parameters.AddWithValue("@BillingAccomodation", 0);
                                    }

                                    if (item.BillingMisc.HasValue)
                                    {
                                        stmt.Parameters.AddWithValue("@BillingMisc", item.BillingMisc.Value);
                                    }
                                    else
                                    {
                                        stmt.Parameters.AddWithValue("@BillingMisc", 0);
                                    }
                                    // get project number
                                    string no = tm.getProjectNoForID(item.ProjectId);
                                    stmt.Parameters.AddWithValue("@ProjectNo", no);
                                    stmt.Parameters.AddWithValue("@Classification", item.Classification);
                                    stmt.Parameters.AddWithValue("@Activity", item.Activity);
                                    if (item.Remarks != null)
                                    {
                                        stmt.Parameters.AddWithValue("@Remarks", item.Remarks);
                                    }
                                    else
                                    {
                                        stmt.Parameters.AddWithValue("@Remarks", " ");
                                    }

                                    if (item.TruckDistance.HasValue)
                                    {
                                        stmt.Parameters.AddWithValue("@BillingTruckDistance", item.TruckDistance);
                                    }
                                    else
                                    {
                                        stmt.Parameters.AddWithValue("@BillingTruckDistance", 0);
                                    }


                                    stmt.ExecuteNonQuery();
                                    stmt.Dispose();
                                    stmt = null;
                                }
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }

                        List<ManagerNonChargeable> nData = tm.getManagerNonChargeableForID(id);

                        if (nData != null)
                        {
                            try
                            {

                                // save each row of non chargeable data to the sync database
                                foreach (ManagerNonChargeable item in nData)
                                {

                                    SqlCommand stmt = conn.CreateCommand();

                                    stmt.CommandText =
                                   "INSERT INTO NonChargeable (TimeSheetId,Hours,Distance,Accomodations,Misc,TypeHours,TypeExpense,Day,Remarks)VALUES(@TimeSheetId,@Hours, @Distance, @Accomodations, @Misc, @TypeHours, @TypeExpense, @Day, @Remarks)";

                                    stmt.Parameters.AddWithValue("@TimeSheetId", t.TimeSheetId);

                                    if (item.Hours.HasValue)
                                    {
                                        stmt.Parameters.AddWithValue("@Hours", item.Hours.Value);
                                    }
                                    else
                                    {
                                        stmt.Parameters.AddWithValue("@Hours", 0);
                                    }

                                    if (item.Distance.HasValue)
                                    {
                                        stmt.Parameters.AddWithValue("@Distance", item.Distance.Value);
                                    }
                                    else
                                    {
                                        stmt.Parameters.AddWithValue("@Distance", 0);
                                    }

                                    if (item.Accomodations.HasValue)
                                    {
                                        stmt.Parameters.AddWithValue("@Accomodations", item.Accomodations.Value);
                                    }
                                    else
                                    {
                                        stmt.Parameters.AddWithValue("@Accomodations", 0);
                                    }
                                    if (item.Misc.HasValue)
                                    {
                                        stmt.Parameters.AddWithValue("@Misc", item.Misc.Value);
                                    }
                                    else
                                    {
                                        stmt.Parameters.AddWithValue("@Misc", 0);
                                    }


                                    if (item.TypeHours != null)
                                    {
                                        stmt.Parameters.AddWithValue("@TypeHours", item.TypeHours);
                                    }
                                    else
                                    {
                                        stmt.Parameters.AddWithValue("@TypeHours", " ");
                                    }
                                    if (item.TypeExpense != null)
                                    {
                                        stmt.Parameters.AddWithValue("@TypeExpense", item.TypeExpense);
                                    }
                                    else
                                    {
                                        stmt.Parameters.AddWithValue("@TypeExpense", " ");

                                    }
                                    stmt.Parameters.AddWithValue("@Day", item.Day);


                                    if (item.Remarks != null)
                                    {
                                        stmt.Parameters.AddWithValue("@Remarks", item.Remarks);
                                    }
                                    else
                                    {
                                        stmt.Parameters.AddWithValue("@Remarks", " ");
                                    }
                                    stmt.ExecuteNonQuery();
                                    stmt.Dispose();
                                    stmt = null;
                                }
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }

                        // Synchronize the Lab and Density Tests
                        List<ManagerTest> tests = tm.getManagerTestsForTimeSheet(id);
                        if (tests != null)
                        {
                            try
                            {
                                foreach (ManagerTest item in tests)
                                {
                                    SqlCommand stmt = conn.CreateCommand();
                                    stmt.CommandText = "INSERT INTO ManagerTest(LabTest,DensityTest,TimeSheetId,Day) VALUES(@LabTest,@DensityTest,@TimeSheetId,@Day)";
                                    if (item.LabTest.HasValue)
                                    {
                                        stmt.Parameters.AddWithValue("@LabTest", item.LabTest);
                                    }
                                    else
                                    {
                                        stmt.Parameters.AddWithValue("@LabTest", 0);
                                    }
                                    if (item.DensityTest.HasValue)
                                    {
                                        stmt.Parameters.AddWithValue("@DensityTest", item.DensityTest);
                                    }
                                    else
                                    {
                                        stmt.Parameters.AddWithValue("@DensityTest", 0);
                                    }

                                    if (item.TimeSheetId > 0)
                                    {
                                        stmt.Parameters.AddWithValue("@TimeSheetId", item.TimeSheetId);
                                    }

                                    if (item.Day != string.Empty)
                                    {
                                        stmt.Parameters.AddWithValue("@Day", item.Day);
                                    }


                                    stmt.ExecuteNonQuery();
                                    stmt.Dispose();
                                    stmt = null;
                                }
                            }
                            catch (Exception)
                            {

                                throw;
                            }

                        }

                        // update timesheet
                        tm.updateTimeSheet(t.TimeSheetId, t.TotalHours.Value, t.TotalDistance.Value, t.TotalTruck.Value, t.TotalExpenses.Value, "Synchronized", " ", " ");
                        conn.Close();
                    }
                }
            }
        }


    }
    protected void statusDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (statusDDL.SelectedValue == "Updated")
        {
            // CANT SEE
            // Manage
            GridView1.Columns[1].Visible = false;
            // view
            GridView1.Columns[0].Visible = false;
            // reject
            GridView1.Columns[15].Visible = false;
            // sync
            GridView1.Columns[3].Visible = false;
            // accept
            GridView1.Columns[14].Visible = false;
            // Approved By
            GridView1.Columns[7].Visible = false;
            // Approved Date
            GridView1.Columns[8].Visible = false;
        
            // CAN SEE
            // Peek
            GridView1.Columns[17].Visible = true;
            // Created Date
            GridView1.Columns[4].Visible = true;
        }
        else if (statusDDL.SelectedValue == "Synchronized")
        {
            // Manage
            GridView1.Columns[1].Visible = false;
            // view
            GridView1.Columns[0].Visible = false;
  
            // sync
            GridView1.Columns[3].Visible = false;
            // accept
            GridView1.Columns[14].Visible = false;
            // Peek
            GridView1.Columns[17].Visible = false;
        }
        else if (statusDDL.SelectedValue == "Completed")
        {
                // view
            GridView1.Columns[0].Visible = false;
            // Manage
            GridView1.Columns[1].Visible = true;
            // reject
            GridView1.Columns[15].Visible = true;
            // Peek
            GridView1.Columns[17].Visible = false;
            // accept
            GridView1.Columns[14].Visible = false;
            // Approved By
            GridView1.Columns[7].Visible = false;
            // Approved Date
            GridView1.Columns[8].Visible = false;
            // sync
            GridView1.Columns[3].Visible = false;
            // Created Date
            GridView1.Columns[4].Visible = true;
        }
        else if (statusDDL.SelectedValue == "All TimeSheets")
        {
            // reject
            GridView1.Columns[15].Visible = true;
            // Manage
            GridView1.Columns[1].Visible = true;
            // View
            GridView1.Columns[0].Visible = false;
            // sync
            GridView1.Columns[3].Visible = false;
            // Peek
            GridView1.Columns[16].Visible = false;
            // accept
            GridView1.Columns[14].Visible = false;
            // Approved By
            GridView1.Columns[7].Visible = false;
            // Approved Date
            GridView1.Columns[8].Visible = false;
            // Created Date
            GridView1.Columns[4].Visible = true;


        }
        else if (statusDDL.SelectedValue == "Updated - Manager") {
            // Approved By
            GridView1.Columns[7].Visible = false;
            // Approved Date
            GridView1.Columns[8].Visible = false;
            // view
            GridView1.Columns[0].Visible = false;
            // Peek
            GridView1.Columns[17].Visible = false;
            // sync
            GridView1.Columns[3].Visible = false;
            // accept
            GridView1.Columns[14].Visible = false;
            // reject
            GridView1.Columns[15].Visible = false;
            // Manage
            GridView1.Columns[1].Visible = true;
        }
        else if (statusDDL.SelectedValue == "Started")
        {
            // reject
            GridView1.Columns[15].Visible = false;
            // Manage
            GridView1.Columns[1].Visible = false;
            // View
            GridView1.Columns[0].Visible = false;
            // sync
            GridView1.Columns[3].Visible = false;
            // Peek
            GridView1.Columns[16].Visible = false;
            // accept
            GridView1.Columns[14].Visible = false;
            // Approved By
            GridView1.Columns[7].Visible = false;
            // Approved Date
            GridView1.Columns[8].Visible = false;
            // Created Date
            GridView1.Columns[4].Visible = true;
        }
        else if (statusDDL.SelectedValue == "Approved")
        {
            GridView1.Columns[3].Visible = true;
            // Approved By
            GridView1.Columns[7].Visible = true;
            // Approved Date
            GridView1.Columns[8].Visible = true;
            // Modified Date
            GridView1.Columns[5].Visible = false;
            // Created Date
            GridView1.Columns[4].Visible = false;

            if (Roles.IsUserInRole("Branch Manager"))
            {
                // Manage
                GridView1.Columns[1].Visible = true;
            }
            else
            {
                // Manage
                GridView1.Columns[1].Visible = false;
            }
            // View
            GridView1.Columns[0].Visible = false;
            // Reject
            GridView1.Columns[15].Visible = false;
            // Peek
            GridView1.Columns[17].Visible = false;
            // accept
            GridView1.Columns[14].Visible = false;
            // accept
            GridView1.Columns[16].Visible = false;

        }
        else if (statusDDL.SelectedValue == "Rejected")
        {
          
            // view 
            GridView1.Columns[0].Visible = false;
            // manage
            GridView1.Columns[1].Visible = false;
            // sync
            GridView1.Columns[3].Visible = false;
            // Approved By
            GridView1.Columns[7].Visible = false;
            // Approved Date
            GridView1.Columns[8].Visible = false;
            GridView1.Columns[5].Visible = false;
            GridView1.Columns[6].Visible = false;
            // Reject
            GridView1.Columns[15].Visible = false;
        
            // accept
            GridView1.Columns[14].Visible = false;
            // Peek
            GridView1.Columns[17].Visible = true;
            // Created Date
            GridView1.Columns[4].Visible = true;
        }
    }
}