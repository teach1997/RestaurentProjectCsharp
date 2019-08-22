using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Project
{
    public class DataAccess
    {
        public static SqlConnection getConnection() //tao ket noi voi csdl
        {
            String conString = ConfigurationManager.ConnectionStrings["CSMSConnectionString"].ToString();
            SqlConnection myConnection = new SqlConnection(conString);
            myConnection.Open();
            return myConnection;
        }

        public static DataTable getDataUsingSql(string sql)
        {
            SqlCommand myCommand = new SqlCommand(sql, getConnection());
            SqlDataAdapter adapt = new SqlDataAdapter();
            adapt.SelectCommand = myCommand;
            DataSet ds = new DataSet();
            adapt.Fill(ds);
            return ds.Tables[0];
        }

        public static void executeSql(string sql)
        {
            SqlCommand command = new SqlCommand(sql, getConnection());
            command.Connection.Close();
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
        }

        public static DataTable getEmployeeByUsername(string username)
        {
            string sql = "select * from Employee e, Account a " +
                "where e.EmployeeID = a.EmployeeID and a.Username = '" + username + "'";
            return getDataUsingSql(sql);
        }

        public static DataTable getEmployeeByID(int cid)
        {
            string sql = "select * from Employee e, Account a where e.EmployeeID = a.EmployeeID and e.EmployeeID = " + cid;
            return getDataUsingSql(sql);
        }

        public static DataTable getAllEmployee()
        {
            string sql = "select * from Employee e, Account a " +
                "where e.EmployeeID = a.EmployeeID and a.[Role] != 'OUT' order by e.EmployeeID";
            return getDataUsingSql(sql);
        }




        public static int getMaxEmployeeID()
        {
            DataTable dt = getDataUsingSql("select Max(EmployeeID)'MaxID' from Employee");
            return Convert.ToInt32(dt.Rows[0]["MaxID"]);
        }

        public static void AddEmployee(string name, string address , string photo, int phone, string gender)
        {
            SqlCommand com = new SqlCommand();
            com.Connection = getConnection();
            string sql = "insert into Employee values(@name,@address,@photo,@phone,@gender)";
            com.CommandText = sql;
            com.Parameters.Add("@name", SqlDbType.NVarChar);
            com.Parameters["@name"].Value = name;
            com.Parameters.Add("@address", SqlDbType.NVarChar);
            com.Parameters["@address"].Value = address;
            com.Parameters.Add("@photo", SqlDbType.VarChar);
            com.Parameters["@photo"].Value = photo;
            com.Parameters.Add("@phone", SqlDbType.Int);
            com.Parameters["@phone"].Value = phone;
            com.Parameters.Add("@gender", SqlDbType.NChar);
            com.Parameters["@gender"].Value = gender;

            com.ExecuteNonQuery();
            com.Connection.Close();

        }

        public static void addAccount(string username, string pass, int eid)
        {
            SqlCommand com = new SqlCommand();
            com.Connection = getConnection();
            string sql = "insert into Account values(@username,@pass,@eid)";
            com.CommandText = sql;
            com.Parameters.Add("@username", SqlDbType.NVarChar);
            com.Parameters["@username"].Value = username;
            com.Parameters.Add("@pass", SqlDbType.NVarChar);
            com.Parameters["@pass"].Value = pass;
            
            com.Parameters.Add("@eid", SqlDbType.Int);
            com.Parameters["@eid"].Value = eid;

            com.ExecuteNonQuery();
            com.Connection.Close();
        }

    public static void AddFullEmpoyee(string name, string address, string photo, int phone, string gender, string username, string pass)
        {
            AddEmployee(name, address, photo, phone, gender);
            int eid = getMaxEmployeeID();
            addAccount(username, pass, eid);
        }

        public static bool isExistUsername(string us)
        {
            DataTable dt = getDataUsingSql("select * from Account where Username in ('"+us+"')");
            if(dt.Rows.Count == 0)
            {
                return false;
            }
            return true;
        }

        public static void DeleteEmployee(int eid)
        {
            executeSql("update Account set [Role] = 'OUT' where EmployeeID = " + eid);
        }

        public static DataTable getAllCategories()
        {
            string sql = "SELECT * FROM Category";
            return getDataUsingSql(sql);
        }
        public static DataTable getAllProductByCateName(string cateName)
        {
            string sql = "SELECT Product.*, Category.CategoryName FROM Category INNER JOIN Product ON Category.CategoryID = Product.CategoryID WHERE Category.CategoryName ='" + cateName + "'";
            return getDataUsingSql(sql);
        }

        public static int getMaxOrderId()
        {
            DataTable dt = getDataUsingSql("SELECT MAX(OrderID) as 'MaxOrder' FROM [Order]");
            int max = Convert.ToInt32(dt.Rows[0]["MaxOrder"].ToString());
            return max;
        }
        public static void addNewOrder(int eID, DateTime OrderDate)
        {
            string sql = "INSERT INTO [Order] VALUES(@eid,@date)";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = getConnection();
            cmd.CommandText = sql;
            cmd.Parameters.Add("@eid", SqlDbType.Int);
            cmd.Parameters["@eid"].Value = eID;
            cmd.Parameters.Add("@date", SqlDbType.DateTime);
            cmd.Parameters["@date"].Value = OrderDate;
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        public static DataTable getEmployee(string user, string pass)
        {
            string sql = "Select * from Employee e, Account a Where a.EmployeeID = e.EmployeeID and a.Username ='" + user + "' AND a.Password = '" + pass + "'";
            return getDataUsingSql(sql);
        }
        public static void addNewOrderDetail(int oID, int pID, int quant)
        {
            string sql = "INSERT INTO [OrderDetail] VALUES(@oid,@pid,@quant)";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = getConnection();
            cmd.CommandText = sql;
            cmd.Parameters.Add("@pid", SqlDbType.Int);
            cmd.Parameters["@pid"].Value = pID;
            cmd.Parameters.Add("@oid", SqlDbType.Int);
            cmd.Parameters["@oid"].Value = oID;
            cmd.Parameters.Add("@quant", SqlDbType.Int);
            cmd.Parameters["@quant"].Value = quant;
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }


        public static int getCateIdByName(string cateName)
        {
            string sql = "SELECT CategoryID,CategoryName From Category WHERE CategoryName= '" + cateName + "' ";
            DataTable dbt = getDataUsingSql(sql);
            int cateId = 1;
            DataRow dr = dbt.Rows[0];
            cateId = Convert.ToInt32(dr["CategoryID"]);
            return cateId;
        }
       
        public static DataTable getAllCateGory()
        {
            string sql = "SELECT * From Category";
            return getDataUsingSql(sql);
        }
        public static DataTable getOrderDetail(int orderId)
        {
            string sql = "SELECT * FROM Employee INNER JOIN"
              + " [Order] ON Employee.EmployeeID = [Order].EmployeeID INNER JOIN"
                        + " OrderDetail ON[Order].OrderID = OrderDetail.OrderID INNER JOIN"
                        + " Product ON OrderDetail.ProductID = Product.ProductID"
                        + " WHERE[Order].OrderID =" + orderId;
            DataTable dt = getDataUsingSql(sql);
            dt.Columns.Add("Total", typeof(double));
            foreach (DataRow row in dt.Rows)
            {
                //need to set value to NewColumn column
                row["Total"] = Convert.ToDouble(row["Price"].ToString()) * Convert.ToDouble(row["Quantity"].ToString());   // or set it to some other value
            }
            return dt;
        }

    }
}
