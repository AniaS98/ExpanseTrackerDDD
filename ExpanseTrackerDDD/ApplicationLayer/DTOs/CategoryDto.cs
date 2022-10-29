using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.DTOs
{
    public enum SubcategoryNameDto
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

    public enum CategoryNameDto
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


    public class CategoryDto
    {
        public CategoryNameDto Name { get; set; }
        public SubcategoryNameDto SubcategoryName { get; set; }
        //private string IconPath;
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
