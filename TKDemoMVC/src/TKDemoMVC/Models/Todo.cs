﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TKDemoMVC.Models {
    public class TodoItem {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public bool IsDone { get; set; }
    }
}
