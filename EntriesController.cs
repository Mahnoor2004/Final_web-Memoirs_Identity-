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
        //public IActionResult Index1()
        //{
        //    string userEmail = User.Identity.Name; // Get the current user's email address
        //    return View(_repository.GetAll(userEmail).ToList());
        //}
        public IActionResult Index1()
{
    string userEmail = User.Identity.Name; // Get the current user's email address
    var memoirs = _repository.GetAll(userEmail).ToList();
    
    if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
    {
        return PartialView("_SearchResultsPartial", memoirs);
    }

    return View(memoirs);
}

        public IActionResult Index2()
        {
            string userEmail = User.Identity.Name; // Get the current user's email address
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
    }


    //[HttpGet]
    //public IActionResult Update(int id)
    //{
    //    var task = _repository.GetById(id);

    //    if (task == null)
    //    {
    //        return NotFound(); 
    //    }


    //    return View(task);
    //}

    //[HttpPost]
    //public IActionResult Update(Memoirs updatedTask, string userEmail)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return View(updatedTask);
    //    }
    //    _repository.Update(updatedTask, userEmail);

    //    return RedirectToAction("Index1");
    //}




    //public IActionResult Details(int id)
    //{
    //    var memoir = _repository.Get(id);
    //    if (memoir == null)
    //    {
    //        return NotFound();
    //    }
    //    return View(memoir);
    //}
    //        public IActionResult Edit(int id)
    //        {
    //            var memoir = _repository.Get(id);
    //            if (memoir == null)
    //            {
    //                return NotFound();
    //            }
    //            return View(memoir);
    //        }

    //        [HttpPost]
    //        [ValidateAntiForgeryToken]
    //        public IActionResult Edit(int id, Memoirs memoir)
    //        {
    //            if (id != memoir.Id)
    //            {
    //                return NotFound();
    //            }

    //            if (ModelState.IsValid)
    //            {
    //                _repository.Update(memoir);
    //                return RedirectToAction(nameof(Index));
    //            }
    //            return View(memoir);
    //        }

    //        public IActionResult Delete(int id)
    //        {
    //            var memoir = _repository.Get(id);
    //            if (memoir == null)
    //            {
    //                return NotFound();
    //            }
    //            return View(memoir);
    //        }

    //        [HttpPost, ActionName("Delete")]
    //        [ValidateAntiForgeryToken]
    //        public IActionResult DeleteConfirmed(int id)
    //        {
    //            _repository.Delete(id);
    //            return RedirectToAction(nameof(Index));
    //        }
    //    }
    //}
}

