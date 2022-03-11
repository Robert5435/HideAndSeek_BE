using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HideAndSeek.Models
{
    public class Data
    {
        public IFormFile File { get; set; }
        public string Key { get; set; }
    }
}
