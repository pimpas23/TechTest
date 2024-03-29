﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechTest.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CallDetailRecords",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CallerNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    RecipientNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    CallDateEndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CallDuration = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    TypeOfCall = table.Column<int>(type: "int", nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallDetailRecords", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CallDetailRecords");
        }
    }
}
