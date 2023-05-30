namespace AGDAR.Models
{
    public class Worker : User
    {
        public int RoleId { get; set; }
        public virtual Role Role { get; set; } = null!;
    }
}
