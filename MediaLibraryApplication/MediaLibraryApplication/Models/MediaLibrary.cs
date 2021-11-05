using MediaLibraryApplication.Core;
using MediaLibraryApplication.Factory;
using MediaLibraryApplication.Core.MediaLibrary;
using MediaLibraryApplication.Core.Players;
using MediaLibraryApplication.Core.PlayList;
using System.Collections.Generic;
using System.Linq;

namespace MediaLibraryApplication.Models
{
    public class MediaLibrary : IMediaLibrary
    {
        private ICollection<File> MediaFiles { get; set; }
        private ICollection<IPlayList> PlayLists { get; set; }

        private MediaPlayer musicPlayer;
        private MediaPlayer videoPlayer;
        private MediaPlayer photoPlayer;

        public MediaLibrary(MediaPlayer musicPlayer, MediaPlayer videoPlayer, MediaPlayer photoPlayer)
        {
            MediaFiles = new List<File>();
            PlayLists = new List<IPlayList>();

            this.musicPlayer = musicPlayer;
            this.videoPlayer = videoPlayer;
            this.photoPlayer = photoPlayer;
        }

        #region work with mideafiles
        /// <summary>
        /// Method for adding a media file to a collection
        /// </summary>
        /// <param name="file"></param>
        public void AddMediaFile(File file)
        {
            MediaFiles.Add(file);
        }
        /// <summary>
        /// Method for deleting a media file from a collection
        /// </summary>
        /// <param name="file"></param>
        public void RemoveMediaFile(File file)
        {
            MediaFiles.Remove(file);
        }
        /// <summary>
        /// Method for searching media files by name
        /// </summary>
        /// <param name="name">File name</param>
        /// <returns>File enumeration</returns>
        public IEnumerable<File> FindMediaFile(string name)
        {
            return MediaFiles.Where(x => x.Name == name);
        }
        /// <summary>
        /// A method for getting a collection of media files
        /// </summary>
        /// <returns></returns>
        public IEnumerable<File> GetMediaFiles()
        {
            return MediaFiles;
        }
        #endregion
        #region work with plyalists
        /// <summary>
        /// Method for adding a playlist to a collection
        /// </summary>
        /// <param name="playList"></param>
        public void AddPlayList(IPlayList playList)
        {
            PlayLists.Add(playList);
        }
        /// <summary>
        /// Method for deleting a playlist from a collection
        /// </summary>
        /// <param name="playList"></param>
        public void RemovePlayList(IPlayList playList)
        {
            PlayLists.Remove(playList);
        }
        /// <summary>
        /// Method for getting a collection of playlists
        /// </summary>
        /// <param name="playList"></param>
        /// <returns>Playlist enumeration</returns>
        public IEnumerable<IPlayList> GetPlayLists(IPlayList playList)
        {
            return PlayLists;
        }
        /// <summary>
        /// Method for searching a playlist by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Playlist enumeration</returns>
        public IEnumerable<IPlayList> FindPlayLists(string name)
        {
            return PlayLists.Where(x => x.Name == name);
        }
        #endregion

        /// <summary>
        /// Determining the file format for calling the desired player
        /// </summary>
        /// <param name="file"></param>
        public void Play(File file)
        {
            if (file.IsVideo())
            {
                PlayVideo(file);
            }
            else if (file.IsMusic())
            {
                PlayMusic(file);
            }
            else if (file.IsPhoto())
            {
                PlayPhoto(file);
            }
        }

        public void Play(IPlayList playList)
        {
            playList.GetMediaFiles().ToList().ForEach(Play);
        }
        /// <summary>
        /// Calling the play method on the music player
        /// </summary>
        /// <param name="file"></param>
        public void PlayMusic(File file)
        {
            musicPlayer.Play(file);
        }
        /// <summary>
        /// Calling the play method on the video player
        /// </summary>
        /// <param name="file"></param>
        public void PlayVideo(File file)
        {
            videoPlayer.Play(file);
        }
        /// <summary>
        /// Calling the play method on the photo player
        /// </summary>
        /// <param name="file"></param>
        public void PlayPhoto(File file)
        {
            photoPlayer.Play(file);
        }
    }
}
