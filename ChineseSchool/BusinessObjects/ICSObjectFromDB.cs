using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseSchool.BusinessObjects
{
    public interface ICSObjectFromDB
    {
        void GetDataFromDB(System.Data.SqlClient.SqlDataReader reader);
    }
}
