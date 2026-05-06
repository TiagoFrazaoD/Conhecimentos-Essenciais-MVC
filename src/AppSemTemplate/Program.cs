using AppSemTemplate.Configuration;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddGlobalizationConfig()
               .AddIdentityConfiguration()
               .AddMvcConfiguration()
               .AddDependdencyInjectionConfiguration();

        var app = builder.Build();

        app.UseMvcConfiguration();

        app.Run();
    }
}