using AppSemTemplate.Configuration;
using AppSemTemplate.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace AppSemTemplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly ApiConfiguration ApiConfig;
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(IConfiguration configuration,
                              IOptions<ApiConfiguration> apiConfig,
                              IStringLocalizer<HomeController> localizer)
        {
            Configuration = configuration;
            ApiConfig = apiConfig.Value;
            _localizer = localizer;
        }

        //[ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any, NoStore = true)]
        public IActionResult Index()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var apiConfig = new ApiConfiguration();
            Configuration.GetSection(ApiConfiguration.ConfigName).Bind(apiConfig);

            var secret = apiConfig.UserSecret;

            var user = Configuration[$"{ApiConfiguration.ConfigName}:UserKey"];

            var domain = ApiConfig.Domain;

            ViewData["Message"] = _localizer["Seja bem vindo!"];

            //ViewData["Horario"] = DateTime.Now;

            if (Request.Cookies.TryGetValue("MeuCookie", out string? cookieValue))
            {
                ViewData["MeuCookie"] = cookieValue;
            }

            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return LocalRedirect(returnUrl);
        }

        [Route("cookies")]
        public IActionResult Cookie()
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddHours(1),
            };

            Response.Cookies.Append("MeuCookie", "Dados do Cookie", cookieOptions);

            return View();
        }

        [Route("teste")]
        public IActionResult Teste()
        {
            throw new Exception("ALGO DEU ERRADO!");

            return View("Index");
        }

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Errors(int id)
        {
            var modelErro = new ErrorViewModel();

            if (id == 500)
            {
                modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                modelErro.Titulo = "Ocorreu um erro!";
                modelErro.ErroCode = id;
            }
            else if (id == 404)
            {
                modelErro.Mensagem = "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte";
                modelErro.Titulo = "Ops! Página não encontrada.";
                modelErro.ErroCode = id;
            }
            else if (id == 403)
            {
                modelErro.Mensagem = "Você não tem permissão para fazer isto.";
                modelErro.Titulo = "Acesso Negado";
                modelErro.ErroCode = id;
            }
            else
            {
                return StatusCode(500);
            }

            return View("Error", modelErro);
        }
    }
}
