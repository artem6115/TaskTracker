using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerUI.Services
{
    public static class UserValidator
    {
        public static bool PasswordValidator(string password,out string errorMassage)
        {
            bool result  = true;
            errorMassage = "";
            if(string.IsNullOrEmpty(password) || password.Length<5) {
                errorMassage += "Минимальная длинна пароля 5 символов\n";
                result= false;
            }
            if(password.ToLower() == password || password.ToUpper() == password)
            {
                result = false;
                errorMassage += "Пароль должен содержать буквы в верхнем и нижнем регистре";
            }
            return result;
        }
        public static bool EmailValidator(string email, out string errorMassage)
        {
            bool result = true;
            errorMassage = "";
            if (!email.Contains('@') || !email.Contains('.'))
            {
                errorMassage += "Почта не корректная\n";
                result = false;
            }

            return result;
        }
        public static bool FullNameValidator(string name, out string errorMassage)
        {
            bool result = true;
            errorMassage = "";
            if (name.Split(' ').Length<3)
            {
                errorMassage += "ФИО указано не полностью\n";
                result = false;
            }

            return result;
        }

    }
}
