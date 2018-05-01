using Entities;
using System.Collections.Generic;

namespace DataAccess.Mapper
{
    public interface IObjectMapper
    {
        List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows);
        BaseEntity BuildObject(Dictionary<string, object> row);
    }
}