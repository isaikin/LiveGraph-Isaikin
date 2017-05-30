using LiveGraph.Common;
using LiveGraph.DAL;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LiveGraph.Test.DAL
{
    public class PageDaoTest
    {
		[Fact]
		public void GetAll()
		{
			var connectionString = new Mock<IOptions<SettingsDao>>();
			connectionString.Setup(item => item.Value).Returns(new SettingsDao { ConnectionString = "Data Source=DESKTOP-L83JPJ8;Initial Catalog=LiveGraph;Integrated Security=True" });

			var dao = new PageDao(connectionString.Object);

			Assert.NotNull(dao.GetAll());

		}

		[Fact]
		public void GetById()
		{
			var connectionString = new Mock<IOptions<SettingsDao>>();
			connectionString.Setup(item => item.Value).Returns(new SettingsDao { ConnectionString = "Data Source=DESKTOP-L83JPJ8;Initial Catalog=LiveGraph;Integrated Security=True" });

			var dao = new PageDao(connectionString.Object);

			Assert.Throws<System.InvalidOperationException>(() => dao.GetById(12));

		}

		[Fact]
		public void Add()
		{
			var connectionString = new Mock<IOptions<SettingsDao>>();
			connectionString.Setup(item => item.Value).Returns(new SettingsDao { ConnectionString = "Data Source=DESKTOP-L83JPJ8;Initial Catalog=LiveGraph;Integrated Security=True" });

			var dao = new PageDao(connectionString.Object);
			Assert.Throws<System.Data.SqlClient.SqlException>(()=> dao.Add(new PageDto()));

		}

		[Fact]
		public void Update()
		{
			var connectionString = new Mock<IOptions<SettingsDao>>();
			connectionString.Setup(item => item.Value).Returns(new SettingsDao { ConnectionString = "Data Source=DESKTOP-L83JPJ8;Initial Catalog=LiveGraph;Integrated Security=True" });

			var dao = new PageDao(connectionString.Object);
			dao.Update(new PageDto());

		}
	}
}
