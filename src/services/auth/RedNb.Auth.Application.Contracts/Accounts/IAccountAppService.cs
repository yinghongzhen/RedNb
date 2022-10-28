using RedNb.Auth.Application.Contracts.Accounts.Dtos;

namespace RedNb.Auth.Application.Contracts.Accounts
{
    public interface IAccountAppService : IApplicationService, ITransientDependency
    {
        Task<LoginOutputDto> WxAppRegAsync(WxAppRegInputDto input);

        Task<LoginOutputDto> WxAppLoginAsync(WxAppLoginInputDto input);

        Task<LoginOutputDto> ClientRegAsync(ClientRegInputDto input);

        Task<LoginOutputDto> LoginAsync(LoginInputDto input);

        Task<LoginOutputDto> ClientLoginAsync(ClientLoginInputDto input);

        Task<LoginOutputDto> ActiveAsync(AccountActiveInputDto input);
    }
}
