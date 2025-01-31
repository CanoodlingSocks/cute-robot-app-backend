using DAL.Models;

namespace DAL
{
    public interface IUserAccess
    {
        public IEnumerable<User> LoginUser(string username);
        public List<User> ReadUsersData();
        public void UserToFile(User user);
    }
}
