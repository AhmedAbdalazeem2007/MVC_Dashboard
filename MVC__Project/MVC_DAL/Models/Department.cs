﻿

namespace MVC_DAL.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime DateOfCreation { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();


    }
}
