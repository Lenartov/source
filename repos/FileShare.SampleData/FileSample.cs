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
        private ObservableCollection<File> availableFiles = new ObservableCollection<File>();
        private ObservableCollection<FileMetaData> metaDatas = new ObservableCollection<FileMetaData>();

        public ObservableCollection<File> GetAvailableFiles()
        {
            if(!availableFiles.Any())
            {
                availableFiles.Add(new File
                {
                        Id = Guid.NewGuid().ToString().Split('-')[4],
                        Name = "Some File",
                        Content = new byte[23234],
                        Length = 23234,
                        Type = "video/mp4"
                });

                availableFiles.Add(new File
                {
                    Id = Guid.NewGuid().ToString().Split('-')[4],
                    Name = "Some File2",
                    Content = new byte[345],
                    Length = 345,
                    Type = "video/mp4"
                });

                availableFiles.Add(new File
                {
                    Id = Guid.NewGuid().ToString().Split('-')[4],
                    Name = "Some File3",
                    Content = new byte[200],
                    Length = 200,
                    Type = "video/mp4"
                });
            }

            return availableFiles;
        }

        public ObservableCollection<FileMetaData> GetFileMetaDatas()
        {
            /* if(!metaDatas.Any())
             {
                 GetAvailableFiles().ToList().ForEach(p =>
                 {
                     metaDatas.Add(new FileMetaData(p.Id, p.Name, p.Length));
                 });
             }
            */
            return null;// metaDatas;
        }
    }
}
