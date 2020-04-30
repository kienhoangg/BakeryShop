﻿using OnlineShop_Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop_Application.ViewModels
{
    public class AppUserViewModel
    {
       
            public AppUserViewModel()
            {
                Roles = new List<string>();
            }
            public Guid? Id { set; get; }
            public string FullName { set; get; }
            public string BirthDay { set; get; }
            public string Email { set; get; }
            public string Password { set; get; }
            public string UserName { set; get; }
            public string Address { get; set; }
            public string PhoneNumber { set; get; }
            public string Avatar { get; set; }
            public Status Status { get; set; }

            public string Gender { get; set; }

            public DateTime CreatedDate { get; set; }

            public List<string> Roles { get; set; }
        
    }
}
