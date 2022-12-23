using Volo.Abp.Application.Dtos;

namespace RedNb.Core.Contracts;

public class PagedInputDto : IPagedResultRequest
{
    private int _pageIndx = 1;
    private int _pageSize = 20;

    public PagedInputDto()
    {
    }

    /// <summary>
    /// 当前页码
    /// </summary>
    public int PageIndex
    {
        get
        {
            return _pageIndx;
        }
        set
        {
            _pageIndx = value <= 0 ? 1 : value;
        }
    }

    /// <summary>
    /// 每页条数
    /// </summary>
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = value <= 0 ? 20 : value;
        }
    }

    [JsonIgnore]
    public int SkipCount
    {
        get
        {
            return (PageIndex - 1) * PageSize;
        }
        set
        {

        }
    }

    [JsonIgnore]
    public int MaxResultCount
    {
        get
        {
            return PageSize;
        }
        set
        {

        }
    }
}
