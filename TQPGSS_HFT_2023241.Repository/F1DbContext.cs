using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TQPGSS_HFT_2023241.Models;

namespace TQPGSS_HFT_2023241.Repository
{
    public class F1DbContext : DbContext
    {
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<GrandPrix> GrandPrixes { get; set; }
        public F1DbContext()
        {
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\F1Database.mdf;Integrated Security=True";

                optionsBuilder.UseSqlServer(conn).UseLazyLoadingProxies();
                //optionsBuilder
                //    .UseLazyLoadingProxies()
                //    .UseInMemoryDatabase("F1");

            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Driver>(entity =>
            entity.HasOne(driver => driver.team)
                  .WithMany(team => team.Drivers)
                  .HasForeignKey(driver => driver.TeamId)
                  .OnDelete(DeleteBehavior.Cascade)
            );
            modelBuilder.Entity<GrandPrix>(entity =>
            entity.HasOne(grandprix => grandprix.Driver)
                  .WithMany()
                  .HasForeignKey(winner => winner.WhoWon)
                  .OnDelete(DeleteBehavior.Cascade)
            );


            XDocument xdoc = XDocument.Load("F1.xml");
            ICollection<Driver> drivers = new List<Driver>();
            ICollection<Team> teams = new List<Team>();
            ICollection<GrandPrix> grandprixes = new List<GrandPrix>();
            foreach (var item in xdoc.Descendants("driver"))
            {
                Driver driver = new Driver()
                {
                    Id = int.Parse(item.Element("Id").Value),
                    Name = item.Element("Name").Value,
                    Age = int.Parse(item.Element("Age").Value),
                    Points = int.Parse(item.Element("Points").Value),
                    TeamId = int.Parse(item.Element("TeamId").Value),
                };
                drivers.Add(driver);
            }
            foreach (var item in xdoc.Descendants("Team"))
            {
                Team team = new Team()
                {
                    Id = int.Parse(item.Element("Id").Value),
                    Name = item.Element("Name").Value,
                    Driver1 = int.Parse(item.Element("Driver1Id").Value),
                    Driver2 = int.Parse(item.Element("Driver2Id").Value),
                    Points = int.Parse(item.Element("Points").Value),
                    Principal = item.Element("TeamPrincipal").Value

                };
                teams.Add(team);
            }
            foreach (var item in xdoc.Descendants("GrandPrix"))
            {
                GrandPrix grandprix = new GrandPrix()
                {
                    Id = int.Parse(item.Element("Id").Value),
                    Name = item.Element("Name").Value,
                    Date = item.Element("Date").Value,
                    WhoWon = int.Parse(item.Element("WhoWon").Value)
                };
                grandprixes.Add(grandprix);
            }
            modelBuilder.Entity<Driver>().HasData(drivers);
            modelBuilder.Entity<Team>().HasData(teams);
            modelBuilder.Entity<GrandPrix>().HasData(grandprixes);
            
        }
    }
}
