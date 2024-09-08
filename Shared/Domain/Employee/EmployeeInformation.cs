using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Shared.Domain.Common.Class;


namespace Shared.Domain.Employee
{
  
    public class EmployeeInformation : AuditableEntity, IStateStatus
    {
        [Key]
        public long EmployeeId { get; set; }
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        [StringLength(50)]
        public string Salutation { get; set; }
        // Basic Info
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(30)]
        public string MiddleName { get; set; }
        [StringLength(30)]
        public string LastName { get; set; }
        [StringLength(30)]
        public string NickName { get; set; }
        [StringLength(200)]
        public string FullName { get; set; }
        public int? GradeId { get; set; }
        public int? DesignationId { get; set; }
        public int? InternalDesignationId { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public int? SubSectionId { get; set; }
        public int? UnitId { get; set; }
        public int? CostCenterId { get; set; }
        public int? DivisionId { get; set; }
        [Column(TypeName = "date")]
        public DateTime? AppointmentDate { get; set; }
        public long WorkShiftId { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateOfJoining { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateOfConfirmation { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ContractStartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ContractEndDate { get; set; }
        public long? JobCategoryId { get; set; }
        public short? ProbationMonth { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ProbationEndDate { get; set; }
        public short? ExtendedProbationMonth { get; set; }
        public long? EmployeeTypeId { get; set; }
        [StringLength(200)]
        public string OfficeMobile { get; set; }
        [EmailAddress, StringLength(200)]
        public string OfficeEmail { get; set; }
        [StringLength(100)]
        public string ReferenceNo { get; set; }
        [StringLength(100)]
        public string FingerID { get; set; }
        [StringLength(100)]
        public string Taxzone { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? MinimumTaxAmount { get; set; }
        [StringLength(100)]
        public string TINNo { get; set; }
        [StringLength(300)]
        public string TINFilePath { get; set; }

        [StringLength(200)]
        public string Remarks { get; set; }
        [StringLength(30)]
        public string JobType { get; set; }

        public bool? IsApproved { get; set; }
        public bool? IsConfirmed { get; set; }
        public bool? IsPFMember { get; set; }
        [Column(TypeName = "date")]
        public DateTime? PFActivationDate { get; set; }
        public bool? IsFBActive { get; set; }
        [Column(TypeName = "date")]
        public DateTime? FBActivationDate { get; set; }
        [StringLength(50)]
        public string TerminationStatus { get; set; }
        [Column(TypeName = "date")]
        public DateTime? TerminationDate { get; set; }
        public bool? CalculateProjectionTaxProratedBasis { get; set; }
        public bool? CalculateFestivalBonusTaxProratedBasis { get; set; }
        [StringLength(50)]
        public string PreviousCode { get; set; }
        [StringLength(50)]
        public string GlobalID { get; set; }



        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [StringLength(50, ErrorMessage = "StateStatus can't be longer than 50 characters")]
        public string StateStatus { get; set; }


    }
}
