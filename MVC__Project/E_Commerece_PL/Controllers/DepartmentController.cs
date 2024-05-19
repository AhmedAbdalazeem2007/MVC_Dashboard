





namespace E_Commerece_PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async  Task<IActionResult> Index()
        {
            var deps = await _unitOfWork.DepartmentRepository.GetAll();
            //   var depsview = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(deps);
            return View(deps);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
            //    var dep=_mapper.Map<Department>(department);
            await    _unitOfWork.DepartmentRepository.Add(department);
             await    _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }
        public async Task< IActionResult> Details(int? id, string viewname = "Details")
        {
            if (id is null)
                return BadRequest();
            var d = await _unitOfWork.DepartmentRepository.Get(id.Value);
            if (d is null)
                return NotFound();
            //    var dep = _mapper.Map<Department, DepartmentViewModel>(d);
            return View(viewname, d);
        }

        public async Task<IActionResult> Edit(int? id)
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
        public async Task<IActionResult> Edit([FromRoute] int id, Department department)
        {
            if (department.Id != id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                  //  var dep=_mapper.Map<DepartmentViewModel,Department>(department);
                    _unitOfWork.DepartmentRepository.Update(department);
                 await   _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute]int id,Department department)
        {
            try
            {
            //    var dep = _mapper.Map<DepartmentViewModel, Department>(department);
                _unitOfWork.DepartmentRepository.Delete(department);
               await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(department);
        }
    }
}
