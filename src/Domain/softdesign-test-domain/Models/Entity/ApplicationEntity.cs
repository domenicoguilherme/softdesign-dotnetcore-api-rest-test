using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using softdesign_test_domain.Models.DTOs;

namespace softdesign_test_domain.Models.Entity
{
    public class ApplicationEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int Application { get; set; }
        public bool DebuggingMode { get; set; }
        public string Url { get; set; }
        public string PathLocal { get; set; }

        public void Map(ApplicationDTO applicationDTO)
        {
            Url = applicationDTO.Url;
            PathLocal = applicationDTO.PathLocal;
            Application = applicationDTO.Application;
            DebuggingMode = applicationDTO.DebuggingMode;
        }
    }
}