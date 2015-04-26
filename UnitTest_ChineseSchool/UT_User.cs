using System;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChineseSchool.BusinessLogic;
using System.Data.SqlClient;
using ChineseSchool.BusinessObjects;

namespace UnitTest_ChineseSchool
{
    [TestClass]
    public class UT_User
    {
        [TestMethod]
        public void GetUserByEmail_Normal()
        {
            //using (TransactionScope ts = new TransactionScope())
            //{
                SqlConnection conn = Toolbox.OpenConnection();

                UserData user = CreateUser(1, conn);
                //UserData user = BuildUserData(1);

                UserData newUser = CSAgent.GetUserByEmail(user.Email, conn);
                Assert.IsFalse(newUser.Error);
                Assert.IsTrue(newUser.Firstname == user.Firstname);
                Assert.IsNotNull(newUser.Children);
                Assert.IsTrue(newUser.Children.Count == user.Children.Count);

                Assert.IsNotNull(newUser.Children[0].PickedClasses);
                Assert.IsTrue(newUser.Children[0].PickedClasses.Count == user.Children[0].PickedClasses.Count);

                Toolbox.CloseConnection(conn);

            //    ts.Dispose();
            //}
        }

        [TestMethod]
        public void InsertUser_Normal()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                UserData user = BuildUserData(1);

                SqlConnection conn = Toolbox.OpenConnection();

                //int userID = CSAgent.InsertUser(user, conn);
                //Assert.IsTrue(userID > 0);

                Toolbox.CloseConnection(conn);

                ts.Dispose();
            }
        }

        [TestMethod]
        public void InsertChild_Normal()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                UserData user = BuildUserData(1);

                //SqlConnection conn = Toolbox.OpenConnection();

                //int userID = CSAgent.InsertUser(user, conn);
                //Assert.IsTrue(userID > 0);

                //SqlTransaction tran = conn.BeginTransaction();

                //foreach (ChildData child in user.Children)
                //{
                //    int childID = CSAgent.InsertChild(userID, child, conn, tran);
                //    Assert.IsTrue(childID > 0);
                //}

                //Toolbox.CloseConnection(conn, tran, true);

                ts.Dispose();
            }
        }

        [TestMethod]
        public void InsertChildClass_Normal()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                SqlConnection conn = Toolbox.OpenConnection();
                
                CreateUser(1, conn);

                Toolbox.CloseConnection(conn);

                ts.Dispose();
            }
        }

        private UserData BuildUserData(int number)
        {
            UserData user = new UserData();

            user.Password = "123456";
            user.Firstname = "UserFirstname" + number;
            user.Lastname = "Usrelastname" + number;
            user.Email = "aa@bb.com";
            user.Phone = "phone";
            user.Phone_Ext = "ext";
            user.Address1 = "address 1";
            user.Address2 = "address 2";
            user.City = "city";
            user.State = new StateData { StateID = 1 };
            user.PostalCode = "12345";

            user.Children.Add(BuildChildren(1));
            user.Children.Add(BuildChildren(2));
            user.Children.Add(BuildChildren(3));

            return user;
        }

        private ChildData BuildChildren(int number)
        {
            ChildData child = new ChildData();
            child.ChildFirstname = "ChildFirstname" + number;
            child.ChildLastname = "ChildLastname" + number;
            child.Gender = CSConstants.Genders.Male;
            child.YOB = "2008";

            child.PickedClasses.Add(BuildClass(1));
            child.PickedClasses.Add(BuildClass(2));

            return child;
        }

        private ClassData BuildClass(int number)
        {
            ClassData myClass = new ClassData();

            myClass.ClassID = 1 + number;
            myClass.ClassName = "Class" + number;
            myClass.Grade.GradeID = 1 + number;

            return myClass;
        }

        private UserData CreateUser(int number, SqlConnection conn)
        {
            UserData user = BuildUserData(number);

            //int userID = CSAgent.InsertUser(user, conn);
            //Assert.IsTrue(userID > 0);

            //SqlTransaction tran = conn.BeginTransaction();

            //foreach (ChildData child in user.Children)
            //{
            //    int childID = CSAgent.InsertChild(userID, child, conn, tran);
            //    Assert.IsTrue(childID > 0);

            //    foreach (ClassData myClass in child.PickedClasses)
            //    {
            //        int childClassID = CSAgent.InsertChildClass(childID, myClass.ClassID, conn, tran);
            //        Assert.IsTrue(childClassID > 0);
            //    }
            //}

            //Toolbox.EndTransaction(tran, true);

            return user;
        }
    }
}
