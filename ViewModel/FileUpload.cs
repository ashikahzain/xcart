using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xcart.ViewModel
{
    public class FileUpload
    {
        public IFormFile file { get; set; }
        public string Item { get; set; }
    }
}
