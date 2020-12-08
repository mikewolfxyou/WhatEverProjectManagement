using System.Threading;

namespace ProjectManagement.Api
{
    public class ApplicationWideCancellationTokenSource
    {
        
        public CancellationTokenSource TokenSource { get; }

        public ApplicationWideCancellationTokenSource(CancellationTokenSource tokenSource)
        {
            TokenSource = tokenSource;
        }
    }
}