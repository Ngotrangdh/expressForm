using expressForm.Core.Models.Forms;
using expressForm.Web.Controllers;
using expressForm.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace expressForm.UnitTest.Web.Controllers
{
    public class FormControllerTests
    {
        [Fact]
        public async void CreateForm_WithCorrectModel()
        {
            var mockFormRepository = new Mock<IFormRepository>();
            var formController = new FormsController(mockFormRepository.Object);

            var viewModel = new FormViewModel { Id = 1, Title = "First Form", Description = "Form 1" };

            IActionResult result = await formController.Create(viewModel);

            Assert.NotNull(result);
        }
    }
}
