using RedNb.Core.Contracts;
using RedNb.Core.Extensions;

namespace RedNb.Core.Application;

public class LoginUser : ITransientDependency
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly LoginUserDto _loginUser;

    public LoginUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;

        _loginUser = _httpContextAccessor.GetLoginUser();
    }

    public long UserId
    {
        get
        {
            return _loginUser.UserId;
        }
    }

    public string Username
    {
        get
        {
            return _loginUser.Username;
        }
    }

    public string Nickname
    {
        get
        {
            return _loginUser.Nickname;
        }
    }

    public long ReferenceId
    {
        get
        {
            return _loginUser.ReferenceId ?? 0;
        }
    }

    public string ReferenceName
    {
        get
        {
            return _loginUser.ReferenceName;
        }
    }

    public long TenantId
    {
        get
        {
            return _loginUser.TenantId;
        }
    }

    public string TenantName
    {
        get
        {
            return _loginUser.TenantName;
        }
    }

    public string Flag
    {
        get
        {
            return _loginUser.Flag;
        }
    }

    public bool IsValid()
    {
        return UserId > 0 &&
            !String.IsNullOrWhiteSpace(Username) &&
            !String.IsNullOrWhiteSpace(Nickname) &&
            TenantId > 0 &&
            !String.IsNullOrWhiteSpace(TenantName);
    }
}
