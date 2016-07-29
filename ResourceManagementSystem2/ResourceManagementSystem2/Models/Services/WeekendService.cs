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

        public bool Insert(WeekendViewModel weekend)
        {
            var weekendDates = context.Weekends.Select(d => d.Date);
            foreach(var date in weekendDates)
            {
                if(weekend.Date == date)
                {
                    return false;
                }
            }
            var weekendEntity = context.Weekends.Add(weekend.ToEntity());
            context.SaveChanges();
            weekend.WeekendViewModelID = weekendEntity.WeekendID;
            return true;
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