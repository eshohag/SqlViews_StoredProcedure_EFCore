﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models
{
    [Keyless]
    public class BlogPostsCount
    {
        public string BlogName { get; set; }
        public int PostCount { get; set; }
    }
}
