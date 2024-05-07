using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;

        public static TableDAO Instance
        {
            get { if (instance == null) instance = new TableDAO(); return instance; }
            private set { instance = value; }
        }

        public static int TableWidth = 85;
        public static int TableHeight = 85;

        private TableDAO() { }

        public void SwitchTable(int id1, int id2)
        {
            DataProvider.Instance.ExecuteQuery("USP_SwitchTable @idTable1 , @idTable2", new object[] {id1, id2});
        }

        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();

            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetTableList");

            foreach(DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table);
            }

            return tableList;
        }

        public List<Table> GetLisTable()
        {
            List<QuanLyNhaHang.DTO.Table> list = new List<QuanLyNhaHang.DTO.Table>();

            string query = "select * from TableFood";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                QuanLyNhaHang.DTO.Table table = new QuanLyNhaHang.DTO.Table(item);
                list.Add(table);
            }

            return list;
        }

        public bool InsertTable(string name, string status)
        {
            string query = string.Format("insert TableFood ( name, status ) values ( N'{0}', N'{1}')", name, status);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool UpdateTable(int idTable, string name, string status)
        {
            string query = string.Format("update TableFood set name = N'{0}', status = N'{1}' where id = {2}", name, status, idTable);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool DeleteTable(int idTable)
        {
            string query = string.Format("delete TableFood where id = {0}", idTable);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
