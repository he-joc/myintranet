using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace MyIntranet.Models
{
    [MetadataType(typeof(LeaveRequestMetaData))]
    public partial class LeaveRequest
    {

    }

     public partial class LeaveRequestMetaData
     {
         [DisplayName("First Day of Leave")]
         [Required]
         [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}", ApplyFormatInEditMode = true)]
         [DataType(DataType.Date)]
         public System.DateTime StartDate { get; set; }

         [DisplayName("Last Day of Leave")]
         [Required]
         [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}", ApplyFormatInEditMode = true)]
         [DataType(DataType.Date)]
         public System.DateTime EndDate { get; set; }

         [DisplayName("Half Day")]
         public string StartHalfDay { get; set; }

         [DisplayName("Half Day")]
         public string EndHalfDay { get; set; }

         [Required]
         [DataType(DataType.Text)]
         public int UserHrId { get; set; }

     }
}