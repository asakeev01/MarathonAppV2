﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Distances
{
    public class DistancePrice
    {
        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public double Price { get; set; }
        public int DistanceId { get; set; }
        public virtual Distance Distance { get; set; }
    }
}
