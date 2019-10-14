using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class UserEmpresaRepository : Contexto, IContext
    {
        public UserEmpresaRepository()
            : base()
        {

        }
        public DbSet<UserEmpresa> userempresas { get; set; }
        
        public bool add(Modelo prEntity)
        {
            try
            {
                UserEmpresa Entity = (UserEmpresa)prEntity;

                userempresas.Add(Entity);

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
                UserEmpresa Entity = (UserEmpresa)prEntity;

                userempresas.Attach(Entity);

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
                UserEmpresa Entity = (UserEmpresa)prEntity;

                userempresas.Attach(Entity);

                if (Entry(Entity).State == EntityState.Detached)
                    userempresas.Attach(Entity);

                userempresas.Remove(Entity);

                return SaveChanges() > 0;
            }
            catch (Exception e)
            {               
                return false;
            }
        }

        public UserEmpresa getUser(int userId)
        {
            IQueryable<UserEmpresa> linq = from users in userempresas
                       where users.userid == userId
                       select users;

            return linq.FirstOrDefault();
        }
    }
}