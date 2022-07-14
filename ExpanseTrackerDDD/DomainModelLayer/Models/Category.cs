using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Models
{
    public class Category
    {
        public string Name { get; protected set; }
        public List<Subcategory> Subcategories { get; protected set; }

    }
}
