using ChaningExample.Entities;
using ChaningExample.MapperSrc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChaningExample
{
    class Program
    {
        static void Main(string[] args)
        {
            IMapper<Entity> mapper = new Mapper<Entity>();

            Entity entity = new Entity
            {
                ApplicationUserId = 5,
                IsDel = 0,
                Make = "Test Make",
                Model = "Test model",
                ProductionDate = DateTime.Now,
                StartKilometers = 25000,
            };
            
            string query = mapper
                .Update(entity)
                .WhereMap(x => (x.Id == 5 || x.Make == "Make") && x.StartKilometers == 50000 )
                .ExecuteNonQuery();

            string secondQuery = mapper
                .Update(entity)
                .WhereMap(e => e.Id == 4 && e.Make == "Yoo")
                .ExecuteNonQuery();

            string thirdStatement = mapper
                .Update(entity)
                .WhereMap(e => e.StartKilometers == 2000)
                .ExecuteNonQuery();
        }
    }
}
