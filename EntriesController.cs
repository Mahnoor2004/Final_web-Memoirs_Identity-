using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EntriesController : Controller
    {

        private readonly IRepository<Memoirs> _repository;

        public EntriesController()
        {
            _repository = new GenericRepository<Memoirs>("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MM_DB;Integrated Security=True;");
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Memoirs t)
        {
            string? userEmail = User.Identity.Name;
            if (userEmail == null)
            {
                return RedirectToAction("Login", "Account");
            }
            _repository.Add(userEmail, t);
            return RedirectToAction("Index1");
        }

        public IActionResult Index1()
        {
            string ?userEmail = User.Identity.Name; 
            var memoirs = _repository.GetAll(userEmail).ToList();
            return View(memoirs);
        }

        public IActionResult Index2()
        {
            string userEmail = User.Identity.Name; 
            return View(_repository.GetAll(userEmail).ToList());

        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var task = _repository.GetById(id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Entries/Update
        [HttpPost]
        public IActionResult Update(Memoirs updatedTask)
        {
            string userEmail = User.Identity.Name;

            if (!ModelState.IsValid)
            {
                return View(updatedTask);
            }

            _repository.Update(updatedTask, userEmail);
            return RedirectToAction("Index1");
        }

        // POST: Entries/SelectMemoirToUpdate
        [HttpPost]
        public IActionResult SelectMemoirToUpdate(int selectedMemoirId)
        {
            return RedirectToAction("Update", new { id = selectedMemoirId });
        }

        public IActionResult Details(int id)
        {
            var memoir = _repository.GetById(id);
            if (memoir == null)
            {
                return NotFound();
            }
            return View(memoir);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var task = _repository.GetById(id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task); // Pass a single Memoirs object, not a list
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            string? userEmail = User.Identity.Name; // Get the current user's email address
            bool success = _repository.Delete(id, userEmail);
            if (success)
            {
                // Redirect to a success message view
                return RedirectToAction("Index1");
            }
            else
            {
                return View("TaskNotFound");
            }
        }

        [HttpPost]
        public IActionResult SelectMemoirToDelete(int selectedMemoirId)
        {
            return RedirectToAction("Delete", new { id = selectedMemoirId });
        }
        //public IActionResult SuccessMessage()
        //{
        //    return View();
        //}
        public IActionResult TaskNotFound()
        {
            return View("TaskNotFound");
        }
    }

}
