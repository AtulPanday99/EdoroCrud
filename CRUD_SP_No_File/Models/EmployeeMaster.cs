using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRUD_SP_No_File.Models
{
    public class EmployeeMaster
    {
        public int RId { get; set; }
     
        public string Name { get; set; }
 
        public string EmailId { get; set; }
     
        public string ContactNo { get; set; }
  
        public string Address { get; set; }
   
        public string State { get; set; }
  
        public string City { get; set; }

        public int PinCode { get; set; }
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Confirm Password required")]
        [Compare("Password", ErrorMessage = "Password doesn't match.")]
        public string ConfirmPassword { get; set; }

        string msg;
        SqlConnection con;
        SqlCommand cmd;
        public EmployeeMaster()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_employeeregistration";
        }
        //internal DataTable ExecuteQuery(string MyCommandText)
        //{
        //    SqlDataAdapter da = new SqlDataAdapter(MyCommandText, con);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    return dt;
        //}

        internal string AddEmployee()
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@name", Name);
            cmd.Parameters.AddWithValue("@emailid", EmailId);
            cmd.Parameters.AddWithValue("@contactno", ContactNo);
            cmd.Parameters.AddWithValue("@address", Address);
            cmd.Parameters.AddWithValue("@state", State);
            cmd.Parameters.AddWithValue("@city", City);
            cmd.Parameters.AddWithValue("@pincode", PinCode);
            cmd.Parameters.AddWithValue("@password", Password);
            cmd.Parameters.AddWithValue("@confirmpassword", ConfirmPassword);
            cmd.Parameters.AddWithValue("@op", 1);
            if (con.State == ConnectionState.Closed)
                con.Open();
            int n = cmd.ExecuteNonQuery();
            con.Close();
            if (n > 0)
                msg = "Record saved successfully.";
            else
                msg = "Sorry!unable to saved record.";
            return msg;
        }
        internal List<EmployeeMaster> GetAllEmployee()
        {
            cmd.Parameters.Clear();
            List<EmployeeMaster> lst = new List<EmployeeMaster>();
            cmd.Parameters.AddWithValue("@op", 2);
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                EmployeeMaster em = new EmployeeMaster();
                em.RId = dr.GetInt32(0);
                em.Name = dr.GetString(1);
                em.EmailId = dr.GetString(2);
                em.ContactNo = dr.GetString(3);
                em.Address = dr.GetString(4);
                em.State = dr.GetString(5);
                em.City = dr.GetString(6);
                em.PinCode = dr.GetInt32(7);
                lst.Add(em);
            }
            con.Close();
            return lst;
        }
        internal string DeleteEmployee(int RId)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("rid", RId);
            cmd.Parameters.AddWithValue("@op", 3);
            if (con.State == ConnectionState.Closed)
                con.Open();
            int n = cmd.ExecuteNonQuery();
            con.Close();
            if (n > 0)
                msg = "Record deleted successfully.";
            else
                msg = "Sorry!unable to delete record.";
            return msg;
        }
        internal EmployeeMaster GetSingleEmployee(int RId)
        {
            EmployeeMaster em = new EmployeeMaster();
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("rid", RId);
            cmd.Parameters.AddWithValue("@op", 4);
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                em.RId = dr.GetInt32(0);
                em.Name = dr.GetString(1);
                em.EmailId = dr.GetString(2);
                em.ContactNo = dr.GetString(3);
                em.Address = dr.GetString(4);
                em.State = dr.GetString(5);
                em.City = dr.GetString(6);
                em.PinCode = dr.GetInt32(7);
            }
            con.Close();
            return em;
        }
        internal string UpdateEmployee(int RId)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("rid", RId);
            cmd.Parameters.AddWithValue("@op", 5);
            cmd.Parameters.AddWithValue("@name", Name);
            cmd.Parameters.AddWithValue("@emailid", EmailId);
            cmd.Parameters.AddWithValue("@contactno", ContactNo);
            cmd.Parameters.AddWithValue("@address", Address);
            cmd.Parameters.AddWithValue("@state", State);
            cmd.Parameters.AddWithValue("@city", City);
            cmd.Parameters.AddWithValue("@pincode", PinCode);
            if (con.State == ConnectionState.Closed)
                con.Open();
            int n = cmd.ExecuteNonQuery();
            con.Close();
            if (n > 0)
                msg = "Record updated successfully.";
            else
                msg = "Sorry!unable to update record.";
            return msg;
        }
    }
}