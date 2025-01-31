namespace Service.DTO
{
    public class UserRobotDTO
    {
        public string UniqueRobotCode { get; set; }
        public string ModelName { get; set; }
        public string RobotNickname { get; set; }
        public bool IsBroken { get; set; }
        public bool IsActive { get; set; }
        public int RepairCount { get; set; }
        public DateTime? LastRepairDate { get; set; }
        public string OwnerNickname { get; set; }
        public string Image_Background { get; set; }
    }
}
