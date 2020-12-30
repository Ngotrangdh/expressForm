namespace expressForm.Web.Models
{
    public class AnswerViewModel 
    {
        public int Id { get; set; }
        [Answer]
        public string Text { get; set; }
        public QuestionViewModel Question { get; set; }
        public ResponseViewModel Response { get; set; }

    }
}
