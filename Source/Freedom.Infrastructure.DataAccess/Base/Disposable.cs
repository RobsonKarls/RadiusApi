using System;

namespace Freedom.Infrastructure.DataAccess.Base
{
    public abstract class Disposable : IDisposable
    {
        bool _disposed;

        ~Disposable()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void DisposeCore()
        {
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                DisposeCore();
            }

            _disposed = true;
        }
    }
}
