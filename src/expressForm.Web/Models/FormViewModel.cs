using expressForm.Core.Models.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace expressForm.Web.Models
{
    public class FormViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        public string Link { get; set; }
        public IEnumerable<QuestionViewModel> Questions { get; set; }
        public IEnumerable<Response> Responses { get; set; } = new List<Response>();
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public User User { get; set; }
    }
}
