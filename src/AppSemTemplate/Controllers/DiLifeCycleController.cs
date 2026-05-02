using AppSemTemplate.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppSemTemplate.Controllers
{
    [Route("teste-di")]
    public class DiLifeCycleController : Controller
    {
        public OperacaoServico OperacaoServico { get; set; }
        public OperacaoServico OperacaoServico2 { get; set; }

        public IServiceProvider ServiceProvider { get; set; }

        public DiLifeCycleController(OperacaoServico operacaoServico,
                                     OperacaoServico operacaoServico2,
                                     IServiceProvider serviceProvider)
        {
            OperacaoServico = operacaoServico;
            OperacaoServico2 = operacaoServico2;
            ServiceProvider = serviceProvider;
        }
        public string Index()
        {
           return
                "Primeira Instância: " + Environment.NewLine +
                OperacaoServico.Transient.OperacaoId + Environment.NewLine +
                OperacaoServico.Scoped.OperacaoId + Environment.NewLine +
                OperacaoServico.Singleton.OperacaoId + Environment.NewLine +
                OperacaoServico.SingletonInstance.OperacaoId + Environment.NewLine +

                Environment.NewLine +
                Environment.NewLine +


                "Segunda Instância: " + Environment.NewLine +
                OperacaoServico2.Transient.OperacaoId + Environment.NewLine +
                OperacaoServico2.Scoped.OperacaoId + Environment.NewLine +
                OperacaoServico2.Singleton.OperacaoId + Environment.NewLine +
                OperacaoServico2.SingletonInstance.OperacaoId + Environment.NewLine;
        }

        [Route("teste")]
        public string Teste([FromServices] OperacaoServico operacaoServico)
        { 
            return operacaoServico.Transient.OperacaoId + Environment.NewLine +
                   operacaoServico.Scoped.OperacaoId + Environment.NewLine +
                   operacaoServico.Singleton.OperacaoId + Environment.NewLine +
                   operacaoServico.SingletonInstance.OperacaoId + Environment.NewLine;  
        }

        [Route("view")]
        public IActionResult TesteView()
        {
            return View("Index");
        }

        [Route("container")]
        public string TesteContainer()
        {
            using(var serviceScope = ServiceProvider.CreateScope())
            {
                var singService = serviceScope.ServiceProvider.GetRequiredService<IOperacaoSingleton>();

                return "Instância Singleton: " + Environment.NewLine +
                       singService.OperacaoId + Environment.NewLine;
            }
        }
    }
}
