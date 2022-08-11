using Com.Ambassador.Service.Sales.Lib.ViewModels.ProductionOrder;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.PDFTemplates
{
    public class ProductionOrderPDFTemplate
    {
        public MemoryStream GeneratePdfTemplate(ProductionOrderViewModel viewModel, int timeoffset)
        {
            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 18);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
            Font extra_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 9);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);

            Document document = new Document(PageSize.A4, 40, 40, 25, 25);
            //Document document = new Document(PageSize.A4, 40, 40, 40, 40);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            document.Open();

            #region customViewModel

            double spelling = (double)viewModel.ShippingQuantityTolerance;
            double spellOrder = ((spelling / 100) * (double)viewModel.OrderQuantity) + (double)viewModel.OrderQuantity;

            #endregion

            #region Header

            //string blankString = " ";
            //Paragraph bankSpace = new Paragraph(blankString, normal_font);
            //bankSpace.SpacingAfter = 30f;
            //document.Add(bankSpace);


            string codeNoString = "FM-PJ-00-03-021/R1";
            Paragraph codeNo = new Paragraph(codeNoString, bold_font) { Alignment = Element.ALIGN_RIGHT };
            codeNo.SpacingAfter = 10f;
            document.Add(codeNo);

            string titleString = "SURAT PERINTAH PRODUKSI";
            Paragraph title = new Paragraph(titleString, bold_font) { Alignment = Element.ALIGN_CENTER };
            title.SpacingAfter = 10f;
            document.Add(title);
            bold_font.SetStyle(Font.NORMAL);

            #endregion

            #region body
            PdfPTable tableIdentity = new PdfPTable(2);
            tableIdentity.DefaultCell.Border = Rectangle.NO_BORDER;
            tableIdentity.WidthPercentage = 100;

            PdfPCell cellIdentityContentLeft = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellIdentityContentRight = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };

            PdfPCell cellIdentityContentLeft1WithBorder = new PdfPCell() { HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellIdentityContentRightWithBorder = new PdfPCell() { HorizontalAlignment = Element.ALIGN_RIGHT };

            PdfPCell cellIdentityContentCenterWithBorder = new PdfPCell() { HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPTable tableIdentity1 = new PdfPTable(2);

            if (viewModel.POType == "SALES")
            {
                cellIdentityContentLeft.Phrase = new Phrase("No. Sales Contract", normal_font);
                tableIdentity1.AddCell(cellIdentityContentLeft);
                cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.FinishingPrintingSalesContract.SalesContractNo, normal_font);
                tableIdentity1.AddCell(cellIdentityContentLeft);
            }

            cellIdentityContentLeft.Phrase = new Phrase("Nomor Order", normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.OrderNo, normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);

            if (viewModel.POType == "SALES")
            {
                cellIdentityContentLeft.Phrase = new Phrase("Nama Buyer", normal_font);
                tableIdentity1.AddCell(cellIdentityContentLeft);
                cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.Buyer.Name, normal_font);
                tableIdentity1.AddCell(cellIdentityContentLeft);

                cellIdentityContentLeft.Phrase = new Phrase("Tipe Buyer", normal_font);
                tableIdentity1.AddCell(cellIdentityContentLeft);
                cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.Buyer.Type, normal_font);
                tableIdentity1.AddCell(cellIdentityContentLeft);
            }

            cellIdentityContentLeft.Phrase = new Phrase("Material", normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.Material.Name, normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);

            cellIdentityContentLeft.Phrase = new Phrase("Konstruksi Material", normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.MaterialConstruction.Name, normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);

            cellIdentityContentLeft.Phrase = new Phrase("Nomor Benang Material", normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.YarnMaterial.Name, normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);

            cellIdentityContentLeft.Phrase = new Phrase("Lebar Material", normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.MaterialWidth, normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);

            cellIdentityContentLeft.Phrase = new Phrase("Jenis Order", normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.OrderType.Name, normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);

            cellIdentityContentLeft.Phrase = new Phrase("Jenis Proses", normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.ProcessType.Name, normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);

            cellIdentityContentLeft.Phrase = new Phrase("Jumlah Order", normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(": " + string.Format("{0:n2}", viewModel.OrderQuantity) + " " + viewModel.Uom.Unit, normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);

            cellIdentityContentLeft.Phrase = new Phrase("Jumlah Order + Toleransi Jumlah Kirim", normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(": " + string.Format("{0:n2}", spellOrder) + " " + viewModel.Uom.Unit, normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);

            cellIdentityContentLeft.Phrase = new Phrase("Asal Material", normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.MaterialOrigin, normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);

            cellIdentityContentLeft.Phrase = new Phrase("Lebar Finish", normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.FinishWidth, normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);

            cellIdentityContentLeft.Phrase = new Phrase("Jenis Finish", normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.FinishType.Name, normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);

            if (!string.IsNullOrWhiteSpace(viewModel.DesignCode) && !string.IsNullOrWhiteSpace(viewModel.DesignNumber))
            {
                cellIdentityContentLeft.Phrase = new Phrase("Kode Design", normal_font);
                tableIdentity1.AddCell(cellIdentityContentLeft);
                cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.DesignCode, normal_font);
                tableIdentity1.AddCell(cellIdentityContentLeft);
                cellIdentityContentLeft.Phrase = new Phrase("Nomor Design", normal_font);
                tableIdentity1.AddCell(cellIdentityContentLeft);
                cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.DesignNumber, normal_font);
                tableIdentity1.AddCell(cellIdentityContentLeft);
            }

            cellIdentityContentLeft.Phrase = new Phrase("Standar Handling", normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.HandlingStandard, normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);

            cellIdentityContentLeft.Phrase = new Phrase("RUN", normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.Run, normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);

            string runWidth = "";

            if (viewModel.RunWidth != null)
                runWidth = string.Join(',', viewModel.RunWidth.Select(x => x.Value));

            cellIdentityContentLeft.Phrase = new Phrase("Lebar RUN (cm)", normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(": " + runWidth, normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);

            cellIdentityContentLeft.Phrase = new Phrase("Tulisan Pinggir Kain", normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.ArticleFabricEdge, extra_font);
            //cellIdentityContentLeft.Colspan = 2;
            tableIdentity1.AddCell(cellIdentityContentLeft);



            if (!string.IsNullOrWhiteSpace(viewModel.Run) && viewModel.RunWidth != null  && (viewModel.RunWidth.Count > 0))
            {
                var index = 0;
                foreach (ProductionOrder_RunWidthViewModel runwidths in viewModel.RunWidth)
                {
                    index++;
                    cellIdentityContentLeft.Phrase = new Phrase("Lebar RUN (cm)", normal_font);
                    tableIdentity1.AddCell(cellIdentityContentLeft);
                    if (index > 1)
                    {
                        cellIdentityContentLeft.Phrase = new Phrase("  " + runwidths.Value, normal_font);
                        tableIdentity1.AddCell(cellIdentityContentLeft);
                    }
                    else
                    {
                        cellIdentityContentLeft.Phrase = new Phrase(": " + runwidths.Value, normal_font);
                        tableIdentity1.AddCell(cellIdentityContentLeft);
                    }
                }
            }

            cellIdentityContentLeft.Phrase = new Phrase("Standar Shrinkage", normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.ShrinkageStandard, normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);

            cellIdentityContentLeft.Phrase = new Phrase("Standar Test", normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.StandardTests.Name, normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);

            cellIdentityContentLeft.Phrase = new Phrase("Sample", normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.Sample, normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);

            cellIdentityContentLeft.Phrase = new Phrase("Packing Instruction", normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.PackingInstruction, normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);

            cellIdentityContentLeft.Phrase = new Phrase("Tanggal Delivery", normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.DeliveryDate.AddHours(timeoffset).ToString("dd MMMM yyyy", new CultureInfo("id-ID")), normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);

            cellIdentityContentLeft.Phrase = new Phrase("Keterangan", normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.Remark, extra_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);

            cellIdentityContentLeft.Phrase = new Phrase("Nama Staff Penjualan", normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.Account.UserName, normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);

            cellIdentityContentLeft.Phrase = new Phrase("Tanggal Order dibuat", normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.CreatedUtc.AddHours(timeoffset).ToString("dd MMMM yyyy", new CultureInfo("id-ID")), normal_font);
            tableIdentity1.AddCell(cellIdentityContentLeft);


            PdfPCell cell = new PdfPCell();
            cell.BorderColor = BaseColor.White;

            PdfPTable tableBody1 = new PdfPTable(1);
            cellIdentityContentCenterWithBorder.Phrase = new Phrase("Standar Lampu", normal_font);
            tableBody1.AddCell(cellIdentityContentCenterWithBorder);

            if (viewModel.LampStandards.Count > 0)
            {
                foreach (var item in viewModel.LampStandards)
                {
                    cellIdentityContentCenterWithBorder.Phrase = new Phrase(item.Name, normal_font);
                    tableBody1.AddCell(cellIdentityContentCenterWithBorder);
                }
            }
            tableBody1.SpacingAfter = 30f;

            PdfPTable tableBody2 = new PdfPTable(4);
            cellIdentityContentCenterWithBorder.Phrase = new Phrase("Acuan / Color Way", normal_font);
            tableBody2.AddCell(cellIdentityContentCenterWithBorder);
            cellIdentityContentCenterWithBorder.Phrase = new Phrase("Warna yang Diminta ", normal_font);
            tableBody2.AddCell(cellIdentityContentCenterWithBorder);
            cellIdentityContentCenterWithBorder.Phrase = new Phrase("Jenis Warna", normal_font);
            tableBody2.AddCell(cellIdentityContentCenterWithBorder);
            cellIdentityContentCenterWithBorder.Phrase = new Phrase("Jumlah", normal_font);
            tableBody2.AddCell(cellIdentityContentCenterWithBorder);

            double Total = 0;
            string uom = "";
            if (viewModel.Details.Count > 0)
            {
                foreach (var detail in viewModel.Details)
                {
                    uom = detail.Uom.Unit;
                    cellIdentityContentCenterWithBorder.Phrase = new Phrase(detail.ColorTemplate, normal_font);
                    tableBody2.AddCell(cellIdentityContentCenterWithBorder);
                    cellIdentityContentCenterWithBorder.Phrase = new Phrase(detail.ColorRequest, normal_font);
                    tableBody2.AddCell(cellIdentityContentCenterWithBorder);
                    cellIdentityContentCenterWithBorder.Phrase = new Phrase(detail.ColorType.Name, normal_font);
                    tableBody2.AddCell(cellIdentityContentCenterWithBorder);
                    cellIdentityContentCenterWithBorder.Phrase = new Phrase(string.Format("{0:n2}", detail.Quantity) + " " + detail.Uom.Unit, normal_font);
                    tableBody2.AddCell(cellIdentityContentCenterWithBorder);
                    Total += (double)detail.Quantity;
                }
            }

            cellIdentityContentCenterWithBorder.Phrase = new Phrase("Total", normal_font);
            tableBody2.AddCell(new PdfPCell(cellIdentityContentCenterWithBorder) { Colspan = 3 });
            cellIdentityContentCenterWithBorder.Phrase = new Phrase(string.Format("{0:n2}", Total) + " " + uom, normal_font);
            tableBody2.AddCell(cellIdentityContentCenterWithBorder);


            cell.AddElement(tableBody1);
            cell.AddElement(tableBody2);

            tableIdentity.AddCell(tableIdentity1);
            tableIdentity.AddCell(cell);

            PdfPCell signatureCellAgent = new PdfPCell(tableIdentity); // dont remove
            tableIdentity.ExtendLastRow = false;
            tableIdentity.SpacingAfter = 10f;
            document.Add(tableIdentity);
            #endregion

            #region signature

            if (viewModel.POType == "SALES")
            {

                PdfPTable tableSignatureRegion = new PdfPTable(4);
                tableSignatureRegion.SpacingBefore = 15f;

                cellIdentityContentCenterWithBorder.Phrase = new Phrase("DIBUAT OLEH", normal_font);
                tableSignatureRegion.AddCell(cellIdentityContentCenterWithBorder);
                cellIdentityContentCenterWithBorder.Phrase = new Phrase("MENGETAHUI", normal_font);
                tableSignatureRegion.AddCell(cellIdentityContentCenterWithBorder);
                cellIdentityContentCenterWithBorder.Phrase = new Phrase("MENYETUJUI", normal_font);
                tableSignatureRegion.AddCell(new PdfPCell(cellIdentityContentCenterWithBorder) { Colspan = 2 });

                string signatureArea = string.Empty;
                for (int i = 0; i < 5; i++)
                {
                    signatureArea += Environment.NewLine;
                }


                cellIdentityContentCenterWithBorder.Phrase = new Phrase(signatureArea, normal_font);
                tableSignatureRegion.AddCell(cellIdentityContentCenterWithBorder);
                cellIdentityContentCenterWithBorder.Phrase = new Phrase(signatureArea, normal_font);
                tableSignatureRegion.AddCell(cellIdentityContentCenterWithBorder);
                cellIdentityContentCenterWithBorder.Phrase = new Phrase(signatureArea, normal_font);
                tableSignatureRegion.AddCell(cellIdentityContentCenterWithBorder);
                cellIdentityContentCenterWithBorder.Phrase = new Phrase(signatureArea, normal_font);
                tableSignatureRegion.AddCell(cellIdentityContentCenterWithBorder);


                cellIdentityContentCenterWithBorder.Phrase = new Phrase("PENJUALAN", normal_font);
                tableSignatureRegion.AddCell(cellIdentityContentCenterWithBorder);
                cellIdentityContentCenterWithBorder.Phrase = new Phrase("KABAG PENJUALAN", normal_font);
                tableSignatureRegion.AddCell(cellIdentityContentCenterWithBorder);
                cellIdentityContentCenterWithBorder.Phrase = new Phrase("KABAG D/P", normal_font);
                tableSignatureRegion.AddCell(cellIdentityContentCenterWithBorder);
                cellIdentityContentCenterWithBorder.Phrase = new Phrase("PPIC D/P", normal_font);
                tableSignatureRegion.AddCell(cellIdentityContentCenterWithBorder);

                PdfPCell tableSignatureRegionCell = new PdfPCell(tableSignatureRegion); // dont remove
                tableSignatureRegion.ExtendLastRow = false;
                document.Add(tableSignatureRegion);
            }
            else
            {
                PdfPTable tableSignatureRegion = new PdfPTable(3);
                tableSignatureRegion.SpacingBefore = 15f;

                cellIdentityContentCenterWithBorder.Phrase = new Phrase("DIBUAT OLEH", normal_font);
                tableSignatureRegion.AddCell(cellIdentityContentCenterWithBorder);
                cellIdentityContentCenterWithBorder.Phrase = new Phrase("MENGETAHUI", normal_font);
                tableSignatureRegion.AddCell(cellIdentityContentCenterWithBorder);
                cellIdentityContentCenterWithBorder.Phrase = new Phrase("MENYETUJUI", normal_font);
                tableSignatureRegion.AddCell(cellIdentityContentCenterWithBorder);

                string signatureArea = string.Empty;
                for (int i = 0; i < 5; i++)
                {
                    signatureArea += Environment.NewLine;
                }
               
               
                cellIdentityContentCenterWithBorder.Phrase = new Phrase(signatureArea, normal_font);
                tableSignatureRegion.AddCell(cellIdentityContentCenterWithBorder);
                cellIdentityContentCenterWithBorder.Phrase = new Phrase(signatureArea, normal_font);
                tableSignatureRegion.AddCell(cellIdentityContentCenterWithBorder);
                cellIdentityContentCenterWithBorder.Phrase = new Phrase(signatureArea, normal_font);
                tableSignatureRegion.AddCell(cellIdentityContentCenterWithBorder);
                //tableSignatureRegion.AddCell(cellIdentityContentCenterWithBorder);
                //cellIdentityContentCenterWithBorder.Phrase = new Phrase(signatureArea, normal_font);
                //tableSignatureRegion.AddCell(cellIdentityContentCenterWithBorder);




                cellIdentityContentCenterWithBorder.Phrase = new Phrase("PPIC", normal_font);
                tableSignatureRegion.AddCell(cellIdentityContentCenterWithBorder);
                cellIdentityContentCenterWithBorder.Phrase = new Phrase("PRODUKSI", normal_font);
                tableSignatureRegion.AddCell(cellIdentityContentCenterWithBorder);
                cellIdentityContentCenterWithBorder.Phrase = new Phrase("PIMPINAN PRODUKSI D/P", normal_font);
                tableSignatureRegion.AddCell(cellIdentityContentCenterWithBorder);
                //tableSignatureRegion.AddCell(cellIdentityContentCenterWithBorder);
                //cellIdentityContentCenterWithBorder.Phrase = new Phrase("KABAG PENJUALAN", normal_font);
                    //tableSignatureRegion.AddCell(cellIdentityContentCenterWithBorder);
                PdfPCell tableSignatureRegionCell = new PdfPCell(tableSignatureRegion); // dont remove
                tableSignatureRegion.ExtendLastRow = false;
                document.Add(tableSignatureRegion);
            }
            

            #endregion

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }
    }
}
