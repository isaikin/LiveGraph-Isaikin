using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using LiveGraph.InterfaceDao;
using LiveGraph.Common;
using LiveGraph.BLL;
namespace LiveGraph.Test.BLL
{
    public class PageBLLTest
    {
		[Fact]
		public void PageBLLAdd()
		{
			var mockDao = new Mock<IPageDao>();
			mockDao.Setup(item => item.Add(It.IsAny<PageDto>()));

			var bll = new PageBLL(mockDao.Object);
			var ex = Record.Exception(() => bll.Add(new PageDto()));

			Assert.Null(ex);
		}

		[Fact]
		public void PageBLLGetAll()
		{
			var mockDao = new Mock<IPageDao>();
			mockDao.Setup(item => item.GetAll()).Returns(new List<PageDto>() { new PageDto { Name = "Name"} });

			var bll = new PageBLL(mockDao.Object);
			

			Assert.NotEmpty(bll.GetAll());
		}

		[Fact]
		public void PageBLLGetById()
		{
			var mockDao = new Mock<IPageDao>();
			mockDao.Setup(item => item.GetById(It.IsAny<int>())).Returns(new PageDto { Name = "Name" });

			var bll = new PageBLL(mockDao.Object);

			var id = 12;
			Assert.NotNull(bll.GetById(id));
		}

		[Fact]
		public void PageBLLUpdate()
		{
			var mockDao = new Mock<IPageDao>();
			mockDao.Setup(item => item.Update(It.IsAny<PageDto>()));

			var bll = new PageBLL(mockDao.Object);

			var page = new PageDto();
			var ex = Record.Exception(() => bll.Update(page));

			Assert.Null(ex);
		}
	}
}
