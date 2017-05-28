using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LiveGraph.InterfaceDao;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
		private IAppTestDao _dao;
		public UnitTest1(IAppTestDao dao)
		{
			_dao = dao;
		}
		[TestMethod]
        public void TestMethod1()
        {
			_dao.Add(null);
        }
    }
}
