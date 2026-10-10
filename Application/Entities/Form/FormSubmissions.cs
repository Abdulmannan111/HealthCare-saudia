using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Application.Entities.Form
{
    [BsonIgnoreExtraElements]
    public class FormSubmissions : AuditInfoComplete
    {

        [BsonElement("project_id")]
        public string? project_id { get; set; }


        [BsonElement("form_id")]
        public string? form_id { get; set; }

        [BsonElement("data")]
        public BsonDocument? data_ { get; set; } // Internal BSON storage

        [BsonIgnore]
        public dynamic? data
        {
            get
            {
                if (data_ == null || data_.IsBsonNull)
                {
                    return null;
                }

                return BsonTypeMapper.MapToDotNetValue(data_);
            }
            set
            {
                try
                {

                    if (value is JsonElement jsonElement && jsonElement.ValueKind != JsonValueKind.Null)
                    {
                        // Convert JsonElement to BsonDocument
                        data_ = BsonDocument.Parse(jsonElement.GetRawText());
                    }
                    else if (value is Dictionary<string, dynamic> dictionary)
                    {
                        // Convert Dictionary to BsonDocument
                        data_ = new BsonDocument(dictionary.ToDictionary(k => k.Key, k => BsonValue.Create(k.Value)));
                    }
                    else if (value is BsonDocument bsonDoc)
                    {
                        // If the value is already a BsonDocument, assign it directly
                        data_ = bsonDoc;
                    }
                    else
                    {
                        // Convert other objects to BsonDocument
                        data_ = value.ToBsonDocument();
                    }
                }

                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }



        [BsonElement("user_id")]
        public string? user_id { get; set; }


        [BsonElement("status")]
        public string? status { get; set; }


        [BsonElement("schema_version")]
        public int? schema_version { get; set; }

        [BsonElement("report_no")]
        public BsonValue? report_no_ { get; set; } // Internal BSON storage

        [BsonIgnore]
        public dynamic? report_no
        {
            get
            {
                if (report_no_ == null || report_no_.IsBsonNull)
                {
                    return null;
                }
                return BsonTypeMapper.MapToDotNetValue(report_no_);
            }
            set
            {
                try
                {
                    if (value == null)
                    {
                        report_no_ = BsonNull.Value;
                    }
                    else if (value is JsonElement jsonElement && jsonElement.ValueKind != JsonValueKind.Null)
                    {
                        report_no_ = BsonValue.Create(jsonElement.ToString());
                    }
                    else
                    {
                        report_no_ = BsonValue.Create(value);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }



        [BsonElement("inspection_date")]
        public DateTime? inspection_date { get; set; }


        [BsonElement("deleted_at")]
        public DateTime? DeletedAt { get; set; }

    }
}
