//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyIntranet.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Absence
    {
        public int Id { get; set; }
        public int UserHrId { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public decimal Days { get; set; }
        public int AbsenceReasonsId { get; set; }
    
        public virtual UserHr UserHr { get; set; }
        public virtual AbsenceReasons AbsenceReason { get; set; }
    }
}
