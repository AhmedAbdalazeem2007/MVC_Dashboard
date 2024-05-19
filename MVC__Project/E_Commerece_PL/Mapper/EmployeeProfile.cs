namespace E_Commerece_PL.Mapper
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>()
                .ReverseMap();
        }
    }
}
