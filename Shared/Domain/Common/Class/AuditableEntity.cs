using System.ComponentModel.DataAnnotations;


namespace Shared.Domain.Common.Class
{
    public class AuditableEntity
    {

        // Create
        [StringLength(100)]
        public virtual string CreatedBy { get; set; }
        public virtual Nullable<DateTime> CreatedDate { get; set; }


        // Update
        [StringLength(100)]
        public virtual string UpdatedBy { get; set; }
        public virtual Nullable<DateTime> UpdatedDate { get; set; }


        // Delete
        [StringLength(100)]
        public virtual string DeletedBy { get; set; }
        public virtual Nullable<DateTime> DeletedDate { get; set; }


    }
}
