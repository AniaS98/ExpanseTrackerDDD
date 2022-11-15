using BaseDDD.DomainModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Models
{
    public enum SubcategoryName
    {
        [Description("Groceries")]
        Food_Groceries,
        [Description("Sweets")]
        Food_Sweets,
        [Description("Restaurant")]
        Food_Restaurant,
        [Description("Fast Food")]
        Food_FastFood,
        [Description("Cafe")]
        Food_Cafe,
        [Description("Clothes & Shoes")]
        Shopping_ClothesShoes,
        [Description("Medicaments")]
        Shopping_Medicaments,
        [Description("Electronics")]
        Shopping_Electronics,
        [Description("Hobby")]
        Shopping_Hobby,
        [Description("Entertainment")]
        Shopping_Entertainment,
        [Description("Gifts")]
        Shopping_Gifts,
        [Description("Health & Beauty")]
        Shopping_HealthBeauty,
        [Description("Home and Garden Accessories")]
        Shopping_Homegarden,
        [Description("Jewellery")]
        Shopping_Jewellery,
        [Description("Children")]
        Shopping_Children,
        [Description("Pets")]
        Shopping_Pets,
        [Description("Stationery")]
        Shopping_Stationery,
        [Description("Tools")]
        Shopping_Tools,
        [Description("Utilities")]
        Shopping_Utilities,
    }

    public enum CategoryName
    {
        Food,
        Shopping,
        Housing,
        Transport,
        Vehicles,
        Life,
        PC,
        Communication,
        FinancialExpanses,
        Investments,
        Income,
        Others
    }

    public class Category : ValueObject
    {
        public CategoryName Name { get; protected set; }
        public SubcategoryName SubcategoryName { get; protected set; }

        public Category(CategoryName name, SubcategoryName subcategoryName)
        {
            this.Name = name;
            this.SubcategoryName = subcategoryName;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name.ToString().ToUpper();
            yield return SubcategoryName.ToString().ToUpper();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Name.ToString());
            if (SubcategoryName.ToString() != "")
                sb.Append(" " + SubcategoryName);
            sb.Append("\n");            

            return sb.ToString();
        }
    }
}
