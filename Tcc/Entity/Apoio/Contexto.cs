using System.Data.Entity;

namespace Tcc.Entity
{
    public class Contexto : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        {
            dbModelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            dbModelBuilder.HasDefaultSchema("dbo");
            base.OnModelCreating(dbModelBuilder);
        }
        public Contexto() : base ("DefaultConnection")   
        {

        }            
    }
}