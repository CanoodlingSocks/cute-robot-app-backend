namespace Service.DTO
{
    public class SuccessfulLoginDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; } //Admin or not
    }
}
