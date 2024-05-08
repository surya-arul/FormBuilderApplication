using FormBuilderDTO.DTOs.Control;
using FormBuilderSharedService.Models;
using FormBuilderSharedService.Repositories;
using FormBuilderSharedService.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace FormBuilderMVC.Controllers
{
    public class ControlController : Controller
    {
        private readonly IControlRepository _controlRepository;

        public ControlController(IControlRepository controlRepository)
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
                return View(new CreateControlRequest());
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddControl(CreateControlRequest createControlRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(nameof(CreateControl), createControlRequest);
                }

                var response = await _controlRepository.CreateControl(createControlRequest);
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

                return View(new UpdateControlRequest { Control = control.Control });

            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), StringHelper.ExtractControllerName(typeof(HomeController)), new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateControl(UpdateControlRequest updatedControlRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(nameof(EditControl), updatedControlRequest);
                }

                var response = await _controlRepository.UpdateControl(updatedControlRequest);

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
