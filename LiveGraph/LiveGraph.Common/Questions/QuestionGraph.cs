using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace LiveGraph.Common.Questions
{

	public class QuestionGraph: BaseQuestions
	{
		[XmlAttribute]
		public Dictionary<int, List<int>> Graph { get; set; }

		[XmlAttribute]
		public Algorithms Algorithm { get; set; }

		[XmlAttribute]
		public int StartVertex { get; set; }
	}
}
