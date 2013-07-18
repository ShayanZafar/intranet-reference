using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Summary
/// </summary>
public class Summary
{
	public Summary(){}

    public int TimeSheetId { get; set; }
    public int NonChargeableId { get; set; }
    public int ChargeableId { get; set; }

    public decimal Hours { get; set; }
    public int Distance { get; set; }
    public int TruckDistance { get; set; }
    public decimal Accom { get; set; }
    public decimal Misc { get; set; }
    public string TypeExpense { get; set; }
    public string TypeHours { get; set; }
    public string Classification { get; set; }
    public string Activity { get; set; }
    public string Remarks { get; set; }

    public int ProjectId { get; set; }
    public string Day { get; set; }
    public string ProjectNum { get; set; }
    public string ProjectName { get; set; }

    public int TestId { get; set; }
    public decimal labTest { get; set; }
    public int DensityTest { get; set; }
}