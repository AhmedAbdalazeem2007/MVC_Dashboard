

namespace MVC_DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
      //  public bool? IsDeleted { get; set; } = false;
        public DateTime HireDate { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
         public string ImageName {  get; set; }
       
    }
}
