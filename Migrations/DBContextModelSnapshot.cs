// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using close_and_cheap.Data;

namespace close_and_cheap.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("close_and_cheap.Data.Entities.BusinessOwner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("BusinessOwners");
                });

            modelBuilder.Entity("close_and_cheap.Data.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Women's shoes"
                        },
                        new
                        {
                            Id = 2,
                            Name = "kids shoes"
                        },
                        new
                        {
                            Id = 3,
                            Name = "men shoes"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Children's clothing"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Restaurants"
                        },
                        new
                        {
                            Id = 6,
                            Name = "furniture"
                        },
                        new
                        {
                            Id = 7,
                            Name = "men clothes"
                        },
                        new
                        {
                            Id = 8,
                            Name = "jewelry"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Makeup"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Pets"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Accessories"
                        });
                });

            modelBuilder.Entity("close_and_cheap.Data.Entities.ClaimData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Type");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Value");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ClaimData");

                    b.HasData(
                        new
                        {
                            Id = 7,
                            Type = "name",
                            UserId = 3,
                            Value = "ruth"
                        },
                        new
                        {
                            Id = 8,
                            Type = "role",
                            UserId = 3,
                            Value = "Admin"
                        },
                        new
                        {
                            Id = 9,
                            Type = "userId",
                            UserId = 3,
                            Value = "3"
                        },
                        new
                        {
                            Id = 1,
                            Type = "name",
                            UserId = 4,
                            Value = "Shlomi"
                        },
                        new
                        {
                            Id = 2,
                            Type = "role",
                            UserId = 4,
                            Value = "User"
                        },
                        new
                        {
                            Id = 3,
                            Type = "userId",
                            UserId = 4,
                            Value = "4"
                        });
                });

            modelBuilder.Entity("close_and_cheap.Data.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "User"
                        });
                });

            modelBuilder.Entity("close_and_cheap.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.HasData(
                        new
                        {
                            Id = 4,
                            Address = "ירושלים, רמות",
                            Email = "shlomi@gmail",
                            Name = "Shlomi",
                            Password = "123456",
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("close_and_cheap.Entities.Admin", b =>
                {
                    b.HasBaseType("close_and_cheap.Data.Entities.User");

                    b.HasDiscriminator().HasValue("Admin");

                    b.HasData(
                        new
                        {
                            Id = 3,
                            Email = "ruth@gmail.com",
                            Name = "ruth",
                            Password = "123456",
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("close_and_cheap.Data.Entities.BusinessOwner", b =>
                {
                    b.HasOne("close_and_cheap.Data.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("close_and_cheap.Data.Entities.ClaimData", b =>
                {
                    b.HasOne("close_and_cheap.Data.Entities.User", "User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("close_and_cheap.Data.Entities.User", b =>
                {
                    b.HasOne("close_and_cheap.Data.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("close_and_cheap.Data.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("close_and_cheap.Data.Entities.User", b =>
                {
                    b.Navigation("Claims");
                });
#pragma warning restore 612, 618
        }
    }
}
