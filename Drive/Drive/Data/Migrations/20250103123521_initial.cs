using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Drive.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Mail = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Folders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    UsersIds = table.Column<string[]>(type: "text[]", nullable: true),
                    ParentFolderId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Folders_Folders_ParentFolderId",
                        column: x => x.ParentFolderId,
                        principalTable: "Folders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Folders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    EditingTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Text = table.Column<List<string>>(type: "text[]", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersIds = table.Column<string[]>(type: "text[]", nullable: true),
                    FolderId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Folders_FolderId",
                        column: x => x.FolderId,
                        principalTable: "Folders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Files_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Comments = table.Column<List<string>>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Files_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Mail", "Password" },
                values: new object[,]
                {
                    { new Guid("0375d7fb-6dd2-44ad-ae74-fbd6e193ee7e"), "duje.saric@example.com", "password101" },
                    { new Guid("21e9d0c3-3e7f-4811-b22e-06917be2d113"), "matija.luketin@example.com", "password789" },
                    { new Guid("373ea26d-9eb6-404b-ba43-4e0c81590592"), "bartol.deak@example.com", "password123" },
                    { new Guid("7f9cf05b-a16d-477e-8942-15f55d9b758c"), "ante.roca@example.com", "password456" },
                    { new Guid("b5364602-c06c-4f62-aeee-a74991543a27"), "marija.sustic@example.com", "password102" }
                });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "EditingTime", "FolderId", "Name", "Text", "UserId", "UsersIds" },
                values: new object[,]
                {
                    { new Guid("0565805d-44eb-4a31-8e97-7649a6c09152"), new DateTime(2024, 12, 24, 12, 35, 21, 132, DateTimeKind.Utc).AddTicks(579), null, "Document1.txt", new List<string> { "dokument1" }, new Guid("373ea26d-9eb6-404b-ba43-4e0c81590592"), new[] { "7f9cf05b-a16d-477e-8942-15f55d9b758c" } },
                    { new Guid("21a9f299-00d7-411d-941f-d3c095e62513"), new DateTime(2024, 12, 29, 12, 35, 21, 132, DateTimeKind.Utc).AddTicks(593), null, "Image1.jpg", new List<string> { "najbolja slika" }, new Guid("7f9cf05b-a16d-477e-8942-15f55d9b758c"), new[] { "373ea26d-9eb6-404b-ba43-4e0c81590592" } },
                    { new Guid("d9b81b94-111d-4ea8-846e-45e9f824630a"), new DateTime(2025, 1, 1, 12, 35, 21, 132, DateTimeKind.Utc).AddTicks(599), null, "ProjectProposal.docx", new List<string> { "prvi projekt" }, new Guid("21e9d0c3-3e7f-4811-b22e-06917be2d113"), new[] { "373ea26d-9eb6-404b-ba43-4e0c81590592" } },
                    { new Guid("ec404ca9-9817-4513-927f-7f07c2f31de3"), new DateTime(2024, 12, 26, 12, 35, 21, 132, DateTimeKind.Utc).AddTicks(605), null, "Document2.pdf", new List<string> { "dokument2" }, new Guid("0375d7fb-6dd2-44ad-ae74-fbd6e193ee7e"), new[] { "21e9d0c3-3e7f-4811-b22e-06917be2d113" } }
                });

            migrationBuilder.InsertData(
                table: "Folders",
                columns: new[] { "Id", "Name", "ParentFolderId", "UserId", "UsersIds" },
                values: new object[,]
                {
                    { new Guid("0708d48e-85d8-4d50-9ff4-47aa7d925324"), "Projects", null, new Guid("21e9d0c3-3e7f-4811-b22e-06917be2d113"), new[] { "373ea26d-9eb6-404b-ba43-4e0c81590592" } },
                    { new Guid("67ca63e3-88f1-46a4-90a2-6ef86d89cf1c"), "Images", null, new Guid("7f9cf05b-a16d-477e-8942-15f55d9b758c"), new[] { "373ea26d-9eb6-404b-ba43-4e0c81590592" } },
                    { new Guid("91cd1709-314b-4bc8-b4b7-01976afa7a6e"), "Documents", null, new Guid("373ea26d-9eb6-404b-ba43-4e0c81590592"), new[] { "7f9cf05b-a16d-477e-8942-15f55d9b758c" } }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_DocumentId",
                table: "Comments",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_FolderId",
                table: "Files",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_UserId",
                table: "Files",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Folders_ParentFolderId",
                table: "Folders",
                column: "ParentFolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Folders_UserId",
                table: "Folders",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Folders");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
