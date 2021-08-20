namespace EmloyeeManagement.Entities.Concrete
{
    public interface IEmployee
    {
        string Email { get; set; }
        string Gender { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        string Status { get; set; }
    }
}