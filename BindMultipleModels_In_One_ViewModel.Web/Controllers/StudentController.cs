using Microsoft.AspNetCore.Mvc;

namespace BindMultipleModels_In_One_ViewModel.Web.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
