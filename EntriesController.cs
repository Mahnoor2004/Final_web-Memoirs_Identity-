//using Microsoft.AspNetCore.Mvc;
//using WebApplication1.Models;

//namespace WebApplication1.Controllers
//{
//    public class EntriesController : Controller
//    {
//        private readonly IRepository<Tasks> _repository;

//        public EntriesController(IRepository<Tasks> repository)
//        {
//            _repository = repository;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }

//        public IActionResult Index1()
//        {
//            var tasks = _repository.GetAll();
//            return View(tasks);
//        }

//        public IActionResult Details(int id)
//        {
//            var task = _repository.Get(id);
//            if (task == null)
//            {
//                return NotFound();
//            }
//            return View(task);
//        }

//        public IActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create(Tasks task)
//        {
//            if (ModelState.IsValid)
//            {
//                _repository.Add(task);
//                return RedirectToAction(nameof(Index));
//            }
//            return View(task);
//        }

//        public IActionResult Edit(int id)
//        {
//            var task = _repository.Get(id);
//            if (task == null)
//            {
//                return NotFound();
//            }
//            return View(task);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Edit(int id, Tasks task)
//        {
//            if (id != task.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                _repository.Update(task);
//                return RedirectToAction(nameof(Index));
//            }
//            return View(task);
//        }

//        public IActionResult Delete(int id)
//        {
//            var task = _repository.Get(id);
//            if (task == null)
//            {
//                return NotFound();
//            }
//            return View(task);
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
        //public EntriesController(IRepository<Memoirs> repository)
        //{
        //    _repository = repository;
        //}

        //public IActionResult Index1()
        //{
        //    var memoirs = _repository.GetAll();
        //    return View(memoirs);
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
            string userEmail = User.Identity.Name; // Get the current user's email address
            return View(_repository.GetAll(userEmail).ToList());
        }
        [HttpGet]
        public IActionResult UpdateTask()
        {
            string c = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TaskManager;Integrated Security=True;";
            GenericRepository<Quotes> repo = new GenericRepository<Quotes>(c);
            Quotes randomTask = repo.GetAny();
            return View(randomTask);
        }

        [HttpPost]
        public IActionResult UpdateTask(Tasks updatedTask)
        {

            string c = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TaskManager;Integrated Security=True;";
            GenericRepository<Tasks> repo = new GenericRepository<Tasks>(c);
            string userEmail = User.Identity.Name; // Get the current user's email address

            // Perform the update operation in the repository
            bool success = repo.Updatetask(updatedTask, userEmail);
            if (success)
            {
                // Redirect to a success message view
                return RedirectToAction("SuccessMessage");
            }
            else
            {
                return View("TaskNotFound");
            }
        }



        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public IActionResult Create(Memoirs memoir)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                _repository.Add(memoir);
        //                return RedirectToAction(nameof(Index));
        //            }
        //            return View(memoir);
        //        }

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
}
