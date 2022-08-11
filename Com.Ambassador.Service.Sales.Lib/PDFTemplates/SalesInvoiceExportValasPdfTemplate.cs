using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.SalesInvoiceExport;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

namespace Com.Ambassador.Service.Sales.Lib.PDFTemplates
{
    public class SalesInvoiceExportValasPdfTemplate
    {
        public MemoryStream GeneratePdfTemplate(SalesInvoiceExportViewModel viewModel, int clientTimeZoneOffset)
        {
            const int MARGIN = 15;

            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 18);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font Title_bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);

            Document document = new Document(PageSize.A4, MARGIN, MARGIN, MARGIN, MARGIN);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            document.Open();

            #region Header

            #region Header_A
            PdfPTable headerTable_A = new PdfPTable(3);
            PdfPTable headerTable_A1 = new PdfPTable(1);
            PdfPTable headerTable_A2 = new PdfPTable(1);
            PdfPTable headerTable_A3 = new PdfPTable(1);
            headerTable_A.SetWidths(new float[] { 10f, 10f, 10f });
            headerTable_A.WidthPercentage = 100;

            PdfPCell cellHeaderBody_A = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeader_A1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeader_A2 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeader_A3 = new PdfPCell() { Border = Rectangle.NO_BORDER };

            cellHeaderBody_A.Phrase = new Phrase("\n", normal_font);
            headerTable_A1.AddCell(cellHeaderBody_A);

            cellHeaderBody_A.Phrase = new Phrase("INVOICE NO : " + viewModel.SalesInvoiceNo, normal_font);
            headerTable_A1.AddCell(cellHeaderBody_A);

            cellHeader_A1.AddElement(headerTable_A1);
            headerTable_A.AddCell(cellHeader_A1);

            cellHeaderBody_A.Phrase = new Phrase("\n", normal_font);
            headerTable_A2.AddCell(cellHeaderBody_A);

            cellHeaderBody_A.Phrase = new Phrase("DATE : " + viewModel.SalesInvoiceDate?.AddHours(clientTimeZoneOffset).ToString("dd MMMM yyyy", new CultureInfo("id-ID")), normal_font);
            headerTable_A2.AddCell(cellHeaderBody_A);

            cellHeader_A2.AddElement(headerTable_A2);
            headerTable_A.AddCell(cellHeader_A2);

            cellHeaderBody_A.Phrase = new Phrase("FM-PJ-00-03-006", normal_font);
            headerTable_A3.AddCell(cellHeaderBody_A);

            cellHeaderBody_A.Phrase = new Phrase("Page : " + "?????" + " of " + "?????", normal_font);
            headerTable_A3.AddCell(cellHeaderBody_A);

            cellHeader_A3.AddElement(headerTable_A3);
            headerTable_A.AddCell(cellHeader_A3);

            document.Add(headerTable_A);
            #endregion Header_A

            #region Header_B
            PdfPTable headerTable_B = new PdfPTable(2);
            PdfPTable headerTable_B1 = new PdfPTable(1);
            PdfPTable headerTable_B2 = new PdfPTable(1);
            headerTable_B.SetWidths(new float[] { 10f, 10f });
            headerTable_B.WidthPercentage = 100;

            PdfPCell cellHeaderBody_B = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeader_B1 = new PdfPCell();
            PdfPCell cellHeader_B2 = new PdfPCell();

            cellHeaderBody_B.Phrase = new Phrase("SOLD BY ORDERS AND FOR ACCOUNT AND RISK OF MESSRS : " + viewModel.BuyerName + "\n" + viewModel.BuyerAddress, normal_font);
            headerTable_B1.AddCell(cellHeaderBody_B);

            cellHeader_B1.AddElement(headerTable_B1);
            headerTable_B.AddCell(cellHeader_B1);

            foreach (var detail in viewModel.SalesInvoiceExportDetails)
            {
                cellHeaderBody_B.Phrase = new Phrase("CONTRACT NO : " + detail.ContractNo, normal_font);
                headerTable_B2.AddCell(cellHeaderBody_B);
            }

