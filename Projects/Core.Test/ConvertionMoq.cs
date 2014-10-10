using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOGI.Framework.Toolkit.Core.Extensions.ExtObject;

namespace Toolkit.Core.Test
{
    [TestClass]
    public class ConvertionMoq
    {
        #region Convert
        [TestMethod]
        public void ConvertToBool()
        {
            Assert.IsTrue("True".To<bool>());
        }

        [TestMethod]
        public void ConvertToInt()
        {
            Assert.Equals(1, "1".To<int>());
        }

        [TestMethod]
        public void ConvertToDateTime1()
        {
            Assert.Equals(new DateTime(2009,01,10), "10.01.2009".To<DateTime>());
        }

        [TestMethod]
        public void ConvertToDateTime2()
        {
            Assert.Equals(new DateTime(2009, 01, 10), "10/01/2009".To<DateTime>());
        }

        #endregion

    }
}
