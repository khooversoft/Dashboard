using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Tools;

namespace Toolbox.StateValidation
{
    public class ValidationBind
    {
        private readonly object _model;

        public ValidationBind(object model)
        {
            model.VerifyNotNull(nameof(model));

            _model = model;
        }

        //public 
    }
}
