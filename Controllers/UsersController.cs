using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using CemeterySoul.Models;


namespace CemeterySoul.Controllers
{
    public class UsersController : ApiController
    {
        //[EnableCors( "*", "*", "*")]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(UsersDB.GetAllUsers());
            }
            catch (Exception ex)
            {
                //return BadRequest(ex.Message);
                return BadRequest("could not get all the users!\n  -- " + ex.Message);
                //return Content(HttpStatusCode.BadRequest,ex);
            }
        }


        //opt1 - time efficient
        //[DisableCors]
        [HttpGet]
        public Users GET(string email, string password)
        {
            return UsersDB.GetUserByEmailAndPassword(email, password);
        }

        //opt2 - less code
        //public Student Get(string email, string password)
        //{
        //    return StudentsDB.GetAllStudents()
        //        .SingleOrDefault(st => st.Email == email && st.Password == password);
        //}


        [Route(Name = "GetUserByEmail")]
        public IHttpActionResult Get(string email_address)
        {
            try
            {
                Users res = UsersDB.GetUserByEmail(email_address);
                if (res == null)
                {
                    return Content(HttpStatusCode.NotFound, $"user with email= {email_address} was not found!");
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        //[DisableCors]
        public IHttpActionResult Post([FromBody] Users val)
        {
            //return UsersDB.GetUserByEmailAndPassword(val.Email_Address, val.Pass);
            try
            {
                Users res = UsersDB.InsertUserToDb(val);
                if (res == null)
                {
                    return Content(HttpStatusCode.BadRequest, $"could not insert user {val.ToString()} or already exists !");
                }
                return Created(new Uri(Url.Link("GetUserByEmail", new { email_address = res.Email_Address })), res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        public IHttpActionResult Delete(string email_address)
        {
            try
            {
                Users u = UsersDB.GetUserByEmail(email_address);
                if (u != null)
                {
                    int res = UsersDB.DeleteUserByEmail(email_address);
                    if (res == 1)
                    {
                        return Ok();
                    }
                    return Content(HttpStatusCode.BadRequest, $"user with email {email_address} exsits but could not be deleted!!!");
                }
                return Content(HttpStatusCode.NotFound, "student with email = " + email_address + " was not found to delete!!!");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
