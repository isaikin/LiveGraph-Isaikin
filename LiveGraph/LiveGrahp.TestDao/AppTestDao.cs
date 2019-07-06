using Microsoft.VisualStudio.TestTools.UnitTesting;
using LiveGraph.DAL;
using LiveGraph.Common;
using LiveGraph.InterfaceDao;

namespace LiveGrahp.TestDao
{
	[TestClass]
	public class UnitTest1
	{
		public UnitTest1(IAppTestDao dao)
		{

		}
		[TestMethod]
        public void TestMethod1()
        {
			AppTestDao dao = new AppTestDao(new IOptions<SettingsDao>());



		}
    }
}
