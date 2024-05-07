using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Triger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                Create TRIGGER [dbo].[UpdateTaskTriger] ON [dbo].[Tasks] 
                After Update,Delete
                AS 
                IF IS_MEMBER ('db_owner') = 0
                declare @Id bigint
                declare @StatusTask int
                declare @IdDelete bigint
                declare @StatusTaskDelete int
                BEGIN
                	set @Id = (select Id from inserted)
                	set @IdDelete = (select Id from deleted)
                	set @StatusTask = (select StatusTask from inserted)
                	set @StatusTaskDelete = (select StatusTask from deleted)
                	if(@StatusTask = 4 and @StatusTaskDelete <> 4)
                	Begin
                		Update Tasks set StatusTask = 0
                		Where PreviousTaskId=@Id and UserId is null
                		Update Tasks set StatusTask = 1
                		Where PreviousTaskId=@Id and UserId is not null;
                	end;
                  	if(@StatusTask <> 4 and @StatusTaskDelete = 4)
                	Begin
                		Update Tasks set StatusTask = 3
                		Where PreviousTaskId=@Id 
                	end;

                END

                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER UpdateTaskTriger;");

        }
    }
}
