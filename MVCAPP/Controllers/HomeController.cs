using EmployeeManagement.ViewModels;
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
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController( IEmployeeRepository employeeRepository
            ,IHostingEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            _hostingEnvironment = hostingEnvironment;
        }


        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Employee> emps = _employeeRepository.AllEmployee();

            IndexViewModel model = new IndexViewModel { Employees = emps, SerachTerm = "" };
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Index(IndexViewModel model)
        {
            var Employees = _employeeRepository.Search(model.SerachTerm);
            model.Employees = Employees;
            return View(model);
        }

        [Authorize]
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
        [Authorize(Policy = "CreateRolePolicy")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Policy = "CreateRolePolicy")]
        public IActionResult Create(CreateViewModel model)
        {
            //check validations
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                var found = _employeeRepository.FindEmployee(model.Email);
                if (found == false)
                {
                    Employee employee = new Employee()
                    {

                        Name = model.Name,
                        Email = model.Email,
                        Department = model.Department,
                        PhotoPath = uniqueFileName,
                        Address = model.Address,
                        Phone = model.Phone,
                        Salary = model.Salary
                    };

                    Employee e = _employeeRepository.AddEmployee(employee);
                    return RedirectToAction("Details", new { id = e.Id });
                }
                else 
                {
                    ViewBag.ErrorMessage = $"The user with Email : {model.Email} is already exist.";
                    return View("RepeatedEmployee");
                }
  
            }
            return View();
        }

        [HttpGet]
        [Authorize(Policy = "EditRolePolicy")]
        public IActionResult Update(int id) 
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            UpdateViewModel model = new UpdateViewModel()
            {
                id = id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath,
                Phone=employee.Phone,
                Address =employee.Address,
                Salary = employee.Salary
            };
            return View(model);
        }
        [HttpPost]
        [Authorize(Policy = "EditRolePolicy")]
        public IActionResult Update(UpdateViewModel model)
        {
            //check validations
            if (ModelState.IsValid)
            {

                Employee employee = _employeeRepository.GetEmployee(model.id);

                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                employee.Phone = model.Phone;
                employee.Address = model.Address;
                employee.Salary = model.Salary;
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
        
        [Authorize(Policy = "DeleteRolePolicy")]
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

        public IActionResult Summary() 
        {
            var SummaryData = new Dictionary<string, int>(); 
            
            foreach (var item in Enum.GetValues(typeof(Dept)))
            {
                var count = _employeeRepository.CountDept((Dept)item);
                SummaryData.Add(item.ToString(), count);
            }
            ViewBag.Summary = SummaryData;
            return View();
        }

        //For Getting photo path
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