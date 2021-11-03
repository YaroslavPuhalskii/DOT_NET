using MediaLibraryApplication.Core;
using MediaLibraryApplication.Core.PlayList;
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

        public override void Play(File file)
        {
            throw new System.NotImplementedException();
        }

        public override void Play(IPlayList playList)
        {
            throw new System.NotImplementedException();
        }
    }
}
