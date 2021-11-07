using MediaLibraryApplication.Abstractions;
using MediaLibraryApplication.Core.Media;
using MediaLibraryApplication.Models;
using MediaLibraryApplication.Models.DataModel;
using MediaLibraryApplication.Models.Specifications;
using MediaLibraryApplication.Players;
using System;
using System.Collections.Generic;

namespace MediaLibraryApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Music
            MediaFile firstMusic = new Music
                (
                id: 1, 
                name: "99 Problems",
                format: ".mp3",
                new MusicParameters("Rap", new DateTime(2021, 11, 1), "Big baby Tape")
                );
            MediaFile secondMusic = new Music
                (
                id: 2,
                name: "Million",
                format: ".mp3",
                new MusicParameters("Rap", new DateTime(2021, 11, 1), "Big baby Tape")
                );
            MediaFile thirdMusic = new Music
                (
                id: 3,
                name: "Без фокусов",
                format: ".mp3",
                new MusicParameters("Rap", new DateTime(2021, 10, 1), "Markul")
                );
            MediaFile fourthMusic = new Music
                (
                id: 1,
                name: "Сан Ларан",
                format: ".mp3",
                new MusicParameters("Rap", new DateTime(2021, 10, 12), "Платина")
                );
            #endregion
            #region Video
            MediaFile firstVideo = new Video
                (
                id: 1,
                name: "РЕДАКЦИЯ", 
                format: ".mp4",
                new VideoParameters(new WindowParameters(width: 150, height: 150), quality: 1080) 
                );
            MediaFile secondVideo = new Video
                (
                id: 2,
                name: "NONSTOP", 
                format: ".mp4",
                new VideoParameters(new WindowParameters(width: 150, height: 150), quality: 1080) 
                );
            #endregion
            #region Playlist
            var firstPlaylist = new PlayList(id: 2, name: "for gym");
            firstPlaylist.Add(firstVideo);
            firstPlaylist.Add(secondMusic);
            #endregion

            IMediaLibrary library = new MediaLibrary(new Dictionary<Type, IMediaPlayer>()
            {
                { typeof(Music), new MusicPlayer() },
                { typeof(Photo), new PhotoPlayer() },
                { typeof(Video), new VideoPlayer() }
            }, new PlayList(id: 1, name: "for relax"));

            library.DefaultPlaylist.Add(firstMusic);
            library.DefaultPlaylist.Add(firstVideo);
            library.DefaultPlaylist.Add(secondMusic);
            library.DefaultPlaylist.Add(secondVideo);
            library.DefaultPlaylist.Add(thirdMusic);
            library.DefaultPlaylist.Add(fourthMusic);

            library.PlayLists.Add(firstPlaylist);

            library.Play(firstMusic);

            library.PlayPlaylist();

            Console.ReadLine();
        }
    }
}
