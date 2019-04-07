using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestSolutionSmartHead.Models.Enums
{
    public enum UserType : byte
    {
        /// <summary>
        /// Простой пользователь
        /// </summary>
        User = 0,
        /// <summary>
        /// Администратор
        /// </summary>
        Admin = 1

    }
}