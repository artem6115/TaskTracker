using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StoredProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Create User
            migrationBuilder.Sql("""
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO

                CREATE PROCEDURE Create_User
                    @FullName nvarchar(max),
                    @Email nvarchar(max),
                    @Password nvarchar(max),
                    @Spice nvarchar(max)=NULL,
                    @Phone nvarchar(max) = NULL
                AS
                BEGIN
                	SET NOCOUNT ON;
                    INSERT INTO USERS (FullName,Email,Password,Spice,Phone) VALUES (@FullName,@Email,@Password,@Spice,@Phone)
                END
                GO
                
                """);

            //Update User
            migrationBuilder.Sql("""
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO

                CREATE PROCEDURE Update_User
                    @Id bigint,
                    @FullName nvarchar(max),
                    @Password nvarchar(max),
                    @RefreshToken nvarchar(max) = NULL,
                    @Spice nvarchar(max) = NULL,
                    @Phone nvarchar(max) = NULL
                AS
                BEGIN
                	SET NOCOUNT ON;
                    UPDATE USERS SET 
                    FullName = @FullName,
                    Password = @Password,
                    RefreshToken = @RefreshToken,
                    Spice = @Spice,
                    Phone = @Phone
                    Where Id = @Id
                END
                GO
                
                """);

            //Users Account Info
            migrationBuilder.Sql("""
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO

                CREATE PROCEDURE Update_Account_User
                    @Id bigint,
                    @AccessFaildCount int,
                    @Banned bit,
                    @Confirmed bit,
                    @Deleted bit
                
                AS
                BEGIN
                	SET NOCOUNT ON;
                    UPDATE USERS SET 
                    AccessFaildCount = @AccessFaildCount,
                    Banned = @Banned,
                    Confirmed = @Confirmed,
                    Deleted = @Deleted
                    Where  Id = @Id
                END
                GO
                
                """);

            //Delete User
            migrationBuilder.Sql("""
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO

                CREATE PROCEDURE Delete_User
                    @Id bigint
                AS
                BEGIN
                	SET NOCOUNT ON;
                    DELETE USERS WHERE Id = @Id
                END
                GO
                
                """);

            //Create Attachment
            migrationBuilder.Sql("""
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO

                CREATE PROCEDURE Create_Attachment
                    @Data varbinary,
                    @Extention nvarchar(max),
                    @Type int,
                    @UserId bigint,
                    @WorkTaskId bigint
                AS
                BEGIN
                	SET NOCOUNT ON;
                    INSERT INTO Attachments VALUES
                    (@Data, @Type, @Extention, @UserId, @WorkTaskId)
                END
                GO
                
                """);

            //Delete Attachment
            migrationBuilder.Sql("""
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO

                CREATE PROCEDURE Delete_Attachment
                    @Id bigint
                AS
                BEGIN
                	SET NOCOUNT ON;
                    DELETE Attachments WHERE Id = @Id
                END
                GO
                
                """);

            //Create Comment
            migrationBuilder.Sql("""
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO

                CREATE PROCEDURE Create_Comment
                    @Description nvarchar(max),
                    @UserId bigint,
                    @TaskId bigint,
                    @Date datetime2(7)
                
                AS
                BEGIN
                	SET NOCOUNT ON;
                    INSERT INTO Comments VALUES
                    (@Description,@UserId,@TaskId,@Date)
                END
                GO
                
                """);

            //Delete Comment
            migrationBuilder.Sql("""
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO

                CREATE PROCEDURE Delete_Comment
                    @Id bigint
                AS
                BEGIN
                	SET NOCOUNT ON;
                    DELETE Comments WHERE Id = @Id
                END
                GO
                
                """);

            //Create Epic
            migrationBuilder.Sql("""
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO

                CREATE PROCEDURE Create_Epic
                    @Title nvarchar(max),
                    @Description nvarchar(max),
                    @ProjectId bigint
                AS
                BEGIN
                	SET NOCOUNT ON;
                    INSERT INTO Epics VALUES 
                    (@Title,@Description,@ProjectId)
                END
                GO
                
                """);

            //Delete Epic
            migrationBuilder.Sql("""
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO

                CREATE PROCEDURE Delete_Epic
                    @Id bigint
                AS
                BEGIN
                	SET NOCOUNT ON;
                    DELETE Epics WHERE Id = @Id
                END
                GO
                
                """);

            //Create Note
            migrationBuilder.Sql("""
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO

                CREATE PROCEDURE Create_Note
                    @Description nvarchar(max),
                    @UserId bigint
                AS
                BEGIN
                	SET NOCOUNT ON;
                    INSERT INTO Notes (Description,DateOfCreated,UserId) VALUES
                    (@Description,GETDATE(),@UserId)
                END
                GO
                
                """);

            //Update Note
            migrationBuilder.Sql("""
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO

                CREATE PROCEDURE Update_Note
                    @Id bigint,
                    @Description nvarchar(max)
                AS
                BEGIN
                	SET NOCOUNT ON;
                    UPDATE Notes SET
                    Description = @Description,
                    DateOfChanged = GETDATE()
                    Where Id = @Id
                END
                GO
                
                """);

            //Delete Note
            migrationBuilder.Sql("""
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO

                CREATE PROCEDURE Delete_Note
                    @Id bigint
                AS
                BEGIN
                	SET NOCOUNT ON;
                    DELETE Notes WHERE Id = @Id
                END
                GO
                
                """);

            //Create Notify
            migrationBuilder.Sql("""
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO

                CREATE PROCEDURE Create_Notify
                    @Title nvarchar(max),
                    @Message nvarchar(max),
                    @UserId bigint
                AS
                BEGIN
                	SET NOCOUNT ON;
                    INSERT INTO Notifies (Title,Message,Date,UserId) VALUES
                    (@Title,@Message, GETDATE(),@UserId)
                END
                GO
                
                """);

            //Delete Notify
            migrationBuilder.Sql("""
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO

                CREATE PROCEDURE Delete_Notify
                    @Id bigint
                AS
                BEGIN
                	SET NOCOUNT ON;
                    DELETE Notifies WHERE Id = @Id
                END
                GO
                
                """);

            //Create Project
            migrationBuilder.Sql("""
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO

                CREATE PROCEDURE Create_Project
                    @Name nvarchar(max),
                    @Description nvarchar(max),
                    @AuthorId bigint
                AS
                BEGIN
                	SET NOCOUNT ON;
                    INSERT INTO Projects VALUES
                    (@Name,@Description, @AuthorId)
                END
                GO
                
                """);

            //Update Project
            migrationBuilder.Sql("""
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO

                CREATE PROCEDURE Update_Project
                    @Id bigint,
                    @Name nvarchar(max),
                    @Description nvarchar(max),
                    @AuthorId bigint
                AS
                BEGIN
                	SET NOCOUNT ON;
                    UPDATE Projects SET
                    Name = @Name,
                    Description = @Description,
                    AuthorId = @AuthorId
                    WHERE Id = @Id
                END
                GO
                
                """);

            //Delete Delete
            migrationBuilder.Sql("""
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO

                CREATE PROCEDURE Delete_Project
                    @Id bigint
                AS
                BEGIN
                	SET NOCOUNT ON;
                    DELETE Projects WHERE Id = @Id
                END
                GO
                
                """);

            //Create relashionship user-project
            migrationBuilder.Sql("""
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO

                CREATE PROCEDURE Create_UserProject
                    @UserId bigint,
                    @ProjectId bigint
                AS
                BEGIN
                	SET NOCOUNT ON;
                    INSERT INTO Users_To_Projects VALUES
                    (@UserId,@ProjectId)
                END
                GO
                
                """);

            //Delete relashionship user-project
            migrationBuilder.Sql("""
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO

                CREATE PROCEDURE Delete_UserProject
                    @UserId bigint,
                    @ProjectId bigint
                AS
                BEGIN
                	SET NOCOUNT ON;
                    Delete Users_To_Projects
                    Where UserId = @UserId AND ProjectId = @ProjectId
                END
                GO
                
                """);

            //Create Task
            migrationBuilder.Sql("""
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO

                CREATE PROCEDURE Create_Task
                    @Importance tinyint = NULL,
                    @Title nvarchar(max),
                    @Description nvarchar(max),
                    @StatusTask int = 0,
                    @UserId bigint = NULL,
                    @PreviousTaskId bigint = NULL,
                    @EpicId bigint = NULL
                
                AS
                BEGIN
                	SET NOCOUNT ON;
                    INSERT INTO Tasks 
                    (Importance, Title, Description, DateOfCreated, StatusTask,UserId,PreviousTaskId,EpicId) 
                    VALUES
                    (@Importance,@Title,@Description, GETDATE(), @StatusTask,@UserId, @PreviousTaskId, @EpicId)
                END
                GO
                
                """);

            //Update Task
            migrationBuilder.Sql("""
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO

                CREATE PROCEDURE Update_Task
                    @Id bigint,
                    @Importance tinyint = NULL,
                    @Title nvarchar(max),
                    @Description nvarchar(max),
                    @StatusTask int,
                    @DateOfClosed datetime2(7) = NULL,
                    @ApproximateDateOfCompleted datetime2(7) = NULL,
                    @UserId bigint = NULL,
                    @PreviousTaskId bigint = NULL,
                    @EpicId bigint = NULL
                
                AS
                BEGIN
                	SET NOCOUNT ON;
                    UPDATE Tasks SET
                    Importance = @Importance,
                    Title = @Title,
                    Description = @Description,
                    StatusTask = @StatusTask,
                    DateOfClosed = @DateOfClosed,
                    ApproximateDateOfCompleted = @ApproximateDateOfCompleted,
                    UserId = @UserId,
                    PreviousTaskId = @PreviousTaskId,
                    EpicId = @EpicId
                    WHERE Id = @Id
                END
                GO
                
                """);

            //Delete Task
            migrationBuilder.Sql("""
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO

                CREATE PROCEDURE Delete_Task
                    @Id bigint
                AS
                BEGIN
                	SET NOCOUNT ON;
                    DELETE Tasks WHERE Id = @Id
                END
                GO
                
                """);




        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE Create_User");
            migrationBuilder.Sql("DROP PROCEDURE Update_User");
            migrationBuilder.Sql("DROP PROCEDURE Update_Account_User");
            migrationBuilder.Sql("DROP PROCEDURE Delete_User");

            migrationBuilder.Sql("DROP PROCEDURE Create_Attachment");
            migrationBuilder.Sql("DROP PROCEDURE Delete_Attachment");
            migrationBuilder.Sql("DROP PROCEDURE Create_Comment");
            migrationBuilder.Sql("DROP PROCEDURE Delete_Comment");
            migrationBuilder.Sql("DROP PROCEDURE Create_Epic");
            migrationBuilder.Sql("DROP PROCEDURE Delete_Epic");

            migrationBuilder.Sql("DROP PROCEDURE Create_Note");
            migrationBuilder.Sql("DROP PROCEDURE Update_Note");
            migrationBuilder.Sql("DROP PROCEDURE Delete_Note");

            migrationBuilder.Sql("DROP PROCEDURE Create_Notify");
            migrationBuilder.Sql("DROP PROCEDURE Delete_Notify");
            migrationBuilder.Sql("DROP PROCEDURE Create_UserProject");
            migrationBuilder.Sql("DROP PROCEDURE Delete_UserProject");

            migrationBuilder.Sql("DROP PROCEDURE Create_Project");
            migrationBuilder.Sql("DROP PROCEDURE Update_Project");
            migrationBuilder.Sql("DROP PROCEDURE Delete_Project");

            migrationBuilder.Sql("DROP PROCEDURE Create_Task");
            migrationBuilder.Sql("DROP PROCEDURE Update_Task");
            migrationBuilder.Sql("DROP PROCEDURE Delete_Task");




        }
    }
}
