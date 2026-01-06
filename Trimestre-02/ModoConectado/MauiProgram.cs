using System.Diagnostics;
using Microsoft.Extensions.Logging;
using ModoConectado.Interfaces;
using ModoConectado.Model;
using ModoConectado.Repository;
using ModoConectado.Service;
using SQLitePCL;

namespace ModoConectado
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            Batteries_V2.Init();

            var builder = MauiApp.CreateBuilder();
            builder.Services.AddSingleton<ICrudRepository<Department, int>, DepartmentRepository>();
            builder.Services.AddSingleton<ICrudRepository<Employee, int>, EmployeeRepository>();
            builder.Services.AddSingleton<IService<Department, int>, DepartmentService>();
            builder.Services.AddSingleton<IService<Employee, int>, EmployeeService>();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
