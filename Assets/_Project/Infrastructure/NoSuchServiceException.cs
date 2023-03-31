using System;

namespace Sgorey.Microloans.Infrastructure
{
    public class NoSuchServiceException : Exception
    {
        public NoSuchServiceException(string message) : base(message)
        {
        }
    }
}