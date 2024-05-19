






namespace E_Commerece_PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        /*        private readonly IEmployeeRepository _employeeRepository;
private readonly IDepartmentRepository _departmentRepository;*/
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string Searchvalue)
        {
            if (string.IsNullOrEmpty(Searchvalue))
            {
                var epms = _unitOfWork.EmployeeRepository.GetAll().Result;
                var empsview = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(epms);
                return View(empsview);
            }
            var emp = _unitOfWork.EmployeeRepository.GetEmployeesByName(Searchvalue);
            var emp_s = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(emp);
            return View(emp_s);

            //ViewData["message"] = "kjjj";
        }
        public IActionResult Create()
        {
            //ViewBag.Departments = _departmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                employee.ImageName = DocumentSeetings.UploadFile(employee.Image, "images");
                var emp = _mapper.Map<EmployeeViewModel, Employee>(employee);
                await _unitOfWork.EmployeeRepository.Add(emp);
                return RedirectToAction(nameof(Index));
            }
            await _unitOfWork.Complete();
            return View(employee);
        }
        public async Task<IActionResult> Details(int? id, string viewname = "Details")
        {
            if (id is null)
                return BadRequest();
            var d = await _unitOfWork.EmployeeRepository.Get(id.Value);
            if (d is null)
                return NotFound();
            var emp = _mapper.Map<Employee, EmployeeViewModel>(d);
            return View(viewname, emp);
        }

        public async Task< IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
            /*   if (id is null)
                   return BadRequest();
               var d = _departmentRepository.Get(id.Value);
               if (d is null)
                   return NotFound();
               return View(d);*/
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Edit([FromRoute] int id, EmployeeViewModel employee)
        {
            if (employee.Id != id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                var emp = _mapper.Map<EmployeeViewModel, Employee>(employee);
                try
                {
                    _unitOfWork.EmployeeRepository.Update(emp);
                  await  _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employee);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Delete([FromRoute] int id, EmployeeViewModel employee)
        {
            if (employee.Id != id)
                return BadRequest();
            try
            {
                var emp = _mapper.Map<EmployeeViewModel, Employee>(employee);
                _unitOfWork.EmployeeRepository.Delete(emp);
                int count = await _unitOfWork.Complete();
                if (count > 0)
                    DocumentSeetings.Deletefile(employee.ImageName, "images");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(employee);
        }
    }
}
