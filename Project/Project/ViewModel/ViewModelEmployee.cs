using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Project.ViewModel
{
    class ViewModelEmployee : Employee
    {
        public ViewModelEmployee()
        {

        }
        /*
        public static Employee getEmployeeByUsername(string username)
        {
            DataTable dt = DataAccess.getEmployeeByUsername(username);
            DataRow dr = dt.Rows[0];
            Employee e = new Employee();
            e.EmployeeID = Convert.ToInt32(dr["EmployeeID"]);
            e.Name = dr["Name"].ToString();
            e.Address = dr["Address"].ToString();
            e.BirthDate = Convert.ToDateTime(dr["BirthDate"]);
            e.Avatar = dr["Photo"].ToString();
            e.Phone = Convert.ToInt32(dr["Phone"]);
            e.Salary = Convert.ToDouble(dr["Salary"]);
            e.DateOfJoin = Convert.ToDateTime(dr["DateOfJoin"]);
            e.Gender = dr["Gender"].ToString();
            e.UserName = dr["Username"].ToString();
            e.Password = dr["Password"].ToString();
            e.Role = dr["Role"].ToString();

            return e;
        }
       
        public static List<Employee> getAllEmployee()
        {
            DataTable dt = DataAccess.getAllEmployee();
            List<Employee> le = new List<Employee>();
            foreach (DataRow dr in dt.Rows)
            {
                Employee e = new Employee();
                e.EmployeeID = Convert.ToInt32(dr["EmployeeID"]);
                e.Name = dr["Name"].ToString();
                e.Address = dr["Address"].ToString();
                e.BirthDate = Convert.ToDateTime(dr["BirthDate"]);
                e.Avatar = dr["Photo"].ToString();
                e.Phone = Convert.ToInt32(dr["Phone"]);
                e.Salary = Convert.ToDouble(dr["Salary"]);
                e.DateOfJoin = Convert.ToDateTime(dr["DateOfJoin"]);
                e.Gender = dr["Gender"].ToString();
                e.UserName = dr["Username"].ToString();
                e.Password = dr["Password"].ToString();
                e.Role = dr["Role"].ToString();
                le.Add(e);
            }

            return le;
        }

        public static void UpdateEmployee(int eid, string name, string address, int phone,
            DateTime doj, string password)
        {
            DataAccess.UpdateEmployee(eid, name, address, phone, doj, password);
        }

        public static void UpdateAvatar(int eid, string path)
        {
            DataAccess.UpdataAvatar(eid, path);
        }

        public static void AddFullEmpolyee(string name, string address, DateTime bd, string photo, int phone,
            double salary, DateTime doj, string gender, string username, string pass, string role)
        {
            DataAccess.AddFullEmployee(name, address, bd, photo, phone, salary, doj, gender, username, pass, role);
        }

        public static bool isExistUsername(string us)
        {
            return DataAccess.isExistUsername(us);
        }

        public static Employee getEmployeeByID(int cid)
        {
            DataTable dt = DataAccess.getEmployeeByID(cid);
            DataRow dr = dt.Rows[0];
            Employee e = new Employee();
            e.EmployeeID = cid;
            e.Name = dr["Name"].ToString();
            e.Address = dr["Address"].ToString();
            e.BirthDate = Convert.ToDateTime(dr["BirthDate"]);
            e.Avatar = dr["Photo"].ToString();
            e.Phone = Convert.ToInt32(dr["Phone"]);
            e.Salary = Convert.ToDouble(dr["Salary"]);
            e.DateOfJoin = Convert.ToDateTime(dr["DateOfJoin"]);
            e.Gender = dr["Gender"].ToString();
            e.UserName = dr["Username"].ToString();
            e.Password = dr["Password"].ToString();
            e.Role = dr["Role"].ToString();

            return e;
        }
        /*
        public static void UpdateSalaryAndRole(int eid, double salary, string role)
        {
            DataAccess.UpdateSalaryAndRole(eid, salary, role);
        }
        */
        public static void DeleteEmployee(int eid)
        {
            DataAccess.DeleteEmployee(eid);
        }
    }
}
