using System;
using System.Data.Entity;

namespace Tcc.Entity
{
    public class ClienteFixoServicoRepository : Contexto, IContext
    {
        public ClienteFixoServicoRepository()
            : base()
        {

        }
        public DbSet<ClienteFixoServico> ClienteFixoServicos { get; set; }

        public bool add(Modelo prEntity)
        {
            try
            {
                ClienteFixoServico Entity = (ClienteFixoServico)prEntity;

                ClienteFixoServicos.Add(Entity);

                return SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool update(Modelo prEntity)
        {
            try
            {
                ClienteFixoServico Entity = (ClienteFixoServico)prEntity;

                ClienteFixoServicos.Attach(Entity);

                Entry(Entity).State = EntityState.Modified;

                return SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool delete(Modelo prEntity)
        {
            try
            {
                ClienteFixoServico Entity = (ClienteFixoServico)prEntity;

                ClienteFixoServicos.Attach(Entity);

                if (Entry(Entity).State == EntityState.Detached)
                {
                    ClienteFixoServicos.Attach(Entity);
                }

                ClienteFixoServicos.Remove(Entity);

                return SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}