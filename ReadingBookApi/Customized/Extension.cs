using ReadingBookApi.Repository;
using ReadingBookApi.Service;
using ReadingBookApi.Services;
using System.Collections.Generic;

namespace ReadingBookApi.Customized
{
    public static class Extension 
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IBook, BookService>();
            services.AddScoped<IUser, UserService>();
       

            return services;
        }
    }
}
