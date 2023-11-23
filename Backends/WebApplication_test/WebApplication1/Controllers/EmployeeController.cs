using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

//1. 创建数据库，数据表
//2. 用了mvc：先建立model；然后webconfig连接数据库，然后做一下调试的配置， controller

namespace WebApplication1.Controllers
{
    public class EmployeeController : ApiController
    {
        [Route("api/Employee/EmployeeInfo")]
        [HttpGet]
        public HttpResponseMessage EmployeeInfo()
        {
                string query = @"select * from EmployeeInfo";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["haha"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return Request.CreateResponse(HttpStatusCode.OK, table);

        }


        [Route("api/Employee/NewEmployee")]
        [HttpPost]
        public string newEmployee(Employee emp)
        {
            try
            {
                string query = @"insert into EmployeeInfo values('" + emp.ID + @"', '" +emp.FirstName + @"', '" + emp.LastName + @"', '" + emp.Email + @"', '" + emp.Tel + @"', '" + emp.DeptID +@"')";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["haha"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Addad Successfully!!";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [Route("api/Employee/DeleteEmployee")]
        [HttpDelete]
         public string deleteEmployee(String ID)
        {
            try
            {
                string query = @"DELETE FROM EmployeeInfo WHERE ID='" + ID + @"'";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["haha"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "delete sucessfully";
            }
            catch (Exception)
            {
                return "something went wrong";

            }

         }

        [Route("api/Employee/UpdateEmployee")]
        [HttpPost]
        public string updateEmployee(Employee emp)
        {
            try
                
            {
                //string query = @"update EmployeeInfo set FirstName='" + emp.FirstName + @"', LastName='" + emp.LastName + @"', Email='" + emp.Email + @"', Tel='" + emp.Tel + @"', DeptID='" + emp.DeptID + @"' WHERE ID='" + emp.ID + @"'";

                string query = @"update EmployeeInfo set ";
                if(emp.FirstName != "" && emp.FirstName != null) 
                {
                    query += @" FirstName= '" + emp.FirstName + @"',";
                }
                if (emp.LastName != "" && emp.LastName != null) 
                {
                    query += @" LastName= '" + emp.LastName + @"',";
                }
                if (emp.Email != "" && emp.Email != null) //''--空字符串， null--空
                {
                    query += @" Email= '" + emp.Email + @"',";
                }
                if (emp.Tel != "" && emp.Tel != null) 
                {
                    query += @" Tel= '" + emp.Tel + @"',";
                }
                if (emp.DeptID != "" && emp.DeptID != null)
                {
                    query += @" DeptID = '" + emp.DeptID + @"',";
                }
                query = query.Substring(0, query.Length - 1);
                query += @" where ID = '" + emp.ID + @"'";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["haha"].ConnectionString))
                using (var cmd = new SqlCommand(query,con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "change Successfully!!";

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

    }
}
