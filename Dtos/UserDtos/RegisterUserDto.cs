namespace Psinder.Dtos.UserDtos
{
    public class RegisterUserDto
    {
        public string Name {  get; set; }
        public string Surename { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