            cellHeaderBody_B.Phrase = new Phrase("SHIPPED PER : " + viewModel.ShippedPer, normal_font);
            headerTable_B2.AddCell(cellHeaderBody_B);

            cellHeaderBody_B.Phrase = new Phrase("SAILING ON OR ABOUT : " + viewModel.SailingDate?.AddHours(clientTimeZoneOffset).ToString("dd MMMM yyyy", new CultureInfo("id-ID")), normal_font);
            headerTable_B2.AddCell(cellHeaderBody_B);

            cellHeaderBody_B.Phrase = new Phrase("FROM : " + viewModel.From, normal_font);
            headerTable_B2.AddCell(cellHeaderBody_B);

            cellHeaderBody_B.Phrase = new Phrase("TO : " + viewModel.To, normal_font);
            headerTable_B2.AddCell(cellHeaderBody_B);

            cellHeader_B2.AddElement(headerTable_B2);
            headerTable_B.AddCell(cellHeader_B2);

            document.Add(headerTable_B);
            #endregion Header_B

            #region Header_C
            PdfPTable headerTable_C = new PdfPTable(2);
            PdfPTable headerTable_C1 = new PdfPTable(1);
            PdfPTable headerTable_C2 = new PdfPTable(1);
            headerTable_C.SetWidths(new float[] { 10f, 10f });
            headerTable_C.WidthPercentage = 100;

            PdfPCell cellHeaderBody_C = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeader_C1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeader_C2 = new PdfPCell() { Border = Rectangle.NO_BORDER };

            cellHeaderBody_C.Phrase = new Phrase("\n", normal_font);
            headerTable_C1.AddCell(cellHeaderBody_C);

            cellHeaderBody_C.Phrase = new Phrase("LETTER OF CREDIT NUMBER  : " + viewModel.LetterOfCreditNumber, normal_font);
            headerTable_C1.AddCell(cellHeaderBody_C);

            cellHeaderBody_C.Phrase = new Phrase("ISSUED BY  : " + viewModel.IssuedBy, normal_font);
            headerTable_C1.AddCell(cellHeaderBody_C);

            cellHeader_C1.AddElement(headerTable_C1);
            headerTable_C.AddCell(cellHeader_C1);

            cellHeaderBody_C.Phrase = new Phrase("\n", normal_font);
            headerTable_C2.AddCell(cellHeaderBody_C);

            cellHeaderBody_C.Phrase = new Phrase("DATE : " + viewModel.LCDate?.AddHours(clientTimeZoneOffset).ToString("dd MMMM yyyy", new CultureInfo("id-ID")), normal_font);
            headerTable_C2.AddCell(cellHeaderBody_C);

            cellHeaderBody_C.Phrase = new Phrase("", normal_font);
            headerTable_C1.AddCell(cellHeaderBody_C);

            cellHeader_C2.AddElement(headerTable_C2);
            headerTable_C.AddCell(cellHeader_C2);

            document.Add(headerTable_C);
            #endregion Header_C

            #endregion Header

            #region Body

            #region Body_A
            PdfPTable bodyTable_A = new PdfPTable(4);
            PdfPCell bodyCell_A = new PdfPCell();

            float[] widthsBody = new float[] { 20f, 8f, 8f, 10f };
            bodyTable_A.SetWidths(widthsBody);
            bodyTable_A.WidthPercentage = 100;

            bodyCell_A.HorizontalAlignment = Element.ALIGN_CENTER;

            bodyCell_A.Phrase = new Phrase("DESCRIPTION", bold_font);
            bodyTable_A.AddCell(bodyCell_A);

            bodyCell_A.Phrase = new Phrase("QUANTITY IN METERS", bold_font);
            bodyTable_A.AddCell(bodyCell_A);

            bodyCell_A.Phrase = new Phrase("UNIT PRICE USD", bold_font);
            bodyTable_A.AddCell(bodyCell_A);

            bodyCell_A.Phrase = new Phrase("TOTAL PRICE USD", bold_font);
            bodyTable_A.AddCell(bodyCell_A);

