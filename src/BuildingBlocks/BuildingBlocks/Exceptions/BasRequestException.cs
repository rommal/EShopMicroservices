using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions
{
    public class BasRequestException : ApplicationException
    {
        public BasRequestException(string message) : base(message)
        {

        }

        public BasRequestException(string message, string details) : base(message)
        {
            Details = details;
        }

        public string? Details { get; }
    }
}
