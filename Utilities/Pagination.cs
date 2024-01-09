public class Pagination<T>
{
    public int PageSize = 10; // 每頁幾筆資料
    public int TotalCount { get; } = 0;
    public int PageCount { get; } = 0;

    private readonly List<T> _list;

    public Pagination(List<T> list)
    {
        _list = list;

        TotalCount = _list.Count;
        PageCount = (int)Math.Ceiling((decimal)TotalCount / (decimal)PageSize);
    }

    public List<T> GetPageList(int pageIndex)
    {
        int index = pageIndex * PageSize;
        int count = Math.Min(PageSize, TotalCount - index);

        if (count > 0)
            return _list.GetRange(index, count);
        else
            return new List<T>();
    }
}
