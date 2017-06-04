using LiveGraph.InterfaceBLL;
using LiveGraph.InterfaceDao;
using System;
using System.Collections.Generic;
using System.Text;
using LiveGraph.Common;
using System.Linq;

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
			foreach (var item in test.QuestionsChoice)
			{
				item.Id = Guid.NewGuid();
				foreach (var answer in item.Answers)
				{
					answer.Id = Guid.NewGuid();
				}
			}

			return _dao.Add(test);
		}

		public IEnumerable<AppTest> GetAll()
		{
			return _dao.GetAll().ToList();
		}

		public AppTest GetById(int id)
		{
			return _dao.GetById(id);
		}

		public void Passage(PassageAppTest passageTest, string name)
		{
			int mark = 0;
			var test = _dao.GetById(passageTest.Id);
			if (passageTest.QuestionsChoice.Count != 0)
			{
				foreach (var item in passageTest.QuestionsChoice)
				{
					if (test.QuestionsChoice.First(x => x.Id == item.IdQuestion).Answers.First(x => x.Id == item.IdAnswer).CorrectAnswer)
					{
						mark += 1;
					}
				}
			}

			_dao.AddMarkUser(mark, name, passageTest.Id);
		}

		public MarkTests GetMarkByName(string nameUser)
		{
			return _dao.GetMarkByName(nameUser);
		}

		public MarkTests GetMarks()
		{
			return _dao.GetMarks();
		}
	}
}
