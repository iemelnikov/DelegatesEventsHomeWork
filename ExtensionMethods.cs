namespace DelegatesEventsHomeWork
{
    internal static class ExtensionMethods
    {
        /// <summary>
        /// Обобщённая функция расширения, находящая максимальный элемент коллекции
        /// </summary>
        /// <param name="convertToNumberDelegate">Делегат, преобразующий входной тип в число для возможности поиска максимального значения.</param>
        /// <returns>Максимальный элемент коллекции класса <typeparamref name="T" /></returns>
        public static T? GetMax<T>(this IEnumerable<T> collection, Func<T, float> convertToNumberDelegate) where T : class
        {
            if (collection.Any())
            {
                return collection.MaxBy(_ => convertToNumberDelegate(_));
            }
            return default;
        }
    }
}
