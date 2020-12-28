using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expressForm.Web.Models
{
    public class AnswerViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public QuestionViewModel Question { get; set; }
        public ResponseViewModel Response { get; set; }
    }
}
