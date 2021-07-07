using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilbayt.WebClient.Models.ResponseErrorModels
{
    public class CreateUserResponseError
    {
        public IDictionary<string, string[]> Errors { get; set; }
    }
}
