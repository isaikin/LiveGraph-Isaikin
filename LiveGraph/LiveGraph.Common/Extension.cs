using LiveGraph.Common.Questions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace LiveGraph.Common
{
    public static class Extension
    {
		public static string Serialize(this List<QuestionGraph>  value)
		{
			if (value == null)
			{
				return string.Empty;
			}
			try
			{
				var xmlserializer = new XmlSerializer(typeof( QuestionGraph));
				var stringWriter = new StringWriter();
				using (var writer = XmlWriter.Create(stringWriter))
				{
					xmlserializer.Serialize(writer, value);
					return stringWriter.ToString();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("An error occurred", ex);
			}
		}
	}
}
