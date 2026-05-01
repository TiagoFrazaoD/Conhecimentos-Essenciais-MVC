using Microsoft.AspNetCore.Mvc;

namespace AppSemTemplate.Areas.Produtos.Controllers
{
    [Area("Produtos")]
    public class CadastroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
