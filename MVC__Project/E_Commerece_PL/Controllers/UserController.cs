using Microsoft.AspNetCore.Mvc;

namespace E_Commerece_PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            List<ApplicationUser> users = new List<ApplicationUser>();

            if (string.IsNullOrEmpty(SearchValue))
                users.AddRange(_userManager.Users.ToList());
            else
            {
                var user = await _userManager.FindByNameAsync(SearchValue); // UserName
                users.Add(user);
            }
            var mappedUsers = _mapper.Map<List<ApplicationUser>, List<UserViewModel>>(users);
            return View(mappedUsers);
        }


    }
}
