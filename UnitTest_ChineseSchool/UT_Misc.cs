using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using ChineseSchool.BusinessLogic;
using System.Collections.Generic;
using ChineseSchool.BusinessObjects;

namespace UnitTest_ChineseSchool
{
    [TestClass]
    public class UT_Misc
    {
        [TestMethod]
        public void GetClassList()
        {
            SqlConnection conn = Toolbox.OpenConnection();

            List<ClassData> list = CSAgent.GetClassList(conn);

            Toolbox.CloseConnection(conn);

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count > 0);
        }
    }
}
