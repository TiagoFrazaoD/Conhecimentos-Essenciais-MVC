using AppSemTemplate.Extensions;
using AppSemTemplate.Services;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AppSemTemplate.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static WebApplicationBuilder AddDependdencyInjectionConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddTransient<IOperacaoTransient, Operacao>();
            builder.Services.AddScoped<IOperacaoScoped, Operacao>();
            builder.Services.AddSingleton<IOperacaoSingleton, Operacao>();
            builder.Services.AddSingleton<IOperacaoSingletonInstance>(new Operacao(Guid.Empty));

            builder.Services.AddTransient<OperacaoServico>();

            builder.Services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();

            return builder;
        }
    }
}
