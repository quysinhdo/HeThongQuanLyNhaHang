using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace QuanLyNhaHang.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance 
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }

        private AccountDAO() { }

        public bool isLogin(string username, string password)
        {
            string query = " select* from dbo.account where username = @username and password = @password ";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { username, password });
            return result.Rows.Count > 0;
        }

        public bool UpdateAccount(string userName, string displayName, string pass, string newPass)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("exec USP_UpdateAccount @userName , @displayName , @password , @newPassword ", new object[] {userName, displayName, pass, newPass});
            return result > 0;
        }

        public Account GetAccountByUserName(string userName)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from account where userName = '" + userName +"'");

            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }

        public DataTable GetListAccount()
        {
            return DataProvider.Instance.ExecuteQuery("select UserName, DisPlayName, type from Account");
        }

        public bool InsertAccount(string userName, string displayName, int type)
        {
            string query = string.Format("insert Account ( UserName, DisplayName, Type ) values ( N'{0}', N'{1}', {2} )", userName, displayName, type);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool UpdateAccount(string userName, string displayName, int type)
        {
            string query = string.Format("update Account set DisplayName = N'{1}', Type = {2} where UserName = N'{0}'", userName, displayName, type);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool DeleteAccount(string userName)
        {
            string query = string.Format("Delete Account where UserName = N'{0}'", userName);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool ResetPasswor(string userName)
        {
            string query = string.Format("update Account set password = N'0' where UserName = N'{0}'", userName);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
