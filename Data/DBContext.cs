
using close_and_cheap.Data.Entities;
using close_and_cheap.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace close_and_cheap.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
               .ToTable("Users")
               .HasDiscriminator();

            modelBuilder.Entity<User>()
            .HasOne<Role>(s => s.Role)
            .WithMany(ad => ad.Users)
            .HasForeignKey(fk => fk.RoleId
            );

            Role role1 = new Role(1, "Admin");

            Role role2 = new Role(2, "User");
            modelBuilder.Entity<Role>().HasData(role1, role2);

            modelBuilder.Entity<User>()
            .HasMany(u => u.Claims)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId);


            Admin adm = new Admin(3, "ruth", "ruth@gmail.com", "123456", 1);
            modelBuilder.Entity<Admin>().HasData(adm);

            ClaimData claim1adm = new ClaimData(7, "name", adm.Name, adm.Id);
            ClaimData claim2adm = new ClaimData(8, "role", role1.Name, adm.Id);
            ClaimData claim3adm = new ClaimData(9, "userId", adm.Id.ToString(), adm.Id);

            User user = new User(4, "Shlomi", "shlomi@gmail", "123456", "ירושלים, רמות", 2);

            modelBuilder.Entity<User>().HasData(user);
            ClaimData claim1stu = new ClaimData(1, "name", user.Name, user.Id);
            ClaimData claim2stu = new ClaimData(2, "role", role2.Name, user.Id);
            ClaimData claim3stu = new ClaimData(3, "userId", user.Id.ToString(), user.Id);

            modelBuilder.Entity<ClaimData>().HasData(claim1adm, claim2adm, claim3adm, claim1stu, claim2stu, claim3stu);
            List<Category> categories = getCategoriesData();

     
                modelBuilder.Entity<Category>().HasData(categories);
            
        }

        private List<Category> getCategoriesData()
        {
            string path = "C:\\Users\\Comp1\\Desktop\\פרוייקט עדכני\\NewProject\\currentProj\\close-and-cheap\\Extensions\\CategorySchema.json";

            string jsonFromFile;
            using (StreamReader reader = new StreamReader(path))
            {
                jsonFromFile = reader.ReadToEnd();
            }// fill which table do you want!
            return JsonConvert.DeserializeObject<List<Category>>(jsonFromFile);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<BusinessOwner> BusinessOwners { get; set; }
        public DbSet<Category> Categories { get; set; }
        public virtual DbSet<Role> Role { get; set; }

        public virtual DbSet<ClaimData> ClaimData { get; set; }
    }
}
