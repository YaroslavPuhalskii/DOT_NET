using MediaLibraryApplication.Core;
using MediaLibraryApplication.Models.Specifications;

namespace MediaLibraryApplication.Models.DataModel
{
    public class Music : File
    {
        /// <summary>
        /// Music parameters
        /// </summary>
        public MusicParameters Parameters { get; set; }
        public Music(int id, string name, string format, MusicParameters parameters)
            : base(id, name, format)
        {
            Parameters = parameters;
        }
    }
}   
