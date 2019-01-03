using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using System.Web;
using Tcc.Models;

namespace Tcc.Entity
{
    public class Contexto : DbContext
    {
        public enum KdOperation
        {
            [Display(Name = "Inserir")]
            kdInsert,
            [Display(Name = "Alterar")]
            kdAlter,
            [Display(Name = "Excluir")]
            kdDelete
        }

        protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        {
            dbModelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            dbModelBuilder.HasDefaultSchema("dbo");
            base.OnModelCreating(dbModelBuilder);
        }

        public Contexto() : base ("DefaultConnection")   
        {

        }

        public bool EntityManager(Modelo modelo, KdOperation operation)
        {
            switch (operation)
            {
                case KdOperation.kdInsert:
                    InserirCNPJs(modelo);
                    break;
                case KdOperation.kdAlter:
                    break;
                case KdOperation.kdDelete:
                    break;
                default:
                    break;
            }

            return false;
        }

        #region CadastrarCNPJs
        DbSet<CadastrarCNPJ> CadastrarCNPJs { get; set; }
        public bool InserirCNPJs(Modelo modelo)
        {
            var Entity = (CadastrarCNPJ)modelo;

            using (Contexto cx = new Contexto())
            {
                cx.CadastrarCNPJs.Add(Entity);                

                return cx.SaveChanges() > 0;
            }
        }

        public bool AlterarCNPJs(Modelo modelo)
        {
            var Entity = (CadastrarCNPJ)modelo;

            using (Contexto cx = new Contexto())
            {
                cx.CadastrarCNPJs.Attach(Entity);

                cx.Entry(Entity).State = EntityState.Modified;

                return cx.SaveChanges() > 0;
            }
        }

        public bool ExcluirCNPJs(Modelo modelo)
        {
            var Entity = (CadastrarCNPJ)modelo;

            using (Contexto cx = new Contexto())
            {
                cx.CadastrarCNPJs.Attach(Entity);

                if (cx.Entry(Entity).State == EntityState.Detached)
                    cx.CadastrarCNPJs.Attach(Entity);

                cx.CadastrarCNPJs.Remove(Entity);

                return cx.SaveChanges() > 0;
            }
        }
        #endregion

    }
}