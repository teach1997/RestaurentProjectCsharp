using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using Project.ViewModel;

namespace Project
{
    public partial class Profile : Form
    {
        Employee emp;
        public Profile()
        {
            InitializeComponent();
            
        }

        public Profile(int curEmployee)
        {
            InitializeComponent();
            emp = Employee.getEmployeeByID(curEmployee);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tbID.Text = emp.EmployeeID.ToString();
            tbName.Text = emp.Name.Trim();
            tbAddress.Text = emp.Address;
            string gender = emp.Gender;


            if (gender.Trim().Equals("Male"))
            {
                rbMale.Checked = true;
                rbFemale.Checked = false;
                rbFemale.Enabled = false;
            }
            else
            {
                rbFemale.Checked = true;
                rbMale.Checked = false;
                rbMale.Enabled = false;
            }
            tbPhone.Text = emp.Phone.ToString().Trim();
            tbUsername.Text = emp.UserName.Trim();
            tbPassword.Text = emp.Password.Trim();
            Image img = Image.FromFile(emp.Avatar);
            pbAvatar.Image = img;
        }

        private void lbEID_Click(object sender, EventArgs e)
        {

        }



        public bool checkString(string s, string mes)
        {
            if (s.Trim() == "")
            {
                MessageBox.Show(mes + " is not empty!");
                return false;
            }
            return true;
        }

        public bool checkPhone(String s)
        {
            int value = 0;
            if (!checkString(s, "Phone"))
            {
                return false;
            }
            try
            {
                value = Convert.ToInt32(s);
                if (value <= 10)
                {
                    throw new IndexOutOfRangeException();
                }
            }
            catch (IndexOutOfRangeException e)
            {
                MessageBox.Show("Phone number must be bigger than or equal to 10!");
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("Phone number is a integer!");
                return false;
            }

            return true;
        }

        public bool checkDouble(String s)
        {
            double value = 0;
            if (!checkString(s, "Salary"))
            {
                return false;
            }
            try
            {
                value = Convert.ToDouble(s);
                if (value < 0)
                {
                    throw new IndexOutOfRangeException();
                }
            }
            catch (IndexOutOfRangeException e)
            {
                MessageBox.Show("Salary is not smaller than 0!");
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("Salary is a number!");
                return false;
            }

            return true;
        }

       

        private void lbRole_Click(object sender, EventArgs e)
        {

        }

 



        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        private void btUpload_Click(object sender, EventArgs e)
        {
        }

        private void btBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BUpdateInfo_Click(object sender, EventArgs e)
        {

        }
    }
}
