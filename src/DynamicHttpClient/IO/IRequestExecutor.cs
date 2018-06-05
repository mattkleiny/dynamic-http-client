using System.Threading.Tasks;

namespace DynamicHttpClient.IO
{
  public interface IRequestExecutor
  {
    IRequestBuilder PrepareRequest();
    IResponse       ExecuteRequest(IRequest request);
    Task<IResponse> ExecuteRequestAsync(IRequest request);
  }
}