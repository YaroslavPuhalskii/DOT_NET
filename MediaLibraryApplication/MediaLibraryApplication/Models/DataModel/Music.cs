﻿using MediaLibraryApplication.Core;
using MediaLibraryApplication.Models.Specifications;

namespace MediaLibraryApplication.Models.DataModel
{
    public class Music : File
    {
        public MusicParameters Parameters;
        public Music(int id, string name, MusicParameters parameters) : base(id, name)
        {
            Parameters = parameters;
        }
    }
}
