using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingCheng.Site.Services
{
    public class PostCommentService
    {
        private readonly DbContext _context;

        public PostCommentService(DbContext context)
        {
            _context = context;
        }


    }
}
