using LiveGraph.Common;
using LiveGraph.InterfaceDao;
using LiveGraph.Validation.Interface;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace LiveGraph.Validation.Test
{
	public class AppUserValidationTest
	{
		[Fact]
		public void GetClaimsById()
		{
			var mockDao = new Mock<IAppUserDao>();

			mockDao.Setup(item => item.IsUserExists(It.IsAny<AppUser>())).Returns(new UserErrors());

			var localize = new Mock<IStringLocalizer<IAppUserValidation>>();
			var validator  = new AppUserValidation(mockDao.Object, null);

			List<CustomError> error;
			Assert.True(validator.IsValid(new AppUser(), out error));
			
		}
	}
}