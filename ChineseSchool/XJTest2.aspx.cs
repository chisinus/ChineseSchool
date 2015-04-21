using ChineseSchool.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChineseSchool
{
    public partial class XJTest2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ctrlSubmit_Click(object sender, EventArgs e)
        {
            Toolbox.Logging("Test caller", "some message", new Exception("test exception"));
        }
    }
}