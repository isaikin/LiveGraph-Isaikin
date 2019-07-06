using LiveGraph.InterfaceBLL;
using LiveGraph.InterfaceDao;
using System;
using System.Collections.Generic;
using System.Text;
using LiveGraph.Common;

namespace LiveGraph.BLL
{
    public class AppTestBLL:IAppTestBLL
    {
		private IAppTestDao _dao;

		public AppTestBLL(IAppTestDao dao)
		{
			_dao = dao;
		}

		public int Add(AppTest test)
		{
			return _dao.Add(test);
		}

		public IEnumerable<AppTest> GetAll()
		{
			return _dao.GetAll();
		}
	}
}
