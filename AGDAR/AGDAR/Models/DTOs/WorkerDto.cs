namespace AGDAR.Models.DTO
{
    public class WorkerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SeckondName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int RoleId { get; set; }
        public string? Role { get; set; }
    }
}
