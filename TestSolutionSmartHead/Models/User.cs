using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TestSolutionSmartHead.Models.Enums;
using BCrypt = BCrypt.Net.BCrypt;

namespace TestSolutionSmartHead.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        public string Password { get; set; }
        public byte Votes { get; set; }
        public DateTime? RefreshDate { get; set; }
        public UserType Role { get; set; }
        public virtual ICollection<Idea> Ideas { get; set; }
        public bool IsActive { get; set; }
        /// <summary>
        /// Хранит id идей за который пользователь голосовал
        /// </summary>
        public string VotesList { get; set; }
        public User(string name, string password)
        {
            this.RefreshDate = DateTime.Now;
            this.IsActive = true;
            this.Name = name;
            this.Password = global::BCrypt.Net.BCrypt.HashPassword(password, global::BCrypt.Net.BCrypt.GenerateSalt());
            this.Votes = 10;
            Ideas = new List<Idea>();
        }

        public User()
        {
            Ideas = new List<Idea>();
        }

        /// <summary>
        /// Проверка пароля
        /// </summary>
        /// <param name="comparedPassword">сравниваемый пароль</param>
        /// <returns></returns>
        public bool CheckPassword(string comparedPassword)
        {
            return global::BCrypt.Net.BCrypt.CheckPassword(comparedPassword, this.Password);
        }
    }
}