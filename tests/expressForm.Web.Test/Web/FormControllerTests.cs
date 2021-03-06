﻿using expressForm.Core.Forms;
using expressForm.Web.Controllers;
using expressForm.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace expressForm.Web.Test
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
