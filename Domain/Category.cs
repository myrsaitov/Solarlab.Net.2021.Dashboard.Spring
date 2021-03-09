﻿using System.Collections;
using System.Collections.Generic;
using WidePictBoard.Domain.General;

namespace WidePictBoard.Domain
{
    public class Category : EntityMutable<int>
    {
        public string Name { get; set; }

        public Category ParentCategory { get; set; }

        public ICollection<Category> ChildCategories { get; set; }
    }
}