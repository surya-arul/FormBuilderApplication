using FormBuilderMVC.DTOs.Base;
using FormBuilderMVC.DTOs.Request;
using FormBuilderMVC.Models;
using FormBuilderMVC.Repositories;
using FormBuilderMVC.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace FormBuilderMVC.Controllers
{
    public class ControlsController : Controller
    {
        private readonly IControlRepository _controlRepository;

        public ControlsController(IControlRepository controlRepository)
        {
            _controlRepository = controlRepository;
        }

        #region ControlCRUD

        #region Get all Controls

        public async Task<IActionResult> Controls()
        {
            try
            {
                var response = await _controlRepository.GetAllControls();
                return View(response);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }

        #endregion

        #region Create Control

        public IActionResult CreateControl()
        {
            try
            {
                return View(new ControlsDto());
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddControl(ControlsDto createControlRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(nameof(CreateControl), createControlRequest);
                }

                var response = await _controlRepository.CreateControl(new CreateControlRequest { Control = createControlRequest });
                return RedirectToAction(nameof(Controls));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }

        #endregion

        #region Edit Control

        public async Task<IActionResult> EditControl(GetControlRequest request)
        {
            try
            {
                var control = await _controlRepository.GetControlById(request);

                if (control is null)
                {
                    return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = "No data to edit." });
                }

                var controlDto = control.Control;

                return View(controlDto);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateControl(ControlsDto updatedControlRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(nameof(EditControl), updatedControlRequest);
                }

                var response = await _controlRepository.UpdateControl(new UpdateControlRequest() { Control = updatedControlRequest });

                return RedirectToAction(nameof(Controls));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }

        #endregion

        #region Delete Control

        public async Task<IActionResult> DeleteControl(DeleteControlRequest deleteControlRequest)
        {
            try
            {
                var response = await _controlRepository.DeleteControl(deleteControlRequest);

                return RedirectToAction(nameof(Controls));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }

        #endregion

        #endregion
    }
}
