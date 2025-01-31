using Service.DTO;

namespace Service.Robot
{
    public interface IRobotService 
    {
        public List<UserRobotDTO> GetAllUserRobots();
        public List<UserRobotDTO> GetRobotByID(string uniqueRobotCode);
        public Task<bool> UpdateNicknameAsync(UpdateNicknameDTO updateNicknameDto);
        public Task<bool> UpdateBackgroundImageAsync(string uniqueRobotCode, string imageUrl);
    }
}
