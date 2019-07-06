using LiveGraph.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveGraph.InterfaceDao
{
    public interface IAppTestDao
    {
		int Add(AppTest test);

		IEnumerable<AppTest> GetAll();
	}
}
