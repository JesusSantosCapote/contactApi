using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Result
{
    public class UnexpectedResult<T> : Result<T>
    {
        private readonly string _error;
        public UnexpectedResult(string error = "") 
        {
            _error = error;
        }
        public override ResultType ResultType => ResultType.Unexpected;
        public override List<string> Errors => new List<string> { "There was an unexpected problem: " + $"{_error}" };
        public override T Data => default(T);
    }
}
