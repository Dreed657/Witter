using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Witter.Data.Models;
using Witter.Services.Data.Contracts;

namespace Witter.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly IUserService usersService;

        public ProfileController(IUserService service, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.usersService = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var profileData = this.usersService.GetProfileByUser(await this.userManager.GetUserAsync(this.User));

            this.ViewBag.data = profileData;

            return this.View();
        }
    }
}
