using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Vote_Final.Migrations
{
    /// <inheritdoc />
    public partial class firstInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ballot",
                columns: table => new
                {
                    BallotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BallotName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BallotDate = table.Column<DateTime>(type: "datetime2", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ballot", x => x.BallotId);
                });

            migrationBuilder.CreateTable(
                name: "Candidate",
                columns: table => new
                {
                    CandidateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PartyAffiliation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidate", x => x.CandidateId);
                });

            migrationBuilder.CreateTable(
                name: "Voters",
                columns: table => new
                {
                    VoterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VotingDistrict = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAlive = table.Column<bool>(type: "bit", nullable: false),
                    VoterStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voters", x => x.VoterId);
                });

            migrationBuilder.CreateTable(
                name: "BallotIssue",
                columns: table => new
                {
                    BallotIssueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    YesOption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoOption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BallotId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BallotIssue", x => x.BallotIssueId);
                    table.ForeignKey(
                        name: "FK_BallotIssue_Ballot_BallotId",
                        column: x => x.BallotId,
                        principalTable: "Ballot",
                        principalColumn: "BallotId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Race",
                columns: table => new
                {
                    RaceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RaceName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsPartisan = table.Column<bool>(type: "bit", nullable: false),
                    BallotId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Race", x => x.RaceId);
                    table.ForeignKey(
                        name: "FK_Race_Ballot_BallotId",
                        column: x => x.BallotId,
                        principalTable: "Ballot",
                        principalColumn: "BallotId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BallotIssueSelection",
                columns: table => new
                {
                    BallotIssueSelectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BallotId = table.Column<int>(type: "int", nullable: false),
                    VoterId = table.Column<int>(type: "int", nullable: false),
                    BallotIssueId = table.Column<int>(type: "int", nullable: false),
                    IsFor = table.Column<bool>(type: "bit", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BallotIssueSelection", x => x.BallotIssueSelectionId);
                    table.ForeignKey(
                        name: "FK_BallotIssueSelection_BallotIssue_BallotIssueId",
                        column: x => x.BallotIssueId,
                        principalTable: "BallotIssue",
                        principalColumn: "BallotIssueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BallotIssueSelection_Ballot_BallotId",
                        column: x => x.BallotId,
                        principalTable: "Ballot",
                        principalColumn: "BallotId");
                    table.ForeignKey(
                        name: "FK_BallotIssueSelection_Voters_VoterId",
                        column: x => x.VoterId,
                        principalTable: "Voters",
                        principalColumn: "VoterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RaceBallotSelection",
                columns: table => new
                {
                    RaceBallotSelectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BallotId = table.Column<int>(type: "int", nullable: false),
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    CandidateId = table.Column<int>(type: "int", nullable: false),
                    VoterId = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceBallotSelection", x => x.RaceBallotSelectionId);
                    table.ForeignKey(
                        name: "FK_RaceBallotSelection_Ballot_BallotId",
                        column: x => x.BallotId,
                        principalTable: "Ballot",
                        principalColumn: "BallotId");
                    table.ForeignKey(
                        name: "FK_RaceBallotSelection_Candidate_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "CandidateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RaceBallotSelection_Race_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Race",
                        principalColumn: "RaceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RaceBallotSelection_Voters_VoterId",
                        column: x => x.VoterId,
                        principalTable: "Voters",
                        principalColumn: "VoterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RaceCandidate",
                columns: table => new
                {
                    RaceCandidateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    CandidateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceCandidate", x => x.RaceCandidateId);
                    table.ForeignKey(
                        name: "FK_RaceCandidate_Candidate_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "CandidateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RaceCandidate_Race_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Race",
                        principalColumn: "RaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Ballot",
                columns: new[] { "BallotId", "BallotDate", "BallotName" },
                values: new object[] { 1, new DateTime(2024, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "June Primary" });

            migrationBuilder.InsertData(
                table: "Candidate",
                columns: new[] { "CandidateId", "FirstName", "LastName", "PartyAffiliation" },
                values: new object[,]
                {
                    { 1, "Gus", "Dawg", "RePAWblican" },
                    { 2, "Donna", "Diamond", "Green Party" },
                    { 3, "Javier", "Mendez", "Socialista" },
                    { 4, "Larry", "Diamond", "Get Down Party" }
                });

            migrationBuilder.InsertData(
                table: "Voters",
                columns: new[] { "VoterId", "Address", "IsAlive", "Phone", "VoterName", "VoterStatus", "VotingDistrict" },
                values: new object[] { 1, "123 3rd St", true, "406-555-9854", "John Smith", "Active", "Ninth" });

            migrationBuilder.InsertData(
                table: "BallotIssue",
                columns: new[] { "BallotIssueId", "BallotId", "Description", "NoOption", "Title", "Type", "YesOption" },
                values: new object[] { 1, 1, "allows all dogs over the age of 5 to vote", "No on Prop 4543", "Proposition 4543", "Proposition", "Yes on Prop 4543" });

            migrationBuilder.InsertData(
                table: "Race",
                columns: new[] { "RaceId", "BallotId", "IsPartisan", "RaceName" },
                values: new object[,]
                {
                    { 1, 1, true, "United States Senate" },
                    { 2, 1, true, "Governor" },
                    { 3, 1, false, "Chief Justice" }
                });

            migrationBuilder.InsertData(
                table: "RaceCandidate",
                columns: new[] { "RaceCandidateId", "CandidateId", "RaceId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 1 },
                    { 4, 4, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BallotIssue_BallotId",
                table: "BallotIssue",
                column: "BallotId");

            migrationBuilder.CreateIndex(
                name: "IX_BallotIssueSelection_BallotId",
                table: "BallotIssueSelection",
                column: "BallotId");

            migrationBuilder.CreateIndex(
                name: "IX_BallotIssueSelection_BallotIssueId",
                table: "BallotIssueSelection",
                column: "BallotIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_BallotIssueSelection_VoterId",
                table: "BallotIssueSelection",
                column: "VoterId");

            migrationBuilder.CreateIndex(
                name: "IX_Race_BallotId",
                table: "Race",
                column: "BallotId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceBallotSelection_BallotId",
                table: "RaceBallotSelection",
                column: "BallotId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceBallotSelection_CandidateId",
                table: "RaceBallotSelection",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceBallotSelection_RaceId",
                table: "RaceBallotSelection",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceBallotSelection_VoterId",
                table: "RaceBallotSelection",
                column: "VoterId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceCandidate_CandidateId",
                table: "RaceCandidate",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceCandidate_RaceId",
                table: "RaceCandidate",
                column: "RaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BallotIssueSelection");

            migrationBuilder.DropTable(
                name: "RaceBallotSelection");

            migrationBuilder.DropTable(
                name: "RaceCandidate");

            migrationBuilder.DropTable(
                name: "BallotIssue");

            migrationBuilder.DropTable(
                name: "Voters");

            migrationBuilder.DropTable(
                name: "Candidate");

            migrationBuilder.DropTable(
                name: "Race");

            migrationBuilder.DropTable(
                name: "Ballot");
        }
    }
}
