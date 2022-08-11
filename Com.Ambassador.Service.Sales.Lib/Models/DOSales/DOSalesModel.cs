using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Ambassador.Service.Sales.Lib.Models.DOSales
{
    public class DOSalesModel : BaseModel
    {
        public DOSalesModel()
        {
            DOSalesDetailItems = new HashSet<DOSalesDetailModel>();
        }

        [MaxLength(255)]
        public string Code { get; set; }
        public long AutoIncreament { get; set; }
        [MaxLength(255)]
        public string DOSalesNo { get; set; }
        [MaxLength(255)]
        public string DOSalesType { get; set; }
        [MaxLength(255)]
        public string DOSalesCategory { get; set; }
        [MaxLength(255)]
        public string Status { get; set; }
        public bool Accepted { get; set; }
        public bool Declined { get; set; }
        [MaxLength(255)]
        public string Type { get; set; }
        public DateTimeOffset Date { get; set; }
        #region Sales Contract
        public int SalesContractId { get; set; }
        [MaxLength(255)]
        public string SalesContractNo { get; set; }
        [MaxLength(255)]
        public string MaterialWidth { get; set; }
        [MaxLength(255)]
        public string PieceLength { get; set; }
        public double OrderQuantity { get; set; }
        #endregion
        #region Material
        public long MaterialId { get; set; }
        [MaxLength(255)]
        public string MaterialCode { get; set; }
        [MaxLength(1000)]
        public string MaterialName { get; set; }
        public double MaterialPrice { get; set; }
        [MaxLength(255)]
        public string MaterialTags { get; set; }
        #endregion
        #region Material Construction
        public int MaterialConstructionId { get; set; }
        [MaxLength(25)]
        public string MaterialConstructionCode { get; set; }
        [MaxLength(255)]
        public string MaterialConstructionName { get; set; }
        [MaxLength(1000)]
        public string MaterialConstructionRemark { get; set; }
        [MaxLength(255)]
        public string ColorRequest { get; set; }
        [MaxLength(255)]
        public string ColorTemplate { get; set; }
        #endregion
        #region Commodity
        public int CommodityId { get; set; }
        [MaxLength(25)]
        public string CommodityCode { get; set; }
        [MaxLength(255)]
        public string CommodityName { get; set; }
        [MaxLength(1000)]
        public string CommodityDescription { get; set; }
        #endregion
        #region Buyer
        public long BuyerId { get; set; }
        [MaxLength(255)]
        public string BuyerCode { get; set; }
        [MaxLength(1000)]
        public string BuyerName { get; set; }
        [MaxLength(255)]
        public string BuyerType { get; set; }
        [MaxLength(1000)]
        public string BuyerAddress { get; set; }
        #endregion
        [MaxLength(255)]
        public string DestinationBuyerName { get; set; }
        [MaxLength(1000)]
        public string DestinationBuyerAddress { get; set; }
        [MaxLength(255)]
        public string SalesName { get; set; }
        [MaxLength(255)]
        public string HeadOfStorage { get; set; }

        
        public int StorageId { get; set; }

        [MaxLength(255)]
        public string StorageName { get; set; }

        [MaxLength(255)]
        public string StorageCode { get; set; }

        [MaxLength(255)]
        public string StorageUnit { get; set; }

        [MaxLength(255)]
        public string PackingUom { get; set; }
        [MaxLength(255)]
        public string LengthUom { get; set; }
        [MaxLength(255)]
        public string WeightUom { get; set; }
        [MaxLength(255)]
        public string BaleUom { get; set; }
        public int Disp { get; set; }
        public int Op { get; set; }
        public int Sc { get; set; }
        [MaxLength(255)]
        public string DoneBy { get; set; }
        public double FillEachBale { get; set; }
        [MaxLength(1000)]
        public string Remark { get; set; }

        [MaxLength(4096)]
        public string Construction { get; set; }

        public virtual ICollection<DOSalesDetailModel> DOSalesDetailItems { get; set; }
    }
}
