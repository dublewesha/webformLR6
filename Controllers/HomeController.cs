using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using webformLR6.db;
using webformLR6.Models;

namespace webformLR6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _context= context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Api(string? ViewBag)
        {
            var items = _context.TodoItems.ToList();
            return View(items);
        }
        [HttpPost]
        public IActionResult Save(TodoItem item)
        {
            _context.TodoItems.Update(item);
            _context.SaveChanges();
            return RedirectToAction("Api");
        }
        public IActionResult Edit(long id)
        {
            var item = _context.TodoItems.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                return View(item);
            }
            else
            {
                ViewBag.Danger = "Ошибка";
                return View();
            }
        }
        public IActionResult Remove(long? id)
        {
            var item = _context.TodoItems.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _context.TodoItems.Remove(item);
                _context.SaveChanges();
                ViewBag.Suc = "Успешно удален";
                return RedirectToAction("Api");
            }
            else
            {
                ViewBag.Danger = "Ошибка";
                return RedirectToAction("Api");
            }
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