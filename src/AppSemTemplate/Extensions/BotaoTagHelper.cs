
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AppSemTemplate.Extensions
{
    [HtmlTargetElement("*", Attributes = "tipo-botao, route-id")]
    public class BotaoTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LinkGenerator _linkGenerator;

        public BotaoTagHelper(IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator)
        {
            _httpContextAccessor = httpContextAccessor;
            _linkGenerator = linkGenerator;
        }


        [HtmlAttributeName("tipo-botao")]
        public TipoBotao TipoBotaoSelecao { get; set; }

        [HtmlAttributeName("route-id")]
        public int RouteId { get; set; }

        private string? nomeAction;
        private string? nomeClasse;
        private string? spanIcone;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            switch (TipoBotaoSelecao)
            {
                case TipoBotao.Detalhes:
                    nomeAction = "Details";
                    nomeClasse = "btn btn-info";
                    spanIcone = "fa-solid fa-search";
                    break;
                case TipoBotao.Editar:
                    nomeAction = "Edit";
                    nomeClasse = "btn btn-warning";
                    spanIcone = "fa-solid fa-pencil";
                    break;
                case TipoBotao.Excluir:
                    nomeAction = "Delete";
                    nomeClasse = "btn btn-danger";
                    spanIcone = "fa-solid fa-trash";
                    break;
            }

            var controllerName = _httpContextAccessor.HttpContext?.GetRouteData().Values["controller"]?.ToString();

            var host = $"{_httpContextAccessor.HttpContext.Request.Scheme}://" +
                $"{_httpContextAccessor.HttpContext.Request.Host.Value}";

            var indexPath = _linkGenerator.GetPathByAction(
                _httpContextAccessor.HttpContext,
                nomeAction,
                controllerName,
                values: new { id = RouteId })!;

            output.TagName = "a";
            output.Attributes.SetAttribute("href", $"{host}{indexPath}");
            output.Attributes.SetAttribute("class", nomeClasse);
            
            var iconSpan = new TagBuilder("span");
            iconSpan.AddCssClass(spanIcone);

            output.Content.AppendHtml(iconSpan);

        }
    }

    public enum TipoBotao
    {
        Detalhes = 1,
        Editar = 2,
        Excluir = 3
    }
}
