using Bogus;
using Com.WkTechnology.Tecnico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.WkTechnology.Tecnico.Test.Fakes
{
    public class CategoryFake
    {
        public CategoryEntity GenerateOne()
        {
            var record = new Faker<CategoryEntity>()
                .RuleFor(x => x.Id, (x, f) => x.IndexGlobal)
                .RuleFor(x => x.Name, (x, f) => x.Random.String2(20))
                .RuleFor(x => x.Description, (x, f) => x.Random.String2(50));
            return record.Generate();
        }

        public CategoryEntity ExistOne()
        {
            var record = new Faker<CategoryEntity>()
                .RuleFor(x => x.Id, (x, f) => 1)
                .RuleFor(x => x.Name, (x, f) => x.Random.String2(20))
                .RuleFor(x => x.Description, (x, f) => x.Random.String2(50));
            return record.Generate();
        }
    }
}
