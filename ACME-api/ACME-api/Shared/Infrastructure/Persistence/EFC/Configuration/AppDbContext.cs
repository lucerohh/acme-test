using ACME_api.ACME.Domain.Model.Aggregates;
using ACME_api.ACME.Domain.Model.Entities;
using ACME_api.Shared.Domain.Aggregates;
using ACME_api.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ACME_api.Shared.Infrastructure.Persistence.EFC.Configuration
{
    /// <summary>
    ///     Application database context
    /// </summary>
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<ACME.Domain.Model.Entities.TaskItem> Tasks { get; set; }
        public DbSet<TaskCategory> TaskCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            // Add the created and updated interceptor
            builder.AddCreatedUpdatedInterceptor();
            base.OnConfiguring(builder);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries<BaseEtity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().HasKey(f => f.Id);
            builder.Entity<User>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(f => f.FirstName).IsRequired();
            builder.Entity<User>().Property(f => f.LastName).IsRequired();
            builder.Entity<User>().Property(f => f.Email).IsRequired();
            builder.Entity<User>().Property(f => f.Password).IsRequired();
            //builder.Entity<User>().HasMany(u => u.Tasks)
            //        .WithOne(t => t.User)
            //        .HasForeignKey(t => t.UserId);

            builder.Entity<TaskItem>().HasKey(t => t.Id);
            builder.Entity<TaskItem>().Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<TaskItem>().Property(t => t.Title).IsRequired();
            builder.Entity<TaskItem>().Property(t => t.Status).IsRequired();
            builder.Entity<TaskItem>().Property<byte[]>(t => t.RowVersion).IsRowVersion();
            builder.Entity<TaskItem>().HasOne(t => t.TaskCategory)
                    .WithMany(c => c.Tasks)
                    .HasForeignKey(t => t.TaskCategoryId)
                    .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<TaskItem>().HasOne(t => t.User)
                    .WithMany(u => u.Tasks)
                    .HasForeignKey(t => t.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<TaskItem>()
                    .Property(t => t.Status)
                    .HasConversion<string>();

            builder.Entity<TaskCategory>().HasKey(t => t.Id);
            builder.Entity<TaskCategory>().Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<TaskCategory>().Property(t => t.Name).IsRequired();
            builder.Entity<TaskCategory>().HasOne(c => c.User)
                    .WithMany()
                    .HasForeignKey(c => c.UserId);

            builder.UseSnakeCaseNamingConvention();
        }
    }
}
