using System;

namespace expressForm.Core.Forms
{
    public class Form
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }

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
