using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Result
{
    public class UnauthorizedResult<T> : Result<T>
    {
        private readonly string _error;
        public UnauthorizedResult(string error) 
        {
            _error = error;
        }
        public override ResultType ResultType => ResultType.Unauthorized;
        public override List<string> Errors => new List<string> { _error};
        public override T Data => default(T);
    }
}
