using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVCAPP.Controllers
{
    public class ErrorController : Controller
    {
        //If he comming from worng Url
        [Route("/Error/{StatusCode}")]
        public IActionResult HttpStatusCodeHandler(int StatusCode)
        {
            switch (StatusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resources you request is not found. ";
                    break;
                case 405:
                    ViewBag.ErrorMessage = "Sorry, the resources you request is not found. ";
                    break;
            }

            return View("NotFound");
        }
    }
}
