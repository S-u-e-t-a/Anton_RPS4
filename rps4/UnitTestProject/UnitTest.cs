using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using rps4;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestAdd()
        {
            var Entity = new Train();
            Entity.ID = 80;
            Entity.Name = "Скорый";
            Entity.Departure = "16.05.2021 11:00";
            Entity.Arrival = "16.05.2021 12:00";
            Entity.Station_dep = "СПб";
            Entity.Station_arr = "МСК";
            Entity.Cost = 2021;
            InteractionDB.Add(Entity, new ApplicationContext());
        }
        [TestMethod]
        public void TestDelete()
        {
            int deleting = 80;
            InteractionDB.Delete(deleting, new ApplicationContext());
        }
    }
}
