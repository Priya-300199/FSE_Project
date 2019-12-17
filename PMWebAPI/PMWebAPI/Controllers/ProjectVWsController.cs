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
    public class ProjectVWsController : ApiController
    {
        private ProjectMgmtDBModels db = new ProjectMgmtDBModels();

        // GET: api/ProjectVWs
        public IQueryable<ProjectVW> GetProjectVWs()
        {
            return db.ProjectVWs;
        }

        // GET: api/ProjectVWs
        public IQueryable<ProjectVW> GetProjectVWs(string strSortBy)
        {
            if (strSortBy == "StartDate")
            {
                return db.ProjectVWs.OrderBy(a => a.StartDate);
            }
            else if (strSortBy == "EndDate")
            {
                return db.ProjectVWs.OrderBy(a => a.EndDate);
            }
            else if (strSortBy == "Priority")
            {
                return db.ProjectVWs.OrderBy(a => a.Priority);
            }
            else if(strSortBy == "Status")
            {
                return db.ProjectVWs.OrderBy (a => a.TaskCount - a.CompletedTask);
            }
            else
            {
                return db.ProjectVWs.OrderBy(a => a.ProjectID);
            }
        }

        // GET: api/ProjectVWs/5
        [ResponseType(typeof(ProjectVW))]
        public IHttpActionResult GetProjectVW(int id)
        {
            ProjectVW projectVW = db.ProjectVWs.Find(id);
            if (projectVW == null)
            {
                return NotFound();
            }

            return Ok(projectVW);
        }

        // PUT: api/ProjectVWs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProjectVW(int id, ProjectVW projectVW)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projectVW.ProjectID)
            {
                return BadRequest();
            }

            db.Entry(projectVW).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectVWExists(id))
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

        // POST: api/ProjectVWs
        [ResponseType(typeof(ProjectVW))]
        public IHttpActionResult PostProjectVW(ProjectVW projectVW)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProjectVWs.Add(projectVW);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProjectVWExists(projectVW.ProjectID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = projectVW.ProjectID }, projectVW);
        }

        // DELETE: api/ProjectVWs/5
        [ResponseType(typeof(ProjectVW))]
        public IHttpActionResult DeleteProjectVW(int id)
        {
            ProjectVW projectVW = db.ProjectVWs.Find(id);
            if (projectVW == null)
            {
                return NotFound();
            }

            db.ProjectVWs.Remove(projectVW);
            db.SaveChanges();

            return Ok(projectVW);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectVWExists(int id)
        {
            return db.ProjectVWs.Count(e => e.ProjectID == id) > 0;
        }
    }
}