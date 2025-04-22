using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTrekApp.Data;
using TaskTrekApp.Models;

namespace TaskTrekApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AppDbContext _db;

        public HomeController(ILogger<HomeController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);
            var cards = _db.Tasks.ToList(); 
            var columns = _db.Columns.ToList(); 
            var tags = _db.Tags.ToList();
            var homeVM = new HomeViewModel { Cards = cards, Columns = columns, Tags = tags, User = user };

            return View(homeVM);
        }

        [HttpPost]
        public JsonResult Create([FromBody] TaskCard taskData)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (taskData == null || string.IsNullOrEmpty(taskData.Title) || taskData.TagId <= 0)
            {
                return Json(new { success = false, message = "Invalid task data" });
            }



            var task = new TaskCard
            {
                Title = taskData.Title,
                TagId = taskData.TagId,
                UserId = userId,
                ColumnId = 1,
                Deadline = taskData.Deadline.Date
            };

            _db.Tasks.Add(task);
            _db.SaveChanges();

            return Json(new
            {
                success = true,
                message = "Task successfully created",
                taskId = task.TaskId,
                deadline = task.Deadline.ToString("dd.MM.yyyy")  // Format the date
            });
        }


        [HttpPost]
        public JsonResult DeleteTask([FromBody] TaskDeleteModel data)
        {
            if (data == null || data.TaskId <= 0)
            {
                return Json(new { success = false, message = "Invalid task ID" });
            }

            var task = _db.Tasks.FirstOrDefault(t => t.TaskId == data.TaskId);
            if (task == null)
            {
                return Json(new { success = false, message = "Task not found" });
            }

            _db.Tasks.Remove(task);
            _db.SaveChanges();

            return Json(new { success = true, message = "Task deleted successfully" });
        }

        public class TaskDeleteModel
        {
            public int TaskId { get; set; }
        }


        [HttpPost]
        public JsonResult UpdateTaskColumn([FromBody] TaskUpdateModel data)
        {
            if (data == null || data.TaskId <= 0 || data.ColumnId <= 0)
            {
                return Json(new { success = false, message = "Invalid data" });
            }

            var task = _db.Tasks.FirstOrDefault(t => t.TaskId == data.TaskId);
            if (task == null)
            {
                return Json(new { success = false, message = "Task not found" });
            }

            task.ColumnId = data.ColumnId;
            _db.SaveChanges();

            return Json(new { success = true, message = "Task moved successfully!" });
        }

        public class TaskUpdateModel
        {
            public int TaskId { get; set; }
            public int ColumnId { get; set; }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
