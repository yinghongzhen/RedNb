using RedNb.Auth.Application.Contracts.Platforms;
using RedNb.Auth.Application.Contracts.Platforms.Dtos;
using RedNb.Auth.Domain.Platforms;

namespace RedNb.Auth.Application.Platforms
{
    public class PlatformAppService : IPlatformAppService
    {
        private readonly IRepository<Platform, long> _platformRepository;
        private readonly IObjectMapper _objectMapper;

        public PlatformAppService(IRepository<Platform, long> platformRepository,
            IObjectMapper objectMapper)
        {
            _platformRepository = platformRepository;
            _objectMapper = objectMapper;
        }

        /// <summary>
        /// 添加平台
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddAsync(PlatformAddInputDto input)
        {
            var Platform = _objectMapper.Map<PlatformAddInputDto, Platform>(input);

            await _platformRepository.InsertAsync(Platform);
        }

        /// <summary>
        /// 批量删除平台
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task DeleteBatchAsync(DeleteBatchInputDto input)
        {
            if (input.Ids != null)
            {
                foreach (var item in input.Ids)
                {
                    var Platform = await _platformRepository.GetAsync(item);

                    await _platformRepository.DeleteAsync(Platform);
                }
            }
        }

        /// <summary>
        /// 更新平台
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateAsync(PlatformUpdateInputDto input)
        {
            var Platform = await _platformRepository.GetAsync(input.Id);

            _objectMapper.Map(input, Platform);
        }
    }
}