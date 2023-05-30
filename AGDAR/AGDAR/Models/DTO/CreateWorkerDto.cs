namespace AGDAR.Models.DTO
{
    public class CreateWorkerDto
    {
        public string Name { get; set; }
        public string SeckondName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Password { get; set; }
        //public int RoleId { get; set; }
    }
}
