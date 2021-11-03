using MediaLibraryApplication.Core;
using MediaLibraryApplication.Core.Players;
using MediaLibraryApplication.Core.PlayList;
using MediaLibraryApplication.Players;
using System.Collections.Generic;
using System.Linq;

namespace MediaLibraryApplication.Models
{
    class MediaLibrary
    {
        private ICollection<File> MediaFiles { get; set; }
        private ICollection<IPlayList> PlayLists { get; set; }

        public MediaLibrary()
        {
            MediaFiles = new List<File>();
            PlayLists = new List<IPlayList>();
        }

        #region work with mideafiles
        public void AddMediaFile(File file)
        {
            MediaFiles.Add(file);
        }

        public void RemoveMediaFile(File file)
        {
            MediaFiles.Remove(file);
        }

        public IEnumerable<File> FindMediaFiles(string name)
        {
            return MediaFiles.Where(x => x.Name == name);
        }

        public IEnumerable<File> GetMediaFiles()
        {
            return MediaFiles;
        }
        #endregion
        #region work with plyalists
        public void AddPlayList(IPlayList playList)
        {
            PlayLists.Add(playList);
        }

        public void RemovePlayList(IPlayList playList)
        {
            PlayLists.Remove(playList);
        }

        public IEnumerable<IPlayList> GetPlayLists(IPlayList playList)
        {
            return PlayLists;
        }

        public IEnumerable<IPlayList> FindPlayLists(string name)
        {
            return PlayLists.Where(x => x.Name == name);
        }
        #endregion 

        //public void Play(File file)
        //{
        //  
        //}

        //public void Play(IPlayList playList)
        //{
        //    
        //}
    }
}
