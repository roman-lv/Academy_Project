using System;
using Xunit;
using Project.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ProjectTests
{
    public class UnitTest1
    {
        [Fact]
        public void TestCoursesControllerIndex()
        {
            CoursesController controller = new CoursesController(null);
            Assert.True(controller.View() is IActionResult);
        }
        [Fact]
        public void TestStudentControllerIndex()
        {
            StudentsController controller = new StudentsController(null);
            Assert.True(controller.View() is IActionResult);
        }
    }
}
