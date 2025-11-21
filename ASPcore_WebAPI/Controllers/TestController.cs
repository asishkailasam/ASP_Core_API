using Microsoft.AspNetCore.Mvc;
using ASPcore_WebAPI.Model;




namespace ASPcore_WebAPI.Controllers
{
    [Route("Test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        EmployeeDB dbobj = new EmployeeDB();      // Create an object for Db

        //------------------------------------------------INSERT-----------------------------------------------------
        // POST api/<TestController>
        [HttpPost]
        [Route("posttab")]
        public void Post(Employee clsobj)
        {
            dbobj.InsertDB(clsobj);
        }
        //-------------------------------------------Get all Items---------------------------------------------------------------
        // GET: api/<TestController>
        [HttpGet]
        [Route("GetAllTab")]
        public List<Employee> Get()
        {
            return dbobj.SelectDB();
        }

        //----------------------------------------------Get With ID-------------------

        // GET api/<TestController>/5
        [HttpGet]
        [Route("GetWithIdTab/{id}")]
        public Employee Get(int id)
        {
            var emp = dbobj.SelectDetailsWithId(id); // Method 1
            //var getEmployee = dbobj.SelectDB().Where(x => x.Id == id).FirstOrDefault();
            //return getEmployee;   // Method 2
            return emp;
        }

        //--------------------------------------------DELETE ----------------------------
        // DELETE api/<TestController>/5
        [HttpDelete]
        [Route("DeleteTab/{id}")]
        public void Delete(int id)
        {
            dbobj.DeleteDB(id);
        }

        //--------------------------------------Update-----------------------------------
        // PUT api/<TestController>/5
        [HttpPut]
        [Route("UpdateTab/{id}")]
        public void Put(int id, Employee obj)
        {
            obj.Id = id;       // Set the ID from the route
            dbobj.UpdateDB(obj);

        }
    }
}
