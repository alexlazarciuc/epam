using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Api
{
    public interface IActionResult { }

    public class OkResult : IActionResult { }

    public class OkObjectResult : IActionResult
    {
        public object Value { get; }
        public OkObjectResult(object value) => Value = value;
    }

    public class BadRequestResult : IActionResult
    {
        public string Error { get; }
        public BadRequestResult(string error) => Error = error;
    }

    public class ConflictResult : IActionResult
    {
        public string Conflict { get; }
        public ConflictResult(string error) => Conflict = error;
    }
}
