using ClickApp.Mappings;
using ClickApp.Models;
using ClickApp.Services.Interfaces;
using ClickApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ClickApp.Controllers
{
    public class SkillController : Controller
    {
        private readonly ISkillsService _skillsService;
        private readonly IGeneralFieldsService _generalFieldsService;
        private readonly UserManager<ApplicationUser> _userManager;
        public SkillController(ISkillsService skillsService, IGeneralFieldsService generalFieldsService, UserManager<ApplicationUser> userManager)
        {
            _skillsService = skillsService;
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
            var skills = _skillsService.GetAll();

            if (skills.Count == 0 || skills == null)
            {
                ViewBag.ErrorMessage =  "There are no skills in the DB at this point";
                return View();
            }
            else
            {
                var skillsForView = skills.Select(x => x.ToSkillViewModel()).ToList();
                return View(skillsForView);
            }

            
        }
        
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            var generalFields = _generalFieldsService.GetAll().Where(x => x.Code == GeneralFieldCode.SKL).ToList();
            var generalFieldsForView = generalFields.Select(x => x.ToGeneralFieldViewModel()).ToList();
            var skillView = new CreateSkillViewModel();
            skillView.GeneralFields = generalFieldsForView;
            return View(skillView);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateSkillViewModel createSkillViewModel)
        {
            if (ModelState.IsValid)
            {
                var skill = createSkillViewModel.ToModel();
                var response = _skillsService.Create(skill);
                if (response.IsSuccessful)
                {
                    return RedirectToAction("Overview", new { SuccessMessage = $"The skill {skill.Name} has been successfully created." });
                }
                else
                {
                    return RedirectToAction("Overview", new { ErrorMessage = response.Message });
                }
            }
            else
            {
                return View(createSkillViewModel);
            }
        }
        
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var response = _skillsService.Delete(id);
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
            var skill = _skillsService.GetById(id);
            if (skill == null)
            {
                return RedirectToAction("Overview", new { ErrorMessage = $"There are no skills with ID {id}." });

            }
            var skillForView = skill.ToSkillViewModel();

            return View(skillForView);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(SkillViewModel skillViewModel)
        {
            if (ModelState.IsValid)
            {
                var skill = skillViewModel.ToModel();
                var response = _skillsService.Update(skill);
                if (response.IsSuccessful)
                {
                    return RedirectToAction("Overview", new { SuccessMessage = $"The skill {skillViewModel.Name} has been successfully created." });
                }
                else
                {
                    return RedirectToAction("Overview", new { ErrorMessage = response.Message });
                }
            }
            else
            {
                return View(skillViewModel);
            }
        }
    }
}
