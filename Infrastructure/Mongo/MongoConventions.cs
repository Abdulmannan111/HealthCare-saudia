using System;
using System.Collections;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;

namespace Infrastructure.Mongo
{
    public static class MongoConventions
    {
        public static void Register()
        {
            var conventionPack = new ConventionPack
        {
            new IgnoreIfNullConvention(true), // Ignore null values globally
            new IgnoreEmptyListsConvention(),  // Ignore empty lists globally
            new IgnoreEmptyStringsConvention() // Ignore empty strings globally
        };

            ConventionRegistry.Register("CustomConventions", conventionPack, t => true);
        }
    }

    // Custom Convention to Ignore Empty Lists
    public class IgnoreEmptyListsConvention : IClassMapConvention
    {
        public string Name => "IgnoreEmptyLists";

        public void Apply(BsonClassMap classMap)
        {
            foreach (var member in classMap.DeclaredMemberMaps)
            {
                var memberType = member.MemberType;

                if (memberType.IsGenericType && memberType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    member.SetShouldSerializeMethod(obj =>
                    {
                        var value = member.Getter(obj) as IList;
                        return value != null && value.Count > 0; // Serialize only if the list is not empty
                    });
                }
            }
        }
    }

    // Custom Convention to Ignore Empty Strings
    public class IgnoreEmptyStringsConvention : IClassMapConvention
    {
        public string Name => "IgnoreEmptyStrings";

        public void Apply(BsonClassMap classMap)
        {
            foreach (var member in classMap.DeclaredMemberMaps)
            {
                if (member.MemberType == typeof(string))
                {
                    member.SetShouldSerializeMethod(obj =>
                    {
                        var value = member.Getter(obj) as string;
                        return !string.IsNullOrEmpty(value); // Serialize only if string is not empty
                    });
                }
            }
        }
    }
}
