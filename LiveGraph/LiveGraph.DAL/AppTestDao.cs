using Dapper;
using ExtendedXmlSerialization;
using LiveGraph.Common;
using LiveGraph.Common.Questions;
using LiveGraph.InterfaceDao;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace LiveGraph.DAL
{
    public class AppTestDao:BaseDao, IAppTestDao
	{
		public AppTestDao(IOptions<SettingsDao> connectionString) : base(connectionString.Value.ConnectionString)
		{
		}

		
		public  int Add(AppTest test)
		{ 
			using (var connection = base.GetConnection())
			{
				ExtendedXmlSerializer serializer = new ExtendedXmlSerializer();

				connection.Open();
				connection.Execute("AddTest",
					new
					{
						QuestionsGraph = test.QuestionsGraph != null 
						? serializer.Serialize(test.QuestionsGraph)
						: "",
						QuestionsChoice = test.QuestionsChoice != null 
						? serializer.Serialize(test.QuestionsChoice)
						: "",
						QuestionsNoAnswer = test.QuestionsNoAnswer != null 
						? serializer.Serialize(test.QuestionsNoAnswer)
						: "",
						Decription = test.Decription
					},
					commandType: System.Data.CommandType.StoredProcedure);
			}; 
			return 0;
		}

		public IEnumerable<AppTest> GetAll()
		{

			using (var connection = base.GetConnection())
			{
				var cmd = new CommandDefinition("GetAllTests", commandType: System.Data.CommandType.StoredProcedure);

				var reader = connection.ExecuteReader(cmd);
				ExtendedXmlSerializer serializer = new ExtendedXmlSerializer();

				while (reader.Read())
				{
					var result = new AppTest();

					result.Decription = reader["Decription"] as string;
					result.Id = (int)reader["Id"];

					yield return result;
				};
			}
		}

		public AppTest GetById(int id)
		{
			AppTest result = null;
			using (var connection = base.GetConnection())
			{
				var cmd = new CommandDefinition("GetById",new { Id = id }, commandType: System.Data.CommandType.StoredProcedure);

				var reader = connection.ExecuteReader(cmd);
				ExtendedXmlSerializer serializer = new ExtendedXmlSerializer();

				if (reader.Read())
				{
					result = new AppTest();

					result.Decription = reader["Decription"] as string;
					result.Id = (int)reader["Id"];
					if (!reader.IsDBNull(reader.GetOrdinal("QuestionsChoice")) && (string)reader["QuestionsChoice"] != "")
					{
						var temp = (string)reader["QuestionsChoice"];
						result.QuestionsChoice = serializer.Deserialize<List<QuestionChoice>>(temp);
					}
					if (!reader.IsDBNull(reader.GetOrdinal("QuestionsGraph")) && (string)reader["QuestionsGraph"] != "")
					{
						var temp = (string)reader["QuestionsGraph"];
						result.QuestionsGraph = serializer.Deserialize<List<QuestionGraph>>(temp);
					}
					if (!reader.IsDBNull(reader.GetOrdinal("QuestionsNoAnswer")) && (string)reader["QuestionsNoAnswer"] != "")
					{
						var temp = (string)reader["QuestionsNoAnswer"];
						result.QuestionsNoAnswer = serializer.Deserialize<List<QuestionNoAnswer>>(temp);
					}
				};
			}

			return result;
		}

		public void AddMarkUser(int mark, string nameUser, int idTest)
		{
			using (var connection = base.GetConnection())
			{
				ExtendedXmlSerializer serializer = new ExtendedXmlSerializer();

				connection.Open();
				connection.Execute("AddMarkUser",
					new
					{
						IdTest = idTest,
						NameUser = nameUser,
						Mark = mark
					},
					commandType: System.Data.CommandType.StoredProcedure);
			};
		}

		public MarkTests GetMarkByName(string nameUser)
		{
			MarkTests result = new MarkTests();
			using (var connection = base.GetConnection())
			{
				var cmd = new CommandDefinition("GetTestMarkByMark", new { NameUser = nameUser } , commandType: System.Data.CommandType.StoredProcedure);
				var reader = connection.ExecuteReader(cmd);


				while (reader.Read())
				{
					var temp = new ResultTest();
					temp.Name = (string)reader["Decription"];
					temp.Mark = (int)reader["Mark"];
					result.Tests.Add(temp);
				}
			};

			return result;
		}

		public MarkTests GetMarks()
		{
			MarkTests result = new MarkTests();
			using (var connection = base.GetConnection())
			{
				var cmd = new CommandDefinition("GetMarkTests", commandType: System.Data.CommandType.StoredProcedure);
				var reader = connection.ExecuteReader(cmd);


				while (reader.Read())
				{
					var temp = new ResultTest();
					temp.Name = (string)reader["Decription"];
					temp.Mark = (int)reader["Mark"];
					temp.LoginUser = (string)reader["Loggin"];
					result.Tests.Add(temp);
				}
			};

			return result;
		}
		private string GetXmlForQuestionChoice(List<QuestionChoice> collection)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = doc.CreateElement("root");
			foreach (var item in collection)
			{
				var id = Guid.NewGuid();

				XmlNode node = doc.CreateElement("Question");

				XmlAttribute question = doc.CreateAttribute("Value");
				question.Value = item.Question;
				node.Attributes.Append(question);

				XmlAttribute IdQuestion = doc.CreateAttribute("IdQuestion");
				IdQuestion.Value = id.ToString();
				node.Attributes.Append(IdQuestion);

				XmlAttribute mark = doc.CreateAttribute("Mark");
				mark.Value = item.Mark.ToString();
				node.Attributes.Append(mark);

				foreach (var answer in item.Answers)
				{
					XmlNode nodeAnswer = doc.CreateElement("Answer");

					nodeAnswer.Attributes.Append(IdQuestion);

					XmlAttribute answerValue = doc.CreateAttribute("Value");
					answerValue.Value = answer.Answer;
					nodeAnswer.Attributes.Append(answerValue);

					XmlAttribute isCorrectAnswer = doc.CreateAttribute("IsCorrectAnswer");
					isCorrectAnswer.Value = answer.CorrectAnswer.ToString();
					nodeAnswer.Attributes.Append(isCorrectAnswer);

					node.AppendChild(nodeAnswer);
				}
			
				root.AppendChild(node);
			}

			return root.OuterXml;
		}
	}
}
