using Microsoft.Data.SqlClient;
using System;
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
        public async static Task Create_User(this TaskTrackerDbContext context, User user)
        {
            await context.Database.ExecuteSqlRawAsync("Create_User @FullName, @Email, @Password, @Spice, @Phone",
                new SqlParameter("@FullName",user.FullName),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@Password", user.Password),
                new SqlParameter("@Spice", user.Spice ?? ""),
                new SqlParameter("@Phone", user.Phone??"")
                );
        }
        public async static Task Create_Attachment(this TaskTrackerDbContext context, Attachment attachment)
        {

            await context.Database.ExecuteSqlRawAsync("Create_Attachment @Data, @Extention, @Type, @UserId, @WorkTaskId",
                new SqlParameter("@Data", attachment.Data),
                new SqlParameter("@Extention", attachment.Extention), 
                new SqlParameter("@Type", attachment.Type),
                new SqlParameter("@UserId", attachment.User.Id),
                new SqlParameter("@WorkTaskId", attachment.WorkTask.Id));
        }
        public async static Task Create_Comment(this TaskTrackerDbContext context, Comment comment)
        {

            await context.Database.ExecuteSqlRawAsync("Create_Comment @Description, @UserId, @TaskId, @Date",
                new SqlParameter("@Date", DateTime.Now),
                new SqlParameter("@Description", comment.Description),
                new SqlParameter("@TaskId", comment.Task.Id),
                new SqlParameter("@UserId", comment.User.Id));
        }

        public async static Task Create_Epic(this TaskTrackerDbContext context, Epic epic)
        {

            await context.Database.ExecuteSqlRawAsync("Create_Epic @Description, @Title, @ProjectId",
                new SqlParameter("@Description", epic.Description),
                new SqlParameter("@Title", epic.Title),
                new SqlParameter("@ProjectId", epic.ProjectId));
        }
   
        public async static Task Create_Note(this TaskTrackerDbContext context, Note note)
        {

            await context.Database.ExecuteSqlRawAsync("Create_Note @Description, @UserId",
                new SqlParameter("@Description", note.Description),
                new SqlParameter("@UserId", note.UserId));
        }

        public async static Task Create_Notify(this TaskTrackerDbContext context, Notify notify)
        {

            await context.Database.ExecuteSqlRawAsync("Create_Notify @Message, @Title, @UserId",
                new SqlParameter("@Message", notify.Message),
                new SqlParameter("@Title", notify.Title),
                new SqlParameter("@UserId", notify.UserId));
        }

        public async static Task Create_Project(this TaskTrackerDbContext context, Project project)
        {

            await context.Database.ExecuteSqlRawAsync("Create_Project @Name, @Description, @AuthorId",
                new SqlParameter("@Name", project.Name),
                new SqlParameter("@Description", project.Description),
                new SqlParameter("@AuthorId", project.AuthorId));
        }

        public async static Task Create_Task(this TaskTrackerDbContext context, WorkTask task)
        {
            
            await context.Database.ExecuteSqlRawAsync("Create_Task @Importance,@Title, @Description, @StatusTask, @UserId, @PreviousTaskId, @EpicId",
                new SqlParameter("@Importance", task.Importance),
                new SqlParameter("@Title", task.Title),
                new SqlParameter("@Description", task.Description),
                new SqlParameter("@StatusTask", task.StatusTask),
                new SqlParameter("@UserId", task.UserId),
                new SqlParameter("@PreviousTaskId", task.PreviousTaskId ?? (object)DBNull.Value),
                new SqlParameter("@EpicId", task.EpicId ?? (object)DBNull.Value)
                );
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
        public async static Task Update_User(this TaskTrackerDbContext context, User user)
        {
            await context.Database.ExecuteSqlRawAsync("Update_User @Id, @FullName, @Password, @RefreshToken, @Spice, @Phone",
                new SqlParameter("@Id", user.Id),
                new SqlParameter("@FullName", user.FullName),
                new SqlParameter("@Password", user.Password),
                new SqlParameter("@RefreshToken", user.RefreshToken ?? ""),
                new SqlParameter("@Spice", user.Spice ?? ""),
                new SqlParameter("@Phone", user.Phone ?? "")
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
                new SqlParameter("@Importance", task.Importance),
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
