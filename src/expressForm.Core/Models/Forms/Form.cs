using expressForm.Core.Models.Forms;
using System;
using System.Collections.Generic;

namespace expressForm.Core.Models.Forms
{
    public class Form
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public List<Question> Questions { get; set; }
        public List<Response> Responses { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public User User { get; set; }

        public Form(string title, string description)
            : this(0, title, description)
        { }

        public Form(int id, string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be blank");
            }

            Id = id;
            Title = title;
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
    }
}
