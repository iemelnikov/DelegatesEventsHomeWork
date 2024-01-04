namespace DelegatesEventsHomeWork
{
    /// <summary>
    /// Класс, обходящий каталог файлов и выдающий событие при нахождении каждого файла
    /// </summary>
    internal class FilesSearcher
    {
        private readonly string _filesSearchDirectoryPath;

        private readonly string _filesSearchPattern;

        public FilesSearcher(string filesSearchDirectoryPath) : this(filesSearchDirectoryPath, "*.*") { }

        public FilesSearcher(string filesSearchDirectoryPath, string filesSearchPattern)
        {
            _filesSearchDirectoryPath = filesSearchDirectoryPath;
            _filesSearchPattern = filesSearchPattern ?? string.Empty;
        }

        /// <summary>
        /// Represents the method that will handle the DelegatesEventsHomeWork.FilesSearcher.FileFound event
        /// of a DelegatesEventsHomeWork.FilesSearcher class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The DelegatesEventsHomeWork.FileArgs that contains the event data.</param>
        public delegate void FileFoundEventHandler(object? sender, FileArgs e);

        /// <summary>
        /// Событие при нахождении файла.
        /// </summary>
        public event FileFoundEventHandler? FileFound;

        /// <summary>
        /// Запуск процедуры поиска файлов.
        /// </summary>
        public void RunFilesSearch()
        {
            if (!string.IsNullOrEmpty(_filesSearchDirectoryPath))
            {
                foreach (string fileNameFound in Directory.EnumerateFiles(_filesSearchDirectoryPath, _filesSearchPattern, SearchOption.AllDirectories))
                {
                    var fileNameFoundFileArgs = new FileArgs(fileNameFound);
                    FileFound?.Invoke(this, fileNameFoundFileArgs);
                    if (fileNameFoundFileArgs.ToFinishSearch)
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Provides data for the DelegatesEventsHomeWork.FilesSearcher.FileFound event.
    /// </summary>
    internal class FileArgs : EventArgs
    {
        private readonly string? _fileName;

        public FileArgs(string? fileName)
        {
            _fileName = fileName;
        }

        /// <summary>
        /// Имя найденного файла.
        /// </summary>
        public string? FileName
        {
            get
            {
                return _fileName;
            }
        }

        /// <summary>
        /// Возможность отмены дальнейшего поиска при значении True.
        /// </summary>
        public bool ToFinishSearch { get; set; }

    }
}
