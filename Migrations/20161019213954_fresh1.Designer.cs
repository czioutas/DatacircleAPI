using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DatacircleAPI.Database;

namespace DatacircleAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161019213954_fresh1")]
    partial class fresh1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DatacircleAPI.Models.Address", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("City")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Country")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("PostCode")
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Salutation");

                    b.Property<string>("Street")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<string>("phone")
                        .HasColumnType("varchar(100)");

                    b.HasKey("ID");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("DatacircleAPI.Models.Company", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("ID");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("DatacircleAPI.Models.ConnectionDetails", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("ConnectionString")
                        .HasColumnType("varchar(250)");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Database")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Host")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<int>("Port");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<string>("Username")
                        .HasColumnType("varchar(250)");

                    b.HasKey("ID");

                    b.ToTable("ConnectionDetails");
                });

            modelBuilder.Entity("DatacircleAPI.Models.Datasource", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<int>("ConnectionDetailsFk");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Type");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("ID");

                    b.HasIndex("ConnectionDetailsFk");

                    b.ToTable("Datasource");
                });

            modelBuilder.Entity("DatacircleAPI.Models.Metric", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<int>("ChartType");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("DatasourceFk");

                    b.Property<string>("Description");

                    b.Property<string>("Query");

                    b.Property<string>("Title");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("ID");

                    b.HasIndex("DatasourceFk");

                    b.ToTable("Metric");
                });

            modelBuilder.Entity("DatacircleAPI.Models.Newsletter", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("ID");

                    b.ToTable("Newsletter");
                });

            modelBuilder.Entity("DatacircleAPI.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Admin");

                    b.Property<int>("ComapnyFk");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<bool>("DashboardRead");

                    b.Property<bool>("DashboardWrite");

                    b.Property<bool>("DatasourceRead");

                    b.Property<bool>("DatasourceWrite");

                    b.Property<bool>("MetricRead");

                    b.Property<bool>("MetricWrite");

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<bool>("WidgetRead");

                    b.Property<bool>("WidgetWrite");

                    b.HasKey("Id");

                    b.HasIndex("ComapnyFk");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("DatacircleAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<int>("AddressFk");

                    b.Property<int>("ComapnyFk");

                    b.Property<int?>("CompanyFk");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsCompanyOwner");

                    b.Property<bool>("IsVerified");

                    b.Property<string>("LastName")
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("MiddleName")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<int>("RoleFk");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Token")
                        .HasColumnType("varchar(250)");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("VerificationKey")
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("AddressFk");

                    b.HasIndex("CompanyFk");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasName("UserNameIndex");

                    b.HasIndex("RoleFk");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("DatacircleAPI.Models.Datasource", b =>
                {
                    b.HasOne("DatacircleAPI.Models.ConnectionDetails", "ConnectionDetails")
                        .WithMany()
                        .HasForeignKey("ConnectionDetailsFk");
                });

            modelBuilder.Entity("DatacircleAPI.Models.Metric", b =>
                {
                    b.HasOne("DatacircleAPI.Models.Datasource", "Datasource")
                        .WithMany()
                        .HasForeignKey("DatasourceFk");
                });

            modelBuilder.Entity("DatacircleAPI.Models.Role", b =>
                {
                    b.HasOne("DatacircleAPI.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("ComapnyFk");
                });

            modelBuilder.Entity("DatacircleAPI.Models.User", b =>
                {
                    b.HasOne("DatacircleAPI.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressFk");

                    b.HasOne("DatacircleAPI.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyFk");

                    b.HasOne("DatacircleAPI.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleFk");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("DatacircleAPI.Models.Role")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("DatacircleAPI.Models.User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("DatacircleAPI.Models.User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<int>", b =>
                {
                    b.HasOne("DatacircleAPI.Models.Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.HasOne("DatacircleAPI.Models.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId");
                });
        }
    }
}
