using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RegisterForm.DatabaseContext;
using RegisterForm.Models;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace RegisterForm.Controllers
{
    public class LoginController : Controller
    {
        private readonly DataContext _context;

        public LoginController(DataContext context)
        {
            _context = context;
        }

        // GET: Users
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Id,Email,Password,ConfirmPassword")] User userInfo)
        {
            var user = await FindUser(userInfo.Email);
            if (user == null)
            {
                ViewBag.message = "Email is wrong!";
                return View("Index");
            }

            if (user.Password != sha256(userInfo.Password))
            {
                ViewBag.message = "Password is wrong!";
                return View("Index");
            }

            HttpContext.Session.SetString("_user", sha256(userInfo + user.Password));

            return Redirect("/");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Password,ConfirmPassword")] User user)
        {
            if (await UserExists(user.Email))
            {
                ViewBag.message = "Email existed!";
                return View("Register");
            }
            user.Password = sha256(user.Password);
            _context.Add(user);
            await _context.SaveChangesAsync();

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("vietanhtran2069@gmail.com", "ylkv rnol cwpq hhsi"),
                EnableSsl = true,
            };

            smtpClient.Send("vietanhtran2069@gmail.com", user.Email, "Confirm Register", "Thank you for registration");

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }

        private async Task<bool> UserExists(string Email)
        {
            return await FindUser(Email) != null;
        }

        private async Task<User?> FindUser(string Email)
        {
            return await _context.User?.FirstOrDefaultAsync(e => e.Email == Email);
        }
        private string sha256(string randomString)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }
    }
}
