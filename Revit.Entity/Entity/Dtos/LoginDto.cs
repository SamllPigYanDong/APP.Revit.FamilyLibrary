﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit.Entity.Entity.Dtos
{
    public class LoginDto : BaseDto
    {
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value;  }
        }

        private string account;

        public string Account
        {
            get { return account; }
            set { account = value;  }
        }

        private string passWord;

        public string PassWord
        {
            get { return passWord; }
            set { passWord = value;  }
        }
    }
}
