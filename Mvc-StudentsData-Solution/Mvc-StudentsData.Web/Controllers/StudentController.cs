using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Mvc_StudentsData.Domain.Entities;
using Mvc_StudentsData.Infrastructure.Context;

namespace Mvc_StudentsData.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;
        public StudentController(ApplicationDbContext db)
        {
            _db = db;
        }
        // Mostrar todos los registros en DataTables 2
        public IActionResult Index()
        {
            var students = _db.Students.ToList();
            return View(students);
        }
        // Mostrar vista del form Create
        public IActionResult Create()
        {
            return View();
        }

        // Guardar registro en DB
        [HttpPost]
        public IActionResult Store(Student student)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Add(student);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Create");
        }

        // Mostrar la vista de Edit
        public IActionResult Edit(int id)
        {
            Student? student = _db.Students.SingleOrDefault(s => s.Id == id);
            //Student? student = _db.Students.FirstOrDefault(s => s.Id == id);

            if(student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // Actualizar registro en DB
        [HttpPost]
        public IActionResult Update(Student student)
        {
            if (ModelState.IsValid && student.Id > 0)
            {
                _db.Students.Update(student);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Edit");
        }

        // Mostrar vista de Delete
        public IActionResult Delete(int id)
        {
            Student? student = _db.Students.SingleOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // Eliminar de la DB
        public IActionResult Destroy(Student student)
        {
            if(ModelState.IsValid && student.Id > 0)
            {
                _db.Students.Remove(student);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Delete");
        }
    }
}
