using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceManagementSystem2.Models
{
    public class WeekendService
    {
        private readonly DbContext context;

        public WeekendService(DbContext context)
        {
            this.context = context;
        }

        public WeekendService()
        {
            context = new DbContext();
        }

        public IQueryable<WeekendViewModel> GetAll()
        {
            var weekends = context.Weekends.ToList();
            var weekendViewModelList = new List<WeekendViewModel>();
            foreach (var w in weekends)
            {
                weekendViewModelList.Add(new WeekendViewModel(w));
            }
            return weekendViewModelList.AsQueryable();
        }

        internal void Insert(object specialization)
        {
            throw new NotImplementedException();
        }

        public void Insert(WeekendViewModel weekend)
        {
            context.Weekends.Add(weekend.ToEntity());
            context.SaveChanges();
        }

        public void Delete(WeekendViewModel weekend)
        {
            if (weekend == null)
                return;
            context.Weekends.Remove(context.Weekends.Find(weekend.WeekendViewModelID));
            context.SaveChanges();
        }

        public void Update(WeekendViewModel weekend)
        {
            var original = context.Weekends.Find(weekend.ToEntity().WeekendID);
            var entityWeekend = weekend.ToEntity();
            if (original != null)
            {
                original.Date = entityWeekend.Date;
                original.Description = entityWeekend.Description;
            }
            context.SaveChanges();
        }

    }
}