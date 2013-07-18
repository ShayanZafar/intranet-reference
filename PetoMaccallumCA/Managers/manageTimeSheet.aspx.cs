using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI;
using petomaccallumModel;

public partial class Managers_manageTimeSheet : System.Web.UI.Page
{
    // PAGE CONSTANTS
    protected decimal MAX_WEEKLY_HOURS = 60;
    protected decimal OVERTIME_THRESHOLD = 12;

    // PAGE JQUERY/JS Variables
    protected string dialogTitle;
    protected string msg;

    // page scope variables to access in markup
    protected string[] projectNoDB;
    protected string[] dbData;
    protected int timesheetID;
    protected int totalDistance;
    protected decimal totalExpenses;
    protected decimal totalHours;
    protected int aID;
    protected List<int> rowIDs;

    protected void chargeableBtn_Click(object sender, EventArgs e)
    {
        int cDist = Convert.ToInt32(totalDistanceLBL.Text), tDist = Convert.ToInt32(totalTruckLBL.Text);
        decimal cHours = Convert.ToDecimal(totalHoursLBL.Text), cExpense = Convert.ToDecimal(Convert.ToDecimal(totalExpensesLBL.Text));
        TimesheetManager tm = new TimesheetManager();
        bool valid = true;
        // sunday
        if (sunBCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(sunBCHoursTB.Text, "Sunday", false))
            {
                valid = false;
            }
        }
        if (valid)
        {
            timesheetStatusLBL.Text = tm.createManagerChargeable(sunBCHoursTB.Text, sunBAccomTB.Text, sunBDistTB.Text, sunTruckDistTB.Text, sunBMiscTB.Text,
                     "Sunday", projectNumTB.Text, classificationDDL.SelectedValue, activitiesDDL.SelectedValue, timesheetID, sunCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
        }

        // monday
        if (monBCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(monBCHoursTB.Text, "Monday", false))
            {
                valid = false;
            }
        }
        if (valid)
        {
            timesheetStatusLBL.Text = tm.createManagerChargeable(monBCHoursTB.Text, monBAccomTB.Text, monBDistTB.Text, monTruckDistTB.Text, monBMiscTB.Text,
                      "Monday", projectNumTB.Text, classificationDDL.SelectedValue, activitiesDDL.SelectedValue, timesheetID, monCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
        }

        // tuesday
        if (tuesBCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(tuesBCHoursTB.Text, "Tuesday", false))
            {
                valid = false;
            }
        }
        if (valid)
        {
            timesheetStatusLBL.Text = tm.createManagerChargeable(tuesBCHoursTB.Text, tuesBAccomTB.Text, tuesBDistTB.Text, tuesTruckDistTB.Text, tuesBMiscTB.Text,
                    "tuesday", projectNumTB.Text, classificationDDL.SelectedValue, activitiesDDL.SelectedValue, timesheetID, tuesCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
        }

        // wednesday
        if (wedsBCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(wedsBCHoursTB.Text, "Wednesday", false))
            {
                valid = false;
            }
        }
        if (valid)
        {
            timesheetStatusLBL.Text = tm.createManagerChargeable(wedsBCHoursTB.Text, wedsBAccomTB.Text, wedsBDistTB.Text, wedsTruckDistTB.Text, wedsBMiscTB.Text,
        "Wednesday", projectNumTB.Text, classificationDDL.SelectedValue, activitiesDDL.SelectedValue, timesheetID, wedsCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
        }

        // thursday
        if (thursBCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(thursBCHoursTB.Text, "Thursday", false))
            {
                valid = false;
            }
        }
        if (valid)
        {
            timesheetStatusLBL.Text = tm.createManagerChargeable(thursBCHoursTB.Text, thursBAccomTB.Text, thursBDistTB.Text, thursTruckDistTB.Text, thursBMiscTB.Text,
                   "Thursday", projectNumTB.Text, classificationDDL.SelectedValue, activitiesDDL.SelectedValue, timesheetID, thursCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
        }

        // friday
        if (friBCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(friBCHoursTB.Text, "Friday", false))
            {
                valid = false;
            }
        }
        if (valid)
        {
            timesheetStatusLBL.Text = tm.createManagerChargeable(friBCHoursTB.Text, friBAccomTB.Text, friBDistTB.Text, friTruckDistTB.Text, friBMiscTB.Text,
              "Friday", projectNumTB.Text, classificationDDL.SelectedValue, activitiesDDL.SelectedValue, timesheetID, friCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
        }

        // saturday
        if (satBCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(satBCHoursTB.Text, "Saturday", false))
            {
                valid = false;
            }
        }
        if (valid)
        {
            timesheetStatusLBL.Text = tm.createManagerChargeable(satBCHoursTB.Text, satBAccomTB.Text, satBDistTB.Text, satTruckDistTB.Text, satBMiscTB.Text,
                           "Saturday", projectNumTB.Text, classificationDDL.SelectedValue, activitiesDDL.SelectedValue, timesheetID, satCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
        }

        // add to totalized value labels
        totalHoursLBL.Text = cHours.ToString("0.#");
        totalExpensesLBL.Text = cExpense.ToString("0.####");
        totalDistanceLBL.Text = cDist.ToString();
        totalTruckLBL.Text = tDist.ToString();
        // if hours exceed 60 throw a dialog
        if (Convert.ToDecimal(totalHoursLBL.Text) > MAX_WEEKLY_HOURS)
        {
            // throw a dialog
            dialogTitle = "Regular Weekly Hours Exceeded";
            msg = "You Worked more than 60 Hours per week. A separate Form must be filled out to accomodate overtime";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "projectNumInvalid", "throwDialog();", true);
        }
        // update timesheet
        tm.updateTimeSheet(timesheetID, cHours, cDist, tDist, cExpense, "Updated - Manager", " ", " ");

        projectNameTB.Enabled = false;
        clientNameTB.Enabled = false;

        // clear form fields
        clearChargeable();

        // chargeable fields
        clientNameTB.Text = string.Empty;
        projectNameTB.Text = string.Empty;
        projectNumTB.Text = string.Empty;
        classificationDDL.Items.Clear();
        activitiesDDL.Items.Clear();

