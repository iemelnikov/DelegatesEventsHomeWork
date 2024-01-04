namespace DelegatesEventsHomeWork
{
    /// <summary>
    /// Класс, обходящий каталог файлов и выдающий событие при нахождении каждого файла
    /// </summary>
    internal class FilesSearcher
    {
        private readonly string _filesSearchDirectoryPath,
                                _filesSearchPattern;

        public FilesSearcher(string filesSearchDirectoryPath) : this(filesSearchDirectoryPath, "*.*") { }

        public FilesSearcher(string filesSearchDirectoryPath, string filesSearchPattern)
        {
            _filesSearchDirectoryPath = filesSearchDirectoryPath;
            _filesSearchPattern = filesSearchPattern ?? string.Empty;
        }

        /// <summary>
        /// Событие при нахождении файла.
        /// </summary>
        public event EventHandler<FileArgs>? FileFound;

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
                    if (fileNameFoundFileArgs.ToCancelSearch)
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
        /// Возможность отмены дальнейшего поиска при значении true.
        /// </summary>
        public bool ToCancelSearch { get; set; }

    }
}
