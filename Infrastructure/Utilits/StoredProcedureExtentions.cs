using Infrastructure.Auth;
using Infrastructure.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utilits
{
    public static class StoredProcedureExtentions
    {
        #region Create
        public static async Task AddUserProject(this TaskTrackerDbContext context , IEnumerable<UserProject> usersProject)
        {
            foreach (var user in usersProject)
            {
                await context.Database.ExecuteSqlRawAsync("Add_UserProject @UserId, @ProjectId",
                     new SqlParameter("@UserId", user.UserId),
                     new SqlParameter("@ProjectId", user.ProjectId));
            }
 
        }

        public async static Task<long> Create_User(this TaskTrackerDbContext context, User user)
        {
            var Id = new Microsoft.Data.SqlClient.SqlParameter
            {
                ParameterName = "@Id",
                SqlDbType = System.Data.SqlDbType.BigInt,
                Direction = System.Data.ParameterDirection.Output
            };
            await context.Database.ExecuteSqlRawAsync("Create_User @FullName, @Email, @Password, @Spice, @Phone, @Id OUT",
                new SqlParameter("@FullName",user.FullName),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@Password", user.Password),
                new SqlParameter("@Spice", user.Spice ?? (object)DBNull.Value),
                new SqlParameter("@Phone", user.Phone?? (object)DBNull.Value),
                Id);
            return long.Parse(Id.Value.ToString());
        }
        public async static Task<long> Create_Attachment(this TaskTrackerDbContext context, Attachment attachment)
        {
            var Id = new Microsoft.Data.SqlClient.SqlParameter
            {
                ParameterName = "@Id",
                SqlDbType = System.Data.SqlDbType.BigInt,
                Direction = System.Data.ParameterDirection.Output
            };
            await context.Database.ExecuteSqlRawAsync("Create_Attachment @Data, @Extention, @Type, @UserId, @WorkTaskId, @Id OUT",
                new SqlParameter("@Data", attachment.Data),
                new SqlParameter("@Extention", attachment.Extention), 
                new SqlParameter("@Type", attachment.Type),
                new SqlParameter("@UserId", attachment.User.Id),
                new SqlParameter("@WorkTaskId", attachment.WorkTask.Id),
                Id);
            return long.Parse(Id.Value.ToString());

        }
        public async static Task<long> Create_Comment(this TaskTrackerDbContext context, Comment comment)
        {
            var Id = new Microsoft.Data.SqlClient.SqlParameter
            {
                ParameterName = "@Id",
                SqlDbType = System.Data.SqlDbType.BigInt,
                Direction = System.Data.ParameterDirection.Output
            };
            await context.Database.ExecuteSqlRawAsync("Create_Comment @Description, @UserId, @WorkTaskId, @Date, @Id OUT",
                new SqlParameter("@Date", DateTime.Now),
                new SqlParameter("@Description", comment.Description),
                new SqlParameter("@WorkTaskId", comment.WorkTaskId),
                new SqlParameter("@UserId", comment.User.Id),
                Id);
            return long.Parse(Id.Value.ToString());

        }

        public async static Task<long> Create_Epic(this TaskTrackerDbContext context, Epic epic)
        {
            var Id = new Microsoft.Data.SqlClient.SqlParameter
            {
                ParameterName = "@Id",
                SqlDbType = System.Data.SqlDbType.BigInt,
                Direction = System.Data.ParameterDirection.Output
            };
            await context.Database.ExecuteSqlRawAsync("Create_Epic @Title, @Description, @ProjectId, @Id OUT",
                new SqlParameter("@Title", epic.Title),
                new SqlParameter("@Description", epic.Description ?? (object)DBNull.Value),
                new SqlParameter("@ProjectId", epic.ProjectId),
                Id);
            return long.Parse(Id.Value.ToString());

        }

        public async static Task<long> Create_Note(this TaskTrackerDbContext context, Note note)
        {
            var Id = new Microsoft.Data.SqlClient.SqlParameter
            {
                ParameterName = "@Id",
                SqlDbType = System.Data.SqlDbType.BigInt,
                Direction = System.Data.ParameterDirection.Output
            };
            await context.Database.ExecuteSqlRawAsync("Create_Note @Description, @UserId, @Id OUT",
                new SqlParameter("@Description", note.Description),
                new SqlParameter("@UserId", note.UserId),
                Id);
            return long.Parse(Id.Value.ToString());

        }

        public async static Task<long> Create_Notify(this TaskTrackerDbContext context, Notify notify)
        {
            var Id = new Microsoft.Data.SqlClient.SqlParameter
            {
                ParameterName = "@Id",
                SqlDbType = System.Data.SqlDbType.BigInt,
                Direction = System.Data.ParameterDirection.Output
            };
            await context.Database.ExecuteSqlRawAsync("Create_Notify @Message, @UserId, @Id OUT",
                new SqlParameter("@Message", notify.Message),
                new SqlParameter("@UserId", notify.UserId),
                Id);
            return long.Parse(Id.Value.ToString());

        }

        public async static Task<long> Create_Project(this TaskTrackerDbContext context, Project project)
        {

            var Id = new Microsoft.Data.SqlClient.SqlParameter
            {
                ParameterName = "@Id",
                SqlDbType = System.Data.SqlDbType.BigInt,
                Direction = System.Data.ParameterDirection.Output
            };
            await context.Database.ExecuteSqlRawAsync("Create_Project @Name, @Description, @AuthorId, @Id OUT",
                new SqlParameter("@Name", project.Name),
                new SqlParameter("@Description", project.Description),
                new SqlParameter("@AuthorId", project.AuthorId),
                Id);
            return long.Parse(Id.Value.ToString());

        }

        public async static Task<long> Create_Task(this TaskTrackerDbContext context, WorkTask task)
        {
            var Id = new Microsoft.Data.SqlClient.SqlParameter
            {
                ParameterName = "@Id",
                SqlDbType = System.Data.SqlDbType.BigInt,
                Direction = System.Data.ParameterDirection.Output
            };
            await context.Database.ExecuteSqlRawAsync("Create_Task @Importance,@Title, @Description, @StatusTask, @UserId, @ApproximateDateOfCompleted, @PreviousTaskId, @EpicId, @Id OUT",
                new SqlParameter("@Importance", task.Importance ?? (object)DBNull.Value),
                new SqlParameter("@Title", task.Title),
                new SqlParameter("@Description", task.Description),
                new SqlParameter("@StatusTask", task.StatusTask),
                new SqlParameter("@UserId", task.UserId ?? (object)DBNull.Value),
                new SqlParameter("@ApproximateDateOfCompleted", task.ApproximateDateOfCompleted ?? (object)DBNull.Value),
                new SqlParameter("@PreviousTaskId", task.PreviousTaskId ?? (object)DBNull.Value),
                new SqlParameter("@EpicId", task.EpicId ?? (object)DBNull.Value),
                Id);
            return long.Parse(Id.Value.ToString());

        }
        public async static Task Create_UserProject(this TaskTrackerDbContext context, UserProject userProject)
        {

            await context.Database.ExecuteSqlRawAsync("Create_Task @UserId, @ProjectId",
                new SqlParameter("@Importance", userProject.UserId),
                new SqlParameter("@Description", userProject.ProjectId)
                );
        }

        #endregion

        #region Update

        public async static Task ReadAll_Notifies(this TaskTrackerDbContext context, DateTime time)
        {
            await context.Database.ExecuteSqlRawAsync("ReadAll_Notifies @UserId, @Date",
                new SqlParameter("@UserId", UserClaims.User.Id),
                new SqlParameter("@Date", time));
        }
        public async static Task Update_Comment(this TaskTrackerDbContext context, Comment comment)
        {
            await context.Database.ExecuteSqlRawAsync("Update_Comment @Id, @Description",
                new SqlParameter("@Id", comment.Id),
                new SqlParameter("@Description", comment.Description));
        }
        public async static Task Update_Epic(this TaskTrackerDbContext context, Epic epic)
        {

            await context.Database.ExecuteSqlRawAsync("Update_Epic @Id, @Title, @Description",
                new SqlParameter("@Id", epic.Id),
                new SqlParameter("@Title", epic.Title),
                new SqlParameter("@Description", epic.Description ?? (object)DBNull.Value));
        }
        public async static Task Update_User(this TaskTrackerDbContext context, User user)
        {
            await context.Database.ExecuteSqlRawAsync("Update_User @Id, @FullName, @Password, @RefreshToken, @Spice, @Phone",
                new SqlParameter("@Id", user.Id),
                new SqlParameter("@FullName", user.FullName),
                new SqlParameter("@Password", user.Password),
                new SqlParameter("@RefreshToken", user.RefreshToken ?? (object)DBNull.Value),
                new SqlParameter("@Spice", user.Spice ?? (object)DBNull.Value),
                new SqlParameter("@Phone", user.Phone ?? (object)DBNull.Value)
                );
        }
        public async static Task Update_Account_User(this TaskTrackerDbContext context, User user)
        {
            await context.Database.ExecuteSqlRawAsync("Update_Account_User @Id, @AccessFaildCount, @Banned, @Confirmed, @Deleted",
                new SqlParameter("@AccessFaildCount", user.AccessFaildCount),
                new SqlParameter("@Id", user.Id),
                new SqlParameter("@AccessFaildCount", user.AccessFaildCount),
                new SqlParameter("@Banned", user.Banned),
                new SqlParameter("@Confirmed", user.Confirmed),
                new SqlParameter("@Deleted", user.Deleted));
        }
        public async static Task Update_Note(this TaskTrackerDbContext context, Note note)
        {

            await context.Database.ExecuteSqlRawAsync("Update_Note @Id, @Description",
                new SqlParameter("@Id", note.Id),
                new SqlParameter("@Description", note.Description));
        }
        public async static Task Update_Project(this TaskTrackerDbContext context, Project project)
        {

            await context.Database.ExecuteSqlRawAsync("Update_Project @Id, @Name, @Description, @AuthorId",
                new SqlParameter("@Id", project.Id),
                new SqlParameter("@Name", project.Name),
                new SqlParameter("@Description", project.Description),
                new SqlParameter("@AuthorId", project.AuthorId));
        }

        public async static Task Update_Task(this TaskTrackerDbContext context, WorkTask task)
        {

            await context.Database.ExecuteSqlRawAsync("Update_Task @Id, @Importance, @Title, @Description, @StatusTask,  @DateOfClosed, @ApproximateDateOfCompleted, @UserId, @PreviousTaskId, @EpicId",
                new SqlParameter("@Id", task.Id),
                new SqlParameter("@Importance", task.Importance ?? (object)DBNull.Value),
                new SqlParameter("@Title", task.Title),
                new SqlParameter("@Description", task.Description),
                new SqlParameter("@StatusTask", task.StatusTask),
                new SqlParameter("@DateOfClosed", task.DateOfClosed ?? (object)DBNull.Value),
                new SqlParameter("@ApproximateDateOfCompleted", task.ApproximateDateOfCompleted ?? (object)DBNull.Value),
                new SqlParameter("@UserId", task.UserId ?? (object)DBNull.Value),
                new SqlParameter("@PreviousTaskId", task.PreviousTaskId ?? (object)DBNull.Value),
                new SqlParameter("@EpicId", task.EpicId ?? (object)DBNull.Value)
                );
        }
        #endregion

        #region Delete
        public async static Task DeleteAll_Notifies(this TaskTrackerDbContext context, DateTime time)
        {
            await context.Database.ExecuteSqlRawAsync("DeleteAll_Notifies @UserId, @Date",
                new SqlParameter("@UserId", UserClaims.User.Id),
                new SqlParameter("@Date", time));
        }
        public static async Task RemoveUserProject(this TaskTrackerDbContext context, IEnumerable<UserProject> usersProject)
        {
            foreach (var user in usersProject)
            {
                await context.Database.ExecuteSqlRawAsync("Remove_UserProject @UserId, @ProjectId",
                     new SqlParameter("@UserId", user.UserId),
                     new SqlParameter("@ProjectId", user.ProjectId));
            }
        }
        public async static Task Delete_Comment(this TaskTrackerDbContext context, long id)
        {
            await context.Database.ExecuteSqlRawAsync("Delete_Comment @Id",
                new SqlParameter("@Id", id));
        }
        public async static Task Delete_User(this TaskTrackerDbContext context, long id)
        {
            await context.Database.ExecuteSqlRawAsync("Delete_User @Id",
                new SqlParameter("@Id", id));
        }
        public async static Task Delete_Attachment(this TaskTrackerDbContext context, long id)
        {
            await context.Database.ExecuteSqlRawAsync("Delete_Attachment @Id",
                new SqlParameter("@Id", id));
        }
        public async static Task Delete_Epic(this TaskTrackerDbContext context, long id)
        {
            await context.Database.ExecuteSqlRawAsync("Delete_Epic @Id",
                new SqlParameter("@Id", id));
        }
        public async static Task Delete_Note(this TaskTrackerDbContext context, long id)
        {
            await context.Database.ExecuteSqlRawAsync("Delete_Note @Id",
                new SqlParameter("@Id", id));
        }
        public async static Task Delete_Notify(this TaskTrackerDbContext context, long id)
        {
            await context.Database.ExecuteSqlRawAsync("Delete_Notify @Id",
                new SqlParameter("@Id", id));
        }
        public async static Task Delete_Project(this TaskTrackerDbContext context, long id)
        {
            await context.Database.ExecuteSqlRawAsync("Delete_Project @Id",
                new SqlParameter("@Id", id));
        }
        public async static Task Delete_Task(this TaskTrackerDbContext context, long id)
        {
            await context.Database.ExecuteSqlRawAsync("Delete_Task @Id",
                new SqlParameter("@Id", id));
        }
        public async static Task Delete_UserProject(this TaskTrackerDbContext context, long id)
        {
            await context.Database.ExecuteSqlRawAsync("Delete_UserProject @Id",
                new SqlParameter("@Id", id));
        }
        #endregion

    }
}
