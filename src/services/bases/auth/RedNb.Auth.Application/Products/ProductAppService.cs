using RedNb.Core.Domain;
using RedNb.Auth.Application.Contracts.Products;
using RedNb.Auth.Application.Contracts.Products.Dtos;
using RedNb.Auth.Domain.Products;
using RedNb.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.ObjectMapping;

namespace RedNb.Auth.Application.Products
{
    public class ProductAppService : IProductAppService
    {
        private readonly IRepository<Product, long> _productRepository;
        private readonly IObjectMapper _objectMapper;

        public ProductAppService(IRepository<Product, long> platformRepository,
            IObjectMapper objectMapper)
        {
            _productRepository = platformRepository;
            _objectMapper = objectMapper;
        }

        public async Task AddAsync(ProductAddInputDto input)
        {
            var model = _objectMapper.Map<ProductAddInputDto, Product>(input);

            model.AddPlatform(new Platform()
            {
                Name = "PC管理端",
            });

            model.AddPlatform(new Platform()
            {
                Name = "移动端",
            });

            await _productRepository.InsertAsync(model);
        }

        public async Task DeletePlatformAsync(PlatformDeleteInputDto input)
        {
            var queryable = await _productRepository.WithDetailsAsync(m => m.Platforms);

            var product = await queryable.SingleOrDefaultAsync(m=>m.Id == input.ProductId);

            product.DeletePlatform(input.PlatformId);
        }

        public async Task DeleteAsync(DeleteInputDto input)
        {
            //foreach (var item in input.Ids)
            //{
            //    var product = await _productRepository.GetAsync(item);

            //    //if (await _pla
            //    //    .AnyAsync(m => m.ProductId == platform.Id))
            //    //{
            //    //    throw new UserFriendlyException("平台已配置权限，禁止删除");
            //    //}

            //    await _productRepository.DeleteAsync(platform);
            //}
        }

        public async Task UpdateAsync(ProductUpdateInputDto input)
        {
            var model = await _productRepository.GetAsync(input.Id);

            _objectMapper.Map(input, model);
        }

        public async Task<List<ProductOutputDto>> GetAllAsync(ProductGetAllInputDto input)
        {
            var queryable = await _productRepository.WithDetailsAsync(m => m.Platforms);

            var list = await queryable
                .OrderByDescending(m => m.Id)
                .ToListAsync();

            var data = _objectMapper.Map<List<Product>, List<ProductOutputDto>>(list);

            return data;
        }
    }
}