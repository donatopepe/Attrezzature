using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AttrOleo.Models
{
    public class FileDescriptionShort
    {
        
        public string Category { get; set; }
        public string Description { get; set; }

        public IFormFile File { get; set; }

        public int? Id { get; set; }
    }
}
