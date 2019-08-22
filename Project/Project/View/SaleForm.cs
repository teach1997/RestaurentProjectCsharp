using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project
{
    public partial class SaleForm : Form
    {
        int curEmployee;
        List<ProductInOrder> prosInOr;
        public SaleForm()
        {
            InitializeComponent();

        }
        public SaleForm(int curEmployee)
        {
            InitializeComponent();
            prosInOr = new List<ProductInOrder>();
            this.curEmployee = curEmployee;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int xlocation = 5;
            timeShow.Start();
            Employee emp = Employee.getEmployeeByID(curEmployee);
            lbEmpName.Text = emp.Name;

            //tao phan lua chon do mua
            DataTable dt = DataAccess.getAllCategories();
            int count = 0;
            foreach (DataRow dr in dt.Rows)
            {
                Button x = new Button();
                x.Font = new Font("Microsoft Sans Serif", 12f);
                x.Text = dr["CategoryName"].ToString();
                x.Size = new Size(100, 90);
                x.Top = count * 100 + 5;
                x.Visible = true;
                x.BackColor = Color.Honeydew;
                x.Location = new System.Drawing.Point(xlocation, 10);
                panelCate.Controls.Add(x);
                count++;
                xlocation = xlocation + 140;
                x.Click += categoryChange;
            }
            loadProductOfCate(dt.Rows[0]["CategoryName"].ToString());
            //Add column for gridviews
            dgvOrders.AutoGenerateColumns = false;
            dgvOrders.Columns.Add("pid", "ID");
            dgvOrders.Columns["pid"].Width = 40;
            dgvOrders.Columns["pid"].DataPropertyName = "ProductID";
            dgvOrders.Columns["pid"].Visible = false;

            dgvOrders.Columns.Add("pName", "Name");
            dgvOrders.Columns["pName"].Width = 150;
            dgvOrders.Columns["pName"].DataPropertyName = "ProductName";

            dgvOrders.Columns.Add("pSize", "Size");
            dgvOrders.Columns["pSize"].Width = 40;
            dgvOrders.Columns["pSize"].DataPropertyName = "Size";

            dgvOrders.Columns.Add("price", "Price");
            dgvOrders.Columns["price"].Width = 70;
            dgvOrders.Columns["price"].DataPropertyName = "Price";

            dgvOrders.Columns.Add("pQuant", "Quantity");
            dgvOrders.Columns["pQuant"].Width = 70;
            dgvOrders.Columns["pQuant"].DataPropertyName = "Quantity";
            /*
            DataGridViewButtonColumn addCol = new DataGridViewButtonColumn();
            addCol.Name = " ";
            addCol.Text = "+";
            addCol.Width = 29;
            addCol.DefaultCellStyle.BackColor = Color.Green;
            addCol.UseColumnTextForButtonValue = true;
            dgvOrders.Columns.Add(addCol);
            */
            DataGridViewButtonColumn subCol = new DataGridViewButtonColumn();
            subCol.Name = " ";
            subCol.Text = "-";
            subCol.Width = 29;
            subCol.DefaultCellStyle.BackColor = Color.Yellow;
            subCol.UseColumnTextForButtonValue = true;
            dgvOrders.Columns.Add(subCol);

            dgvOrders.Columns.Add("cost", "Cost");
            dgvOrders.Columns["cost"].DataPropertyName = "Cost";
            /*
            DataGridViewButtonColumn delCol = new DataGridViewButtonColumn();
            delCol.Name = " ";
            delCol.Text = "X";
            delCol.Width = 30;
            delCol.DefaultCellStyle.BackColor = Color.Red;
            delCol.UseColumnTextForButtonValue = true;
            dgvOrders.Columns.Add(delCol);
            */
        }

        public void loadProductOfCate(string cateName)
        {
            lvPro.Items.Clear();
            DataTable dt = DataAccess.getAllProductByCateName(cateName);
            ImageList imgs = new ImageList();
            imgs.ImageSize = new Size(100, 100);
            lvPro.TileSize = new Size(230, 130);
            foreach (DataRow dr in dt.Rows)
            {
                string pic = dr["Picture"].ToString();
                try
                {
                    imgs.Images.Add(Image.FromFile(pic));
                }
                catch (Exception ex)
                {
                    imgs.Images.Add(Image.FromFile("Product_Picture/default.jpg"));
                }


            }
            lvPro.LargeImageList = imgs;
            int count = 0;
            foreach (DataRow dr in dt.Rows)
            {
                lvPro.Items.Add(dr["ProductName"].ToString(), count);
                string[] price = dr["Price"].ToString().Split('.');
                lvPro.Items[count].SubItems.Add("Price: " + price[0]);
                lvPro.Items[count].SubItems.Add("Size: " + dr["Size"].ToString());
                lvPro.Items[count].SubItems.Add(dr["ProductID"].ToString());
                lvPro.Items[count].SubItems.Add(price[0]);
                lvPro.Items[count].SubItems.Add(dr["Size"].ToString());
                count++;
            }
        }
        public void categoryChange(object sender, EventArgs e)
        {
            Button x = (Button)sender;
            loadProductOfCate(x.Text.Trim());
        }

        private void lvPro_MouseClick(object sender, MouseEventArgs e)
        {
            string idStr = lvPro.SelectedItems[0].SubItems[3].Text;
            int id = Convert.ToInt32(idStr);
            string priceStr = lvPro.SelectedItems[0].SubItems[4].Text;
            double price = Convert.ToDouble(priceStr);
            string size = lvPro.SelectedItems[0].SubItems[5].Text;
            string name = lvPro.SelectedItems[0].SubItems[0].Text;
            addNewProToList(new ProductInOrder(id, name, price, size, 1));
            dgvOrders.DataSource = null;
            dgvOrders.DataSource = prosInOr;
        }
        public void addNewProToList(ProductInOrder pIO)
        {
            bool duplicate = false;
            foreach (ProductInOrder p in prosInOr)
            {
                if (p.ProductID == pIO.ProductID)
                {
                    p.Quantity++;
                    p.Cost = p.Quantity * p.Price;
                    duplicate = true;
                }
            }
            if (!duplicate) prosInOr.Add(pIO);
            calculateCost();
        }
        public void calculateCost()
        {
            double sum = 0;
            foreach (ProductInOrder p in prosInOr)
            {
                sum += p.Cost;
            }
            string sumStr = sum.ToString();
            string disSum = "";
            int i;
            for (i = sumStr.Length - 1; i > 2; i = i - 3)
            {
                if (i == sumStr.Length - 1)
                {
                    disSum += sumStr.Substring(i - 2, 3);
                }
                else
                {
                    disSum = sumStr.Substring(i - 2, 3) + "." + disSum;
                }

            }
            disSum = sumStr.Substring(0, i + 1) + "." + disSum;
            lbTotal.Text = disSum + " VNĐ";
        }

        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                prosInOr.RemoveAt(e.RowIndex);
                calculateCost();
                dgvOrders.DataSource = null;
                dgvOrders.DataSource = prosInOr;
            }
            if (e.ColumnIndex == 5)
            {
                if (prosInOr[e.RowIndex].Quantity > 0)
                {
                    prosInOr[e.RowIndex].Quantity--;
                    prosInOr[e.RowIndex].Cost = prosInOr[e.RowIndex].Quantity * prosInOr[e.RowIndex].Price;
                    if (prosInOr[e.RowIndex].Quantity == 0) prosInOr.RemoveAt(e.RowIndex);
                }
                calculateCost();
                dgvOrders.DataSource = null;
                dgvOrders.DataSource = prosInOr;
            }
            /*
            if (e.ColumnIndex == 5)
            {
                prosInOr[e.RowIndex].Quantity++;
                prosInOr[e.RowIndex].Cost = prosInOr[e.RowIndex].Quantity * prosInOr[e.RowIndex].Price;
                calculateCost();
                dgvOrders.DataSource = null;
                dgvOrders.DataSource = prosInOr;
            }
            */
        }

        private void btnChout_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Now;
            DataAccess.addNewOrder(curEmployee, today);
            int addedOId = DataAccess.getMaxOrderId();
            foreach (ProductInOrder p in prosInOr)
            {
                DataAccess.addNewOrderDetail(addedOId, p.ProductID, p.Quantity);
            }
            if (prosInOr.Count <= 0)
            {
                MessageBox.Show("There is no order. You cannot checkout!");
            }
            else
            {
                prosInOr.RemoveRange(0, prosInOr.Count);
                dgvOrders.DataSource = null;
                dgvOrders.DataSource = prosInOr;

                formOrderDetail fOd = new formOrderDetail(addedOId);
                fOd.Show();
            }

        }

        private void ViewInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Profile prf = new Profile(curEmployee);
            prf.Show();
        }

        private void LogOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login lg = new Login();
            this.Close();
            lg.Show();
        }

        private void LvPro_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
