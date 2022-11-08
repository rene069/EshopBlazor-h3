using Datalayer;
using Eshop.Areas.Admin.Pages;
using Eshop.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.Interface;
using ServiceLayer.Repository;
using System;


namespace UnittestWeb
{
    public class UnitTest1
    {


        [Fact]
        public void Test1()
        {

            //arrange
            var pageModel = new IndexModel();

            //act
            pageModel.ModelState.AddModelError("Error", "Sample error description");
            var result = pageModel.OnPostMultiplyNumbers();

            //assert
            Assert.IsType<BadRequestResult>(result);

        }
    }
}