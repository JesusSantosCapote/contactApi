using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Result
{
    public class NoContentResult<T> : Result<T>
    {
        public override ResultType ResultType => ResultType.NoContent;

        public override List<string> Errors => new List<string>();

        public override T Data => default(T);
    }
}
