﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Tcc
{
    public static class Utility
    {
        public static string removerCaracteresEspeciais(this string str)
        {
            return Regex.Replace(str, @"[^0-9a-zA-Z]+", "");
        }
    }

    public enum DiaSemana
    {
        [Description("Domingo")]
        Domingo = DayOfWeek.Sunday,
        [Description("Segunda-Feira")]
        Segunda = DayOfWeek.Monday,
        [Description("Terça-Feira")]
        Terca = DayOfWeek.Tuesday,
        [Description("Quarta-Feira")]
        Quarta = DayOfWeek.Wednesday,
        [Description("Quinta-Feira")]
        Quinta = DayOfWeek.Thursday,
        [Description("Sexta-Feira")]
        Sexta = DayOfWeek.Friday,
        [Description("Sábado")]
        Sabado = DayOfWeek.Saturday
    }

    public static class ValidaCNPJ
    {
        public static bool IsCnpj(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj) || cnpj.Length != 14)
                return false;

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
                return false;

            tempCnpj = cnpj.Substring(0, 12);

            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }
    }

    public static class ValidaCPF
    {
        public static bool IsCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11)
                return false;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
    }

    public class CPFAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var cpf = Convert.ToString(value);
            cpf = cpf.removerCaracteresEspeciais();
            if (String.IsNullOrEmpty(cpf))
                return true;

            return ValidaCPF.IsCpf(cpf); // Aqui chamada para sua função de validar CPF
        }
    }
}