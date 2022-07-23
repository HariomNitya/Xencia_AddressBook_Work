using hariDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace hariDemo.Repository
{
    public class DBrepository
    {
        private SqlConnection conn;

        private void Connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            conn = new SqlConnection(constr);
        }
        //Get All details
        public List<AddressBookModel> GetDetails()
        {
            Connection();
            List<AddressBookModel> EmpList = new List<AddressBookModel>();


            SqlCommand com = new SqlCommand("GetDetails", conn);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            conn.Open();
            da.Fill(dt);
            conn.Close();
            //Bind AddressBookModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                EmpList.Add(

                    new AddressBookModel
                    {

                        Id = Convert.ToInt32(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        City = Convert.ToString(dr["City"]),
                        Address = Convert.ToString(dr["Address"])

                    }
                    );
            }

            return EmpList;
        }

        //To Add details    
        public bool AddDetails(AddressBookModel obj)
        {
            Connection();
            SqlCommand com = new SqlCommand("AddDetails", conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Name", obj.Name);
            com.Parameters.AddWithValue("@City", obj.City);
            com.Parameters.AddWithValue("@Address", obj.Address);

            conn.Open();
            int i = com.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
            {

                return true;

            }
            else
            {

                return false;
            }


        }

        //To Update details    
        public bool UpdateDetails(AddressBookModel obj)
        {

            Connection();
            SqlCommand com = new SqlCommand("UpdateDetails", conn);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", obj.Id);
            com.Parameters.AddWithValue("@Name", obj.Name);
            com.Parameters.AddWithValue("@City", obj.City);
            com.Parameters.AddWithValue("@Address", obj.Address);
            conn.Open();
            int i = com.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        //To delete details    
        public bool DeleteDetails(int Id)
        {

            Connection();
            SqlCommand com = new SqlCommand("DeleteDetailById", conn);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", Id);

            conn.Open();
            int i = com.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {

                return false;
            }
        }
    }
}