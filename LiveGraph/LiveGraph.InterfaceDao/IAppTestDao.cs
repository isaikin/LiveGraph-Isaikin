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

		AppTest GetById(int id);

		void AddMarkUser(int mark, string nameUser, int idTest);

		MarkTests GetMarkByName(string nameUser);

		MarkTests GetMarks();
	}
}
