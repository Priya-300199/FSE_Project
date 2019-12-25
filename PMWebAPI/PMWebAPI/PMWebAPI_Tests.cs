using NUnit.Framework;
using PMWebAPI.Controllers;
using PMWebAPI.Models;
using System;
/*using System.Collections.Generic;*/
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
/*using System.Web;*/

namespace PMWebAPI
{
    [TestFixture]
    public class UserControllerTests
    {
        int usr_id;
        [TestCase]
        public void GetUsers()
        {
            UsersController usrController = new UsersController();
            IQueryable<User> response = usrController.GetUsers();
            Assert.IsNotNull(response);
        }

        [TestCase]
        public void GetUserByID()
        {
            int id = 1;
            UsersController usrController = new UsersController();
            System.Web.Http.IHttpActionResult response = usrController.GetUser(id);
            var contentResult = response as OkNegotiatedContentResult<User>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(id, contentResult.Content.UserID);            
        }

        [TestCase]
        public void AddUser()
        {
            User newUser = new User { FirstName = "NunitTest", LastName = "NunitTest" };
            UsersController usrController = new UsersController();
            IHttpActionResult response = usrController.PostUser(newUser);
            var contentResult = response as CreatedAtRouteNegotiatedContentResult<User>;
            Assert.IsNotNull(contentResult);
            usr_id = contentResult.Content.UserID;
            Assert.IsNotNull(contentResult.Content.UserID);
        }

        [TestCase]
        public void UpdateUser()
        {
            //int id = 2003;
            User upUser = new User { UserID = usr_id, FirstName = "NunitTest", LastName = "LastName" };
            UsersController usrController = new UsersController();
            IHttpActionResult response = usrController.PutUser(usr_id, upUser);
            var contentResult = response as CreatedAtRouteNegotiatedContentResult<User>;
            IHttpActionResult actionResult = usrController.GetUser(usr_id);
            var responseContent = actionResult as OkNegotiatedContentResult<User>;
            Assert.AreEqual("LastName", responseContent.Content.LastName);            
        }

        [TestCase]
        public void DeleteUser()
        {
            //int id = 2003;
            UsersController usrController = new UsersController();
            IHttpActionResult response = usrController.DeleteUser(usr_id);
            var contentResult = response as OkNegotiatedContentResult<User>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(usr_id, contentResult.Content.UserID);
        }
    }

    [TestFixture]
    public class ProjectsControllerTests
    {
        int prj_id;
        [TestCase]
        public void GetProjects()
        {
            ProjectsController prjController = new ProjectsController();
            IQueryable<Project> response = prjController.GetProjects();
            Assert.IsNotNull(response);
        }

        [TestCase]
        public void GetProjectByID()
        {
            int id = 1;
            ProjectsController prjController = new ProjectsController();
            IHttpActionResult response = prjController.GetProject(id);
            var contentResult = response as OkNegotiatedContentResult<Project>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(id, contentResult.Content.ProjectID);
        }

        [TestCase]
        public void AddProject()
        {
            Project newProject = new Project { ProjectName = "NunitTest", StartDate = DateTime.Today.Date, EndDate = DateTime.Today.Date.AddDays(1), Priority = 2, UserID = 1 };
            ProjectsController prjController = new ProjectsController();
            IHttpActionResult response = prjController.PostProject(newProject);
            var contentResult = response as CreatedAtRouteNegotiatedContentResult<Project>;
            Assert.IsNotNull(contentResult);
            prj_id = contentResult.Content.ProjectID;
            Assert.IsNotNull(contentResult.Content.ProjectID);
        }

        [TestCase]
        public void UpdateProject()
        {
            //int id = 5;
            Project upProject = new Project { ProjectID = prj_id, ProjectName = "NunitProject", StartDate = DateTime.Today.Date, EndDate = DateTime.Today.Date.AddDays(1), Priority = 2, UserID = 1 };
            ProjectsController prjController = new ProjectsController();
            IHttpActionResult response = prjController.PutProject(prj_id , upProject);
            var contentResult = response as CreatedAtRouteNegotiatedContentResult<Project>;
            IHttpActionResult actionResult = prjController.GetProject(prj_id);
            var responseContent = actionResult as OkNegotiatedContentResult<Project>;
            Assert.AreEqual("NunitProject", responseContent.Content.ProjectName);
        }

