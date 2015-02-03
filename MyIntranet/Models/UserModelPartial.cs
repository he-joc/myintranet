using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace MyIntranet.Models
{
    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {
        public int Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int age = today.Year - this.DateOfBirth.Year;
                if (DateOfBirth > today.AddYears(-age)) age--;

                return age;
            }
        }
        public string FullName
        {
            get
            {
                string name = this.FirstName + " " + this.LastName;
                return name;
            }
        }
        public int UserHrId
        {
            get { return this.UserHr.Id; }
        }
    }
    public class UserMetaData
    {
        [DisplayName("First Name")]
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 50 characters!")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last Name must be between 2 and 50 characters!")]
        public string LastName { get; set; }

        [DisplayName("Department")]
        [Required]
        public int DepartmentId { get; set; }

        [DisplayName("Gender")]
        [Required]
        public string Gender { get; set; }

        [DisplayName("Job Title")]
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Job Title must be between 2 and 50 characters!")]
        public string Title { get; set; }

        [DisplayName("Date of Birth")]
        [DataType(DataType.Date)]
        [Required]
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}",  ApplyFormatInEditMode = true)]
        public System.DateTime DateOfBirth { get; set; }

        [DisplayName("Username")]
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "User Name must be between 2 and 50 characters!")]
        public string UserName { get; set; }

        [Required]    
        public bool Active { get; set; }

        [DisplayName("First Day of Employment")]
        [DataType(DataType.Date)]
        [Required]
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}",  ApplyFormatInEditMode = true)]
        public System.DateTime DateStarted { get; set; }

        [DisplayName("Last Day of Employment")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}")]
        public Nullable<System.DateTime> DateLeft { get; set; }


    }
}