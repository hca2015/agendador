using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace Tcc.Entity
{
    public class CopiarRepo : Contexto, IContext
    {
        public CopiarRepo()
            : base()
        {

        }
        public DbSet<ParametrizacaoAgenda> ParametrizacaoAgendas { get; set; }
        
        public bool add(Modelo prEntity)
        {
            try
            {
                ParametrizacaoAgenda Entity = (ParametrizacaoAgenda)prEntity;

                ParametrizacaoAgendas.Add(Entity);

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
                ParametrizacaoAgenda Entity = (ParametrizacaoAgenda)prEntity;

                ParametrizacaoAgendas.Attach(Entity);

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
                ParametrizacaoAgenda Entity = (ParametrizacaoAgenda)prEntity;

                ParametrizacaoAgendas.Attach(Entity);

                if (Entry(Entity).State == EntityState.Detached)
                    ParametrizacaoAgendas.Attach(Entity);

                ParametrizacaoAgendas.Remove(Entity);

                return SaveChanges() > 0;
            }
            catch (Exception e)
            {                
                return false;
            }
        }

    }
}