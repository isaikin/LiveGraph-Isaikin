using LiveGraph.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveGraph.InterfaceBLL
{
    public interface IAppTestBLL
    {
		int Add(AppTest test);

		IEnumerable<AppTest> GetAll();

		AppTest GetById(int id);

		void Passage(PassageAppTest passageTest, string name);

		MarkTests GetMarkByName(string nameUser);

		MarkTests GetMarks();
	}
}
