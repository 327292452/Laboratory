using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyDB.MYSQL
{
    public class MYContext : DbContext
    {
        private readonly string _connectionString;
        /// <summary>
        /// 机构部门
        /// </summary>
        public DbSet<Test> Tests { get; set; }
        public DbSet<WorkBank> WorkBanks { get; set; }


        public MYContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Test>();
            modelBuilder.Entity<WorkBank>();
        }
    }
    [Table("test")]
    public class Test
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
    }
    [Table("workbank")]
    public class WorkBank
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
    }
}
