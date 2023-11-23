using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DepartmentController : ApiController
    {
        [Route("api/Department/DepartmentInfo")]
        [HttpGet]
        public HttpResponseMessage DepartmentInfo()
        {
            string query = @"select * from DepartmentInfo";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DeptConfig"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        [Route("api/Department/NewDepartment")]
        [HttpPost]
        public string newDepartment(Department dept)
        {
            try
            {
                string query = @"insert into DepartmentInfo values('" + dept.ID + @"', '" + dept.DeptName + @"', '" + dept.Supervisor + @"', '" + dept.Tel + @"', '" + dept.Email + @"','" + dept.Num + @"')";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DeptConfig"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Added successfully!";

            }
            catch (Exception)
            {
                return "something went wrong";
            }
        }

        [Route("api/Department/UpdateDepartment")]
        [HttpPost]
        public string updateDepartment(Department dept)
        {
            try
            {
                string query = @"update DepartmentInfo set ";
                if (dept.DeptName != "" && dept.DeptName != null)
                {
                    query += @" DeptName = '" + dept.DeptName + @"',";
                }
                if (dept.Supervisor != "" && dept.Supervisor != null)
                {
                    query += @" Supervisor = '" + dept.Supervisor + @"',";
                }
                if (dept.Tel != "" && dept.Tel != null)
                {
                    query += @" Tel = '" + dept.Tel + @"',";
                }
                if (dept.Email != "" && dept.Email != null)
                {
                    query += @" Email = '" + dept.Email + @"',";
                }
                if (dept.Num != "" && dept.Num != null)

                {
                    query += @" Num = '" + dept.Num + @"',";
                }

                query = query.Substring(0, query.Length - 1);
                query += @" WHERE ID = '" + dept.ID + @"'";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DeptConfig"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "update departmentinfo successfully";
            }
            catch (Exception)
            {
                return "update went wrong!";
            }
        }


        [Route("api/Department/DeleteDepartment")]
        [HttpDelete]
        public string deleteDepartment(String ID)
        {
            try
            {
                string query = @" DELETE FROM DepartmentInfo WHERE ID = '" + ID+ @"'";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DeptConfig"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "delete department successfully";

            }
            catch (Exception)
            {

                return "delete department failed";
            }
        }

    }

}


