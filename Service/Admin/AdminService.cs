using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Service.DTO;

namespace Service.Admin
{
    public class AdminService : IAdminService
    {
        public async Task<AdminAddNewPartDTO> AddNewPartAsync(AdminAddNewPartDTO partDTO)
        {
            var newPart = new Parts
            {
                PartName = partDTO.PartName,
                Description = partDTO.Description,
                Type = partDTO.Type,
                CloseUpImage = partDTO.CloseUpImage,
                PartImage = partDTO.PartImage,
                Order = partDTO.Order
            };

            using (var context = new ChubbyBotDbContext())
            {
                context.Parts.Add(newPart);
                await context.SaveChangesAsync();
            }
            return partDTO;     
        }

        public async Task<AdminAddNewRobotDTO> AddNewRobotAsync(AdminAddNewRobotDTO robotDTO)
        {
            var newRobot = new DAL.Models.Robot // <-- Writing just Robot seems to point to Service.Robot
            {
                ModelName = robotDTO.ModelName,
                RobotNickname = robotDTO.RobotNickname,
                ManufacturingDate = robotDTO.ManufacturingDate,
                Description = robotDTO.Description,
                Front_View = robotDTO.Front_View,
                Left_View = robotDTO.Left_View,
                Right_View = robotDTO.Right_View,
                Back_View = robotDTO.Back_View
            };

            using (var context = new ChubbyBotDbContext())
            {
                context.Robots.Add(newRobot);
                await context.SaveChangesAsync(); 
            }
            return robotDTO;
        }

        public async Task<List<PartDTO>> GetPartsAsync(PartFilterDTO filterOptions)
        {
            using (var context = new ChubbyBotDbContext())
            {
                var query = context.Parts.AsQueryable();

                if (filterOptions.Type != "All")
                {
                    query = query.Where(p => p.Type == filterOptions.Type);
                }

                if (filterOptions.SortBy == "Part Name")
                {
                    query = query.OrderBy(p => p.PartName);
                }
                else if (filterOptions.SortBy == "Type") 
                {
                    query = query.OrderBy(p => p.Type);
                }
                else
                {
                    query = query.OrderBy(p => p.Order);
                }

                if (!string.IsNullOrEmpty(filterOptions.SearchKeyWord))
                {
                    query = query.Where(p => p.PartName.Contains(filterOptions.SearchKeyWord));
                }

                var parts = await query
                    .Select(p => new PartDTO
                    {
                        Id = p.PartId,
                        PartName = p.PartName,
                        Description = p.Description,
                        Type = p.Type,
                        CloseUpImage = p.CloseUpImage,
                        PartImage = p.PartImage,
                        Order = p.Order,
                    })
                    .ToListAsync();

                return parts;
            }
        }

        public async Task<bool> DeletePartAsync(int partId)
        {
            using (var context = new ChubbyBotDbContext())
            {
                var partToDelete = await context.Parts.FindAsync(partId);
                if (partToDelete != null)
                {
                    context.Parts.Remove(partToDelete);
                    await context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}

