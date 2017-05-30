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
    public class AppUserDaoTest
    {
		[Fact]
		public void Add()
		{
			var connectionString = new Mock<IOptions<SettingsDao>>();
			connectionString.Setup(item => item.Value).Returns(new SettingsDao { ConnectionString = "Data Source=DESKTOP-L83JPJ8;Initial Catalog=LiveGraph;Integrated Security=True" });

			var dao = new AppUserDao(connectionString.Object);

			Assert.Null( dao.Login("", new List<byte>()));
		}

		[Fact]
		public void GetClaimsById()
		{
			var connectionString = new Mock<IOptions<SettingsDao>>();
			connectionString.Setup(item => item.Value).Returns(new SettingsDao { ConnectionString = "Data Source=DESKTOP-L83JPJ8;Initial Catalog=LiveGraph;Integrated Security=True" });

			var dao = new AppUserDao(connectionString.Object);

			Assert.NotNull(dao.GetClaimsById(Guid.Empty));

		}

		[Fact]
		public void GetNameAppUserById()
		{
			var connectionString = new Mock<IOptions<SettingsDao>>();
			connectionString.Setup(item => item.Value).Returns(new SettingsDao { ConnectionString = "Data Source=DESKTOP-L83JPJ8;Initial Catalog=LiveGraph;Integrated Security=True" });

			var dao = new AppUserDao(connectionString.Object);

			Assert.Throws<System.InvalidOperationException>(()=>dao.GetNameAppUserById(Guid.Empty));

		}
		[Fact]
		public void Registration()
		{
			var connectionString = new Mock<IOptions<SettingsDao>>();
			connectionString.Setup(item => item.Value).Returns(new SettingsDao { ConnectionString = "Data Source=DESKTOP-L83JPJ8;Initial Catalog=LiveGraph;Integrated Security=True" });

			var dao = new AppUserDao(connectionString.Object);

			Assert.Throws<System.NullReferenceException>(() => dao.Registration(new AppUser()));

		}

		[Fact]
		public void IsUserExists()
		{
			var connectionString = new Mock<IOptions<SettingsDao>>();
			connectionString.Setup(item => item.Value).Returns(new SettingsDao { ConnectionString = "Data Source=DESKTOP-L83JPJ8;Initial Catalog=LiveGraph;Integrated Security=True" });

			var dao = new AppUserDao(connectionString.Object);

			Assert.NotNull(dao.IsUserExists(new AppUser()));

		}
	}
}
