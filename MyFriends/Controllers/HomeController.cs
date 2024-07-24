using Microsoft.AspNetCore.Mvc;
using MyFriends.Models;
using MyFriends.DAL;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace MyFriends.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //פונקציה להצגת פרטי חבר כולל תמונות
        public IActionResult Details(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            Friend friend = //Data.Get.Friends.FirstOrDefault(f => f.ID == id);
             Data.GetData.Friends.Include(f => f.Images).ToList().Find(f => f.ID == id);
            if (friend == null) return RedirectToAction("Index");
            return View(friend);
        }
        //פונקציה להוספת תמונה לחבר קיים במערכת
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddImage(Friend friend)
        {
            Data.GetData.Friends.FirstOrDefault(f => f.ID == friend.ID).SaveImage(friend.Images.First().MyImage);
            Data.GetData.SaveChanges();
            return RedirectToAction("Details", new { id = friend.ID });
        }

        //פונקציה לעריכת פרטי חבר
        public IActionResult Edit(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            Friend friend = //Data.Get.Friends.FirstOrDefault(f => f.ID == id);
             Data.GetData.Friends.ToList().Find(f => f.ID == id);
            if (friend == null) return RedirectToAction("Index");
            return View(friend);
        }

        [HttpPost]
        public IActionResult Edit(Friend friend)
        {
            if (friend == null) return RedirectToAction(nameof(Index));
            Friend friendDB = Data.GetData.Friends.FirstOrDefault(f => f.ID == friend.ID);
            if (friendDB == null) return RedirectToAction(nameof(Index));
            //שלב עריכת פרטי החבר
            friendDB.FirstName = friend.FirstName;
            friendDB.LastName = friend.LastName;
            friendDB.EmailAddress = friend.EmailAddress;
            friendDB.Phone = friend.Phone;
            //שמירה
            Data.GetData.SaveChanges();
            return RedirectToAction(nameof(Index));
        }



        //פונקציה להוספת חבר
        public IActionResult Create()
        {
            return View(new Friend());
        }
        //פונקציה המקבלת בחזרה את פרטי החבר החדש

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Friend friend)
        {
            Data.GetData.Friends.Add(friend);
            Data.GetData.SaveChanges();
            return RedirectToAction("Index");
        }

        //הצגת כל החברים 
        public IActionResult Index()
        {
            List<Friend> friends = Data.GetData.Friends.ToList();
            return View(friends);
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