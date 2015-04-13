using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseSchool.BusinessObjects
{
    public interface ICSObjectContainer
    {
        bool HasSameKeyValue(System.Data.SqlClient.SqlDataReader reader, string comparingFieldName);

        void AddItem(System.Data.SqlClient.SqlDataReader reader);
    }
}