        [TestCase]
        public void DeleteProject()
        {
            //int id = 2003;
            ProjectsController prjController = new ProjectsController();
            IHttpActionResult response = prjController.DeleteProject(prj_id);
            var contentResult = response as OkNegotiatedContentResult<Project>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(prj_id, contentResult.Content.ProjectID);
        }
    }

    [TestFixture]
    public class ProjectVWsControllerTests
    {        
        [TestCase]
        public void GetProjectVWs()
        {
            ProjectVWsController prjVWController = new ProjectVWsController();
            IQueryable<ProjectVW> response = prjVWController.GetProjectVWs();
            Assert.IsNotNull(response);
        }        
    }

    [TestFixture]
    public class ParentTasksControllerTests
    {
        [TestCase]
        public void GetParentTasks()
        {
            ParentTasksController ptController = new ParentTasksController();
            IQueryable<ParentTask> response = ptController.GetParentTasks();
            Assert.IsNotNull(response);
        }

        [TestCase]
        public void GetParentTaskByID()
        {
            int id = 1;
            ParentTasksController ptController = new ParentTasksController();
            IHttpActionResult response = ptController.GetParentTask(id);
            var contentResult = response as OkNegotiatedContentResult<ParentTask>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(id, contentResult.Content.ParentTaskID);
        }
    }

    [TestFixture]
    public class TasksControllerTests
    {
        int tsk_id;
        [TestCase]
        public void GetTasks()
        {
            TasksController tskController = new TasksController();
            IQueryable<Task> response = tskController.GetTasks();
            Assert.IsNotNull(response);
        }

        [TestCase]
        public void GetTaskByID()
        {
            int id = 1;
            TasksController tskController = new TasksController();
            IHttpActionResult response = tskController.GetTask(id,"");
            var contentResult = response as OkNegotiatedContentResult<Task>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(id, contentResult.Content.TaskID);
        }

        [TestCase]
        public void AddTask()
        {
            Task newTask = new Task { TaskName = "NunitTest", StartDate = DateTime.Today.Date, EndDate = DateTime.Today.Date.AddDays(1), Priority = 2, UserID = 1, ProjectID = 1, ParentTaskID = 1, Status = "Assigned" };
            TasksController tskController = new TasksController();
            IHttpActionResult response = tskController.PostTask(newTask);
            var contentResult = response as CreatedAtRouteNegotiatedContentResult<Task>;
            Assert.IsNotNull(contentResult);
            tsk_id = contentResult.Content.TaskID;
            Assert.IsNotNull(contentResult.Content.TaskID);
        }

        [TestCase]
        public void UpdateTask()
        {
            //int id = 5;
            Task upTask = new Task { TaskID = tsk_id, TaskName = "NunitTask", StartDate = DateTime.Today.Date, EndDate = DateTime.Today.Date.AddDays(1), Priority = 2, UserID = 1, ProjectID = 1, ParentTaskID = 1, Status = "Assigned" };
            TasksController tskController = new TasksController();
            IHttpActionResult response = tskController.PutTask(tsk_id,upTask);
            var contentResult = response as CreatedAtRouteNegotiatedContentResult<Task>;
            IHttpActionResult actionResult = tskController.GetTask(tsk_id,"");
            var responseContent = actionResult as OkNegotiatedContentResult<Task>;
            Assert.AreEqual("NunitTask", responseContent.Content.TaskName);
        }

        [TestCase]
        public void DeleteTask()
        {
            //int id = 2003;
            TasksController tskController = new TasksController();
            IHttpActionResult response = tskController.DeleteTask(tsk_id);
            var contentResult = response as OkNegotiatedContentResult<Task>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(tsk_id, contentResult.Content.TaskID);
        }
    }
    public class PMWebAPI_Tests
    {
    }
}