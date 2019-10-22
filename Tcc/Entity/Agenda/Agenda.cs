using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tcc.Entity
{
    public class Agenda : Modelo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int agendaid { get; set; }
        public int empresaid { get; set; }
        public int? servicoid { get; set; }
        public int? clienteid { get; set; }
        public int horaini { get; set; }
        public int horafim { get; set; }
        public DateTime dia { get; set; }

        public bool Validate()
        {
            if (clienteid < 1)
                return withoutError(newError("Cliente deve ser informado!"));

            if (servicoid < 1)
                return withoutError(newError("Servico deve ser informado!"));

            if (horaini < 1)
                return withoutError(newError("Hora inicio deve ser informado!"));

            if (horafim < 1)
                return withoutError(newError("Hora fim deve ser informado!"));

            if (empresaid < 1)
                return withoutError(newError("Empresa deve ser informada!"));

            return withoutError();
        }
    }
}