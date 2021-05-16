using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rps4
{
    public class InteractionDB
    {
        public static void Add(Train newEnt, ApplicationContext db)
        {
            db.Trains.Add(newEnt);
            db.SaveChanges();

        }

        public static void Delete(int del, ApplicationContext db)
        {
            db.Trains.Remove(db.Trains.Find(del));
            db.SaveChanges();
        }

    }
}
