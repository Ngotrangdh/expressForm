using System.Collections.Generic;

namespace expressForm.Web.Models
{
    public class AnswerViewModel 
    {
        public int Id { get; set; }
        //[Answer]
        public List<string> Text { get; set; }
        public QuestionViewModel Question { get; set; }
        public ResponseViewModel Response { get; set; }

    }
}
