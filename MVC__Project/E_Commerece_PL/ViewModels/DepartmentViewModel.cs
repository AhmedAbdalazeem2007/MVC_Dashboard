
namespace E_Commerece_PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "It is Required")]
        [MaxLength(200, ErrorMessage = "max length is 200")]
        [MinLength(10, ErrorMessage = "minimmum is 10")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required(ErrorMessage = "It is Required")]
        [MaxLength(200, ErrorMessage = "max length is 200")]
        [MinLength(10, ErrorMessage = "minimmum is 10")]
        [DataType(DataType.Text)]
        public string Code { get; set; }
        [Required(ErrorMessage = "It is Required")]
        public DateTime CreatedDate { get; set; }
        public DateTime HirDate { get; set; } 

    }
}
