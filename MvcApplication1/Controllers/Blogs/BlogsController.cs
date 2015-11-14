using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcApplication1.Controllers.Blogs;

namespace MvcApplication1.Controllers.Videos
{
    public class BlogsController : ApiController
    {
        public List<Blog> Get()
        {
            return new List<Blog> { new Blog { Name = "Blake's First Post", Description = "HI GUISE. I'M DUMB" }, 
                                    new Blog { Name = "Blake's Second Post", Description = "GUISE. I'M STILL DUMB" },
                                    new Blog { Name = "Blake's Third Post", Description = "GUISE. HELP" },
                                    new Blog { Name = "Blake's Fourth Post", Description = "GUISE. PLS" }};
        }
    }
}
