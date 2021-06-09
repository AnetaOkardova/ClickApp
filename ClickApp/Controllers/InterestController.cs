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
    public class InterestController : Controller
    {
        private readonly IInterestsService _interestsService;
        private readonly IGeneralFieldsService _generalFieldsService;
        private readonly UserManager<ApplicationUser> _userManager;

        public InterestController(IInterestsService interestsService, IGeneralFieldsService generalFieldsService, UserManager<ApplicationUser> userManager)
        {
            _interestsService = interestsService;
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
            var interests = _interestsService.GetAll();

            if (interests.Count == 0 || interests == null)
            {
                ViewBag.ErrorMessage = "There are no interests in the DB at this point";
                return View();
            }
            else
            {
                var interestsForView = interests.Select(x => x.ToInterestViewModel()).ToList();
                return View(interestsForView);
            }


        }
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            var generalFields = _generalFieldsService.GetAll();
            var generalFieldsForView = generalFields.Select(x => x.ToGeneralFieldViewModel()).ToList();
            var interestView = new CreateInterestViewModel();
            interestView.GeneralFields = generalFieldsForView;
            return View(interestView);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateInterestViewModel createInterestViewModel)
        {
            if (ModelState.IsValid)
            {
                var interest = createInterestViewModel.ToModel();
                var response = _interestsService.Create(interest);
                if (response.IsSuccessful)
                {
                    return RedirectToAction("Overview", new { SuccessMessage = $"The interest {interest.Name} has been successfully created." });
                }
                else
                {
                    return RedirectToAction("Overview", new { ErrorMessage = response.Message });
                }
            }
            else
            {
                return View(createInterestViewModel);
            }
        }
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var response = _interestsService.Delete(id);
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
            var interest = _interestsService.GetById(id);
            if (interest == null)
            {
                return RedirectToAction("Overview", new { ErrorMessage = $"There are no interests with ID {id}." });

            }
            var interestForView = interest.ToInterestViewModel();

            var generalFields = _generalFieldsService.GetAll();
            var generalFieldsForView = generalFields.Select(x => x.ToGeneralFieldViewModel()).ToList();

            var editInterestViewModel = new EditInterestViewModel();
            editInterestViewModel.GeneralFields = generalFieldsForView;
            editInterestViewModel.Interest = interestForView;


            return View(editInterestViewModel);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditInterestViewModel editInterestViewModel)
        {
            if (ModelState.IsValid)
            {
                var interest = editInterestViewModel.Interest.ToModel();
                var response = _interestsService.Update(interest);
                if (response.IsSuccessful)
                {
                    return RedirectToAction("Overview", new { SuccessMessage = $"The interest {editInterestViewModel.Interest.Name} has been successfully created." });
                }
                else
                {
                    return RedirectToAction("Overview", new { ErrorMessage = response.Message });
                }
            }
            else
            {
                return View(editInterestViewModel);
            }
        }
    }
}
