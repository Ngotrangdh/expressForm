using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace expressForm.Web.Models
{
    public class ResponseViewModel
    {
        public string FormTitle { get; set; }
        public string FormDescription { get; set; }
        public List<AnswerViewModel> Answers { get; set; }
    }
}