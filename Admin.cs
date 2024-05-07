using QuanLyNhaHang.DAO;
using QuanLyNhaHang.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLyNhaHang
{
    public partial class Admin : Form
    {
        BindingSource foodList = new BindingSource();
        BindingSource categoryList = new BindingSource();
        BindingSource tableList = new BindingSource();
        BindingSource accountList = new BindingSource();

        public Account loginAccount;
        public Admin()
        {
            InitializeComponent();

            LoadFunction();
        }        

        #region methods

        List<Food> SearchFoodByName(string name)
        {
            List<Food> listFood = FoodDAO.Instance.SearchFoodByName(name);

            return listFood;
        }

        void LoadFunction()
        {
            dtgvFood.DataSource = foodList;
            dtgvCategory.DataSource = categoryList;
            dtgvTable.DataSource = tableList;
            dtgvAccount.DataSource = accountList;

            LoadDateTimePickerBill();
            LoadListBillByDate(dtpFromDate.Value, dtpToDate.Value);
            LoadListFood();
            LoadListCategory();
            LoadListTable();
            LoadListAccount();
            LoadCategoryIntoCombobox(cbFoodCategory);
            LoadTableStatusIntoCombobox(cbTableStatus);
            AddFoodBiding();
            AddCategoryBiding();
            AddTableBiding();
            AddAccountBiding();
        }

        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpToDate.Value = dtpFromDate.Value.AddMonths(1).AddDays(-1);
        }
        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetBillListByDate(checkIn, checkOut);
        }

        void AddFoodBiding()
        {
            txtFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txtFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "ID", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }

        void AddCategoryBiding()
        {
            txtCategoryID.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtCategoryName.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "Name", true, DataSourceUpdateMode.Never));
        }

        void AddTableBiding()
        {
            txtTableID.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtTableName.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "Name", true, DataSourceUpdateMode.Never));
        }

        void AddAccountBiding()
        {
            txtUserName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            txtDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            nmAccountType.DataBindings.Add(new Binding("Value", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }

        void LoadTableStatusIntoCombobox(System.Windows.Forms.ComboBox cb)
        {
            cbTableStatus.DataSource = TableDAO.Instance.GetLisTable();
            cb.DisplayMember = "Status";
        }

        void LoadCategoryIntoCombobox(System.Windows.Forms.ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }

        void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();
        }

        void LoadListCategory()
        {
            categoryList.DataSource = CategoryDAO.Instance.GetListCategory();
        }

        void LoadListTable()
        {
            tableList.DataSource = TableDAO.Instance.GetLisTable();
        }

        void LoadListAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();
        }

        void AddAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.InsertAccount(userName, displayName, type))
            {
                MessageBox.Show("Thêm tài khoản thành công!", "Thông báo");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại!", "Thông báo");
            }

            LoadListAccount();
        }

        void EditAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.UpdateAccount(userName, displayName, type))
            {
                MessageBox.Show("Cập nhật tài khoản thành công!", "Thông báo");
            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản thất bại!", "Thông báo");
            }

            LoadListAccount();
        }

        void DeleteAccount(string userName)
        {
            if (loginAccount.UserName.Equals(userName))
            {
                MessageBox.Show("Không thể xóa tài khoản đang đăng nhập!", "Thông báo");
                return;
            }
            if (AccountDAO.Instance.DeleteAccount(userName))
            {
                MessageBox.Show("Xóa tài khoản thành công!", "Thông báo");
            }
            else
            {
                MessageBox.Show("Xóa tài khoản thất bại!", "Thông báo");
            }

            LoadListAccount();
        }

        void ResetPass(string userName)
        {
            if (AccountDAO.Instance.ResetPasswor(userName))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công!", "Thông báo");
            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại!", "Thông báo");
            }
        }

        #endregion

        #region events

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string displayName = txtDisplayName.Text;
            int type = (int)nmAccountType.Value;

            AddAccount(userName, displayName, type );
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;            

            DeleteAccount(userName);
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string displayName = txtDisplayName.Text;
            int type = (int)nmAccountType.Value;

            EditAccount(userName, displayName, type);
        }

        private void btnResetPassWord_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;

            ResetPass(userName);
        }

        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            foodList.DataSource = SearchFoodByName(txtSearchFoodName.Text);
        }

        private void txtFoodID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgvFood.SelectedCells.Count > 0)
                {
                    int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;

                    Category category = CategoryDAO.Instance.GetCategoryByID(id);

                    cbFoodCategory.SelectedItem = category;

                    int index = -1;
                    int i = 0;
                    foreach (Category item in cbFoodCategory.Items)
                    {
                        if (item.ID == category.ID)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }

                    cbFoodCategory.SelectedIndex = index;
                }
            }
            catch { }
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txtFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;

            if (FoodDAO.Instance.InsertFood(name, categoryID, price))
            {
                MessageBox.Show("Thêm món ăn thành công!", "Thông báo");
                LoadListFood();
                if (insertFood != null)
                {
                    insertFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Không thể thêm món ăn!", "Thông báo");
            }
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            string name = txtFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;
            int id = Convert.ToInt32(txtFoodID.Text);

            if (FoodDAO.Instance.UpdateFood(id, name, categoryID, price))
            {
                MessageBox.Show("Sửa món ăn thành công!", "Thông báo");
                LoadListFood();
                if (updateFood != null)
                {
                    updateFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Không thể sửa món ăn!", "Thông báo");
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtFoodID.Text);

            if (FoodDAO.Instance.DeleteFood(id))
            {
                MessageBox.Show("Xóa món ăn thành công!", "Thông báo");
                LoadListFood();
                if (deleteFood != null)
                {
                    deleteFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Không thể xóa món ăn!", "Thông báo");
            }
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = txtCategoryName.Text;

            if (CategoryDAO.Instance.InsertCategory(name))
            {
                MessageBox.Show("Thêm danh mục thành công!", "Thông báo");
                LoadListCategory();
            }
            else
            {
                MessageBox.Show("Không thể thêm danh mục!", "Thông báo");
            }
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            string name = txtCategoryName.Text;
            int id = Convert.ToInt32(txtCategoryID.Text);

            if (CategoryDAO.Instance.UpdateCategory(id, name))
            {
                MessageBox.Show("Sửa danh mục thành công!", "Thông báo");
                LoadListCategory();
            }
            else
            {
                MessageBox.Show("Không thể Sửa danh mục!", "Thông báo");
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtCategoryID.Text);

            if (CategoryDAO.Instance.DeleteCategory(id))
            {
                MessageBox.Show("Xóa danh mục thành công!", "Thông báo");
                LoadListCategory();
            }
            else
            {
                MessageBox.Show("Không thể xóa danh mục!", "Thông báo");
            }
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            string name = txtTableName.Text;
            string status = (cbTableStatus.SelectedItem as Table).Status.ToString();

            if (TableDAO.Instance.InsertTable(name, status))
            {
                MessageBox.Show("Thêm bàn thành công!", "Thông báo");
                LoadListTable();                
            }
            else
            {
                MessageBox.Show("Không thể thêm bàn!", "Thông báo");
            }
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            string name = txtTableName.Text;
            string status = (cbTableStatus.SelectedItem as Table).Status.ToString();
            int id = Convert.ToInt32(txtTableID.Text);

            if (TableDAO.Instance.UpdateTable(id, name, status))
            {
                MessageBox.Show("Sửa bàn thành công!", "Thông báo");
                LoadListTable();                
            }
            else
            {
                MessageBox.Show("Không thể sửa bàn!", "Thông báo");
            }
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtTableID.Text);

            if (TableDAO.Instance.DeleteTable(id))
            {
                MessageBox.Show("Xóa bàn thành công!", "Thông báo");
                LoadListTable();
            }
            else
            {
                MessageBox.Show("Không thể xóa bàn!", "Thông báo");
            }
        }
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpFromDate.Value, dtpToDate.Value);
        }

        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadListCategory();
        }

        private void btnShowTable_Click(object sender, EventArgs e)
        {
            LoadListTable();
        }

        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadListAccount();
        }

        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }

        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }

        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }



        #endregion

        
    }
}
