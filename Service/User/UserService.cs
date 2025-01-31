using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Service.DTO;
using Service.User;
using System.Security.Cryptography;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            using (var context = new ChubbyBotDbContext())
            {
                var users = context.Users.ToList();

                var userDTOs = users.Select(user => new UserDTO
                {
                    Id = user.Id,
                    Username = user.Username,
                    Nickname = user.Nickname,
                });

                return userDTOs;
            }
        }

        public IEnumerable<UserDTO> GetUserById(int userId) 
        {
            using (var context = new ChubbyBotDbContext())
            {
                try
                {
                    var user = context.Users.Where(u => u.Id == userId).ToList();
                    var userDTO = user.Select(user => new UserDTO
                    {
                        Id = user.Id,
                        Username = user.Username,
                        Nickname = user.Nickname,
                    });

                    return userDTO;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public List<ListUserRobotsDTO> ListUserRobots(int userId)
        {
            using (var context = new ChubbyBotDbContext())
            {
                try
                {
                    var usersRobots = context.UserRobots
                         .Include(ur => ur.Robot)
                         .Where(ur => ur.UserId == userId)
                         .Select(ur => new ListUserRobotsDTO
                         {
                             UniqueRobotCode = ur.UniqueRobotCode,
                             RobotNickname = ur.CustomRobotNickName,
                             ModelName = ur.Robot.ModelName,
                             IsActive = ur.IsActive,
                             IsBroken = ur.IsBroken,
                             Image_Background = ur.Image_Background,
                         })
                         .ToList();
                    return usersRobots;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<UserRobotImagesDTO> GetUserRobotImagesAsync(string uniqueRobotCode)
        {
            using (var context = new ChubbyBotDbContext())
            {
                try
                {
                    var userRobotData = await context.UserRobots
                .Where(ur => ur.UniqueRobotCode == uniqueRobotCode)
                .FirstOrDefaultAsync();

                    if (userRobotData == null)
                    {
                        throw new Exception("No Robot Data was found");
                    }

                    var partImageUrls = new List<string>();

                    // Determine the base URL based on the environment
                    string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    string baseUrl = _configuration.GetSection($"BaseUrls:{environmentName}").Value;

                    // Head
                    if (userRobotData.HeadId != null)
                    {
                        var headPart = await context.Parts
                            .Where(p => p.PartId == userRobotData.HeadId)
                            .Select(p => p.PartImage)
                            .FirstOrDefaultAsync();

                        if (!string.IsNullOrEmpty(headPart))
                        {
                            partImageUrls.Add(headPart);
                        }
                    }

                    // Body
                    if (userRobotData.BodyId != null)
                    {
                        var bodyPart = await context.Parts
                            .Where(p => p.PartId == userRobotData.BodyId)
                            .Select(p => p.PartImage)
                            .FirstOrDefaultAsync();

                        if (!string.IsNullOrEmpty(bodyPart))
                        {
                            partImageUrls.Add(bodyPart);
                        }
                    }

                    // RightArm
                    if (userRobotData.RightArmId != null)
                    {
                        var rightArmPart = await context.Parts
                            .Where(p => p.PartId == userRobotData.RightArmId)
                            .Select(p => p.PartImage)
                            .FirstOrDefaultAsync();

                        if (!string.IsNullOrEmpty(rightArmPart))
                        {
                            partImageUrls.Add(rightArmPart);
                        }
                    }

                    // LeftArm
                    if (userRobotData.LeftArmId != null)
                    {
                        var leftArmPart = await context.Parts
                            .Where(p => p.PartId == userRobotData.LeftArmId)
                            .Select(p => p.PartImage)
                            .FirstOrDefaultAsync();

                        if (!string.IsNullOrEmpty(leftArmPart))
                        {
                            partImageUrls.Add(leftArmPart);
                        }

                    }

                    // RightLeg
                    if (userRobotData.RightLegId != null)
                    {
                        var rightLegPart = await context.Parts
                            .Where(p => p.PartId == userRobotData.RightLegId)
                            .Select(p => p.PartImage)
                            .FirstOrDefaultAsync();

                        if (!string.IsNullOrEmpty(rightLegPart))
                        {
                            partImageUrls.Add(rightLegPart);
                        }
                    }

                    // LeftLeg
                    if (userRobotData.LeftLegId != null)
                    {
                        var leftLegPart = await context.Parts
                            .Where(p => p.PartId == userRobotData.LeftLegId)
                            .Select(p => p.PartImage)
                            .FirstOrDefaultAsync();

                        if (!string.IsNullOrEmpty(leftLegPart))
                        {
                            partImageUrls.Add(leftLegPart);
                        }
                    }


                    // Ordered parts based on userRobotData
                    var orderedParts = new List<Parts>();

                    var partPropertyMap = new Dictionary<string, Func<int?>>
                    {
                        { "Head", () => userRobotData.HeadId },
                        { "Body", () => userRobotData.BodyId },
                        { "LeftArm", () => userRobotData.LeftArmId },
                        { "RightArm", () => userRobotData.RightArmId },
                        { "LeftLeg", () => userRobotData.LeftLegId },
                        { "RightLeg", () => userRobotData.RightLegId }
                    };

                    foreach (var partType in partPropertyMap.Keys)
                    {
                        var partId = partPropertyMap[partType]();

                        if (partId != null)
                        {
                            var part = await context.Parts
                                .Where(p => p.PartId == partId)
                                .Select(p => new { p.PartImage, p.Order })
                                .FirstOrDefaultAsync();

                            if (part != null)
                            {
                                orderedParts.Add(new Parts
                                {
                                    PartImage = part.PartImage,
                                    Order = part.Order
                                });
                            }
                        }
                    }

                    // Clear partImageUrls and sort orderedParts by the Order property
                    partImageUrls.Clear();
                    orderedParts = orderedParts.OrderBy(p => p.Order).ToList();

                    foreach (var part in orderedParts)
                    {
                        if (!string.IsNullOrEmpty(part.PartImage))
                        {
                            partImageUrls.Add(part.PartImage);
                        }
                    }

                    var userRobotImages = new UserRobotImagesDTO
                    {
                        PartImageUrls = partImageUrls
                    };

                    return userRobotImages;
                }
                catch (Exception ex)
                {
                    throw new Exception("Backend Robot Image Fetch Error: " + ex.Message);
                }
            }
        }

        public SuccessfulLoginDTO UserLogin(UserLoginDTO login)
        {
            using (var context = new ChubbyBotDbContext())
            {
                try
                {
                    var user = context.Users
                        .Where(u => u.Username == login.Username)
                        .FirstOrDefault();

                    if (user == null) throw new Exception("User was not found");

                    var passwordMatch = PasswordDecryption(login.Password, user.Password);

                    if (passwordMatch)
                    {
                        return new SuccessfulLoginDTO()
                        {
                            Id = user.Id,
                            Username = login.Username,
                            Role = user.Role.ToString(),
                        };
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public string GetUserRole(SuccessfulLoginDTO userLogin)
        {
            using (var context = new ChubbyBotDbContext())
            {
                var users = context.Users.ToList();

                var user = users
                    .Where(u => u.Id == userLogin.Id)
                    .FirstOrDefault();

                var role = user.Role.ToString();
                return role;
            }
        }

        // Pbkdf2 (Password-Based Key Derivation Function 2) with HMACSHA256 as the underlying cryptographic hash function
        private string PasswordEncryption(string password)
        {
            byte[] salt = new byte[128 / 8];

            using var rng = RandomNumberGenerator.Create();
            rng.GetNonZeroBytes(salt);

            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            hashedPassword = hashedPassword + "@" + Convert.ToBase64String(salt);
            return hashedPassword;
        }

        private bool PasswordDecryption(string enteredPassword, string passwordHash)
        {
            string salt = passwordHash.Split('@')[1];
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: enteredPassword,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return passwordHash == hashedPassword + "@" + salt;
        }
    }
}
