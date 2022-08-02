using Business.Core.Contracts;
using Business.Core.Interfaces;
using Framework.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Seisankanri.Model;
using Seisankanri.Model.Entities;

namespace Seisankanri.Api.Extensions
{
    public static class DbInitializer
    {
        public static IApplicationBuilder ConfigureCoreDb(this IApplicationBuilder app)
        {
            // configure app
            SeedData.Initialize(app.ApplicationServices);
            return app;
        }

    }

    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<CoreDbContext>();

                // auto migration
                context.Database.Migrate();


                // Seed the database.
                InitializeUserAndRoles(context);
                //InitializeFunctionAndResource(context);
            }
        }

        private static void InitializeUserAndRoles(CoreDbContext context)
        {
            // init user and roles  
        }

        private static void InitializeFunctionAndResource(CoreDbContext context)
        {
            if (context.Functions.ToList().Count > 0 || context.Resources.ToList().Count > 0 || context.Permissions.ToList().Count > 0)
            {
                context.RemoveRange(context.Permissions.ToList());
                context.RemoveRange(context.Functions.ToList());
                context.RemoveRange(context.Resources.ToList());
            }
            context.SaveChanges();

            using (StreamReader r = new StreamReader("DataSeed/function.json"))
            {
                string json = r.ReadToEnd();
                var Items = JsonConvert.DeserializeObject<List<Function>>(json);

                foreach (var item in Items)
                {
                    item.id = Guid.NewGuid();
                    item.created_date = DateTime.UtcNow;
                    item.created_by = Guid.NewGuid();
                    context.Functions.Add(item);
                }
            }

            using (StreamReader r = new StreamReader("DataSeed/resource.json"))
            {
                string json = r.ReadToEnd();
                var Items = JsonConvert.DeserializeObject<List<Resource>>(json);
                foreach (var item in Items)
                {
                    item.id = Guid.NewGuid();
                    item.created_date = DateTime.UtcNow;
                    item.created_by = Guid.NewGuid();
                    context.Resources.Add(item);
                }
            }
            context.SaveChanges();

        }
    }

}
