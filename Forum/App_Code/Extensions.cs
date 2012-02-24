using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Forum.App_Code
{
    public static class MongodbExtentions
    {
        public static T FindOneWithFields<T>(this MongoCollection collection,
                                             params string[] includedFields)
        {
            return collection
                    .FindAllAs<T>()
                    .SetLimit(1)
                    .SetFields(Fields.Include(includedFields))
                    .FirstOrDefault();
        }


        public static T FindOneWithFields<T>(this MongoCollection collection,
                                             IMongoQuery query,
                                             params string[] includedFields)
        {
            return collection
                    .FindAs<T>(query)
                    .SetLimit(1)
                    .SetFields(Fields.Include(includedFields))
                    .FirstOrDefault();
        }
    }
}