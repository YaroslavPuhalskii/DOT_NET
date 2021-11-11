namespace MediaLibraryApplication.Abstractions
{
    public interface IFile
    {
        int Id { get; }

        string Name { get; set; }

        void Play(IMediaPlayer player);
    }
}
