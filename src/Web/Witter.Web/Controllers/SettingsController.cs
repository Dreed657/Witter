﻿namespace Witter.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Witter.Data.Common.Repositories;
    using Witter.Data.Models;
    using Witter.Services.Data;
    using Witter.Services.Messaging;
    using Witter.Web.ViewModels.Settings;

    public class SettingsController : BaseController
    {
        private readonly ISettingsService settingsService;

        private readonly IDeletableEntityRepository<Setting> repository;

        private readonly IEmailSender emailSender;

        public SettingsController(ISettingsService settingsService, IDeletableEntityRepository<Setting> repository, IEmailSender emailSender)
        {
            this.settingsService = settingsService;
            this.repository = repository;
            this.emailSender = emailSender;
        }

        public IActionResult Index()
        {
            var settings = this.settingsService.GetAll<SettingViewModel>();
            var model = new SettingsListViewModel { Settings = settings };

            return this.View(model);
        }

        public async Task<IActionResult> InsertSetting()
        {
            var random = new Random();
            var setting = new Setting { Name = $"Name_{random.Next()}", Value = $"Value_{random.Next()}" };

            await this.repository.AddAsync(setting);
            await this.repository.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
