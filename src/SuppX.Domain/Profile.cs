﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuppX.Domain
{
    public class Profile
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public string AvatarURL { get; set; }
        public string Name { get; set; }
        public string Surname{ get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }

    }
}
