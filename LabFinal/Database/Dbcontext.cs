using LabFinal.Models;
using Microsoft.EntityFrameworkCore; // Impoet EF


namespace LabFinal.Database
{
    public class DataDbcontext:DbContext
    {
        // Constructure Method
        public DataDbcontext(DbContextOptions<DataDbcontext> options) : base(options) { }
        //Table manufacturers
        //Table devices
        public DbSet<Positions> positions { get; set; }

        public DbSet<employees> employees { get; set; }
    }
}

