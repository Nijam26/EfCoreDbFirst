using HomeEmp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HomeEmp.Controllers
{
    public class EmpController : Controller
    {
        private readonly MyContext _context;
        public EmpController(MyContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var emp = _context.Employee.FromSqlRaw($"sp_GetAllEmp").AsEnumerable().ToList();
            return View(emp);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee emp)
        {
            if (ModelState.IsValid)
            {
                string forCreate = $"sp_CreateEmp {emp.EmpName},{emp.EmpAddress},{emp.Phone},{emp.Age},{emp.Salary},'{emp.DoB}'";
                _context.Database.ExecuteSqlRaw(forCreate);
                return RedirectToAction("Index");
            }
            return View(emp);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var emp = _context.Employee.FromSqlRaw($"sp_GetEmpById {id}").AsEnumerable().FirstOrDefault();
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee emp)
        {
            if (ModelState.IsValid)
            {
                string forUpdate = $"sp_UpdateEmp '{emp.Id}','{emp.EmpName}','{emp.EmpAddress}','{emp.Phone}','{emp.Age}','{emp.Salary}','{emp.DoB}'";
                _context.Database.ExecuteSqlRaw(forUpdate);
                return RedirectToAction("Index");
            }
            return View(emp);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var emp = _context.Employee.FirstOrDefault(x => x.Id == id);
            return View(emp);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var emp = _context.Employee.FromSqlRaw($"sp_GetEmpById {id}").AsEnumerable().FirstOrDefault();
            return View(emp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Employee emp)
        {
            if (ModelState.IsValid)
            {
                _context.Database.ExecuteSqlRaw($"sp_DeleteEmp {emp.Id}");
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult EmployeeStats()
        {
            //count
            int empCount = _context.Employee.Count();
            ViewBag.Count = empCount;
            //average
            /* double avgAge = (double)_context.Employee.Average(a => a.Age);
            ViewBag.AvgAge = avgAge;*/

            double avgAge = (double)_context.Employee.Average(a => a.Age);
            string formattedAvgAge = avgAge.ToString("0.00");
            ViewBag.AvgAge = formattedAvgAge;

            //min
            int minAge = (int)_context.Employee.Min(a => a.Age);
            ViewBag.MinAge = minAge;
            //max
            int maxAge = (int)_context.Employee.Max(a => a.Age);
            ViewBag.MaxAge = maxAge;
            //sum
            decimal totalSalary = (decimal)_context.Employee.Sum(a => a.Salary);
            ViewBag.SumSalary = totalSalary;
            return View();
        }
    }
}
