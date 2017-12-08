using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ComponentLibrary
{
    public class BootstrapModalComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(BootstrapModalViewModel model)
        {
            var viewmodel = new BootstrapModalViewModel()
            {
                Body = model.Body,
                Id = model.Id ?? String.Empty,
                Title = model.Title ?? String.Empty,
                AriaLabel = String.Format("{0}-{1}", model.Id, "lbl"),
                ConfirmButtonName = model.ConfirmButtonName ?? "Ok",
                ConfirmButtonId = model.ConfirmButtonId ?? "modal-confirm",
                CancelButtonName = model.CancelButtonName ?? "Cancel",
                CancelButtonId = model.CancelButtonId ?? "modal-cancel"
            };

            return View(viewmodel);
        }
    }
}
