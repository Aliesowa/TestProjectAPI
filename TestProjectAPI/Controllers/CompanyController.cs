using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestProjectAPI.Models;

namespace TestProjectAPI.Controllers
{
    public class CompanyController : ApiController
    {

        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mycon2"].ConnectionString);

        SqlCommand cmd = null;

      

        public HttpResponseMessage Get()
        {
            try
            {
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter("Select * from Company", con);

                DataTable dt = new DataTable();

                List<Company> list = new List<Company>();

                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new Company
                    {
                        id = Convert.ToInt32(row["id"].ToString()),
                        CompanyName = row["CompanyName"].ToString(),
                        CompanyAddress = row["CompanyAddress"].ToString(),
                        CompanyContact = row["CompanyContact"].ToString(),
                        
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
        // GET: api/Company/5
        [HttpGet]
        [Route("api/Company/{Name}")]
        public HttpResponseMessage Get(string Name)
        {
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("Select * from Company where CompanyName = @Name", con);
                da.SelectCommand.Parameters.AddWithValue("@Name", Name);
                DataTable dt = new DataTable();

                List<Company> list = new List<Company>();

                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new Company
                    {
                        
                        id = Convert.ToInt32( row["id"].ToString()),
                        CompanyName = row["CompanyName"].ToString(),
                        CompanyAddress = row["CompanyAddress"].ToString(),
                        CompanyContact = row["CompanyContact"].ToString()
                       

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


        // POST: api/Company
        [HttpPost]
        public HttpResponseMessage Post(Company company)

        {
            try
            {

                con.Open();

                cmd = new SqlCommand("Insert into Company (CompanyName,CompanyAddress,CompanyContact) Values( @name,@address,@contact)", con);
                
                cmd.Parameters.AddWithValue("@name", company.CompanyName);
                cmd.Parameters.AddWithValue("@address", company.CompanyAddress);
                cmd.Parameters.AddWithValue("@contact", company.CompanyContact);

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

        // PUT: api/Company/5
        [Route("api/Company/{Name}")]
        public HttpResponseMessage Put(string Name,Company company)

        {
            try
            {

                con.Open();

                cmd = new SqlCommand("Update Company set CompanyName =@newname ,CompanyAddress = @address,CompanyContact = @contact where CompanyName = @name", con);

                cmd.Parameters.AddWithValue("@newname", company.CompanyName);
                cmd.Parameters.AddWithValue("@name", Name);
                cmd.Parameters.AddWithValue("@address", company.CompanyAddress);
                cmd.Parameters.AddWithValue("@contact", company.CompanyContact);

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

        // DELETE: api/Company/5
        public HttpResponseMessage Delete(string CompanyName)
        {
            try
            {

                con.Open();

                cmd = new SqlCommand("Delete from Company Where CompanyName = @name", con);
                cmd.Parameters.AddWithValue("@name", CompanyName);

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
