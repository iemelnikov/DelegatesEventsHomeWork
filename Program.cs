using System.Globalization;
using System.Text.RegularExpressions;

namespace DelegatesEventsHomeWork
{
    public class Program
    {
        private static readonly Func<string, float> ConvertStringToNumberDelegate = (_) =>
        {
            return float.TryParse(Regex.Replace(_, "[.,]", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), out float number) ? number : 0;
        };

        private static void Main(string[] args)
        {
            string[] array = {"4", "18", "38.9", "2", "5,2" };
            string? arrMembMax = array.GetMax(ConvertStringToNumberDelegate);
            Console.WriteLine($"Максимальный элемент из массива [{string.Join(", ", array)}]: {arrMembMax ?? "не найден"}.");
            FilesSearcher filesSearcher = new FilesSearcher(Path.GetTempPath()); // поиск файлов в папке Temp
            filesSearcher.FileFound += OnFileFound;
            filesSearcher.RunFilesSearch();
            filesSearcher.FileFound -= OnFileFound;
        }

        private static void OnFileFound(object? sender, FileArgs e)
        {
            Console.WriteLine($"Найден файл: {e.FileName}.");
            Console.WriteLine("Продолжить поиск? Enter - да, (n) - нет: ");
            if (Console.ReadLine()?.ToLower() == "n")
                e.ToFinishSearch = true;
        }
    }
}
