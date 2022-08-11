using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrinting
{
    public class FinishingPrintingSalesContractViewModel : BaseViewModel, IValidatableObject
    {
        public AccountBankViewModel AccountBank { get; set; }
        public double? Amount { get; set; }
        public AgentViewModel Agent { get; set; }
        public int? AutoIncrementNumber { get; set; }
        public BuyerViewModel Buyer { get; set; }
        public string Code { get; set; }
        public string Commission { get; set; }
        public CommodityViewModel Commodity { get; set; }
        public string CommodityDescription { get; set; }
        public string Condition { get; set; }
        public string DeliveredTo { get; set; }
        public DateTimeOffset? DeliverySchedule { get; set; }
        public OrderTypeViewModel DesignMotive { get; set; }
        public string DispositionNumber { get; set; }
        public bool? FromStock { get; set; }
        public ProductViewModel Material { get; set; }
        public MaterialConstructionViewModel MaterialConstruction { get; set; }
        public string MaterialWidth { get; set; }
        public double? OrderQuantity { get; set; }
        public OrderTypeViewModel OrderType { get; set; }
        public string Packing { get; set; }
        public string PieceLength { get; set; }
        public double? PointLimit { get; set; }
        public int? PointSystem { get; set; }
        public QualityViewModel Quality { get; set; }
        public string SalesContractNo { get; set; }
        public string ShipmentDescription { get; set; }
        public double? ShippingQuantityTolerance { get; set; }
        public TermOfPaymentViewModel TermOfPayment { get; set; }
        public string TermOfShipment { get; set; }
        public string TransportFee { get; set; }
        public bool? UseIncomeTax { get; set; }
        public VatTaxViewModel VatTax { get; set; }
        public UomViewModel UOM { get; set; }
        public YarnMaterialViewModel YarnMaterial { get; set; }
        public double? RemainingQuantity { get; set; }
        public ProductTypeViewModel ProductType { get; set; }
        public string DownPayments { get; set; }
        public double? PriceDP { get; set; }
        public string PaymentMethods { get; set; }
        public int? Day { get; set; }
        public double? precentageDP { get; set; }
        public int? LatePayment { get; set; }
        public int? LateReturn { get; set; }
        public double? Claim { get; set; }
        public List<FinishingPrintingSalesContractDetailViewModel> Details { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Buyer == null || Buyer.Id.Equals(0))
            {
                yield return new ValidationResult("Buyer harus di isi", new List<string> { "BuyerID" });
            }
            else
            {
                if (Buyer.Type.ToLower().Equals("ekspor") || Buyer.Type.ToLower().Equals("export"))
                {
                    if (string.IsNullOrWhiteSpace(TermOfShipment))
                    {
                        yield return new ValidationResult("Term of Shipment harus diisi", new List<string> { "TermOfShipment" });
                    }
                    if (Amount <= 0)
                    {
                        yield return new ValidationResult("Amount harus lebih besar dari 0", new List<string> { "Amount" });
                    }
                    if (Agent != null && !Agent.Id.Equals(0))
                    {
                        if (string.IsNullOrWhiteSpace(Commission))
                        {
                            yield return new ValidationResult("Komisi harus diisi", new List<string> { "Commission" });
                        }
                    }
                }
            }

            if (Commodity == null || Commodity.Id.Equals(0))
            {
                yield return new ValidationResult("Komoditas harus diisi", new List<string> { "CommodityID" });
            }

            if (OrderType == null || OrderType.Id.Equals(0))
            {
                yield return new ValidationResult("Jenis Order harus diisi", new List<string> { "OrderTypeID" });
            }

            if (Material == null || Material.Id.Equals(0))
            {
                yield return new ValidationResult("Material harus diisi", new List<string> { "MaterialID" });
            }

            if (MaterialConstruction == null || MaterialConstruction.Id.Equals(0))
            {
                yield return new ValidationResult("Konstruksi Finish harus diisi", new List<string> { "MaterialConstructionID" });
            }

            if (YarnMaterial == null || YarnMaterial.Id.Equals(0))
            {
                yield return new ValidationResult("Nomor Benang Material harus diisi", new List<string> { "YarnMaterialID" });
            }

            if (string.IsNullOrWhiteSpace(MaterialWidth))
            {
                yield return new ValidationResult("Lebar Finish harus diisi", new List<string> { "MaterialWidth" });
            }

            if (OrderQuantity <= 0)
            {
                yield return new ValidationResult("Jumlah Order harus lebih besar dari 0", new List<string> { "OrderQuantity" });
            }

            if (UOM == null || UOM.Id.Equals(0))
            {
                yield return new ValidationResult("Satuan harus diisi", new List<string> { "UOMID" });
            }

            if (Quality == null || Quality.Id.Equals(0))
            {
                yield return new ValidationResult("Kualitas harus diisi", new List<string> { "QualityID" });
            }

            //if (TermOfPayment == null || TermOfPayment.Id.Equals(0))
            //{
            //    yield return new ValidationResult("Syarat Pembayaran harus diisi", new List<string> { "TermOfPaymentID" });
            //}

            if (AccountBank == null || AccountBank.Id.Equals(0))
            {
                yield return new ValidationResult("Pembayaran ke Rekening harus diisi", new List<string> { "AccountBankID" });
            }

            if (string.IsNullOrWhiteSpace(DeliveredTo))
            {
                yield return new ValidationResult("Tujuan Kirim harus diisi", new List<string> { "DeliveredTo" });
            }

            if (DeliverySchedule == null || DeliverySchedule <= DateTimeOffset.Now)
            {
                yield return new ValidationResult("Jadwal Pengiriman harus diisi", new List<string> { "DeliverySchedule" });
            }

            if (PointSystem != 10 && PointSystem != 4)
            {
                yield return new ValidationResult("Point sistem tidak valid", new List<string> { "PointSystem" });
            }
            else if (PointSystem == 4)
            {
                if (PointLimit <= 0)
                {
                    yield return new ValidationResult("Point limit harus lebih besar dari 0", new List<string> { "PointLimit" });
                }
            }

            if (Details == null || Details.Count.Equals(0))
            {
                yield return new ValidationResult("Detail harus diisi", new List<string> { "Details" });
            }
            else
            {
                int Count = 0;
                string DetailError = "[";

                foreach (FinishingPrintingSalesContractDetailViewModel Detail in Details)
                {
                    if (string.IsNullOrWhiteSpace(Detail.Color))
                    {
                        Count++;
                        DetailError += "{ Color: 'Warna harus diisi' }, ";
                    }

                    if (Detail.Price <= 0)
                    {
                        Count++;
                        DetailError += "{ Price: 'Harga harus lebih besar dari 0' }, ";
                    }
                }

                if (Count > 0)
                {
                    yield return new ValidationResult(DetailError, new List<string> { "Details" });
                }
            }

            //if (PaymentMethods.Equals(null) || PaymentMethods == "")
            //{
            //    yield return new ValidationResult("Cara Pembayaran harus diisi", new List<string> { "PaymentMethods" });
            //}

            if (this.Day.Equals(null))
            {
                yield return new ValidationResult("Hari harus diisi", new List<string> { "Day" });
            }

            //if (DownPayments.Equals(null) || DownPayments == "")
            //{
            //    yield return new ValidationResult("Tipe DP harus diisi", new List<string> { "DownPayments" });
            //}

            //if (precentageDP.Equals(null) || precentageDP == 0)
            //{
            //    yield return new ValidationResult("Percentage (%) DP harus diisi", new List<string> { "precentageDP" });
            //}

            if (this.LatePayment.Equals(null))
            {
                yield return new ValidationResult("Besar Denda harus diisi", new List<string> { "LatePayment" });
            }

            if(this.LateReturn.Equals(null))
            {
                yield return new ValidationResult("Hari Pengembalian harus diisi", new List<string> { "LateReturn" });
            }

            //if(Claim.Equals(null))
            //{
            //    yield return new ValidationResult("Claim Hari harus diisi", new List<string> { "Claim" });
            //}
        }
    }
}
