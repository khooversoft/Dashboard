using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.StateValidation
{
    public record ValidationMessage
    {
        public string Key { get; init; } = null!;

        public string? ErrorMsg { get; init; } = null!;
    }
}
