using System.ComponentModel.DataAnnotations;

public class Create
{
    [StringLength(100, ErrorMessage = "CreatedBy cannot be longer than 100 characters.")]
    public string CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool IsActive { get; set; } = false; // Default is false

    [StringLength(50, ErrorMessage = "StateStatus can't be longer than 50 characters.")]
    public string StateStatus { get; set; }
}

public class Update : Create
{
    [StringLength(100, ErrorMessage = "UpdatedBy cannot be longer than 100 characters.")]
    public string UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }
}

public class Delete : Update
{
    public bool IsDeleted { get; set; } = false; // Default is false

    [StringLength(100, ErrorMessage = "DeletedBy cannot be longer than 100 characters.")]
    public string DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }
}

public class Restore : Delete
{
    public bool IsRestored { get; set; } = false; // Default is false

    [StringLength(100, ErrorMessage = "RestoredBy cannot be longer than 100 characters.")]
    public string RestoredBy { get; set; }

    public DateTime? RestoredDate { get; set; }
}

public class Activate : Restore
{
    public bool IsActivated { get; set; } = false; // Default is false

    [StringLength(100, ErrorMessage = "ActivatedBy cannot be longer than 100 characters.")]
    public string ActivatedBy { get; set; }

    public DateTime? ActivatedDate { get; set; }
}

public class Deactivate : Activate
{
    public bool IsDeactivated { get; set; } = false; // Default is false

    [StringLength(100, ErrorMessage = "DeactivatedBy cannot be longer than 100 characters.")]
    public string DeactivatedBy { get; set; }

    public DateTime? DeactivatedDate { get; set; }
}

public class Approve : Deactivate
{
    public bool IsApproved { get; set; } = false; // Default is false

    [StringLength(100, ErrorMessage = "ApprovedBy cannot be longer than 100 characters.")]
    public string ApprovedBy { get; set; }

    public DateTime? ApprovedDate { get; set; }
}

public class Reject : Approve
{
    public bool IsRejected { get; set; } = false; // Default is false

    [StringLength(100, ErrorMessage = "RejectedBy cannot be longer than 100 characters.")]
    public string RejectedBy { get; set; }

    public DateTime? RejectedDate { get; set; }
}

public class Cancel : Reject
{
    public bool IsCancelled { get; set; } = false; // Default is false

    [StringLength(100, ErrorMessage = "CancelledBy cannot be longer than 100 characters.")]
    public string CancelledBy { get; set; }

    public DateTime? CancelledDate { get; set; }
}
