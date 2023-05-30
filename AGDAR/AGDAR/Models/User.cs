namespace AGDAR.Models
{
    public abstract class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SeckondName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Password { get; set; }
    }
}
