using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Configuration;

namespace MyIntranet.Models
{
    [MetadataType(typeof(UserHrMetaData))]
    public partial class UserHr
    {
        private string CurrentYearText = (DateTime.Today.Year).ToString();
        private string PreviousYearText = (DateTime.Today.Year - 1).ToString();
        private string NextYearText = (DateTime.Today.Year + 1).ToString();

        private DateTime finYear1 = new DateTime(DateTime.Today.Year - 1, Convert.ToInt32(ConfigurationManager.AppSettings["HolidayYearStartMonth"]), 1);
        private DateTime finYear2 = new DateTime(DateTime.Today.Year, Convert.ToInt32(ConfigurationManager.AppSettings["HolidayYearStartMonth"]), 1);
        private DateTime finYear3 = new DateTime(DateTime.Today.Year + 1, Convert.ToInt32(ConfigurationManager.AppSettings["HolidayYearStartMonth"]), 1);

        public string ThisHolidayYear
        {
            get { return (CurrentYearText + "-" + NextYearText).Trim(); }
        }
        public string NextHolidayYear
        {
            get { return (PreviousYearText + "-" + CurrentYearText).Trim(); }
        }
        public decimal LeaveTakenThisYear
        {
            get
            {
                if (DateTime.Today.Month > 3) return (from l in this.LeaveRequests where l.StartDate >= this.finYear2 && l.EndDate < this.finYear3 select l.Days).Sum();
                else return (from l in this.LeaveRequests where l.StartDate >= this.finYear1 && l.EndDate < this.finYear2 select l.Days).Sum();
            }
        }
        public decimal LeaveTakenNextYear
        {
            get
            {
                if (DateTime.Today.Month > 3) return (from l in this.LeaveRequests where l.StartDate > this.finYear3 select l.Days).Sum();
                else return (from l in this.LeaveRequests where l.StartDate >= this.finYear2 && l.EndDate < this.finYear3 select l.Days).Sum();
            }
        }
        public decimal LeaveRemainingThisYear
        {
            get { return this.LeaveThisYear - this.LeaveTakenThisYear; }
        }
        public decimal LeaveRemainingNextYear
        {
            get { return this.LeaveNextYear - this.LeaveTakenNextYear; }
        }
        public decimal LeaveRemainingByDate(DateTime date)
        {
            if (DateTime.Today.Month > 3 && date.Month < 4) return LeaveTakenThisYear;
            if (DateTime.Today.Month < 4 && date.Month > 3) return LeaveTakenNextYear;
            else return 0;
        }
        public bool LeaveConflict(MyIntranetEntities db, DateTime start, DateTime end)
        {
            //Returns true if there's a leave request between the two given dates for each user listed as dependent
            bool conflict = false;

            foreach (UserHrDependencies u in this.UserHrDependencies)
            {
                int leave = (from l in db.LeaveRequests where l.StartDate <= end && l.EndDate >= start && l.UserHrId == u.UserHrId select l).Count();

                if (leave > 0) conflict = true;
            }

            return conflict;
        }
        public IEnumerable<Absence> AbsencesPastYear
        {
            get
            {
                return (from a in this.Absences where a.StartDate >= DateTime.Today.AddYears(-1) select a).AsEnumerable();
            }
        }
        public int CountAbsences(int period)
        {
            return (from a in this.Absences where a.StartDate >= DateTime.Today.AddMonths(period * -1) select a).Count();
        }
        public int SumAbsences(int period)
        {
            return (int)(from a in this.Absences where a.StartDate >= DateTime.Today.AddMonths(period * -1) select a.Days).Sum();
        }
    }

    public partial class UserHrMetaData
    {
        [DisplayName("Contracted Hours Per Week")]
        [Required]
        [Range(1,50,ErrorMessage = "Contract Hours must be between 1 and 50 hours")]
        [DisplayFormat(DataFormatString = "{0:f1}", ApplyFormatInEditMode = true)]
        public decimal ContractedHours { get; set; }

        [DisplayName("Salary")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:f1}", ApplyFormatInEditMode = true)]
        public decimal Salary { get; set; }

        [DisplayName("Shift Pattern")]
        [MinLength(5),MaxLength(5)]
        [Required]
        public string ShiftPattern { get; set; }
        
        [DisplayName("Leave This Year")]
        [Required]
        public short LeaveThisYear { get; set; }

        [DisplayName("Leave Next Year")]
        [Required]
        public short LeaveNextYear { get; set; }
        
        [DisplayName("Leave Supervisor")]
        [Required]
        public int LeaveSupervisor { get; set; }
    }
}
