using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tcc.Entity
{
    public class ClienteFixoDTO : Modelo
    {        
        [Key]
        public decimal clienteid { get; set; }
        [Description("Último pagamento")]
        [DataType(DataType.Date)]
        public DateTime dataultimopagamento { get; set; }
        [Description("Frequencia")]
        [EnumDataType(typeof(ClienteFixo.TipoFrequencia))]
        public int tipofrequencia { get; set; }
        [Description("Horário")]        
        public int horario { get; set; }
        [Description("Nome cliente")]
        [DataType(DataType.Text)]
        public string nomecliente { get; set; }
        [Description("Data de nascimento")]
        [DataType(DataType.Date)]
        public DateTime datanascimento { get; set; }
        [Description("Documento")]
        [DataType(DataType.Text)]
        public string documento { get; set; }
        [Description("Nome serviço")]
        [DataType(DataType.Text)]
        public string nomeservico { get; set; }
        public int empresaid { get; set; }
    }
}