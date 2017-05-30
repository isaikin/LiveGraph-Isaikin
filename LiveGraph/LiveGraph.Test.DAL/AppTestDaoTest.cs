using LiveGraph.Common;
using Moq;
using System;
using Xunit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using LiveGraph.DAL;

namespace LiveGraph.Test.DAL
{
    public class AppTestDaoTest
    {
        [Fact]
        public void Add()
        {
			var connectionString = new Mock<IOptions<SettingsDao>>();
			connectionString.Setup(item => item.Value).Returns(new SettingsDao { ConnectionString = "Data Source=DESKTOP-L83JPJ8;Initial Catalog=LiveGraph;Integrated Security=True" });

			var dao = new AppTestDao(connectionString.Object);

			Assert.NotNull(dao.Add(new AppTest()));

		}

		[Fact]
		public void GetALl()
		{
			var connectionString = new Mock<IOptions<SettingsDao>>();
			connectionString.Setup(item => item.Value).Returns(new SettingsDao { ConnectionString = "Data Source=DESKTOP-L83JPJ8;Initial Catalog=LiveGraph;Integrated Security=True" });

			var dao = new AppTestDao(connectionString.Object);

			Assert.NotEmpty(dao.GetAll());

		}
	}
}
