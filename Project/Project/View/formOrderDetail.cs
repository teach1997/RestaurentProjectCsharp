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
    public partial class formOrderDetail : Form
    {
        int curOrderId;
        public formOrderDetail()
        {
            InitializeComponent();
        }
        public formOrderDetail(int curO)
        {
            InitializeComponent();
            curOrderId = curO;

        }

        private void formOrderDetail_Load(object sender, EventArgs e)
        {
            DataTable dt = DataAccess.getOrderDetail(curOrderId);
            labelInvoice.Text = dt.Rows[0]["OrderId"].ToString();
            labelServe.Text = dt.Rows[0]["Name"].ToString();
           // labelDate.Text = dt.Rows[0]["OrderDate"].ToString();

            dataGridView.AutoGenerateColumns = false;

            dataGridView.Columns.Add("Items", "ProductName");
            dataGridView.Columns["Items"].DataPropertyName = "ProductName";

            dataGridView.Columns.Add("Price", "Price");
            dataGridView.Columns["Price"].DataPropertyName = "Price";

            dataGridView.Columns.Add("Quantity", "Quantity");
            dataGridView.Columns["Quantity"].DataPropertyName = "Quantity";


            dataGridView.Columns.Add("Total", "Total");
            dataGridView.Columns["Total"].DataPropertyName = "Total";

            dataGridView.DataSource = dt;

            double total = 0;
            foreach(DataRow row in dt.Rows)
            {
               total += Convert.ToDouble(row["Total"].ToString());
            }
            labelTotal.Text = total.ToString();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        Bitmap bmp;
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bmp, 0, 0);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
