namespace Biblioteka.DAL;

public static class CollectionExtensions
{
    public static void UpdateCollection<T>(
        this ICollection<T> existingCollection,
        IEnumerable<T> newModels,
        Func<T, Guid> getKey)
    {
        ArgumentNullException.ThrowIfNull(existingCollection);
        ArgumentNullException.ThrowIfNull(newModels);
        ArgumentNullException.ThrowIfNull(getKey);

        var existingKeys = existingCollection.Select(getKey).ToList();

        // Удаляем элементы, которых нет в новом списке
        var itemsToRemove = existingCollection
            .Where(item => newModels.All(newModel => getKey(newModel) != getKey(item)))
            .ToList();

        foreach (var item in itemsToRemove)
        {
            existingCollection.Remove(item);
        }

        // Добавляем элементы, которых нет в существующей коллекции
        foreach (var newModel in newModels)
        {
            if (!existingKeys.Contains(getKey(newModel)))
            {
                existingCollection.Add(newModel);
            }
        }
    }
}