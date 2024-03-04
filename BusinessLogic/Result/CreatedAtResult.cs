using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Result
{
    internal class CreatedAtResult<T> : Result<T>
    {
        private readonly T _data;
        public CreatedAtResult(T data) 
        {
            _data = data;
        }
        public override ResultType ResultType => ResultType.CreatedAt;

        public override List<string> Errors => new List<string>();

        public override T Data => _data;
    }
}
