using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace Tcc.Entity
{
    public class AgendaRepository : Contexto, IContext
    {
        public AgendaRepository()
            : base()
        {

        }
        public DbSet<Agenda> Agendas { get; set; }
        
        public bool add(Modelo prEntity)
        {
            try
            {
                Agenda Entity = (Agenda)prEntity;

                Agendas.Add(Entity);

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
                Agenda Entity = (Agenda)prEntity;

                Agendas.Attach(Entity);

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
                Agenda Entity = (Agenda)prEntity;

                Agendas.Attach(Entity);

                if (Entry(Entity).State == EntityState.Detached)
                    Agendas.Attach(Entity);

                Agendas.Remove(Entity);

                return SaveChanges() > 0;
            }
            catch (Exception e)
            {                
                return false;
            }
        }

        public List<AgendaDTO> getAgendaDTO(DateTime prData)
        {
            return new List<AgendaDTO>();
        }
    }
}