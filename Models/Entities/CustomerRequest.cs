//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LawOfficeDesktopApp.Models.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class CustomerRequest
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string QuestionText { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
    
        public virtual Service Service { get; set; }
        public virtual User User { get; set; }
    }
}
