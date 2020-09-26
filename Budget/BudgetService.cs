using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;

namespace Budget
{
    public class BudgetService
    {
        private readonly IBudgetRepo _repo;

        public BudgetService(IBudgetRepo repo)
        {
            _repo = repo;
        }

        public double Query(DateTime start, DateTime end)
        {
            if (start > end)
            {
                return 0;
            }

            var period = new Period(start, end);
            var totalBudget = 0;
            foreach (var budget in _repo.GetAll())
            {
                totalBudget += budget.GetOverlappingAmount(period);
            }

            return totalBudget;
        }
    }
}