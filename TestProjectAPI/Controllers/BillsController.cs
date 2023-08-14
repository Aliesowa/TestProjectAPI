using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestProjectAPI.Models;
using System.Configuration;

namespace TestProjectAPI.Controllers
{
    public class BillsController : ApiController
    {

        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mycon2"].ConnectionString);

        SqlCommand cmd = null;
        // GET: api/Bills
        public HttpResponseMessage Get()
        {

            try
            {
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter("Select * from Bills", con);

                DataTable dt = new DataTable();

                List<Bill> list = new List<Bill>();

                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new Bill
                    {

                        Bill_ID = Convert.ToInt32(row["Bill_ID"].ToString()),
                        Staff = row["Staff"].ToString(),
                        BillDate = row["BillDate"].ToString(),
                        Amount = row["Amount"].ToString(),
                        Description = row["Description"].ToString(),
                        ServiceProvider = row["ServiceProvider"].ToString(),
                        Quantity = row["Quantity"].ToString(),
                        Timestamp = row["Timestamp"].ToString()

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
        // GET: api/Bills/5
        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("Select * from Bills where Bill_ID = @id", con);
                da.SelectCommand.Parameters.AddWithValue("@id", id);
                DataTable dt = new DataTable();

                List<Bill> list = new List<Bill>();

                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {

                    list.Add(new Bill
                    {

                        Bill_ID = Convert.ToInt32(row["Bill_ID"].ToString()),
                        Staff = row["Staff"].ToString(),
                        BillDate = row["BillDate"].ToString(),
                        Amount = row["Amount"].ToString(),
                        Description = row["Description"].ToString(),
                        ServiceProvider = row["ServiceProvider"].ToString(),
                        Quantity = row["Quantity"].ToString(),
                           Timestamp = row["Timestamp"].ToString()
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


        // POST: api/Bills
        public HttpResponseMessage Post(Bill bill)

        {
            try
            {

                con.Open();

                cmd = new SqlCommand("Insert into Bills (Staff,ServiceProvider,Description,Quantity,Amount,BillDate) Values(@staff,@serviceProvider,@description,@qty,@amount,@billdate)", con);
                
                cmd.Parameters.AddWithValue("@staff", bill.Staff);
                cmd.Parameters.AddWithValue("@serviceProvider", bill.ServiceProvider);
                cmd.Parameters.AddWithValue("@description", bill.Description);
                cmd.Parameters.AddWithValue("@qty", bill.Quantity);
                cmd.Parameters.AddWithValue("@amount", bill.Amount);
                cmd.Parameters.AddWithValue("@billdate", bill.BillDate);

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

        // PUT: api/Bills/5
        public HttpResponseMessage Put(int id ,Bill bill)
        {
            try
            {

                con.Open();

                cmd = new SqlCommand("Update Bills set Staff =@staff, ServiceProvider =@serviceprovider, Description =@description, Quantity =@qty, Amount =@amount, BillDate = @billdate Where Bill_ID = @id", con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@staff", bill.Staff);
                cmd.Parameters.AddWithValue("@serviceProvider", bill.ServiceProvider);
                cmd.Parameters.AddWithValue("@description", bill.Description);
                cmd.Parameters.AddWithValue("@qty", bill.Quantity);
                cmd.Parameters.AddWithValue("@amount", bill.Amount);
                cmd.Parameters.AddWithValue("@billdate", bill.BillDate);
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

        // DELETE: api/Bills/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {

                con.Open();

                cmd = new SqlCommand("Delete from Bills Where Bill_ID = @id", con);
                cmd.Parameters.AddWithValue("@id", id);

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
