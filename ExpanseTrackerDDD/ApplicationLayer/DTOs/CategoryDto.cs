using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.DTOs
{
    public class CategoryDto
    {
        public string Name { get; set; }
        public string IconPath { get; set; }
        public Guid TransactionDtoId { get; set; }
    }
}
