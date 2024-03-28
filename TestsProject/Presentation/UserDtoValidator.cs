using BuisnnesService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerAPI.Validators;

namespace TestsProject.Presentation
{
    public class UserDtoValidator
    {
        [Test]
        public void Validate1()
        {
            var validator = new TaskTrackerAPI.Validators.UserDtoValidator();
            var entity = new UserDto() {
                Email = "artem2.ru",
                Password = "fffffff",
                FullName = "ijl"
            };
            var result = validator.Validate(entity);
            Assert.IsFalse(result.IsValid);
        }
        [Test]
        public void Validate2()
        {
            var validator = new TaskTrackerAPI.Validators.UserDtoValidator();
            var entity = new UserDto()
            {
                Email = "artem@.ru",
                Password = "fFffffff",
                FullName = "ijl"
            };
            var result = validator.Validate(entity);
            Assert.IsTrue(result.IsValid);
        }
    }
}
