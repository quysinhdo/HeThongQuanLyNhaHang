using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;
        public static CategoryDAO Instance
        {
            get { if (instance == null) instance = new CategoryDAO() ; return instance; }
            private set { instance = value; }
        }

        private CategoryDAO() { }

        public List<QuanLyNhaHang.DTO.Category> GetListCategory()
        {
            List<QuanLyNhaHang.DTO.Category> list = new List<QuanLyNhaHang.DTO.Category>();

            string query = "select * from FoodCategory";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Category category = new Category(item);
                list.Add(category);
            }

            return list;
        }

        public Category GetCategoryByID(int id)
        {
            Category category = null;

            string query = "select * from FoodCategory where id = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                category = new Category(item);
                return category;
            }

            return category;
        }

        public bool InsertCategory(string name)
        {
            string query = string.Format("insert FoodCategory ( name ) values ( N'{0}' )", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool UpdateCategory(int idCategory, string name)
        {
            string query = string.Format("update FoodCategory set name = N'{0}' where id = {1}", name, idCategory);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool DeleteCategory(int idCategory)
        {
            string query = string.Format("delete FoodCategory where id = {0}", idCategory);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
