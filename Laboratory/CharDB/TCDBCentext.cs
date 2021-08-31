using Microsoft.EntityFrameworkCore;

namespace CharDB
{
    public class TCDBCentext : DbContext
    {
        #region 

        //public DbSet<Todos> Todos { get; set; }
        #endregion

        public TCDBCentext(DbContextOptions<TCDBCentext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
