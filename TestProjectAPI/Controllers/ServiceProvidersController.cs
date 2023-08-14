using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestProjectAPI.Models;
using System.Configuration;

namespace TestProjectAPI.Controllers
{
    public class ServiceProvidersController : ApiController
    {

        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mycon2"].ConnectionString);

        SqlCommand cmd = null;

        // GET: api/ServiceProviders
        public HttpResponseMessage Get()
        {
            try
            {
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter("Select * from ServiceProviders", con);

                DataTable dt = new DataTable();

                List<ServiceProviders> list = new List<ServiceProviders>();

                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new ServiceProviders
                    {

                        id = Convert.ToInt32( row["id"].ToString()),
                        ServiceProviderName = row["ServiceProviderName"].ToString(),
                        ServiceProviderAddress = row["ServiceProviderAddress"].ToString(),
                        ServiceProviderContact = row["ServiceProviderContact"].ToString(),

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

        // GET: api/ServiceProviders/5
        [HttpGet]
        [Route("api/ServiceProviders/{Name}")]
        public HttpResponseMessage Get(string Name)
        {
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("Select * from ServiceProviders where ServiceProviderName = @name", con);
                da.SelectCommand.Parameters.AddWithValue("@name", Name);
                DataTable dt = new DataTable();

                List<ServiceProviders> list = new List<ServiceProviders>();

                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new ServiceProviders
                    {
                        id = Convert.ToInt32(row["id"].ToString()),
                        ServiceProviderName = row["ServiceProviderName"].ToString(),
                        ServiceProviderAddress = row["ServiceProviderAddress"].ToString(),
                        ServiceProviderContact = row["ServiceProviderContact"].ToString()


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


        // POST: api/ServiceProviders
        [HttpPost]

        public HttpResponseMessage Post(ServiceProviders serviceProviders)

        {
            try
            {

                con.Open();

                cmd = new SqlCommand("Insert into ServiceProviders (ServiceProviderName,ServiceProviderAddress,ServiceProviderContact) Values( @name,@address,@contact)", con);

                cmd.Parameters.AddWithValue("@name", serviceProviders.ServiceProviderName);
                cmd.Parameters.AddWithValue("@address", serviceProviders.ServiceProviderAddress);
                cmd.Parameters.AddWithValue("@contact", serviceProviders.ServiceProviderContact);

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


        // PUT: api/ServiceProviders/5
        [Route("api/ServiceProviders/{Name}")]
        public HttpResponseMessage Put(string Name, ServiceProviders serviceProviders)

        {
            try
            {

                con.Open();

                cmd = new SqlCommand("Update ServiceProviders set ServiceProviderName = @NewName ,ServiceProviderAddress = @address,ServiceProviderContact = @contact where ServiceProviderName = @name", con);

                cmd.Parameters.AddWithValue("@name", Name);
                cmd.Parameters.AddWithValue("@NewName", serviceProviders.ServiceProviderName);
                cmd.Parameters.AddWithValue("@address", serviceProviders.ServiceProviderAddress);
                cmd.Parameters.AddWithValue("@contact", serviceProviders.ServiceProviderContact);

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

        // DELETE: api/ServiceProviders/5
        public HttpResponseMessage Delete(string Name)
        {
            try
            {

                con.Open();

                cmd = new SqlCommand("Delete from ServiceProviders Where ServiceProviderName = @name", con);
                cmd.Parameters.AddWithValue("@name", Name);

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