            double totalPrice = 0;
            double grandTotalPrice = 0;
            double totalLength = 0;

            foreach (var detail in viewModel.SalesInvoiceExportDetails)
            {
                //TAMBAHIN DESCRIPTION
                bodyCell_A.HorizontalAlignment = Element.ALIGN_LEFT;

                bodyCell_A.Phrase = new Phrase(detail.Description, normal_font);
                bodyTable_A.AddCell(bodyCell_A);

                bodyCell_A.Phrase = new Phrase("", normal_font);
                bodyTable_A.AddCell(bodyCell_A);

                bodyCell_A.Phrase = new Phrase("", normal_font);
                bodyTable_A.AddCell(bodyCell_A);

                bodyCell_A.Phrase = new Phrase("", normal_font);
                bodyTable_A.AddCell(bodyCell_A);

                foreach (var item in detail.SalesInvoiceExportItems)
                {
                    totalPrice = item.QuantityItem.GetValueOrDefault() * item.Price.GetValueOrDefault();
                    grandTotalPrice += totalPrice;
                    totalLength += item.QuantityItem.GetValueOrDefault();

                    bodyCell_A.HorizontalAlignment = Element.ALIGN_LEFT;
                    bodyCell_A.Phrase = new Phrase(item.ProductName, normal_font);
                    bodyTable_A.AddCell(bodyCell_A);

                    bodyCell_A.HorizontalAlignment = Element.ALIGN_CENTER;
                    bodyCell_A.Phrase = new Phrase(string.Format("{0:n2}", item.QuantityItem), normal_font);
                    bodyTable_A.AddCell(bodyCell_A);

                    bodyCell_A.Phrase = new Phrase(string.Format("{0:n0}", item.Price), normal_font);
                    bodyTable_A.AddCell(bodyCell_A);

                    bodyCell_A.Phrase = new Phrase(totalPrice.ToString("N2"), normal_font);
                    bodyTable_A.AddCell(bodyCell_A);
                }
            }


            bodyCell_A.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyCell_A.Phrase = new Phrase("TOTAL", bold_font);
            bodyTable_A.AddCell(bodyCell_A);

            bodyCell_A.Phrase = new Phrase(totalLength.ToString("N2"), bold_font);
            bodyTable_A.AddCell(bodyCell_A);

            bodyCell_A.Phrase = new Phrase(".............", bold_font);
            bodyTable_A.AddCell(bodyCell_A);

            bodyCell_A.Phrase = new Phrase(grandTotalPrice.ToString("N2"), bold_font);
            bodyTable_A.AddCell(bodyCell_A);

            document.Add(bodyTable_A);
            #endregion Body_A

            double convert = ((double)grandTotalPrice);
            string ENText = NumberToTextEN.toWords(convert);

            #region Body_B
            PdfPTable bodyTable_B = new PdfPTable(2);
            PdfPTable bodyTable_B1 = new PdfPTable(1);
            PdfPTable bodyTable_B2 = new PdfPTable(1);
            bodyTable_B.SetWidths(new float[] { 10f, 10f });
            bodyTable_B.WidthPercentage = 100;

            PdfPCell cellBody_B = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell bodyCell_A1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell bodyCell_A2 = new PdfPCell() { Border = Rectangle.NO_BORDER };

            cellBody_B.Phrase = new Phrase("", normal_font);
            bodyTable_B1.AddCell(cellBody_B);

            cellBody_B.Phrase = new Phrase("SAY : " + ENText + "Dollar", normal_font);
            bodyTable_B1.AddCell(cellBody_B);

            cellBody_B.Phrase = new Phrase("SHIPPING MARKS : " + viewModel.ShippingRemark, normal_font);
            bodyTable_B1.AddCell(cellBody_B);

            bodyCell_A1.AddElement(bodyTable_B1);
            bodyTable_B.AddCell(bodyCell_A1);

            cellBody_B.Phrase = new Phrase("", normal_font);
            bodyTable_B2.AddCell(cellBody_B);

