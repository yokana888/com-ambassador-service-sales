using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.Spinning;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Globalization;
using System.IO;

namespace Com.Ambassador.Service.Sales.Lib.PDFTemplates
{
    public class SpinningSalesContractModelExportPDFTemplate
    {
        public MemoryStream GeneratePdfTemplate(SpinningSalesContractViewModel viewModel, int timeoffset)
        {
            Font normal_font_9 = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 9);
            Font normal_font_10 = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
            Font bold_font_10 = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10, Font.BOLD);

            Document document = new Document(PageSize.A4, 40, 40, 120, 40);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            document.Open();

            #region customViewModel

            var uom = "";
            double convertion = 0;
            if (viewModel.UomUnit == "BALL")
            {
                uom = "BALES";
                convertion = (viewModel.OrderQuantity) * (181.44);
            }

            var ppn = viewModel.IncomeTax;
            if (ppn == "Include PPn")
            {
                ppn = "Include PPn 10%";
            }

            string QuantityToText = NumberToTextEN.toWords(viewModel.OrderQuantity);
            double amount = Convert.ToDouble((viewModel.Price * convertion).ToString("N2"));
            string AmountToText = NumberToTextEN.toWords(amount);

            var tax = viewModel.IncomeTax == "Include PPn" ? "Include PPn 10%" : viewModel.IncomeTax;

            var detailprice = viewModel.AccountBank.Currency.Symbol + " " + string.Format("{0:n2}", viewModel.Price) + " / KG";

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

            #endregion

            #region Header

            string codeNoString = "FM-PJ-00-03-004";
            Paragraph dateString = new Paragraph($"{codeNoString}\nSukoharjo, {viewModel.CreatedUtc.AddHours(timeoffset).ToString("MMMM dd, yyyy", new CultureInfo("en-US"))}", normal_font_9) { Alignment = Element.ALIGN_RIGHT, Leading  = 10 };
            dateString.SpacingAfter = 5f;
            document.Add(dateString);

            #region Identity


            PdfPTable tableIdentityOpeningLetter = new PdfPTable(2);
            tableIdentityOpeningLetter.SetWidths(new float[] { 15f, 2f });
            PdfPCell cellIdentityContentLeft = new PdfPCell() { Border = Rectangle.NO_BORDER, Padding = 0, HorizontalAlignment = Element.ALIGN_LEFT };
            cellIdentityContentLeft.SetLeading(1.5f, 1);
            PdfPCell cellIdentityContentRight = new PdfPCell() { Border = Rectangle.NO_BORDER, Padding = 0, HorizontalAlignment = Element.ALIGN_RIGHT };
            cellIdentityContentRight.SetLeading(1.5f, 1);

