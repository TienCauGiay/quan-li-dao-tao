using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace quan_li_dao_tao.Models
{
    public partial class QLSVDbContext : DbContext
    {
        public QLSVDbContext()
            : base("name=PostgresConnection")
        {
        }

        public virtual DbSet<classes> CLASSes { get; set; }
        public virtual DbSet<faculty> FACULTies { get; set; }
        public virtual DbSet<register_subject> REGISTERSUBJECTs { get; set; }
        public virtual DbSet<role> Roles { get; set; }
        public virtual DbSet<score> SCOREs { get; set; }
        public virtual DbSet<semester> SEMESTERs { get; set; }
        public virtual DbSet<student> STUDENTs { get; set; }
        public virtual DbSet<subject> SUBJECTs { get; set; }
        public virtual DbSet<teacher> TEACHERs { get; set; }
        public virtual DbSet<users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.Entity<role>()
                .HasMany(navigationPropertyExpression: e => e.Users)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<student>()
                .HasMany(e => e.REGISTERSUBJECTs)
                .WithRequired(e => e.STUDENT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<student>()
                .HasMany(e => e.SCOREs)
                .WithRequired(e => e.STUDENT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<subject>()
                .HasMany(e => e.SCOREs)
                .WithRequired(e => e.SUBJECT)
                .WillCascadeOnDelete(false);
        }
    }
}