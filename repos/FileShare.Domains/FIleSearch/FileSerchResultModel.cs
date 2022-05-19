﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FileShare.Domains.FIleSearch
{
    [DataContract]
    public class FileSerchResultModel
    {
        [DataMember]
        public HostInfo ServiceHost { get; set; }
        
        [DataMember]
        public ObservableCollection<FileMetaData> Files { get; set; }
    }
}
