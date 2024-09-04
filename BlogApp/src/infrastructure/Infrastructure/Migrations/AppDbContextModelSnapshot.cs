using System.Diagnostics.CodeAnalysis;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace Infrastructure.Migrations;

[DbContext(typeof(AppDbContext))]
[ExcludeFromCodeCoverage]
partial class AppDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.8")
            .HasAnnotation("Relational:MaxIdentifierLength", 63);

        NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

        modelBuilder.Entity("Domain.Entities.Article", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer");

                NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                b.Property<int>("AuthorId")
                    .HasColumnType("integer");

                b.Property<string>("Body")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("Description")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<bool>("Favorited")
                    .HasColumnType("boolean");

                b.Property<int>("FavoritesCount")
                    .HasColumnType("integer");

                b.Property<string>("Slug")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<List<string>>("Tags")
                    .HasColumnType("text[]");

                b.Property<string>("Title")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<DateTime>("UpdatedAt")
                    .HasColumnType("timestamp with time zone");

                b.HasKey("Id");

                b.ToTable("Articles");
            });

        modelBuilder.Entity("Domain.Entities.Comment", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer");

                NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                b.Property<int>("AuthorId")
                    .HasColumnType("integer");

                b.Property<string>("Body")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("timestamp with time zone");

                b.Property<DateTime>("UpdatedAt")
                    .HasColumnType("timestamp with time zone");

                b.HasKey("Id");

                b.ToTable("Comments");
            });

        modelBuilder.Entity("Domain.Entities.User", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer");

                NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                b.Property<string>("Bio")
                    .HasColumnType("text");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<bool>("Following")
                    .HasColumnType("boolean");

                b.Property<string>("Image")
                    .HasColumnType("text");

                b.Property<string>("Password")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<string>("UserName")
                    .IsRequired()
                    .HasColumnType("text");

                b.HasKey("Id");

                b.ToTable("Users");
            });
    }
}
