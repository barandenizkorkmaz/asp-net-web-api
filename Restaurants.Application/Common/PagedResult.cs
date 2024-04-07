namespace Restaurants.Application.Common;

public class PagedResult<T>
{
    public PagedResult(IEnumerable<T> items, int totalCount, int pageSize, int pageNumber)
    {
        Items = items;
        NumTotalItems = totalCount;
        TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        ItemsFrom = pageSize * (pageNumber - 1) + 1;
        ItemsTo = Math.Min(ItemsFrom + (pageSize - 1), totalCount);
    }

    public IEnumerable<T> Items { get; set; }
    public int TotalPages {  get; set; }
    public int NumTotalItems { get; set; }
    public int ItemsFrom { get; set; }
    public int ItemsTo { get; set; }
}
