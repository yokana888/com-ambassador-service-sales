using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentMasterPlan.MonitoringInterfaces;
using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.MonitoringLogics;
using Com.Ambassador.Service.Sales.Lib.Models.GarmentSewingBlockingPlanModel;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentMasterPlan.MonitoringViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using Newtonsoft.Json;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.MonitoringFacades
{
    public class SewingBlockingPlanReportFacade : ISewingBlockingPlanReportFacade
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<GarmentSewingBlockingPlan> DbSet;
        private IdentityService IdentityService;
        private SewingBlockingPlanReportLogic SewingBlockingPlanReportLogic;

        public SewingBlockingPlanReportFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<GarmentSewingBlockingPlan>();
            IdentityService = serviceProvider.GetService<IdentityService>();
            SewingBlockingPlanReportLogic = serviceProvider.GetService<SewingBlockingPlanReportLogic>();
        }

        public Tuple<MemoryStream, string> GenerateExcel(string filter = "{}")
        {
            var Query = SewingBlockingPlanReportLogic.GetQuery(filter);
            var data = Query.ToList();

            DataTable result = new DataTable();

            List<object> rowValuesForEmptyData = new List<object>();
            Dictionary<string, List<UnitDataTable>> rowData = new Dictionary<string, List<UnitDataTable>>();
            Dictionary<string, int> column = new Dictionary<string, int>();

            result.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "BUYER-KOMODITI", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "SMV Sewing", DataType = typeof(string) });
            rowValuesForEmptyData.Add("");
            
            foreach (var weeks in data.First().items)
            {
                string week = string.Concat("W", weeks.weekNumber, "  ", weeks.weekEndDate.ToString("dd MMM"));
                result.Columns.Add(new DataColumn() { ColumnName = week, DataType = typeof(string) });
                if (!column.ContainsKey(week))
                {
                    column.Add(week, weeks.weekNumber);
                }
            }

            bool flagSK = false;
            foreach (var dta in data)
            {
                if (dta.unit == "SK")
                {
                    flagSK = true;
                }
            }
            Dictionary<string, int> count = new Dictionary<string, int>();
            Dictionary<string, double> smv = new Dictionary<string, double>();
            Dictionary<string, double> total = new Dictionary<string, double>();
            Dictionary<string, string> totalConfirmed = new Dictionary<string, string>();
            Dictionary<string, string> units = new Dictionary<string, string>();
            Dictionary<string, double> totalPerUnit = new Dictionary<string, double>();
            Dictionary<string, unitTable> unitBuyers = new Dictionary<string, unitTable>();
            Dictionary<string, double> totalWHConfirm = new Dictionary<string, double>();
            Dictionary<int, double> Grandtotal = new Dictionary<int, double>();
            Dictionary<long, long> bookId = new Dictionary<long, long>();
            Dictionary<string, double> totalConfirmPerUnit = new Dictionary<string, double>();
            Dictionary<string, double> ehConfirm = new Dictionary<string, double>();

            Dictionary<int, double> GrandtotalConf = new Dictionary<int, double>();
            Dictionary<int, double> GrandtotalEff = new Dictionary<int, double>();
            Dictionary<int, double> GrandtotalOp = new Dictionary<int, double>();
            Dictionary<int, double> GrandtotalWH = new Dictionary<int, double>();
            Dictionary<int, double> GrandtotalAH = new Dictionary<int, double>();
            Dictionary<int, double> GrandtotalEH = new Dictionary<int, double>();
            Dictionary<int, double> GrandtotalusedEH = new Dictionary<int, double>();
            Dictionary<int, double> GrandtotalusedEHConfirm = new Dictionary<int, double>();
            Dictionary<int, double> GrandtotalremEH = new Dictionary<int, double>();
            Dictionary<int, double> GrandtotalWHBook = new Dictionary<int, double>();
            Dictionary<int, double> GrandtotalWHConf = new Dictionary<int, double>();

            Dictionary<int, double> GrandtotalUnit = new Dictionary<int, double>();
            Dictionary<int, double> GrandtotalConfUnit = new Dictionary<int, double>();
            Dictionary<int, double> GrandtotalEffUnit = new Dictionary<int, double>();
            Dictionary<int, double> GrandtotalOpUnit = new Dictionary<int, double>();
            Dictionary<int, double> GrandtotalWHUnit = new Dictionary<int, double>();
            Dictionary<int, double> GrandtotalAHUnit = new Dictionary<int, double>();
            Dictionary<int, double> GrandtotalEHUnit = new Dictionary<int, double>();
            Dictionary<int, double> GrandtotalusedEHUnit = new Dictionary<int, double>();
            Dictionary<int, double> GrandtotalusedEHConfirmUnit = new Dictionary<int, double>();
            Dictionary<int, double> GrandtotalremEHUnit = new Dictionary<int, double>();
            Dictionary<int, double> GrandtotalWHBookUnit = new Dictionary<int, double>();
            Dictionary<int, double> GrandtotalWHConfUnit = new Dictionary<int, double>();
            var BGC = "";
            foreach (var dt in data)
            {
                string uw = string.Concat(dt.unit, "-", dt.weekSewingBlocking);
                string uwb = string.Concat(dt.unit, "-", dt.weekSewingBlocking, "-", dt.buyer);
                string ub = string.Concat(dt.unit, "-", dt.buyer);
                byte w = dt.weekSewingBlocking;

                #region FooterPerUnit
                if (!totalPerUnit.ContainsKey(uw))
                {
                    totalPerUnit.Add(uw, dt.bookingQty);
                }
                else
                {
                    totalPerUnit[uw] += dt.bookingQty;
                }

                if (!totalConfirmPerUnit.ContainsKey(uw))
                {
                    if(dt.isConfirmed)
                        totalConfirmPerUnit.Add(uw, dt.bookingQty);
                }
                else
                {
                    if (dt.isConfirmed)
                        totalConfirmPerUnit[uw] += dt.bookingQty;
                }

                if (!ehConfirm.ContainsKey(uw))
                {
                    if (dt.isConfirmed)
                        ehConfirm.Add(uw, dt.UsedEH);
                }
                else
                {
                    if (dt.isConfirmed)
                        ehConfirm[uw] += dt.UsedEH;
                }

                if (!totalWHConfirm.ContainsKey(uw))
                {
                    if(dt.isConfirmed)
                        totalWHConfirm.Add(uw, dt.UsedEH);
                }
                else
                {
                    if (dt.isConfirmed)
                        totalWHConfirm[uw] += dt.UsedEH;
                }

                if (!count.ContainsKey(ub))
                {
                    count.Add(ub, 1);
                }
                else
                {
                    count[ub] += 1;
                }

                if (!smv.ContainsKey(ub))
                {
                    smv.Add(ub, dt.SMVSewing);
                }
                else
                {
                    smv[ub] += dt.SMVSewing;
                }

                if (!total.ContainsKey(uwb))
                {
                    total.Add(uwb, dt.bookingQty);
                }
                else
                {
                    total[uwb] += dt.bookingQty;
                }

                if (!totalConfirmed.ContainsKey(uwb))
                {
                    BGC = dt.bookingOrderItems.Count == 0 ? "yellow" :
                        dt.bookingOrderItems.Sum(a => a.ConfirmQuantity) < dt.bookingOrderQty ? "orange" :
                        "transparent";
                    totalConfirmed.Add(uwb, BGC);
                }
                else
                {
                    if(totalConfirmed[uwb]== "transparent")
                    {
                        BGC = dt.bookingOrderItems.Count == 0 ? "yellow" :
                        dt.bookingOrderItems.Sum(a => a.ConfirmQuantity) < dt.bookingOrderQty ? "orange" :
                        "transparent";
                        totalConfirmed[uwb] = BGC;
                    }
                    else if(totalConfirmed[uwb] == "orange")
                    {
                        if(dt.bookingOrderItems.Count == 0)
                        {
                            BGC = "yellow";
                            totalConfirmed[uwb] = BGC;
                        }
                        
                    }
                }


                if (!units.ContainsKey(dt.unit))
                {
                    units.Add(dt.unit, dt.unit);

                    foreach(var item in dt.items)
                    {
                        if (!GrandtotalEff.ContainsKey(item.weekNumber))
                        {
                            GrandtotalEff.Add(item.weekNumber, item.efficiency);
                        }
                        else
                        {
                            GrandtotalEff[item.weekNumber] += item.efficiency;
                        }

                        if (!GrandtotalOp.ContainsKey(item.weekNumber))
                        {
                            GrandtotalOp.Add(item.weekNumber, item.head);
                        }
                        else
                        {
                            GrandtotalOp[item.weekNumber] += item.head;
                        }

                        if (!GrandtotalWH.ContainsKey(item.weekNumber))
                        {
                            GrandtotalWH.Add(item.weekNumber, item.workingHours);
                        }
                        else
                        {
                            GrandtotalWH[item.weekNumber] += item.workingHours;
                        }

                        if (!GrandtotalAH.ContainsKey(item.weekNumber))
                        {
                            GrandtotalAH.Add(item.weekNumber, item.AHTotal);
                        }
                        else
                        {
                            GrandtotalAH[item.weekNumber] += item.AHTotal;
                        }

                        if (!GrandtotalEH.ContainsKey(item.weekNumber))
                        {
                            GrandtotalEH.Add(item.weekNumber, item.EHTotal);
                        }
                        else
                        {
                            GrandtotalEH[item.weekNumber] += item.EHTotal;
                        }

                        if (!GrandtotalusedEH.ContainsKey(item.weekNumber))
                        {
                            GrandtotalusedEH.Add(item.weekNumber, item.usedTotal);
                        }
                        else
                        {
                            GrandtotalusedEH[item.weekNumber] += item.usedTotal;
                        }

                        if (!GrandtotalremEH.ContainsKey(item.weekNumber))
                        {
                            GrandtotalremEH.Add(item.weekNumber, item.remainingEH);
                        }
                        else
                        {
                            GrandtotalremEH[item.weekNumber] += item.remainingEH;
                        }

                        if (dt.unit != "SK")
                        {
                            if (!GrandtotalEffUnit.ContainsKey(item.weekNumber))
                            {
                                GrandtotalEffUnit.Add(item.weekNumber, item.efficiency);
                            }
                            else
                            {
                                GrandtotalEffUnit[item.weekNumber] += item.efficiency;
                            }

                            if (!GrandtotalOpUnit.ContainsKey(item.weekNumber))
                            {
                                GrandtotalOpUnit.Add(item.weekNumber, item.head);
                            }
                            else
                            {
                                GrandtotalOpUnit[item.weekNumber] += item.head;
                            }

                            if (!GrandtotalWHUnit.ContainsKey(item.weekNumber))
                            {
                                GrandtotalWHUnit.Add(item.weekNumber, item.workingHours);
                            }
                            else
                            {
                                GrandtotalWHUnit[item.weekNumber] += item.workingHours;
                            }

                            if (!GrandtotalAHUnit.ContainsKey(item.weekNumber))
                            {
                                GrandtotalAHUnit.Add(item.weekNumber, item.AHTotal);
                            }
                            else
                            {
                                GrandtotalAHUnit[item.weekNumber] += item.AHTotal;
                            }

                            if (!GrandtotalEHUnit.ContainsKey(item.weekNumber))
                            {
                                GrandtotalEHUnit.Add(item.weekNumber, item.EHTotal);
                            }
                            else
                            {
                                GrandtotalEHUnit[item.weekNumber] += item.EHTotal;
                            }

                            if (!GrandtotalusedEHUnit.ContainsKey(item.weekNumber))
                            {
                                GrandtotalusedEHUnit.Add(item.weekNumber, item.usedTotal);
                            }
                            else
                            {
                                GrandtotalusedEHUnit[item.weekNumber] += item.usedTotal;
                            }

                            if (!GrandtotalremEHUnit.ContainsKey(item.weekNumber))
                            {
                                GrandtotalremEHUnit.Add(item.weekNumber, item.remainingEH);
                            }
                            else
                            {
                                GrandtotalremEHUnit[item.weekNumber] += item.remainingEH;
                            }
                        }
                    }
                }

                unitTable ut = new unitTable
                {
                    unit=dt.unit,
                    buyer=dt.buyer,
                    week=dt.weekSewingBlocking
                };

                if (!unitBuyers.ContainsKey(ub))
                {
                    unitBuyers.Add(ub, ut);
                }
                #endregion

                #region GRANDTOTAL
                if (!Grandtotal.ContainsKey(w))
                {
                    Grandtotal.Add(w, dt.bookingQty);
                }
                else
                {
                    Grandtotal[w] += dt.bookingQty;
                }

                if (!GrandtotalConf.ContainsKey(w))
                {
                    if (dt.isConfirmed)
                        GrandtotalConf.Add(w, dt.bookingQty);
                }
                else
                {
                    if (dt.isConfirmed)
                        GrandtotalConf[w] += dt.bookingQty;
                }

                if (!GrandtotalusedEHConfirm.ContainsKey(w))
                {
                    if (dt.isConfirmed)
                        GrandtotalusedEHConfirm.Add(w, dt.UsedEH);
                }
                else
                {
                    if (dt.isConfirmed)
                        GrandtotalusedEHConfirm[w] += dt.UsedEH;
                }

                #endregion

                #region GRANDTOTALUNIT
                if (dt.unit != "SK")
                {
                    if (!GrandtotalUnit.ContainsKey(w))
                    {
                        GrandtotalUnit.Add(w, dt.bookingQty);
                    }
                    else
                    {
                        GrandtotalUnit[w] += dt.bookingQty;
                    }

                    if (!GrandtotalConfUnit.ContainsKey(w))
                    {
                        if (dt.isConfirmed)
                            GrandtotalConfUnit.Add(w, dt.bookingQty);
                    }
                    else
                    {
                        if (dt.isConfirmed)
                            GrandtotalConfUnit[w] += dt.bookingQty;
                    }

                    if (!GrandtotalusedEHConfirmUnit.ContainsKey(w))
                    {
                        if (dt.isConfirmed)
                            GrandtotalusedEHConfirmUnit.Add(w, dt.UsedEH);
                    }
                    else
                    {
                        if (dt.isConfirmed)
                            GrandtotalusedEHConfirmUnit[w] += dt.UsedEH;
                    }
                }
                
                #endregion
            }

            Dictionary<string, string> color = new Dictionary<string, string>();

            foreach (var un in units)
            {
                foreach (var ub in unitBuyers)
                {
                    if (un.Value == ub.Value.unit)
                    {
                        foreach (var w in column)
                        {
                            string unitBuyerWeek = string.Concat(ub.Value.unit, "-", w.Value, "-", ub.Value.buyer);
                            string unitBuyer = string.Concat(ub.Value.unit, "-", ub.Value.buyer);
                            string unitWeek = string.Concat(ub.Value.unit, "-", w.Value);
                            UnitDataTable unitDataTable = new UnitDataTable
                            {
                                Unit = ub.Value.unit,
                                buyer = ub.Value.buyer,
                                qty = total.ContainsKey(unitBuyerWeek) ? total[unitBuyerWeek].ToString() : "-",
                                week = w.Value
                            };

                            if (rowData.ContainsKey(unitBuyer))
                            {
                                var rowValue = rowData[unitBuyer];
                                var unit = rowValue.FirstOrDefault(a => a.week == w.Value && a.buyer == ub.Value.buyer && a.Unit == ub.Value.unit);
                                if (unit == null)
                                {
                                    rowValue.Add(unitDataTable);
                                }
                            }
                            else
                            {
                                rowData.Add(unitBuyer, new List<UnitDataTable> { unitDataTable });
                            }

                        }
                    }
                }
                #region Footer per Unit
                foreach (var weekNum in column)
                {
                    string totalKey = un.Value + "-" + "Total Booking";
                    string totalConfKey = un.Value + "-" + "Total Confirm";
                    string totalConfPrsKey = un.Value + "-" + "Persentase Confirm";
                    string effKey = un.Value + "-" + "Efisiensi";
                    string opKey = un.Value + "-" + "Total Operator Sewing";
                    string whKey = un.Value + "-" + "Working Hours";
                    string AHKey = un.Value + "-" + "Total AH";
                    string EHKey = un.Value + "-" + "Total EH";
                    string useEHKey = un.Value + "-" + "Used EH Booking";
                    string useEHConfKey = un.Value + "-" + "Used EH Confirm";
                    string remEHKey = un.Value + "-" + "Remaining EH";
                    string whBookKey = un.Value + "-" + "WH Booking";
                    string whConfirmKey = un.Value + "-" + "WH Confirm";

                    string unWeek = string.Concat(un.Value, "-", weekNum.Value);

                    

                    UnitDataTable unitDataTable1 = new UnitDataTable
                    {
                        Unit = un.Value,
                        buyer = "Total Booking",
                        qty = totalPerUnit.ContainsKey(unWeek) ? totalPerUnit[unWeek].ToString() : "-",
                        week = weekNum.Value
                    };
                    if (!rowData.ContainsKey(totalKey))
                    {
                        
                        rowData.Add(totalKey, new List<UnitDataTable> { unitDataTable1 });
                    }
                    else
                    {
                        rowData[totalKey].Add(unitDataTable1);
                    }

                    UnitDataTable unitDataTableConfirm = new UnitDataTable
                    {
                        Unit = un.Value,
                        buyer = "Total Confirm",
                        qty = totalConfirmPerUnit.ContainsKey(unWeek) ? totalConfirmPerUnit[unWeek].ToString() : "-",
                        week = weekNum.Value
                    };
                    if (!rowData.ContainsKey(totalConfKey))
                    {

                        rowData.Add(totalConfKey, new List<UnitDataTable> { unitDataTableConfirm });
                    }
                    else
                    {
                        rowData[totalConfKey].Add(unitDataTableConfirm);
                    }

                    double percentConfirm = 0;
                    if (!totalPerUnit.ContainsKey(unWeek))
                    {
                        if (!totalConfirmPerUnit.ContainsKey(unWeek))
                        {
                            percentConfirm = 0;
                        }
                        else
                        {
                            percentConfirm = 100;
                        }
                    }
                    else if(!totalConfirmPerUnit.ContainsKey(unWeek))
                    {
                        if (!totalPerUnit.ContainsKey(unWeek))
                        {
                            percentConfirm = 0;
                        }
                    }
                    else
                    {
                        percentConfirm = Math.Round(((totalConfirmPerUnit[unWeek] / totalPerUnit[unWeek]) * 100),2);
                    }

                    UnitDataTable unitDataTablePrsConfirm = new UnitDataTable
                    {
                        Unit = un.Value,
                        buyer = "Persentase Confirm",
                        qty = string.Concat(percentConfirm, "%"),
                        week = weekNum.Value
                    };
                    if (!rowData.ContainsKey(totalConfPrsKey))
                    {

                        rowData.Add(totalConfPrsKey, new List<UnitDataTable> { unitDataTablePrsConfirm });
                    }
                    else
                    {
                        rowData[totalConfPrsKey].Add(unitDataTablePrsConfirm);
                    }

                    var dataStatic = data.FirstOrDefault(a => a.unit == un.Value).items;
                    var dtItem = dataStatic.FirstOrDefault(a => a.weekNumber == weekNum.Value);
                    double eff = dtItem == null ? 0 : dtItem.efficiency;
                    double op = dtItem == null ? 0 : dtItem.head;
                    double EHTot = dtItem == null ? 0 : dtItem.EHTotal;
                    double AHTot = dtItem == null ? 0 : dtItem.AHTotal;
                    double usedEH = dtItem == null ? 0 : dtItem.usedTotal;
                    double remainingEH = dtItem == null ? 0 : dtItem.remainingEH;
                    double workingHours = dtItem == null ? 0 : dtItem.workingHours;
                    double whBooking = dtItem == null ? 0 : dtItem.WHBooking;

                    UnitDataTable unitDataTableEfficiency = new UnitDataTable
                    {
                        Unit = un.Value,
                        buyer = "Efisiensi",
                        qty = eff.ToString() + "%",
                        week = weekNum.Value
                    };

                    if (!rowData.ContainsKey(effKey))
                    {
                        rowData.Add(effKey, new List<UnitDataTable> { unitDataTableEfficiency });
                    }
                    else
                    {
                        rowData[effKey].Add(unitDataTableEfficiency);
                    }

                    UnitDataTable unitDataTableOP = new UnitDataTable
                    {
                        Unit = un.Value,
                        buyer = "Operator",
                        qty = op.ToString(),
                        week = weekNum.Value
                    };

                    if (!rowData.ContainsKey(opKey))
                    {
                        rowData.Add(opKey, new List<UnitDataTable> { unitDataTableOP });
                    }
                    else
                    {
                        rowData[opKey].Add(unitDataTableOP);
                    }

                    UnitDataTable unitDataTableWH = new UnitDataTable
                    {
                        Unit = un.Value,
                        buyer = "WorkingHours",
                        qty = workingHours.ToString(),
                        week = weekNum.Value
                    };

                    if (!rowData.ContainsKey(whKey))
                    {
                        rowData.Add(whKey, new List<UnitDataTable> { unitDataTableWH });
                    }
                    else
                    {
                        rowData[whKey].Add(unitDataTableWH);
                    }

                    UnitDataTable unitDataTableAH = new UnitDataTable
                    {
                        Unit = un.Value,
                        buyer = "Total AH",
                        qty = AHTot.ToString(),
                        week = weekNum.Value
                    };

                    if (!rowData.ContainsKey(AHKey))
                    {
                        rowData.Add(AHKey, new List<UnitDataTable> { unitDataTableAH });
                    }
                    else
                    {
                        rowData[AHKey].Add(unitDataTableAH);
                    }

                    UnitDataTable unitDataTableEH = new UnitDataTable
                    {
                        Unit = un.Value,
                        buyer = "Total EH",
                        qty = EHTot.ToString(),
                        week = weekNum.Value
                    };

                    if (!rowData.ContainsKey(EHKey))
                    {
                        rowData.Add(EHKey, new List<UnitDataTable> { unitDataTableEH });
                    }
                    else
                    {
                        rowData[EHKey].Add(unitDataTableEH);
                    }

                    UnitDataTable unitDataTableUseEH = new UnitDataTable
                    {
                        Unit = un.Value,
                        buyer = "Used EH Booking",
                        qty = usedEH.ToString(),
                        week = weekNum.Value
                    };

                    if (!rowData.ContainsKey(useEHKey))
                    {
                        rowData.Add(useEHKey, new List<UnitDataTable> { unitDataTableUseEH });
                    }
                    else
                    {
                        rowData[useEHKey].Add(unitDataTableUseEH);
                    }

                    UnitDataTable unitDataTableEHConf = new UnitDataTable
                    {
                        Unit = un.Value,
                        buyer = "Used EH Confirm",
                        qty = ehConfirm.ContainsKey(unWeek) ? ehConfirm[unWeek].ToString() : "0",
                        week = weekNum.Value
                    };
                    if (!rowData.ContainsKey(useEHConfKey))
                    {

                        rowData.Add(useEHConfKey, new List<UnitDataTable> { unitDataTableEHConf });
                    }
                    else
                    {
                        rowData[useEHConfKey].Add(unitDataTableEHConf);
                    }

                    UnitDataTable unitDataTableRemEH = new UnitDataTable
                    {
                        Unit = un.Value,
                        buyer = "Remaining EH",
                        qty = remainingEH.ToString(),
                        week = weekNum.Value
                    };

                    if (!rowData.ContainsKey(remEHKey))
                    {
                        rowData.Add(remEHKey, new List<UnitDataTable> { unitDataTableRemEH });
                    }
                    else
                    {
                        rowData[remEHKey].Add(unitDataTableRemEH);
                    }

                    UnitDataTable unitDataTableWHBook = new UnitDataTable
                    {
                        Unit = un.Value,
                        buyer = "WH Booking",
                        qty = Math.Round(whBooking,2).ToString(),
                        week = weekNum.Value
                    };

                    if (!rowData.ContainsKey(whBookKey))
                    {
                        rowData.Add(whBookKey, new List<UnitDataTable> { unitDataTableWHBook });
                    }
                    else
                    {
                        rowData[whBookKey].Add(unitDataTableWHBook);
                    }

                    if (!GrandtotalWHBook.ContainsKey(weekNum.Value))
                    {
                        GrandtotalWHBook.Add(weekNum.Value, whBooking);
                    }
                    else
                    {
                        GrandtotalWHBook[weekNum.Value] += whBooking;
                    }

                    

                    double ehConf = totalWHConfirm.ContainsKey(unWeek) ? totalWHConfirm[unWeek] : 0;
                    double whConf = ehConf != 0 ? ehConf / (op * eff / 100) : 0;



                    UnitDataTable unitDataTableWHConfirm = new UnitDataTable
                    {
                        Unit = un.Value,
                        buyer = "WH Confirm",
                        qty = Math.Round(whConf, 2).ToString(),
                        week = weekNum.Value
                    };

                    if (!rowData.ContainsKey(whConfirmKey))
                    {
                        rowData.Add(whConfirmKey, new List<UnitDataTable> { unitDataTableWHConfirm });
                    }
                    else
                    {
                        rowData[whConfirmKey].Add(unitDataTableWHConfirm);
                    }

                    if (!GrandtotalWHConf.ContainsKey(weekNum.Value))
                    {
                        GrandtotalWHConf.Add(weekNum.Value, whConf);
                    }
                    else
                    {
                        GrandtotalWHConf[weekNum.Value] += whConf;
                    }

                    if (un.Value != "SK")
                    {

                        double ehConfUnit = totalWHConfirm.ContainsKey(unWeek) ? totalWHConfirm[unWeek] : 0;
                        double whConfUnit = ehConfUnit != 0 ? ehConf / (op * eff / 100) : 0;

                        if (!GrandtotalWHBookUnit.ContainsKey(weekNum.Value))
                        {
                            GrandtotalWHBookUnit.Add(weekNum.Value, whBooking);
                        }
                        else
                        {
                            GrandtotalWHBookUnit[weekNum.Value] += whBooking;
                        }

                        if (!GrandtotalWHConfUnit.ContainsKey(weekNum.Value))
                        {
                            GrandtotalWHConfUnit.Add(weekNum.Value, whConfUnit);
                        }
                        else
                        {
                            GrandtotalWHConfUnit[weekNum.Value] += whConfUnit;
                        }
                    }
                }
                #endregion
            }

            int unitCount = units.Count;
            int unitCountSK = flagSK ? unitCount - 1 : unitCount;

            #region GRANDTOTAL ROWS

            string GrandtotalKey = "GRAND TOTAL" + "-" + "Total Booking";
            string GrandtotalBookingConfKey = "GRAND TOTAL" + "-" + "Total Confirm";
            string GrandtotalBookingConfPrsKey = "GRAND TOTAL" + "-" + "Persentase Confirm";
            string GrandtotaleffKey = "GRAND TOTAL" + "-" + "Efisiensi";
            string GrandtotalopKey = "GRAND TOTAL" + "-" + "Total Operator Sewing";
            string GrandtotalwhKey = "GRAND TOTAL" + "-" + "Working Hours";
            string GrandtotalAHKey = "GRAND TOTAL" + "-" + "Total AH";
            string GrandtotalEHKey = "GRAND TOTAL" + "-" + "Total EH";
            string GrandtotaluseEHKey = "GRAND TOTAL" + "-" + "Used EH Booking";
            string GrandtotaluseEHConfKey = "GRAND TOTAL" + "-" + "Used EH Confirm";
            string GrandtotalremEHKey = "GRAND TOTAL" + "-" + "Remaining EH";
            string GrandtotalwhBookKey = "GRAND TOTAL" + "-" + "WH Booking";
            string GrandtotalwhConfirmKey = "GRAND TOTAL" + "-" + "WH Confirm";

            string GrandtotalKeyUnit = "GRAND TOTAL UNIT" + "-" + "Total Booking";
            string GrandtotalBookingConfKeyUnit = "GRAND TOTAL UNIT" + "-" + "Total Confirm";
            string GrandtotalBookingConfPrsKeyUnit = "GRAND TOTAL UNIT" + "-" + "Persentase Confirm";
            string GrandtotaleffKeyUnit = "GRAND TOTAL UNIT" + "-" + "Efisiensi";
            string GrandtotalopKeyUnit = "GRAND TOTAL UNIT" + "-" + "Total Operator Sewing";
            string GrandtotalwhKeyUnit = "GRAND TOTAL UNIT" + "-" + "Working Hours";
            string GrandtotalAHKeyUnit = "GRAND TOTAL UNIT" + "-" + "Total AH";
            string GrandtotalEHKeyUnit = "GRAND TOTAL UNIT" + "-" + "Total EH";
            string GrandtotaluseEHKeyUnit = "GRAND TOTAL UNIT" + "-" + "Used EH Booking";
            string GrandtotaluseEHConfKeyUnit = "GRAND TOTAL UNIT" + "-" + "Used EH Confirm";
            string GrandtotalremEHKeyUnit = "GRAND TOTAL UNIT" + "-" + "Remaining EH";
            string GrandtotalwhBookKeyUnit = "GRAND TOTAL UNIT" + "-" + "WH Booking";
            string GrandtotalwhConfirmKeyUnit = "GRAND TOTAL UNIT" + "-" + "WH Confirm";

            foreach (var wNum in column)
            {
                int weekKey = wNum.Value;

                UnitDataTable unitDataTableTotalUnit = new UnitDataTable
                {
                    Unit = "GRAND TOTAL UNIT",
                    buyer = "Total Booking",
                    qty = GrandtotalUnit.ContainsKey(weekKey) ? GrandtotalUnit[weekKey].ToString() : "-",
                    week = weekKey
                };
                if (!rowData.ContainsKey(GrandtotalKeyUnit))
                {

                    rowData.Add(GrandtotalKeyUnit, new List<UnitDataTable> { unitDataTableTotalUnit });
                }
                else
                {
                    rowData[GrandtotalKeyUnit].Add(unitDataTableTotalUnit);
                }

                UnitDataTable unitDataTableTotalConfUnit = new UnitDataTable
                {
                    Unit = "GRAND TOTAL UNIT",
                    buyer = "Total Confirm",
                    qty = GrandtotalConfUnit.ContainsKey(weekKey) ? GrandtotalConfUnit[weekKey].ToString() : "-",
                    week = weekKey
                };
                if (!rowData.ContainsKey(GrandtotalBookingConfKeyUnit))
                {

                    rowData.Add(GrandtotalBookingConfKeyUnit, new List<UnitDataTable> { unitDataTableTotalConfUnit });
                }
                else
                {
                    rowData[GrandtotalBookingConfKeyUnit].Add(unitDataTableTotalConfUnit);
                }

                double percentConfirmTotalUnit = 0;
                if (!GrandtotalUnit.ContainsKey(weekKey))
                {
                    if (!GrandtotalConfUnit.ContainsKey(weekKey))
                    {
                        percentConfirmTotalUnit = 0;
                    }
                    else
                    {
                        percentConfirmTotalUnit = 100;
                    }
                }
                else if (!GrandtotalConfUnit.ContainsKey(weekKey))
                {
                    if (!GrandtotalUnit.ContainsKey(weekKey))
                    {
                        percentConfirmTotalUnit = 0;
                    }
                }
                else
                {
                    percentConfirmTotalUnit = Math.Round(((GrandtotalConfUnit[weekKey] / GrandtotalUnit[weekKey]) * 100), 2);
                }

                UnitDataTable unitDataTableTotalConfPrsUnit = new UnitDataTable
                {
                    Unit = "GRAND TOTAL UNIT",
                    buyer = "Persentase Confirm",
                    qty = string.Concat(percentConfirmTotalUnit, "%"),
                    week = weekKey
                };
                if (!rowData.ContainsKey(GrandtotalBookingConfPrsKeyUnit))
                {

                    rowData.Add(GrandtotalBookingConfPrsKeyUnit, new List<UnitDataTable> { unitDataTableTotalConfPrsUnit });
                }
                else
                {
                    rowData[GrandtotalBookingConfPrsKeyUnit].Add(unitDataTableTotalConfPrsUnit);
                }

                decimal efisiensiUnit = (decimal)(GrandtotalEffUnit[weekKey] / unitCountSK);

                UnitDataTable unitDataTableTotalEffUnit = new UnitDataTable
                {
                    Unit = "GRAND TOTAL UNIT",
                    buyer = "Efisiensi",
                    qty = string.Format("{0:N2}%", efisiensiUnit),
                    week = weekKey
                };
                if (!rowData.ContainsKey(GrandtotaleffKeyUnit))
                {

                    rowData.Add(GrandtotaleffKeyUnit, new List<UnitDataTable> { unitDataTableTotalEffUnit });
                }
                else
                {
                    rowData[GrandtotaleffKeyUnit].Add(unitDataTableTotalEffUnit);
                }

                UnitDataTable unitDataTableTotalOPUnit = new UnitDataTable
                {
                    Unit = "GRAND TOTAL UNIT",
                    buyer = "Operator",
                    qty = GrandtotalOpUnit[weekKey].ToString(),
                    week = weekKey
                };
                if (!rowData.ContainsKey(GrandtotalopKeyUnit))
                {

                    rowData.Add(GrandtotalopKeyUnit, new List<UnitDataTable> { unitDataTableTotalOPUnit });
                }
                else
                {
                    rowData[GrandtotalopKeyUnit].Add(unitDataTableTotalOPUnit);
                }

                UnitDataTable unitDataTableTotalWHUnit = new UnitDataTable
                {
                    Unit = "GRAND TOTAL UNIT",
                    buyer = "Working Hours",
                    qty = (Math.Round(GrandtotalWHUnit[weekKey] / unitCountSK, 2)).ToString(),
                    week = weekKey
                };
                if (!rowData.ContainsKey(GrandtotalwhKeyUnit))
                {

                    rowData.Add(GrandtotalwhKeyUnit, new List<UnitDataTable> { unitDataTableTotalWHUnit });
                }
                else
                {
                    rowData[GrandtotalwhKeyUnit].Add(unitDataTableTotalWHUnit);
                }

                UnitDataTable unitDataTableTotalAHUnit = new UnitDataTable
                {
                    Unit = "GRAND TOTAL UNIT",
                    buyer = "Total AH",
                    qty = (GrandtotalAHUnit[weekKey]).ToString(),
                    week = weekKey
                };
                if (!rowData.ContainsKey(GrandtotalAHKeyUnit))
                {

                    rowData.Add(GrandtotalAHKeyUnit, new List<UnitDataTable> { unitDataTableTotalAHUnit });
                }
                else
                {
                    rowData[GrandtotalAHKeyUnit].Add(unitDataTableTotalAHUnit);
                }

                UnitDataTable unitDataTableTotalEHUnit = new UnitDataTable
                {
                    Unit = "GRAND TOTAL UNIT",
                    buyer = "Total EH",
                    qty = (GrandtotalEHUnit[weekKey]).ToString(),
                    week = weekKey
                };
                if (!rowData.ContainsKey(GrandtotalEHKeyUnit))
                {

                    rowData.Add(GrandtotalEHKeyUnit, new List<UnitDataTable> { unitDataTableTotalEHUnit });
                }
                else
                {
                    rowData[GrandtotalEHKeyUnit].Add(unitDataTableTotalEHUnit);
                }

                UnitDataTable unitDataTableTotalUsedEHUnit = new UnitDataTable
                {
                    Unit = "GRAND TOTAL UNIT",
                    buyer = "Used EH Booking",
                    qty = (GrandtotalusedEHUnit[weekKey]).ToString(),
                    week = weekKey
                };
                if (!rowData.ContainsKey(GrandtotaluseEHKeyUnit))
                {

                    rowData.Add(GrandtotaluseEHKeyUnit, new List<UnitDataTable> { unitDataTableTotalUsedEHUnit });
                }
                else
                {
                    rowData[GrandtotaluseEHKeyUnit].Add(unitDataTableTotalUsedEHUnit);
                }

                UnitDataTable unitDataTableUsedEHConfUnit = new UnitDataTable
                {
                    Unit = "GRAND TOTAL UNIT",
                    buyer = "Used EH Confirm",
                    qty = GrandtotalusedEHConfirmUnit.ContainsKey(weekKey) ? GrandtotalusedEHConfirmUnit[weekKey].ToString() : "0",
                    week = weekKey
                };
                if (!rowData.ContainsKey(GrandtotaluseEHConfKeyUnit))
                {

                    rowData.Add(GrandtotaluseEHConfKeyUnit, new List<UnitDataTable> { unitDataTableUsedEHConfUnit });
                }
                else
                {
                    rowData[GrandtotaluseEHConfKeyUnit].Add(unitDataTableUsedEHConfUnit);
                }

                UnitDataTable unitDataTableTotalremEHUnit = new UnitDataTable
                {
                    Unit = "GRAND TOTAL UNIT",
                    buyer = "Remaining EH",
                    qty = (GrandtotalremEHUnit[weekKey]).ToString(),
                    week = weekKey
                };
                if (!rowData.ContainsKey(GrandtotalremEHKeyUnit))
                {

                    rowData.Add(GrandtotalremEHKeyUnit, new List<UnitDataTable> { unitDataTableTotalremEHUnit });
                }
                else
                {
                    rowData[GrandtotalremEHKeyUnit].Add(unitDataTableTotalremEHUnit);
                }

                var totalWHBookUnit = GrandtotalusedEHUnit.ContainsKey(weekKey) ? GrandtotalusedEHUnit[weekKey] /(GrandtotalOpUnit[weekKey] * Math.Round((GrandtotalEffUnit[weekKey] / unitCountSK), 2) / 100) : 0;

                UnitDataTable unitDataTableTotalWHBookingUnit = new UnitDataTable
                {
                    Unit = "GRAND TOTAL UNIT",
                    buyer = "WH Booking",
                    qty = (Math.Round(totalWHBookUnit, 2)).ToString(),
                    week = weekKey
                };
                if (!rowData.ContainsKey(GrandtotalwhBookKeyUnit))
                {

                    rowData.Add(GrandtotalwhBookKeyUnit, new List<UnitDataTable> { unitDataTableTotalWHBookingUnit });
                }
                else
                {
                    rowData[GrandtotalwhBookKeyUnit].Add(unitDataTableTotalWHBookingUnit);
                }

                var totalWHConfUnit = GrandtotalusedEHConfirmUnit.ContainsKey(weekKey)? GrandtotalusedEHConfirmUnit[weekKey]/ (GrandtotalOpUnit[weekKey] * Math.Round((GrandtotalEffUnit[weekKey] / unitCountSK), 2) / 100):0;

                UnitDataTable unitDataTableTotalWHConfUnit = new UnitDataTable
                {
                    Unit = "GRAND TOTAL UNIT",
                    buyer = "WH Confirm",
                    qty = (Math.Round(totalWHConfUnit, 2)).ToString(),
                    week = weekKey
                };
                if (!rowData.ContainsKey(GrandtotalwhConfirmKeyUnit))
                {

                    rowData.Add(GrandtotalwhConfirmKeyUnit, new List<UnitDataTable> { unitDataTableTotalWHConfUnit });
                }
                else
                {
                    rowData[GrandtotalwhConfirmKeyUnit].Add(unitDataTableTotalWHConfUnit);
                }

                

                if (flagSK)
                {
                    UnitDataTable unitDataTableTotal = new UnitDataTable
                    {
                        Unit = "GRAND TOTAL",
                        buyer = "Total Booking",
                        qty = Grandtotal.ContainsKey(weekKey) ? Grandtotal[weekKey].ToString() : "-",
                        week = weekKey
                    };
                    if (!rowData.ContainsKey(GrandtotalKey))
                    {

                        rowData.Add(GrandtotalKey, new List<UnitDataTable> { unitDataTableTotal });
                    }
                    else
                    {
                        rowData[GrandtotalKey].Add(unitDataTableTotal);
                    }

                    UnitDataTable unitDataTableTotalConf = new UnitDataTable
                    {
                        Unit = "GRAND TOTAL",
                        buyer = "Total Confirm",
                        qty = GrandtotalConf.ContainsKey(weekKey) ? GrandtotalConf[weekKey].ToString() : "-",
                        week = weekKey
                    };
                    if (!rowData.ContainsKey(GrandtotalBookingConfKey))
                    {

                        rowData.Add(GrandtotalBookingConfKey, new List<UnitDataTable> { unitDataTableTotalConf });
                    }
                    else
                    {
                        rowData[GrandtotalBookingConfKey].Add(unitDataTableTotalConf);
                    }

                    double percentConfirmTotal = 0;
                    if (!Grandtotal.ContainsKey(weekKey))
                    {
                        if (!GrandtotalConf.ContainsKey(weekKey))
                        {
                            percentConfirmTotal = 0;
                        }
                        else
                        {
                            percentConfirmTotal = 100;
                        }
                    }
                    else if (!GrandtotalConf.ContainsKey(weekKey))
                    {
                        if (!Grandtotal.ContainsKey(weekKey))
                        {
                            percentConfirmTotal = 0;
                        }
                    }
                    else
                    {
                        percentConfirmTotal = Math.Round(((GrandtotalConf[weekKey] / Grandtotal[weekKey]) * 100), 2);
                    }

                    UnitDataTable unitDataTableTotalConfPrs = new UnitDataTable
                    {
                        Unit = "GRAND TOTAL",
                        buyer = "Persentase Confirm",
                        qty = string.Concat(percentConfirmTotal, "%"),
                        week = weekKey
                    };
                    if (!rowData.ContainsKey(GrandtotalBookingConfPrsKey))
                    {

                        rowData.Add(GrandtotalBookingConfPrsKey, new List<UnitDataTable> { unitDataTableTotalConfPrs });
                    }
                    else
                    {
                        rowData[GrandtotalBookingConfPrsKey].Add(unitDataTableTotalConfPrs);
                    }

                    decimal efisiensi =(decimal) (GrandtotalEff[weekKey] / unitCount);

                    UnitDataTable unitDataTableTotalEff = new UnitDataTable
                    {
                        Unit = "GRAND TOTAL",
                        buyer = "Efisiensi",
                        qty = string.Format("{0:N2}%", efisiensi),
                        week = weekKey
                    };
                    if (!rowData.ContainsKey(GrandtotaleffKey))
                    {

                        rowData.Add(GrandtotaleffKey, new List<UnitDataTable> { unitDataTableTotalEff });
                    }
                    else
                    {
                        rowData[GrandtotaleffKey].Add(unitDataTableTotalEff);
                    }

                    UnitDataTable unitDataTableTotalOP = new UnitDataTable
                    {
                        Unit = "GRAND TOTAL",
                        buyer = "Operator",
                        qty = GrandtotalOp[weekKey].ToString(),
                        week = weekKey
                    };
                    if (!rowData.ContainsKey(GrandtotalopKey))
                    {

                        rowData.Add(GrandtotalopKey, new List<UnitDataTable> { unitDataTableTotalOP });
                    }
                    else
                    {
                        rowData[GrandtotalopKey].Add(unitDataTableTotalOP);
                    }

                    UnitDataTable unitDataTableTotalWH = new UnitDataTable
                    {
                        Unit = "GRAND TOTAL",
                        buyer = "Working Hours",
                        qty = (Math.Round(GrandtotalWH[weekKey] / unitCount, 2)).ToString(),
                        week = weekKey
                    };
                    if (!rowData.ContainsKey(GrandtotalwhKey))
                    {

                        rowData.Add(GrandtotalwhKey, new List<UnitDataTable> { unitDataTableTotalWH });
                    }
                    else
                    {
                        rowData[GrandtotalwhKey].Add(unitDataTableTotalWH);
                    }

                    UnitDataTable unitDataTableTotalAH = new UnitDataTable
                    {
                        Unit = "GRAND TOTAL",
                        buyer = "Total AH",
                        qty = (GrandtotalAH[weekKey]).ToString(),
                        week = weekKey
                    };
                    if (!rowData.ContainsKey(GrandtotalAHKey))
                    {

                        rowData.Add(GrandtotalAHKey, new List<UnitDataTable> { unitDataTableTotalAH });
                    }
                    else
                    {
                        rowData[GrandtotalAHKey].Add(unitDataTableTotalAH);
                    }

                    UnitDataTable unitDataTableTotalEH = new UnitDataTable
                    {
                        Unit = "GRAND TOTAL",
                        buyer = "Total EH",
                        qty = (GrandtotalEH[weekKey]).ToString(),
                        week = weekKey
                    };
                    if (!rowData.ContainsKey(GrandtotalEHKey))
                    {

                        rowData.Add(GrandtotalEHKey, new List<UnitDataTable> { unitDataTableTotalEH });
                    }
                    else
                    {
                        rowData[GrandtotalEHKey].Add(unitDataTableTotalEH);
                    }

                    UnitDataTable unitDataTableTotalUsedEH = new UnitDataTable
                    {
                        Unit = "GRAND TOTAL",
                        buyer = "Used EH Booking",
                        qty = (GrandtotalusedEH[weekKey]).ToString(),
                        week = weekKey
                    };
                    if (!rowData.ContainsKey(GrandtotaluseEHKey))
                    {

                        rowData.Add(GrandtotaluseEHKey, new List<UnitDataTable> { unitDataTableTotalUsedEH });
                    }
                    else
                    {
                        rowData[GrandtotaluseEHKey].Add(unitDataTableTotalUsedEH);
                    }

                    UnitDataTable unitDataTableUsedEHConf = new UnitDataTable
                    {
                        Unit = "GRAND TOTAL",
                        buyer = "Used EH Confirm",
                        qty = GrandtotalusedEHConfirm.ContainsKey(weekKey) ? GrandtotalusedEHConfirm[weekKey].ToString() : "0",
                        week = weekKey
                    };
                    if (!rowData.ContainsKey(GrandtotaluseEHConfKey))
                    {

                        rowData.Add(GrandtotaluseEHConfKey, new List<UnitDataTable> { unitDataTableUsedEHConf });
                    }
                    else
                    {
                        rowData[GrandtotaluseEHConfKey].Add(unitDataTableUsedEHConf);
                    }

                    UnitDataTable unitDataTableTotalremEH = new UnitDataTable
                    {
                        Unit = "GRAND TOTAL",
                        buyer = "Remaining EH",
                        qty = (GrandtotalremEH[weekKey]).ToString(),
                        week = weekKey
                    };
                    if (!rowData.ContainsKey(GrandtotalremEHKey))
                    {

                        rowData.Add(GrandtotalremEHKey, new List<UnitDataTable> { unitDataTableTotalremEH });
                    }
                    else
                    {
                        rowData[GrandtotalremEHKey].Add(unitDataTableTotalremEH);
                    }

                    //var totalWHBook = GrandtotalWHBook[weekKey] / unitCount;
                    var totalWHBook = GrandtotalusedEH.ContainsKey(weekKey) ? GrandtotalusedEH[weekKey] / (GrandtotalOp[weekKey] * Math.Round((GrandtotalEff[weekKey] / unitCount), 2) / 100):0;

                    UnitDataTable unitDataTableTotalWHBooking = new UnitDataTable
                    {
                        Unit = "GRAND TOTAL",
                        buyer = "WH Booking",
                        qty = (Math.Round(totalWHBook, 2)).ToString(),
                        week = weekKey
                    };
                    if (!rowData.ContainsKey(GrandtotalwhBookKey))
                    {

                        rowData.Add(GrandtotalwhBookKey, new List<UnitDataTable> { unitDataTableTotalWHBooking });
                    }
                    else
                    {
                        rowData[GrandtotalwhBookKey].Add(unitDataTableTotalWHBooking);
                    }

                    //var totalWHConf = GrandtotalWHConf[weekKey] / unitCount;
                    var totalWHConf = GrandtotalusedEHConfirm.ContainsKey(weekKey)? GrandtotalusedEHConfirm[weekKey] / (GrandtotalOp[weekKey] * Math.Round((GrandtotalEff[weekKey] / unitCount), 2) / 100):0;

                    UnitDataTable unitDataTableTotalWHConf = new UnitDataTable
                    {
                        Unit = "GRAND TOTAL",
                        buyer = "WH Confirm",
                        qty = (Math.Round(totalWHConf, 2)).ToString(),
                        week = weekKey
                    };
                    if (!rowData.ContainsKey(GrandtotalwhConfirmKey))
                    {

                        rowData.Add(GrandtotalwhConfirmKey, new List<UnitDataTable> { unitDataTableTotalWHConf });
                    }
                    else
                    {
                        rowData[GrandtotalwhConfirmKey].Add(unitDataTableTotalWHConf);
                    }
                }

                
            }
            #endregion

            //var c = 0;
            foreach (var s in smv.Keys.OrderBy(a=>a))
            {
                var buy = "Total Booking";
                var smvAvg = smv[s] / count[s];
                var smvKey= s.Split("-");

                var un = smvKey[0];
                var smvTotalKey = string.Concat(un,"-", buy);

                if (!smv.ContainsKey(smvTotalKey))
                {
                    smv.Add(smvTotalKey, smvAvg);
                }
                else
                {
                    smv[smvTotalKey] += smvAvg;
                }

                if (!count.ContainsKey(smvTotalKey))
                {
                    count.Add(smvTotalKey, 1);
                }
                else
                {
                    count[smvTotalKey] += 1;
                }
            }


            Dictionary<string, string> Rowcount = new Dictionary<string, string>();
            int idx = 1;
            var rCount = 0;
            if (data.Count == 0)
            {
                result.Rows.Add(rowValuesForEmptyData.ToArray());
            }
            else
            {
                foreach (var rowValue in rowData)
                {
                    idx++;
                    List<object> rowValues = new List<object>();

                    var keys = rowValue.Key.Split("-");

                    string unBuy = "";


                    var unitCode= keys[0];

                    if (!Rowcount.ContainsKey(unitCode))
                    {
                        rCount = 0;
                        var index = idx;
                        Rowcount.Add(unitCode, index.ToString() );
                    }
                    else
                    {
                        rCount += 1;
                        Rowcount[unitCode] = Rowcount[unitCode] + "-" + rCount.ToString();
                        var val = Rowcount[unitCode].Split("-");
                        if ((val).Length > 0)
                        {
                            Rowcount[unitCode] = val[0] + "-" + rCount.ToString();
                        }
                    }

                    if (keys.Length > 2)
                    {
                        rowValues.Add(keys[0]);//unit
                        rowValues.Add(keys[1].Trim() + " - " + keys[2].Trim());//buyer-como
                        unBuy = string.Concat(keys[0], "-", keys[1], "-", keys[2]);
                    }
                    else
                    {
                        rowValues.Add(keys[0]);//unit
                        rowValues.Add(keys[1].Trim());//TOTAL
                        unBuy = string.Concat(keys[0], "-", keys[1]);
                    }

                    var smvAvg = smv.ContainsKey(unBuy) && count.ContainsKey(unBuy) ? Math.Round((smv[unBuy] / count[unBuy]),2).ToString() : "-";
                    rowValues.Add(smvAvg);

                    foreach (var a in rowValue.Value)
                    {
                        foreach (var w in column)
                        {
                            if(w.Value==a.week)
                                rowValues.Add(a.qty);
                            
                        }
                    }
                    result.Rows.Add(rowValues.ToArray());
                }
            }

            ExcelPackage package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Blocking Plan Sewing");
            sheet.Cells["A1"].LoadFromDataTable(result, true, OfficeOpenXml.Table.TableStyles.Light16);

            for (int y = 1; y <= sheet.Dimension.Rows; y++)
            {
                for (int x = 1; x <= sheet.Dimension.Columns; x++)
                {
                    var cell = sheet.Cells[y, x];

                    cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    cell.Style.Font.Size = 12;
                    cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cell.Style.Fill.BackgroundColor.SetColor(Color.WhiteSmoke);

                    if (y > 1)
                    {
                        if (x == 2)
                        {
                            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        }
                        if (sheet.Cells[y, 2].Value.ToString() == "Remaining EH")
                        {
                            if (x > 3 && x <= sheet.Dimension.Columns)
                            {
                                cell.Style.Fill.BackgroundColor.SetColor(
                                    Convert.ToInt32(cell.Value) > 0 ? Color.Yellow :
                                    Convert.ToInt32(cell.Value) < 0 ? Color.Red :
                                    Color.Green);
                            }
                        }
                        if (sheet.Cells[y, 2].Value.ToString() == "WH Booking" || sheet.Cells[y, 2].Value.ToString() == "WH Confirm")
                        {
                            if (x > 3 && x <= sheet.Dimension.Columns)
                            {
                                cell.Style.Fill.BackgroundColor.SetColor(
                                    Convert.ToDouble(cell.Value) <= 45.5 ? Color.Yellow :
                                    Convert.ToDouble(cell.Value) > 45.5 && Convert.ToDouble(cell.Value) <=50.5 ? Color.Green :
                                    Convert.ToDouble(cell.Value) > 50.5 && Convert.ToDouble(cell.Value) <= 56.5 ? Color.Red :
                                    Color.LightSlateGray);
                            }
                        }

                        if (sheet.Cells[y, 2].Value.ToString().Contains("-"))
                        {
                            if (x > 3 && x <= sheet.Dimension.Columns)
                            {
                                var categoryUWB = string.Concat(sheet.Cells[y, 1].Value.ToString(), "-", (x - 3).ToString(), "-", sheet.Cells[y, 2].Value.ToString());
                                if (cell.Value.ToString() != "-")
                                {
                                    cell.Style.Fill.BackgroundColor.SetColor(
                                        totalConfirmed.ContainsKey(categoryUWB) ? totalConfirmed[categoryUWB] == "yellow" ?
                                        Color.Yellow : totalConfirmed[categoryUWB] == "orange" ? Color.Orange : Color.WhiteSmoke : Color.WhiteSmoke);
                                }
                            }
                        }
                        else
                        {
                            if (x > 1 && x <= sheet.Dimension.Columns)
                            {
                                cell.Style.Font.Bold = true;
                            }
                            if (x == 3 && cell.Value.ToString() == "-")
                            {
                                cell.Value = "";
                            }
                        }
                    }

                }
            }

            foreach (var rowMerge in Rowcount)
            {
                var UnitrowNum= rowMerge.Value.Split("-");
                int rowNum2 = 1;
                int rowNum1 = Convert.ToInt32(UnitrowNum[0]);
                if (UnitrowNum.Length > 1)
                {
                    rowNum2 = Convert.ToInt32(rowNum1) + Convert.ToInt32(UnitrowNum[1]);
                }
                else
                {
                    rowNum2 = Convert.ToInt32(rowNum1);
                }
                
                sheet.Cells[$"A{rowNum1}:A{rowNum2}"].Merge = true;
                sheet.Cells[$"A{rowNum1}:A{rowNum2}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                sheet.Cells[$"A{rowNum1}:A{rowNum2}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            }

            
            sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
            MemoryStream streamExcel = new MemoryStream();
            package.SaveAs(streamExcel);

            Dictionary<string, string> FilterDictionary = new Dictionary<string, string>(JsonConvert.DeserializeObject<Dictionary<string, string>>(filter), StringComparer.OrdinalIgnoreCase);
            string fileName = string.Concat("Sewing Blocking Plan Report ", FilterDictionary["year"], ".xlsx");

            return Tuple.Create(streamExcel, fileName);
        }

        public Tuple<List<SewingBlockingPlanReportViewModel>, int> Read(int page = 1, int size = 25, string filter = "{}")
        {
            var Query = SewingBlockingPlanReportLogic.GetQuery(filter);
            var data = Query.ToList();
            return Tuple.Create(data, data.Count);
        }

        class UnitDataTable
        {
            public string Unit { get; set; }
            public string buyer { get; set; }
            public string qty { get; set; }
            public int week { get; set; }
        }

        class unitTable
        {
            public string unit { get; set; }
            public string buyer { get; set; }
            public byte week { get; set; }
        }

    }
}
