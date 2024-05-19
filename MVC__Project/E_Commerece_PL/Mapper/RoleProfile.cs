namespace E_Commerece_PL.Mapper
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole, RoleViewModel>()
                .ForMember(d => d.RoleName, O => O.MapFrom(s => s.Name)).ReverseMap();
        }
    }
}
