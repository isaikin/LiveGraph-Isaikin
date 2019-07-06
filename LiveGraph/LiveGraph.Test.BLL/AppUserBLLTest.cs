
using LiveGraph.BLL;
using LiveGraph.Common;
using LiveGraph.InterfaceBLL;
using LiveGraph.InterfaceDao;
using LiveGraph.Validation.Interface;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace LiveGraph.Test.BLL
{
    public class AppUserBLLTest
    {
        [Fact]
        public void AppUserBllLogin()
        {
			var mockDao = new Mock<IAppUserDao>();
			mockDao.Setup(item => item.Login(It.IsAny<string>(), It.IsAny<List<byte>>())).Returns(Guid.Empty);

			var mockValidation = new Mock<IAppUserValidation>();
			List<CustomError> list;
			mockValidation.Setup(item => item.IsValid(It.IsAny<AppUser>(), out list)).Returns(true);

			var bll = new AppUserBLL(mockDao.Object, mockValidation.Object);

			Assert.NotNull(bll.Login("234","234"));
		}

		[Fact]
		public void AppUserBllRegistration()
		{
			var mockDao = new Mock<IAppUserDao>();
			mockDao.Setup(item => item.Registration(It.IsAny<AppUser>())).Returns(true);

			var mockValidation = new Mock<IAppUserValidation>();
			List<CustomError> list;
			mockValidation.Setup(item => item.IsValid(It.IsAny<AppUser>(), out list)).Returns(true);

			var bll = new AppUserBLL(mockDao.Object, mockValidation.Object);

			Assert.NotNull(bll.Registration(new AppUser { Login = "123",Email="www.and1.v@bk.ru",Password = new List<byte>()},out  list));
		}

		[Fact]
		public void AppUserBllClaimsPrincipalById()
		{
			var mockDao = new Mock<IAppUserDao>();
			mockDao.Setup(item => item.GetClaimsById(It.IsAny<Guid>())).Returns(new List<Claim>());
			mockDao.Setup(item => item.GetNameAppUserById(It.IsAny<Guid>())).Returns("Admin");
			var mockValidation = new Mock<IAppUserValidation>();
			List<CustomError> list;
			mockValidation.Setup(item => item.IsValid(It.IsAny<AppUser>(), out list)).Returns(true);

			var bll = new AppUserBLL(mockDao.Object, mockValidation.Object);

			Assert.NotNull(bll.ClaimsPrincipalById(Guid.Empty));
		}
	}
}
