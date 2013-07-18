using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Web.UI;
using petomaccallumModel;

public partial class Member_createTimeSheet : System.Web.UI.Page
{
    // CONSTANTS
    protected decimal MAX_WEEKLY_HOURS = 60;

    protected decimal OVERTIME_THRESHOLD = 12;

    // JS helper Page Variables
    protected string dialogTitle;
  
    protected string msg;

    // page scope variables to access in markup
    protected string[] projectNoDB;
    protected List<int> rowIDs = new List<int>();
    protected string[] dbData;
    protected int timesheetID;
    protected int totalDistance;
    protected decimal totalExpenses;
    protected decimal totalHours;
    protected int aID;

    protected void chargeableBtn_Click(object sender, EventArgs e)
    {
        if (weekEndingTB.Text != string.Empty && timesheetID > 0)
        {
            bool valid = true;
            int cDist = Convert.ToInt32(totalDistanceLBL.Text), tDist = Convert.ToInt32(totalTruckLBL.Text);
            decimal cHours = Convert.ToDecimal(totalHoursLBL.Text), cExpense = Convert.ToDecimal(Convert.ToDecimal(totalExpensesLBL.Text));
            TimesheetManager tm = new TimesheetManager();

            // sunday
            if (sunCHoursTB.Text != string.Empty)
            {
                if (!hoursValidator(sunCHoursTB.Text, "Sunday", false))
                {
                    valid = false;
                }
            }
            if (valid)
            {
                timesheetStatusLBL.Text = tm.createChargeable(sunCHoursTB.Text, sunAccomTB.Text, sunDistTB.Text, sunTruckDistTB.Text, sunMiscTB.Text, "Sunday", projectNumTB.Text, classificationDDL.SelectedValue,
                              activitiesDDL.SelectedValue, timesheetID, sunCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
            }

            // monday

            if (monCHoursTB.Text != string.Empty)
            {
                if (!hoursValidator(monCHoursTB.Text, "Monday", false))
                {
                    valid = false;
                }
            }
            if (valid)
            {
                timesheetStatusLBL.Text = tm.createChargeable(monCHoursTB.Text, monAccomTB.Text, monDistTB.Text, monTruckDistTB.Text, monMiscTB.Text, "Monday", projectNumTB.Text, classificationDDL.SelectedValue,
                         activitiesDDL.SelectedValue, timesheetID, monCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
            }

            // tuesday
            if (tuesCHoursTB.Text != string.Empty)
            {
                if (!hoursValidator(tuesCHoursTB.Text, "Tuesday", false))
                {
                    valid = false;
                }
            }
            if (valid)
            {
                timesheetStatusLBL.Text = tm.createChargeable(tuesCHoursTB.Text, tuesAccomTB.Text, tuesDistTB.Text, tuesTruckDistTB.Text, tuesMiscTB.Text, "Tuesday", projectNumTB.Text, classificationDDL.SelectedValue,
               activitiesDDL.SelectedValue, timesheetID, tuesCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
            }

            // wednesday
            if (wedsCHoursTB.Text != string.Empty)
            {
                if (!hoursValidator(wedsCHoursTB.Text, "Wednesday", false))
                {
                    valid = false;
                }
            }
            if (valid)
            {
                timesheetStatusLBL.Text = tm.createChargeable(wedsCHoursTB.Text, wedsAccomTB.Text, wedsDistTB.Text, wedsTruckDistTB.Text, wedsMiscTB.Text, "Wednesday", projectNumTB.Text, classificationDDL.SelectedValue,
                       activitiesDDL.SelectedValue, timesheetID, wedsCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
            }

            // thursday

            if (thursCHoursTB.Text != string.Empty)
            {
                if (!hoursValidator(thursCHoursTB.Text, "Thursday", false))
                {
                    valid = false;
                }
            }
            if (valid)
            {
                timesheetStatusLBL.Text = tm.createChargeable(thursCHoursTB.Text, thursAccomTB.Text, thursDistTB.Text, thursTruckDistTB.Text, thursMiscTB.Text, "Thursday", projectNumTB.Text, classificationDDL.SelectedValue,
                           activitiesDDL.SelectedValue, timesheetID, thursCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
            }

            // friday
            if (friCHoursTB.Text != string.Empty)
            {
                if (!hoursValidator(friCHoursTB.Text, "Friday", false))
                {
                    valid = false;
                }
            }
            if (valid)
            {
                timesheetStatusLBL.Text = tm.createChargeable(friCHoursTB.Text, friAccomTB.Text, friDistTB.Text, friTruckDistTB.Text, friMiscTB.Text, "Friday", projectNumTB.Text, classificationDDL.SelectedValue,
                     activitiesDDL.SelectedValue, timesheetID, friCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
            }

            // saturday
            if (satCHoursTB.Text != string.Empty)
            {
                if (!hoursValidator(satCHoursTB.Text, "Saturday", false))
                {
                    valid = false;
                }
            }
            if (valid)
            {
                timesheetStatusLBL.Text = tm.createChargeable(satCHoursTB.Text, satAccomTB.Text, satDistTB.Text, satTruckDistTB.Text, satMiscTB.Text, "Saturday", projectNumTB.Text, classificationDDL.SelectedValue,
                   activitiesDDL.SelectedValue, timesheetID, satCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
            }

            // add to totalized value labels
            totalHoursLBL.Text = cHours.ToString("0.#");
            totalExpensesLBL.Text = cExpense.ToString("0.####");
            totalDistanceLBL.Text = cDist.ToString();
            totalTruckLBL.Text = tDist.ToString();

            // if TOTAL WEEKLY hours exceed 60 throw a dialog
            if (Convert.ToDecimal(totalHoursLBL.Text) > MAX_WEEKLY_HOURS)
            {
                // throw a dialog
                dialogTitle = "Regular Weekly Hours Exceeded";
                msg = "You Worked more than 60 hrs per Week. A separate Form must be filled out to accomodate overtime";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "projectNumInvalid", "throwDialog();", true);
            }

            // update timesheet
            tm.updateTimeSheet(timesheetID, cHours, cDist, tDist, cExpense, "Updated", " ", " ");

            projectNameTB.Enabled = false;
            clientNameTB.Enabled = false;

            // clear form fields
            clearChargeable();
            // update totals
            updateChargeableTotals();
            updateLabTotals();
            updateNonChargeableTotals();
            // chargeable fields
            clientNameTB.Text = string.Empty;
            projectNameTB.Text = string.Empty;
            projectNumTB.Text = string.Empty;
            classificationDDL.Items.Clear();
            activitiesDDL.Items.Clear();

            // update gridview
            summaryGV.DataBind();
        }
        else
        {
            dialogTitle = "Missing Weekending Date";
            msg = "The timesheet needs a verified Weekending Date in order to proceed";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "projectNumInvalid", "throwDialog();", true);
        }
    }

    protected void classificationDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimesheetManager tm = new TimesheetManager();
        activitiesDDL.DataSource = tm.getActivityForClassification(classificationDDL.SelectedValue);
        activitiesDDL.DataBind();
        activitiesDDL.Focus();

        // re-populate the dates
        ScriptManager.RegisterStartupScript(this, this.GetType(), "populate", "populate();", true);
        // activate corresponding accordion panel
        aID = 0;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "toggleAccordion", "accordionToggle();", true);
    }

    protected void clearChargeable()
    {
        // clear form fields

        // sunday
        sunCHoursTB.Text = string.Empty;
        sunAccomTB.Text = string.Empty;
        sunDistTB.Text = string.Empty;
        sunMiscTB.Text = string.Empty;
        sunTruckDistTB.Text = string.Empty;
        sunCRemarksTB.Text = string.Empty;

        // monday
        monCHoursTB.Text = string.Empty;
        monAccomTB.Text = string.Empty;
        monDistTB.Text = string.Empty;
        monTruckDistTB.Text = string.Empty;
        monMiscTB.Text = string.Empty;
        monCRemarksTB.Text = string.Empty;

        // tuesday
        tuesCHoursTB.Text = string.Empty;
        tuesAccomTB.Text = string.Empty;
        tuesDistTB.Text = string.Empty;
        tuesTruckDistTB.Text = string.Empty;
        tuesMiscTB.Text = string.Empty;
        tuesCRemarksTB.Text = string.Empty;

        // wedssday
        wedsCHoursTB.Text = string.Empty;
        wedsAccomTB.Text = string.Empty;
        wedsDistTB.Text = string.Empty;
        wedsTruckDistTB.Text = string.Empty;
        wedsMiscTB.Text = string.Empty;
        wedsCRemarksTB.Text = string.Empty;

        // thursday
        thursCHoursTB.Text = string.Empty;
        thursAccomTB.Text = string.Empty;
        thursDistTB.Text = string.Empty;
        thursTruckDistTB.Text = string.Empty;
        thursMiscTB.Text = string.Empty;
        thursCRemarksTB.Text = string.Empty;

        // friday
        friCHoursTB.Text = string.Empty;
        friAccomTB.Text = string.Empty;
        friDistTB.Text = string.Empty;
        friTruckDistTB.Text = string.Empty;
        friMiscTB.Text = string.Empty;
        friCRemarksTB.Text = string.Empty;

        //saturday
        satCHoursTB.Text = string.Empty;
        satAccomTB.Text = string.Empty;
        satDistTB.Text = string.Empty;
        satTruckDistTB.Text = string.Empty;
        satMiscTB.Text = string.Empty;
        satCRemarksTB.Text = string.Empty;
    }

    protected void clearChargeableBtn_Click(object sender, EventArgs e)
    {
        updateChargeableBtn.Visible = false;
        chargeableBtn.Visible = true;

        // clear form fields
        clearChargeable();

        // chargeable fields
        clientNameTB.Text = string.Empty;
        projectNameTB.Text = string.Empty;
        projectNumTB.Text = string.Empty;
        classificationDDL.Items.Clear();
        activitiesDDL.Items.Clear();
    }

    protected void clearNonChargeable()
    {
        // sunday
        sunNCHoursTB.Text = string.Empty;
        sunNCAccomTB.Text = string.Empty;
        sunNCDistanceTB.Text = string.Empty;
        sunNCMiscTB.Text = string.Empty;
        sunNuclearDensityTestTB.Text = string.Empty;
        sunNCRemarksTB.Text = string.Empty;

        // monday
        monNCHoursTB.Text = string.Empty;
        monNCAccomTB.Text = string.Empty;
        monNCDistanceTB.Text = string.Empty;
        monNCMiscTB.Text = string.Empty;
        monNuclearDensityTestTB.Text = string.Empty;
        monNCRemarksTB.Text = string.Empty;

        // tuesday
        tuesNCHoursTB.Text = string.Empty;
        tuesNCAccomTB.Text = string.Empty;
        tuesNCDistanceTB.Text = string.Empty;
        tuesNCMiscTB.Text = string.Empty;
        tuesNuclearDensityTestTB.Text = string.Empty;
        tuesNCRemarksTB.Text = string.Empty;

        // wednessday
        wedsNCHoursTB.Text = string.Empty;
        wedsNCAccomTB.Text = string.Empty;
        wedsNCDistanceTB.Text = string.Empty;
        wedsNCMiscTB.Text = string.Empty;
        wedsNuclearDensityTestTB.Text = string.Empty;
        wedsNCRemarksTB.Text = string.Empty;

        // thursday
        thursNCHoursTB.Text = string.Empty;
        thursNCAccomTB.Text = string.Empty;
        thursNCDistanceTB.Text = string.Empty;
        thursNCMiscTB.Text = string.Empty;
        thursNuclearDensityTestTB.Text = string.Empty;
        thursNCRemarksTB.Text = string.Empty;

        // friday
        friNCHoursTB.Text = string.Empty;
        friNCAccomTB.Text = string.Empty;
        friNCDistanceTB.Text = string.Empty;
        friNCMiscTB.Text = string.Empty;
        friNuclearDensityTestTB.Text = string.Empty;
        friNCRemarksTB.Text = string.Empty;

        //saturday
        satNCHoursTB.Text = string.Empty;
        satNCAccomTB.Text = string.Empty;
        satNCDistanceTB.Text = string.Empty;
        satNCMiscTB.Text = string.Empty;
        satNuclearDensityTestTB.Text = string.Empty;
        satNCRemarksTB.Text = string.Empty;
        // close panels
        NonChargeExpensesPanel.Visible = false;
        NonChargeHoursPanel.Visible = false;
    }

    protected void clearNonChargeBtn_Click(object sender, EventArgs e)
    {
        clearNonChargeable();

        // reset drop down list to first index
        detailsDDL.SelectedIndex = 0;
        specifyDDL.SelectedIndex = 0;
        updateNonChargeableBtn.Visible = false;
        nonChargeBtn.Visible = true;

        // activate corresponding accordion panel
        aID = 2;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "toggleAccordion", "accordionToggle();", true);
    }

    protected void completeBtn_Click(object sender, EventArgs e)
    {
        try
        {
            // if chargeable or non chargeable are being edited prompt user to save or update changes, else execute intended code
            if (activitiesDDL.Items.Count > 0 || specifyDDL.SelectedValue != "Select" || detailsDDL.SelectedValue != "Select")
            {
                // throw a dialog
                msg = "You have Un-Saved work on this Page, Please save,update or Clear the form";
                dialogTitle = "Warning: Un-saved Work";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "unsavedwork", "throwDialog();", true);
            }
            else
            {
                TimesheetManager tm = new TimesheetManager();
                TimeSheet t = tm.getTimesheetForID(timesheetID);
                // if the timesheet has been approved or completed, do not save the timesheet
                if (t.Status != "Approved" && t.Status != "Completed")
                {
                    tm.updateTimeSheet(timesheetID, Convert.ToDecimal(totalHoursLBL.Text), Convert.ToInt32(totalDistanceLBL.Text), Convert.ToInt32(totalTruckLBL.Text),
                        Convert.ToDecimal(totalExpensesLBL.Text), "Completed", empCommentsTB.Text, " ");

                    // copy all of the timesheet's contents to managerial versions
                    tm.copyTimesheet(timesheetID);

                    dialogTitle = "Timesheet Complete";
                    msg = "Timesheet Completed and submitted to manager for Approval. You may view but not Re-submit this Timesheet";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "timesheetSubmitted", "throwDialog();", true);
                }
                else
                {
                    dialogTitle = "Timesheet Complete";
                    msg = "Timesheet has already been Completed or Approved. It cannot be re-submitted. Please Speak to Manager";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "timesheetSubmitted", "throwDialog();", true);
                }

                //  Server.Transfer("createTimeSheet.aspx");
            }
        }
        catch (Exception)
        {
            // throw a dialog
            msg = "Your TimeSheet was not Approved. Select a Weekending Date";
            dialogTitle = "TimeSheet not Approved";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "weekending", "throwDialog();", true);
        }
    }

    protected void detailsDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (detailsDDL.SelectedIndex != 0)
        {
            NonChargeExpensesPanel.Visible = true;
        }
        else
        {
            NonChargeExpensesPanel.Visible = false;
        }
        // activate corresponding accordion panel
        aID = 2;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "toggleAccordion", "accordionToggle();", true);
    }

    // get project no for ID
    protected string getProjectNum(object idObj)
    {
        if (string.IsNullOrEmpty(idObj.ToString()))
            return null;

        int id = Convert.ToInt32(idObj.ToString());
        TimesheetManager tm = new TimesheetManager();
        return tm.getProjectNoForID(id);
    }

    protected void nonChargeBtn_Click(object sender, EventArgs e)
    {
        if (weekEndingTB.Text != string.Empty && timesheetID > 0)
        {
            bool valid = true;
            int nCDist = Convert.ToInt32(totalDistanceLBL.Text);
            decimal nCHours = Convert.ToDecimal(totalHoursLBL.Text), nCExpense = Convert.ToDecimal(totalExpensesLBL.Text);

            TimesheetManager tm = new TimesheetManager();

            // sunday
            if (sunNCHoursTB.Text != string.Empty)
            {
                if (!hoursValidator(sunNCHoursTB.Text, "Sunday", false))
                {
                    valid = false;
                }
            }
            if (valid)
            {
                timesheetStatusLBL.Text = tm.createNonChargeable(sunNCHoursTB.Text, sunNCAccomTB.Text, sunNCDistanceTB.Text, sunNCMiscTB.Text, "Sunday",
                         detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, sunNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);
            }

            // monday

            if (monNCHoursTB.Text != string.Empty)
            {
                if (!hoursValidator(monNCHoursTB.Text, "Monday", false))
                {
                    valid = false;
                }
            }
            if (valid)
	            {
		            timesheetStatusLBL.Text = tm.createNonChargeable(monNCHoursTB.Text, monNCAccomTB.Text, monNCDistanceTB.Text, monNCMiscTB.Text, "Monday",
                       detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, monNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);
	            }

            // tuesday

            if (tuesNCHoursTB.Text != string.Empty)
            {
                if (!hoursValidator(tuesNCHoursTB.Text, "Tuesday", false))
                {
                    valid = false;
                }
            }
                 if (valid)
	            {
		            timesheetStatusLBL.Text = tm.createNonChargeable(tuesNCHoursTB.Text, tuesNCAccomTB.Text, tuesNCDistanceTB.Text, tuesNCMiscTB.Text, "Tuesday",
                    detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, tuesNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);
	            }

            // wednessday
            if (wedsNCHoursTB.Text != string.Empty)
            {
                if (!hoursValidator(wedsNCHoursTB.Text, "Wednesday", false))
                {
                    valid = false;
                }
            }

                    if (valid)
	{
		timesheetStatusLBL.Text = tm.createNonChargeable(wedsNCHoursTB.Text, wedsNCAccomTB.Text, wedsNCDistanceTB.Text, wedsNCMiscTB.Text, "Wednesday",
            detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, tuesNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);
	}

            // thursday
            if (thursNCHoursTB.Text != string.Empty)
            {
                if (!hoursValidator(thursNCHoursTB.Text, "Thursday", false))
                {
                    valid = false;
                      }
            }
                    if (valid)
	{
		timesheetStatusLBL.Text = tm.createNonChargeable(thursNCHoursTB.Text, thursNCAccomTB.Text, thursNCDistanceTB.Text, thursNCMiscTB.Text, "Thursday",
            detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, thursNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);
	}

            // friday
            if (friNCHoursTB.Text != string.Empty)
            {
                if (!hoursValidator(friNCHoursTB.Text, "Friday", false))
                {
                        }
            }
                    if (valid)
	{
		timesheetStatusLBL.Text = tm.createNonChargeable(friNCHoursTB.Text, friNCAccomTB.Text, friNCDistanceTB.Text, friNCMiscTB.Text, "Friday",
             detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, friNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);
	}

            // saturday
            if (satNCHoursTB.Text != string.Empty)
            {
                if (!hoursValidator(satNCHoursTB.Text, "Saturday", false))
                {
                    valid = false;
                       }
            }
                    if (valid)
	{
		timesheetStatusLBL.Text = tm.createNonChargeable(satNCHoursTB.Text, satNCAccomTB.Text, satNCDistanceTB.Text, satNCMiscTB.Text, "Saturday",
               detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, satNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);
	}

            // add the totalized labels except truck which does not apply to non-chargeable
            totalHoursLBL.Text = nCHours.ToString("0.#");
            totalExpensesLBL.Text = nCExpense.ToString("0.####");
            totalDistanceLBL.Text = nCDist.ToString();
            // if hours exceed 60 throw a dialog
            if (Convert.ToDecimal(totalHoursLBL.Text) > MAX_WEEKLY_HOURS)
            {
                // throw a dialog
                dialogTitle = "Regular Hours Exceeded";
                msg = "A separate Form must be filled out to accomodate overtime";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "projectNumInvalid", "throwDialog();", true);
            }

            // update timesheet
            tm.updateTimeSheet(timesheetID, nCHours, nCDist, -1, nCExpense, "Updated", " ", " ");

            // update gridview
            summaryGV.DataBind();

            // populate generic non-chargeable drop down list elements
            specifyDDL.DataSource = tm.getWorkType();
            specifyDDL.DataBind();
            specifyDDL.Items.Insert(0, "Select");
            detailsDDL.DataSource = tm.getExpenses();
            detailsDDL.DataBind();
            detailsDDL.Items.Insert(0, "Select");

            // clear form fields
            clearNonChargeable();
            // update totals

            updateNonChargeableTotals();
            // reset drop down list to first index
            detailsDDL.SelectedIndex = 0;
            specifyDDL.SelectedIndex = 0;
            NonChargeHoursPanel.Visible = false;
            NonChargeExpensesPanel.Visible = false;
            updateNonChargeableBtn.Visible = false;
            nonChargeBtn.Visible = true;
        }
        else
        {
            dialogTitle = "Missing Weekending Date";
            msg = "The timesheet needs a verified Weekending Date in order to proceed";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "projectNumInvalid", "throwDialog();", true);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        dbData = new string[7] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        // Fields to populate when user visits this page: Employee ID, Employee Name, and Branch
        TimesheetManager tm = new TimesheetManager();

        // re-highlight rows if possible
        HighLightDeletedRows();
        if (!IsPostBack)
        {
           
            verifyBtn.Enabled = true;
            // get and populate user information
            MembershipUser loggedInUser = Membership.GetUser();
            User employee = tm.getEmployeeForId(loggedInUser.UserName);
            empIdTB.Text = employee.empNo;
            nameTB.Text = employee.FirstName + " " + employee.MiddleName + " " + employee.LastName;
            departmentTB.Text = employee.branch;

            // populate generic non-chargeable drop down list elements
            specifyDDL.DataSource = tm.getWorkType();
            specifyDDL.DataBind();
            specifyDDL.Items.Insert(0, "Select");
            detailsDDL.DataSource = tm.getExpenses();
            detailsDDL.DataBind();
            detailsDDL.Items.Insert(0, "Select");

            // sub-menu's of non-chargeable section
            NonChargeExpensesPanel.Visible = false;
            NonChargeHoursPanel.Visible = false;
            detailsDDL.SelectedIndex = 0;
            specifyDDL.SelectedIndex = 0;

            // check query strings for form data to be populated
            if (Request.QueryString.Count > 0)
            {
                string timesheet = Request.QueryString["time"];
                string expense = Request.QueryString["expense"];
                string hours = Request.QueryString["hours"];
                string project = Request.QueryString["project"];
                string classification = Request.QueryString["class"];
                string activity = Request.QueryString["activity"];
                string delete = Request.QueryString["delete"];
                int chID = Convert.ToInt32(Request.QueryString["charge"]);
                int nonChID = Convert.ToInt32(Request.QueryString["nonCh"]);
                int lab = Convert.ToInt32(Request.QueryString["test"]);
                // reset weekending dates
                ScriptManager.RegisterStartupScript(this, this.GetType(), "populate", "populate();", true);

                int id = Convert.ToInt32(timesheet);
                timesheetID = id;
                HiddenField1.Value = id.ToString();

                // VIEW ONLY from a MANAGER's perspective.
                if (Request.QueryString["view"] == "view") {
                    verifyBtn.Enabled = false;
                    chargeableBtn.Enabled = false;
                    nonChargeBtn.Enabled = false;
                    labTestsBtn.Enabled = false;
                    // disable edit and delete columns
                    summaryGV.Columns[0].Visible = false;
                    summaryGV.Columns[1].Visible = false;
                    summaryGV.Columns[0].Visible = false;
                    summaryGV.Columns[1].Visible = false;
                    string username = Membership.GetUser().UserName;

                    TimeSheet ts = tm.getTimesheetForID(timesheetID);
                    User user = tm.getEmployeeForId(ts.EmployeeId);
                    departmentTB.Text = user.branch;
                    empIdTB.Text = user.empNo;
                    nameTB.Text = user.FirstName + " " + user.MiddleName + " " + user.LastName;
                }
                // display lab test hours
                if (lab > 0 && delete != "delete")
                {
                    List<EmpTest> tests = tm.getEmpTestsForTimeSheet(id);
                    updateLabTestsBtn.Visible = true;
                    labTestsBtn.Visible = false;
                    aID = 1;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toggleAccordion", "accordionToggle();", true);
                    foreach (EmpTest item in tests)
                    {
                        if (item.Day == "Sunday")
                        {
                            sunLabTB.Text = item.LabTest.ToString();
                            sunNuclearDensityTestTB.Text = item.DensityTest.ToString();
                        }
                        else if (item.Day == "Monday")
                        {
                            monLabTB.Text = item.LabTest.ToString();
                            monNuclearDensityTestTB.Text = item.DensityTest.ToString();
                        }
                        else if (item.Day == "Tuesday")
                        {
                            tuesLabTB.Text = item.LabTest.ToString();
                            tuesNuclearDensityTestTB.Text = item.DensityTest.ToString();
                        }
                        else if (item.Day == "Wednesday")
                        {
                            wedsLabTB.Text = item.LabTest.ToString();
                            wedsNuclearDensityTestTB.Text = item.DensityTest.ToString();
                        }
                        else if (item.Day == "Thursday")
                        {
                            thursLabTB.Text = item.LabTest.ToString();
                            thursNuclearDensityTestTB.Text = item.DensityTest.ToString();
                        }
                        else if (item.Day == "Friday")
                        {
                            friLabTB.Text = item.LabTest.ToString();
                            friNuclearDensityTestTB.Text = item.DensityTest.ToString();
                        }
                        else if (item.Day == "Saturday")
                        {
                            satLabTB.Text = item.LabTest.ToString();
                            satNuclearDensityTestTB.Text = item.DensityTest.ToString();
                        }
                    }
                }

                // set static properties on the webpage
                weekEndingTB.Text = tm.getWeekendingForTimeSheet(id);

                // delete summary non-chargeable and chargeable
                if (delete == "delete")
                {
                    rowIDs.Add(summaryGV.SelectedIndex);
                    HighLightDeletedRows();
                    //tm.deleteSummary(nonChID, chID, id, lab, false);

                    summaryGV.Focus();
                }
                // if there is a query string, load the totals from the corresponding timesheet
                updateTimeSheetTotals();
                // this conditional structure determines what module to load based on query string values
                // Project Only
                if (Request.QueryString["projectOnly"] != null)
                {
                    int projectID = Convert.ToInt32(project);
                    Project p = tm.getProjectForID(projectID);
                    projectNumTB.Text = p.ProjectNo;
                    projectNameTB.Text = p.ProjectName;
                    clientNameTB.Text = p.ClientName;
                    classificationDDL.DataSource = tm.getClassificationForProject(p.ProjectNo);
                    classificationDDL.DataBind();
                    classificationDDL.SelectedValue = classification;

                    projectNumTB.Enabled = true;
                    aID = 0;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toggleAccordion", "accordionToggle();", true);
                }
                // chargeable
                else if (project != null && project != "0")
                {
                    aID = 0;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toggleAccordion", "accordionToggle();", true);

                    // populate the project information fields
                    int projectID = Convert.ToInt32(project);
                    Project p = tm.getProjectForID(projectID);
                    if (p != null)
                    {
                        projectNumTB.Text = p.ProjectNo;
                        projectNameTB.Text = p.ProjectName;
                        clientNameTB.Text = p.ClientName;
                        classificationDDL.DataSource = tm.getClassificationForProject(p.ProjectNo);
                        classificationDDL.DataBind();
                        classificationDDL.SelectedValue = classification;
                        activitiesDDL.DataSource = tm.getActivityForClassification(classification);
                        activitiesDDL.DataBind();
                        activitiesDDL.SelectedValue = activity;
                        projectNumTB.Enabled = true;

                        // populate the chargeable fields based on the classification and activity selected
                        List<ChargeableJob> cJobs = tm.getChargeableJobsForTimesheetID(timesheetID);

                        if (chID > 0)
                        {
                            cJobs.Add(tm.getChargeForID(chID));
                        }

                        if (cJobs != null)
                        {
                            chargeableBtn.Visible = false;
                            updateChargeableBtn.Visible = true;

                            foreach (var item in cJobs)
                            {
                                if (item.Day == "Sunday" && item.Classification == classification && item.Activity == activity)
                                {
                                    if (item.Accomodations.HasValue)
                                    {
                                        sunAccomTB.Text = item.Accomodations.Value.ToString("0.00##");
                                    }
                                    sunCHoursTB.Text = item.EmpHours.ToString("0.0");
                                    sunDistTB.Text = item.TravelDistance.ToString();
                                    sunTruckDistTB.Text = item.TruckDistance.ToString();
                                    if (item.Misc.HasValue)
                                    {
                                        sunMiscTB.Text = item.Misc.Value.ToString("0.00##");
                                    }
                                    sunCRemarksTB.Text = item.Remarks;
                                }
                                else if (item.Day == "Monday" && item.Classification == classification && item.Activity == activity)
                                {
                                    if (item.Accomodations.HasValue)
                                    {
                                        monAccomTB.Text = item.Accomodations.Value.ToString("0.00##");
                                    }
                                    monCHoursTB.Text = item.EmpHours.ToString("0.0");
                                    monDistTB.Text = item.TravelDistance.ToString();
                                    monTruckDistTB.Text = item.TruckDistance.ToString();
                                    if (item.Misc.HasValue)
                                    {
                                        monMiscTB.Text = item.Misc.Value.ToString("0.00##");
                                    }
                                    monCRemarksTB.Text = item.Remarks;
                                }
                                else if (item.Day == "Tuesday" && item.Classification == classification && item.Activity == activity)
                                {
                                    if (item.Accomodations.HasValue)
                                    {
                                        tuesAccomTB.Text = item.Accomodations.Value.ToString("0.00##");
                                    }
                                    tuesCHoursTB.Text = item.EmpHours.ToString("0.0");
                                    tuesDistTB.Text = item.TravelDistance.ToString();
                                    tuesTruckDistTB.Text = item.TruckDistance.ToString();
                                    if (item.Misc.HasValue)
                                    {
                                        tuesMiscTB.Text = item.Misc.Value.ToString("0.00##");
                                    }
                                    tuesCRemarksTB.Text = item.Remarks;
                                }
                                else if (item.Day == "Wednesday" && item.Classification == classification && item.Activity == activity)
                                {
                                    if (item.Accomodations.HasValue)
                                    {
                                        wedsAccomTB.Text = item.Accomodations.Value.ToString("0.00##");
                                    }
                                    wedsCHoursTB.Text = item.EmpHours.ToString("0.0");
                                    wedsDistTB.Text = item.TravelDistance.ToString();
                                    wedsTruckDistTB.Text = item.TruckDistance.ToString();
                                    if (item.Misc.HasValue)
                                    {
                                        wedsMiscTB.Text = item.Misc.Value.ToString("0.00##");
                                    }
                                    wedsCRemarksTB.Text = item.Remarks;
                                }
                                else if (item.Day == "Thursday" && item.Classification == classification && item.Activity == activity)
                                {
                                    if (item.Accomodations.HasValue)
                                    {
                                        thursAccomTB.Text = item.Accomodations.Value.ToString("0.00##");
                                    }
                                    thursCHoursTB.Text = item.EmpHours.ToString("0.0");
                                    thursDistTB.Text = item.TravelDistance.ToString();
                                    thursTruckDistTB.Text = item.TruckDistance.ToString();
                                    if (item.Misc.HasValue)
                                    {
                                        thursMiscTB.Text = item.Misc.Value.ToString("0.00##");
                                    }
                                    thursCRemarksTB.Text = item.Remarks;
                                }
                                else if (item.Day == "Friday" && item.Classification == classification && item.Activity == activity)
                                {
                                    if (item.Accomodations.HasValue)
                                    {
                                        friAccomTB.Text = item.Accomodations.Value.ToString("0.00##");
                                    }
                                    friCHoursTB.Text = item.EmpHours.ToString("0.0");
                                    friDistTB.Text = item.TravelDistance.ToString();
                                    friTruckDistTB.Text = item.TruckDistance.ToString();
                                    if (item.Misc.HasValue)
                                    {
                                        friMiscTB.Text = item.Misc.Value.ToString("0.00##");
                                    }
                                    friCRemarksTB.Text = item.Remarks;
                                }
                                else if (item.Day == "Saturday" && item.Classification == classification && item.Activity == activity)
                                {
                                    if (item.Accomodations.HasValue)
                                    {
                                        satAccomTB.Text = item.Accomodations.Value.ToString("0.00##");
                                    }
                                    satCHoursTB.Text = item.EmpHours.ToString("0.0");
                                    satDistTB.Text = item.TravelDistance.ToString();
                                    satTruckDistTB.Text = item.TruckDistance.ToString();
                                    if (item.Misc.HasValue)
                                    {
                                        satMiscTB.Text = item.Misc.Value.ToString("0.00##");
                                    }
                                    satCRemarksTB.Text = item.Remarks;
                                }
                            }
                        }
                    }
                }
                // Non-Chargeable
                else if (expense != "Select" || hours != "Select")
                {
                    aID = 2;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toggleAccordion", "accordionToggle();", true);
                    // set the dropdownlists and display the different panels accordingly
                    if (hours != string.Empty)
                    {
                        specifyDDL.SelectedValue = hours;
                        NonChargeHoursPanel.Visible = true;
                    }
                    else
                    {
                        specifyDDL.SelectedIndex = 0;
                        NonChargeHoursPanel.Visible = false;
                    }

                    if (expense != string.Empty)
                    {
                        detailsDDL.SelectedValue = expense;
                        NonChargeExpensesPanel.Visible = true;
                    }
                    else
                    {
                        detailsDDL.SelectedIndex = 0;
                        NonChargeExpensesPanel.Visible = false;
                    }

                    List<NonChargeable> nCh = tm.getNonChargeableForTimesheetID(timesheet, expense, hours);
                    if (nCh != null)
                    {
                        // adjust buttons
                        nonChargeBtn.Visible = false;
                        updateNonChargeableBtn.Visible = true;
                        // populate the non chargeable text fields based on the expense and type of hours selected
                        foreach (var item in nCh)
                        {
                            if (item.Day == "Sunday")
                            {
                                if (item.Accomodation.HasValue)
                                {
                                    sunNCAccomTB.Text = item.Accomodation.Value.ToString("0.00##");
                                }
                                sunNCHoursTB.Text = item.Hours.ToString("0.0");
                                sunNCDistanceTB.Text = item.Distance.ToString();
                                sunTruckDistTB.Text = item.TruckDistance.ToString();
                                if (item.Misc.HasValue)
                                {
                                    sunNCMiscTB.Text = item.Misc.Value.ToString("0.00##");
                                }

                                sunNCRemarksTB.Text = item.Remarks;
                            }
                            else if (item.Day == "Monday")
                            {
                                if (item.Accomodation.HasValue)
                                {
                                    monNCAccomTB.Text = item.Accomodation.Value.ToString("0.00##");
                                }
                                monNCHoursTB.Text = item.Hours.ToString("0.0");
                                monNCDistanceTB.Text = item.Distance.ToString();
                                monTruckDistTB.Text = item.TruckDistance.ToString();
                                if (item.Misc.HasValue)
                                {
                                    monNCMiscTB.Text = item.Misc.Value.ToString("0.00##");
                                }

                                monNCRemarksTB.Text = item.Remarks;
                            }
                            else if (item.Day == "Tuesday")
                            {
                                if (item.Accomodation.HasValue)
                                {
                                    tuesNCAccomTB.Text = item.Accomodation.Value.ToString("0.00##");
                                }
                                tuesNCHoursTB.Text = item.Hours.ToString("0.0");
                                tuesNCDistanceTB.Text = item.Distance.ToString();
                                tuesTruckDistTB.Text = item.TruckDistance.ToString();
                                if (item.Misc.HasValue)
                                {
                                    tuesNCMiscTB.Text = item.Misc.Value.ToString("0.00##");
                                }

                                tuesNCRemarksTB.Text = item.Remarks;
                            }
                            else if (item.Day == "Wednesday")
                            {
                                if (item.Accomodation.HasValue)
                                {
                                    wedsNCAccomTB.Text = item.Accomodation.Value.ToString("0.00##");
                                }
                                wedsNCHoursTB.Text = item.Hours.ToString("0.0");
                                wedsNCDistanceTB.Text = item.Distance.ToString();
                                wedsTruckDistTB.Text = item.TruckDistance.ToString();
                                if (item.Misc.HasValue)
                                {
                                    wedsNCMiscTB.Text = item.Misc.Value.ToString("0.00##");
                                }
                                wedsNCRemarksTB.Text = item.Remarks;
                            }
                            else if (item.Day == "Thursday")
                            {
                                if (item.Accomodation.HasValue)
                                {
                                    thursNCAccomTB.Text = item.Accomodation.Value.ToString("0.00##");
                                }
                                thursNCHoursTB.Text = item.Hours.ToString("0.0");
                                thursNCDistanceTB.Text = item.Distance.ToString();
                                thursTruckDistTB.Text = item.TruckDistance.ToString();
                                if (item.Misc.HasValue)
                                {
                                    thursNCMiscTB.Text = item.Misc.Value.ToString("0.00##");
                                }
                                thursNCRemarksTB.Text = item.Remarks;
                            }
                            else if (item.Day == "Friday")
                            {
                                if (item.Accomodation.HasValue)
                                {
                                    friNCAccomTB.Text = item.Accomodation.Value.ToString("0.00##");
                                }
                                friNCHoursTB.Text = item.Hours.ToString("0.0");
                                friNCDistanceTB.Text = item.Distance.ToString();
                                friTruckDistTB.Text = item.TruckDistance.ToString();
                                if (item.Misc.HasValue)
                                {
                                    friNCMiscTB.Text = item.Misc.Value.ToString("0.00##");
                                }
                                friNCRemarksTB.Text = item.Remarks;
                            }
                            else if (item.Day == "Saturday")
                            {
                                if (item.Accomodation.HasValue)
                                {
                                    satNCAccomTB.Text = item.Accomodation.Value.ToString("0.00##");
                                }
                                satNCHoursTB.Text = item.Hours.ToString("0.0");
                                satNCDistanceTB.Text = item.Distance.ToString();
                                satTruckDistTB.Text = item.TruckDistance.ToString();
                                satNCRemarksTB.Text = item.Remarks;
                                if (item.Misc.HasValue)
                                {
                                    satNCMiscTB.Text = item.Misc.Value.ToString("0.00##");
                                }
                            }
                        }
                    }
                }
            }
        }
        // check to see if weekending is specified, if it is get timesheetID set it and update the gridviews
        if (weekEndingTB.Text != string.Empty)
        {
            // re-populate the dates
            ScriptManager.RegisterStartupScript(this, this.GetType(), "populate", "populate();", true);
            // try and get the timesheet for the logged in user
            int id = tm.idForDate(weekEndingTB.Text, tm.idForUsername(Membership.GetUser().UserName));
            if (id != 0)
            {
                timesheetID = id;
                HiddenField1.Value = id.ToString();
                // update totals
                updateTimeSheetTotals();
                summaryGV.DataBind();
            }
        }
        // populate the predictive text arrays
        projectNoDB = tm.getProjectNumbers();
    }

    protected void projectNumTB_TextChanged(object sender, EventArgs e)
    {
        if (projectNumTB.Text != string.Empty)
        {
            string numFormat = "[0-9]{2}[TBHKPDY]{1}[MFXE]{1}[0-9]{3}[A-Z]*";
            // uppercase the letters if possible
            projectNumTB.Text = projectNumTB.Text.ToUpper();

            // check to see if the project number follows the correct format
            if (Regex.IsMatch(projectNumTB.Text, numFormat))
            {
                TimesheetManager tm = new TimesheetManager();
                Project p = tm.getProjectForID(projectNumTB.Text.Trim());
                classificationDDL.DataSource = tm.getClassificationForProject(projectNumTB.Text.Trim());
                classificationDDL.DataBind();
                classificationDDL.Items.Insert(0, "Select");
                classificationDDL.Focus();

                // if project is null get all classifications
                if (p != null)
                {
                    projectNameTB.Text = p.ProjectName;
                    clientNameTB.Text = p.ClientName;
                }
                else
                {
                    projectNameTB.Text = " ";
                    clientNameTB.Text = " ";
                    // get all classifications if project was not matched
                    classificationDDL.DataSource = tm.getClassifications();
                    classificationDDL.DataBind();
                    classificationDDL.Items.Insert(0, "Select");
                }
                chargeableBtn.Visible = true;
            }
            else
            {
                TimesheetManager tm = new TimesheetManager();
                // throw a dialog
                msg = "Project Number is not in Correct Format i.e: 11PM121(ABC)";
                dialogTitle = "Project Number Format";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "projectNumInvalid", "throwDialog();", true);
                // get all classifications
                classificationDDL.DataSource = tm.getClassifications();
                classificationDDL.DataBind();
                classificationDDL.Items.Insert(0, "Select");
            }
            // re-populate the dates
            ScriptManager.RegisterStartupScript(this, this.GetType(), "populate", "populate();", true);
        }
    }

    protected void specifyDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (specifyDDL.SelectedIndex != 0)
        {
            NonChargeHoursPanel.Visible = true;
        }
        else
        {
            NonChargeHoursPanel.Visible = false;
        }
        // activate corresponding accordion panel
        aID = 2;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "toggleAccordion", "accordionToggle();", true);
    }

    protected void updateChargeableBtn_Click(object sender, EventArgs e)
    {
        TimesheetManager tm = new TimesheetManager();
        bool result, valid = true;
        int count = 0, cDist = Convert.ToInt32(totalDistanceLBL.Text), tDist = Convert.ToInt32(totalTruckLBL.Text);
        decimal cHours = Convert.ToDecimal(totalHoursLBL.Text), cExpense = Convert.ToDecimal(Convert.ToDecimal(totalExpensesLBL.Text)); ;
        int id = tm.idForProjectNo(projectNumTB.Text);

        // sunday

        if (sunCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(sunCHoursTB.Text, "Sunday", true))
            {
                valid = false;
            }
        }

        if (valid)
        {
            result = tm.updateChargeable(sunCHoursTB.Text, sunAccomTB.Text, sunDistTB.Text, sunTruckDistTB.Text, sunMiscTB.Text, "Sunday", id, classificationDDL.SelectedValue,
        activitiesDDL.SelectedValue, timesheetID, sunCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);

            if (result)
            {
                count++;
            }
            else
            {
                timesheetStatusLBL.Text = tm.createChargeable(sunCHoursTB.Text, sunAccomTB.Text, sunDistTB.Text, sunTruckDistTB.Text, sunMiscTB.Text, "Sunday", projectNumTB.Text, classificationDDL.SelectedValue,
                activitiesDDL.SelectedValue, timesheetID, sunCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
            }
        }

        // monday

        if (monCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(monCHoursTB.Text, "Monday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            result = tm.updateChargeable(monCHoursTB.Text, monAccomTB.Text, monDistTB.Text, monTruckDistTB.Text, monMiscTB.Text, "Monday", id, classificationDDL.SelectedValue,
                activitiesDDL.SelectedValue, timesheetID, monCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);

            if (result)
            {
                count++;
            }
            else
            {
                timesheetStatusLBL.Text = tm.createChargeable(monCHoursTB.Text, monAccomTB.Text, monDistTB.Text, monTruckDistTB.Text, monMiscTB.Text, "Monday", projectNumTB.Text, classificationDDL.SelectedValue,
                activitiesDDL.SelectedValue, timesheetID, monCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
            }
        }

        // tuesday
        if (tuesCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(tuesCHoursTB.Text, "Tuesday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            result = tm.updateChargeable(tuesCHoursTB.Text, tuesAccomTB.Text, tuesDistTB.Text, tuesTruckDistTB.Text, tuesMiscTB.Text, "Tuesday", id, classificationDDL.SelectedValue,
              activitiesDDL.SelectedValue, timesheetID, tuesCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);

            if (result)
            {
                count++;
            }
            else
            {
                timesheetStatusLBL.Text = tm.createChargeable(tuesCHoursTB.Text, tuesAccomTB.Text, tuesDistTB.Text, tuesTruckDistTB.Text, tuesMiscTB.Text, "Tuesday", projectNumTB.Text, classificationDDL.SelectedValue,
                activitiesDDL.SelectedValue, timesheetID, tuesCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
            }
        }

        // Wednesday
        if (wedsCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(wedsCHoursTB.Text, "Wednesday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            result = tm.updateChargeable(wedsCHoursTB.Text, wedsAccomTB.Text, wedsDistTB.Text, wedsTruckDistTB.Text, wedsMiscTB.Text, "Wednesday", id, classificationDDL.SelectedValue,
               activitiesDDL.SelectedValue, timesheetID, wedsCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);

            if (result)
            {
                count++;
            }
            else
            {
                timesheetStatusLBL.Text = tm.createChargeable(wedsCHoursTB.Text, wedsAccomTB.Text, wedsDistTB.Text, wedsTruckDistTB.Text, wedsMiscTB.Text, "Wednesday", projectNumTB.Text, classificationDDL.SelectedValue,
                activitiesDDL.SelectedValue, timesheetID, wedsCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
            }
        }

        // Thursday
        if (thursCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(thursCHoursTB.Text, "Thursday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            result = tm.updateChargeable(thursCHoursTB.Text, thursAccomTB.Text, thursDistTB.Text, thursTruckDistTB.Text, thursMiscTB.Text, "Thursday", id, classificationDDL.SelectedValue,
              activitiesDDL.SelectedValue, timesheetID, thursCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);

            if (result)
            {
                count++;
            }
            else
            {
                timesheetStatusLBL.Text = tm.createChargeable(thursCHoursTB.Text, thursAccomTB.Text, thursDistTB.Text, thursTruckDistTB.Text, thursMiscTB.Text, "Thursday", projectNumTB.Text, classificationDDL.SelectedValue,
                activitiesDDL.SelectedValue, timesheetID, thursCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
            }
        }

        // friday
        if (friCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(friCHoursTB.Text, "Friday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            result = tm.updateChargeable(friCHoursTB.Text, friAccomTB.Text, friDistTB.Text, friTruckDistTB.Text, friMiscTB.Text, "Friday", id, classificationDDL.SelectedValue,
          activitiesDDL.SelectedValue, timesheetID, friCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);

            if (result)
            {
                count++;
            }
            else
            {
                timesheetStatusLBL.Text = tm.createChargeable(friCHoursTB.Text, friAccomTB.Text, friDistTB.Text, friTruckDistTB.Text, friMiscTB.Text, "Friday", projectNumTB.Text, classificationDDL.SelectedValue,
                activitiesDDL.SelectedValue, timesheetID, friCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
            }
        }

        // Saturday
        if (satCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(satCHoursTB.Text, "Saturday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            result = tm.updateChargeable(satCHoursTB.Text, satAccomTB.Text, satDistTB.Text, satTruckDistTB.Text, satMiscTB.Text, "Saturday", id, classificationDDL.SelectedValue,
                 activitiesDDL.SelectedValue, timesheetID, satCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);

            if (result)
            {
                count++;
            }
            else
            {
                timesheetStatusLBL.Text = tm.createChargeable(satCHoursTB.Text, satAccomTB.Text, satDistTB.Text, satTruckDistTB.Text, satMiscTB.Text, "Saturday", projectNumTB.Text, classificationDDL.SelectedValue,
                activitiesDDL.SelectedValue, timesheetID, satCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
            }
        }

        // update the totalized labels
        totalHoursLBL.Text = cHours.ToString("0.0");
        totalExpensesLBL.Text = cExpense.ToString("0.00##");
        totalDistanceLBL.Text = cDist.ToString();
        totalTruckLBL.Text = tDist.ToString();
        // if hours exceed 60 throw a dialog
        if (Convert.ToDecimal(totalHoursLBL.Text) > MAX_WEEKLY_HOURS)
        {
            // throw a dialog
            dialogTitle = "Regular Hours Exceeded";
            msg = "A separate Form must be filled out to accomodate overtime";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "projectNumInvalid", "throwDialog();", true);
        }
        // if hours exceed 60 throw a dialog
        if (Convert.ToDecimal(totalHoursLBL.Text) > MAX_WEEKLY_HOURS)
        {
            // throw a dialog
            dialogTitle = "Regular Hours Exceeded";
            msg = "A separate Form must be filled out to accomodate overtime";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "FillOTForm", "throwDialog();", true);
        }
        tm.updateTimeSheet(timesheetID, cHours, cDist, tDist, cExpense, "Updated", " ", " ");

        // update gridview
        summaryGV.DataBind();

        updateChargeableBtn.Visible = false;
        chargeableBtn.Visible = true;
        // update totals
        updateChargeableTotals();
        updateLabTotals();
        updateNonChargeableTotals();
        // clear form fields
        clearChargeable();

        // chargeable fields
        clientNameTB.Text = string.Empty;
        projectNameTB.Text = string.Empty;
        projectNumTB.Text = string.Empty;
        classificationDDL.Items.Clear();
        activitiesDDL.Items.Clear();
    }

    protected void updateNonChargeableBtn_Click(object sender, EventArgs e)
    {
        TimesheetManager tm = new TimesheetManager();
        bool result, valid = true;
        int count = 0, nCDist = Convert.ToInt32(totalDistanceLBL.Text), tDist = Convert.ToInt32(totalTruckLBL.Text);
        decimal nCHours = Convert.ToDecimal(totalHoursLBL.Text), nCExpense = Convert.ToDecimal(Convert.ToDecimal(totalExpensesLBL.Text));

        //sunday
        if (sunNCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(sunNCHoursTB.Text, "Sunday", true))
            {
                valid = false; 
            }
        }
        if (valid)
        {
            result = tm.updateNonChargeable(sunNCHoursTB.Text, sunNCAccomTB.Text, sunNCDistanceTB.Text, sunNCMiscTB.Text, "Sunday",
                 detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, sunNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);

            if (result)
            {
                count++;
                timesheetStatusLBL.Text = count + " Row(s) Updated Successfully";
            }
            else
            {
                timesheetStatusLBL.Text = tm.createNonChargeable(sunNCHoursTB.Text, sunNCAccomTB.Text, sunNCDistanceTB.Text, sunNCMiscTB.Text, "Sunday",
                detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, sunNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);
            } 
        }

        // monday
        if (monNCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(monNCHoursTB.Text, "Monday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            result = tm.updateNonChargeable(monNCHoursTB.Text, monNCAccomTB.Text, monNCDistanceTB.Text, monNCMiscTB.Text, "Monday",
                  detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, monNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);

            if (result)
            {
                count++;
                timesheetStatusLBL.Text = count + " Row(s) Updated Successfully";
            }
            else
            {
                timesheetStatusLBL.Text = tm.createNonChargeable(monNCHoursTB.Text, monNCAccomTB.Text, monNCDistanceTB.Text, monNCMiscTB.Text, "Monday",
                detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, monNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);
            } 
        }
         

        // tuesday
        if (tuesNCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(tuesNCHoursTB.Text, "Tuesday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            result = tm.updateNonChargeable(tuesNCHoursTB.Text, tuesNCAccomTB.Text, tuesNCDistanceTB.Text, tuesNCMiscTB.Text, "Tuesday",
                 detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, tuesNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);

            if (result)
            {
                count++;
                timesheetStatusLBL.Text = count + " Row(s) Updated Successfully";
            }
            else
            {
                timesheetStatusLBL.Text = tm.createNonChargeable(tuesNCHoursTB.Text, tuesNCAccomTB.Text, tuesNCDistanceTB.Text, tuesNCMiscTB.Text, "Tuesday",
                detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, tuesNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);
            } 
        }
        
        // wednessday
        if (wedsNCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(wedsNCHoursTB.Text, "Wednesday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            result = tm.updateNonChargeable(wedsNCHoursTB.Text, wedsNCAccomTB.Text, wedsNCDistanceTB.Text, wedsNCMiscTB.Text, "Wednesday",
            detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, wedsNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);

            if (result)
            {
                count++;
                timesheetStatusLBL.Text = count + " Row(s) Updated Successfully";
            }
            else
            {
                timesheetStatusLBL.Text = tm.createNonChargeable(wedsNCHoursTB.Text, wedsNCAccomTB.Text, wedsNCDistanceTB.Text, wedsNCMiscTB.Text, "Wednesday",
                detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, wedsNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);
            } 
        }
         
        // thursday
        if (thursNCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(thursNCHoursTB.Text, "Thursday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            result = tm.updateNonChargeable(thursNCHoursTB.Text, thursNCAccomTB.Text, thursNCDistanceTB.Text, thursNCMiscTB.Text, "Thursday",
                detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, thursNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);

            if (result)
            {
                count++;
                timesheetStatusLBL.Text = count + " Row(s) Updated Successfully";
            }
            else
            {
                timesheetStatusLBL.Text = tm.createNonChargeable(thursNCHoursTB.Text, thursNCAccomTB.Text, thursNCDistanceTB.Text, thursNCMiscTB.Text, "Thursday",
                detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, thursNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);
            } 
        }
           
        // friday
        if (friNCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(friNCHoursTB.Text, "Friday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            result = tm.updateNonChargeable(friNCHoursTB.Text, friNCAccomTB.Text, friNCDistanceTB.Text, friNCMiscTB.Text, "Friday",
            detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, friNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);

            if (result)
            {
                count++;
                timesheetStatusLBL.Text = count + " Row(s) Updated Successfully";
            }
            else
            {
                timesheetStatusLBL.Text = tm.createNonChargeable(friNCHoursTB.Text, friNCAccomTB.Text, friNCDistanceTB.Text, friNCMiscTB.Text, "Friday",
                detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, friNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);
            } 
        }
         
        // saturday
        if (satNCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(satNCHoursTB.Text, "Saturday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            result = tm.updateNonChargeable(satNCHoursTB.Text, satNCAccomTB.Text, satNCDistanceTB.Text, satNCMiscTB.Text, "Saturday",
             detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, satNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);

            if (result)
            {
                count++;
                timesheetStatusLBL.Text = count + " Row(s) Updated Successfully";
            }
            else
            {
                timesheetStatusLBL.Text = tm.createNonChargeable(satNCHoursTB.Text, satNCAccomTB.Text, satNCDistanceTB.Text, satNCMiscTB.Text, "Saturday",
                detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, satNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);
            } 
        }
          
        // update the totalized labels
        totalHoursLBL.Text = nCHours.ToString("0.0");
        totalExpensesLBL.Text = nCExpense.ToString("0.00##");
        totalDistanceLBL.Text = nCDist.ToString();

        // if hours exceed 60 throw a dialog
        if (nCHours > MAX_WEEKLY_HOURS)
        {
            // throw a dialog
            dialogTitle = "Regular Weekly Hours Exceeded";
            msg = "You Worked more than 60 hours. A separate Form must be filled out to accomodate overtime";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "projectNumInvalid", "throwDialog();", true);
        }

        tm.updateTimeSheet(timesheetID, nCHours, nCDist, tDist, nCExpense, "Updated", " ", " ");
        // update gridview
        summaryGV.DataBind();
        // clear form fields
        clearNonChargeable();
        // update totals
        updateNonChargeableTotals();
        // reset drop down list to first index
        detailsDDL.SelectedIndex = 0;
        specifyDDL.SelectedIndex = 0;
        NonChargeHoursPanel.Visible = false;
        NonChargeExpensesPanel.Visible = false;

        updateNonChargeableBtn.Visible = false;
        nonChargeBtn.Visible = true;
    }

    protected void verifyBtn_Click(object sender, EventArgs e)
    {
        // if chargeable or non chargeable are being edited prompt user to save or update changes, else execute intended code for verify
        if (activitiesDDL.Items.Count > 0 || specifyDDL.SelectedValue != "Select" || detailsDDL.SelectedValue != "Select")
        {
            // throw a dialog
            msg = "You have Un-Saved work on this Page, Please save,update or Clear the form";
            dialogTitle = "Warning: Un-saved Work";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "unsavedwork", "throwDialog();", true);
        }
        else
        {
            if (weekEndingTB.Text != string.Empty)
            {
                // disable the non chargeable hours and expense panels
                NonChargeHoursPanel.Visible = false;
                NonChargeExpensesPanel.Visible = false;
                specifyDDL.SelectedIndex = 0;
                detailsDDL.SelectedIndex = 0;

                TimesheetManager tm = new TimesheetManager();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "populate", "populate();", true);
                int userID = tm.idForUsername(Membership.GetUser().UserName);
                timesheetID = tm.idForDate(weekEndingTB.Text, userID);
                projectNumTB.Enabled = true;

                // if a timesheet does not exist for the weekending date, create a new one
                if (timesheetID == 0)
                {
                    TimeSheet t = new TimeSheet();
                    t.WeekEnding = weekEndingTB.Text;
                    t.EmployeeId = userID;
                    t.DateCreated = DateTime.Now;
                    t.DateModified = DateTime.Now;
                    t.Status = "Started";
                    t.EmployeeName = nameTB.Text;
                    t.TotalHours = 0;
                    t.TotalExpenses = 0;
                    t.TotalDistance = 0;
                    t.Branch = departmentTB.Text;
                    totalDistanceLBL.Text = "0";
                    totalExpensesLBL.Text = "0";
                    totalHoursLBL.Text = "0";
                    totalTruckLBL.Text = "0";

                    // populate global variables
                    timesheetID = tm.createTimesheet(t);

                    HiddenField1.Value = t.TimeSheetId.ToString();
                    // enable controls
                    projectNumTB.Enabled = true;
                }
                else
                {
                    TimeSheet ts = tm.getTimesheetForID(timesheetID);
                    if (ts != null)
                    {
                        if (ts.Status == "Completed" || ts.Status == "Approved" || ts.Status == "Manager - Updated" || ts.Status == "Synchronized")
                        {
                            // throw a dialog
                            dialogTitle = "Completed TimeSheet";
                            msg = "The timesheet for this weekending date has already been Completed and Approved";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "projectNumInvalid", "throwDialog();", true);

                            // disable edit and delete columns
                            summaryGV.Columns[0].Visible = false;
                            summaryGV.Columns[1].Visible = false;
                            summaryGV.Columns[0].Visible = false;
                            summaryGV.Columns[1].Visible = false;
                            // disable the save buttons
                            chargeableBtn.Visible = false;
                            nonChargeBtn.Visible = false;
                            labTestsBtn.Visible = false;
                            updateChargeableBtn.Visible = false;
                            updateNonChargeableBtn.Visible = false;
                            updateLabTestsBtn.Visible = false;

                            // update totals
                            updateTimeSheetTotals();
                        }
                        else
                        {
                            // UPDATE TOTALS
                            updateTimeSheetTotals();
                            // for gridview
                            HiddenField1.Value = timesheetID.ToString();
                            // enable controls
                            projectNumTB.Enabled = true;

                            // re-ENABLE edit and delete columns
                            summaryGV.Columns[0].Visible = true;
                            summaryGV.Columns[1].Visible = true;
                            summaryGV.Columns[0].Visible = true;
                            summaryGV.Columns[1].Visible = true;
                            // re-enable the save buttons
                            chargeableBtn.Visible = true;
                            nonChargeBtn.Visible = true;
                            labTestsBtn.Visible = true;
                        }
                    }
                }
                // re-populate the dates
                ScriptManager.RegisterStartupScript(this, this.GetType(), "populate", "populate();", true);
                summaryGV.DataBind();
            }
        }
    }

    // get totals for chargeable and final total hours
    protected void updateChargeableTotals()
    {
        TimesheetManager tm = new TimesheetManager();

        decimal[] totals = tm.getEmpChargeableTotalsForID(timesheetID);
        decimal finalTotal = 0;

        sunChargeLBL.Text = totals[0].ToString("0.0");
        sunTotalHoursLBL.Text = totals[0].ToString("0.0");
        finalTotal += totals[0];
        monChargeLBL.Text = totals[1].ToString("0.0");
        monTotalHoursLBL.Text = totals[1].ToString("0.0");
        finalTotal += totals[1];
        tuesChargeLBL.Text = totals[2].ToString("0.0");
        tuesTotalHoursLBL.Text = totals[2].ToString("0.0");
        finalTotal += totals[2];
        wedsChargeLBL.Text = totals[3].ToString("0.0");
        wedsTotalHoursLBL.Text = totals[3].ToString("0.0");
        finalTotal += totals[3];
        thursChargeLBL.Text = totals[4].ToString("0.0");
        thursTotalHoursLBL.Text = totals[4].ToString("0.0");
        finalTotal += totals[4];
        friChargeLBL.Text = totals[5].ToString("0.0");
        friTotalHoursLBL.Text = totals[5].ToString("0.0");
        finalTotal += totals[5];
        satChargeLBL.Text = totals[6].ToString("0.0");
        satTotalHoursLBL.Text = totals[6].ToString("0.0");
        finalTotal += totals[6];
        totalChargeLBL.Text = finalTotal.ToString("0.0");
    }

    // this method should be called after the updateChargeableTotal method to display the of sum the two values
    protected void updateNonChargeableTotals()
    {
        TimesheetManager tm = new TimesheetManager();
        decimal[] totals = tm.getEmpNonChargeableTotalsForID(timesheetID);
        decimal finalTotal = 0;
        sunNonChLBL.Text = totals[0].ToString("0.0");

        sunTotalHoursLBL.Text = (totals[0] + Convert.ToDecimal(sunTotalHoursLBL.Text)).ToString("0.0");

        finalTotal += totals[0];
        monNonChLBL.Text = totals[1].ToString("0.0");
        monTotalHoursLBL.Text = (totals[1] + Convert.ToDecimal(monTotalHoursLBL.Text)).ToString("0.0");

        finalTotal += totals[1];
        tuesNonChLBL.Text = totals[2].ToString("0.0");
        tuesTotalHoursLBL.Text = (totals[2] + Convert.ToDecimal(tuesTotalHoursLBL.Text)).ToString("0.0");

        finalTotal += totals[2];
        wedsNonChLBL.Text = totals[3].ToString("0.0");
        wedsTotalHoursLBL.Text = (totals[3] + Convert.ToDecimal(wedsTotalHoursLBL.Text)).ToString("0.0");

        finalTotal += totals[3];
        thursNonChLBL.Text = totals[4].ToString("0.0");
        thursTotalHoursLBL.Text = (totals[4] + Convert.ToDecimal(thursTotalHoursLBL.Text)).ToString("0.0");

        finalTotal += totals[4];
        friNonChLBL.Text = totals[5].ToString("0.0");
        friTotalHoursLBL.Text = (totals[5] + Convert.ToDecimal(friTotalHoursLBL.Text)).ToString("0.0");

        finalTotal += totals[5];
        satNonChLBL.Text = totals[6].ToString("0.0");
        satTotalHoursLBL.Text = (totals[6] + Convert.ToDecimal(satTotalHoursLBL.Text)).ToString("0.0");

        finalTotal += totals[6];
        totalNonChargeLBL.Text = finalTotal.ToString("0.0");
    }

    protected void updateLabTotals()
    {
        TimesheetManager tm = new TimesheetManager();
        decimal[] totals = tm.getLabTotalsForId(timesheetID, false);
        decimal finalTotal = 0;
        sunLabLBL.Text = totals[0].ToString("0.0");

        sunTotalHoursLBL.Text = (totals[0] + Convert.ToDecimal(sunTotalHoursLBL.Text)).ToString("0.0");

        finalTotal += totals[0];
        monLabLBL.Text = totals[1].ToString("0.0");
        monTotalHoursLBL.Text = (totals[1] + Convert.ToDecimal(monTotalHoursLBL.Text)).ToString("0.0");

        finalTotal += totals[1];
        tuesLabLBL.Text = totals[2].ToString("0.0");
        tuesTotalHoursLBL.Text = (totals[2] + Convert.ToDecimal(tuesTotalHoursLBL.Text)).ToString("0.0");

        finalTotal += totals[2];
        wedsLabLBL.Text = totals[3].ToString("0.0");
        wedsTotalHoursLBL.Text = (totals[3] + Convert.ToDecimal(wedsTotalHoursLBL.Text)).ToString("0.0");

        finalTotal += totals[3];
        thursLabLBL.Text = totals[4].ToString("0.0");
        thursTotalHoursLBL.Text = (totals[4] + Convert.ToDecimal(thursTotalHoursLBL.Text)).ToString("0.0");

        finalTotal += totals[4];
        friLabLBL.Text = totals[5].ToString("0.0");
        friTotalHoursLBL.Text = (totals[5] + Convert.ToDecimal(friTotalHoursLBL.Text)).ToString("0.0");

        finalTotal += totals[5];
        satLabLBL.Text = totals[6].ToString("0.0");
        satTotalHoursLBL.Text = (totals[6] + Convert.ToDecimal(satTotalHoursLBL.Text)).ToString("0.0");

        finalTotal += totals[6];
        totalLabLBL.Text = finalTotal.ToString("0.0");
    }

    protected void labTestsBtn_Click(object sender, EventArgs e)
    {
        decimal tHours = Convert.ToDecimal(totalHoursLBL.Text);
        bool valid = true;
        // begin to save tests if there is a timesheet id of non zero and weekending date exists
        if (weekEndingTB.Text != string.Empty && timesheetID != 0)
        {
            TimesheetManager tm = new TimesheetManager();
            // Sunday
            if (sunLabTB.Text != string.Empty && sunLabTB.Text != "0")
            {
                if (!hoursValidator(sunLabTB.Text, "Sunday", false))
                {
                    valid = false;
                }
            }
            if (valid)
            {
                tm.createLabTest("Sunday", sunNuclearDensityTestTB.Text, sunLabTB.Text, timesheetID, false, ref tHours);
                sunLabTB.Text = string.Empty;
                sunNuclearDensityTestTB.Text = string.Empty;
            }

            // Monday
            if (monLabTB.Text != string.Empty && monLabTB.Text != "0")
            {
                if (!hoursValidator(monLabTB.Text, "Monnday", false))
                {
                    valid = false;
                }
            }
            if (valid)
            {
                tm.createLabTest("Monday", monNuclearDensityTestTB.Text, monLabTB.Text, timesheetID, false, ref tHours);
                monLabTB.Text = string.Empty;
                monNuclearDensityTestTB.Text = string.Empty;
            }

            // Tuesday
            if (tuesLabTB.Text != string.Empty && tuesLabTB.Text != "0")
            {
                if (!hoursValidator(tuesLabTB.Text, "Tuesday", false))
                {
                    valid = false;
                }
            }
            if (valid)
            {
                tm.createLabTest("Tuesday", tuesNuclearDensityTestTB.Text, tuesLabTB.Text, timesheetID, false, ref tHours);
                tuesLabTB.Text = string.Empty;
                tuesNuclearDensityTestTB.Text = string.Empty;
            }

            // Wednesday
            if (wedsLabTB.Text != string.Empty && wedsLabTB.Text != "0")
            {
                if (!hoursValidator(wedsLabTB.Text, "Wednesday", false))
                {
                    valid = false;
                }
            }
            if (valid)
            {
                tm.createLabTest("Wednesday", wedsNuclearDensityTestTB.Text, wedsLabTB.Text, timesheetID, false, ref tHours);
                wedsLabTB.Text = string.Empty;
                wedsNuclearDensityTestTB.Text = string.Empty;
            }

            // Thursday
            if (thursLabTB.Text != string.Empty && thursLabTB.Text != "0")
            {
                if (!hoursValidator(thursLabTB.Text, "Thursday", false))
                {
                    valid = false;
                }
            }
            if (valid)
            {
                tm.createLabTest("Thursday", thursNuclearDensityTestTB.Text, thursLabTB.Text, timesheetID, false, ref tHours);
                thursLabTB.Text = string.Empty;
                thursNuclearDensityTestTB.Text = string.Empty;
            }

            // Friday
            if (friLabTB.Text != string.Empty && friLabTB.Text != "0")
            {
                if (!hoursValidator(friLabTB.Text, "Friday", false))
                {
                    valid = false;
                }
            }
            if (valid)
            {
                tm.createLabTest("Friday", friNuclearDensityTestTB.Text, friLabTB.Text, timesheetID, false, ref tHours);
                friLabTB.Text = string.Empty;
                friNuclearDensityTestTB.Text = string.Empty;
            }

            // Saturday
            if (satLabTB.Text != string.Empty && satLabTB.Text != "0")
            {
                if (!hoursValidator(satLabTB.Text, "Saturday", false))
                {
                    valid = false;
                }
            }
            if (valid)
            {
                tm.createLabTest("Saturday", satNuclearDensityTestTB.Text, satLabTB.Text, timesheetID, false, ref tHours);
                satLabTB.Text = string.Empty;
                satNuclearDensityTestTB.Text = string.Empty;
            }
            updateLabTotals();
            totalHoursLBL.Text = tHours.ToString("0.0");

            // update timesheet
            tm.updateTimeSheet(timesheetID, tHours, Convert.ToInt32(totalDistanceLBL.Text), Convert.ToInt32(totalTruckLBL.Text), Convert.ToDecimal(totalExpensesLBL.Text), "Updated", " ", " ");
            // update gridview
            summaryGV.DataBind();
        }
        else
        {
            dialogTitle = "Missing Weekending Date";
            msg = "The timesheet needs a verified Weekending Date in order to proceed";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "projectNumInvalid", "throwDialog();", true);
        }
    }

    protected void updateLabTestsBtn_Click(object sender, EventArgs e)
    {
        bool valid = true;
        TimesheetManager tm = new TimesheetManager();
        decimal tHours = Convert.ToDecimal(totalHoursLBL.Text);
        // try to update, if cannot update, then create new lab test
        if (sunLabTB.Text != string.Empty && sunLabTB.Text != "0")
        {
            if (!hoursValidator(sunLabTB.Text, "Sunday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            if (!tm.updateLabTest("Sunday", sunNuclearDensityTestTB.Text, sunLabTB.Text, timesheetID, false, ref tHours))
            {
                tm.createLabTest("Sunday", sunNuclearDensityTestTB.Text, sunLabTB.Text, timesheetID, false, ref tHours);
            }
            sunLabTB.Text = string.Empty;
            sunNuclearDensityTestTB.Text = string.Empty;
        }

        if (monLabTB.Text != string.Empty && monLabTB.Text != "0")
        {
            if (!hoursValidator(monLabTB.Text, "Monday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            if (!tm.updateLabTest("Monday", monNuclearDensityTestTB.Text, monLabTB.Text, timesheetID, false, ref tHours))
            {
                tm.createLabTest("Monday", monNuclearDensityTestTB.Text, monLabTB.Text, timesheetID, false, ref tHours);
            }
            monLabTB.Text = string.Empty;
            monNuclearDensityTestTB.Text = string.Empty;
        }

        if (tuesLabTB.Text != string.Empty && tuesLabTB.Text != "0")
        {
            if (!hoursValidator(tuesLabTB.Text, "Tuesday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            if (!tm.updateLabTest("Tuesday", tuesNuclearDensityTestTB.Text, tuesLabTB.Text, timesheetID, false, ref tHours))
            {
                tm.createLabTest("Tuesday", tuesNuclearDensityTestTB.Text, tuesLabTB.Text, timesheetID, false, ref tHours);
            }
            tuesLabTB.Text = string.Empty;
            tuesNuclearDensityTestTB.Text = string.Empty;
        }

        if (wedsLabTB.Text != string.Empty && wedsLabTB.Text != "0")
        {
            if (!hoursValidator(wedsLabTB.Text, "Wednesday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            if (!tm.updateLabTest("Wednesday", wedsNuclearDensityTestTB.Text, wedsLabTB.Text, timesheetID, false, ref tHours))
            {
                tm.createLabTest("Wednesday", wedsNuclearDensityTestTB.Text, wedsLabTB.Text, timesheetID, false, ref tHours);
            }
            wedsLabTB.Text = string.Empty;
            wedsNuclearDensityTestTB.Text = string.Empty;
        }

        if (thursLabTB.Text != string.Empty && thursLabTB.Text != "0")
        {
            if (!hoursValidator(thursLabTB.Text, "Thursday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            if (!tm.updateLabTest("Thursday", thursNuclearDensityTestTB.Text, thursLabTB.Text, timesheetID, false, ref tHours))
            {
                tm.createLabTest("Thursday", thursNuclearDensityTestTB.Text, thursLabTB.Text, timesheetID, false, ref tHours);
            }
            thursLabTB.Text = string.Empty;
            thursNuclearDensityTestTB.Text = string.Empty;
        }

        if (friLabTB.Text != string.Empty && friLabTB.Text != "0")
        {
            if (!hoursValidator(friLabTB.Text, "Friday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            if (!tm.updateLabTest("Friday", friNuclearDensityTestTB.Text, friLabTB.Text, timesheetID, false, ref tHours))
            {
                tm.createLabTest("Friday", friNuclearDensityTestTB.Text, friLabTB.Text, timesheetID, false, ref tHours);
            }
            friLabTB.Text = string.Empty;
            friNuclearDensityTestTB.Text = string.Empty;
        }

        if (satLabTB.Text != string.Empty && satLabTB.Text != "0")
        {
            if (!hoursValidator(satLabTB.Text, "Saturday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            if (!tm.updateLabTest("Saturday", satNuclearDensityTestTB.Text, satLabTB.Text, timesheetID, false, ref tHours))
            {
                tm.createLabTest("Saturday", satNuclearDensityTestTB.Text, satLabTB.Text, timesheetID, false, ref tHours);
            }
            satLabTB.Text = string.Empty;
            satNuclearDensityTestTB.Text = string.Empty;
        }

        totalHoursLBL.Text = tHours.ToString("0.0");
        updateLabTotals();
        // update timesheet
        tm.updateTimeSheet(timesheetID, tHours, Convert.ToInt32(totalDistanceLBL.Text), Convert.ToInt32(totalTruckLBL.Text), Convert.ToDecimal(totalExpensesLBL.Text), "Updated", " ", " ");

        summaryGV.DataBind();
        updateLabTestsBtn.Visible = false;
        labTestsBtn.Visible = true;
    }

    protected void summaryGV_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
    {
        summaryGV.Focus();
    }

    protected void summaryGV_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        summaryGV.Focus();
    }

    /// <summary>
    /// This method checks hours to see if they exceed 12 or if the daily total for the day does no exceed 24. if so user is notified once after save.
    /// returns true and will allow save if valid, false and will not allow save otherwise
    /// </summary>
    /// <param name="hours"></param>
    /// <param name="day"></param>
    /// <returns></returns>
    protected bool hoursValidator(string hours, string day, bool update)
    {
        bool overtime = false, exceedDaily = false;

        decimal userHours = Convert.ToDecimal(hours);

        if (day == "Sunday")
        {
            if (update)
            {
                // overtime
                if (userHours > OVERTIME_THRESHOLD)
                {
                    overtime = true;
                }

                // check to see if the sum of all hours exceeds 24
                if (userHours > 24)
                {
                    exceedDaily = true;
                }

            }

            else
            {
                // overtime
                if (userHours + Convert.ToDecimal(sunTotalHoursLBL.Text) > OVERTIME_THRESHOLD)
                {
                    overtime = true;
                }

                // check to see if the sum of all hours exceeds 24
                if (userHours + Convert.ToDecimal(sunTotalHoursLBL.Text) > 24)
                {
                    exceedDaily = true;
                }
            }
        }
        else if (day == "Monday")
        {
            if (update)
            {
                // overtime
                if (userHours > OVERTIME_THRESHOLD)
                {
                    overtime = true;
                }

                // check to see if the sum of all hours exceeds 24
                if (userHours > 24)
                {
                    exceedDaily = true;
                }
            }
            else
            {
                // overtime
                if (userHours + Convert.ToDecimal(monTotalHoursLBL.Text) > OVERTIME_THRESHOLD)
                {
                    overtime = true;
                }
                // check to see if the sum of all hours exceeds 24
                if (userHours + Convert.ToDecimal(monTotalHoursLBL.Text) > 24)
                {
                    exceedDaily = true;
                }
            }
        }
        else if (day == "Tuesday")
        {
            if (update)
            {
                // overtime
                if (userHours > OVERTIME_THRESHOLD)
                {
                    overtime = true;
                }

                // check to see if the sum of all hours exceeds 24
                if (userHours > 24)
                {
                    exceedDaily = true;
                }
            }

            else
            {
                // overtime
                if (userHours + Convert.ToDecimal(tuesTotalHoursLBL.Text) > OVERTIME_THRESHOLD)
                {
                    overtime = true;
                }
                // check to see if the sum of all hours exceeds 24
                if (userHours + Convert.ToDecimal(tuesTotalHoursLBL.Text) > 24)
                {
                    exceedDaily = true;
                }
            }
        }
        else if (day == "Wednesday")
        {
            if (update)
            {
                // overtime
                if (userHours > OVERTIME_THRESHOLD)
                {
                    overtime = true;
                }

                // check to see if the sum of all hours exceeds 24
                if (userHours > 24)
                {
                    exceedDaily = true;
                }

            }
            else
            {
                // overtime
                if (userHours + Convert.ToDecimal(wedsTotalHoursLBL.Text) > OVERTIME_THRESHOLD)
                {
                    overtime = true;
                }
                // check to see if the sum of all hours exceeds 24
                if (userHours + Convert.ToDecimal(wedsTotalHoursLBL.Text) > 24)
                {
                    exceedDaily = true;
                }
            }
        }
        else if (day == "Thursday")
        {
            if (update)
            {
                // overtime
                if (userHours > OVERTIME_THRESHOLD)
                {
                    overtime = true;
                }

                // check to see if the sum of all hours exceeds 24
                if (userHours > 24)
                {
                    exceedDaily = true;
                }
            }
            else
            {
                // overtime
                if (userHours + Convert.ToDecimal(thursTotalHoursLBL.Text) > OVERTIME_THRESHOLD)
                {
                    overtime = true;
                }
                // check to see if the sum of all hours exceeds 24
                if (userHours + Convert.ToDecimal(thursTotalHoursLBL.Text) > 24)
                {
                    exceedDaily = true;
                }
            }
        }
        else if (day == "Friday")
        {
            if (update)
            {
                // overtime
                if (userHours > OVERTIME_THRESHOLD)
                {
                    overtime = true;
                }

                // check to see if the sum of all hours exceeds 24
                if (userHours > 24)
                {
                    exceedDaily = true;
                }
            }
            else
            {
                // overtime
                if (userHours + Convert.ToDecimal(friTotalHoursLBL.Text) > OVERTIME_THRESHOLD)
                {
                    overtime = true;
                }
                // check to see if the sum of all hours exceeds 24
                if (userHours + Convert.ToDecimal(friTotalHoursLBL.Text) > 24)
                {
                    exceedDaily = true;
                }
            }
        }
        else if (day == "Saturday")
        {
            if (update)
            {
                // overtime
                if (userHours > OVERTIME_THRESHOLD)
                {
                    overtime = true;
                }

                // check to see if the sum of all hours exceeds 24
                if (userHours > 24)
                {
                    exceedDaily = true;
                }

            }

            else
            {
                // overtime
                if (userHours + Convert.ToDecimal(satTotalHoursLBL.Text) > OVERTIME_THRESHOLD)
                {
                    overtime = true;
                }
                // check to see if the sum of all hours exceeds 24
                if (userHours + Convert.ToDecimal(satTotalHoursLBL.Text) > 24)
                {
                    exceedDaily = true;
                }
            }
        }

        // notify the user accordingly return false if error true if valid
        if (exceedDaily)
        {
            dialogTitle = "Daily Hour Exceed 24";
            msg = "You cannot work more than 24 hours per day. Please Review your Timesheet for " + day;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "projectNumInvalid", "throwDialog();", true);
            return false;
        }
        else if (overtime)
        {
            dialogTitle = "Overtime";
            msg = "You have worked more than 12 hours per day. Fill out a separate form to accomodate for overtime";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "projectNumInvalid", "throwDialog();", true);
            return true;
        }
        else
        {
            return true;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        sunLabTB.Text = string.Empty;
        monLabTB.Text = string.Empty;
        tuesLabTB.Text = string.Empty;
        wedsLabTB.Text = string.Empty;
        thursLabTB.Text = string.Empty;
        friLabTB.Text = string.Empty;
        satLabTB.Text = string.Empty;

        sunNuclearDensityTestTB.Text = string.Empty;
        monNuclearDensityTestTB.Text = string.Empty;
        tuesNuclearDensityTestTB.Text = string.Empty;
        wedsNuclearDensityTestTB.Text = string.Empty;
        thursNuclearDensityTestTB.Text = string.Empty;
        friNuclearDensityTestTB.Text = string.Empty;
        satNuclearDensityTestTB.Text = string.Empty;
    }

    protected void updateTimeSheetTotals() {
        TimesheetManager tm = new TimesheetManager();

        TimeSheet ts = tm.getTimesheetForID(timesheetID);

        if (ts != null)
        {
            // populate totals check for nulls and empty string
            if (ts.TotalDistance.ToString() == null || ts.TotalDistance.ToString() == string.Empty)
            {
                totalDistanceLBL.Text = "0";
            }
            else
            {
                totalDistanceLBL.Text = ts.TotalDistance.ToString();
            }

            // populate totals check for nulls and empty string
            if (ts.TotalTruck.ToString() == null || ts.TotalTruck.ToString() == string.Empty)
            {
                totalTruckLBL.Text = "0";
            }
            else
            {
                totalTruckLBL.Text = ts.TotalTruck.ToString();
            }

            if (ts.TotalExpenses.ToString() == null || ts.TotalExpenses.ToString() == string.Empty)
            {
                totalExpensesLBL.Text = "0";
            }
            else
            {
                totalExpensesLBL.Text = ts.TotalExpenses.Value.ToString("0.####");
            }

            if (ts.TotalHours.ToString() == null || ts.TotalHours.ToString() == string.Empty)
            {
                totalHoursLBL.Text = "0";
            }
            else
            {
                totalHoursLBL.Text = ts.TotalHours.Value.ToString("0.#");
            }
            // update totals
            updateChargeableTotals();
            updateLabTotals();
            updateNonChargeableTotals(); 
        }
    
    }

    protected void HighLightDeletedRows() {

        if (rowIDs != null)
        {
            for (int i = 0; i < rowIDs.Count; i++)
            {
                summaryGV.Rows[i].BackColor = System.Drawing.Color.PaleVioletRed; 
            }   
        }
    }
}