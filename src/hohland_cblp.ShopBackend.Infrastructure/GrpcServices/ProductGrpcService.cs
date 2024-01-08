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

    public override async Task<GetListResponse> GetList(GetListRequest request, ServerCallContext context)
    {
        var products = await _service.GetList(new CancellationToken());
        var result = new GetListResponse
        {
            Products = { _mapper.Map<List<ProductGrpc_v1>>(products) }
        };
        return result;
    }

    public override async Task<GetListByIdsResponse> GetListByIds(GetListByIdsRequest request, ServerCallContext context)
    {
        var products = await _service.GetList(request.Ids.ToList(), new CancellationToken());
        var result = new GetListByIdsResponse
        {
            Products = { _mapper.Map<List<ProductGrpc_v1>>(products) }
        };
        return result;
    }

    public override async Task<GetResponse> Get(GetRequest request, ServerCallContext context)
    {
        var product = await _service.Get(request.Id, new CancellationToken());
        var result = new GetResponse
        {
            Product = _mapper.Map<ProductGrpc_v1>(product)
        };
        return result;
    }

    public override async Task<GetProductTypeResponse> GetProductType(GetProductTypeRequest request, ServerCallContext context)
    {
        var type = await _service.GetProductType(request.Id, new CancellationToken());
        var result = new GetProductTypeResponse
        {
            Type = (ProductTypeGrpc_v1)type
        };
        return result;
    }

    public override async Task<CreateResponse> Create(CreateRequest request, ServerCallContext context)
    {
        var id = await _service.Create(_mapper.Map<Product>(request.Product), new CancellationToken());
        var result = new CreateResponse
        {
            Id = id
        };
        return result;
    }

    public override async Task<CreateSeveralResponse> CreateSeveral(CreateSeveralRequest request, ServerCallContext context)
    {
        var ids = await _service.Create(_mapper.Map<List<Product>>(request.Products), new CancellationToken());
        var result = new CreateSeveralResponse
        {
            Ids = { ids }
        };
        return result;
    }

    public override async Task<UpdateResponse> Update(UpdateRequest request, ServerCallContext context)
    {
        try
        {
            await _service.Update(_mapper.Map<Product>(request.Product), new CancellationToken());
        }
        catch (Exception e)
        {
            return new UpdateResponse { Result = false };
        }
        
        return new UpdateResponse { Result = true };
    }

    public override async Task<DeleteResponse> Delete(DeleteRequest request, ServerCallContext context)
    {
        try
        {
            await _service.Delete(request.Id, new CancellationToken());
        }
        catch (Exception e)
        {
            return new DeleteResponse { Result = false };
        }
        return new DeleteResponse { Result = true };
    }

    public override async Task<DeleteSeveralResponse> DeleteSeveral(DeleteSeveralRequest request, ServerCallContext context)
    {
        try
        {
            await _service.Delete(request.Ids.ToList(), new CancellationToken());
        }
        catch (Exception e)
        {
            return new DeleteSeveralResponse { Result = false };
        }
        return new DeleteSeveralResponse { Result = true };
    }
}