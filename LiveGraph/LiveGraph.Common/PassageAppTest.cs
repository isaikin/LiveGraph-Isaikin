using System;
using System.Collections.Generic;
using System.Text;

namespace LiveGraph.Common
{
    public class PassageAppTest
    {
		public int Id { get; set; }
		public List<PassageAppTestQuestionsChoice> QuestionsChoice { get; set; } = new List<PassageAppTestQuestionsChoice>();
	}
}
