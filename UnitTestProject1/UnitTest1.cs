using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tcc.Entity;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var b = new PARAMETRIZACAOAGENDA { HORAINI = 1 };
            ParametrizacaoAgendaRepository a = new ParametrizacaoAgendaRepository();
            a.add(b);
        }
    }
}
