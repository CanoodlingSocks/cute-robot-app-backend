using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Service.DTO;
using Sprache;
using SQLitePCL;

namespace Service.Robot
{
    public class RobotService : IRobotService
    {

        public List<UserRobotDTO> GetAllUserRobots()
        {
            using (var context = new ChubbyBotDbContext())
            {

                var userRobots = context.UserRobots
                .Include(u => u.Robot)
                .Include(u => u.User)
                .ToList();

                var userRobotDtos = userRobots.Select(u => new UserRobotDTO
                {
                    UniqueRobotCode = u.UniqueRobotCode,
                    ModelName = u.Robot.ModelName,
                    RobotNickname = u.CustomRobotNickName ?? u.Robot.RobotNickname,
                    IsBroken = u.IsBroken,
                    IsActive = u.IsActive,
                    RepairCount = u.RepairCount,
                    LastRepairDate = u.LastRepairDate,
                    OwnerNickname = u.User.Nickname
                }).ToList();

                return userRobotDtos;
            }
        }

        public List<UserRobotDTO> GetRobotByID(string uniqueRobotCode)
        {
            using (var context = new ChubbyBotDbContext())
            {
                var robot = context.UserRobots.Where(r => r.UniqueRobotCode == uniqueRobotCode)
                    .Include(u => u.Robot)
                    .Include(u => u.User)
                    .ToList();

                var robotDto = robot.Select(u => new UserRobotDTO
                {
                    UniqueRobotCode = u.UniqueRobotCode,
                    ModelName = u.Robot.ModelName,
                    RobotNickname = u.CustomRobotNickName,
                    IsActive = u.IsActive,
                    IsBroken = u.IsBroken,
                    RepairCount = u.RepairCount,
                    LastRepairDate = u.LastRepairDate,
                    OwnerNickname = u.User.Nickname,
                    Image_Background = u.Image_Background,
                }).ToList();
                return robotDto;
            }
        }

        public async Task<bool> UpdateNicknameAsync(UpdateNicknameDTO updateNicknameDto)
        {
            using (var context = new ChubbyBotDbContext())
            {
                var userRobot = await context.UserRobots
                    .FirstOrDefaultAsync(ur => ur.UniqueRobotCode == updateNicknameDto.UniqueRobotCode);

                if (userRobot != null)
                {
                    userRobot.CustomRobotNickName = updateNicknameDto.NewNickname;
                    await context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
        }

        public async Task<bool> UpdateBackgroundImageAsync(string uniqueRobotCode, string imageUrl)
        {
            using (var context = new ChubbyBotDbContext())
            {
                try
                {
                    var userRobot = await context.UserRobots
                        .Where(ur => ur.UniqueRobotCode == uniqueRobotCode)
                        .FirstOrDefaultAsync();

                    if (userRobot != null)
                    {
                        userRobot.Image_Background = imageUrl;
                        context.Entry(userRobot).State = EntityState.Modified;
                        await context.SaveChangesAsync();

                        return true;
                    }

                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
    }
}


