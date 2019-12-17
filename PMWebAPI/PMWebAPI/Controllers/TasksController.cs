using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PMWebAPI.Models;

namespace PMWebAPI.Controllers
{
    public class TasksController : ApiController
    {
        private ProjectMgmtDBModels db = new ProjectMgmtDBModels();

        // GET: api/Tasks
        public IQueryable<Task> GetTasks()
        {
            return db.Tasks;
        }

        

        // GET: api/Tasks
        public IQueryable<Task> GetTasks(int id,string strSortBy)
        {
            //int prjid = Int32.Parse(id);
            List<string> preferences = new List<string> { "Completed", "Inprogress", "Assigned" };
            if ( id == 0)
            {
                if (strSortBy == "StartDate")
                {
                    return db.Tasks.OrderBy(a => a.StartDate);
                }
                else if (strSortBy == "EndDate")
                {
                    return db.Tasks.OrderBy(a => a.EndDate);
                }
                else if (strSortBy == "Priority")
                {
                    return db.Tasks.OrderBy(a => a.Priority);
                }
                else if (strSortBy == "Status")
                {
                    return db.Tasks.OrderByDescending(a => a.Status == "Completed").ThenByDescending(a => a.Status);
                }
                else
                {
                    return db.Tasks.OrderBy(a => a.TaskID);
                }
            }
            else
            {
                if (strSortBy == "StartDate")
                {
                    return db.Tasks.Where(a => a.ProjectID == id).OrderBy(a => a.StartDate);
                }
                else if (strSortBy == "EndDate")
                {
                    return db.Tasks.Where(a => a.ProjectID == id).OrderBy(a => a.EndDate);
                }
                else if (strSortBy == "Priority")
                {
                    return db.Tasks.Where(a => a.ProjectID == id).OrderBy(a => a.Priority);
                }
                else if (strSortBy == "Status")
                {
                    return db.Tasks.Where(a => a.ProjectID == id).OrderByDescending(a => a.Status == "Completed").ThenByDescending(a => a.Status); ;
                }
                else
                {
                    return db.Tasks.Where(a => a.ProjectID == id).OrderBy(a => a.TaskID);
                }
            }
            
        }

        

        // GET: api/Tasks/5
        [ResponseType(typeof(Task))]
        public IHttpActionResult GetTask(int id, string idname)
        {
            if (idname == "Project")
                return Ok(db.Tasks.Where(a => a.ProjectID == id));
            else if (idname == "TaskCount")
                return Ok(db.Tasks.Count(a => a.ProjectID == id));
            else
            {
                Task task = db.Tasks.Find(id);
                if (task == null)
                {
                    return NotFound();
                }

                return Ok(task);
            }
            

            //if (task == null)
            //{
            //    return NotFound();
            //}

            //return Ok(task);
        }

        // PUT: api/Tasks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTask(int id, Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != task.TaskID)
            {
                return BadRequest();
            }

            db.Entry(task).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tasks
        [ResponseType(typeof(Task))]
        public IHttpActionResult PostTask(Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tasks.Add(task);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = task.TaskID }, task);
        }

        // DELETE: api/Tasks/5
        [ResponseType(typeof(Task))]
        public IHttpActionResult DeleteTask(int id)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            db.Tasks.Remove(task);
            db.SaveChanges();

            return Ok(task);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaskExists(int id)
        {
            return db.Tasks.Count(e => e.TaskID == id) > 0;
        }
    }
}