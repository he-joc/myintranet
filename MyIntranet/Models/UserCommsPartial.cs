using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace MyIntranet.Models
{
    [MetadataType(typeof(UserCommsMetaData))]
    public partial class UserComms
    {

    }

    public partial class UserCommsMetaData
    {
        [DisplayName("Internal Extension")]
        public string Extension { get; set; }
        
        [DisplayName("Direct Dial")]
        [MinLength(6,ErrorMessage="Not enough digits in direct dial"),MaxLength(11,ErrorMessage="Too many digits in direct dial")]
        public string DDI { get; set; }

        [DisplayName("Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
    }
}