using System.Threading.Tasks;

namespace DynamicHttpClient.IO
{
  /// <summary>
  /// An executor for <see cref="IRequest"/>s.
  /// </summary>
  public interface IRequestExecutor
  {
    /// <summary>
    /// Creates a new <see cref="IRequestBuilder"/>.
    /// </summary>
    IRequestBuilder BuildRequest();

    /// <summary>
    /// Executes the given <see cref="IRequest"/> and returns the <see cref="IResponse"/>.
    /// </summary>
    IResponse ExecuteRequest(IRequest request);

    /// <summary>
    /// Asynchronously executes the given <see cref="IRequest"/> and returns a task representing the operation.
    /// </summary>
    Task<IResponse> ExecuteRequestAsync(IRequest request);
  }
}