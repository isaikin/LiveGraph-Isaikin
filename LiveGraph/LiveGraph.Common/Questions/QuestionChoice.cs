using System;
using System.Collections.Generic;
using System.Text;

namespace LiveGraph.Common.Questions
{
    public class QuestionChoice: BaseQuestions
	{
		public string Question { get; set; }

		public List<Answers> Answers { get; set; }
	}
}
