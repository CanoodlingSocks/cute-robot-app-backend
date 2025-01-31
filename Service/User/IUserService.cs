using Service.DTO;

namespace Service.User
{
    public interface IUserService
    {
        public List<ListUserRobotsDTO> ListUserRobots(int userId);
        public SuccessfulLoginDTO UserLogin(UserLoginDTO login);
        public IEnumerable<UserDTO> GetAllUsers();
        public string GetUserRole(SuccessfulLoginDTO user);
        public Task<UserRobotImagesDTO> GetUserRobotImagesAsync(string uniqueRobotCode);
        public IEnumerable<UserDTO> GetUserById(int userId);
    }
}
