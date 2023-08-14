using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using TestProjectAPI.Models;

namespace TestProjectAPI.Controllers
{

    public class StaffController : ApiController
    {



        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mycon2"].ConnectionString);

        SqlCommand cmd = null;
       

      
        public HttpResponseMessage Get()
        {

            try
            {
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter("Select * from Staff", con);

                DataTable dt = new DataTable();

                List<Staff> list = new List<Staff>();

                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new Staff
                    {

                        StaffID = row["StaffID"].ToString(),
                        FirstName = row["FirstName"].ToString(),
                        MiddleName = row["MiddleName"].ToString(),
                        LastName = row["LastName"].ToString(),
                        Gender = row["Gender"].ToString(),
                        Company = row["Company"].ToString(),
                        StaffAddress = row["Staffaddress"].ToString()
                         
                    });
                }
                con.Close();
                List<Response> responses = new List<Response>
                {
                    new Response
                    {
                        success = true,
                        message = "Successfully",
                        data = list.ToArray()

                    }
                };


                return Request.CreateResponse(HttpStatusCode.OK, responses);

            }

            catch (Exception ex)
            {

                List<Response> responses = new List<Response>
                {
                    new Response
                    {
                        success = false,
                        message = ex.Message,
                        data = null

                    }
                };


                return Request.CreateResponse(HttpStatusCode.Forbidden, responses);

            }
        }
            
        

        // GET: api/Main/5
        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("Select * from staff where Staffid = @id", con);
                da.SelectCommand.Parameters.AddWithValue("@id", id);
                DataTable dt = new DataTable();

                List<Staff> list = new List<Staff>();

                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {

                    list.Add(new Staff
                    {

                        StaffID = row["StaffID"].ToString(),
                        FirstName = row["FirstName"].ToString(),
                        MiddleName = row["MiddleName"].ToString(),
                        LastName = row["LastName"].ToString(),
                        Gender = row["Gender"].ToString(),
                        Company = row["Company"].ToString(),
                        StaffAddress = row["Staffaddress"].ToString()

                    });


                }
                con.Close();

                if (list.Count > 0)
                {
                    List<Response> responses = new List<Response>
                {
                    new Response
                    {
                        success = true,
                        message = "Successfully",
                        data = list.ToArray()

                    }
                };


                    return Request.CreateResponse(HttpStatusCode.OK, responses);

                }

                else
                {
                    List<Response> responses = new List<Response>
                {
                    new Response
                    {
                        success = true,
                        message = "No record found",
                        data = list.ToArray()

                    }
                };


                    return Request.CreateResponse(HttpStatusCode.NotFound, responses);

                }



            }
            catch (Exception ex)
            {
                List<Response> responses = new List<Response>
                {
                    new Response
                    {
                        success = false,
                        message = ex.Message,
                        data = null

                    }
                };


                return Request.CreateResponse(HttpStatusCode.Forbidden, responses);

            }
        }

        // POST: api/Main
        [HttpPost]
        public HttpResponseMessage Post(Staff staff)

        { 
            try
            {
                
                con.Open();

                cmd = new SqlCommand("Insert into Staff (StaffID,FirstName,MiddleName,LastName,Gender,Company,StaffAddress) Values(@staffID,@firstname,@middlename,@lastname,@gender,@company,@Staffaddress)", con);
                cmd.Parameters.AddWithValue("@staffid", staff.StaffID);
                cmd.Parameters.AddWithValue("@firstname", staff.FirstName);
                cmd.Parameters.AddWithValue("@middlename",staff.MiddleName);
                cmd.Parameters.AddWithValue("@lastname", staff.LastName);
                cmd.Parameters.AddWithValue("@gender", staff.Gender);
                cmd.Parameters.AddWithValue("@company", staff.Company);
                cmd.Parameters.AddWithValue("@Staffaddress", staff.StaffAddress);

                int rowsAffected = cmd.ExecuteNonQuery();

                con.Close();

                if (rowsAffected >= 1)
                {

                    List<Response> responses = new List<Response>
                {
                    new Response
                    {
                        success = true,
                        message = "Saved Successfully",
                        data = null

                    }
                };


                    return Request.CreateResponse(HttpStatusCode.OK, responses);



                }

                else
                {
                    List<Response> responses = new List<Response>
                {
                    new Response
                    {
                        success = false,
                        message = "Not executed. " + rowsAffected.ToString() +" affected",
                        data = null

                    }
                };


                    return Request.CreateResponse(HttpStatusCode.OK, responses);
                }

            }
            catch (Exception ex)
            {
                List<Response> responses = new List<Response>
                {
                    new Response
                    {
                        success = false,
                        message = ex.Message,
                        data = null

                    }
                };


                return Request.CreateResponse(HttpStatusCode.Forbidden, responses);



            }
        }

        // PUT: api/Main/5
        public HttpResponseMessage Put(string id ,Staff staff)
        {
            try
            {

                con.Open();

                cmd = new SqlCommand("Update Staff set FirstName =@firstname, MiddleName =@middlename, LastName =@lastname, Gender =@gender, Company =@company, StaffAddress = @address Where StaffID = @staffID", con);
                cmd.Parameters.AddWithValue("@staffid", id);
                cmd.Parameters.AddWithValue("@firstname", staff.FirstName);
                cmd.Parameters.AddWithValue("@middlename", staff.MiddleName);
                cmd.Parameters.AddWithValue("@lastname", staff.LastName);
                cmd.Parameters.AddWithValue("@gender", staff.Gender);
                cmd.Parameters.AddWithValue("@company", staff.Company);
                cmd.Parameters.AddWithValue("@address", staff.StaffAddress);
                int rowsAffected = cmd.ExecuteNonQuery();

                con.Close();

                if (rowsAffected >= 1)
                {

                    List<Response> responses = new List<Response>
                {
                    new Response
                    {
                        success = true,
                        message = "Updated Successfully",
                        data = null

                    }
                };


                    return Request.CreateResponse(HttpStatusCode.OK, responses);



                }

                else
                {
                    List<Response> responses = new List<Response>
                {
                    new Response
                    {
                        success = false,
                        message = "Not executed. " + rowsAffected.ToString() +" affected",
                        data = null

                    }
                };


                    return Request.CreateResponse(HttpStatusCode.OK, responses);
                }

            }
            catch (Exception ex)
            {
                List<Response> responses = new List<Response>
                {
                    new Response
                    {
                        success = false,
                        message = ex.Message,
                        data = null

                    }
                };


                return Request.CreateResponse(HttpStatusCode.Forbidden, responses);

            }
        }

        // DELETE: api/Main/5
        public HttpResponseMessage Delete(string Staffid)
        {
            try
            {

                con.Open();

                cmd = new SqlCommand("Delete from staff Where StaffID = @staffID", con);
                cmd.Parameters.AddWithValue("@staffid", Staffid);

                int rowsAffected = cmd.ExecuteNonQuery();

                con.Close();

                if (rowsAffected >= 1)
                {

                    List<Response> responses = new List<Response>
                {
                    new Response
                    {
                        success = true,
                        message = "Deleted Successfully",
                        data = null

                    }
                };


                    return Request.CreateResponse(HttpStatusCode.OK, responses);



                }

                else
                {
                    List<Response> responses = new List<Response>
                {
                    new Response
                    {
                        success = false,
                        message = "Not executed. " + rowsAffected.ToString() +" affected",
                        data = null

                    }
                };


                    return Request.CreateResponse(HttpStatusCode.OK, responses);
                }

            }
            catch (Exception ex)
            {
                List<Response> responses = new List<Response>
                {
                    new Response
                    {
                        success = false,
                        message = ex.Message,
                        data = null

                    }
                };


                return Request.CreateResponse(HttpStatusCode.Forbidden, responses);

            }

        }
    }
}
