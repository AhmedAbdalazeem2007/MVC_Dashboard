


namespace MVC_DAL.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FName {  get; set; }
        public string LName {  get; set; }
        public bool IaAgrre { get; set; } = false;

    }
}