        // update totals
        updateChargeableTotals();
        ;
        // update gridview
        summaryGV.DataBind();
    }

    protected void ChargeableLB_Click(object sender, EventArgs e)
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
            aID = 0;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "toggleAccordion", "accordionToggle();", true);
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
        sunBCHoursTB.Text = string.Empty;
        sunBAccomTB.Text = string.Empty;
        sunBDistTB.Text = string.Empty;
        sunTruckDistTB.Text = string.Empty;
        sunBMiscTB.Text = string.Empty;
        sunNuclearDensityTestTB.Text = string.Empty;
        sunCRemarksTB.Text = string.Empty;

        // monday
        monBCHoursTB.Text = string.Empty;
        monBAccomTB.Text = string.Empty;
        monBDistTB.Text = string.Empty;
        monTruckDistTB.Text = string.Empty;
        monBMiscTB.Text = string.Empty;
        monNuclearDensityTestTB.Text = string.Empty;
        monCRemarksTB.Text = string.Empty;

        // tuesday
        tuesBCHoursTB.Text = string.Empty;
        tuesBAccomTB.Text = string.Empty;
        tuesBDistTB.Text = string.Empty;
        tuesTruckDistTB.Text = string.Empty;
        tuesBMiscTB.Text = string.Empty;
        tuesNuclearDensityTestTB.Text = string.Empty;
        tuesCRemarksTB.Text = string.Empty;

        // wedssday
        wedsBCHoursTB.Text = string.Empty;
        wedsBAccomTB.Text = string.Empty;
        wedsBDistTB.Text = string.Empty;
        wedsTruckDistTB.Text = string.Empty;
        wedsBMiscTB.Text = string.Empty;
        wedsNuclearDensityTestTB.Text = string.Empty;
        wedsCRemarksTB.Text = string.Empty;

        // thursday
        thursBCHoursTB.Text = string.Empty;
        thursBAccomTB.Text = string.Empty;
        thursBDistTB.Text = string.Empty;
        thursTruckDistTB.Text = string.Empty;
        thursBMiscTB.Text = string.Empty;
        thursNuclearDensityTestTB.Text = string.Empty;
        thursCRemarksTB.Text = string.Empty;

        // friday
        friBCHoursTB.Text = string.Empty;
        friBAccomTB.Text = string.Empty;
        friBDistTB.Text = string.Empty;
        friTruckDistTB.Text = string.Empty;
        friBMiscTB.Text = string.Empty;
        friNuclearDensityTestTB.Text = string.Empty;
        friCRemarksTB.Text = string.Empty;

        //saturday
        satBCHoursTB.Text = string.Empty;
        satBAccomTB.Text = string.Empty;
        satBDistTB.Text = string.Empty;
        satTruckDistTB.Text = string.Empty;
        satBMiscTB.Text = string.Empty;
        satNuclearDensityTestTB.Text = string.Empty;
        satCRemarksTB.Text = string.Empty;
    }

    protected void clearChargeableBtn_Click(object sender, EventArgs e)
    {
        updateChargeableBtn.Visible = false;
        chargeableBtn.Visible = true;
        // clear fields
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
        // clear non-chargeable form fields
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
                tm.updateTimeSheet(Convert.ToInt32(HiddenField1.Value), Convert.ToDecimal(totalHoursLBL.Text), Convert.ToInt32(totalDistanceLBL.Text), Convert.ToInt32(totalTruckLBL.Text), Convert.ToDecimal(totalExpensesLBL.Text), "Approved", empCommentsTB.Text, " ");
                //// update user status
                //AdminManager am = new AdminManager();
                //User user = tm.getEmployeeForId(timesheetID);
                //am.updateEmployee(Membership.GetUser().UserName, "Incomplete", user.branch, user.ManagedBy);
                dialogTitle = "Timesheet Approved";
                msg = "Timesheet has Been Approved.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "timesheetSubmitted", "throwDialog();", true);
            }
        }
        catch (Exception)
        {
            // throw a dialog
            msg = "Your TimeSheet was not Approved.";
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
        // keep displaying non-chargeable panel on postback
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

    protected void summaryGV_RowDeleted(object sender, System.Web.UI.WebControls.GridViewDeletedEventArgs e)
    {
        // update totals when a row is successfully deleted
        TimesheetManager tm = new TimesheetManager();

        TimeSheet t = tm.getTimesheetForID(timesheetID);
        if (t != null)
        {
            if (t.TotalHours >= 0)
            {
                totalHoursLBL.Text = t.TotalHours.Value.ToString("0.#");
            }
            if (t.TotalDistance >= 0)
            {
                totalDistanceLBL.Text = t.TotalDistance.ToString();
            }
            if (t.TotalTruck >= 0)
            {
                totalTruckLBL.Text = t.TotalTruck.ToString();
            }
            if (t.TotalExpenses >= 0)
            {
                totalExpensesLBL.Text = t.TotalExpenses.ToString();
            }
        }
    }

    protected void NonChargeableLB_Click(object sender, EventArgs e)
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
            aID = 2;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "toggleAccordion", "accordionToggle();", true);
        }
        
    }

    protected void nonChargeBtn_Click(object sender, EventArgs e)
    {
        bool valid = true;
        int nCDist = Convert.ToInt32(totalDistanceLBL.Text), tDist = Convert.ToInt32(totalTruckLBL.Text);
        decimal nCHours = Convert.ToDecimal(totalHoursLBL.Text), nCExpense = Convert.ToDecimal(Convert.ToDecimal(totalExpensesLBL.Text));

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
            timesheetStatusLBL.Text = tm.createManagerNonChargeable(sunNCHoursTB.Text, sunNCAccomTB.Text, sunNCDistanceTB.Text, sunNCMiscTB.Text, "Sunday",
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
            timesheetStatusLBL.Text = tm.createManagerNonChargeable(monNCHoursTB.Text, monNCAccomTB.Text, monNCDistanceTB.Text, monNCMiscTB.Text, "Monday",
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
            timesheetStatusLBL.Text = tm.createManagerNonChargeable(tuesNCHoursTB.Text, tuesNCAccomTB.Text, tuesNCDistanceTB.Text, tuesNCMiscTB.Text, "Tuesday",
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
            timesheetStatusLBL.Text = tm.createManagerNonChargeable(wedsNCHoursTB.Text, wedsNCAccomTB.Text, wedsNCDistanceTB.Text, wedsNCMiscTB.Text, "Wednesday",
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
            timesheetStatusLBL.Text = tm.createManagerNonChargeable(thursNCHoursTB.Text, thursNCAccomTB.Text, thursNCDistanceTB.Text, thursNCMiscTB.Text, "Thursday",
                        detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, thursNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);
        }

        // friday
        if (friNCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(friNCHoursTB.Text, "Friday", false))
            {
                valid = false;
            }
        }
        if (valid)
        {
            timesheetStatusLBL.Text = tm.createManagerNonChargeable(friNCHoursTB.Text, friNCAccomTB.Text, friNCDistanceTB.Text, friNCMiscTB.Text, "Friday",
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
            timesheetStatusLBL.Text = tm.createManagerNonChargeable(satNCHoursTB.Text, satNCAccomTB.Text, satNCDistanceTB.Text, satNCMiscTB.Text, "Saturday",
                          detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, satNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);
        }

        // add the totalized labels
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
        tm.updateTimeSheet(timesheetID, nCHours, nCDist, tDist, nCExpense, "Updated - Manager", " ", " ");
        //update totals
        updateNonChargeableTotals();

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
        clearChargeable();

        // reset drop down list to first index
        detailsDDL.SelectedIndex = 0;
        specifyDDL.SelectedIndex = 0;
        NonChargeHoursPanel.Visible = false;
        NonChargeExpensesPanel.Visible = false;
        updateNonChargeableBtn.Visible = false;
        nonChargeBtn.Visible = true;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        dbData = new string[7] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        weekEndingTB.Enabled = false;
        // Fields to populate when user visits this page: Employee ID, Employee Name, and Branch
        TimesheetManager tm = new TimesheetManager();
        projectNumTB.Enabled = true;
        if (!IsPostBack)
        {
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

            // check query strings for form data to be populated
            if (Request.QueryString.Count > 0)
            {
                string timesheet = Request.QueryString["time"];
                string expense = Request.QueryString["expense"];
                string hours = Request.QueryString["hours"];
                string project = Request.QueryString["project"];
                string classification = Request.QueryString["class"];
                string activity = Request.QueryString["activity"];
                int chID = Convert.ToInt32(Request.QueryString["charge"]);
                int nonChID = Convert.ToInt32(Request.QueryString["nonCh"]);
                string delete = Request.QueryString["delete"];
                int lab = Convert.ToInt32(Request.QueryString["test"]);

                // reset weekending dates
                ScriptManager.RegisterStartupScript(this, this.GetType(), "populate", "populate();", true);

                int id = Convert.ToInt32(timesheet);
                timesheetID = id;
                HiddenField1.Value = id.ToString();

                // handle lab hours
                if (lab > 0 && delete != "delete")
                {
                    List<ManagerTest> tests = tm.getManagerTestsForTimeSheet(id);
                    updateLabTestsBtn.Visible = true;
                    labTestsBtn.Visible = false;
                    aID = 1;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toggleAccordion", "accordionToggle();", true);
                    foreach (ManagerTest item in tests)
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
                // if there is a query string, load the totals from the corresponding timesheet

                if (delete == "delete")
                {
                    
                   tm.deleteSummary(nonChID, chID, id, lab, true);
                    summaryGV.Focus();
                }
                // update Timesheet Totals
                updateTimeSheetTotals();
                TimeSheet t = tm.getTimesheetForID(timesheetID);
                // get and populate user information
                User emp = tm.getEmployeeForId(t.EmployeeId);
                empIdTB.Text = emp.empNo;
                nameTB.Text = emp.FirstName + " " + emp.MiddleName + " " + emp.LastName;
                departmentTB.Text = emp.branch;
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
                // Chargeable
                else if (project != null && project != "0" && project != string.Empty)
                {
                    // adjust accordion panels
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

                        // adjust buttons
                        chargeableBtn.Visible = false;
                        updateChargeableBtn.Visible = true;

                        // populate the chargeable fields based on the classification and activity selected
                        List<ManagerChargeable> cJobs = tm.getManagerChargeableForID(timesheetID);

                        if (chID > 0)
                        {
                            cJobs.Add(tm.getManagerChargeForID(chID));
                        }

                        if (cJobs != null)
                        {
                            foreach (var item in cJobs)
                            {
                                if (item.Day == "Sunday" && item.Classification == classification && item.Activity == activity)
                                {
                                    // Accomodation

                                    if (item.BillingAccomodation.HasValue)
                                    {
                                        sunBAccomTB.Text = item.PayRollAccomodation.Value.ToString("0.00##");
                                    }

                                    // Hours
                                    if (item.BillingHours.HasValue)
                                    {
                                        sunBCHoursTB.Text = item.BillingHours.Value.ToString("0.0");
                                    }
                                    // Distance
                                    if (item.BillingTravelDistance.HasValue)
                                    {
                                        sunBDistTB.Text = item.BillingTravelDistance.ToString();
                                    }

                                    // truck distance
                                    if (item.TruckDistance.HasValue)
                                    {
                                        sunTruckDistTB.Text = item.TruckDistance.ToString();
                                    }

                                    // Misc

                                    if (item.BillingMisc.HasValue)
                                    {
                                        sunBMiscTB.Text = item.BillingMisc.Value.ToString("0.00##");
                                    }

                                    sunCRemarksTB.Text = item.Remarks;
                                }
                                else if (item.Day == "Monday" && item.Classification == classification && item.Activity == activity)
                                {
                                    // Accomodation

                                    if (item.BillingAccomodation.HasValue)
                                    {
                                        monBAccomTB.Text = item.PayRollAccomodation.Value.ToString("0.00##");
                                    }

                                    // Hours

                                    if (item.BillingHours.HasValue)
                                    {
                                        monBCHoursTB.Text = item.BillingHours.Value.ToString("0.0");
                                    }
                                    // Distance

                                    if (item.BillingTravelDistance.HasValue)
                                    {
                                        monBDistTB.Text = item.BillingTravelDistance.ToString();
                                    }
                                    // truck distance
                                    if (item.TruckDistance.HasValue)
                                    {
                                        monTruckDistTB.Text = item.TruckDistance.ToString();
                                    }
                                    // Misc

                                    if (item.BillingMisc.HasValue)
                                    {
                                        monBMiscTB.Text = item.BillingMisc.Value.ToString("0.00##");
                                    }
                                    monCRemarksTB.Text = item.Remarks;
                                }
                                else if (item.Day == "Tuesday" && item.Classification == classification && item.Activity == activity)
                                {
                                    // Accomodation

                                    if (item.BillingAccomodation.HasValue)
                                    {
                                        tuesBAccomTB.Text = item.PayRollAccomodation.Value.ToString("0.00##");
                                    }

                                    // Hours

                                    if (item.BillingHours.HasValue)
                                    {
                                        tuesBCHoursTB.Text = item.BillingHours.Value.ToString("0.0");
                                    }
                                    // Distance

                                    if (item.BillingTravelDistance.HasValue)
                                    {
                                        tuesBDistTB.Text = item.BillingTravelDistance.ToString();
                                    }
                                    // truck distance
                                    if (item.TruckDistance.HasValue)
                                    {
                                        tuesTruckDistTB.Text = item.TruckDistance.ToString();
                                    }
                                    // Misc

                                    if (item.BillingMisc.HasValue)
                                    {
                                        tuesBMiscTB.Text = item.BillingMisc.Value.ToString("0.00##");
                                    }
                                    tuesCRemarksTB.Text = item.Remarks;
                                }
                                else if (item.Day == "Wednesday" && item.Classification == classification && item.Activity == activity)
                                {
                                    // Accomodation

                                    if (item.BillingAccomodation.HasValue)
                                    {
                                        wedsBAccomTB.Text = item.PayRollAccomodation.Value.ToString("0.00##");
                                    }

                                    // Hours

                                    if (item.BillingHours.HasValue)
                                    {
                                        wedsBCHoursTB.Text = item.BillingHours.Value.ToString("0.0");
                                    }
                                    // Distance

                                    if (item.BillingTravelDistance.HasValue)
                                    {
                                        wedsBDistTB.Text = item.BillingTravelDistance.ToString();
                                    }
                                    // truck distance
                                    if (item.TruckDistance.HasValue)
                                    {
                                        wedsTruckDistTB.Text = item.TruckDistance.ToString();
                                    }
                                    // Misc

                                    if (item.BillingMisc.HasValue)
                                    {
                                        wedsBMiscTB.Text = item.BillingMisc.Value.ToString("0.00##");
                                    }
                                    wedsCRemarksTB.Text = item.Remarks;
                                }
                                else if (item.Day == "Thursday" && item.Classification == classification && item.Activity == activity)
                                {
                                    // Accomodation

                                    if (item.BillingAccomodation.HasValue)
                                    {
                                        thursBAccomTB.Text = item.PayRollAccomodation.Value.ToString("0.00##");
                                    }

                                    // Hours
                                    if (item.BillingHours.HasValue)
                                    {
                                        thursBCHoursTB.Text = item.BillingHours.Value.ToString("0.0");
                                    }
                                    // Distance

                                    if (item.BillingTravelDistance.HasValue)
                                    {
                                        thursBDistTB.Text = item.BillingTravelDistance.ToString();
                                    }
                                    // truck distance
                                    if (item.TruckDistance.HasValue)
                                    {
                                        thursTruckDistTB.Text = item.TruckDistance.ToString();
                                    }
                                    // Misc

                                    if (item.BillingMisc.HasValue)
                                    {
                                        thursBMiscTB.Text = item.BillingMisc.Value.ToString("0.00##");
                                    }
                                    thursCRemarksTB.Text = item.Remarks;
                                }
                                else if (item.Day == "Friday" && item.Classification == classification && item.Activity == activity)
                                {    // Accomodation
                                    if (item.BillingAccomodation.HasValue)
                                    {
                                        friBAccomTB.Text = item.PayRollAccomodation.Value.ToString("0.00##");
                                    }

                                    // Hours
                                    if (item.BillingHours.HasValue)
                                    {
                                        friBCHoursTB.Text = item.BillingHours.Value.ToString("0.0");
                                    }
                                    // Distance

                                    if (item.BillingTravelDistance.HasValue)
                                    {
                                        friBDistTB.Text = item.BillingTravelDistance.ToString();
                                    }
                                    // truck distance
                                    if (item.TruckDistance.HasValue)
                                    {
                                        friTruckDistTB.Text = item.TruckDistance.ToString();
                                    }
                                    // Misc

                                    if (item.BillingMisc.HasValue)
                                    {
                                        friBMiscTB.Text = item.BillingMisc.Value.ToString("0.00##");
                                    }
                                    friCRemarksTB.Text = item.Remarks;
                                }
                                else if (item.Day == "Saturday" && item.Classification == classification && item.Activity == activity)
                                {
                                    // Accomodation
                                    if (item.BillingAccomodation.HasValue)
                                    {
                                        satBAccomTB.Text = item.PayRollAccomodation.Value.ToString("0.00##");
                                    }

                                    // Hours
                                    if (item.BillingHours.HasValue)
                                    {
                                        satBCHoursTB.Text = item.BillingHours.Value.ToString("0.0");
                                    }
                                    // Distance
                                    if (item.BillingTravelDistance.HasValue)
                                    {
                                        satBDistTB.Text = item.BillingTravelDistance.ToString();
                                    }
                                    // truck distance
                                    if (item.TruckDistance.HasValue)
                                    {
                                        satTruckDistTB.Text = item.TruckDistance.ToString();
                                    }
                                    // Misc
                                    if (item.BillingMisc.HasValue)
                                    {
                                        satBMiscTB.Text = item.BillingMisc.Value.ToString("0.00##");
                                    }
                                    satCRemarksTB.Text = item.Remarks;
                                }
                            }
                        }
                    }
                }
                // Non-Chargeable
                else if (expense != string.Empty && expense != null || hours != string.Empty && hours != null)
                {
                    // activate corresponding accordion panel
                    aID = 2;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toggleAccordion", "accordionToggle();", true);

                    // set the dropdownlists and display the different panels accordingly
                    if (hours != string.Empty)
                    {
                        specifyDDL.SelectedValue = hours;
                        NonChargeHoursPanel.Visible = true;
                    } if (expense != string.Empty)
                    {
                        detailsDDL.SelectedValue = expense;
                        NonChargeExpensesPanel.Visible = true;
                    }

                    // adjust buttons
                    nonChargeBtn.Visible = false;
                    updateNonChargeableBtn.Visible = true;

                    List<ManagerNonChargeable> nonCh = tm.getManagerNonChargeableForTimesheetID(timesheet, expense, hours);
                    if (nonCh != null)
                    {
                        // populate the non chargeable text fields based on the expense and type of hours selected
                        foreach (var item in nonCh)
                        {
                            if (item.Day == "Sunday")
                            {
                                if (item.Accomodations.HasValue)
                                {
                                    sunNCAccomTB.Text = item.Accomodations.Value.ToString("0.####");
                                }
                                sunNCHoursTB.Text = item.Hours.Value.ToString("0.#");
                                sunNCDistanceTB.Text = item.Distance.ToString();
                                if (item.Misc.HasValue)
                                {
                                    sunNCMiscTB.Text = item.Misc.Value.ToString("0.####");
                                }

                                sunNCRemarksTB.Text = item.Remarks;
                            }
                            else if (item.Day == "Monday")
                            {
                                if (item.Accomodations.HasValue)
                                {
                                    monNCAccomTB.Text = item.Accomodations.Value.ToString("0.####");
                                }
                                monNCHoursTB.Text = item.Hours.Value.ToString("0.#");
                                monNCDistanceTB.Text = item.Distance.ToString();
                                if (item.Misc.HasValue)
                                {
                                    monNCMiscTB.Text = item.Misc.Value.ToString("0.####");
                                }

                                monNCRemarksTB.Text = item.Remarks;
                            }
                            else if (item.Day == "Tuesday")
                            {
                                if (item.Accomodations.HasValue)
                                {
                                    tuesNCAccomTB.Text = item.Accomodations.Value.ToString("0.####");
                                }
                                tuesNCHoursTB.Text = item.Hours.Value.ToString("0.#");
                                tuesNCDistanceTB.Text = item.Distance.ToString();
                                if (item.Misc.HasValue)
                                {
                                    tuesNCMiscTB.Text = item.Misc.Value.ToString("0.####");
                                }

                                tuesNCRemarksTB.Text = item.Remarks;
                            }
                            else if (item.Day == "Wednesday")
                            {
                                if (item.Accomodations.HasValue)
                                {
                                    wedsNCAccomTB.Text = item.Accomodations.Value.ToString("0.####");
                                }
                                wedsNCHoursTB.Text = item.Hours.Value.ToString("0.#");
                                wedsNCDistanceTB.Text = item.Distance.ToString();
                                if (item.Misc.HasValue)
                                {
                                    wedsNCMiscTB.Text = item.Misc.Value.ToString("0.####");
                                }
                                wedsNCRemarksTB.Text = item.Remarks;
                            }
                            else if (item.Day == "Thursday")
                            {
                                if (item.Accomodations.HasValue)
                                {
                                    thursNCAccomTB.Text = item.Accomodations.Value.ToString("0.####");
                                }
                                thursNCHoursTB.Text = item.Hours.Value.ToString("0.#");
                                thursNCDistanceTB.Text = item.Distance.ToString();
                                if (item.Misc.HasValue)
                                {
                                    thursNCMiscTB.Text = item.Misc.Value.ToString("0.####");
                                }
                                thursNCRemarksTB.Text = item.Remarks;
                            }
                            else if (item.Day == "Friday")
                            {
                                if (item.Accomodations.HasValue)
                                {
                                    friNCAccomTB.Text = item.Accomodations.Value.ToString("0.####");
                                }
                                friNCHoursTB.Text = item.Hours.Value.ToString("0.#");
                                friNCDistanceTB.Text = item.Distance.ToString();
                                if (item.Misc.HasValue)
                                {
                                    friNCMiscTB.Text = item.Misc.Value.ToString("0.####");
                                }
                                friNCRemarksTB.Text = item.Remarks;
                            }
                            else if (item.Day == "Saturday")
                            {
                                if (item.Accomodations.HasValue)
                                {
                                    satNCAccomTB.Text = item.Accomodations.Value.ToString("0.####");
                                }
                                satNCHoursTB.Text = item.Hours.Value.ToString("0.#");
                                satNCDistanceTB.Text = item.Distance.ToString();
                                satNCRemarksTB.Text = item.Remarks;
                                if (item.Misc.HasValue)
                                {
                                    satNCMiscTB.Text = item.Misc.Value.ToString("0.####");
                                }
                            }
                        }
                    }
                }
            }
        }
        // check to see if weekending is specified update the gridviews
        if (weekEndingTB.Text != string.Empty)
        {
            // re-populate the dates
            ScriptManager.RegisterStartupScript(this, this.GetType(), "populate", "populate();", true);
            // Get the timesheet id for the users whos timesheet we are managing
            string username = tm.getUsernameForEmpNo(empIdTB.Text);
            int id = tm.idForDate(weekEndingTB.Text, tm.idForUsername(username));

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
            // check to see if the project number follows the correct format
            if (Regex.IsMatch(projectNumTB.Text, numFormat))
            {
                TimesheetManager tm = new TimesheetManager();
                Project p = tm.getProjectForID(projectNumTB.Text.Trim());
                classificationDDL.DataSource = tm.getClassificationForProject(projectNumTB.Text.Trim());
                classificationDDL.DataBind();
                classificationDDL.Items.Insert(0, "Select");
                classificationDDL.Focus();
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "projectVerify", "confirmProject();", true);
                aID = 0;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toggleAccordion", "accordionToggle();", true);
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
        // keep showing the non-chargeable part of the accordion on postback
        aID = 2;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "toggleAccordion", "accordionToggle();", true);
    }

    protected void updateChargeableBtn_Click(object sender, EventArgs e)
    {
        TimesheetManager tm = new TimesheetManager();
        bool result, valid = false;
        int count = 0, cDist = Convert.ToInt32(totalDistanceLBL.Text), tDist = Convert.ToInt32(totalTruckLBL.Text);
        decimal cHours = Convert.ToDecimal(totalHoursLBL.Text), cExpense = Convert.ToDecimal(Convert.ToDecimal(totalExpensesLBL.Text)); ;
        int id = tm.idForProjectNo(projectNumTB.Text);
        // sunday
        if (sunBCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(sunBCHoursTB.Text, "Sunday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            result = tm.updateManagerChargeable(sunBCHoursTB.Text, sunBAccomTB.Text, sunBDistTB.Text, sunTruckDistTB.Text, sunBMiscTB.Text,
                "Sunday", id, classificationDDL.SelectedValue, activitiesDDL.SelectedValue, timesheetID, sunCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);

            if (result)
            {
                count++;
            }
            else
            {
                timesheetStatusLBL.Text = tm.createManagerChargeable(sunBCHoursTB.Text, sunBAccomTB.Text, sunBDistTB.Text, monTruckDistTB.Text, sunBMiscTB.Text,
                    "Sunday", projectNumTB.Text, classificationDDL.SelectedValue, activitiesDDL.SelectedValue, timesheetID, sunCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
            }
        }

        // monday
        if (monBCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(monBCHoursTB.Text, "Monday", true))
            {
                valid = false;
            }
        }

        if (valid)
        {
            result = tm.updateManagerChargeable(monBCHoursTB.Text, monBAccomTB.Text, monBDistTB.Text, monTruckDistTB.Text, monBMiscTB.Text,
                     "Monday", id, classificationDDL.SelectedValue, activitiesDDL.SelectedValue, timesheetID, monCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);

            if (result)
            {
                count++;
            }
            else
            {
                timesheetStatusLBL.Text = tm.createManagerChargeable(monBCHoursTB.Text, monBAccomTB.Text, monBDistTB.Text, monTruckDistTB.Text, monBMiscTB.Text,
                   "Monday", projectNumTB.Text, classificationDDL.SelectedValue, activitiesDDL.SelectedValue, timesheetID, monCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
            }
        }

        // tuesday
        if (tuesBCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(tuesBCHoursTB.Text, "Tuesday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            result = tm.updateManagerChargeable(tuesBCHoursTB.Text, tuesBAccomTB.Text, tuesBDistTB.Text, tuesTruckDistTB.Text, tuesBMiscTB.Text,
                            "Tuesday", id, classificationDDL.SelectedValue, activitiesDDL.SelectedValue, timesheetID, tuesCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);

            if (result)
            {
                count++;
            }
            else
            {
                timesheetStatusLBL.Text = tm.createManagerChargeable(tuesBCHoursTB.Text, tuesBAccomTB.Text, tuesBDistTB.Text, tuesTruckDistTB.Text, tuesBMiscTB.Text,
                   "Tuesday", projectNumTB.Text, classificationDDL.SelectedValue, activitiesDDL.SelectedValue, timesheetID, tuesCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
            }
        }

        // Wednesday
        if (wedsBCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(wedsBCHoursTB.Text, "Wednesday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            result = tm.updateManagerChargeable(wedsBCHoursTB.Text, wedsBAccomTB.Text, wedsBDistTB.Text, wedsTruckDistTB.Text, wedsBMiscTB.Text,
                          "Wednesday", id, classificationDDL.SelectedValue, activitiesDDL.SelectedValue, timesheetID, wedsCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
            if (result)
            {
                count++;
            }
            else
            {
                timesheetStatusLBL.Text = tm.createManagerChargeable(wedsBCHoursTB.Text, wedsBAccomTB.Text, wedsBDistTB.Text, wedsTruckDistTB.Text, wedsBMiscTB.Text,
                     "Wednesday", projectNumTB.Text, classificationDDL.SelectedValue, activitiesDDL.SelectedValue, timesheetID, wedsCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
            }
        }

        // Thursday
        if (thursBCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(thursBCHoursTB.Text, "Thursday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            result = tm.updateManagerChargeable(thursBCHoursTB.Text, thursBAccomTB.Text, thursBDistTB.Text, thursTruckDistTB.Text, thursBMiscTB.Text,
                          "Thursday", id, classificationDDL.SelectedValue, activitiesDDL.SelectedValue, timesheetID, thursCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);

            if (result)
            {
                count++;
            }
            else
            {
                timesheetStatusLBL.Text = tm.createManagerChargeable(thursBCHoursTB.Text, thursBAccomTB.Text, thursBDistTB.Text, thursTruckDistTB.Text, thursBMiscTB.Text,
                   "Thursday", projectNumTB.Text, classificationDDL.SelectedValue, activitiesDDL.SelectedValue, timesheetID, thursCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
            }
        }

        // friday
        if (friBCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(friBCHoursTB.Text, "Friday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            result = tm.updateManagerChargeable(friBCHoursTB.Text, friBAccomTB.Text, friBDistTB.Text, friTruckDistTB.Text, friBMiscTB.Text,
                               "Friday", id, classificationDDL.SelectedValue, activitiesDDL.SelectedValue, timesheetID, friCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);

            if (result)
            {
                count++;
            }
            else
            {
                timesheetStatusLBL.Text = tm.createManagerChargeable(friBCHoursTB.Text, friBAccomTB.Text, friBDistTB.Text, friTruckDistTB.Text, friBMiscTB.Text,
                     "Friday", projectNumTB.Text, classificationDDL.SelectedValue, activitiesDDL.SelectedValue, timesheetID, friCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
            }
        }

        // Saturday
        if (satBCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(satBCHoursTB.Text, "Saturday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            result = tm.updateManagerChargeable(satBCHoursTB.Text, satBAccomTB.Text, satBDistTB.Text, satTruckDistTB.Text, satBMiscTB.Text,
                               "Saturday", id, classificationDDL.SelectedValue, activitiesDDL.SelectedValue, timesheetID, satCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);

            if (result)
            {
                count++;
            }
            else
            {
                timesheetStatusLBL.Text = tm.createManagerChargeable(satBCHoursTB.Text, satBAccomTB.Text, satBDistTB.Text, satTruckDistTB.Text, satBMiscTB.Text,
                     "Saturday", projectNumTB.Text, classificationDDL.SelectedValue, activitiesDDL.SelectedValue, timesheetID, satCRemarksTB.Text, ref cHours, ref cDist, ref tDist, ref cExpense);
            }
        }

        // update the totalized labels
        totalHoursLBL.Text = cHours.ToString("0.#");
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
        tm.updateTimeSheet(timesheetID, cHours, cDist, tDist, cExpense, "Updated - Manager", " ", " ");
        // update totals
        updateChargeableTotals();

        // update gridview
        summaryGV.DataBind();

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

    /// <summary>
    /// check hours if they were entered
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void updateNonChargeableBtn_Click(object sender, EventArgs e)
    {
        TimesheetManager tm = new TimesheetManager();
        bool result, valid = true;
        int count = 0, nCDist = Convert.ToInt32(totalDistanceLBL.Text);
        decimal nCHours = Convert.ToDecimal(totalHoursLBL.Text), nCExpense = Convert.ToDecimal(Convert.ToDecimal(totalExpensesLBL.Text));

        //sunday
        if (sunNCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(sunNCHoursTB.Text, "Sunday",true ))
            {
                valid = false;
            }
        }
        if (valid)
        {
            result = tm.updateManagerNonChargeable(sunNCHoursTB.Text, sunNCAccomTB.Text, sunNCDistanceTB.Text, sunNCMiscTB.Text, "Sunday",
                      detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, sunNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);

            if (result)
            {
                count++;
                timesheetStatusLBL.Text = count + " Row(s) Updated Successfully";
            }
            else
            {
                timesheetStatusLBL.Text = tm.createManagerNonChargeable(sunNCHoursTB.Text, sunNCAccomTB.Text, sunNCDistanceTB.Text, sunNCMiscTB.Text, "Sunday",
                detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, sunNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);
            }
        }

        // monday
        if (monNCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(sunNCHoursTB.Text, "Monday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            result = tm.updateManagerNonChargeable(monNCHoursTB.Text, monNCAccomTB.Text, monNCDistanceTB.Text, monNCMiscTB.Text, "Monday",
                    detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, monNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);

            if (result)
            {
                count++;
                timesheetStatusLBL.Text = count + " Row(s) Updated Successfully";
            }
            else
            {
                timesheetStatusLBL.Text = tm.createManagerNonChargeable(monNCHoursTB.Text, monNCAccomTB.Text, monNCDistanceTB.Text, monNCMiscTB.Text, "Monday",
                detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, monNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);
            }
        }

        // tuesday
        if (tuesNCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(sunNCHoursTB.Text, "Tuesday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            result = tm.updateManagerNonChargeable(tuesNCHoursTB.Text, tuesNCAccomTB.Text, tuesNCDistanceTB.Text, tuesNCMiscTB.Text, "Tuesday",
                       detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, tuesNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);

            if (result)
            {
                count++;
                timesheetStatusLBL.Text = count + " Row(s) Updated Successfully";
            }
            else
            {
                timesheetStatusLBL.Text = tm.createManagerNonChargeable(tuesNCHoursTB.Text, tuesNCAccomTB.Text, tuesNCDistanceTB.Text, tuesNCMiscTB.Text, "Tuesday",
                detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, tuesNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);
            }
        }

        // wednesday
        if (wedsNCHoursTB.Text != string.Empty)
        {
            if (!hoursValidator(wedsNCHoursTB.Text, "Wednesday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            result = tm.updateManagerNonChargeable(wedsNCHoursTB.Text, wedsNCAccomTB.Text, wedsNCDistanceTB.Text, wedsNCMiscTB.Text, "Wednesday",
                      detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, wedsNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);

            if (result)
            {
                count++;
                timesheetStatusLBL.Text = count + " Row(s) Updated Successfully";
            }
            else
            {
                timesheetStatusLBL.Text = tm.createManagerNonChargeable(wedsNCHoursTB.Text, wedsNCAccomTB.Text, wedsNCDistanceTB.Text, wedsNCMiscTB.Text, "Wednesday",
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
            result = tm.updateManagerNonChargeable(thursNCHoursTB.Text, thursNCAccomTB.Text, thursNCDistanceTB.Text, thursNCMiscTB.Text, "Thursday",
                      detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, thursNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);

            if (result)
            {
                count++;
                timesheetStatusLBL.Text = count + " Row(s) Updated Successfully";
            }
            else
            {
                timesheetStatusLBL.Text = tm.createManagerNonChargeable(thursNCHoursTB.Text, thursNCAccomTB.Text, thursNCDistanceTB.Text, thursNCMiscTB.Text, "Thursday",
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
            result = tm.updateManagerNonChargeable(friNCHoursTB.Text, friNCAccomTB.Text, friNCDistanceTB.Text, friNCMiscTB.Text, "Friday",
                        detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, friNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);

            if (result)
            {
                count++;
                timesheetStatusLBL.Text = count + " Row(s) Updated Successfully";
            }
            else
            {
                timesheetStatusLBL.Text = tm.createManagerNonChargeable(friNCHoursTB.Text, friNCAccomTB.Text, friNCDistanceTB.Text, friNCMiscTB.Text, "Friday",
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
            result = tm.updateManagerNonChargeable(satNCHoursTB.Text, satNCAccomTB.Text, satNCDistanceTB.Text, satNCMiscTB.Text, "Saturday",
                         detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, satNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);

            if (result)
            {
                count++;
                timesheetStatusLBL.Text = count + " Row(s) Updated Successfully";
            }
            else
            {
                timesheetStatusLBL.Text = tm.createManagerNonChargeable(satNCHoursTB.Text, satNCAccomTB.Text, satNCDistanceTB.Text, satNCMiscTB.Text, "Saturday",
                detailsDDL.SelectedValue, specifyDDL.SelectedValue, timesheetID, satNCRemarksTB.Text, ref nCHours, ref nCDist, ref nCExpense);
            }
        }

        // update the totalized labels
        totalHoursLBL.Text = nCHours.ToString("0.#");
        totalExpensesLBL.Text = nCExpense.ToString("0.####");
        totalDistanceLBL.Text = nCDist.ToString();
        // if hours exceed 60 throw a dialog
        if (nCHours > MAX_WEEKLY_HOURS)
        {
            // throw a dialog
            dialogTitle = "Regular Hours Exceeded";
            msg = "A separate Form must be filled out to accomodate overtime";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "projectNumInvalid", "throwDialog();", true);
        }
        tm.updateTimeSheet(timesheetID, nCHours, nCDist, -1, nCExpense, "Updated - Manager", " ", " ");

        // update totals
        updateNonChargeableTotals();

        // update gridview
        summaryGV.DataBind();
        // clear form fields
        clearNonChargeable();

        // reset drop down list to first index
        detailsDDL.SelectedIndex = 0;
        specifyDDL.SelectedIndex = 0;
        NonChargeHoursPanel.Visible = false;
        NonChargeExpensesPanel.Visible = false;
        updateNonChargeableBtn.Visible = false;
        nonChargeBtn.Visible = true;
    }

    protected void updateChargeableTotals()
    {
        TimesheetManager tm = new TimesheetManager();

        decimal[] totals = tm.getChargeableTotalsForID(timesheetID);
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
        // if hours exceed 60 throw a dialog or 12 for any day
        if (totals[0] > OVERTIME_THRESHOLD || totals[1] > OVERTIME_THRESHOLD || totals[2] > OVERTIME_THRESHOLD || totals[3] > OVERTIME_THRESHOLD ||
           totals[4] > OVERTIME_THRESHOLD || totals[5] > OVERTIME_THRESHOLD || totals[6] > OVERTIME_THRESHOLD)
        {
            // throw a dialog
            dialogTitle = "Regular Hours Exceeded";
            msg = "A separate Form must be filled out to accomodate overtime";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "projectNumInvalid", "throwDialog();", true);
        }
    }

    protected void updateNonChargeableTotals()
    {
        TimesheetManager tm = new TimesheetManager();
        decimal[] totals = tm.getNonChargeableTotalsForID(timesheetID);
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

    protected void labTestsBtn_Click(object sender, EventArgs e)
    {
        bool valid = true;
        decimal tHours = Convert.ToDecimal(totalHoursLBL.Text);
        if (weekEndingTB.Text != string.Empty && timesheetID > 0)
        {
            TimesheetManager tm = new TimesheetManager();
            // Sunday
            if (sunLabTB.Text != string.Empty && sunLabTB.Text != "0")
            {
                if (!hoursValidator(sunLabTB.Text, "Sunday",false))
                {
                    valid = false;
                }
            }
            if (valid)
            {
                tm.createLabTest("Sunday", sunNuclearDensityTestTB.Text, sunLabTB.Text, timesheetID, true, ref tHours);
                sunLabTB.Text = string.Empty;
                sunNuclearDensityTestTB.Text = string.Empty;
            }

            // Monday
            if (monLabTB.Text != string.Empty && monLabTB.Text != "0")
            {
                if (!hoursValidator(monLabTB.Text, "Monday", false))
                {
                    valid = false;
                }
            }
            if (valid)
            {
                tm.createLabTest("Monday", monNuclearDensityTestTB.Text, monLabTB.Text, timesheetID, true, ref tHours);
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
                tm.createLabTest("Tuesday", tuesNuclearDensityTestTB.Text, tuesLabTB.Text, timesheetID, true, ref tHours);
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
                tm.createLabTest("Wednesday", wedsNuclearDensityTestTB.Text, wedsLabTB.Text, timesheetID, true, ref tHours);
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
                tm.createLabTest("Thursday", thursNuclearDensityTestTB.Text, thursLabTB.Text, timesheetID, true, ref tHours);
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
                tm.createLabTest("Friday", friNuclearDensityTestTB.Text, friLabTB.Text, timesheetID, true, ref tHours);
                friLabTB.Text = string.Empty;
                friNuclearDensityTestTB.Text = string.Empty;
            }

            // SATURDAY
            if (satLabTB.Text != string.Empty && satLabTB.Text != "0")
            {
                if (!hoursValidator(satLabTB.Text, "Saturday", false))
                {
                    valid = false;
                }
            }
            if (valid)
            {
                tm.createLabTest("Saturday", satNuclearDensityTestTB.Text, satLabTB.Text, timesheetID, true, ref tHours);
                satLabTB.Text = string.Empty;
                satNuclearDensityTestTB.Text = string.Empty;
            }

            totalHoursLBL.Text = tHours.ToString("0.0");
            updateLabTotals();
            // update timesheet
            tm.updateTimeSheet(timesheetID, tHours, Convert.ToInt32(totalDistanceLBL.Text), Convert.ToInt32(totalTruckLBL.Text), Convert.ToDecimal(totalExpensesLBL.Text), "Updated", " ", " ");
            // update gridview
            summaryGV.DataBind();
        }
        else
        {
            msg = "Select Weekending date and Verify before attempting to complete Timesheet";
            dialogTitle = "Error: Weekending Date";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "InvalidEntry", "throwDialog();", true);
        }
    }

    protected void updateLabTestsBtn_Click(object sender, EventArgs e)
    {
        TimesheetManager tm = new TimesheetManager();
        decimal tHours = Convert.ToDecimal(totalHoursLBL.Text);
        bool valid = true;
        // Sunday
        if (sunLabTB.Text != string.Empty && sunLabTB.Text != "0")
        {
            if (!hoursValidator(sunLabTB.Text, "Sunday",true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            if (!tm.updateLabTest("Sunday", sunNuclearDensityTestTB.Text, sunLabTB.Text, timesheetID, true, ref tHours))
            {
                tm.createLabTest("Sunday", sunNuclearDensityTestTB.Text, sunLabTB.Text, timesheetID, true, ref tHours);
            }
            sunLabTB.Text = string.Empty;
        }

        // Monday
        if (monLabTB.Text != string.Empty && monLabTB.Text != "0")
        {
            if (!hoursValidator(sunLabTB.Text, "Monday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            if (!tm.updateLabTest("Monday", monNuclearDensityTestTB.Text, monLabTB.Text, timesheetID, true, ref tHours))
            {
                tm.createLabTest("Monday", monNuclearDensityTestTB.Text, monLabTB.Text, timesheetID, true, ref tHours);
            }
            monLabTB.Text = string.Empty;
        }

        // Tuesday
        if (tuesLabTB.Text != string.Empty && tuesLabTB.Text != "0")
        {
            if (!hoursValidator(tuesLabTB.Text, "Tuesday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            if (!tm.updateLabTest("Tuesday", tuesNuclearDensityTestTB.Text, tuesLabTB.Text, timesheetID, true, ref tHours))
            {
                tm.createLabTest("Tuesday", tuesNuclearDensityTestTB.Text, tuesLabTB.Text, timesheetID, true, ref tHours);
            }
            tuesLabTB.Text = string.Empty;
        }

        // Wednessday
        if (wedsLabTB.Text != string.Empty && wedsLabTB.Text != "0")
        {
            if (!hoursValidator(wedsLabTB.Text, "Wednesday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            if (!tm.updateLabTest("Wednesday", wedsNuclearDensityTestTB.Text, wedsLabTB.Text, timesheetID, true, ref tHours))
            {
                tm.createLabTest("Wednesday", wedsNuclearDensityTestTB.Text, wedsLabTB.Text, timesheetID, true, ref tHours);
            }
            wedsLabTB.Text = string.Empty;
        }
        // Thursday
        if (thursLabTB.Text != string.Empty && thursLabTB.Text != "0")
        {
            if (!hoursValidator(thursLabTB.Text, "Thursday", true))
            {
                valid = false;
            }
        }

        if (valid)
        {
            if (!tm.updateLabTest("Thursday", thursNuclearDensityTestTB.Text, thursLabTB.Text, timesheetID, true, ref tHours))
            {
                tm.createLabTest("Thursday", thursNuclearDensityTestTB.Text, thursLabTB.Text, timesheetID, true, ref tHours);
            }
            thursLabTB.Text = string.Empty;
        }
        // Friday
        if (friLabTB.Text != string.Empty && friLabTB.Text != "0")
        {
            if (!hoursValidator(friLabTB.Text, "Friday", true))
            {
                if (!tm.updateLabTest("Friday", friNuclearDensityTestTB.Text, friLabTB.Text, timesheetID, true, ref tHours))
                {
                    tm.createLabTest("Friday", friNuclearDensityTestTB.Text, friLabTB.Text, timesheetID, true, ref tHours);
                }
                friLabTB.Text = string.Empty;
            }
        }
        // Saturday
        if (satLabTB.Text != string.Empty && satLabTB.Text != "0")
        {
            if (!hoursValidator(satLabTB.Text, "Saturday", true))
            {
                valid = false;
            }
        }
        if (valid)
        {
            if (!tm.updateLabTest("Saturday", satNuclearDensityTestTB.Text, satLabTB.Text, timesheetID, true, ref tHours))
            {
                tm.createLabTest("Saturday", satNuclearDensityTestTB.Text, satLabTB.Text, timesheetID, true, ref tHours);
            }
            satLabTB.Text = string.Empty;
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

    protected void clearLabTests_Click(object sender, EventArgs e)
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

    /// <summary>
    /// update lab totals
    /// </summary>
    protected void updateLabTotals()
    {
        TimesheetManager tm = new TimesheetManager();
        decimal[] totals = tm.getLabTotalsForId(timesheetID, true);
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

    protected void updateTimeSheetTotals()
    {
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
    protected void HighLightRows() {
        foreach (int item in rowIDs)
        {
            summaryGV.Rows[item].BackColor = System.Drawing.Color.Red;
        }
    }

}