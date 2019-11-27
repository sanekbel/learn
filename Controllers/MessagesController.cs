using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using learn.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace learn.Controllers
{
    public class MessagesController : Controller
    {
        private readonly CrudExampleContext _context;
        public MessagesController(CrudExampleContext context)
        {
            _context = context;
        }

        public ActionResult ListMessages()
        {
            var result = _context.Messages.ToList();
            return View(result);
        }

        public ActionResult Details(int id)
        {
            var result = _context.Messages.Where(m => m.Id == id).FirstOrDefault();
            return View(result);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult SoftDeleteMessage(int id)
        {
            var message = _context.Messages.Where(m => m.Id == id).Single();
            if (message.IsDeleted == false)
            {
                message.IsDeleted = true;
                _context.Update(message);
                _context.SaveChanges();
            }
            else
            {
                message.IsDeleted = false;
                _context.Update(message);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(ListMessages));
        }

        public ActionResult HardDeleteMessage(int id)
        {
            var message = _context.Messages.Where(m => m.Id == id).Single();
            _context.Remove(message);
            _context.SaveChanges();
            return RedirectToAction(nameof(ListMessages));
        }

        public ActionResult CreateRandom()
        {
            string RandomString()
            {
                string path = Path.GetRandomFileName();
                path = path.Replace(".", "");
                return path;
            }
            var message = new Messages
            {
                Date = DateTime.Now,
                Message = RandomString()
            };

            _context.Messages.Add(message);
            _context.SaveChanges();
            return RedirectToAction(nameof(ListMessages));
        }

        [HttpPost]
        public ActionResult Create(IFormCollection data)
        {
            try
            {
                var message = new Messages
                {
                    Date = DateTime.Now,
                    Message = data["Message"]
                };

                _context.Messages.Add(message);
                _context.SaveChanges();

                return RedirectToAction(nameof(ListMessages));
            }

            catch 
            {
                return View();
            }
        }
    }
}