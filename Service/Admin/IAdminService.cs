using Service.DTO;

namespace Service.Admin
{
    public interface IAdminService
    {
       public Task<AdminAddNewPartDTO> AddNewPartAsync(AdminAddNewPartDTO partDTO);
        public Task<AdminAddNewRobotDTO> AddNewRobotAsync(AdminAddNewRobotDTO robotDTO);
        public Task<List<PartDTO>> GetPartsAsync(PartFilterDTO filterOptions);
        public Task<bool> DeletePartAsync(int partId);
    }
}
