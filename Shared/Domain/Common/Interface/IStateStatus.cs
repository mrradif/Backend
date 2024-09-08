
public interface IStateStatus
{
    bool IsActive { get; set; }
    bool IsDeleted { get; set; }
    string StateStatus { get; set; }
}