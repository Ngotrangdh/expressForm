using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expressForm.Web.Models
{
    public class ResponseViewModel
    {
        public string FormTitle { get; set; }
        public string FormDescription { get; set; }
        public List<AnswerViewModel> Answers { get; set; }
    }
}
