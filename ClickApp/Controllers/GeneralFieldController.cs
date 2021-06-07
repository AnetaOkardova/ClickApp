using ClickApp.Mappings;
using ClickApp.Models;
using ClickApp.Services.Interfaces;
using ClickApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.Controllers
{
    public class GeneralFieldController : Controller
    {
        private readonly IGeneralFieldsService _generalFieldsService;
        private readonly UserManager<ApplicationUser> _userManager;

        public GeneralFieldController(IGeneralFieldsService generalFieldsService, UserManager<ApplicationUser> userManager)
        {
            _generalFieldsService = generalFieldsService;
            _userManager = userManager;
        }
        public IActionResult Overview(string successMessage, string errorMessage)
        {
            if (successMessage != null)
            {
                ViewBag.SuccessMessage = successMessage;
            }
            if (errorMessage != null)
            {
                ViewBag.ErrorMessage = errorMessage;
            }
            var generalFields = _generalFieldsService.GetAll();

            if(generalFields.Count == 0 || generalFields == null)
            {
                return View(new { ErrorMessage = "There are no General fields in the DB at this point" });
            }

            var generalFieldsForView = generalFields.Select(x => x.ToGeneralFieldViewModel()).ToList();
            return View(generalFieldsForView);
        }
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateGeneralFieldViewModel createGeneralFieldViewModel)
        {
            if (ModelState.IsValid)
            {
                var generalField = createGeneralFieldViewModel.ToModel();
               var response = _generalFieldsService.Create(generalField);
                if (response.IsSuccessful)
                {
                return RedirectToAction("Overview", new { SuccessMessage = $"The general field {createGeneralFieldViewModel.Name} has been successfully created." });
                }
                else
                {
                    return RedirectToAction("Overview", new { ErrorMessage = response.Message });
                }
            }
            else
            {
                return View(createGeneralFieldViewModel);
            }
        }
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var response = _generalFieldsService.Delete(id);
            if (response.IsSuccessful)
            {
                return RedirectToAction("Overview", new { SuccessMessage = response.Message });
            }
            else
            {
                return RedirectToAction("Overview", new { ErrorMessage = response.Message });
            }
        }
        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            var generalField = _generalFieldsService.GetById(id);
            if (generalField == null)
            {
                return RedirectToAction("Overview", new { ErrorMessage = $"There are no general fields with ID {id}." });

            }
            var generalFieldForView = generalField.ToGeneralFieldViewModel();

            return View(generalFieldForView);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(GeneralFieldViewModel generalFieldViewModel)
        {
            if (ModelState.IsValid)
            {
                var generalField = generalFieldViewModel.ToModel();
                var response = _generalFieldsService.Update(generalField);
                if (response.IsSuccessful)
                {
                    return RedirectToAction("Overview", new { SuccessMessage = $"The general field {generalFieldViewModel.Name} has been successfully created." });
                }
                else
                {
                    return RedirectToAction("Overview", new { ErrorMessage = response.Message });
                }
            }
            else
            {
                return View(generalFieldViewModel);
            }
        }
    }
}
