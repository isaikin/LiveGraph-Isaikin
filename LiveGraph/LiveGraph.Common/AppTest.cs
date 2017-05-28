using LiveGraph.Common.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveGraph.Common
{
	public class AppTest
	{
		public int Id { get; set; }

		public string Decription { get; set; }

		public List<QuestionChoice> QuestionsChoice { get; set; }

		public List<QuestionGraph> QuestionsGraph { get; set; }

		public List<QuestionNoAnswer> QuestionsNoAnswer { get; set; }

	}
}