            cellIdentityContentLeft.Phrase = new Phrase($"MESSRS,\n{viewModel.Buyer.Name}\n{viewModel.Buyer.Address}\n{viewModel.Buyer.Country?.ToUpper()}\n{viewModel.Buyer.Contact}", normal_font_10);
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);

            PdfPCell cellIdentityOpeningLetter = new PdfPCell(tableIdentityOpeningLetter); // dont remove
            tableIdentityOpeningLetter.ExtendLastRow = false;
            tableIdentityOpeningLetter.SpacingAfter = 10f;
            document.Add(tableIdentityOpeningLetter);

            #endregion

            string titleString = "SALES CONTRACT";
            Paragraph title = new Paragraph(titleString, bold_font_10) { Alignment = Element.ALIGN_CENTER };
            title.SpacingAfter = 10f;
            document.Add(title);
            #endregion

            string HeaderParagraphString = "On behalf of :";
            Paragraph HeaderParagraph = new Paragraph(HeaderParagraphString, normal_font_9) { Alignment = Element.ALIGN_LEFT, Leading = 11 };
            document.Add(HeaderParagraph);

            string firstParagraphString = "P.T. DAN LIRIS KELURAHAN BANARAN, KECAMATAN GROGOL SUKOHARJO - INDONESIA, we confrm the order under the following terms and conditions as mentioned below: ";
            Paragraph firstParagraph = new Paragraph(firstParagraphString, normal_font_9) { Alignment = Element.ALIGN_LEFT, Leading = 11 };
            firstParagraph.SpacingAfter = 10f;
            document.Add(firstParagraph);

            #region body
            PdfPTable tableBody = new PdfPTable(3);
            tableBody.SetWidths(new float[] { 0.3f, 0.05f, 1.1f });
            PdfPCell bodyContentCenter = new PdfPCell() { Border = Rectangle.NO_BORDER, Padding = 0, HorizontalAlignment = Element.ALIGN_CENTER };
            bodyContentCenter.SetLeading(1.5f, 1);
            PdfPCell bodyContentLeft = new PdfPCell() { Border = Rectangle.NO_BORDER, Padding = 0, HorizontalAlignment = Element.ALIGN_LEFT };
            bodyContentLeft.SetLeading(1.5f, 1);
            PdfPCell bodyContentRight = new PdfPCell() { Border = Rectangle.NO_BORDER, Padding = 0, HorizontalAlignment = Element.ALIGN_RIGHT };
            bodyContentRight.SetLeading(1.5f, 1);
            PdfPCell bodyContentColon = new PdfPCell() { Border = Rectangle.NO_BORDER, Padding = 0, HorizontalAlignment = Element.ALIGN_LEFT };
            bodyContentColon.SetLeading(1.5f, 1);
            bodyContentColon.Phrase = new Phrase(":", normal_font_9);

            bodyContentLeft.Phrase = new Phrase("Contract Number", normal_font_9);
            tableBody.AddCell(bodyContentLeft);
            tableBody.AddCell(bodyContentColon);
            bodyContentLeft.Phrase = new Phrase(viewModel.SalesContractNo, normal_font_9);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Comodity", normal_font_9);
            tableBody.AddCell(bodyContentLeft);
            tableBody.AddCell(bodyContentColon);
            string comodity = viewModel.Comodity.Name;
            if (!string.IsNullOrEmpty(viewModel.ComodityDescription) && !string.IsNullOrWhiteSpace(viewModel.ComodityDescription))
            {
                comodity = comodity + "\n" + viewModel.ComodityDescription;
            }
            bodyContentLeft.Phrase = new Phrase(comodity, normal_font_9);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Quality", normal_font_9);
            tableBody.AddCell(bodyContentLeft);
            tableBody.AddCell(bodyContentColon);
            bodyContentLeft.Phrase = new Phrase(viewModel.Quality.Name, normal_font_9);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Quantity", normal_font_9);
            tableBody.AddCell(bodyContentLeft);
            tableBody.AddCell(bodyContentColon);
            bodyContentLeft.Phrase = new Phrase("ABOUT " + viewModel.OrderQuantity.ToString("N2") + " " + uom + " ( ABOUT : " + convertion.ToString("N2") + " KG) ", normal_font_9);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Price & Payment", normal_font_9);
            tableBody.AddCell(bodyContentLeft);
            tableBody.AddCell(bodyContentColon);
            bodyContentLeft.Phrase = new Phrase(detailprice + "\n" + viewModel.TermOfShipment + "\n" + viewModel.TermOfPayment.Name, normal_font_9);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Amount", normal_font_9);
            tableBody.AddCell(bodyContentLeft);
            tableBody.AddCell(bodyContentColon);
            bodyContentLeft.Phrase = new Phrase(viewModel.AccountBank.Currency.Symbol + " " + string.Format("{0:n2}", amount) + " ( " + AmountToText + " " + viewModel.AccountBank.Currency.Description?.ToUpper() + " ) (APPROXIMATELLY)", normal_font_9);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Shipment", normal_font_9);
            tableBody.AddCell(bodyContentLeft);
            tableBody.AddCell(bodyContentColon);
            bodyContentLeft.Phrase = new Phrase(appx + " " + (viewModel.DeliverySchedule.Value.AddHours(timeoffset).ToString("MMMM yyyy", new CultureInfo("en-US")))?.ToUpper() + "\n " + viewModel.ShipmentDescription, normal_font_9);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Destination", normal_font_9);
            tableBody.AddCell(bodyContentLeft);
            tableBody.AddCell(bodyContentColon);
            bodyContentLeft.Phrase = new Phrase(viewModel.DeliveredTo, normal_font_9);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Packing", normal_font_9);
            tableBody.AddCell(bodyContentLeft);
            tableBody.AddCell(bodyContentColon);
            bodyContentLeft.Phrase = new Phrase(viewModel.Packing, normal_font_9);
            tableBody.AddCell(bodyContentLeft);
            PdfPCell cellBody = new PdfPCell(tableBody); // dont remove
            tableBody.ExtendLastRow = false;
            document.Add(tableBody);

            PdfPTable conditionListBody = new PdfPTable(3);
            conditionListBody.SetWidths(new float[] { 0.3f, 0.05f, 1.1f });

            bodyContentLeft.Phrase = new Phrase("Condition", normal_font_9);
            conditionListBody.AddCell(bodyContentLeft);
            bodyContentCenter.Phrase = new Phrase("-", normal_font_9);
            conditionListBody.AddCell(bodyContentCenter);
            bodyContentLeft.Phrase = new Phrase("THIS CONTRACT IS IRREVOCABLE UNLESS AGREED UPON BY THE TWO PARTIES, THE BUYER AND SELLER.", normal_font_9);
            conditionListBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font_9);
            conditionListBody.AddCell(bodyContentLeft);
            bodyContentCenter.Phrase = new Phrase("-", normal_font_9);
            conditionListBody.AddCell(bodyContentCenter);
            bodyContentLeft.Phrase = new Phrase("+/- " + viewModel.ShippingQuantityTolerance + " % FROM QUANTITY ORDER SHOULD BE ACCEPTABLE.", normal_font_9);
            conditionListBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(" ", normal_font_9);
            conditionListBody.AddCell(bodyContentLeft);
            bodyContentCenter.Phrase = new Phrase("-", normal_font_9);
            conditionListBody.AddCell(bodyContentCenter);
            bodyContentLeft.Phrase = new Phrase("LOCAL CONTAINER DELIVERY CHARGES AT DESTINATION FOR BUYER'S ACCOUNT.", normal_font_9);
            conditionListBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(" ", normal_font_9);
            conditionListBody.AddCell(bodyContentLeft);
            bodyContentCenter.Phrase = new Phrase("- ", normal_font_9);
            conditionListBody.AddCell(bodyContentCenter);
            bodyContentLeft.Phrase = new Phrase(viewModel.Condition, normal_font_9);
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
            cell_signature.Phrase = new Phrase("Accepted and confrmed :", normal_font_9);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("PT DANLIRIS", normal_font_9);
            signature.AddCell(cell_signature);

            cell_signature.Phrase = new Phrase("", normal_font_9);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("", normal_font_9);
            signature.AddCell(cell_signature);

            string signatureArea = string.Empty;
            for (int i = 0; i < 4; i++)
            {
                signatureArea += Environment.NewLine;
            }

            cell_signature.Phrase = new Phrase(signatureArea, normal_font_9);
            signature.AddCell(cell_signature);
            signature.AddCell(cell_signature);

            cell_signature.Phrase = new Phrase("(...........................)", normal_font_9);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("( SRI HENDRATNO )", normal_font_9);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("Authorized signature", normal_font_9);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("Marketing Textile", normal_font_9);
            signature.AddCell(cell_signature);
            cellIdentityContentRight.Phrase = new Phrase("");
            signature.AddCell(cellIdentityContentRight);

            PdfPCell signatureCell = new PdfPCell(signature); // dont remove
            signature.ExtendLastRow = false;
            document.Add(signature);
            #endregion

            #region ConditionPage
            
            string ConditionString = "REMARK :";
            Paragraph ConditionName = new Paragraph(ConditionString, normal_font_9) { Alignment = Element.ALIGN_LEFT };
            document.Add(ConditionName);

            string bulletListSymbol = "\u2022";
            PdfPCell bodyContentJustify = new PdfPCell() { Border = Rectangle.NO_BORDER, Padding = 0, HorizontalAlignment = Element.ALIGN_JUSTIFIED };
            bodyContentJustify.SetLeading(1.5f, 1);

            PdfPTable conditionList = new PdfPTable(2);
            conditionList.SetWidths(new float[] { 0.02f, 1f });

            cellIdentityContentLeft.Phrase = new Phrase(bulletListSymbol, normal_font_9);
            conditionList.AddCell(cellIdentityContentLeft);
            bodyContentJustify.Phrase = new Phrase("All instructions regarding sticker, shipping marks etc. to be received 1 (one) month prior to shipment.", normal_font_9);
            conditionList.AddCell(bodyContentJustify);
            cellIdentityContentLeft.Phrase = new Phrase(bulletListSymbol, normal_font_9);
            conditionList.AddCell(cellIdentityContentLeft);
            bodyContentJustify.Phrase = new Phrase("Benefciary :  P.T. DAN LIRIS KELURAHAN BANARAN, KECAMATAN GROGOL SUKOHARJO - INDONESIA  (Phone No. 0271 - 740888 / 714400). ", normal_font_9);
            conditionList.AddCell(bodyContentJustify);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font_9);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("Payment Transferred to: ", normal_font_9);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font_9);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("PAYMENT TO BE TRANSFERRED TO BANK " + viewModel.AccountBank.BankName, normal_font_9);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font_9);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(viewModel.AccountBank.BankAddress, normal_font_9);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font_9);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("ACCOUNT NAME : " + viewModel.AccountBank.AccountName, normal_font_9);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font_9);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("ACCOUNT NO : " + viewModel.AccountBank.AccountNumber + " SWIFT CODE : " + viewModel.AccountBank.SwiftCode, normal_font_9);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(bulletListSymbol, normal_font_9);
            conditionList.AddCell(cellIdentityContentLeft);
            bodyContentJustify.Phrase = new Phrase(viewModel.TermOfPayment.Name + " to be negotiable with BANK " + viewModel.AccountBank.BankName, normal_font_9);
            conditionList.AddCell(bodyContentJustify);
            cellIdentityContentLeft.Phrase = new Phrase(bulletListSymbol, normal_font_9);
            conditionList.AddCell(cellIdentityContentLeft);
            bodyContentJustify.Phrase = new Phrase("Please find enclosed some Indonesia Banking Regulations.", normal_font_9);
            conditionList.AddCell(bodyContentJustify);
            cellIdentityContentLeft.Phrase = new Phrase(bulletListSymbol, normal_font_9);
            conditionList.AddCell(cellIdentityContentLeft);
            bodyContentJustify.Phrase = new Phrase("If you find anything not order, please let us know immediately.", normal_font_9);
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
                cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font_9);
                agentIdentity.AddCell(cellIdentityContentLeft);
                cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font_9);
                agentIdentity.AddCell(cellIdentityContentLeft);
                cellIdentityContentRight.Phrase = new Phrase($"Sukoharjo, {viewModel.CreatedUtc.AddHours(timeoffset).ToString("MMMM dd, yyyy", new CultureInfo("en-US"))}", normal_font_9);
                agentIdentity.AddCell(cellIdentityContentRight);
                cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font_9);
                agentIdentity.AddCell(cellIdentityContentLeft);
                cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font_9);
                agentIdentity.AddCell(cellIdentityContentLeft);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentity.AddCell(cellIdentityContentRight);
                PdfPCell agentCellIdentity = new PdfPCell(agentIdentity); // dont remove
                agentIdentity.ExtendLastRow = false;
                agentIdentity.SpacingAfter = 10f;
                document.Add(agentIdentity);

                PdfPTable agentIdentityOpeningLetter = new PdfPTable(3);
                agentIdentityOpeningLetter.SetWidths(new float[] { 15f, 1f, 1f });
                cellIdentityContentLeft.Phrase = new Phrase("MESSRS,", normal_font_9);
                agentIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentLeft.Phrase = new Phrase(viewModel.Agent.Name, normal_font_9);
                agentIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentLeft.Phrase = new Phrase(viewModel.Agent.Address, normal_font_9);
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
                cellIdentityContentLeft.Phrase = new Phrase(viewModel.Agent.Country?.ToUpper(), normal_font_9);
                agentIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetter.AddCell(cellIdentityContentRight);
                cellIdentityContentLeft.Phrase = new Phrase(viewModel.Agent.Contact, normal_font_9);
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
                bodyContentCenter.Phrase = new Phrase("COMMISSION AGREEMENT NO: " + viewModel.DispositionNumber, bold_font_10);
                agentIdentityOpeningLetterHeader.AddCell(bodyContentCenter);
                bodyContentCenter.Phrase = new Phrase("FOR SALES CONTRACT NO: " + viewModel.SalesContractNo, bold_font_10);
                agentIdentityOpeningLetterHeader.AddCell(bodyContentCenter);
                cellIdentityContentRight.Phrase = new Phrase("");
                agentIdentityOpeningLetterHeader.AddCell(cellIdentityContentRight);
                PdfPCell agentIdentityOpeningLetterHeaderCell = new PdfPCell(agentIdentityOpeningLetterHeader); // dont remove
                agentIdentityOpeningLetterHeader.ExtendLastRow = false;
                agentIdentityOpeningLetterHeader.SpacingAfter = 10f;
                document.Add(agentIdentityOpeningLetterHeader);

                #endregion

                #region agentBody
                string agentFirstParagraphString = "This is to confirm that your order for " + viewModel.Buyer.Name + " concerning " + viewModel.OrderQuantity.ToString("N2") + " ( " + QuantityToText + ") " + uom + " ( ABOUT: " + convertion.ToString("N2") + " KG ) of ";
                Paragraph agentFirstParagraph = new Paragraph(agentFirstParagraphString, normal_font_9) { Alignment = Element.ALIGN_JUSTIFIED };
                document.Add(agentFirstParagraph);
                string agentFirstParagraphStringName = viewModel.Comodity.Name;
                Paragraph agentFirstParagraphName = new Paragraph(agentFirstParagraphStringName, normal_font_9) { Alignment = Element.ALIGN_JUSTIFIED };
                document.Add(agentFirstParagraphName);
                string agentFirstParagraphStringDescription = viewModel.ComodityDescription;
                Paragraph agentFirstParagraphDescription = new Paragraph(agentFirstParagraphStringDescription, normal_font_9) { Alignment = Element.ALIGN_JUSTIFIED };
                agentFirstParagraphDescription.SpacingAfter = 10f;
                document.Add(agentFirstParagraphDescription);
                string agentSecondParagraphString = "Placed with us, P.T. DAN LIRIS - SOLO INDONESIA, is inclusive of " + viewModel.Comission + " sales commission each KG on " + viewModel.TermOfShipment + " value, payable to you upon final negotiation and clearance of " + viewModel.TermOfPayment.Name + '.';
                Paragraph agentSecondParagraph = new Paragraph(agentSecondParagraphString, normal_font_9) { Alignment = Element.ALIGN_JUSTIFIED };
                agentSecondParagraph.SpacingAfter = 10f;
                document.Add(agentSecondParagraph);
                string agentThirdParagraphString = "Kindly acknowledge receipt by undersigning this Commission Agreement letter and returned one copy to us after having been confirmed and signed by you.";
                Paragraph agentThirdParagraph = new Paragraph(agentThirdParagraphString, normal_font_9) { Alignment = Element.ALIGN_JUSTIFIED };
                agentThirdParagraph.SpacingAfter = 30f;
                document.Add(agentThirdParagraph);
                #endregion

                #region signature
                PdfPTable signatureAgent = new PdfPTable(2);
                signatureAgent.SetWidths(new float[] { 1f, 1f });
                signatureAgent.SetWidths(new float[] { 1f, 1f });
                cell_signature.Phrase = new Phrase("Accepted and confrmed :", normal_font_9);
                signatureAgent.AddCell(cell_signature);
                cell_signature.Phrase = new Phrase("PT DANLIRIS", normal_font_9);
                signatureAgent.AddCell(cell_signature);

                cell_signature.Phrase = new Phrase("", normal_font_9);
                signatureAgent.AddCell(cell_signature);
                cell_signature.Phrase = new Phrase("", normal_font_9);
                signatureAgent.AddCell(cell_signature);

                string signatureAreaAgent = string.Empty;
                for (int i = 0; i < 5; i++)
                {
                    signatureAreaAgent += Environment.NewLine;
                }

                cell_signature.Phrase = new Phrase(signatureArea, normal_font_9);
                signatureAgent.AddCell(cell_signature);
                signatureAgent.AddCell(cell_signature);

                cell_signature.Phrase = new Phrase("(...........................)", normal_font_9);
                signatureAgent.AddCell(cell_signature);
                cell_signature.Phrase = new Phrase("( SRI HENDRATNO )", normal_font_9);
                signatureAgent.AddCell(cell_signature);
                cell_signature.Phrase = new Phrase("Authorized signature", normal_font_9);
                signatureAgent.AddCell(cell_signature);
                cell_signature.Phrase = new Phrase("Marketing Textile", normal_font_9);
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

