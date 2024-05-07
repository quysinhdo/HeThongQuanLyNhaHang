using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DTO
{
    public class Bill
    {
        private int id;
        private DateTime? dateCheckIn;
        private DateTime? dateCheckOut;
        private int status;
        private int discount;

        public int Id { get => id; set => id = value; }
        public DateTime? DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public DateTime? DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        public int Status { get => status; set => status = value; }
        public int Discount { get => discount; set => discount = value; }

        public Bill(int id, DateTime? dateCheckIn, DateTime? dateCheckOut, int status, int discount = 0)
        {
            this.Id = id;
            this.DateCheckIn = dateCheckIn;
            this.DateCheckOut = dateCheckOut;
            this.Status = status;
            this.Discount = discount;
        }

        public Bill(DataRow row)
        {
            this.Id = (int)row["id"];
            this.DateCheckIn = (DateTime?)row["dateCheckIn"];

            var dataCheckOutTemp = row["dateCheckOut"];
            if (dataCheckOutTemp.ToString() != "")
                this.DateCheckOut = (DateTime?)dataCheckOutTemp;

            this.Status = (int)row["status"];
            if (row["discount"].ToString() != "")
                this.Discount = (int)row["discount"];
        }
    }
}
