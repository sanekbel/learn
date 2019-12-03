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
    public class UserController : Controller
    {
        private readonly CrudExampleContext _context;
        public UserController(CrudExampleContext context)
        {
            _context = context;
        }

        public ActionResult ListUsers()
        {
            var result = _context.User.ToList();
            return View(result);
        }

        public ActionResult Details(int id)
        {
            var result = _context.User.Where(m => m.Id == id).FirstOrDefault();
            return View(result);
        }

        public ActionResult Create()
        {
            return View();
        }        

        public ActionResult DeleteUser(int id)
        {
            var user = _context.User.Where(m => m.Id == id).Single();
            _context.Remove(user);
            _context.SaveChanges();
            return RedirectToAction(nameof(ListUsers));
        }

        public ActionResult CreateRandom()
        {
            string RandomString()
            {
                string path = Path.GetRandomFileName();
                path = path.Replace(".", "");
                return path;
            }
            var user = new User
            {
                Name = RandomString()
            };

            _context.User.Add(user);
            _context.SaveChanges();
            return RedirectToAction(nameof(ListUsers));
        }

        [HttpPost]
        public ActionResult Create(IFormCollection data)
        {
            try
            {
                var user = new User
                {
                    Name = data["Name"]
                };

                _context.User.Add(user);
                _context.SaveChanges();

                return RedirectToAction(nameof(ListUsers));
            }

            catch
            {
                return View();
            }
        }
    }
}