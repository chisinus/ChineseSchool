using AjaxControlToolkit;
using ChineseSchool.BusinessLogic;
using ChineseSchool.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ChineseSchool
{
    /// <summary>
    /// Summary description for CSService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class CSService : System.Web.Services.WebService
    {
        [WebMethod]
        public CascadingDropDownNameValue[] GetGrades(string knownCategoryValues)
        {
            List<GradeData> grades = CSAgent.GetGrades();
            if (grades == null) return null;

            List<CascadingDropDownNameValue> ret = new List<CascadingDropDownNameValue>();
            foreach (GradeData grade in grades)
            {
                ret.Add(new CascadingDropDownNameValue(grade.GradeName, grade.GradeID.ToString()));
            }

            return ret.ToArray();
        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetTeachersByGrade(string knownCategoryValues)
        {
            int gradeID = Toolbox.StringToInt(CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)["Grade"]);
            if (gradeID <= 0) return null;

            DataTable dt = CSAgent.GetTeachersByGrade(gradeID);
            if (dt == null) return null;

            List<CascadingDropDownNameValue> teachers = new List<CascadingDropDownNameValue>();
            foreach (DataRow row in dt.Rows)
            {
                teachers.Add(new CascadingDropDownNameValue((string)row["TeacherName_C"] + "  " + (string)row["TeacherName_E"], row["TeacherID"].ToString()));
            }

            return teachers.ToArray();
        }
    }
}
