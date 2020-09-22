using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Witter.Data.Common.Repositories;
using Witter.Data.Models;
using Witter.Services.Contracts;
using Witter.Web.ViewModels.Weets;

namespace Witter.Web.Controllers
{
    public class WeetsController : BaseController
    {
        private readonly IWeetsService weetsService;

        private readonly IRepository<Weet> repository;

        public WeetsController(IWeetsService service, IRepository<Weet> repository)
        {
            this.weetsService = service;
            this.repository = repository;
        }

        public IActionResult Index()
        {
            var weets = this.weetsService.GetAll<FeedWeetViewModel>();

            return this.View(weets);
        }
    }
}
