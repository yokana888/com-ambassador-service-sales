using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.Garment.WeeklyPlanInterfaces;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.Garment.WeeklyPlanViewModels
{
    public class GarmentWeeklyPlanViewModel : BaseViewModel, IValidatableObject
    {
        public short Year { get; set; }

        public UnitViewModel Unit { get; set; }

        public List<GarmentWeeklyPlanItemViewModel> Items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Unit == null || Unit.Id == 0)
            {
                yield return new ValidationResult("Unit tidak boleh kosong", new List<string> { "Unit" });
            }
            else if (Id == 0)
            {
                IWeeklyPlanFacade facade = validationContext.GetService<IWeeklyPlanFacade>();
                var filter = new
                {
                    Year,
                    UnitId = Unit.Id
                };
                var readResponse = facade.Read(1, 25, "{}", null, "", JsonConvert.SerializeObject(filter));
                if (readResponse.Count > 0)
                {
                    yield return new ValidationResult($"Master Minggu Unit {Unit.Code} - {Unit.Name} sudah dibuat untuk tahun {Year}", new List<string> { "Unit" });
                }
            }
        }
    }
}
