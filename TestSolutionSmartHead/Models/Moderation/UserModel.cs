using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestSolutionSmartHead.Models.Enums;

namespace TestSolutionSmartHead.Models.Moderation
{
    public class UserModel
    {
        public string Name { get; set; }
        public UserType Role { get; set; }
    }
}