using Dapper;

using ProDashBoard.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using ProDashBoard.Model.Repository;

namespace ProDashBoard.Data
{
    public class ProjectRepository : IProjectRepository
    {

        private readonly IDbConnection _db;
        
        public ProjectRepository()
        {
            _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DashBoard1"].ConnectionString);
            
            
        }

        public List<Project> Get()
        {
            Debug.WriteLine("Threshold");
            return this._db.Query<Project>("SELECT [Id],[AccountId],[Name],[ProjetCode],[Enabled],[RowVersion],[ProjectOwner] FROM [Project] WHERE Enabled=1").ToList();
        }

        public Project Get(int id)
        {
            return _db.Query<Project>("SELECT [Id],[AccountId],[Name],[ProjetCode],[Enabled],[RowVersion],[ProjectOwner] FROM [Project] WHERE Enabled=1 AND ID = '" + id+"'").SingleOrDefault();
        }

        public List<ProjectData> getProjectData()
        {
            return _db.Query<ProjectData>("SELECT p.[Id],p.[AccountId],a.[AccountName],p.[Name],p.[ProjetCode],p.[Enabled],p.[RowVersion],p.[ProjectOwner] FROM [Project] p,[Account] a WHERE p.AccountId=a.Id and p.Enabled=1").ToList();
        }

        public Spec GetSpec(int AccountId)
        {
            return _db.Query<Spec>("SELECT [Id],[AccountId],[linkId],[SpecLevel],[PendingCount] FROM [Spec] WHERE AccountId = '" + AccountId + "'").SingleOrDefault();
        }

        public List<Project> getSelectedAccountProjects(int accountId)
        {
            return _db.Query<Project>("SELECT [Id],[AccountId],[Name],[ProjetCode],[Enabled],[RowVersion],[ProjectOwner] FROM [Project] WHERE Enabled=1 AND AccountId = '" + accountId + "'").ToList();
        }

        public List<Project> getSelectedAdminAccountProjects(int accountId)
        {
            return _db.Query<Project>("SELECT [Id],[AccountId],[Name],[ProjetCode],[Enabled],[RowVersion],[ProjectOwner] FROM [Project] WHERE AccountId = '" + accountId + "'").ToList();
        }

        public int add(string projectName, int accountId)
        {
            int datarows = 0;

            if (accountId == 0)
            {
                datarows = this._db.Execute(@"INSERT Project([AccountId],[Name],[Enabled]) values ((select MAX(Id) from Account),@Name,@Enabled)",
                new { Name = projectName, Enabled = true });
            }
            else
            {
                datarows = this._db.Execute(@"INSERT Project([AccountId],[Name],[Enabled]) values (@AccountId,@Name,@Enabled)",
                    new { AccountId=accountId, Name = projectName, Enabled = true });
            }
            return datarows;
        }
    }
} 