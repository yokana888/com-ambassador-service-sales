using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentSewingBlockingPlanViewModels
{
    public class GarmentSewingBlockingPlanViewModel : BaseViewModel, IValidatableObject
    {
        public long BookingOrderId { get; set; }
        public string BookingOrderNo { get; set; }
        public DateTimeOffset BookingOrderDate { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public BuyerViewModel Buyer { get; set; }
        public double OrderQuantity { get; set; }
        public string Remark { get; set; }
        public string BookingItems { get; set; }
        public string Status { get; set; }

        public List<GarmentSewingBlockingPlanItemViewModel> Items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(BookingOrderNo))
            {
                yield return new ValidationResult("Kode Booking Md tidak boleh kosong", new List<string> { "BookingOrderNo" });
            }
            if(Items==null || Items.Count == 0)
            {
                yield return new ValidationResult("Detail tidak boleh kosong", new List<string> { "details" });
            }
            else
            {
                int Count = 0;
                string ItemError = "[";
                Dictionary<long,double> weeklyId = new Dictionary<long,double>();

                SalesDbContext dbContext = validationContext == null ? null : (SalesDbContext)validationContext.GetService(typeof(SalesDbContext));
                var wh = dbContext.MaxWHConfirms.OrderByDescending(a => a.CreatedUtc).First();
                if (Id > 0)
                {
                    var oldItems = dbContext.GarmentSewingBlockingPlanItems.AsNoTracking().Where(a => a.BlockingPlanId == Id).ToList();
                    foreach (var i in oldItems)
                    {
                        double oldWH = 0;
                        var week = dbContext.GarmentWeeklyPlanItems.FirstOrDefault(a => a.Id == i.WeeklyPlanItemId);

                        if (i.Id > 0)
                        {
                            var bp = dbContext.GarmentSewingBlockingPlanItems.AsNoTracking().FirstOrDefault(a => a.Id == i.Id && a.WeeklyPlanItemId == i.WeeklyPlanItemId && a.IsConfirm);
                            oldWH = bp == null ? 0 : Math.Round((bp.EHBooking / (week.Operator * week.Efficiency)), 2);

                            if (weeklyId.ContainsKey(i.WeeklyPlanItemId))
                            {
                                weeklyId[i.WeeklyPlanItemId] -= oldWH;
                            }
                            else
                            {
                                weeklyId.Add(i.WeeklyPlanItemId, (Math.Round(week.WHConfirm, 2) - oldWH));
                            }
                        }
                    }
                }
                

                foreach (GarmentSewingBlockingPlanItemViewModel item in Items)
                {
                    ItemError += "{";
                    if (item.Comodity==null)
                    {
                        Count++;
                        ItemError += " Comodity: 'Komoditas harus diisi' , ";
                    }

                    if (item.OrderQuantity <= 0)
                    {
                        Count++;
                        ItemError += " OrderQuantity: 'Jumlah tidak boleh kurang dari 0' , ";
                    }

                    if (item.DeliveryDate == DateTimeOffset.MinValue || item.DeliveryDate == null)
                    {
                        Count++;
                        ItemError += " DeliveryDate: 'Tanggal Pengiriman Harus Diisi' , ";
                    }
                    //else if(item.DeliveryDate.AddHours(7).Date < DateTimeOffset.UtcNow.Date)
                    //{
                    //    Count++;
                    //    ItemError += " DeliveryDate: 'Tanggal Pengiriman Tidak Boleh Kurang dari Hari Ini' , ";
                    //}
                    else if (item.DeliveryDate > this.DeliveryDate)
                    {
                        Count++;
                        ItemError += " DeliveryDate: 'Tanggal Pengiriman Tidak Boleh Lebih dari Tanggal Pengiriman Booking' , ";
                    }
                    else if (item.DeliveryDate <= this.BookingOrderDate)
                    {
                        Count++;
                        ItemError += " DeliveryDate: 'Tanggal Pengiriman Harus Lebih dari Tanggal Booking' , ";
                    }


                    if(item.IsConfirm)
                    {
                        var week = dbContext.GarmentWeeklyPlanItems.FirstOrDefault(a => a.Id == item.WeeklyPlanItemId);

                        if (weeklyId.ContainsKey(item.WeeklyPlanItemId))
                        {
                            weeklyId[item.WeeklyPlanItemId] += Math.Round(item.whConfirm,2);
                        }
                        else
                        {
                            weeklyId.Add(item.WeeklyPlanItemId, (Math.Round(week.WHConfirm,2) + Math.Round(item.whConfirm, 2)));
                        }

                        double maxValue = 0;
                        if (item.Unit != null)
                        {
                            maxValue = item.Unit.Code == "SK" ? wh.SKMaxValue : wh.UnitMaxValue;
                        }
                        else
                        {
                            Count++;
                            ItemError += " unit: 'Unit harus diisi' , ";
                        }

                        if (Math.Round(weeklyId[item.WeeklyPlanItemId],2) > maxValue)
                        {
                            Count++;
                            ItemError += $" whConfirm: 'Tidak bisa simpan blocking plan sewing. WH Confirm > {maxValue}' , ";
                        }
                    }
                    
                    
                    ItemError += "}, ";
                }

                ItemError += "]";

                if (Count > 0)
                {
                    yield return new ValidationResult(ItemError, new List<string> { "Items" });
                }
            }
        }
    }
}
