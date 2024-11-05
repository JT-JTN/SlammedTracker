using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ST.DataAccess.Repositories;
using ST.Infrastructure.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.DataAccess
{
    public static class DataAccessServicesDI
    {
        public static void AddDataAccessServices(this IServiceCollection services, string stConnection)
        {
            services.AddDbContext<STDataContext>(options => options.UseSqlServer(stConnection));

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
        }
    }
}
