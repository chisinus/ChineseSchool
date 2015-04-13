using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChineseSchool.BusinessObjects
{
    public class CSMessage
    {
        public const string ERR_Format_Invalid_Email = "Invalid Email Address Format.";
        public const string ERR_CompleteRequest = "Application Error: Cannot complete your request. Please try again later.";
        public const string ERR_Login_Failed = "Email address and password do not match";
        public const string ERR_RequiredField = "Your input is required.";
    }
}