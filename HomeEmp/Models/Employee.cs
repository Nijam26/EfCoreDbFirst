using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HomeEmp.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string EmpName { get; set; }
        public string EmpAddress { get; set; }
        public int? Phone { get; set; }
        public int? Age { get; set; }
        public decimal? Salary { get; set; }
        public DateTime? DoB { get; set; }
    }
}
