using LiveGraph.BLL;
using LiveGraph.Common;
using LiveGraph.InterfaceDao;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LiveGraph.Test.BLL
{
    public class AppTestBLLTest
    {
		[Fact]
		public void AppTestBLLAddEx()
		{
			var mockDao = new Mock<IAppTestDao>();
			mockDao.Setup(item => item.Add(It.IsAny<AppTest>())).Returns(1);

			var bll = new AppTestBLL(mockDao.Object);
			var ex = Record.Exception(() => bll.Add(new AppTest()));

			Assert.Null(ex);
		}

		[Fact]
		public void AppTestBLLAdd()
		{
			var mockDao = new Mock<IAppTestDao>();
			mockDao.Setup(item => item.Add(It.IsAny<AppTest>())).Returns(1);

			var bll = new AppTestBLL(mockDao.Object);


			Assert.Equal (bll.Add(new AppTest()),1);
		}

		[Fact]
		public void AppTestBLLGetAll()
		{
			var mockDao = new Mock<IAppTestDao>();
			mockDao.Setup(item => item.GetAll()).Returns(new List<AppTest>());

			var bll = new AppTestBLL(mockDao.Object);

			Assert.Empty(bll.GetAll());
		}

		[Fact]
		public void AppTestBLLGetAllEx()
		{
			var mockDao = new Mock<IAppTestDao>();
			mockDao.Setup(item => item.GetAll()).Returns(new List<AppTest>());

			var bll = new AppTestBLL(mockDao.Object);
			var ex = Record.Exception(() => bll.GetAll());

			Assert.Null(ex);
		}
	}
}
