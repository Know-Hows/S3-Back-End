﻿using MongoDB.Bson.Serialization.Attributes;

namespace KnowHows_Back_End.Models
{
    public class Article
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
    }
}
