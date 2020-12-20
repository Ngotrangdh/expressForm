using expressForm.Core.Models.Forms;
using System.Collections.Generic;

namespace expressForm.Core.Models.Forms
{
    public class User
    {
        public int Id { get; set; }
        public List<Form> Forms { get; set; }
    }
}
