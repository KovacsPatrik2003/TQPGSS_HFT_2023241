using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using TQPGSS_HFT_2023241.Logic;
using TQPGSS_HFT_2023241.Models;
using TQPGSS_HFT_2023241.Repository;

namespace TQPGSS_HFT_2023241.Test
{
    [TestFixture]
    public class TestClass
    {
        DriverLogic dl;
        Mock<IRepository<Driver>> mockDriverRepository;
        TeamLogic tl;
        Mock<IRepository<Team>> mockTeamRepository;
        GrandPrixLogic gl;
        Mock<IRepository<GrandPrix>> mockGrandPrixRepository;
        [SetUp]
        public void Init()
        {
            var teams = new List<Team>()
            {
                new Team()
                {
                    Id = 1,
                    Name = "Red Bull",
                    Driver1=1,
                    Driver2=11,
                    Points=400,
                    Principal="Horner"

                },
                new Team()
                {
                    Id = 2,
                    Name = "Ferrari",
                    Driver1 = 16,
                    Driver2 = 55,
                    Points = 300,
                    Principal = "Binotto"
                },
                new Team()
                {
                    Id = 3,
                    Name = "Mercedes",
                    Driver1 = 44,
                    Driver2 = 63,
                    Points = 200,
                    Principal = "Toto"
                }

            }.AsQueryable();
            var drivers = new List<Driver>()
            {
                new Driver()
                {
                    Id = 1,
                    Name = "Max Verstappen",
                    Age = 25,
                    Points = 250,
                    TeamId = 1,
                },
                new Driver()
                {
                    Id = 11,
                    Name = "Sergio Perez",
                    Age = 25,
                    Points = 150,
                    TeamId = 1,
                },
                new Driver()
                {
                    Id = 16,
                    Name = "Charles Leclerc",
                    Age = 25,
                    Points = 151,
                    TeamId = 2,
                },
                new Driver()
                {
                    Id = 55,
                    Name = "Carlos Sainz",
                    Age = 25,
                    Points = 149,
                    TeamId = 2,
                },
                new Driver()
                {
                    Id = 44,
                    Name = "Lewis Hamilton",
                    Age = 25,
                    Points = 110,
                    TeamId = 3,
                },
                new Driver()
                {
                    Id = 63,
                    Name = "George Russell",
                    Age = 25,
                    Points = 90,
                    TeamId = 3,
                }
            }.AsQueryable();
            var grandPrixes = new List<GrandPrix>()
            {
                new GrandPrix()
                {
                    Id = 1,
                    Name = "BahRain",
                    Date = "marc 1",
                    WhoWon = 1
                },
                new GrandPrix()
                {
                    Id = 2,
                    Name = "Jeddah",
                    Date = "april 1",
                    WhoWon = 1
                },
                new GrandPrix()
                {
                    Id = 3,
                    Name = "Austria",
                    Date = "may 1",
                    WhoWon = 1
                },
                new GrandPrix()
                {
                    Id = 4,
                    Name = "Hungary",
                    Date = "june 1",
                    WhoWon = 16
                },
                new GrandPrix()
                {
                    Id = 5,
                    Name = "Abu Dhabi",
                    Date = "july 1",
                    WhoWon = 11
                }
            }.AsQueryable();
            

            mockTeamRepository = new Mock<IRepository<Team>>();
            mockTeamRepository.Setup(r => r.ReadAll()).Returns(teams);

            mockDriverRepository = new Mock<IRepository<Driver>>();
            mockDriverRepository.Setup(r => r.ReadAll()).Returns(drivers);

            mockGrandPrixRepository=new Mock<IRepository<GrandPrix>>();
            mockGrandPrixRepository.Setup(r => r.ReadAll()).Returns(grandPrixes);

            tl = new TeamLogic(mockTeamRepository.Object, mockDriverRepository.Object, mockGrandPrixRepository.Object);
            dl = new DriverLogic(mockDriverRepository.Object, mockGrandPrixRepository.Object);
            gl = new GrandPrixLogic(mockGrandPrixRepository.Object, mockDriverRepository.Object);


        }
        [Test]
        public void teamDriversTest()
        {

            var result = tl.teamsDrivers("Ferrari");
            var expected = new List<string>()
            {
                "Charles Leclerc",
                "Carlos Sainz"
            };
            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void teamDriversTest2()
        {

            var result = tl.teamsDrivers("Red Bull");
            var expected = new List<string>()
            {
                "Max Verstappen",
                "Sergio Perez"
            };
            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void driverWinsTest()
        {
            var result = dl.driverWins("Max Verstappen");
            var expected = new List<string>()
            {
                "BahRain",
                "Jeddah",
                "Austria"
            };
            Assert.That(result, Is.EqualTo(expected));
        }
        

        /*[Test]
        public void firstAndSecondDriverByPointsTest()
        {
            var result = tl.firstAndSecondDriverByPoints();
            result.ToString();
            
            List<string> expected = new List<string>()
            {
                "{ team = Red Bull, Driver1 = 1, Driver2 = 11, Driver1Points = 250, Driver2Points = 150, first = 1, second = 11 }",
                "{ team = Ferrari, Driver1 = 16, Driver2 = 55, Driver1Points = 151, Driver2Points = 149, first = 16, second = 55 }",
                "{ team = Mercedes, Driver1 = 44, Driver2 = 63, Driver1Points = 110, Driver2Points = 90, first = 44, second = 63 }"
            };
            Assert.That(result, Is.EqualTo(expected));
        }*/

    }
}
