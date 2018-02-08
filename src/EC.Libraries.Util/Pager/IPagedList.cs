namespace EC.Libraries.Util.Pager
{
    public interface IPagedList
    {
        int CurrentPageIndex { get; set; }
        int PageSize { get; set; }
        int TotalCount { get; set; }
    }
}
