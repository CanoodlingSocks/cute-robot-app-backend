namespace Service.DTO
{
    public class ListUserRobotsDTO
    {
        public string UniqueRobotCode { get; set; }
        public string RobotNickname { get; set; }
        public string ModelName { get; set; }
        public bool IsBroken { get; set; }
        public bool IsActive { get; set; }
        public string Image_Background { get; set; }
    }
}
