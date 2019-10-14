using System;
using System.Data.Entity;

namespace Tcc.Entity
{
    public class ClienteFixoEmpresaRepository : Contexto, IContext
    {
        public ClienteFixoEmpresaRepository()
            : base()
        {

        }
        public DbSet<ClienteFixoEmpresa> ClienteFixoEmpresas { get; set; }

        public bool add(Modelo prEntity)
        {
            try
            {
                ClienteFixoEmpresa Entity = (ClienteFixoEmpresa)prEntity;

                ClienteFixoEmpresas.Add(Entity);

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
                ClienteFixoEmpresa Entity = (ClienteFixoEmpresa)prEntity;

                ClienteFixoEmpresas.Attach(Entity);

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
                ClienteFixoEmpresa Entity = (ClienteFixoEmpresa)prEntity;

                ClienteFixoEmpresas.Attach(Entity);

                if (Entry(Entity).State == EntityState.Detached)
                {
                    ClienteFixoEmpresas.Attach(Entity);
                }

                ClienteFixoEmpresas.Remove(Entity);

                return SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}