namespace AGDAR.Models.DTO
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? SeckondName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? OrderdId { get; set; }
    }
}
