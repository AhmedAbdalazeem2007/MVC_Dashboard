﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_DAL.Models
{
    public class SmsMessage
    {
        public string phoneNumber { get; set; }
        public string Body { get; set; }
    }
}
