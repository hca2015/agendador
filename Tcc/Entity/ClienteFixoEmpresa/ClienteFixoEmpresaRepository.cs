using System;
using System.Linq;
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

        public ClienteFixoEmpresa getClienteFixo(int id)
        {
            return (from c in ClienteFixoEmpresas where c.clientefixoid == id select c).FirstOrDefault();
        }

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

                ClienteFixoEmpresa lClienteFixoEmpresa = ClienteFixoEmpresas.Find(Entity.clientefixoempresaid);

                if (lClienteFixoEmpresa != null && lClienteFixoEmpresa != Entity)
                {
                    lClienteFixoEmpresa.Update(Entity);
                }

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