using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace rps4.Tests
{
    [TestClass]
    public class Tests
    {
        ApplicationContext db = new ApplicationContext();
        [TestMethod]
        
        public void TestAdd()
        {
            var Entity = new Train();
            Entity.Name = "Скорый";
            Entity.Departure = "16.05.2021 11:00";
            Entity.Arrival = "16.05.2021 12:00";
            Entity.Station_dep = "СПб";
            Entity.Station_arr = "МСК";
            Entity.Cost = 2021;
            InteractionDB.Add(Entity, db);
        }
        [TestMethod]
        public void TestDelete()
        {
            int deleting = 5;
            InteractionDB.Delete(deleting, db);
        }
        [TestMethod]

        public void TestAdd2()
        {
            var Entity = new Train();
            Entity.Name = "Паровоз";
            Entity.Departure = "16.05.2001 11:00";
            Entity.Arrival = "16.05.2021 12:00";
            Entity.Station_dep = "СПб";
            Entity.Station_arr = "МСК";
            Entity.Cost = 100;
            InteractionDB.Add(Entity, db);
        }
        [TestMethod]
        public void TestDelete2()
        {
            int deleting = 6;
            InteractionDB.Delete(deleting, db);
        }

    }
}
