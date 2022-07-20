using Base.DomainModelLayer.Events;
using Base.DomainModelLayer.Models;
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

    public class Budget : AggregateRoot
    {
        public string Name { get; protected set; }
        public Guid Id { get; protected set; }
        public Money Limit { get; protected set; }
        public Money CurrentValue { get; protected set; }
        public Money LimitUtilization { get; protected set; }
        public Currency Currency { get; protected set; }
        public BudgetType Type { get; protected set; }
        public DateTime StartTime { get; protected set; }
        public DateTime EndTime { get; protected set; }
        public Guid UserId { get; protected set; }

        public Budget(Guid id, IDomainEventPublisher domainEventPublisher, string name, Money limit, BudgetType type, Currency currency, Guid userId) : base(id, domainEventPublisher)
        {
            this.Name = name;
            this.Id = new Guid();
            this.Limit = limit;
            this.Currency = currency;
            this.CurrentValue = new Money(0,currency);
            this.LimitUtilization = new Money(0,currency);
            this.Type = type;
            this.StartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.EndTime = this.StartTime.AddMonths(1).AddDays(-1);
            this.UserId = userId;
        }

        public Budget(Guid id, IDomainEventPublisher domainEventPublisher, string name, Money limit, BudgetType type, DateTime startDate, DateTime endDate, Currency currency, Guid userId) : base(id, domainEventPublisher)
        {
            this.Name = name;
            this.Id = new Guid();
            this.Limit = limit;
            this.Currency = currency;
            this.CurrentValue = new Money(0, currency);
            this.Type = type;
            this.StartTime = startDate;
            this.EndTime = endDate;
            this.UserId = userId;
        }

        private void RenewBudget(Budget oldBudget)//dopisać żeby raport się restartował dzień po ostatnim dniu budżetu
        {
            this.Name = oldBudget.Name;
            this.Id = new Guid();
            this.Limit = oldBudget.Limit;
            this.CurrentValue = new Money(0, this.Currency);
            this.Type = oldBudget.Type;
            this.StartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.EndTime = this.StartTime.AddMonths(1).AddDays(-1);
        }

        public void UpdateCurrentValue(Money value)
        {
            this.CurrentValue += value;
            this.LimitUtilization = this.CurrentValue / this.Limit;
        }

        public void UpdateLimit(Money limit)
        {
            this.Limit = limit;
            this.LimitUtilization = this.CurrentValue / this.Limit;

        }


    }
}
