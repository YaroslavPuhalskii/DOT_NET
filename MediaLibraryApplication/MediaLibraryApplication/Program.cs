using MediaLibraryApplication.Core;
using MediaLibraryApplication.Core.MediaLibrary;
using MediaLibraryApplication.Models;
using MediaLibraryApplication.Models.DataModel;
using MediaLibraryApplication.Models.Specifications;
using System;

namespace MediaLibraryApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Music
            File firstMusic = new Music
                (
                id: 1, 
                name: "99 Problems",
                format: ".mp3",
                new MusicParameters("Rap", new DateTime(2021, 11, 1), "Big baby Tape")
                );
            File secondMusic = new Music
                (
                id: 2,
                name: "Million",
                format: ".mp3",
                new MusicParameters("Rap", new DateTime(2021, 11, 1), "Big baby Tape")
                );
            File thirdMusic = new Music
                (
                id: 3,
                name: "Без фокусов",
                format: ".mp3",
                new MusicParameters("Rap", new DateTime(2021, 10, 1), "Markul")
                );
            File fourthMusic = new Music
                (
                id: 1,
                name: "Сан Ларан",
                format: ".mp3",
                new MusicParameters("Rap", new DateTime(2021, 10, 12), "Платина")
                );
            #endregion
            #region Video
            File firstVideo = new Video
                (
                id: 1,
                name: "РЕДАКЦИЯ", 
                format: ".mp4",
                new VideoParameters(new WindowParameters(width: 150, height: 150), quality: 1080) 
                );
            File secondVideo = new Video
                (
                id: 2,
                name: "NONSTOP", 
                format: ".mp4",
                new VideoParameters(new WindowParameters(width: 150, height: 150), quality: 1080) 
                );
            #endregion

            IMediaLibrary library = new MediaLibrary();

            library.AddMediaFile(firstMusic);
            library.AddMediaFile(firstVideo);
            library.AddMediaFile(secondMusic);
            library.AddMediaFile(secondVideo);
            library.AddMediaFile(thirdMusic);
            library.AddMediaFile(fourthMusic);

            library.Play(firstMusic);

            library.Play();

            Console.ReadLine();
        }
    }
}
