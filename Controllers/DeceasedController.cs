using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using CemeterySoul.Models;

namespace CemeterySoul.Controllers
{
    public class DeceasedController : ApiController
    {
        //GET
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(DeceasedDB.GetAllDeceased());
            }
            catch (Exception ex)
            {
                //return BadRequest(ex.Message);
                return BadRequest("could not get all the Deceased!\n  -- " + ex.Message);
                //return Content(HttpStatusCode.BadRequest,ex);
            }
        }


        //GET BY ID
        [Route("{id:int:min(1)}", Name = "GetDeceasedByID")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Deceased d = DeceasedDB.GetDeceasedByID(id);
                if (d != null)
                {
                    return Ok(d);
                }
                return Content(HttpStatusCode.NotFound, $"Deceased with id {id} was not found!!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //GET BY FIRST AND LAST NAME
        public IHttpActionResult Get(string first_name, string last_name)
        {
            try
            {
                Deceased d = DeceasedDB.GetDeceasedByFirstAndLastName(first_name, last_name);
                if (d != null)
                {
                    return Ok(d);
                }
                return Content(HttpStatusCode.NotFound, $"Deceased with first name '{first_name}' and last name '{last_name}' was not found!!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        //INSERT - CREATE - POST
        public IHttpActionResult Post([FromBody] Deceased Deceased2Insert)
        {

            try
            {
                int res = DeceasedDB.InsertDeceased(Deceased2Insert);
                if (res == -1)
                {
                    return Content(HttpStatusCode.BadRequest, $"Deceased id = {Deceased2Insert.ID} was not created in the DB!!!");
                }
                Deceased2Insert.ID = res;
                return Created(new Uri(Url.Link("GetDeceasedByID", new { id = res })), Deceased2Insert);
                //return Created(new Uri(Request.RequestUri.AbsoluteUri + res), Deceased2Insert);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        //UPDATE - PUT LAT AND LONG 
        public IHttpActionResult Put(Deceased Deceased2Update)
        {
            try
            {
                Deceased d = DeceasedDB.GetDeceasedByID(Deceased2Update.ID);
                if (d != null)
                {
                    int res = DeceasedDB.UpdateLatAndLong(Deceased2Update);
                    if (res == 1)
                    {
                        return Ok();
                    }
                    return Content(HttpStatusCode.NotModified, $"Deceased with id {Deceased2Update.ID} exsits but could not be modified!!!");
                }
                return Content(HttpStatusCode.NotFound, "Deceased with id = " + Deceased2Update.ID + " was not found to update!!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }



        //DELETE
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Deceased d = DeceasedDB.GetDeceasedByID(id);
                if (d != null)
                {
                    int res = DeceasedDB.DeleteDeceasedByID(id);
                    if (res == 1)
                    {
                        return Ok();
                    }
                    return Content(HttpStatusCode.BadRequest, $"Deceased with id {id} exsits but could not be deleted!!!");
                }
                return Content(HttpStatusCode.NotFound, "Deceased with id = " + id + " was not found to delete!!!");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        //DELETE BY POST METHOD
        //public IHttpActionResult Post(int id)
        //{

        //    try
        //    {
        //        Deceased d = DeceasedDB.GetDeceasedByID(id);
        //        if (d != null)
        //        {
        //            int res = DeceasedDB.DeleteDeceasedByID(id);
        //            if (res == 1)
        //            {
        //                return Ok();
        //            }
        //            return Content(HttpStatusCode.BadRequest, $"Deceased with id {id} exsits but could not be deleted!!!");
        //        }
        //        return Content(HttpStatusCode.NotFound, "Deceased with id = " + id + " was not found to delete!!!");

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}



        // POST - GET - LAT AND LONG


        public IHttpActionResult Post(int id)
        {

            try
            {
                Deceased d = DeceasedDB.GetLanAndLong(id);
                if (d != null)
                {
                    return Ok(d);
                }
                return Content(HttpStatusCode.NotFound, $"Deceased with id {id} was not found!!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
