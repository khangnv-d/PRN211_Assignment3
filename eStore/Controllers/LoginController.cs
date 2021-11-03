using DataAccess;
using DataAccess.Repository;
using eStore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eStore.Controllers
{
    public class LoginController : Controller
    {
		private readonly IMemberRepository memberRepository;

		private readonly DefaultAccount defaultAccount;

		public IConfiguration Configuration { get; set; }


		public LoginController()
		{
			//defaultAccount = Configuration.GetSection("AdminAccount").Get<DefaultAccount>();
			IConfiguration config = new ConfigurationBuilder()
									.SetBasePath(System.IO.Directory.GetCurrentDirectory())
									.AddJsonFile("appsettings.json", true, true)
									.Build();

			memberRepository = new MemberRepository();

			defaultAccount = config.GetSection("AdminAccount").Get<DefaultAccount>();
		}

		public IActionResult LoginPage ()
        {
			return View();
        }

		public IActionResult Login(LoginRequest model)
		{
			if (model.Email == defaultAccount.Email && model.Password == defaultAccount.Password)
			{
				HttpContext.Session.SetObject("Role", "Admin");
				TempData["Role"] = "Admin";
				return RedirectToAction("Index", "Home");
			}
			else
			{
				var member = memberRepository.GetMemberByEmail(model.Email);
				if (member == null)
				{
					TempData["Error"] = "Invalid!!!";
					return RedirectToAction("Login", "LoginPage");
				}

				if (model.Password != member.Password)
				{
					TempData["Error"] = "The email or password is incorrect";
					return RedirectToAction("Login", "LoginPage");
				}

				HttpContext.Session.SetObject("Role", "Member");
				TempData["Role"] = "Member";
				return RedirectToAction("Index", "Home");
			}
		}
		 

		public IActionResult Logout()
		{
			HttpContext.Session.SetObject("Role", null);
			HttpContext.Session.SetObject("Member", null);
			return View("LoginPage");
		}
	}
}
