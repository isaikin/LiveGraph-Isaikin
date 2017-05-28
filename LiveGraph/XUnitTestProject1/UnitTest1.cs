using LiveGraph.InterfaceDao;
using System;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {

		private IAppTestDao _dao;
		public UnitTest1(IAppTestDao dao)
		{
			_dao = dao;
		}
		[Fact]
        public void Test1(IAppTestDao dao)
        {
			dao.Add(null);
        }
    }
}
