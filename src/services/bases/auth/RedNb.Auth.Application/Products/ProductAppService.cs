using RedNb.Auth.Application.Contracts.Products;
using RedNb.Auth.Application.Contracts.Products.Dtos;
using RedNb.Auth.Domain.Platforms;
using RedNb.Auth.Domain.Products;

namespace RedNb.Auth.Application.Products
{
    public class ProductAppService : IProductAppService
    {
        private readonly IRepository<Product, long> _productRepository;
        private readonly IRepository<Platform, long> _platformRepository;
        private readonly IObjectMapper _objectMapper;

        public ProductAppService(IRepository<Product, long> productRepository,
            IRepository<Platform, long> platformRepository,
            IObjectMapper objectMapper)
        {
            _productRepository = productRepository;
            _platformRepository = platformRepository;
            _objectMapper = objectMapper;
        }

        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddAsync(ProductAddInputDto input)
        {
            var product = _objectMapper.Map<ProductAddInputDto, Product>(input);

            await _productRepository.InsertAsync(product);
        }

        /// <summary>
        /// 批量删除产品
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
                    var product = await _productRepository.GetAsync(item);

                    if (await _platformRepository
                        .AnyAsync(m => m.ProductId == product.Id))
                    {
                        throw new UserFriendlyException("产品已配置平台，禁止删除");
                    }

                    await _productRepository.DeleteAsync(product);
                }
            }
        }

        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateAsync(ProductUpdateInputDto input)
        {
            var product = await _productRepository.GetAsync(input.Id);

            _objectMapper.Map(input, product);
        }

        /// <summary>
        /// 获取全部产品列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<ProductOutputDto>> GetAllAsync(ProductGetAllInputDto input)
        {
            var queryable = await _productRepository.WithDetailsAsync(m => m.Platforms);

            var list = await queryable
                .OrderByDescending(m => m.Id)
                .ToListAsync();

            return _objectMapper.Map<List<Product>, List<ProductOutputDto>>(list);
        }
    }
}