using Dapper.FluentMap.Mapping;
using Com.WkTechnology.Tecnico.Domain.Entities;

namespace Com.WkTechnology.Tecnico.Data.DataMapper
{
    public class CategoryMap: EntityMap<CategoryEntity>
    {
        public CategoryMap()
        {
            Map(u => u.Name).ToColumn("name");
            Map(u => u.Description).ToColumn("description");
        }
    }
}
