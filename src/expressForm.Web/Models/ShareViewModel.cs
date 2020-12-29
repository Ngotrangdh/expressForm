using System.ComponentModel.DataAnnotations;

namespace expressForm.Web.Models
{
    public class ShareViewModel
    {
        [Display(Prompt ="Recipient's Email")]
        public string Email { get; set; } 
        public string Subject { get; set; }
        public string Message { get; set; } = "I've invited you to fill out a form:";
        public string Link { get; set; }
        public string FormTitle { get; set; }
        public string FormDescription { get; set; }
        public int FormId { get; set; }
    }
}
