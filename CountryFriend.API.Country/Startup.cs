using CountryFriend.Domain.Country.Repository;
using CountryFriend.Domain.Friend.Repository;
using CountryFriend.Domain.State.Repository;
using CountryFriend.Repository.Context;
using CountryFriend.Repository.Country;
using CountryFriend.Repository.Friend;
using CountryFriend.Repository.State;
using CountryFriend.Services.Country;
using CountryFriend.Services.Friend;
using CountryFriend.Services.State;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CountryFriend.API.Country
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IFriendService, FriendService>();
            services.AddTransient<IFriendRepository, FriendRepository>();
            services.AddTransient<IFriendFriendService, FriendFriendService>();
            services.AddTransient<IFriendFriendRepository, FriendFriendRepository>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IStateService, StateService>();
            services.AddTransient<IStateRepository, StateRepository>();
            services.AddDbContext<CountryFriendContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("CountryFriendConnection"));
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
