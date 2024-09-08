using System.ComponentModel.DataAnnotations;


namespace Shared.Domain.Common.Interface
{
    public interface IAuditableEntity
    {
        [StringLength(100)]
        string CreatedBy { get; set; }
        DateTime? CreatedDate { get; set; }



        [StringLength(100)]
        string UpdatedBy { get; set; }
        DateTime? UpdatedDate { get; set; }



        [StringLength(100)]
        string DeletedBy { get; set; }
        DateTime? DeletedDate { get; set; }


    }
}
