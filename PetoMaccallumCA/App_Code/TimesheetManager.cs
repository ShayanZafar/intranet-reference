using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using petomaccallumModel;

/// <summary>
/// Summary description for TimesheetManager
/// </summary>
public class TimesheetManager
{
    public TimesheetManager() { }

    /// <summary>
    /// copy all nonchargeable and chargeable records to their manager equivalents
    /// </summary>
    /// <param name="id"></param>
    public void copyTimesheet(int id)
    {
        // get all non-chargeable and chargeable records that pertain to timesheetID
        List<ChargeableJob> cJobs = getChargeableJobsForTimesheetID(id);
        List<NonChargeable> nCJobs = getNonChargeableForTimesheetID(id);
        List<EmpTest> tests = getEmpTestsForTimeSheet(id);

        // save each of these records into their manager equivalents
        foreach (ChargeableJob item in cJobs)
        {
            copyToManagerChargeable(item);
        }

        foreach (NonChargeable item in nCJobs)
        {
            copyToManagerNonChargeable(item);
        }

        foreach (EmpTest item in tests)
        {
            copyToManagerTest(item);
        }
    }

    /// <summary>
    /// copy chargeable record into a manager chargeable record
    /// </summary>
    /// <param name="j"></param>
    public void copyToManagerChargeable(ChargeableJob j)
    {
        using (var context = new PetoEntities())
        {
            try
            {
                ManagerChargeable m = new ManagerChargeable();

                m.BillingHours = j.EmpHours;
                m.PayRollHours = j.EmpHours;

                m.BillingAccomodation = j.Accomodations;
                m.PayRollAccomodation = j.Accomodations;

                m.BillingTravelDistance = j.TravelDistance;
                m.PayRollTravelDistance = j.TravelDistance;

                m.TruckDistance = j.TruckDistance;

                m.TimeSheetId = j.TimeSheetId;
                m.ProjectId = j.ProjectId.Value;

                m.Classification = j.Classification;
                m.Activity = j.Activity;
                m.Day = j.Day;

                m.Remarks = j.Remarks;

                context.ManagerChargeables.AddObject(m);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    /// <summary>
    /// copy nonchargeable record to manager nonchargeable record
    /// </summary>
    /// <param name="n"></param>
    public void copyToManagerNonChargeable(NonChargeable n)
    {
        using (var context = new PetoEntities())
        {
            try
            {
                ManagerNonChargeable mNCh = new ManagerNonChargeable();

                mNCh.Hours = n.Hours;
                mNCh.Accomodations = n.Accomodation;
                mNCh.Misc = n.Misc;
                mNCh.Distance = n.Distance;

                mNCh.Remarks = n.Remarks;
                mNCh.TimeSheetId = n.TimeSheetId;
                mNCh.TypeHours = n.TypeHours;
                mNCh.TypeExpense = n.TypeExpense;
                mNCh.Day = n.Day;

                context.ManagerNonChargeables.AddObject(mNCh);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    /// <summary>
    /// create chargeable job in db
    /// </summary>
    /// <param name="j"></param>
    public string createChargeable(string hours, string accom, string distance, string truckDist, string misc, string day, string projectNo, string classification, string activity, int timesheetID, string remarks,
        ref decimal hoursLBL, ref int distLBL, ref int truckLBL, ref decimal expenseLBL)
    {
        using (var context = new PetoEntities())
        {
            try
            {
                if (timesheetID > 0 && hours != "0" && hours != string.Empty || accom != "0" && accom != string.Empty || distance != "0" && distance != string.Empty || misc != "0" && misc != string.Empty || truckDist != "0" && truckDist != string.Empty)
                {
                    ChargeableJob c = new ChargeableJob();

                    if (hours != "0" && hours != string.Empty)
                    {
                        c.EmpHours = Convert.ToDecimal(hours);
                        hoursLBL += c.EmpHours;
                    }
                    if (accom != "0" && accom != string.Empty)
                    {
                        c.Accomodations = Convert.ToDecimal(accom);
                        expenseLBL += Convert.ToDecimal(c.Accomodations);
                    }
                    if (distance != "0" && distance != string.Empty)
                    {
                        c.TravelDistance = Convert.ToInt32(distance);
                        distLBL += Convert.ToInt32(distance);
                    }
                    if (truckDist != "0" && truckDist != string.Empty)
                    {
                        c.TruckDistance = Convert.ToInt32(truckDist);
                        truckLBL += Convert.ToInt32(truckDist);
                    }

                    if (misc != "0" && misc != string.Empty)
                    {
                        c.Misc = Convert.ToDecimal(misc);
                        expenseLBL += Convert.ToDecimal(misc);
                    }
                    if (remarks != string.Empty)
                    {
                        c.Remarks = remarks;
                    }
                    if (c.EmpHours > 0 || c.TravelDistance > 0 || c.Accomodations > 0 || c.Misc > 0 || c.TruckDistance > 0)
                    {
                        c.Day = day;
                        c.ProjectId = idForProjectNo(projectNo);

                        if (classification != "Select")
                        {
                            c.Classification = classification;
                        }

                        c.Activity = activity;
                        c.TimeSheetId = Convert.ToInt32(timesheetID);

                        context.ChargeableJobs.AddObject(c);
                        context.SaveChanges();
                        return "Data was successfully saved";
                    }
                    else
                    {
                        return "Data was not Entered in input fields.";
                    }
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    /// <summary>
    /// create manager chargeable job in db
    /// </summary>
    /// <param name="j"></param>
    public string createManagerChargeable(string bHours, string bAccom, string bDist, string truckDist, string bMisc,
        string day, string projectNo, string classification, string activity, int timesheetID, string remarks, ref decimal hoursLBL, ref int distLBL, ref int truckLBL, ref decimal expenseLBL)
    {
        using (var context = new PetoEntities())
        {
            try
            {
                if (timesheetID > 0 && bHours != "0" && bHours != string.Empty || bAccom != "0" && bAccom != string.Empty || bDist != "0" && bDist != string.Empty || bMisc != "0" && bMisc != string.Empty || truckDist != "0" && truckDist != string.Empty)
                {
                    ManagerChargeable c = new ManagerChargeable();
                    // Hours

                    if (bHours != "0" && bHours != string.Empty)
                    {
                        c.BillingHours = Convert.ToDecimal(bHours);
                        if (c.BillingHours.HasValue)
                        {
                            hoursLBL += c.BillingHours.Value;
                        }
                    }
                    // Accomodation

                    if (bAccom != "0" && bAccom != string.Empty)
                    {
                        c.BillingAccomodation = Convert.ToDecimal(bAccom);
                        expenseLBL += Convert.ToDecimal(c.BillingAccomodation);
                    }
                    // Distance

                    if (bDist != "0" && bDist != string.Empty)
                    {
                        c.BillingTravelDistance = Convert.ToInt32(bDist);
                        distLBL += Convert.ToInt32(bDist);
                    }
                    // truck distance
                    if (truckDist != "0" && truckDist != string.Empty)
                    {
                        c.TruckDistance = Convert.ToInt32(truckDist);
                        truckLBL += Convert.ToInt32(truckDist);
                    }

                    // Misc
                    if (bMisc != "0" && bMisc != string.Empty)
                    {
                        c.BillingMisc = Convert.ToDecimal(bMisc);
                        expenseLBL += Convert.ToDecimal(c.BillingMisc);
                    }

                    if (remarks != string.Empty)
                    {
                        c.Remarks = remarks;
                    }
                    if (c.BillingHours > 0 || c.BillingTravelDistance > 0 || c.BillingAccomodation > 0 || c.BillingMisc > 0 || c.TruckDistance > 0)
                    {
                        c.Day = day;
                        c.ProjectId = idForProjectNo(projectNo);

                        if (classification != "Select")
                        {
                            c.Classification = classification;
                        }

                        c.Activity = activity;
                        c.TimeSheetId = Convert.ToInt32(timesheetID);

                        context.ManagerChargeables.AddObject(c);
                        context.SaveChanges();
                        return "Data was successfully saved";
                    }
                    else
                    {
                        return "Data was not Entered in input fields.";
                    }
                }
                else
                {
                    return "Input values are empty";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    /// <summary>
    /// create a Manager NonChargeable Record
    /// </summary>
    /// <param name="n"></param>
    public string createManagerNonChargeable(string hours, string accom, string distance, string misc, string day, string expense, string hourType, int timesheetID, string remarks,
        ref decimal hoursLBL, ref int distLBL, ref decimal expenseLBL)
    {
        using (var context = new PetoEntities())
        {
            try
            {
                // to ensure only not null values are saved
                if (timesheetID > 0 && hours != "0" && hours != string.Empty || accom != "0" && accom != string.Empty || distance != "0" && distance != string.Empty || misc != "0" && misc != string.Empty)
                {
                    ManagerNonChargeable nonCh = new ManagerNonChargeable();
                    if (hours != "0" && hours != string.Empty)
                    {
                        nonCh.Hours = Convert.ToDecimal(hours);
                        if (hourType != "Select")
                        {
                            nonCh.TypeHours = hourType;
                        }
                        if (nonCh.Hours.HasValue)
                        {
                            hoursLBL += nonCh.Hours.Value;
                        }
                    }
                    if (accom != "0" && accom != string.Empty)
                    {
                        nonCh.Accomodations = Convert.ToDecimal(accom);
                        expenseLBL += Convert.ToDecimal(accom);
                    }
                    if (distance != "0" && distance != string.Empty)
                    {
                        nonCh.Distance = Convert.ToInt32(distance);
                        distLBL += Convert.ToInt32(distance);
                    }

                    if (remarks != string.Empty)
                    {
                        nonCh.Remarks = remarks;
                    }
                    if (misc != "0" && misc != string.Empty)
                    {
                        nonCh.Misc = Convert.ToDecimal(misc);
                        expenseLBL += Convert.ToDecimal(misc);
                    }

                    if (misc != "0" && misc != string.Empty || distance != "0" && distance != string.Empty || accom != "0" && accom != string.Empty)
                    {
                        if (expense != "Select")
                        {
                            nonCh.TypeExpense = expense;
                        }
                    }
                    if (misc != "0" && misc != string.Empty || distance != "0" && distance != string.Empty || accom != "0" && accom != string.Empty || hours != "0" && hours != string.Empty)
                    {
                        nonCh.TimeSheetId = timesheetID;
                    }
                    nonCh.Day = day;

                    // one of these must be selected in order to save
                    if (expense != "Select" || hours != "Select")
                    {
                        context.ManagerNonChargeables.AddObject(nonCh);
                        context.SaveChanges();
                    }
                }

                return "Data Saved Successfully";
            }
            catch (Exception ex)
            {
                return "Data was not saved due to: " + ex.Message;
            }
        }
    }

    /// <summary>
    /// create a NonChargeable Record
    /// </summary>
    /// <param name="n"></param>
    public string createNonChargeable(string hours, string accom, string distance, string misc, string day, string expense, string hourType, int timesheetID, string remarks,
        ref decimal hoursLBL, ref int distLBL, ref decimal expenseLBL)
    {
        using (var context = new PetoEntities())
        {
            try
            {
                // to ensure only not null values are saved
                if (timesheetID > 0 && (hours != "0" && hours != string.Empty) || (accom != "0" && accom != string.Empty) || (distance != "0" && distance != string.Empty) || (misc != "0" && misc != string.Empty))
                {
                    NonChargeable nonCh = new NonChargeable();
                    if (hours != "0" && hours != string.Empty)
                    {
                        nonCh.Hours = Convert.ToDecimal(hours);
                        // assign type of hour
                        if (hourType != "Select")
                        {
                            nonCh.TypeHours = hourType;
                        }
                        hoursLBL += nonCh.Hours;
                    }
                    if (accom != "0" && accom != string.Empty)
                    {
                        nonCh.Accomodation = Convert.ToDecimal(accom);
                        expenseLBL += Convert.ToDecimal(accom);
                    }
                    if (distance != "0" && distance != string.Empty)
                    {
                        nonCh.Distance = Convert.ToInt32(distance);
                        distLBL += Convert.ToInt32(distance);
                    }
                    if (remarks != string.Empty)
                    {
                        nonCh.Remarks = remarks;
                    }
                    if (misc != "0" && misc != string.Empty)
                    {
                        nonCh.Misc = Convert.ToDecimal(misc);
                        expenseLBL += Convert.ToDecimal(misc);
                    }
                    // assign expenses
                    if ((misc != "0" && misc != string.Empty) || (distance != "0" && distance != string.Empty) || (accom != "0" && accom != string.Empty))
                    {
                        if (expense != "Select")
                        {
                            nonCh.TypeExpense = expense;
                        }
                    }
                    if (misc != "0" && misc != string.Empty || distance != "0" && distance != string.Empty || accom != "0" && accom != string.Empty || hours != "0" && hours != string.Empty)
                    {
                        nonCh.TimeSheetId = timesheetID;
                    }
                    nonCh.Day = day;

                    // one of these must be selected in order to save
                    if (expense != "Select" || hours != "Select")
                    {
                        context.NonChargeables.AddObject(nonCh);
                        context.SaveChanges();
                    }
                }

                return "Data Saved Successfully";
            }
            catch (Exception ex)
            {
                return "Data was not saved due to: " + ex.Message;
            }
        }
    }

    /// <summary>
    /// create timesheet instance singleton for weekending. returns timesheetID
    /// </summary>
    /// <param name="t"></param>
    public int createTimesheet(TimeSheet t)
    {
        using (var context = new PetoEntities())
        {
            try
            {
                if (context.TimeSheets.FirstOrDefault(x => x.TimeSheetId == t.TimeSheetId) == null)
                {
                    context.TimeSheets.AddObject(t);
                    context.SaveChanges();
                    return t.TimeSheetId;
                }
                else
                {
                    return context.TimeSheets.FirstOrDefault(x => x.TimeSheetId == t.TimeSheetId).TimeSheetId;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    /// <summary>
    /// delete a chargeable in the timesheet
    /// </summary>
    /// <param name="timesheetID"></param>
    public void deleteChargeableForID(int ChargeableId)
    {
        using (var context = new PetoEntities())
        {
            ChargeableJob c = context.ChargeableJobs.FirstOrDefault(x => x.ChargeableId == ChargeableId);
            if (c != null)
            {
                // subtract the values from the timesheet
                TimeSheet t = context.TimeSheets.FirstOrDefault(x => x.TimeSheetId == c.TimeSheetId);
                if (t != null)
                {
                    if (c.EmpHours > 0)
                    {
                        t.TotalHours -= c.EmpHours;
                    }
                    if (c.TravelDistance > 0)
                    {
                        t.TotalDistance -= c.TravelDistance;
                    }
                    if (c.TruckDistance > 0)
                    {
                        t.TotalTruck -= c.TruckDistance;
                    }
                    if (c.Accomodations > 0 || c.Misc > 0)
                    {
                        t.TotalExpenses -= c.Accomodations - c.Misc;
                    }
                }
                context.ChargeableJobs.DeleteObject(c);
                context.SaveChanges();
            }
        }
    }
    /// <summary>
    /// delete a Summary record of Employee entered chargeable and nonchargeable jobs
    /// </summary>
    /// <param name="NonChargeableId"></param>
    /// <param name="ChargeableId"></param>
    /// <param name="id"></param>
    public void deleteSummary(int NonChargeableId, int ChargeableId, int id, int labID, bool manager)
    {
        using (var context = new PetoEntities())
        {
            TimeSheet t = context.TimeSheets.FirstOrDefault(x => x.TimeSheetId == id);
            // Lab test deletion
            if (labID > 0)
            {
                if (manager)
                {
                    // subtract hours from total hours in timesheet
                    ManagerTest m = context.ManagerTests.FirstOrDefault(x => x.TestId == labID);
                    if (m != null)
                    {
                        if (m.LabTest.HasValue)
                        {
                            t.TotalHours -= m.LabTest.Value;
                        }
                        context.ManagerTests.DeleteObject(m);
                        context.SaveChanges();
                    }
                }
                else
                {
                    EmpTest e = context.EmpTests.FirstOrDefault(x => x.TestId == labID);
                    if (e != null)
                    {
                        // subtract hours from total hours in timesheet
                        if (e.LabTest.HasValue)
                        {
                            t.TotalHours -= e.LabTest.Value;
                        }
                        context.EmpTests.DeleteObject(e);
                        context.SaveChanges();
                    }
                }
            }

            if (NonChargeableId > 0)
            {
                if (!manager)
                {
                    NonChargeable m = context.NonChargeables.FirstOrDefault(x => x.NonChargeId == NonChargeableId);
                    if (m != null)
                    {
                        // subtract the values from the times heet before deleting
                        if (t != null)
                        {
                            if (m.Hours > 0)
                            {
                                t.TotalHours -= m.Hours;
                            }
                            if (m.Distance > 0)
                            {
                                t.TotalDistance -= m.Distance;
                            }
                            if (m.Accomodation > 0)
                            {
                                t.TotalExpenses -= m.Accomodation;
                            }
                            if (m.Misc > 0)
                            {
                                t.TotalExpenses -= m.Misc;
                            }
                        }
                        // save changes to db
                        context.NonChargeables.DeleteObject(m);
                        context.SaveChanges();
                    }
                }
                else
                {
                    ManagerNonChargeable m = context.ManagerNonChargeables.FirstOrDefault(x => x.ManagerNonChargeableId == NonChargeableId);
                    if (m != null)
                    {
                        // subtract the values from the timesheet before deleting
                        if (t != null)
                        {
                            if (m.Hours > 0)
                            {
                                t.TotalHours -= m.Hours;
                            }
                            if (m.Distance > 0)
                            {
                                t.TotalDistance -= m.Distance;
                            }
                            if (m.TruckDistance > 0)
                            {
                                t.TotalTruck -= m.TruckDistance;
                            }
                            if (m.Accomodations > 0 || m.Misc > 0)
                            {
                                t.TotalExpenses -= m.Accomodations - m.Misc;
                            }
                        }
                        // save changes to db
                        context.ManagerNonChargeables.DeleteObject(m);
                        context.SaveChanges();
                    }
                }
            }

            if (ChargeableId > 0)
            {
                if (!manager)
                {
                    ChargeableJob c = context.ChargeableJobs.FirstOrDefault(x => x.ChargeableId == ChargeableId);

                    if (c != null)
                    {
                        if (t != null)
                        {
                            // hours
                            if (c.EmpHours > 0)
                            {
                                t.TotalHours -= c.EmpHours;
                            }

                            // distance
                            if (c.TravelDistance > 0)
                            {
                                t.TotalDistance -= c.TravelDistance;
                            }

                            // truck distance
                            if (c.TruckDistance > 0)
                            {
                                t.TotalTruck -= c.TruckDistance;
                            }
                            // Expenses
                            if (c.Accomodations > 0)
                            {
                                t.TotalExpenses -= c.Accomodations;
                            }
                            if (c.Misc > 0)
                            {
                                t.TotalExpenses -= c.Misc;
                            }
                        }
                        // save changes to db
                        context.ChargeableJobs.DeleteObject(c);
                        context.SaveChanges();
                    }
                }
                else
                {
                    ManagerChargeable c = context.ManagerChargeables.FirstOrDefault(x => x.ManagerChargeableId == ChargeableId);

                    if (c != null)
                    {
                        if (t != null)
                        {
                            // hours
                            if (c.BillingHours > 0)
                            {
                                t.TotalHours -= c.BillingHours;
                            }

                            // distance
                            if (c.BillingTravelDistance > 0)
                            {
                                t.TotalDistance -= c.BillingTravelDistance;
                            }

                            // truck distance
                            if (c.TruckDistance > 0)
                            {
                                t.TotalTruck -= c.TruckDistance;
                            }

                            // Expenses
                            if (c.BillingAccomodation > 0 || c.BillingMisc > 0)
                            {
                                t.TotalExpenses -= c.BillingAccomodation - c.BillingMisc;
                            }
                        }
                        // save changes to db
                        context.ManagerChargeables.DeleteObject(c);
                        context.SaveChanges();
                    }
                }
            }
        }
    }

    /// <summary>
    /// delete a nonchargeable job in the timesheet
    /// </summary>
    /// <param name="timesheetID"></param>
    public void deleteNonChargeableForID(int NonChargeId)
    {
        using (var context = new PetoEntities())
        {
            NonChargeable n = context.NonChargeables.FirstOrDefault(x => x.NonChargeId == NonChargeId);
            if (n != null)
            {
                // subtract the values from the timesheet before deleting
                TimeSheet t = context.TimeSheets.FirstOrDefault(x => x.TimeSheetId == n.TimeSheetId);

                if (t != null)
                {
                    if (n.Hours > 0)
                    {
                        t.TotalHours -= n.Hours;
                    }
                    if (n.Distance > 0)
                    {
                        t.TotalDistance -= n.Distance;
                    }

                    if (n.Accomodation > 0 || n.Misc > 0)
                    {
                        t.TotalExpenses -= n.Accomodation - n.Misc;
                    }
                }
                context.NonChargeables.DeleteObject(n);
                context.SaveChanges();
            }
        }
    }

    /// <summary>
    /// get activity names
    /// </summary>
    /// <returns></returns>
    public string[] getActivities()
    {
        using (var context = new PetoEntities())
        {
            return (from x in context.ClassificationActivities
                    orderby x.ActivityName
                    select x.ActivityName).Distinct().ToArray();
        }
    }

    /// <summary>
    /// get activities for the classification name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public string[] getActivityForClassification(string name)
    {
        using (var context = new PetoEntities())
        {
            return (from x in context.ClassificationActivities
                    orderby x.ActivityName
                    where x.ClassificationName == name
                    select x.ActivityName).Distinct().ToArray();
        }
    }

    /// <summary>
    /// get chargeable records for a project
    /// </summary>
    /// <param name="projectID"></param>
    /// <returns></returns>
    public List<ChargeableJob> getChargeableForProjectID(int projectID)
    {
        using (var context = new PetoEntities())
        {
            return (from x in context.ChargeableJobs
                    where x.ProjectId == projectID
                    select x).ToList();
        }
    }

    /// <summary>
    /// get chargeablejobs for the timesheet
    /// </summary>
    /// <param name="timesheetID"></param>
    /// <returns></returns>
    public List<ChargeableJob> getChargeableJobsForTimesheetID(int timesheetID)
    {
        using (var context = new PetoEntities())
        {
            return (from x in context.ChargeableJobs
                    where x.TimeSheetId == timesheetID
                    select x).ToList();
        }
    }

    /// <summary>
    ///  get the classifications related to the project
    /// </summary>
    /// <param name="projectNo"></param>
    /// <returns></returns>
    public string[] getClassificationForProject(string projectNo)
    {
        using (var context = new PetoEntities())
        {
            return (from x in context.ClassificationActivities
                    orderby x.ClassificationName
                    where x.ProjectNo == projectNo
                    select x.ClassificationName).Distinct().ToArray();
        }
    }

    /// <summary>
    /// get classification names
    /// </summary>
    /// <returns></returns>
    public string[] getClassifications()
    {
        using (var context = new PetoEntities())
        {
            return (from x in context.ClassificationActivities
                    orderby x.ClassificationName
                    select x.ClassificationName).Distinct().ToArray();
        }
    }

    /// <summary>
    /// get employee names
    /// </summary>
    /// <returns></returns>
    public string[] getData()
    {
        using (var context = new PetoEntities())
        {
            return (from x in context.TimeSheets
                    select x.EmployeeName).Distinct().ToArray();
        }
    }

    /// <summary>
    /// get the employee record for the username
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public User getEmployeeForId(string username)
    {
        using (var context = new PetoEntities())
        {
            return context.Users.FirstOrDefault(x => x.username == username);
        }
    }

    public User getEmployeeForId(int id)
    {
        using (var context = new PetoEntities())
        {
            return context.Users.FirstOrDefault(x => x.UserId == id);
        }
    }

    /// <summary>
    /// get all types of expenses
    /// </summary>
    /// <returns></returns>
    public string[] getExpenses()
    {
        using (var context = new PetoEntities())
        {
            return (from x in context.ExpenseHeaders
                    select x.Name).ToArray();
        }
    }

    /// <summary>
    /// get manager chargeable records for timesheetID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public List<ManagerChargeable> getManagerChargeableForID(int id)
    {
        using (var context = new PetoEntities())
        {
            return (from x in context.ManagerChargeables
                    where x.TimeSheetId == id
                    select x).ToList();
        }
    }

    /// <summary>
    /// get manager non chargeable records for timesheetID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public List<ManagerNonChargeable> getManagerNonChargeableForID(int id)
    {
        using (var context = new PetoEntities())
        {
            return (from x in context.ManagerNonChargeables
                    where x.TimeSheetId == id
                    select x).ToList();
        }
    }

    /// <summary>
    /// get ManagerNonChargeable for timesheet
    /// </summary>
    /// <param name="id"></param>
    /// <param name="expense"></param>
    /// <param name="hours"></param>
    /// <returns></returns>
    public List<ManagerNonChargeable> getManagerNonChargeableForTimesheetID(string id, string expense, string hours)
    {
        using (var context = new PetoEntities())
        {
            int timeId = Convert.ToInt32(id);
            if (hours != string.Empty && expense != string.Empty)
            {
                return (from x in context.ManagerNonChargeables
                        where x.TimeSheetId == timeId && x.TypeHours == hours && x.TypeExpense == expense
                        select x).ToList();
            }
            else if (hours != string.Empty)
            {
                return (from x in context.ManagerNonChargeables
                        where x.TimeSheetId == timeId && x.TypeHours == hours
                        select x).ToList();
            }
            else
            {
                return (from x in context.ManagerNonChargeables
                        where x.TimeSheetId == timeId && x.TypeExpense == expense
                        select x).ToList();
            }
        }
    }

    /// <summary>
    /// merge nonchargeable and chargeable records into one list of ManagerSummary, allow sorting
    /// </summary>
    /// <param name="id"></param>
    /// <param name="sort"></param>
    /// <returns></returns>
    public List<ManagerSummary> getManagerSummary(int id, string sort)
    {
        using (var context = new PetoEntities())
        {
            List<ManagerSummary> summary = new List<ManagerSummary>();
            // non chargeable
            List<ManagerNonChargeable> list1 = (from x in context.ManagerNonChargeables
                                                where x.TimeSheetId == id
                                                select x).ToList();

            // non chargeable data
            foreach (ManagerNonChargeable item in list1)
            {
                ManagerSummary s = new ManagerSummary();
                s.TimeSheetId = item.TimeSheetId;
                s.NonChargeableId = item.ManagerNonChargeableId;

                s.TypeExpense = item.TypeExpense;
                s.TypeHours = item.TypeHours;

                if (item.Hours.HasValue)
                {
                    s.Hours = item.Hours.Value;
                }
                if (item.Distance.HasValue)
                {
                    s.Distance = item.Distance.Value;
                }

                if (item.TruckDistance.HasValue)
                {
                    s.TruckDistance = item.TruckDistance.Value;
                }

                if (item.Misc.HasValue)
                {
                    s.Misc = item.Misc.Value;
                }
                if (item.Accomodations.HasValue)
                {
                    s.Accom = item.Accomodations.Value;
                }
                s.Day = item.Day;

                s.Remarks = item.Remarks;

                summary.Add(s);
            }
            // chargeable data
            List<ManagerChargeable> list2 = (from x in context.ManagerChargeables
                                             where x.TimeSheetId == id
                                             select x).ToList();

            foreach (ManagerChargeable item in list2)
            {
                ManagerSummary s = new ManagerSummary();

                s.TimeSheetId = item.TimeSheetId;
                s.ChargeableId = item.ManagerChargeableId;

                if (item.BillingHours.HasValue)
                {
                    s.Hours = item.BillingHours.Value;
                }
                if (item.BillingAccomodation.HasValue)
                {
                    s.Accom = item.BillingAccomodation.Value;
                }
                if (item.BillingTravelDistance.HasValue)
                {
                    s.Distance = item.BillingTravelDistance.Value;
                }

                if (item.TruckDistance.HasValue)
                {
                    s.TruckDistance = item.TruckDistance.Value;
                }
                if (item.BillingMisc.HasValue)
                {
                    s.Misc = item.BillingMisc.Value;
                }
                s.Day = item.Day;
                s.Classification = item.Classification;
                s.Activity = item.Activity;
                s.ProjectId = item.ProjectId;
                s.Remarks = item.Remarks;
                s.ProjectNum = context.Projects.FirstOrDefault(x => x.ProjectId == item.ProjectId).ProjectNo;
                s.ProjectName = context.Projects.FirstOrDefault(x => x.ProjectId == item.ProjectId).ProjectName;
                summary.Add(s);
            }

            // Lab Test data
            List<ManagerTest> Tests = (from x in context.ManagerTests
                                       where x.TimeSheetId == id
                                       select x).ToList();

            foreach (ManagerTest item in Tests)
            {
                ManagerSummary s = new ManagerSummary();

                s.Day = item.Day;
                s.TestId = item.TestId;
                s.TimeSheetId = item.TimeSheetId;
                if (item.LabTest.HasValue)
                {
                    s.labTest = item.LabTest.Value;
                }
                if (item.DensityTest.HasValue)
                {
                    s.DensityTest = item.DensityTest.Value;
                }

                summary.Add(s);
            }

            // sort the list
            switch (sort)
            {
                case "Hours":
                    return summary.OrderBy(x => x.Hours).ToList();
                case "Distance":
                    return summary.OrderBy(x => x.Distance).ToList();
                case "DensityTest":
                    return summary.OrderBy(x => x.DensityTest).ToList();
                case "Classification":
                    return summary.OrderBy(x => x.Classification).ToList();
                case "Misc":
                    return summary.OrderBy(x => x.Misc).ToList();
                case "TypeExpense":
                    return summary.OrderBy(x => x.TypeExpense).ToList();
                case "TypeHours":
                    return summary.OrderBy(x => x.TypeHours).ToList();
                case "Remarks":
                    return summary.OrderBy(x => x.Remarks).ToList();
                case "Activity":
                    return summary.OrderBy(x => x.Activity).ToList();
                case "BillingAccom":
                    return summary.OrderBy(x => x.BillingAccom).ToList();
                case "BillingDistance":
                    return summary.OrderBy(x => x.BillingDistance).ToList();
                case "BillingHours":
                    return summary.OrderBy(x => x.BillingHours).ToList();
                case "BillingMisc":
                    return summary.OrderBy(x => x.BillingMisc).ToList();
                case "Day":
                    return sortDayOfWeek(summary);
                case "ProjectNum":
                    return summary.OrderBy(x => x.ProjectNum).ToList();
                case "ProjectName":
                    return summary.OrderBy(x => x.ProjectName).ToList();
                case "LabTest":
                    return summary.OrderBy(x => x.labTest).ToList();
                case "TruckDistance":
                    return summary.OrderBy(x => x.TruckDistance).ToList();
                case "Accom":
                    return summary.OrderBy(x => x.Accom).ToList();
                default:
                    return sortDayOfWeek(summary);
            }
        }
    }

    /// <summary>
    /// get Summary
    /// </summary>
    /// <param name="id"></param>
    /// <param name="sort"></param>
    /// <returns></returns>
    public List<Summary> getSummary(int id, string sort)
    {
        using (var context = new PetoEntities())
        {
            List<Summary> summary = new List<Summary>();
            // non chargeable
            List<NonChargeable> list1 = (from x in context.NonChargeables
                                         where x.TimeSheetId == id
                                         select x).ToList();

            // non chargeable data
            foreach (NonChargeable item in list1)
            {
                Summary s = new Summary();
                s.TimeSheetId = item.TimeSheetId;
                s.NonChargeableId = item.NonChargeId;

                s.TypeExpense = item.TypeExpense;
                s.TypeHours = item.TypeHours;

                s.Hours = item.Hours;

                if (item.Distance.HasValue)
                {
                    s.Distance = item.Distance.Value;
                }
                if (item.TruckDistance.HasValue)
                {
                    s.TruckDistance = item.TruckDistance.Value;
                }
                if (item.Misc.HasValue)
                {
                    s.Misc = item.Misc.Value;
                }
                if (item.Accomodation != null)
                {
                    s.Accom = item.Accomodation.Value;
                }
                s.Day = item.Day;

                s.Remarks = item.Remarks;

                summary.Add(s);
            }
            // chargeable data
            List<ChargeableJob> list2 = (from x in context.ChargeableJobs
                                         where x.TimeSheetId == id
                                         select x).ToList();

            foreach (ChargeableJob item in list2)
            {
                Summary s = new Summary();

                s.TimeSheetId = item.TimeSheetId;
                s.ChargeableId = item.ChargeableId;

                s.Hours = item.EmpHours;

                if (item.Accomodations.HasValue)
                {
                    s.Accom = item.Accomodations.Value;
                }
                if (item.TravelDistance.HasValue)
                {
                    s.Distance = item.TravelDistance.Value;
                }
                if (item.TruckDistance.HasValue)
                {
                    s.TruckDistance = item.TruckDistance.Value;
                }
                if (item.Misc.HasValue)
                {
                    s.Misc = item.Misc.Value;
                }
                s.Day = item.Day;
                s.Classification = item.Classification;
                s.Activity = item.Activity;
                s.ProjectId = item.ProjectId.Value;
                s.ProjectNum = context.Projects.FirstOrDefault(x => x.ProjectId == item.ProjectId.Value).ProjectNo;
                s.ProjectName = context.Projects.FirstOrDefault(x => x.ProjectId == item.ProjectId.Value).ProjectName;
                s.Remarks = item.Remarks;

                summary.Add(s);
            }
            // Lab Test data
            List<EmpTest> Tests = (from x in context.EmpTests
                                   where x.TimeSheetId == id
                                   select x).ToList();

            foreach (EmpTest item in Tests)
            {
                Summary s = new Summary();

                s.Day = item.Day;
                s.TestId = item.TestId;
                s.TimeSheetId = item.TimeSheetId;
                if (item.DensityTest.HasValue)
                {
                    s.DensityTest = item.DensityTest.Value;
                }
                if (item.LabTest.HasValue)
                {
                    s.labTest = item.LabTest.Value;
                }

                summary.Add(s);
            }
            // sort the list
            switch (sort)
            {
                case "Hours":
                    return summary.OrderBy(x => x.Hours).ToList();
                case "Distance":
                    return summary.OrderBy(x => x.Distance).ToList();
                case "DensityTest":
                    return summary.OrderBy(x => x.DensityTest).ToList();
                case "Classification":
                    return summary.OrderBy(x => x.Classification).ToList();
                case "Misc":
                    return summary.OrderBy(x => x.Misc).ToList();
                case "TypeExpense":
                    return summary.OrderBy(x => x.TypeExpense).ToList();
                case "TypeHours":
                    return summary.OrderBy(x => x.TypeHours).ToList();
                case "Remarks":
                    return summary.OrderBy(x => x.Remarks).ToList();
                case "Activity":
                    return summary.OrderBy(x => x.Activity).ToList();
                case "Day":
                    return sortDayOfWeek(summary);
                case "ProjectNum":
                    return summary.OrderBy(x => x.ProjectNum).ToList();
                case "ProjectName":
                    return summary.OrderBy(x => x.ProjectName).ToList();
                case "LabTest":
                    return summary.OrderBy(x => x.labTest).ToList();
                case "TruckDistance":
                    return summary.OrderBy(x => x.TruckDistance).ToList();
                case "Accom":
                    return summary.OrderBy(x => x.Accom).ToList();
                default:
                    return sortDayOfWeek(summary);
            }
        }
    }

    /// <summary>
    /// get nonchargeable for timesheet
    /// </summary>
    /// <param name="timesheetID"></param>
    /// <returns></returns>
    public List<NonChargeable> getNonChargeableForTimesheetID(int timesheetID)
    {
        using (var context = new PetoEntities())
        {
            return (from x in context.NonChargeables
                    where x.TimeSheetId == timesheetID
                    select x).ToList();
        }
    }

    /// <summary>
    /// get nonchargeable for timesheet
    /// </summary>
    /// <param name="id"></param>
    /// <param name="expense"></param>
    /// <param name="hours"></param>
    /// <returns></returns>
    public List<NonChargeable> getNonChargeableForTimesheetID(string id, string expense, string hours)
    {
        using (var context = new PetoEntities())
        {
            int timeId = Convert.ToInt32(id);
            if (hours != string.Empty && expense != string.Empty)
            {
                return (from x in context.NonChargeables
                        where x.TimeSheetId == timeId && x.TypeHours == hours && x.TypeExpense == expense
                        select x).ToList();
            }
            else if (hours != string.Empty)
            {
                return (from x in context.NonChargeables
                        where x.TimeSheetId == timeId && x.TypeHours == hours
                        select x).ToList();
            }
            else
            {
                return (from x in context.NonChargeables
                        where x.TimeSheetId == timeId && x.TypeExpense == expense
                        select x).ToList();
            }
        }
    }

    /// <summary>
    /// get project for project number
    /// </summary>
    /// <param name="projectNo"></param>
    /// <returns></returns>
    public Project getProjectForID(string projectNo)
    {
        using (var context = new PetoEntities())
        {
            return context.Projects.FirstOrDefault(x => x.ProjectNo == projectNo);
        }
    }

    /// <summary>
    /// get project for project ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Project getProjectForID(int id)
    {
        using (var context = new PetoEntities())
        {
            return context.Projects.FirstOrDefault(x => x.ProjectId == id);
        }
    }

    /// <summary>
    /// get project names
    /// </summary>
    /// <returns></returns>
    public string[] getProjectNames()
    {
        using (var context = new PetoEntities())
        {
            return (from x in context.Projects
                    orderby x.ProjectName
                    select x.ProjectName).Distinct().ToArray();
        }
    }

    /// <summary>
    /// get project no for project ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public string getProjectNoForID(int id)
    {
        using (var context = new PetoEntities())
        {
            if (id > 0)
            {
                return context.Projects.FirstOrDefault(x => x.ProjectId == id).ProjectNo;
            }
            else
            {
                return "";
            }
        }
    }

    /// <summary>
    /// get project numbers from projects table
    /// </summary>
    /// <returns></returns>
    public string[] getProjectNumbers()
    {
        using (var context = new PetoEntities())
        {
            return (from x in context.Projects
                    orderby x.ProjectNo
                    select x.ProjectNo).Distinct().ToArray();
        }
    }

    /// <summary>
    /// get timesheet for timesheetID
    /// </summary>
    /// <param name="timesheetID"></param>
    /// <returns></returns>
    public TimeSheet getTimesheetForID(int timesheetID)
    {
        using (var context = new PetoEntities())
        {
            return context.TimeSheets.FirstOrDefault(x => x.TimeSheetId == timesheetID);
        }
    }

    /// <summary>
    /// get timesheet for status selected
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public List<TimeSheet> getTimesheetsForStatus(string status, string branch, string weekending)
    {
        using (var context = new PetoEntities())
        {
            switch (status)
            {
                case "Updated":
                    return (from x in context.TimeSheets
                            orderby x.DateModified
                            where (x.Status == "Updated" || x.Status == "Updated - Incomplete") && x.Branch == branch && x.WeekEnding == weekending
                            select x).ToList();
                case "Approved":
                    return (from x in context.TimeSheets
                            orderby x.DateApproved
                            where x.Status == "Approved" && x.Branch == branch && x.WeekEnding == weekending
                            select x).ToList();
                case "Synchronized":
                    return (from x in context.TimeSheets
                            orderby x.DateApproved
                            where x.Status == "Synchronized" && x.Branch == branch && x.WeekEnding == weekending
                            select x).ToList();
                case "Updated - Manager":
                    return (from x in context.TimeSheets
                            orderby x.WeekEnding
                            where x.Status == "Updated - Manager" && x.Branch == branch && x.WeekEnding == weekending
                            select x).ToList();
                case "Rejected":
                    return (from x in context.TimeSheets
                            orderby x.DateApproved
                            where x.Status == "Rejected" && x.Branch == branch && x.WeekEnding == weekending
                            select x).ToList();
                case "Completed":
                    return (from x in context.TimeSheets
                            orderby x.WeekEnding
                            where x.Status == "Completed" && x.Branch == branch && x.WeekEnding == weekending
                            select x).ToList();
                case "Started":
                    return (from x in context.TimeSheets
                            orderby x.WeekEnding
                            where x.Status == "Started" && x.Branch == branch && x.WeekEnding == weekending
                            select x).ToList();
                default:
                    return (from x in context.TimeSheets
                            orderby x.WeekEnding
                            where x.Branch == branch && x.WeekEnding == weekending
                            select x).ToList();
            }
        }
    }

    /// <summary>
    /// get timesheet statuses
    /// </summary>
    /// <returns></returns>
    public List<string> getTimesheetStatus()
    {
        using (var context = new PetoEntities())
        {
            return (from x in context.TimesheetStatus1
                    orderby x.status
                    select x.status).ToList();
        }
    }

    /// <summary>
    /// get username for empNo
    /// </summary>
    /// <param name="empID"></param>
    /// <returns></returns>
    public string getUsernameForEmpNo(string empID)
    {
        using (var context = new PetoEntities())
        {
            return context.Users.FirstOrDefault(x => x.empNo == empID).username;
        }
    }

    /// <summary>
    /// get weekending from a timesheet
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public string getWeekendingForTimeSheet(int id)
    {
        using (var context = new PetoEntities())
        {
            return context.TimeSheets.FirstOrDefault(x => x.TimeSheetId == id).WeekEnding;
        }
    }

    /// <summary>
    /// get type of work list
    /// </summary>
    /// <returns></returns>
    public string[] getWorkType()
    {
        using (var context = new PetoEntities())
        {
            return (from x in context.TypeOfWorks
                    orderby x.name
                    select x.name).ToArray();
        }
    }

    /// <summary>
    /// get timesheetid for weekending date
    /// </summary>
    /// <param name="weekending"></param>
    /// <returns></returns>
    public int idForDate(string weekending, int userID)
    {
        using (var context = new PetoEntities())
        {
            try
            {
                if (context.TimeSheets.FirstOrDefault(x => x.WeekEnding == weekending && x.EmployeeId == userID) == null)
                {
                    return 0;
                }
                else
                {
                    return context.TimeSheets.FirstOrDefault(x => x.WeekEnding == weekending && x.EmployeeId == userID).TimeSheetId;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }

    /// <summary>
    /// get projectid for project number
    /// </summary>
    /// <param name="no"></param>
    /// <returns></returns>
    public int idForProjectNo(string no)
    {
        using (var context = new PetoEntities())
        {
            try
            {
                return context.Projects.FirstOrDefault(x => x.ProjectNo == no).ProjectId;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    /// <summary>
    /// get userid
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public int idForUsername(string username)
    {
        using (var context = new PetoEntities())
        {
            return context.Users.FirstOrDefault(x => x.username == username).UserId;
        }
    }

    /// <summary>
    /// update chargeable expense record
    /// </summary>
    /// <param name="hours"></param>
    /// <param name="accom"></param>
    /// <param name="distance"></param>
    /// <param name="misc"></param>
    /// <param name="day"></param>
    /// <param name="projectNo"></param>
    /// <param name="classification"></param>
    /// <param name="activity"></param>
    /// <param name="timesheetID"></param>
    /// <returns></returns>
    public bool updateChargeable(string hours, string accom, string distance, string truckDist, string misc, string day, int projectNo, string classification, string activity, int timesheetID, string remarks,
        ref decimal totalHours, ref int totalDist, ref int totalTruck, ref decimal totalExpense)
    {
        using (var context = new PetoEntities())
        {
            try
            {
                TimesheetManager tm = new TimesheetManager();
                ChargeableJob cJob = context.ChargeableJobs.FirstOrDefault(x => x.ProjectId == projectNo && x.TimeSheetId == timesheetID && x.Day == day && x.Classification == classification && x.Activity == activity);

                // update fields
                if (cJob != null)
                {
                    if (hours != string.Empty)
                    {
                        totalHours -= cJob.EmpHours;
                        cJob.EmpHours = Convert.ToDecimal(hours.Trim());
                        // totalize hours
                        totalHours += cJob.EmpHours;
                    }
                    if (misc != string.Empty)
                    {
                        if (cJob.Misc.HasValue)
                        {
                            totalExpense -= cJob.Misc.Value;
                        }
                        cJob.Misc = Convert.ToDecimal(misc.Trim());
                        totalExpense += cJob.Misc.Value;
                    }
                    if (projectNo > 0)
                    {
                        cJob.ProjectId = projectNo;
                    }

                    if (remarks != string.Empty)
                    {
                        cJob.Remarks = remarks;
                    }
                    if (classification != "Select")
                    {
                        cJob.Classification = classification;
                    }
                    if (activity != "Select")
                    {
                        cJob.Activity = activity;
                    }
                    if (distance != string.Empty)
                    {
                        // subtract the distance if it already exists
                        if (cJob.TravelDistance.HasValue)
                        {
                            totalDist -= cJob.TravelDistance.Value;
                        }
                        // assign inputted value
                        cJob.TravelDistance = Convert.ToInt32(distance.Trim());
                        // totalize distance
                        totalDist += cJob.TravelDistance.Value;
                    }

                    if (truckDist != string.Empty)
                    {
                        // subtract the distance if it already exists
                        if (cJob.TruckDistance.HasValue)
                        {
                            totalTruck -= cJob.TruckDistance.Value;
                        }
                        // assign inputted value
                        cJob.TruckDistance = Convert.ToInt32(truckDist);
                        // totalize truck dist
                        totalTruck += Convert.ToInt32(truckDist);
                    }

                    if (accom != string.Empty)
                    {
                        if (cJob.Accomodations.HasValue)
                        {
                            totalExpense -= cJob.Accomodations.Value;
                        }
                        cJob.Accomodations = Convert.ToDecimal(accom.Trim());
                        // totalize expense
                        totalExpense += cJob.Accomodations.Value;
                    }
                    context.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    /// <summary>
    /// update manager chargeable expense record
    /// </summary>
    /// <param name="hours"></param>
    /// <param name="accom"></param>
    /// <param name="distance"></param>
    /// <param name="misc"></param>
    /// <param name="day"></param>
    /// <param name="projectNo"></param>
    /// <param name="classification"></param>
    /// <param name="activity"></param>
    /// <param name="timesheetID"></param>
    /// <returns></returns>
    public bool updateManagerChargeable(string bHours, string bAccom, string bDist, string truckDist, string bMisc,
        string day, int projectNo, string classification, string activity, int timesheetID, string remarks, ref decimal totalHours, ref int totalDist, ref int totalTruck, ref decimal totalExpense)
    {
        using (var context = new PetoEntities())
        {
            try
            {
                TimesheetManager tm = new TimesheetManager();
                ManagerChargeable cJob = context.ManagerChargeables.FirstOrDefault(x => x.ProjectId == projectNo && x.TimeSheetId == timesheetID && x.Day == day && x.Classification == classification && x.Activity == activity);

                // update fields
                if (cJob != null)
                {
                    // Hours
                    if (bHours != string.Empty)
                    {
                        if (cJob.BillingHours.HasValue)
                        {
                            totalHours -= cJob.BillingHours.Value;
                        }
                        cJob.BillingHours = Convert.ToDecimal(bHours.Trim());
                        // totalize hours
                        totalHours += cJob.BillingHours.Value;
                    }

                    // Misc
                    if (bMisc != string.Empty)
                    {
                        if (cJob.BillingMisc.HasValue)
                        {
                            totalExpense -= cJob.BillingMisc.Value;
                        }
                        cJob.BillingMisc = Convert.ToDecimal(bMisc.Trim());
                        totalExpense += cJob.BillingMisc.Value;
                    }
                    if (projectNo > 0)
                    {
                        cJob.ProjectId = projectNo;
                    }

                    if (remarks != string.Empty)
                    {
                        cJob.Remarks = remarks;
                    }
                    if (classification != "Select")
                    {
                        cJob.Classification = classification;
                    }
                    if (activity != "Select")
                    {
                        cJob.Activity = activity;
                    }
                    // Distance
                    if (bDist != string.Empty)
                    {
                        if (cJob.BillingTravelDistance.HasValue)
                        {
                            totalDist -= cJob.BillingTravelDistance.Value;
                        }
                        // assign inputted value
                        cJob.BillingTravelDistance = Convert.ToInt32(bDist.Trim());
                        // totalize distance
                        totalDist += cJob.BillingTravelDistance.Value;
                    }
                    // truck distance
                    if (truckDist != string.Empty)
                    {
                        // subtract the value if it already exists
                        if (cJob.TruckDistance.HasValue)
                        {
                            totalTruck -= cJob.TruckDistance.Value;
                        }
                        cJob.TruckDistance = Convert.ToInt32(truckDist);
                        totalTruck += cJob.TruckDistance.Value;
                    }
                    // Accom
                    if (bAccom != string.Empty)
                    {
                        if (cJob.BillingAccomodation.HasValue)
                        {
                            totalExpense -= cJob.BillingAccomodation.Value;
                        }
                        cJob.BillingAccomodation = Convert.ToDecimal(bAccom.Trim());
                        // totalize expense
                        totalExpense += cJob.BillingAccomodation.Value;
                    }
                    context.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    /// <summary>
    /// update a non-chargeable record
    /// </summary>
    /// <param name="hours"></param>
    /// <param name="accom"></param>
    /// <param name="distance"></param>
    /// <param name="misc"></param>
    /// <param name="day"></param>
    /// <param name="density"></param>
    /// <param name="expense"></param>
    /// <param name="hourType"></param>
    /// <param name="timesheetID"></param>
    /// <returns></returns>
    public bool updateManagerNonChargeable(string hours, string accom, string distance, string misc, string day, string expense, string hourType, int timesheetID, string remarks,
        ref decimal totalHours, ref int totalDist, ref decimal totalExpense)
    {
        using (var context = new PetoEntities())
        {
            try
            {
                ManagerNonChargeable nCh = context.ManagerNonChargeables.FirstOrDefault(x => x.Day == day && x.TimeSheetId == timesheetID && (x.TypeExpense == expense || x.TypeHours == hourType));

                if (nCh != null)
                {
                    if (hours != "0" && hours != string.Empty)
                    {
                        if (nCh.Hours.HasValue)
                        {
                            totalHours -= nCh.Hours.Value;
                        }
                        nCh.Hours = Convert.ToDecimal(hours);
                        totalHours += nCh.Hours.Value;
                    }
                    if (misc != "0" && misc != string.Empty)
                    {
                        if (nCh.Misc.HasValue)
                        {
                            totalExpense -= nCh.Misc.Value;
                        }
                        nCh.Misc = Convert.ToDecimal(misc.Trim());
                        totalExpense += nCh.Misc.Value;
                    }

                    if (remarks != string.Empty)
                    {
                        nCh.Remarks = remarks;
                    }
                    if (expense != "Select")
                    {
                        nCh.TypeExpense = expense;
                    }
                    if (hourType != "Select")
                    {
                        nCh.TypeHours = hourType;
                    }
                    if (distance != "0" && distance != string.Empty)
                    {
                        if (nCh.Distance.HasValue)
                        {
                            totalDist -= nCh.Distance.Value;
                        }
                        // assign distance
                        nCh.Distance = Convert.ToInt32(distance.Trim());

                        if (nCh.Distance.HasValue)
                        {
                            totalDist += nCh.Distance.Value;
                        }
                    }
                    if (accom != "0" && accom != string.Empty)
                    {
                        if (nCh.Accomodations.HasValue)
                        {
                            totalExpense -= nCh.Accomodations.Value;
                        }
                        nCh.Accomodations = Convert.ToDecimal(accom.Trim());
                        totalExpense += nCh.Accomodations.Value;
                    }

                    // one drop down list value cannot be Select
                    if (expense != "Select" || hourType != "Select")
                    {
                        context.SaveChanges();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    /// <summary>
    /// update a non-chargeable record
    /// </summary>
    /// <param name="hours"></param>
    /// <param name="accom"></param>
    /// <param name="distance"></param>
    /// <param name="misc"></param>
    /// <param name="day"></param>
    /// <param name="density"></param>
    /// <param name="expense"></param>
    /// <param name="hourType"></param>
    /// <param name="timesheetID"></param>
    /// <returns></returns>
    public bool updateNonChargeable(string hours, string accom, string distance, string misc, string day, string expense, string hourType, int timesheetID, string remarks,
        ref decimal totalHours, ref int totalDist, ref decimal totalExpense)
    {
        using (var context = new PetoEntities())
        {
            try
            {
                NonChargeable nCh = context.NonChargeables.FirstOrDefault(x => x.Day == day && x.TimeSheetId == timesheetID && (x.TypeExpense == expense || x.TypeHours == hourType));

                if (nCh != null)
                {
                    if (hours != string.Empty)
                    {
                        totalHours -= nCh.Hours;
                        nCh.Hours = Convert.ToDecimal(hours);
                        totalHours += nCh.Hours;
                    }
                    if (misc != string.Empty)
                    {
                        if (nCh.Misc.HasValue)
                        {
                            totalExpense -= nCh.Misc.Value;
                        }
                        nCh.Misc = Convert.ToDecimal(misc.Trim());
                        totalExpense += nCh.Misc.Value;
                    }

                    if (remarks != string.Empty)
                    {
                        nCh.Remarks = remarks;
                    }
                    if (expense != "Select")
                    {
                        nCh.TypeExpense = expense;
                    }
                    if (hourType != "Select")
                    {
                        nCh.TypeHours = hourType;
                    }
                    if (distance != string.Empty)
                    {
                        if (nCh.Distance.HasValue)
                        {
                            totalDist -= nCh.Distance.Value;
                        }
                        // assign distance
                        nCh.Distance = Convert.ToInt32(distance.Trim());
                    }
                    if (accom != string.Empty)
                    {
                        if (nCh.Accomodation.HasValue)
                        {
                            totalExpense -= nCh.Accomodation.Value;
                        }
                        nCh.Accomodation = Convert.ToDecimal(accom.Trim());
                        totalExpense += nCh.Accomodation.Value;
                    }

                    // one drop down list value cannot be Select
                    if (expense != "Select" || hourType != "Select")
                    {
                        context.SaveChanges();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    /// <summary>
    /// update an existing timesheet
    /// </summary>
    /// <param name="id"></param>
    /// <param name="weekending"></param>
    /// <param name="status"></param>
    /// <param name="empComment"></param>
    /// <param name="mComment"></param>
    /// <param name="hours"></param>
    /// <param name="distance"></param>
    /// <param name="expenses"></param>
    public void updateTimeSheet(int id, decimal hours, int distance, int truckDist, decimal expenses, string status, string empComment, string mComment)
    {
        using (var context = new PetoEntities())
        {
            try
            {
                TimeSheet t = context.TimeSheets.FirstOrDefault(x => x.TimeSheetId == id);
                // determine who is trying to update the timesheet
                string name = Membership.GetUser().UserName;
                User user = context.Users.FirstOrDefault(x => x.username == name);

                // ensure timesheet exists and has not already been approved or completed
                if (t != null)
                {
                    if (status != string.Empty)
                    {
                        t.Status = status;
                    }

                    if (empComment != string.Empty)
                    {
                        t.EmployeeComments = empComment;
                    }
                    if (mComment != string.Empty)
                    {
                        t.ManagerComments = mComment;
                    }
                    t.ModifiedBy = user.username;
                    t.DateModified = DateTime.Now;

                    if (expenses > 0)
                    {
                        t.TotalExpenses = expenses;
                    }
                    else if (expenses == 0)
                    {
                        t.TotalExpenses = 0;
                    }
                    if (distance > 0)
                    {
                        t.TotalDistance = distance;
                    }
                    else if (distance == 0)
                    {
                        t.TotalDistance = 0;
                    }

                    // truck distance
                    if (truckDist > 0)
                    {
                        t.TotalTruck = truckDist;
                    }
                    else if (distance == 0)
                    {
                        t.TotalTruck = 0;
                    }

                    if (hours > 0)
                    {
                        t.TotalHours = hours;
                    }
                    else if (hours == 0)
                    {
                        t.TotalHours = 0;
                    }

                    // if timesheet is rejected copy manager comments into employee comments
                    if (t.Status == "Rejected")
                    {
                        t.EmployeeComments = t.EmployeeComments + "\n MANAGER COMMENT: \n " + mComment;
                    }

                    // only a manager can update an already approved timesheet
                    if (Roles.GetRolesForUser().Contains("Manager") && t.Status == "Approved")
                    {
                        t.ApprovedBy = user.username;
                        t.DateApproved = DateTime.Now;
                        t.ManagerComments = mComment;

                        context.SaveChanges();
                    }
                    else if (t.Status != "Approved")
                    {
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    /// <summary>
    /// get the totals for timesheetID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public decimal[] getChargeableTotalsForID(int id)
    {
        decimal[] result = new decimal[8];

        using (var context = new PetoEntities())
        {
            // get manager chargeable
            List<ManagerChargeable> mCh = (from x in context.ManagerChargeables
                                           where x.TimeSheetId == id
                                           select x).ToList();
            foreach (ManagerChargeable item in mCh)
            {
                switch (item.Day)
                {
                    case "Sunday":
                        if (item.BillingHours.HasValue)
                        {
                            result[0] += item.BillingHours.Value;
                        }
                        break;

                    case "Monday":
                        if (item.BillingHours.HasValue)
                        {
                            result[1] += item.BillingHours.Value;
                        }
                        break;

                    case "Tuesday":
                        if (item.BillingHours.HasValue)
                        {
                            result[2] += item.BillingHours.Value;
                        }
                        break;

                    case "Wednesday":
                        if (item.BillingHours.HasValue)
                        {
                            result[3] += item.BillingHours.Value;
                        }
                        break;

                    case "Thursday":
                        if (item.BillingHours.HasValue)
                        {
                            result[4] += item.BillingHours.Value;
                        }
                        break;

                    case "Friday":
                        if (item.BillingHours.HasValue)
                        {
                            result[5] += item.BillingHours.Value;
                        }
                        break;

                    case "Saturday":
                        if (item.BillingHours.HasValue)
                        {
                            result[6] += item.BillingHours.Value;
                        }
                        break;
                    default:
                        break;
                }
            }
            return result;
        }
    }

    public decimal[] getEmpChargeableTotalsForID(int id)
    {
        decimal[] result = new decimal[8];

        using (var context = new PetoEntities())
        {
            // get manager chargeable
            List<ChargeableJob> mCh = (from x in context.ChargeableJobs
                                       where x.TimeSheetId == id
                                       select x).ToList();
            foreach (ChargeableJob item in mCh)
            {
                switch (item.Day)
                {
                    case "Sunday":

                        result[0] += item.EmpHours;

                        break;

                    case "Monday":

                        result[1] += item.EmpHours;

                        break;

                    case "Tuesday":

                        result[2] += item.EmpHours;

                        break;

                    case "Wednesday":

                        result[3] += item.EmpHours;

                        break;

                    case "Thursday":

                        result[4] += item.EmpHours;

                        break;

                    case "Friday":

                        result[5] += item.EmpHours;

                        break;

                    case "Saturday":

                        result[6] += item.EmpHours;

                        break;
                    default:
                        break;
                }
            }
            return result;
        }
    }

    public decimal[] getNonChargeableTotalsForID(int id)
    {
        decimal[] result = new decimal[8];

        using (var context = new PetoEntities())
        {
            // manager non-chargeable
            List<ManagerNonChargeable> mNCh = (from x in context.ManagerNonChargeables
                                               where x.TimeSheetId == id
                                               select x).ToList();
            foreach (ManagerNonChargeable item in mNCh)
            {
                switch (item.Day)
                {
                    case "Sunday":
                        if (item.Hours.HasValue)
                        {
                            result[0] += item.Hours.Value;
                        }
                        break;

                    case "Monday":
                        if (item.Hours.HasValue)
                        {
                            result[1] += item.Hours.Value;
                        }
                        break;

                    case "Tuesday":
                        if (item.Hours.HasValue)
                        {
                            result[2] += item.Hours.Value;
                        }
                        break;

                    case "Wednesday":
                        if (item.Hours.HasValue)
                        {
                            result[3] += item.Hours.Value;
                        }
                        break;

                    case "Thursday":
                        if (item.Hours.HasValue)
                        {
                            result[4] += item.Hours.Value;
                        }
                        break;

                    case "Friday":
                        if (item.Hours.HasValue)
                        {
                            result[5] += item.Hours.Value;
                        }
                        break;

                    case "Saturday":
                        if (item.Hours.HasValue)
                        {
                            result[6] += item.Hours.Value;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        return result;
    }

    /// <summary>
    /// employee nonchargeable daily totals
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public decimal[] getEmpNonChargeableTotalsForID(int id)
    {
        decimal[] result = new decimal[8];

        using (var context = new PetoEntities())
        {
            // manager non-chargeable
            List<NonChargeable> mNCh = (from x in context.NonChargeables
                                        where x.TimeSheetId == id
                                        select x).ToList();
            foreach (NonChargeable item in mNCh)
            {
                switch (item.Day)
                {
                    case "Sunday":

                        result[0] += item.Hours;

                        break;

                    case "Monday":

                        result[1] += item.Hours;

                        break;

                    case "Tuesday":

                        result[2] += item.Hours;

                        break;

                    case "Wednesday":

                        result[3] += item.Hours;

                        break;

                    case "Thursday":

                        result[4] += item.Hours;

                        break;

                    case "Friday":

                        result[5] += item.Hours;

                        break;

                    case "Saturday":

                        result[6] += item.Hours;

                        break;
                    default:
                        break;
                }
            }
        }

        return result;
    }

    /// <summary>
    /// get lab totals for timesheet id manager toggles employee and manager lab totals
    /// </summary>
    /// <param name="id"></param>
    /// <param name="manager"></param>
    /// <returns></returns>
    public decimal[] getLabTotalsForId(int id, bool manager)
    {
        decimal[] result = new decimal[8];

        using (var context = new PetoEntities())
        {
            if (manager)
            {
                // manager lab hours
                List<ManagerTest> mNCh = (from x in context.ManagerTests
                                          where x.TimeSheetId == id
                                          select x).ToList();
                foreach (ManagerTest item in mNCh)
                {
                    switch (item.Day)
                    {
                        case "Sunday":

                            if (item.LabTest.HasValue)
                            {
                                result[0] += item.LabTest.Value;
                            }

                            break;

                        case "Monday":

                            if (item.LabTest.HasValue)
                            {
                                result[1] += item.LabTest.Value;
                            }

                            break;

                        case "Tuesday":

                            if (item.LabTest.HasValue)
                            {
                                result[2] += item.LabTest.Value;
                            }

                            break;

                        case "Wednesday":

                            if (item.LabTest.HasValue)
                            {
                                result[3] += item.LabTest.Value;
                            }

                            break;

                        case "Thursday":

                            if (item.LabTest.HasValue)
                            {
                                result[4] += item.LabTest.Value;
                            }

                            break;

                        case "Friday":

                            if (item.LabTest.HasValue)
                            {
                                result[5] += item.LabTest.Value;
                            }

                            break;

                        case "Saturday":

                            if (item.LabTest.HasValue)
                            {
                                result[6] += item.LabTest.Value;
                            }

                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                // manager lab hours
                List<EmpTest> mNCh = (from x in context.EmpTests
                                      where x.TimeSheetId == id
                                      select x).ToList();
                foreach (EmpTest item in mNCh)
                {
                    switch (item.Day)
                    {
                        case "Sunday":

                            if (item.LabTest.HasValue)
                            {
                                if (item.LabTest.HasValue)
                                {
                                    result[0] += item.LabTest.Value;
                                }
                            }

                            break;

                        case "Monday":

                            if (item.LabTest.HasValue)
                            {
                                result[1] += item.LabTest.Value;
                            }

                            break;

                        case "Tuesday":

                            if (item.LabTest.HasValue)
                            {
                                result[2] += item.LabTest.Value;
                            }

                            break;

                        case "Wednesday":

                            if (item.LabTest.HasValue)
                            {
                                result[3] += item.LabTest.Value;
                            }

                            break;

                        case "Thursday":

                            if (item.LabTest.HasValue)
                            {
                                result[4] += item.LabTest.Value;
                            }

                            break;

                        case "Friday":

                            if (item.LabTest.HasValue)
                            {
                                result[5] += item.LabTest.Value;
                            }

                            break;

                        case "Saturday":

                            if (item.LabTest.HasValue)
                            {
                                result[6] += item.LabTest.Value;
                            }

                            break;
                        default:
                            break;
                    }
                }
            }
        }

        return result;
    }

    public string[] summaryGVSearch(int id)
    {
        using (var context = new PetoEntities())
        {
            return (from x in context.ChargeableJobs
                    where x.TimeSheetId == id
                    select x.Day).ToArray();
        }
    }

    /// <summary>
    /// get row matching chargeable id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ChargeableJob getChargeForID(int id)
    {
        using (var context = new PetoEntities())
        {
            return context.ChargeableJobs.FirstOrDefault(x => x.ChargeableId == id);
        }
    }

    /// <summary>
    /// get record matching manager chargeable id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ManagerChargeable getManagerChargeForID(int id)
    {
        using (var context = new PetoEntities())
        {
            return context.ManagerChargeables.FirstOrDefault(x => x.ManagerChargeableId == id);
        }
    }

    /// <summary>
    /// get record matching noncharge id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public NonChargeable getNonChargeForID(int id)
    {
        using (var context = new PetoEntities())
        {
            return context.NonChargeables.FirstOrDefault(x => x.NonChargeId == id);
        }
    }

    /// <summary>
    /// get the record which matches manager noncharge id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ManagerNonChargeable getManagerNonChargeForID(int id)
    {
        using (var context = new PetoEntities())
        {
            return context.ManagerNonChargeables.FirstOrDefault(x => x.ManagerNonChargeableId == id);
        }
    }

    /// <summary>
    /// create a lab test
    /// </summary>
    /// <param name="day"></param>
    /// <param name="test"></param>
    /// <param name="timesheet"></param>
    /// <param name="manager"></param>
    public void createLabTest(string day, string density, string lab, int timesheet, bool manager, ref decimal totalLab)
    {
        using (var context = new PetoEntities())
        {
            try
            {
                if (manager)
                {
                    ManagerTest t = new ManagerTest();
                    t.Day = day;
                    if (density != string.Empty)
                    {
                        t.DensityTest = Convert.ToInt32(density);
                    }
                    if (lab != string.Empty)
                    {
                        t.LabTest = Convert.ToDecimal(lab);
                        totalLab += t.LabTest.Value;
                    }
                    t.TimeSheetId = timesheet;

                    if (lab != string.Empty || density != string.Empty)
                    {
                        context.ManagerTests.AddObject(t);
                        context.SaveChanges();
                    }
                }
                else
                {
                    EmpTest t = new EmpTest();
                    t.Day = day;
                    if (density != string.Empty)
                    {
                        t.DensityTest = Convert.ToInt32(density);
                    }
                    if (lab != string.Empty)
                    {
                        t.LabTest = Convert.ToDecimal(lab);
                        totalLab += t.LabTest.Value;
                    }
                    t.TimeSheetId = timesheet;

                    if (lab != string.Empty || density != string.Empty)
                    {
                        context.EmpTests.AddObject(t);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    /// <summary>
    /// update lab test
    /// </summary>
    /// <param name="day"></param>
    /// <param name="tests"></param>
    /// <param name="timesheet"></param>
    /// <param name="manager"></param>
    public bool updateLabTest(string day, string density, string lab, int timesheet, bool manager, ref decimal totalLab)
    {
        using (var context = new PetoEntities())
        {
            try
            {
                if (manager)
                {
                    ManagerTest t = context.ManagerTests.FirstOrDefault(x => x.TimeSheetId == timesheet && x.Day == day);
                    t.Day = day;
                    if (density != string.Empty)
                    {
                        t.DensityTest = Convert.ToInt32(density);
                    }
                    if (lab != string.Empty)
                    {
                        totalLab -= Convert.ToInt32(lab);
                        t.LabTest = Convert.ToInt32(lab);
                        totalLab += t.LabTest.Value;
                    }
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    EmpTest t = context.EmpTests.FirstOrDefault(x => x.TimeSheetId == timesheet && x.Day == day);
                    if (density != string.Empty)
                    {
                        t.DensityTest = Convert.ToInt32(density);
                    }
                    if (lab != string.Empty)
                    {
                        totalLab -= Convert.ToInt32(lab);
                        t.LabTest = Convert.ToInt32(lab);
                        totalLab += t.LabTest.Value;
                    }
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    /// <summary>
    /// delete lab test
    /// </summary>
    /// <param name="timesheet"></param>
    /// <param name="day"></param>
    /// <param name="manager"></param>
    public void deleteLabTest(int id, int time, bool manager)
    {
        using (var context = new PetoEntities())
        {
            TimeSheet ts = context.TimeSheets.FirstOrDefault(t => t.TimeSheetId == time);
            if (manager)
            {
                // subtract hours from total hours in timesheet
                ManagerTest t = context.ManagerTests.FirstOrDefault(x => x.TestId == id);
                if (t != null)
                {
                    if (t.LabTest > 0)
                    {
                        ts.TotalHours -= t.LabTest;
                    }
                    context.ManagerTests.DeleteObject(t);
                    context.SaveChanges();
                }
            }
            else
            {
                EmpTest t = context.EmpTests.FirstOrDefault(x => x.TestId == id);
                if (t != null)
                {
                    // subtract hours from total hours in timesheet
                    if (t.LabTest > 0)
                    {
                        ts.TotalHours -= t.LabTest;
                    }
                    context.EmpTests.DeleteObject(t);
                    context.SaveChanges();
                }
            }
        }
    }

    /// <summary>
    /// get manager lab tests for timesheet
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public List<ManagerTest> getManagerTestsForTimeSheet(int time)
    {
        using (var context = new PetoEntities())
        {
            return (from x in context.ManagerTests
                    where x.TimeSheetId == time
                    select x).ToList();
        }
    }

    /// <summary>
    /// get employee lab tests for timesheet
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public List<EmpTest> getEmpTestsForTimeSheet(int time)
    {
        using (var context = new PetoEntities())
        {
            return (from x in context.EmpTests
                    where x.TimeSheetId == time
                    select x).ToList();
        }
    }

    /// <summary>
    /// sort by SUNDAY, MONDAY, TUES, WEDS ETC. Inefficient n * 7. value of n isn't very high so i didn't code a more efficient sorting algorithm
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public List<Summary> sortDayOfWeek(List<Summary> s)
    {
        List<Summary> sorted = new List<Summary>();

        for (int i = 0; i < 7; i++)
        {
            foreach (Summary item in s)
            {
                if (i == 0 && item.Day == "Sunday")
                {
                    sorted.Add(item);
                }
                else if (i == 1 && item.Day == "Monday")
                {
                    sorted.Add(item);
                }
                else if (i == 2 && item.Day == "Tuesday")
                {
                    sorted.Add(item);
                }
                else if (i == 3 && item.Day == "Wednesday")
                {
                    sorted.Add(item);
                }
                else if (i == 4 && item.Day == "Thursday")
                {
                    sorted.Add(item);
                }
                else if (i == 5 && item.Day == "Friday")
                {
                    sorted.Add(item);
                }
                else if (i == 6 && item.Day == "Saturday")
                {
                    sorted.Add(item);
                }
            }
        }
        return sorted;
    }

    /// <summary>
    /// sort by day of week manager summary
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public List<ManagerSummary> sortDayOfWeek(List<ManagerSummary> s)
    {
        List<ManagerSummary> sorted = new List<ManagerSummary>();

        for (int i = 0; i < 7; i++)
        {
            foreach (ManagerSummary item in s)
            {
                if (i == 0 && item.Day == "Sunday")
                {
                    sorted.Add(item);
                }
                else if (i == 1 && item.Day == "Monday")
                {
                    sorted.Add(item);
                }
                else if (i == 2 && item.Day == "Tuesday")
                {
                    sorted.Add(item);
                }
                else if (i == 3 && item.Day == "Wednesday")
                {
                    sorted.Add(item);
                }
                else if (i == 4 && item.Day == "Thursday")
                {
                    sorted.Add(item);
                }
                else if (i == 5 && item.Day == "Friday")
                {
                    sorted.Add(item);
                }
                else if (i == 6 && item.Day == "Saturday")
                {
                    sorted.Add(item);
                }
            }
        }
        return sorted;
    }

    /// <summary>
    /// get Timesheets for status and for employee. The returned List is ordered by weekending (most recent)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="empId"></param>
    /// <returns></returns>
    public List<TimeSheet> getTimeSheetForStatusAndEmployee(int empId, string status)
    {
        using (var context = new PetoEntities())
        {
            return (from x in context.TimeSheets
                    orderby x.WeekEnding
                    where x.EmployeeId == empId && x.Status == status
                    select x).ToList();
        }
    }

    /// <summary>
    /// copy EmpTests to ManagerTests
    /// </summary>
    /// <param name="emps"></param>
    public void copyToManagerTest(EmpTest item)
    {
        using (var context = new PetoEntities())
        {
            try
            {
                ManagerTest m = new ManagerTest();
                m.TimeSheetId = item.TimeSheetId;
                if (item.LabTest.HasValue)
                {
                    m.LabTest = item.LabTest;
                }
                if (item.DensityTest.HasValue)
                {
                    m.DensityTest = item.DensityTest;
                }
                m.Day = item.Day;

                context.ManagerTests.AddObject(m);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    /// <summary>
    /// get branch names
    /// </summary>
    /// <returns></returns>
    public string[] getBranches()
    {
        using (var context = new PetoEntities())
        {
            return (from x in context.Branches
                    orderby x.name
                    select x.name).ToArray();
        }
    }

    /// <summary>
    /// get weekending dates from timesheets
    /// </summary>
    /// <returns></returns>
    public List<string> getDates()
    {
        using (var context = new PetoEntities())
        {
            return (from x in context.TimeSheets
                    orderby x.WeekEnding descending
                    select x.WeekEnding).Distinct().ToList();
        }
    }

    /// <summary>
    /// get users with pending timesheets for weekending date
    /// </summary>
    /// <param name="weekending"></param>
    /// <param name="sort"></param>
    /// <returns></returns>
    public List<User> getUsersWithPendingTimeSheet(string weekending, string sort)
    {
        List<User> result = new List<User>();
        using (var context = new PetoEntities())
        {
            switch (sort)
            {
                default:
                    try
                    {
                        // get timesheets that are in the process of being approved
                        List<int> complete = (from x in context.TimeSheets
                                              orderby x.DateModified descending
                                              where x.WeekEnding == weekending && x.Status == "Approved" || x.Status == "Updated - Manager" || x.Status == "Synchronized" || x.Status == "Completed"
                                              select x.EmployeeId).ToList();
                        // all users in the system
                        List<int> all = (from x in context.Users
                                         select x.UserId).ToList();

                        // loop all the users removing each complete userid
                        foreach (int item in all)
                        {
                            // if it does not contain a user who has completed timesheet display that user
                            if (!complete.Contains(item))
                            {
                                User u = context.Users.FirstOrDefault(x => x.UserId == item);
                                if (u != null)
                                {
                                    result.Add(u);
                                }
                            }
                        }
                        return result.OrderBy(x => x.username).ToList();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
            }
        }
    }
}