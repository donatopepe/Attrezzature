using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AttrOleo.Models
{
    public class FileDescriptionNoFile
    {
        [Key]
        public Guid Id { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
     
    }
}
