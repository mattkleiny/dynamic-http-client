using System.Threading.Tasks;

namespace DynamicHttpClient.IO
{
  public interface IRequestExecutor
  {
    IRequestBuilder PrepareRequest();
    Task<IResponse> ExecuteRequestAsync(IRequest request);
  }
}