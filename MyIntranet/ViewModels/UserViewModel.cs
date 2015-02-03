using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyIntranet.ViewModels
{
    public class UserViewModel
    {
        public List<MyIntranet.Models.Department> Department{ get; set; }
        public List<MyIntranet.Models.User> User { get; set; }
    }
}