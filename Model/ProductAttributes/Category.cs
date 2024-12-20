﻿using Model.ProductAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<SubCategory> SubCategory { get; set; }
        public string Icon { get; set; }
        public bool Active { get; set; }
    }
}
