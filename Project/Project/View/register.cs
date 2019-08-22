using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.View
{
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }
        public void registercomponentnext()
        {
            lbusername.Hide();
            lbpassword.Hide();
            lbcfpassword.Hide();
            txtUsername.Hide();
            txtPassword.Hide();
            txtcfPassword.Hide();
            btnRegister.Text = "Register";
            btncancel.Text = "Back";
            lbName.Visible = true;
            lbAddress.Visible = true;
            lbGender.Visible = true;
            lbPhone.Visible = true;
            tbName.Visible = true;
            tbAddress.Visible = true;
            tbPhone.Visible = true;
            rbMale.Visible = true;
            rbFemale.Visible = true;
        }
        public void registercomponentback()
        {
            lbusername.Visible = true;
            lbpassword.Visible = true;
            lbcfpassword.Visible = true;
            txtUsername.Visible = true;
            txtPassword.Visible = true;
            txtcfPassword.Visible = true;
            btnRegister.Text = "Next";
            btncancel.Text = "Cancel";
            lbName.Visible = false;
            lbAddress.Visible = false;
            lbGender.Visible = false;
            lbPhone.Visible = false;
            tbName.Visible = false;
            tbAddress.Visible = false;
            tbPhone.Visible = false;
            rbMale.Visible = false;
            rbFemale.Visible = false;
        }
        bool flat = false;
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string cfpassword = txtcfPassword.Text;

            int id = DataAccess.getMaxEmployeeID() + 1;
            if (password.Equals(cfpassword) && password.Length > 0 && username.Length > 0 && flat == false)
            {
                flat = true;
                registercomponentnext();

            }
 
            else if (flat == true)
            {
                string path = "Avatar/default.png";
                string name = tbName.Text;
                int phone = 123;
                if(name.Length == 0) { MessageBox.Show("please input name!"); }
                string address = tbAddress.Text;
                if(address.Length == 0) { MessageBox.Show("please input address"); }
                string gender = "";
                if (rbMale.Checked)
                {
                    gender = "Male";
                }
                else if (rbFemale.Checked)
                {
                    gender = "Female";
                }
                else
                {
                    MessageBox.Show("please choose gender!");
                }
                try
                {
                   phone = Int32.Parse(tbPhone.Text);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Please input phone number!");
                    return;
                }
                
                DataAccess.AddFullEmpoyee(name, address, path, phone, gender, username, password);
                MessageBox.Show("Done!");
                this.Close();
            }
            else
            {
                MessageBox.Show("password and cf password be the same!!!");
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (flat == true)
            {
                flat = false;
                registercomponentback();
            }
            else this.Close();
        }
    }
}
