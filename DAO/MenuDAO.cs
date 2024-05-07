using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;

        public static MenuDAO Instance
        {
            get { if (instance == null) instance  = new MenuDAO(); return instance; }
            private set { instance = value; }
        }

        private MenuDAO() { }

        public List<QuanLyNhaHang.DTO.Menu> GetListMenuByTable(int id)
        {
            List<QuanLyNhaHang.DTO.Menu> listMenu = new List<QuanLyNhaHang.DTO.Menu> ();

            string query = "select f.name, bi.count, f.price, f.price*bi.count totalPrice from dbo.Food f, dbo.BillInfo bi, dbo.Bill b where b.id = bi.idBill and bi.idFood = f.id and b.status = 0 and b.idTable = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                QuanLyNhaHang.DTO.Menu menu = new QuanLyNhaHang.DTO.Menu(item);
                listMenu.Add(menu);
            }

            return listMenu;
        }
    }
}
