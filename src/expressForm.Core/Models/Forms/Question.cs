using System.Collections.Generic;

namespace expressForm.Core.Models.Forms
{
    public class Question
    {
        public int Id { get; set; }
        public QuestionType Type { get; set; }
        public string Text { get; set; }
        public bool IsRequired { get; set; }
        public string Options { get; set; }
        public Form Form { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
