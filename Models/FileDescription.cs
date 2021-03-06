using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AttrOleo.Models
{
    public class FileDescription
    {
        [Key]
        public Guid Id { get; set; }
        public string FileName { get; set; }
        [Display(Name = "Categoria", Prompt = "Categoria", Description = "Categoria")]
        public string Category { get; set; }
        public string Description { get; set; }
        public byte[] File { get; set; }
        
        public DateTime CreatedTimestamp { get; set; }
        
        public DateTime UpdatedTimestamp { get; set; }
        public string ContentType { get; set; }

        public Pressione Pressione { get; set; }
        public Valvola Valvola { get; set; }
        public Disco Disco { get; set; }
    }
}
