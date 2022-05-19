namespace FileShare.Domains
{
    public class File
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Length { get; set; }
        public byte[] Content { get; set; }
        public FileMetaData GetFileMetaData() 
        {
            return new FileMetaData(Id, Name, Length);
        }
    }
}
