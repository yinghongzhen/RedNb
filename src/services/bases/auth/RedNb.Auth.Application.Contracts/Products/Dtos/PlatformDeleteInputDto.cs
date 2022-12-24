using RedNb.Core.Contracts;
using System.ComponentModel.DataAnnotations;

namespace RedNb.Auth.Application.Contracts.Products.Dtos;

public class PlatformDeleteInputDto
{
    public long ProductId { get; set; }

    public long PlatformId { get; set; }
}
