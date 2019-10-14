using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace Tcc.Entity
{
    public class UsersRepository : Contexto, IContext
    {
        public UsersRepository()
            : base()
        {

        }
        public DbSet<User> Userss { get; set; }
        public DbSet<AspNetUsers> AspNets { get; set; }

        public bool add(Modelo prEntity)
        {
            try
            {
                User Entity = (User)prEntity;

                Userss.Add(Entity);

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
                User Entity = (User)prEntity;

                Userss.Attach(Entity);

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
                User Entity = (User)prEntity;

                Userss.Attach(Entity);

                if (Entry(Entity).State == EntityState.Detached)
                    Userss.Attach(Entity);

                Userss.Remove(Entity);

                return SaveChanges() > 0;
            }
            catch (Exception e)
            {                
                return false;
            }
        }

        public User getId(int id)
        {
            return (from usu in Userss where usu.userid == id select usu).FirstOrDefault();
        }
        public User getCPF(string cpf)
        {
            return (from usu in Userss where usu.cpf == cpf && usu.ativo == 1 select usu).FirstOrDefault();
        }

        public User GetUserId(string membershipid)
        {
            var linq = from usu in Userss
                       where usu.membershipid == membershipid
                       select usu;

            return linq.FirstOrDefault();                       
        }

        public List<User> getSlaves(decimal userid)
        {
            var linq = from usu in Userss
                       where usu.usermasterid == userid
                       select usu;

            return linq.ToList();
        }

        public User getMembershipEmail(string email)
        {
            var linq = from usu in AspNets
                       where usu.Email == email.ToLower()
                       select usu;

            var aspUser = linq.FirstOrDefault();

            User lRetorno = null;

            if (aspUser != null)
                lRetorno = GetUserId(aspUser.Id);

            return lRetorno;
        }
    }
}