using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChineseSchool.BusinessObjects
{
    public class CSConstants
    {
        public static DateTime EMPTYDATE = DateTime.Parse("1/1/1900");
        public static int CURRENTSCHOOLYEAR = 2015;

        public enum Genders
        {
            None = 0,
            Male = 1,
            Female = 2
        }

        public enum UserTypes
        {
            None = 0,
            Admin = 1,
            User = 2
        }

        public enum EditMode
        {
            New = 1,
            Edit = 2
        }
    }
}