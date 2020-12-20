using expressForm.Core.Models.Forms;
using System;
using System.Collections.Generic;

namespace expressForm.Core.Models.Forms
{
    public class Response
    {
        public int Id { get; set; }
        public Form Form { get; set; }
        public List<Answer> Answers { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
