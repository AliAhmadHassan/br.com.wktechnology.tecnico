using Dapper.FluentMap.Mapping;
using Com.WkTechnology.Tecnico.Domain.Entities;

namespace Com.WkTechnology.Tecnico.Data.DataMapper
{
    public class ProductMap: EntityMap<ProductEntity>
    {
        public ProductMap()
        {
            Map(u => u.Name).ToColumn("name");
            Map(u => u.Description).ToColumn("description");
            Map(u => u.UnitPrice).ToColumn("unit_price");
            Map(u => u.Amount).ToColumn("amount");
            Map(u => u.Category).ToColumn("category");
        }
    }
}
