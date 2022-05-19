using FileShare.Domains;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileShare.SampleData
{
    public class FileSample
    {
        private static ObservableCollection<File> availableFiles = new ObservableCollection<File>();
        private static ObservableCollection<FileMetaData> metaDatas = new ObservableCollection<FileMetaData>();

        public static ObservableCollection<File> GetAvailableFiles()
        {
            if(!availableFiles.Any())
            {
                availableFiles.Add(new Task<File>(() =>
                {
                    byte[] bytes = new byte[1000];
                    File file = new File()
                    {
                        Id = Guid.NewGuid().ToString().Split('-')[4],
                        Name = "Some File",
                        Content = bytes,
                        Length = bytes.Length,
                        Type = "video/mp4"
                    };
                    metaDatas.Add(file.GetFileMetaData());
                    return file;
                }).Result);
                availableFiles.Add(new Task<File>(() =>
                {
                    byte[] bytes = new byte[50];
                    File file = new File()
                    {
                        Id = Guid.NewGuid().ToString().Split('-')[4],
                        Name = "Some File2",
                        Content = bytes,
                        Length = bytes.Length,
                        Type = "audio/mp3"
                    };
                    metaDatas.Add(file.GetFileMetaData());
                    return file;
                }).Result);
                availableFiles.Add(new Task<File>(() =>
                {
                    byte[] bytes = new byte[2000];
                    File file = new File()
                    {
                        Id = Guid.NewGuid().ToString().Split('-')[4],
                        Name = "Some File3",
                        Content = bytes,
                        Length = bytes.Length,
                        Type = "hyi/mp5"
                    };
                    metaDatas.Add(file.GetFileMetaData());
                    return file;
                }).Result);
            }

            return availableFiles;
        }

        public static ObservableCollection<FileMetaData> GetFileMetaDatas()
        {
            if(!metaDatas.Any())
            {
                GetAvailableFiles();
            }

            return metaDatas;
        }
    }
}
