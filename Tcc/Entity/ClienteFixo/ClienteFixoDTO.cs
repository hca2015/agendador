﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tcc.Entity
{
    public class ClienteFixoDTO : Modelo
    {
        [Key]
        public int? clientefixoid { get; set; }
        public int? clienteid { get; set; }
        [DisplayName("Último serviço")]
        [DataType(DataType.Date)]
        public DateTime? dataultimoservico { get; set; }
        [DisplayName("Dia da semana")]
        [EnumDataType(typeof(DiaSemana))]
        public DiaSemana? diasemana { get; set; }
        [DisplayName("Frequencia")]
        [EnumDataType(typeof(ClienteFixo.TipoFrequencia))]
        public ClienteFixo.TipoFrequencia? tipofrequencia { get; set; }
        [DisplayName("Horário")]
        public int? horario { get; set; }
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
        public int? servicoid { get; set; }
        [DisplayName("Nome serviço")]
        [DataType(DataType.Text)]
        public string nomeservico { get; set; }
        public int? empresaid { get; set; }

        public bool ValidarUltimoServico(DateTime prData)
        {
            bool buscar = false;

            if (dataultimoservico.HasValue)
            {
                switch (tipofrequencia)
                {
                    case ClienteFixo.TipoFrequencia.Diario:
                        buscar = (prData - dataultimoservico.Value).TotalDays >= 1;
                        break;
                    case ClienteFixo.TipoFrequencia.Semanal:
                        buscar = (prData - dataultimoservico.Value).TotalDays >= 7;
                        break;
                    case ClienteFixo.TipoFrequencia.Quinzenal:
                        buscar = (prData - dataultimoservico.Value).TotalDays >= 15;
                        break;
                    case ClienteFixo.TipoFrequencia.Mensal:
                        buscar = (prData - dataultimoservico.Value).TotalDays >= 30;
                        break;
                    default:
                        break;
                }
            }
            else
                buscar = true;

            return buscar;
        }

        public void setUltimaConsulta(DateTime prData)
        {
            if (clientefixoid > 0)
            {
                var repo = new ClienteFixoRepository();
                var entity = repo.getId(clientefixoid.Value);
                entity.dataultimoservico = prData;
                repo.update(entity);
            }
        }
    }
}