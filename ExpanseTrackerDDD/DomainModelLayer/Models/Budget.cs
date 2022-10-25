using ExpanseTrackerDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.DomainModelLayer.Models.Basic;
using ExpanseTrackerDDD.DomainModelLayer.Interfaces;
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

    public enum BudgetStatus
    {
        Active,
        Past
    }

    public class Budget : AggregateRoot
    {
        public string Name { get; protected set; }
        public Money Limit { get; protected set; }
        public Money CurrentValue { get; protected set; }
        public decimal LimitUtilization { get; protected set; }
        public BudgetType Type { get; protected set; }
        public DateTime StartTime { get; protected set; }
        public DateTime EndTime { get; protected set; }
        public BudgetStatus CurrentStatus { get; protected set; }
        public Guid AccountId { get; protected set; }
        
        private List<Category> _categories;
        public IEnumerable<Category> Categories
        {
            get { return _categories.AsReadOnly(); }
        }

        protected Budget() { }

        public Budget(Guid id, string name, Money limit, BudgetType type, IEnumerable<Category> categories, Guid accountId) : base(id) //obsługa gdy limit jest zerowy
        {
            this.Name = name;
            this.Id = id;
            this.Limit = limit;
            this.CurrentValue = new Money(0, limit.Currency);
            this.LimitUtilization = CurrentValue.Amount/Limit.Amount;
            this.Type = type;
            this.StartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.EndTime = this.StartTime.AddMonths(1).AddDays(-1);
            this.CurrentStatus = BudgetStatus.Active;
            this.AccountId = accountId;
            this._categories = (List<Category>)categories; 
        }

        public void DeactivateBudget()
        {
            this.CurrentStatus = BudgetStatus.Past;
        }

        public void UpdateCurrentValue(Money value)
        {
            this.CurrentValue += value;
            this.LimitUtilization = this.CurrentValue.Amount / this.Limit.Amount;
        }

        public void UpdateLimit(Money limit)
        {
            this.Limit = limit;
            this.LimitUtilization = this.CurrentValue.Amount / this.Limit.Amount;
        }

        public void ChangeCategories(List<Category> newCategories)
        {
            _categories = newCategories;
        }



    }
}
