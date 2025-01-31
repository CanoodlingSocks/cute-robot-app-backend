using DAL.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace DAL
{
    public class ChubbyBotDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Robot> Robots { get; set; }
        public DbSet<UserRobots> UserRobots { get; set; }
        public DbSet<RobotParts> RobotParts { get; set; }
        public DbSet<Parts> Parts { get; set; }
        public DbSet<RobotMetrics> RobotMetrics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=ChubbyBot.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            //Seed the admin user
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "lela",
                    Password = HashPassword("P@ssw0rd@S4lt"),
                    Nickname = "Lela",
                    Role = Role.Admin,
                    Address = "Default Address 42"
                }
            );

            // Seed normal user
            modelBuilder.Entity<User>().HasData(

                new User
                {
                    Id = 2,
                    Username = "joemama",
                    Password = HashPassword("iLik3@yoM0mma@jok3s"),
                    Nickname = "Joe",
                    Role = Role.User,
                    Address = "Somewhere Street 19"
                }
                );

            // Seed robot models

            modelBuilder.Entity<Robot>().HasData(
                new Robot
                {
                    Id = 1,
                    ModelName = "CHU-66Y",
                    RobotNickname = "ChubbyBot",
                    ManufacturingDate = new DateTime(2019, 1, 22),
                    Description = "Description for CHU-66Y",
                    Front_View = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/CHU-66Y/Front_View.png",
                    Left_View = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/CHU-66Y/Left_View.png",
                    Right_View = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobsCHU-66Y/Right_View.png",
                    Back_View = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobsCHU-66Y/Back_View.png"
                },

                new Robot
                {
                    Id = 2,
                    ModelName = "5-KY",
                    RobotNickname = "SkyBot",
                    ManufacturingDate = new DateTime(2020, 4, 6),
                    Description = "Description for 5-KY",
                    Front_View = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/5-KY/Front_View.png",
                    Left_View = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/5-KY/Left_View.png",
                    Right_View = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/5-KY/Right_View.png",
                    Back_View = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/5-KY/Back_View.png"
                });

            // Seed Parts

            modelBuilder.Entity<Parts>().HasData(
    new Parts
    {
        PartId = 1,
        PartName = "CHU-66Y Standard Head",
        Description = "Standard head part for the CHU-66Y model",
        Type = "Head",
        CloseUpImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/CHU-66Y/CloseUp_Standard_Head.png",
        PartImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/CHU-66Y/Standard_Head.png",
        Order = 6,
    },
    new Parts
    {
        PartId = 2,
        PartName = "CHU-66Y Standard Body",
        Description = "Standard body part for CHU-66Y model",
        Type = "Body",
        CloseUpImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/CHU-66Y/CloseUp_Standard_Body.png",
        PartImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/CHU-66Y/Standard_Body.png",
        Order = 1,
    },
    new Parts
    {
        PartId = 3,
        PartName = "CHU-66Y Standard Left Arm",
        Description = "Standard left arm part for CHU-66Y model",
        Type = "LeftArm",
        CloseUpImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/CHU-66Y/CloseUp_Standard_LeftArm.png",
        PartImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/CHU-66Y/Standard_LeftArm.png",
        Order = 5,
    },
    new Parts
    {
        PartId = 4,
        PartName = "CHU-66Y Standard Right Arm",
        Description = "Standard right arm part for CHU-66Y model",
        Type = "RightArm",
        CloseUpImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/CHU-66Y/CloseUp_Standard_RightArm.png",
        PartImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/CHU-66Y/Standard_RightArm.png",
        Order = 4,
    },
    new Parts
    {
        PartId = 5,
        PartName = "CHU-66Y Standard Left Leg",
        Description = "Standard left leg part for CHU-66Y model",
        Type = "LeftLeg",
        CloseUpImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/CHU-66Y/CloseUp_Standard_LeftLeg.png",
        PartImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/CHU-66Y/Standard_LeftLeg.png",
        Order = 3
    },
    new Parts
    {
        PartId = 6,
        PartName = "CHU-66Y Standard Right Leg",
        Description = "Standard right leg part for CHU-66Y model",
        Type = "RightLeg",
        CloseUpImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/CHU-66Y/CloseUp_Standard_RightLeg.png",
        PartImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/CHU-66Y/Standard_RightLeg.png",
        Order = 2,
    },

            //5-KY Parts Seeds
            new Parts
            {
                PartId = 7,
                PartName = "5-KY Standard Head",
                Description = "Standard head part for the 5-KY model",
                Type = "Head",
                CloseUpImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/5-KY/CloseUp_Standard_Head.png",
                PartImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/5-KY/Standard_Head.png",
                Order = 6,
            },
            new Parts
            {
                PartId = 8,
                PartName = "5-KY Standard Body",
                Description = "Standard body part for 5-KY model",
                Type = "Body",
                CloseUpImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/5-KY/CloseUp_Standard_Body.png",
                PartImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/5-KY/Standard_Body.png",
                Order = 5,
            },
            new Parts
            {
                PartId = 9,
                PartName = "5-KY Standard Left Arm",
                Description = "Standard left arm part for 5-KY model",
                Type = "LeftArm",
                CloseUpImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/5-KY/CloseUp_Standard_LeftArm.png",
                PartImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/5-KY/Standard_LeftArm.png",
                Order = 1,
            },
            new Parts
            {
                PartId = 10,
                PartName = "5-KY Standard Right Arm",
                Description = "Standard right arm part for 5-KY model",
                Type = "RightArm",
                CloseUpImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/5-KY/CloseUp_Standard_RightArm.png",
                PartImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/5-KY/Standard_RightArm.png",
                Order = 2,
            },
            new Parts
            {
                PartId = 11,
                PartName = "5-KY Standard Left Leg",
                Description = "Standard left leg part for 5-KY model",
                Type = "LeftLeg",
                CloseUpImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/5-KY/CloseUp_Standard_LeftLeg.png",
                PartImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/5-KY/Standard_LeftLeg.png",
                Order = 3,
            },
            new Parts
            {
                PartId = 12,
                PartName = "5-KY Standard Right Leg",
                Description = "Standard right leg part for 5-KY model",
                Type = "RightLeg",
                CloseUpImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/5-KY/CloseUp_Standard_RightLeg.png",
                PartImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/5-KY/Standard_RightLeg.png",
                Order = 4
            },

            //Seed Special Parts
            new Parts
            {
                PartId = 13,
                PartName = "5-KY StrongMan Right Arm",
                Description = "The Strongman Arm part is designed for heavier jobs and heavy lifts. Both the arm and claws are made of a really tough steel that can withstand lots of force and high heatlevels. The claws, while sturdy, are at the same time thin which allows for more precision.",
                Type = "RightArm",
                CloseUpImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/5-KY/CloseUp_StrongMan_RightArm.png",
                PartImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/5-KY/StrongMan_RightArm.png",
                Order = 4
            },
            new Parts
            {
                PartId = 14,
                PartName = "CHU-66Y Purr Mode",
                Description = "Introducing \"Purr Mode\" – your robot's playful transformation, where its head turns into a cat, purring like an engine and acts like an adorable cat :3",
                Type = "Head",
                CloseUpImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/CHU-66Y/CloseUp_Mode_Purr.png",
                PartImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/CHU-66Y/Mode_Purr.png",
                Order = 6
            },
            new Parts
            {
                PartId = 15,
                PartName = "CHU-66Y Light Mode",
                Type = "Head",
                Description = "Introducing \"Light Mode\" – your robot's practical transformation, where its eyes turn into two powerful flashlights. Illuminate dark spaces effortlessly and enhance productivity in low-light environments. A handy feature for those late-night tasks and dimly lit workspaces!",
                CloseUpImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/CHU-66Y/CloseUp_Mode_Light.png",
                PartImage = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/CHU-66Y/Mode_Light.png",
                Order = 6
            }
            );
            //Seed UserRobots
            modelBuilder.Entity<UserRobots>().HasData(
    new UserRobots
    {
        UniqueRobotCode = "987095ae-3ba6-4aa2-b29e-4b455afc0e3e",
        UserId = 1, // Reference to the admin user
        RobotId = 1, // Reference to CHU-66Y
        CustomRobotNickName = "ChubbyBot",
        IsBroken = false,
        IsActive = true,
        LastRepairDate = DateTime.Now,
        RepairCount = 2,
        HeadId = 1, //Standard CHU-66Y partId's
        BodyId = 2,
        LeftArmId = 3,
        RightArmId = 4,
        LeftLegId = 5,
        RightLegId = 6,
        Image_Background = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/Background%20Images/LivingRoom-4.png",
    },
       new UserRobots
       {
           UniqueRobotCode = "73ff3169-795d-47fb-97c9-5bcdcc53c184",
           UserId = 1, // Reference to the admin user
           RobotId = 2, // Reference to 5-KY
           CustomRobotNickName = "SkyBot",
           IsBroken = false,
           IsActive = true,
           LastRepairDate = DateTime.Now,
           RepairCount = 5,
           HeadId = 7, // 5-KY partId's
           BodyId = 8,
           LeftArmId = 9,
           RightArmId = 13, //StrongMan RightArm
           LeftLegId = 11,
           RightLegId = 12,
           Image_Background = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/Background%20Images/Bathroom-4.png",
       },
            new UserRobots
            {
                UniqueRobotCode = "5c68f532-a719-4934-8f44-b86597a92b8c",
                UserId = 1, // Reference to the admin user
                RobotId = 1, // Reference to CHU-66Y
                CustomRobotNickName = "LightBot",
                IsBroken = true,
                IsActive = false,
                LastRepairDate = DateTime.Now,
                RepairCount = 0,
                HeadId = 15, //Chubby Light Mode
                BodyId = 2,
                LeftArmId = 3,
                RightArmId = 4,
                LeftLegId = 5,
                RightLegId = 6,
                Image_Background = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/Background%20Images/ChildsRoom-2.png",
            });
            //Seed User with 2 robots
            modelBuilder.Entity<UserRobots>().HasData(
                       new UserRobots
                       {
                           UniqueRobotCode = "a9910612-f88e-4183-9cd4-82b1f13840f2",
                           UserId = 2, // Reference to the user
                           RobotId = 2, // Reference to 5-KY
                           CustomRobotNickName = "CoolBot",
                           IsBroken = false,
                           IsActive = true,
                           LastRepairDate = DateTime.Now,
                           RepairCount = 5,
                           HeadId = 7, // 5-KY partId's
                           BodyId = 8,
                           LeftArmId = 9,
                           RightArmId = 13, //StrongMan RightArm
                           LeftLegId = 11,
                           RightLegId = 12,
                           Image_Background = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/Background%20Images/LivingRoom-1.png", 
                       },
                        new UserRobots
                        {
                            UniqueRobotCode = "3daa9137-25df-4ddf-a12e-54d743178e87",
                            UserId = 2, // Reference to the user
                            RobotId = 1, // Reference to CHU-66Y
                            CustomRobotNickName = "CharlesBot",
                            IsBroken = true,
                            IsActive = false,
                            LastRepairDate = DateTime.Now,
                            RepairCount = 0,
                            HeadId = 14, //Chubby Purr Mode
                            BodyId = 2,
                            LeftArmId = 3,
                            RightArmId = 4,
                            LeftLegId = 5,
                            RightLegId = 6,
                            Image_Background = "BLOBSTORAGE_CONNECTIONSTRING/ChubbyBlobs/Background%20Images/Bathroom-1.png",
                        }
               );


            //Seed RobotParts

            modelBuilder.Entity<RobotParts>().HasData(
    // Head
    new RobotParts
    {
        RobotPartId = 1,
        RobotId = 1, // Ref to CHU-66Y Model
        PartId = 1
    },
    // Body
    new RobotParts
    {
        RobotPartId = 2,
        RobotId = 1,
        PartId = 2
    },
    // Left Arm
    new RobotParts
    {
        RobotPartId = 3,
        RobotId = 1,
        PartId = 3
    },
    // Right Arm
    new RobotParts
    {
        RobotPartId = 4,
        RobotId = 1,
        PartId = 4
    },
    // Left Leg
    new RobotParts
    {
        RobotPartId = 5,
        RobotId = 1,
        PartId = 5
    },
    // Right Leg
    new RobotParts
    {
        RobotPartId = 6,
        RobotId = 1,
        PartId = 6
    },


                    new RobotParts
                    {
                        RobotPartId = 7,
                        RobotId = 2, // Ref to 5-KY Model
                        PartId = 7
                    },
            // Body
            new RobotParts
            {
                RobotPartId = 8,
                RobotId = 2,
                PartId = 8
            },
            // Left Arm
            new RobotParts
            {
                RobotPartId = 9,
                RobotId = 2,
                PartId = 9
            },
            // Right Arm
            new RobotParts
            {
                RobotPartId = 10,
                RobotId = 2,
                PartId = 10
            },
            // Left Leg
            new RobotParts
            {
                RobotPartId = 11,
                RobotId = 2,
                PartId = 11
            },
            // Right Leg
            new RobotParts
            {
                RobotPartId = 12,
                RobotId = 2,
                PartId = 12
            }
            );

            // Seed RobotMetrics data
            modelBuilder.Entity<RobotMetrics>().HasData(
                new RobotMetrics
                {
                    Id = 1,
                    UniqueRobotCode = "987095ae-3ba6-4aa2-b29e-4b455afc0e3e",
                    BatteryLevel = 80.5m, 
                    Temperature = 25.0m,
                    PersonalityChipStability = 95.2m, 
                    InteractionLog = "Said 'Hello' to the neighbours.", 
                    TaskCompletionTime = TimeSpan.FromHours(2),
                    SentimentAnalysis = "Positive", 
                    Timestamp = DateTime.UtcNow
                },
                new RobotMetrics
                {
                    Id = 2,
                    UniqueRobotCode = "73ff3169-795d-47fb-97c9-5bcdcc53c184",
                    BatteryLevel = 43.8m,
                    Temperature = 32.2m,
                    PersonalityChipStability = 35.2m,
                    InteractionLog = "Had a scary interaction with a dog.",
                    TaskCompletionTime = TimeSpan.FromHours(4),
                    SentimentAnalysis = "Scared",
                    Timestamp = DateTime.UtcNow
                },
                new RobotMetrics
                {
                    Id = 3,
                    UniqueRobotCode = "5c68f532-a719-4934-8f44-b86597a92b8c",
                    BatteryLevel = 91.0m,
                    Temperature = 27.3m,
                    PersonalityChipStability = 98.2m,
                    InteractionLog = "Interacted with a group of school children",
                    TaskCompletionTime = TimeSpan.FromHours(1),
                    SentimentAnalysis = "Positive",
                    Timestamp = DateTime.UtcNow
                }
            );

            base.OnModelCreating(modelBuilder);
        }

        private string HashPassword(string password) //Exact the same method as in UserService, only for the purpose of seeding and hashing the admin/user password ;-)
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
    }
}
