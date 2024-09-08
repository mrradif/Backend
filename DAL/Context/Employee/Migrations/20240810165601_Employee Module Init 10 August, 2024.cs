using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Context.Employee.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeModuleInit10August2024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HR_EmployeeInformation",
                columns: table => new
                {
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Salutation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    NickName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    GradeId = table.Column<int>(type: "int", nullable: true),
                    DesignationId = table.Column<int>(type: "int", nullable: true),
                    InternalDesignationId = table.Column<int>(type: "int", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    SectionId = table.Column<int>(type: "int", nullable: true),
                    SubSectionId = table.Column<int>(type: "int", nullable: true),
                    UnitId = table.Column<int>(type: "int", nullable: true),
                    CostCenterId = table.Column<int>(type: "int", nullable: true),
                    DivisionId = table.Column<int>(type: "int", nullable: true),
                    AppointmentDate = table.Column<DateTime>(type: "date", nullable: true),
                    WorkShiftId = table.Column<long>(type: "bigint", nullable: false),
                    DateOfJoining = table.Column<DateTime>(type: "date", nullable: true),
                    DateOfConfirmation = table.Column<DateTime>(type: "date", nullable: true),
                    ContractStartDate = table.Column<DateTime>(type: "date", nullable: true),
                    ContractEndDate = table.Column<DateTime>(type: "date", nullable: true),
                    JobCategoryId = table.Column<long>(type: "bigint", nullable: true),
                    ProbationMonth = table.Column<short>(type: "smallint", nullable: true),
                    ProbationEndDate = table.Column<DateTime>(type: "date", nullable: true),
                    ExtendedProbationMonth = table.Column<short>(type: "smallint", nullable: true),
                    EmployeeTypeId = table.Column<long>(type: "bigint", nullable: true),
                    OfficeMobile = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    OfficeEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ReferenceNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FingerID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Taxzone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MinimumTaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TINNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TINFilePath = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    JobType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: true),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: true),
                    IsPFMember = table.Column<bool>(type: "bit", nullable: true),
                    PFActivationDate = table.Column<DateTime>(type: "date", nullable: true),
                    IsFBActive = table.Column<bool>(type: "bit", nullable: true),
                    FBActivationDate = table.Column<DateTime>(type: "date", nullable: true),
                    TerminationStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TerminationDate = table.Column<DateTime>(type: "date", nullable: true),
                    CalculateProjectionTaxProratedBasis = table.Column<bool>(type: "bit", nullable: true),
                    CalculateFestivalBonusTaxProratedBasis = table.Column<bool>(type: "bit", nullable: true),
                    PreviousCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GlobalID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StateStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HR_EmployeeInformation", x => x.EmployeeId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HR_EmployeeInformation");
        }
    }
}