            cellBody_B.Phrase = new Phrase(viewModel.TermOfPaymentType + " " + viewModel.TermOfPaymentRemark, normal_font);
            bodyTable_B2.AddCell(cellBody_B);

            cellBody_B.Phrase = new Phrase("REMARKS : " + viewModel.Remark, normal_font);
            bodyTable_B2.AddCell(cellBody_B);

            bodyCell_A2.AddElement(bodyTable_B2);
            bodyTable_B.AddCell(bodyCell_A2);

            document.Add(bodyTable_B);
            #endregion Body_B

            #endregion Body

            #region Footer

            #region Footer_A
            PdfPTable footerTable_A = new PdfPTable(1);
            PdfPTable footerTable_A1 = new PdfPTable(2);
            footerTable_A1.SetWidths(new float[] { 10f, 40f });
            footerTable_A1.WidthPercentage = 80;

            PdfPCell cellFooter_A = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell footerCell_A1 = new PdfPCell() { Border = Rectangle.NO_BORDER };

            footerTable_A.HorizontalAlignment = Element.ALIGN_LEFT;
            cellFooter_A.HorizontalAlignment = Element.ALIGN_LEFT;

            cellFooter_A.Phrase = new Phrase("", normal_font);
            footerTable_A1.AddCell(cellFooter_A);
            cellFooter_A.Phrase = new Phrase("", normal_font);
            footerTable_A1.AddCell(cellFooter_A);

            cellFooter_A.Phrase = new Phrase("", normal_font);
            footerTable_A1.AddCell(cellFooter_A);
            cellFooter_A.Phrase = new Phrase("", normal_font);
            footerTable_A1.AddCell(cellFooter_A);

            foreach (var detail in viewModel.SalesInvoiceExportDetails)
            {
                cellFooter_A.Phrase = new Phrase("GROSS WEIGHT", normal_font);
                footerTable_A1.AddCell(cellFooter_A);
                cellFooter_A.Phrase = new Phrase(" :    " + detail.GrossWeight + " " + detail.WeightUom + "S", normal_font);
                footerTable_A1.AddCell(cellFooter_A);

                cellFooter_A.Phrase = new Phrase("NETT WEIGHT", normal_font);
                footerTable_A1.AddCell(cellFooter_A);
                cellFooter_A.Phrase = new Phrase(" :    " + detail.NetWeight + " " + detail.WeightUom + "S", normal_font);
                footerTable_A1.AddCell(cellFooter_A);

                cellFooter_A.Phrase = new Phrase("TOTAL MEASS.", normal_font);
                footerTable_A1.AddCell(cellFooter_A);
                cellFooter_A.Phrase = new Phrase(" :    " + detail.TotalMeas + " " + detail.TotalUom, normal_font);
                footerTable_A1.AddCell(cellFooter_A);
            }

            footerCell_A1.AddElement(footerTable_A1);
            footerTable_A.AddCell(footerCell_A1);

            document.Add(footerTable_A);
            #endregion Footer_A

            #region Footer_B
            PdfPTable footerTable_B = new PdfPTable(1);
            PdfPTable footerTable_B1 = new PdfPTable(1);

            PdfPCell cellFooter_B = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell footerCell_B1 = new PdfPCell() { Border = Rectangle.NO_BORDER };

            footerTable_B.HorizontalAlignment = Element.ALIGN_CENTER;
            cellFooter_B.HorizontalAlignment = Element.ALIGN_RIGHT;

            cellFooter_B.Phrase = new Phrase("\n\n\n ( " + viewModel.Authorized + " )", normal_font);
            footerTable_B1.AddCell(cellFooter_B);

            cellFooter_B.Phrase = new Phrase("AUTHORIZED SIGNATURE", normal_font);
            footerTable_B1.AddCell(cellFooter_B);

            footerCell_B1.AddElement(footerTable_B1);
            footerTable_B.AddCell(footerCell_B1);

            document.Add(footerTable_B);
            #endregion Footer_B

            #endregion Footer

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }
    }
}
