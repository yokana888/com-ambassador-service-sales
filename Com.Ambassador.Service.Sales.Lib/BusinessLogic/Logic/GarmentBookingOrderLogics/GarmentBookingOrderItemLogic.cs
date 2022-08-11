using Com.Ambassador.Service.Sales.Lib.Models.GarmentBookingOrderModel;
using Com.Ambassador.Service.Sales.Lib.Services;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Sales.Lib.BusinessLogic.Logic.GarmentBookingOrderLogics
{
    public class GarmentBookingOrderItemLogic : BaseLogic<GarmentBookingOrderItem>
    {
        public GarmentBookingOrderItemLogic(IIdentityService IdentityService, IServiceProvider serviceProvider, SalesDbContext dbContext) : base(IdentityService, serviceProvider, dbContext)
        {
        }

        public async override Task DeleteAsync(long id)
        {
            var model = await ReadByIdAsync(id);

            EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service", true);
            DbSet.Update(model);
        }

        public override ReadResponse<GarmentBookingOrderItem> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            throw new NotImplementedException();
        }

        public HashSet<long> GetBookingOrderIds(long id)
        {
            return new HashSet<long>(DbSet.Where(d => d.GarmentBookingOrder.Id == id).Select(d => d.Id));
        }

        public override void UpdateAsync(long id, GarmentBookingOrderItem model)
        {
            EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
            DbSet.Update(model);
        }
    }
}
