using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessLayer
{
    public class Image
    {
        public int UserId { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string imagename { get; set; }
        
        public string imagepath { get; set; }
        
        public HttpPostedFileBase imagefile { get; set; }
    }
}
