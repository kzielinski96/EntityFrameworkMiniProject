using Microsoft.VisualStudio.TestTools.UnitTesting;
using EF___miniproject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF___miniprojectTests;

namespace EF___miniproject.Tests
{
    [TestClass()]
    public class MainWindowTests
    {

        Entities context = new Entities();

    
        [TestMethod()]
        public void AddRecordTest()
        {
            var newRecord = new Table_1()
            {
                city_name = "LOL",
                city_id = "122",
                temperature = 20.5,
            };

            context.Table_1.Add(newRecord);
            context.SaveChanges();
        }

        [TestMethod()]
        public void UpdateRecordTest()
        {
            var existingRecord = context.Table_1.Find(323) as Table_1;
            String name = "LOOL";
            String id = "1222";
            double temp = 20.55;

            existingRecord.city_name = name;
            existingRecord.city_id = id;
            existingRecord.temperature = temp;

            context.SaveChanges();
        }

        [TestMethod()]
        public void DeleteRecordTest()
        {
            var record = context.Table_1.Find(323) as Table_1;
            context.Table_1.Remove(record);
            context.SaveChanges();
        }
    }
}