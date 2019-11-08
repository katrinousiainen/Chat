using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YACA.Models;

namespace Miniprojekti.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Read()
        {
            AcademyChatContext acc = new AcademyChatContext();
            var m = (from b in acc.Message
                     select b).ToList().First();
            return View(m);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Message m)
        {
            AcademyChatContext acc = new AcademyChatContext();
            acc.Message.Add(m);
            acc.SaveChanges();

            return View();


        }
    }
}