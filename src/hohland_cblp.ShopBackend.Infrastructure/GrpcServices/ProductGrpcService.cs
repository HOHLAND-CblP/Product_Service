using AutoMapper;
using Grpc.Core;
using ProductGrpc;
using hohland_cblp.ShopBackend.Domain.Entities;
using hohland_cblp.ShopBackend.Domain.Contracts.Services;


namespace hohland_cblp.ShopBackend.Infrastructure.GrpcServices;

public class ProductGrpcService : ProductGrpc.ProductGrpcService.ProductGrpcServiceBase
{
    private readonly IProductService _service;
    private readonly IMapper _mapper;

    public ProductGrpcService(IProductService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public override async Task<GetProductListResponse> GetProductList(GetProductListRequest request,
        ServerCallContext context)
    {
        var result = await _service.GetList(new CancellationToken());

        var resultGrpc = _mapper.Map<List<ProductModel>>(result);

        var response = new GetProductListResponse()
        {
            Products = { resultGrpc }
        };

        return response;
    }

    public override async Task<CreateProductResponse> CreateProduct(CreateProductRequest request,
        ServerCallContext context)
    {
        var product = _mapper.Map<Product>(request.Product);
        var result = await _service.Create(product, new CancellationToken());

        return _mapper.Map<CreateProductResponse>(result);
    }

    public override async Task<GetProductByIdResponse> GetProductById(GetProductByIdRequest request,
        ServerCallContext context)
    {
        var result = await _service.Get(request.Id, new CancellationToken());

        var resultGrpc = _mapper.Map<ProductModel>(result);

        var response = new GetProductByIdResponse()
        {
            Product = resultGrpc
        };

        return response;
    }

    /*public override async Task<UpdateProductPriceResponse> UpdateProductPrice(UpdateProductPriceRequest request,
        ServerCallContext context)
    {
        var result = await _service.Update(request.Id, new CancellationToken());

        return _mapper.Map<UpdateProductPriceResponse>(result);
    }*/

    /*public override async Task<DeleteByIdResponse> DeleteById(DeleteByIdRequest request, ServerCallContext context)
    {
        var result = await _service.Delete(request.Id, new CancellationToken());

        return _mapper.Map<DeleteByIdResponse>(result);
    }*/
}