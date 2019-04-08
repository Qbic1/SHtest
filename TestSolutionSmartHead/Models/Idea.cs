using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestSolutionSmartHead.Models
{
    public class Idea
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public int Positive { get; set; }
        public int Negative { get; set; }
        public string Text { get; set; }
        public int? UserId { get; set; }
        public bool Blocked { get; set; }
    }
}