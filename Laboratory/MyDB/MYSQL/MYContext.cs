using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
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


        public MYContext()
        {
            _connectionString = ConfigurationManager.AppSettings["mysqlTest"].ToString();
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
        public string work { get; set; }
        public string pinyin { get; set; }
        public int tone { get; set; }
        /// <summary>
        /// 类别（0：简；1:繁）
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 笔画
        /// </summary>
        public int step { get; set; }
        /// <summary>
        /// 部首
        /// </summary>
        public int radical { get; set; }
        public int seq { get; set; }
    }
    [Table("workbankinfo")]
    public class WorkBankInfo
    {
        [Key]
        public int id { get; set; }
        public string workbankid { get; set; }
        public string pinyin { get; set; }
        public int tone { get; set; }
    }
}
