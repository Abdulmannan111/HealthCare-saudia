using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

namespace Application.Entities
{
    public class PhoneSerializer : SerializerBase<string>
    {
        public override string Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            // Check the type of BSON value being read
            var bsonType = context.Reader.GetCurrentBsonType();

            // If the type is a string, read it as a string
            if (bsonType == BsonType.String)
            {
                return context.Reader.ReadString();  // Read as a string
            }
            // If the type is an integer, read it as an integer and convert it to a string
            else if (bsonType == BsonType.Int32)
            {
                return context.Reader.ReadInt32().ToString();  // Convert to string
            }
            else
            {
                throw new InvalidOperationException("Unexpected BSON type for Phone field");
            }
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, string value)
        {
            context.Writer.WriteString(value);  // Write as a string
        }
    }
}