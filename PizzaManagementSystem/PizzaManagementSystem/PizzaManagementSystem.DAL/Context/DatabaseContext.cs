using Microsoft.EntityFrameworkCore;
using PizzaManagementSystem.DAL.Context.EntityConfigurations;
using PizzaManagementSystem.DAL.Models;
using System;

namespace PizzaManagementSystem.DAL.Context
{
    public class DatabaseContext : DbContext
    {
        private string _connectionString;

        public DatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DatabaseContext()
        {
            object runtimeConnectionString = System.AppContext.GetData("RuntimeConnectionString");

            if (runtimeConnectionString != null)
            {
                _connectionString = runtimeConnectionString.ToString();
            }
        }

        #region DbSet

        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        #endregion


        /// <summary>
        /// Override per controllare se la connection string è stata definita e nel caso configura 
        /// la connessione al DB altrimenti genera un'eccezione.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (string.IsNullOrEmpty(_connectionString))
                throw new ApplicationException("Connection string not defined.");

            optionsBuilder.UseSqlServer(_connectionString);
        }

        /// <summary>
        /// Override per applicare le configurazioni create per i modelli definiti.
        /// Nelle configurazioni vengono indicati vincoli e relazioni.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MenuItemConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
        }
    }
}
