using System.Runtime.Serialization;
using HMS.Model.Core.DomainModels.Base;

namespace HMS.Model.Core.DomainModels
{
    [DataContract]
    public class RoomImage : ObjectModel
    {
        [DataMember(Name = "uploadUid")]
        public string UploadUid { get; set; }
        [DataMember(Name = "fileName")]
        public string FileName { get; set; }
        [DataMember(Name = "contentType")]
        public string ContentType { get; set; }
        [DataMember(Name = "chunkIndex")]
        public long Index { get; set; }
        [DataMember(Name = "totalChunks")]
        public long Total { get; set; }
        [DataMember(Name = "totalFileSize")]
        public long TotalFileSize { get; set; }
        [DataMember(Name = "content")]
        public string Path { get; set; }

        public Room Room { get; set; }
    }
}
