using System;
using System.Collections.Generic;
using System.Text;

namespace LiveGraph.Common.Questions
{
    public class QuestionGraph: BaseQuestions
	{
		public Dictionary<int, List<int>> Graph { get; set; }

		public Algorithms Algorithm { get; set; }

	}
}
