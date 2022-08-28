
namespace ServerBot
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {

            MyDataInitializer.InitializeDatabaseAsync(app.ApplicationServices);
        }

        //public Startup(IHostingEnvironment env)
        //{
            
        //}
    }
}
