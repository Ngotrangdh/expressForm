using expressForm.Core.Models.Forms;
using System;
using System.Collections.Generic;

namespace expressForm.Core.Models.Forms
{
    public class Form
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
        public List<Response> Responses { get; set; } = new List<Response>();
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public User User { get; set; }
    }
}
