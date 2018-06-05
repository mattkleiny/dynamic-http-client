using System;
using System.Threading.Tasks;

namespace DynamicHttpClient.IO
{
  public interface IRequestExecutor : IDisposable
  {
    IRequestBuilder PrepareRequest();
    Task<IResponse> ExecuteRequestAsync(IRequest request);
  }
}