using Com.Ambassador.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.Models.FinishingPrintingCostCalculation
{
    public class FinishingPrintingCostCalculationModel : BaseModel
    {
        [MaxLength(16)]
        public string Code { get; set; }

        [MaxLength(64)]
        public string ProductionOrderNo { get; set; }

        public long PreSalesContractId { get; set; }

        [MaxLength(64)]
        public string PreSalesContractNo { get; set; }

        public int UnitId { get; set; }

        [MaxLength(512)]
        public string UnitName { get; set; }

        public long MaterialId { get; set; }

        [MaxLength(1024)]
        public string MaterialName { get; set; }

        [MaxLength(25)]
        public string MaterialCode { get; set; }

        public double MaterialPrice { get; set; }

        [MaxLength(255)]
        public string MaterialTags { get; set; }

        [MaxLength(256)]
        public string Color { get; set; }

        public int InstructionId { get; set; }
        
        [MaxLength(128)]
        public string InstructionName { get; set; }

        public long UomId { get; set; }

        [MaxLength(128)]
        public string UomUnit { get; set; }

        public long SalesId { get; set; }

        [MaxLength(1024)]
        public string SalesUserName { get; set; }

        [MaxLength(1024)]
        public string SalesFirstName { get; set; }

        [MaxLength(1024)]
        public string SalesLastName { get; set; }

        public DateTimeOffset Date { get; set; }

        public decimal CurrencyRate { get; set; }

        public decimal ProductionUnitValue { get; set; }

        public decimal ManufacturingServiceCost { get; set; }

        public decimal HelperMaterial { get; set; }

        public decimal MiscMaterial { get; set; }

        public decimal Lubricant { get; set; }

        public decimal SparePart { get; set; }

        public decimal StructureMaintenance { get; set; }

        public decimal MachineMaintenance { get; set; }

        public decimal ConfirmPrice { get; set; }

        public long GreigeId { get; set; }
        
        [MaxLength(1024)]
        public string GreigeName { get; set; }

        public decimal GreigePrice { get; set; }

        public double PreparationFabricWeight { get; set; }
        
        public double RFDFabricWeight { get; set; }
        
        public decimal ActualPrice { get; set; }

        public decimal ScreenCost { get; set; }

        public string ScreenDocumentNo { get; set; }

        public ICollection<FinishingPrintingCostCalculationMachineModel> Machines { get; set; }

        public decimal FreightCost { get; set; }

        public decimal Embalase { get; set; }

        public decimal GeneralAdministrationCost { get; set; }

        public decimal DirectorOfficeCost { get; set; }

        public decimal BankMiscCost { get; set; }

        [MaxLength(4096)]
        public string Remark { get; set; }
        
        public bool IsPosted { get; set; }

        public bool IsSCCreated { get; set; }

        public bool IsApprovedPPIC { get; set; }

        public DateTimeOffset ApprovedPPICDate { get; set; }
        
        [MaxLength(512)]
        public string ApprovedPPICBy { get; set; }

        public bool IsApprovedMD { get; set; }

        public DateTimeOffset ApprovedMDDate { get; set; }

        [MaxLength(512)]
        public string ApprovedMDBy { get; set; }

        public string ImageFile { get; set; }
        [MaxLength(1000)]
        public string ImagePath { get; set; }
    }
}
