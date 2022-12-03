using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCAPP.Models;
using MVCAPP.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace MVCAPP.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(ILogger<HomeController> logger, IEmployeeRepository employeeRepository
            ,IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
            _hostingEnvironment = hostingEnvironment;
        }


        [AllowAnonymous]/*to allow access index page without authotization*/
        public IActionResult Index()
        {
            var model = _employeeRepository.AllEmployee();
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound",id);
            }
            return View(employee);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateViewModel model)
        {
            //check validations
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);


                Employee employee = new Employee()
                {

                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };
                
                Employee e= _employeeRepository.AddEmployee(employee);
                return RedirectToAction("Details", new { id = e.Id });
            }
            return View();
        }

        [HttpGet]
        public IActionResult Update(int id) 
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            UpdateViewModel model = new UpdateViewModel()
            {
                id = id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Update(UpdateViewModel model)
        {
            //check validations
            if (ModelState.IsValid)
            {

                Employee employee = _employeeRepository.GetEmployee(model.id);

                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Imgs", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    employee.PhotoPath = ProcessUploadedFile(model);

                }
                else { employee.PhotoPath = model.ExistingPhotoPath; }

                Employee e = _employeeRepository.Update(employee);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            Employee emp= _employeeRepository.GetEmployee(id);
            if (emp.PhotoPath != null)
            {
                string imgePath = Path.Combine(_hostingEnvironment.WebRootPath, "Imgs", emp.PhotoPath);
                System.IO.File.Delete(imgePath);
            }

            _employeeRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy() 
        {
            return View();
        }



        private string ProcessUploadedFile(CreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string rootpath = Path.Combine(_hostingEnvironment.WebRootPath, "Imgs");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(rootpath, uniqueFileName);
                using (var file = new FileStream(filePath, FileMode.Create)) 
                { model.Photo.CopyTo(file); }
                    
            }
            return uniqueFileName;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}