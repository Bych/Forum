using System;

namespace Forum.Documents
{
    public class ResumeFileDocument
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime UploadDate { get; set; }

        public byte[] Content { get; set; }

        public string ContentType { get; set; }
    }
}