﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites
{
    public class Hero
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Item> Items { get; set; }
    }
}
