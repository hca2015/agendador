using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tcc.Entity
{
    public class AgendaDTO : Modelo
    {        
        public Servico servico { get; set; }
        public Empresa empresa { get; set; }
        public Agenda agenda { get; set; }
        public ClienteFixoDTO cliente { get; set; }
    }
}