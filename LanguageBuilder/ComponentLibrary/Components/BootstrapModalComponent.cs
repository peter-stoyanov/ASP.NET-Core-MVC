using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ComponentLibrary
{
    public class BootstrapModalComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(BootstrapModalViewModel model)
        //string body,
        //string title = null,
        //string id = null,
        //string buttonName = null,
        //string cancelButtonName = null,
        //string confirmButtonId = null,
        //string cancelButtonId = null)
        {
            var viewmodel = new BootstrapModalViewModel()
            {
                //Body = model.body,
                //ConfirmButtonName = buttonName ?? "Ok",
                //Id = id ?? String.Empty,
                //Title = title ?? String.Empty,
                //AriaLabel = String.Format("{0}-{1}", id, "lbl"),
                //CancelButtonName = cancelButtonName ?? "Cancel",
                //ConfirmButtonId =
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
