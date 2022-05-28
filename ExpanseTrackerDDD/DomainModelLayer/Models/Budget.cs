using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Models
{
    //Na początku działania aplikacji podczytana zostanie obecna data, sprawdzone zostaną timestampy obecnych budżetów i ewentualnie przeniesione do Past. Jak jest miesięczny to dodany zostanie nowy budżet
    public enum BudgetType
    {
        Monthly,
        OneTime
    }

    public class Budget //agregat?
    {
        public string Name { get; protected set; }
        public Guid Id { get; protected set; }
        public float Limit { get; protected set; }
        public float CurrentValue { get; protected set; }
        public float LimitUtilization { get; protected set; }
        public BudgetType Type { get; protected set; }
        public DateTime StartTime { get; protected set; }
        public DateTime EndTime { get; protected set; }

        public Budget(string name, float limit, BudgetType type)
        {
            this.Name = name;
            this.Id = new Guid();
            this.Limit = limit;
            this.CurrentValue = 0;
            this.LimitUtilization = 0;
            this.Type = type;
            this.StartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.EndTime = this.StartTime.AddMonths(1).AddDays(-1);
        }

        public Budget(string name, float limit, BudgetType type, DateTime startDate, DateTime endDate)
        {
            this.Name = name;
            this.Id = new Guid();
            this.Limit = limit;
            this.CurrentValue = 0;
            this.Type = type;
            this.StartTime = startDate;
            this.EndTime = endDate;
        }

        private void RenewBudget(Budget oldBudget)//dopisać żeby raport się startowało na początku miesiąca i na końcu
        {
            this.Name = oldBudget.Name;
            this.Id = new Guid();
            this.Limit = oldBudget.Limit;
            this.CurrentValue = 0;
            this.Type = oldBudget.Type;
            this.StartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.EndTime = this.StartTime.AddMonths(1).AddDays(-1);
        }

        public void UpdateCurrentValue(float value)
        {
            this.CurrentValue += value;
            this.LimitUtilization = this.CurrentValue / this.Limit;
        }



    }
}
