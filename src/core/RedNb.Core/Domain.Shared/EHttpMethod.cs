namespace RedNb.Core.Domain.Shared;

/// <summary>
/// restful类型
/// </summary>
[Description("restful类型")]
public enum EHttpMethod
{
    /// <summary>
    /// Get
    /// </summary>
    [Description("Get")]
    Get = 0,

    /// <summary>
    /// Post
    /// </summary>
    [Description("Post")]
    Post = 1,

    /// <summary>
    /// Put
    /// </summary>
    [Description("Put")]
    Put = 2,

    /// <summary>
    /// Delete
    /// </summary>
    [Description("Delete")]
    Delete = 3
}
