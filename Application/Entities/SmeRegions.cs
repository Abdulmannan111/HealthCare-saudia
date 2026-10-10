using Application.DTOs;
using Application.Entities.Form;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Entities
{
    public class SmeRegions
    {
        public string _id { get; set; } = string.Empty;
        public region region { get; set; } = new region();

        //  [BsonSerializer(typeof(CustomDateTimeConverter))] 

        [BsonElement("start_date")]
        private BsonValue? start_date_ { get; set; }

        [BsonIgnore]
        public DateTime? _start_date
        {
            get
            {
                if (start_date_ == null || start_date_.IsBsonNull)
                {
                    return null;
                }
                else if (start_date_.IsString)
                {
                    if (DateTime.TryParse(start_date_.AsString, out var parsedDate))
                    {
                        return parsedDate;
                    }
                    throw new Exception($"Invalid date format: {start_date_.AsString}");
                }
                else if (start_date_.IsBsonDateTime)
                {
                    return (DateTime?)start_date_;
                }
                throw new Exception($"Unexpected BSON type: {start_date_.BsonType}");
            }
            set
            {
                if (value == null)
                {
                    start_date_ = BsonNull.Value;
                }
                else
                {
                    start_date_ = value.Value.ToUniversalTime().ToString("ddd, dd MMM yyyy HH:mm:ss 'GMT'"); // Storing as string
                }
            }
        }


    }
}
