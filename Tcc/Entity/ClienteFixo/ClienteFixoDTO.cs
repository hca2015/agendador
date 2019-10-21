using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tcc.Entity
{
    public class ClienteFixoDTO : Modelo
    {        
        [Key]
        public int clientefixoid { get; set; }
        public int? clienteid { get; set; }
        [DisplayName("Último pagamento")]
        [DataType(DataType.Date)]       
        public DateTime? dataultimopagamento { get; set; }
        [DisplayName("Dia da semana")]
        [EnumDataType(typeof(DayOfWeek))]
        public DayOfWeek diasemana { get; set; }
        [DisplayName("Frequencia")]
        [EnumDataType(typeof(ClienteFixo.TipoFrequencia))]
        public ClienteFixo.TipoFrequencia tipofrequencia { get; set; }
        [DisplayName("Horário")]        
        public int horario { get; set; }
        [DisplayName("Nome cliente")]
        [DataType(DataType.Text)]
        public string nomecliente { get; set; }
        [DisplayName("Data de nascimento")]
        [DataType(DataType.Date)]
        public DateTime? datanascimento { get; set; }
        [DisplayName("Documento")]
        [DataType(DataType.Text)]
        [CPF]
        public string documento { get; set; }
        [DataType(DataType.Text)]
        public int servicoid { get; set; }
        [DisplayName("Nome serviço")]
        [DataType(DataType.Text)]
        public string nomeservico { get; set; }
        public int empresaid { get; set; }
    }
}