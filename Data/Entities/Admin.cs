using close_and_cheap.Data.Entities;

namespace close_and_cheap.Entities
{
    public class Admin : User
    {
        public Admin()
        {
        }
        public Admin(int Id, string name, string Email, string Password, int RoleId)
            : base(Id, name, Email,  Password, RoleId)
        {

        }
    }
}
