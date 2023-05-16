using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QingCheng.Site.Data.Blogs;
using QingCheng.Tools;
using QingCheng.Tools.Helper;
using QingCheng.Site.Models;
using QingCheng.Site.Models.Blogs;
using QingCheng.Site.Models.Dto;

namespace QingCheng.Site.Services.Blogs
{
    [IgnoreInject]
    public abstract class PostCommonService
    {
        readonly DbContext _context;

        protected PostCommonService(DbContext context)
        {
            _context = context;
        }

        
    }
}
