using BeltExam.Models;
using Microsoft.EntityFrameworkCore;
 
namespace BeltExam.Models
{
    public class BeltContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public BeltContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users {get;set;}
        public DbSet<Activity> Activity {get;set;}

        public DbSet<RSVP> RSVP {get;set;}

    }
}