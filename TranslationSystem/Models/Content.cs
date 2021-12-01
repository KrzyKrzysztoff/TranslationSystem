using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TranslationSystem.Models
{
    public class Content
    {
        public int Id { get; set; }
        public string Translated { get; set; }
        [Required(ErrorMessage = "Text is required!")]
        [MinLength(2, ErrorMessage = "Text is too short!")]
        [MaxLength(100, ErrorMessage = "Text is too long!")]
        public string Text { get; set; }
        public string Translation { get; set; }
        public DateTime CurrentDate { get; set; }
    }

}
