using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginApi.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public string CommentBy { get; set; }

        public string CommentText { get; set; }
    }
}
