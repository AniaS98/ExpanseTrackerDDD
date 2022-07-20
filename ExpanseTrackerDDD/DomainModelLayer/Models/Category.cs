using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Models
{
    public class Category
    {
        public string Name { get; protected set; }
        public string IconPath { get; protected set; }
        private List<Subcategory> subcategories = new List<Subcategory>();
        public ReadOnlyCollection<Subcategory> Subcategories { get { return this.subcategories.AsReadOnly(); } }

    }
}
