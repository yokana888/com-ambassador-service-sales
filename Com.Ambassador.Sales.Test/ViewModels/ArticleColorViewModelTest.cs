using Com.Ambassador.Service.Sales.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Ambassador.Sales.Test.ViewModels
{
   public class ArticleColorViewModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            ArticleColorViewModel viewModel = new ArticleColorViewModel()
            {
                Name = "Name",
                Description = "Description"
            };

            Assert.Equal("Name", viewModel.Name);
            Assert.Equal("Description", viewModel.Description);
        }

        [Fact]
        public void ValidateDefault()
        {
            ArticleColorViewModel viewModel = new ArticleColorViewModel();
            var result = viewModel.Validate(null);
            Assert.True(result.Count() > 0);
        }
    }
}
