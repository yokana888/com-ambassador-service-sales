using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.FinishingPrinting;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.PDFTemplates
{
    public class ShinFinishingPrintingSalesContractExportPDFTemplate
    {
        public MemoryStream GeneratePdfTemplate(ShinFinishingPrintingSalesContractViewModel viewModel, int timeoffset)
        {
            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 18);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);

            Document document = new Document(PageSize.A4, 40, 40, 140, 40);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            document.Open();


            #region customViewModel

            var uom = "";
            var uom1 = "";
            List<string> details = new List<string>();
            //double convertion = 0;

            if (viewModel.UOM.Unit.ToLower() == "yds")
            {
                uom = "YARDS";
                uom1 = "YARD";
            }
            else if (viewModel.UOM.Unit.ToLower() == "mtr")
            {
                uom = "METRES";
                uom1 = "METRE";
            }
            else
            {
                uom = viewModel.UOM.Unit;
                uom1 = viewModel.UOM.Unit;
            }


            string QuantityToText = NumberToTextEN.toWords((double)viewModel.PreSalesContract.OrderQuantity);
            double amount = ((double)viewModel.Amount);
            string AmountToText = NumberToTextEN.toWords(amount);

            //var detailprice = viewModel.AccountBank.Currency.Symbol + " " + string.Format("{0:n2}", viewModel.Price) + " / KG";

            var appx = "";
            var date = viewModel.DeliverySchedule.Value.Day;
            if (date >= 1 && date <= 10)
            {
                appx = "EARLY";
            }
            else if (date >= 11 && date <= 20)
            {
                appx = "MIDDLE";
            }
            else if (date >= 21 && date <= 31)
            {
                appx = "END";
            }

            List<string> newDetail = new List<string>();

            foreach (var i in viewModel.Details)
            {
                //var ppn = "";
                //if ((bool)viewModel.UseIncomeTax)
                //{
                //    if (i.UseIncomeTax)
                //    {
                //        ppn = "INCLUDING PPN 10%";
                //    }
                //    else
                //    {
                //        ppn = "EXCLUDING PPN";
                //    }
                //}
                //else
                //{
                //    ppn = "TANPA PPN";
                //}
                var nominal = string.Format("{0:n2}", i.Price);

                if (i.Currency.Code.ToLower() == "usd")
                {
                    nominal = string.Format("{0:n2}", i.Price);
                }

                details.Add(i.Color + " " + i.Currency.Symbol + " " + nominal + " / " + uom1 + " ");
            }

            #endregion

            #region Header

            string codeNoString = "FM-PJ-00-03-004";
            Paragraph codeNo = new Paragraph(codeNoString, bold_font) { Alignment = Element.ALIGN_RIGHT };
            document.Add(codeNo);
            Paragraph dateString = new Paragraph($"Sukoharjo, {viewModel.CreatedUtc.AddHours(timeoffset).ToString("MMMM dd, yyyy", new CultureInfo("en-US"))}", normal_font) { Alignment = Element.ALIGN_RIGHT };
            dateString.SpacingAfter = 5f;
            document.Add(dateString);

            #region Identity

            //PdfPTable tableIdentity = new PdfPTable(3);
            //tableIdentity.SetWidths(new float[] { 0.5f, 4.5f, 2.5f });

            //cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            //tableIdentity.AddCell(cellIdentityContentLeft);
            //cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            //tableIdentity.AddCell(cellIdentityContentLeft);
            //cellIdentityContentLeft.Phrase = new Phrase($"Sukoharjo, {viewModel.CreatedUtc.AddHours(timeoffset).ToString("dd MMMM yyyy", new CultureInfo("en-US"))}", normal_font);
            //tableIdentity.AddCell(cellIdentityContentLeft);
            //cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            //tableIdentity.AddCell(cellIdentityContentLeft);
            //cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            //tableIdentity.AddCell(cellIdentityContentLeft);
            //cellIdentityContentRight.Phrase = new Phrase("");
            //tableIdentity.AddCell(cellIdentityContentRight);
            //PdfPCell cellIdentity = new PdfPCell(tableIdentity); // dont remove
            //tableIdentity.ExtendLastRow = false;
            //tableIdentity.SpacingAfter = 10f;
            //document.Add(tableIdentity);

            PdfPTable tableIdentityOpeningLetter = new PdfPTable(3);
            tableIdentityOpeningLetter.SetWidths(new float[] { 3f, 1f, 1f });
            PdfPCell cellIdentityContentLeft = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellIdentityContentRight = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            cellIdentityContentLeft.Phrase = new Phrase("MESSRS,", normal_font);
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("", normal_font);
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(viewModel.PreSalesContract.Buyer.Name, normal_font);
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(viewModel.PreSalesContract.Buyer.Address, normal_font);
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            //cellIdentityContentLeft.Phrase = new Phrase(viewModel.Buyer.City, normal_font);
            //tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            //cellIdentityContentLeft.Phrase = new Phrase("");
            //tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            //cellIdentityContentLeft.Phrase = new Phrase("");
            //tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(viewModel.PreSalesContract.Buyer.Country?.ToUpper(), normal_font);
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(viewModel.PreSalesContract.Buyer.Contact, normal_font);
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentRight.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentRight);
            cellIdentityContentRight.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentRight);
            PdfPCell cellIdentityOpeningLetter = new PdfPCell(tableIdentityOpeningLetter); // dont remove
            tableIdentityOpeningLetter.ExtendLastRow = false;
            tableIdentityOpeningLetter.SpacingAfter = 10f;
            document.Add(tableIdentityOpeningLetter);

            #endregion

            string titleString = "SALES CONTRACT";
            Paragraph title = new Paragraph(titleString, bold_font) { Alignment = Element.ALIGN_CENTER };
            title.SpacingAfter = 10f;
            document.Add(title);
            bold_font.SetStyle(Font.NORMAL);

            #endregion



            #region HeaderParagraphString
            string HeaderParagraphString = "On behalf of :";
            Paragraph HeaderParagraph = new Paragraph(HeaderParagraphString, normal_font) { Alignment = Element.ALIGN_LEFT };
            document.Add(HeaderParagraph);

            string firstParagraphString = "P.T. DAN LIRIS KELURAHAN BANARAN, KECAMATAN GROGOL SUKOHARJO - INDONESIA, we confrm the order under the following terms and conditions as mentioned below: ";
            Paragraph firstParagraph = new Paragraph(firstParagraphString, normal_font) { Alignment = Element.ALIGN_JUSTIFIED };
            firstParagraph.SpacingAfter = 10f;
            document.Add(firstParagraph);
            #endregion

            #region body
            PdfPTable tableBody = new PdfPTable(2);
            tableBody.SetWidths(new float[] { 0.75f, 2f });
            PdfPCell bodyContentCenter = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell bodyContentLeft = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell bodyContentRight = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            bodyContentLeft.Phrase = new Phrase("Contract Number", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.SalesContractNo, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Comodity", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.Material.Name, normal_font);
            tableBody.AddCell(bodyContentLeft);

            if (!string.IsNullOrEmpty(viewModel.YarnMaterial.Name) && !string.IsNullOrWhiteSpace(viewModel.YarnMaterial.Name))
            {
                bodyContentLeft.Phrase = new Phrase(" ", normal_font);
                tableBody.AddCell(bodyContentLeft);
                bodyContentLeft.Phrase = new Phrase("  " + viewModel.YarnMaterial.Name, normal_font);
                tableBody.AddCell(bodyContentLeft);
            }

            if (!string.IsNullOrEmpty(viewModel.CommodityDescription) && !string.IsNullOrWhiteSpace(viewModel.CommodityDescription))
            {
                bodyContentLeft.Phrase = new Phrase(" ", normal_font);
                tableBody.AddCell(bodyContentLeft);
                bodyContentLeft.Phrase = new Phrase("  " + viewModel.CommodityDescription, normal_font);
                tableBody.AddCell(bodyContentLeft);
            }

            bodyContentLeft.Phrase = new Phrase("Quality", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.Quality.Name, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Quantity", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.PreSalesContract.OrderQuantity.ToString("N2") + " ( " + QuantityToText + " ) " + uom, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Piece Length", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.PieceLength, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Price & Payment", normal_font);
            tableBody.AddCell(bodyContentLeft);

            int index = 0;

            foreach (var detail in details)
            {
                index++;
                if (index == 1)
                {
                    bodyContentLeft.Phrase = new Phrase(": " + detail, normal_font);
                    tableBody.AddCell(bodyContentLeft);
                }
                else
                {
                    bodyContentLeft.Phrase = new Phrase(" ", normal_font);
                    tableBody.AddCell(bodyContentLeft);
                    bodyContentLeft.Phrase = new Phrase("  " + detail, normal_font);
                    tableBody.AddCell(bodyContentLeft);
                }
            }
            bodyContentLeft.Phrase = new Phrase(" ", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("  " + viewModel.TermOfShipment, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(" ", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("  " + viewModel.TermOfPayment.Name, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Amount", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.AccountBank.Currency.Symbol + " " + string.Format("{0:n2}", amount) + " ( " + AmountToText + " " + viewModel.AccountBank.Currency.Description?.ToUpper() + " ) (APPROXIMATELLY)", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Shipment", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + appx + " " + (viewModel.DeliverySchedule.Value.AddHours(timeoffset).ToString("MMMM yyyy", new CultureInfo("en-US")))?.ToUpper(), normal_font);
            tableBody.AddCell(bodyContentLeft);

            if (!string.IsNullOrEmpty(viewModel.ShipmentDescription) && !string.IsNullOrWhiteSpace(viewModel.ShipmentDescription))
            {
                bodyContentLeft.Phrase = new Phrase(" ", normal_font);
                tableBody.AddCell(bodyContentLeft);
                bodyContentLeft.Phrase = new Phrase("  " + viewModel.ShipmentDescription, normal_font);
                tableBody.AddCell(bodyContentLeft);
            }

            bodyContentLeft.Phrase = new Phrase("Destination", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.DeliveredTo, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Packing", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.Packing, normal_font);
            tableBody.AddCell(bodyContentLeft);
            PdfPCell cellBody = new PdfPCell(tableBody); // dont remove
            tableBody.ExtendLastRow = false;
            document.Add(tableBody);

            PdfPTable conditionListBody = new PdfPTable(3);
            conditionListBody.SetWidths(new float[] { 0.4f, 0.025f, 1f });

            bodyContentLeft.Phrase = new Phrase("Condition", normal_font);
            conditionListBody.AddCell(bodyContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("-", normal_font);
            conditionListBody.AddCell(cellIdentityContentLeft);
            bodyContentLeft.Phrase = new Phrase("THIS CONTRACT IS IRREVOCABLE UNLESS AGREED UPON BY THE TWO PARTIES, THE BUYER AND SELLER.", normal_font);
            conditionListBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            conditionListBody.AddCell(bodyContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("-", normal_font);
            conditionListBody.AddCell(cellIdentityContentLeft);
            bodyContentLeft.Phrase = new Phrase("+/- " + viewModel.ShippingQuantityTolerance + " % FROM QUANTITY ORDER SHOULD BE ACCEPTABLE.", normal_font);
            conditionListBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionListBody.AddCell(bodyContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("-", normal_font);
            conditionListBody.AddCell(cellIdentityContentLeft);
            bodyContentLeft.Phrase = new Phrase("CONTAINER DELIVERY CHARGES AT DESTINATION FOR BUYER'S ACCOUNT.", normal_font);
            conditionListBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionListBody.AddCell(bodyContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("- ", normal_font);
            conditionListBody.AddCell(cellIdentityContentLeft);
            bodyContentLeft.Phrase = new Phrase(viewModel.Condition, normal_font);
            conditionListBody.AddCell(bodyContentLeft);
            bodyContentRight.Phrase = new Phrase("");
            conditionListBody.AddCell(bodyContentRight);
            PdfPCell cellConditionList = new PdfPCell(conditionListBody); // dont remove
            conditionListBody.ExtendLastRow = false;
            conditionListBody.SpacingAfter = 10f;
            document.Add(conditionListBody);

            #endregion

            #region signature
            PdfPTable signature = new PdfPTable(2);
            signature.SetWidths(new float[] { 1f, 1f });
            PdfPCell cell_signature = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 2 };
            signature.SetWidths(new float[] { 1f, 1f });
            cell_signature.Phrase = new Phrase("Accepted and confrmed :", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("PT DANLIRIS", normal_font);
            signature.AddCell(cell_signature);

            cell_signature.Phrase = new Phrase("", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("", normal_font);
            signature.AddCell(cell_signature);

            string signatureArea = string.Empty;
            for (int i = 0; i < 5; i++)
            {
                signatureArea += Environment.NewLine;
            }

            cell_signature.Phrase = new Phrase(signatureArea, normal_font);
            signature.AddCell(cell_signature);
            signature.AddCell(cell_signature);

            cell_signature.Phrase = new Phrase("(...........................)", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("( ROBBY SAPUTRO )", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("Authorized signature", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("Marketing Textile", normal_font);
            signature.AddCell(cell_signature);
            cellIdentityContentRight.Phrase = new Phrase("");
            signature.AddCell(cellIdentityContentRight);

            PdfPCell signatureCell = new PdfPCell(signature); // dont remove
            signature.ExtendLastRow = false;
            signature.SpacingAfter = 10f;
            document.Add(signature);
            #endregion

            #region ConditionPage
            document.NewPage();

            string ConditionString = "Remark";
            Paragraph ConditionName = new Paragraph(ConditionString, header_font) { Alignment = Element.ALIGN_LEFT };
            document.Add(ConditionName);

            string bulletListSymbol = "\u2022";
            PdfPCell bodyContentJustify = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_JUSTIFIED };

            PdfPTable conditionList = new PdfPTable(2);
            conditionList.SetWidths(new float[] { 0.01f, 1f });

            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(bulletListSymbol, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            bodyContentJustify.Phrase = new Phrase("All instructions regarding sticker, shipping marks etc. to be received 1 (one) month prior to shipment.", normal_font);
            conditionList.AddCell(bodyContentJustify);
            //cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            //conditionList.AddCell(cellIdentityContentLeft);
            //cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            //conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(bulletListSymbol, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            bodyContentJustify.Phrase = new Phrase("Benefciary :  P.T. DAN LIRIS KELURAHAN BANARAN, KECAMATAN GROGOL SUKOHARJO - INDONESIA  (Phone No. 0271 - 740888 / 714400). ", normal_font);
            conditionList.AddCell(bodyContentJustify);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("Payment Transferred to: ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("PAYMENT TO BE TRANSFERRED TO BANK " + viewModel.AccountBank.BankName, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);

            if (!string.IsNullOrEmpty(viewModel.AccountBank.BankAddress))
            {
                cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
                conditionList.AddCell(cellIdentityContentLeft);
                cellIdentityContentLeft.Phrase = new Phrase(viewModel.AccountBank.BankAddress, normal_font);
                conditionList.AddCell(cellIdentityContentLeft);
            }

            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("ACCOUNT NAME : " + viewModel.AccountBank.AccountName, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("ACCOUNT NO : " + viewModel.AccountBank.AccountNumber + " SWIFT CODE : " + viewModel.AccountBank.SwiftCode, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(bulletListSymbol, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            bodyContentJustify.Phrase = new Phrase(viewModel.TermOfPayment.Name + " to be negotiable with BANK " + viewModel.AccountBank.BankName, normal_font);
            conditionList.AddCell(bodyContentJustify);
            cellIdentityContentLeft.Phrase = new Phrase(bulletListSymbol, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            bodyContentJustify.Phrase = new Phrase("Please find enclosed some Indonesia Banking Regulations.", normal_font);
            conditionList.AddCell(bodyContentJustify);
            cellIdentityContentLeft.Phrase = new Phrase(bulletListSymbol, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            bodyContentJustify.Phrase = new Phrase("If you find anything not order, please let us know immediately.", normal_font);
            conditionList.AddCell(bodyContentJustify);
            PdfPCell conditionListData = new PdfPCell(conditionList); // dont remove
            conditionList.ExtendLastRow = false;
            document.Add(conditionList);
            #endregion

            #region agentTemplate
            if (viewModel.Agent.Id != 0)
            {
                document.NewPage();

                #region Identity
                PdfPTable agentIdentity = new PdfPTable(3);
                agentIdentity.SetWidths(new float[] { 0.5f, 4.5f, 2.5f });
                cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
                agentIdentity.AddCell(cellIdentityContentLeft);
                cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
                agentIdentity.AddCell(cellIdentityContentLeft);
                cellIdentityContentRight.Phrase = new Phrase($"Sukoharjo, {viewModel.CreatedUtc.AddHours(timeoffset).ToString("MMMM dd, yyyy", new CultureInfo("en-US"))}", normal_font);
                agentIdentity.AddCell(cellIdentityContentRight);
                cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
                agentIdentity.AddCell(cellIdentityContentLeft);
                cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
                agentIdentity.AddCell(cellIdentityContentLeft);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentity.AddCell(cellIdentityContentRight);
                PdfPCell agentCellIdentity = new PdfPCell(agentIdentity); // dont remove
                agentIdentity.ExtendLastRow = false;
                agentIdentity.SpacingAfter = 10f;
                document.Add(agentIdentity);

                PdfPTable agentIdentityOpeningLetter = new PdfPTable(3);
                agentIdentityOpeningLetter.SetWidths(new float[] { 3f, 1f, 1f });
                cellIdentityContentLeft.Phrase = new Phrase("MESSRS,", normal_font);
                agentIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentLeft.Phrase = new Phrase(viewModel.Agent.Name, normal_font);
                agentIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentLeft.Phrase = new Phrase(viewModel.Agent.Address, normal_font);
                agentIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                //if (!string.IsNullOrEmpty(viewModel.Agent.City))
                //{
                //    cellIdentityContentLeft.Phrase = new Phrase(viewModel.Agent.City, normal_font);
                //    agentIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
                //    cellIdentityContentRight.Phrase = new Phrase("");
                //    agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                //    cellIdentityContentRight.Phrase = new Phrase("");
                //    agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                //}
                cellIdentityContentLeft.Phrase = new Phrase(viewModel.Agent.Country, normal_font);
                agentIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentLeft.Phrase = new Phrase(viewModel.Agent.Contact, normal_font);
                agentIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                PdfPCell agentCellIdentityOpeningLetter = new PdfPCell(agentIdentityOpeningLetter); // dont remove
                agentIdentityOpeningLetter.ExtendLastRow = false;
                agentIdentityOpeningLetter.SpacingAfter = 10f;
                document.Add(agentIdentityOpeningLetter);


                PdfPTable agentIdentityOpeningLetterHeader = new PdfPTable(1);
                bodyContentCenter.Phrase = new Phrase("COMMISSION AGREEMENT NO: " + viewModel.DispositionNumber, bold_font);
                agentIdentityOpeningLetterHeader.AddCell(bodyContentCenter);
                bodyContentCenter.Phrase = new Phrase("FOR SALES CONTRACT NO: " + viewModel.SalesContractNo, bold_font);
                agentIdentityOpeningLetterHeader.AddCell(bodyContentCenter);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetterHeader.AddCell(cellIdentityContentRight);
                PdfPCell agentIdentityOpeningLetterHeaderCell = new PdfPCell(agentIdentityOpeningLetterHeader); // dont remove
                agentIdentityOpeningLetterHeader.ExtendLastRow = false;
                agentIdentityOpeningLetterHeader.SpacingAfter = 10f;
                document.Add(agentIdentityOpeningLetterHeader);

                #endregion

                #region agentBody
                string agentFirstParagraphString = "This is to confirm that your order for " + viewModel.PreSalesContract.Buyer.Name + " concerning " + viewModel.PreSalesContract.OrderQuantity + " ( " + QuantityToText + ") " + uom + " of ";
                Paragraph agentFirstParagraph = new Paragraph(agentFirstParagraphString, normal_font) { Alignment = Element.ALIGN_JUSTIFIED };
                document.Add(agentFirstParagraph);
                string agentFirstParagraphStringName = viewModel.Commodity.Name;
                Paragraph agentFirstParagraphName = new Paragraph(agentFirstParagraphStringName, normal_font) { Alignment = Element.ALIGN_JUSTIFIED };
                document.Add(agentFirstParagraphName);
                string agentFirstParagraphStringDescription = viewModel.CommodityDescription;
                Paragraph agentFirstParagraphDescription = new Paragraph(agentFirstParagraphStringDescription, normal_font) { Alignment = Element.ALIGN_JUSTIFIED };
                document.Add(agentFirstParagraphDescription);
                string agentFirstParagraphStringContruction = "CONSTRUCTION: " + viewModel.Material.Name  + " / " + viewModel.YarnMaterial.Name;
                Paragraph agentFirstParagraphContruction = new Paragraph(agentFirstParagraphStringContruction, normal_font) { Alignment = Element.ALIGN_JUSTIFIED };
                agentFirstParagraphContruction.SpacingAfter = 10f;
                document.Add(agentFirstParagraphContruction);
                string agentSecondParagraphString = "Placed with us, P.T. DAN LIRIS - SOLO INDONESIA, is inclusive of " + viewModel.Commission + " sales commission each " + uom1 + " on " + viewModel.TermOfShipment + " value, payable to you upon final negotiation and clearance of " + viewModel.TermOfPayment.Name + '.';
                Paragraph agentSecondParagraph = new Paragraph(agentSecondParagraphString, normal_font) { Alignment = Element.ALIGN_JUSTIFIED };
                agentSecondParagraph.SpacingAfter = 10f;
                document.Add(agentSecondParagraph);
                string agentThirdParagraphString = "Kindly acknowledge receipt by undersigning this Commission Agreement letter and returned one copy to us after having been confirmed and signed by you.";
                Paragraph agentThirdParagraph = new Paragraph(agentThirdParagraphString, normal_font) { Alignment = Element.ALIGN_JUSTIFIED };
                agentThirdParagraph.SpacingAfter = 30f;
                document.Add(agentThirdParagraph);
                #endregion

                #region signature
                PdfPTable signatureAgent = new PdfPTable(2);
                signatureAgent.SetWidths(new float[] { 1f, 1f });
                signatureAgent.SetWidths(new float[] { 1f, 1f });
                cell_signature.Phrase = new Phrase("Accepted and confrmed :", normal_font);
                signatureAgent.AddCell(cell_signature);
                cell_signature.Phrase = new Phrase("PT DANLIRIS", normal_font);
                signatureAgent.AddCell(cell_signature);

                cell_signature.Phrase = new Phrase("", normal_font);
                signatureAgent.AddCell(cell_signature);
                cell_signature.Phrase = new Phrase("", normal_font);
                signatureAgent.AddCell(cell_signature);

                string signatureAreaAgent = string.Empty;
                for (int i = 0; i < 5; i++)
                {
                    signatureAreaAgent += Environment.NewLine;
                }

                cell_signature.Phrase = new Phrase(signatureArea, normal_font);
                signatureAgent.AddCell(cell_signature);
                signatureAgent.AddCell(cell_signature);

                cell_signature.Phrase = new Phrase("(...........................)", normal_font);
                signatureAgent.AddCell(cell_signature);
                cell_signature.Phrase = new Phrase("( ROBBY SAPUTRO )", normal_font);
                signatureAgent.AddCell(cell_signature);
                cell_signature.Phrase = new Phrase("Authorized signature", normal_font);
                signatureAgent.AddCell(cell_signature);
                cell_signature.Phrase = new Phrase("Marketing Textile", normal_font);
                signatureAgent.AddCell(cell_signature);
                cellIdentityContentRight.Phrase = new Phrase("");
                signatureAgent.AddCell(cellIdentityContentRight);

                PdfPCell signatureCellAgent = new PdfPCell(signatureAgent); // dont remove
                signatureAgent.ExtendLastRow = false;
                signatureAgent.SpacingAfter = 10f;
                document.Add(signatureAgent);
            }
            #endregion

            #endregion



            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }
    }
}
