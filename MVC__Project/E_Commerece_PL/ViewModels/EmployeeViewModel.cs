
namespace E_Commerece_PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required!")]
        [MaxLength(50, ErrorMessage = "Max Length of Name is 50 Chars")]
        [MinLength(5, ErrorMessage = "Min Length of Name is 5 Chars")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Range(20, 60)]
        public int Age { get; set; }
        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$"
            , ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public Decimal Salary { get; set; }
        public bool? IsActive { get; set; } = false;
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime HireDate { get; set; }
        public string ImageName { get; set; }
        public IFormFile Image { get; set; } 
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
      //  public IEnumerable<SelectListItem> selectListItems = Enumerable.Empty<SelectListItem>();
     

    }
}
