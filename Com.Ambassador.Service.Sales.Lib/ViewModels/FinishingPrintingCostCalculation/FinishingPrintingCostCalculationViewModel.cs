using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.Models.FinishingPrintingCostCalculation;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrinting;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrintingCostCalculation
{
    public class FinishingPrintingCostCalculationViewModel : BaseViewModel, IValidatableObject
    {
        public FinishingPrintingPreSalesContractViewModel PreSalesContract { get; set; }

        public MaterialViewModel Material { get; set; }

        public string Color { get; set; }

        public InstructionViewModel Instruction { get; set; }

        public UomViewModel UOM { get; set; }

        public AccountViewModel Sales { get; set; }

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

        public ProductViewModel Greige { get; set; }

        public double PreparationFabricWeight { get; set; }

        public double RFDFabricWeight { get; set; }

        public decimal ActualPrice { get; set; }

        public decimal ScreenCost { get; set; }

        public string ScreenDocumentNo { get; set; }

        public List<FinishingPrintingCostCalculationMachineViewModel> Machines { get; set; }

        public decimal FreightCost { get; set; }

        public decimal Embalase { get; set; }

        public decimal GeneralAdministrationCost { get; set; }

        public decimal DirectorOfficeCost { get; set; }

        public decimal BankMiscCost { get; set; }

        public string Remark { get; set; }

        public string ProductionOrderNo { get; set; }

        public bool IsPosted { get; set; }

        public ApprovalViewModel ApprovalMD { get; set; }

        public ApprovalViewModel ApprovalPPIC { get; set; }

        public string ImageFile { get; set; }

        public string ImagePath { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PreSalesContract == null)
            {

                yield return new ValidationResult("Sales Contract harus diisi!", new List<string> { "PreSalesContract" });
            }
            //else
            //{
            //    var fpCCService = validationContext.GetService<IFinishingPrintingCostCalculationService>();
            //    if (fpCCService.ValidatePreSalesContractId(PreSalesContract.Id).Result)
            //    {
            //        yield return new ValidationResult("Sales Contract Sudah Dibuat!", new List<string> { "PreSalesContract" });
            //    }
            //}

            if (Instruction == null)
                yield return new ValidationResult("Instruksi harus diisi!", new List<string> { "Instruction" });

            if (UOM == null)
            {
                yield return new ValidationResult("Satuan harus diisi!", new List<string> { "UOM" });
            }

            if (Sales == null)
                yield return new ValidationResult("Sales harus diisi!", new List<string> { "Sales" });

            if (string.IsNullOrEmpty(Color))
                yield return new ValidationResult("Warna harus diisi!", new List<string> { "Color" });

            if (Greige == null)
                yield return new ValidationResult("Greige harus diisi!", new List<string> { "Greige" });


            if (Material == null)
                yield return new ValidationResult("Material harus diisi!", new List<string> { "Material" });

            if (CurrencyRate <= 0)
                yield return new ValidationResult("Kurs harus lebih dari 0!", new List<string> { "CurrencyRate" });

            if (ProductionUnitValue <= 0)
                yield return new ValidationResult("Produksi Unit harus lebih dari 0!", new List<string> { "ProductionUnitValue" });

            if (PreSalesContract.Unit.Name.ToLower() == "printing")
            {
                if (ScreenCost <= 0)
                    yield return new ValidationResult("Biaya Screen harus lebih dari 0!", new List<string> { "ScreenCost" });

                if (string.IsNullOrEmpty(ScreenDocumentNo))
                    yield return new ValidationResult("No Dokumen harus diisi!", new List<string> { "ScreenDocumentNo" });
            }

            if (PreparationFabricWeight <= 0)
                yield return new ValidationResult("Berat Kain Prep harus lebih dari 0!", new List<string> { "PreparationFabricWeight" });

            if (RFDFabricWeight <= 0)
                yield return new ValidationResult("Berat Kain RFD harus lebih dari 0!", new List<string> { "RFDFabricWeight" });

            if (ActualPrice <= 0)
                yield return new ValidationResult("Harga Real harus lebih dari 0!", new List<string> { "ActualPrice" });

            if (ConfirmPrice <= 0)
                yield return new ValidationResult("Confirm Price harus diisi!", new List<string> { "ConfirmPrice" });

            if (FreightCost <= 0)
                yield return new ValidationResult("Biaya Angkut harus lebih dari 0!", new List<string> { "FreightCost" });


            if (Machines == null || Machines.Count == 0)
                yield return new ValidationResult("Asuransi harus lebih dari 0!", new List<string> { "Machine" });

            else
            {
                var anyError = false;
                var machinesErrors = "[ ";
                foreach (var machine in Machines)
                {
                    machinesErrors += "{";

                    if (machine.Machine == null)
                    {
                        anyError = true;
                        machinesErrors += "Machine: 'Mesin harus diisi!', ";
                    }

                    if (machine.Step == null)
                    {
                        anyError = true;
                        machinesErrors += "StepProcess: 'Proses harus diisi!', ";
                    }

                    if (machine.Chemicals == null || machine.Chemicals.Count == 0)
                    {
                        anyError = true;
                        machinesErrors += "Chemical: 'Biaya Chemical harus diisi!', ";
                    }
                    else
                    {
                        machinesErrors += "Chemicals: [ ";

                        foreach (var chemical in machine.Chemicals)
                        {
                            machinesErrors += "{";

                            if (chemical.Chemical == null)
                            {
                                anyError = true;
                                machinesErrors += "Chemical: 'Chemical harus diisi!', ";
                            }

                            if (chemical.ChemicalQuantity <= 0)
                            {
                                anyError = true;
                                machinesErrors += "ChemicalQuantity: 'Chemical Quantity harus lebih dari 0!', ";
                            }

                            machinesErrors += "}, ";
                        }

                        machinesErrors += "], ";
                    }

                    machinesErrors += "}, ";
                }
                machinesErrors += " ]";

                if (anyError)
                {
                    yield return new ValidationResult(machinesErrors, new List<string> { "Machines" });
                }
            }
        }
    }
    
}
