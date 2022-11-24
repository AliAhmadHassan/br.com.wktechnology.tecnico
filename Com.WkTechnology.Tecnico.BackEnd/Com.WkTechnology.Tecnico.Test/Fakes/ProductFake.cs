using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Com.WkTechnology.Tecnico.Domain.Entities;

namespace Com.WkTechnology.Tecnico.Test.Fakes
{
    public class ProductFake
    {
        public ProductEntity GenerateOne()
        {
            var record = new Faker<ProductEntity>()
                .RuleFor(x => x.Name, (x, f) => x.Random.String2(10))
                .RuleFor(x => x.Description, (x, f) => x.Random.String2(50))
                .RuleFor(x => x.UnitPrice, (x, f) => x.Random.Decimal())
                .RuleFor(x => x.Amount, (x, f) => x.Random.Int())
                .RuleFor(x => x.Category, (x, f) => new CategoryFake().ExistOne() );
            return record.Generate();
        }
    }
}
