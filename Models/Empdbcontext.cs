using Microsoft.EntityFrameworkCore;



namespace Employewebapp.Models
{
    public class Empdbcontext : DbContext
    {
        public DbSet<CreateEmployee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Logviewmodel> Logviewmodel { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        public Empdbcontext(DbContextOptions options) : base(options)
        {
        }
    }
}
