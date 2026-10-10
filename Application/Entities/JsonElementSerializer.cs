using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json;

public class JsonElementSerializer : IBsonSerializer<JsonElement>
{
    // Deserialize method reads the BSON data (string, object, or array) and converts it back to JsonElement
    public JsonElement Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        // Check BSON type and handle accordingly
        var bsonType = context.Reader.GetCurrentBsonType();

        if (bsonType == MongoDB.Bson.BsonType.String)
        {
            // If it's a string, just read it as a string
            var jsonString = context.Reader.ReadString();
            return JsonSerializer.Deserialize<JsonElement>(jsonString);
        }
        else if (bsonType == MongoDB.Bson.BsonType.Array || bsonType == MongoDB.Bson.BsonType.Document)
        {
            // If it's an array or document, read it as JSON
            var jsonString = context.Reader.ReadRawBsonArray().ToString();
            return JsonSerializer.Deserialize<JsonElement>(jsonString);
        }
        else
        {
            throw new InvalidOperationException($"Unexpected BSON type {bsonType} during deserialization.");
        }
    }

    // Serialize method writes JsonElement as BSON (stored as a string or array depending on content)
    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, JsonElement value)
    {
        var jsonString = value.ToString(); // Convert JsonElement to string
        context.Writer.WriteString(jsonString); // Write it as BSON string
    }

    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        // Check BSON type and handle accordingly
        var bsonType = context.Reader.GetCurrentBsonType();

        if (bsonType == MongoDB.Bson.BsonType.String)
        {
            // If it's a string, just read it as a string
            var jsonString = context.Reader.ReadString();
            return JsonSerializer.Deserialize<JsonElement>(jsonString);
        }
        else if (bsonType == MongoDB.Bson.BsonType.Array || bsonType == MongoDB.Bson.BsonType.Document)
        {
            // If it's an array or document, read it as JSON
            var jsonString = context.Reader.ReadRawBsonArray().ToString();
            return JsonSerializer.Deserialize<JsonElement>(jsonString);
        }
        else
        {
            throw new InvalidOperationException($"Unexpected BSON type {bsonType} during deserialization.");
        }
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
    {
        var jsonString = value.ToString(); // Convert JsonElement to string
        context.Writer.WriteString(jsonString); // Write it as BSON string
                                                }

    // Required by the interface: Returns the type that this serializer works with
    public Type ValueType => typeof(JsonElement);
}
