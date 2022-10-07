using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace sg.customer
{
    public class IdCollection
    {
        [BsonId]
        public MongoDB.Bson.ObjectId _id { get; set; }

        [DataMember]
        public int LastId { get; set; }

        [DataMember]
        public string Key { get; set; }

    }
}