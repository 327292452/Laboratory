using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDB.MSSQL
{
    public class TestContext : DbContext
    {
        private readonly string _connectionString;
        /// <summary>
        /// 机构部门
        /// </summary>
        public DbSet<UUMSOwnerDeptsDTO> UUMSOwnerDeptses { get; set; }


        public TestContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }

    [Table("UUMS_Owner_Depts")]
    public class UUMSOwnerDeptsDTO
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        [Key]
        [Column("DeptGUID")]
        [JsonProperty("DeptGUID")]
        public Guid Id { get; set; }
        /// <summary>
        /// 单位ID
        /// </summary>
        [JsonIgnore]
        public Guid OwnerGUID { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [JsonIgnore]
        public Single OrderNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public Guid? RefDeptGUID { get; set; }
    }
}
