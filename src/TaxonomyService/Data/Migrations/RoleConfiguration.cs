using System.Data.Entity.Migrations;
using TaxonomyService.Data;
using TaxonomyService.Data.Model;
using TaxonomyService.Features.Users;

namespace TaxonomyService.Migrations
{
    public class RoleConfiguration
    {
        public static void Seed(TaxonomyServiceContext context) {

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.SYSTEM
            });

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.ACCOUNT_HOLDER
            });

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.DEVELOPMENT
            });

            context.SaveChanges();
        }
    }
}
