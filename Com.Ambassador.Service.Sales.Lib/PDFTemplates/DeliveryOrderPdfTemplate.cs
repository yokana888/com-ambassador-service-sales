using Com.Ambassador.Service.Sales.Lib.ViewModels.SalesInvoice;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Globalization;
using System.IO;

namespace Com.Ambassador.Service.Sales.Lib.PDFTemplates
{
    public class DeliveryOrderPdfTemplate
    {
        public MemoryStream GeneratePdfTemplate(SalesInvoiceViewModel viewModel, int clientTimeZoneOffset)
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

            PdfPTable headerTable = new PdfPTable(2);
            headerTable.SetWidths(new float[] { 10f, 10f });
            headerTable.WidthPercentage = 100;
            PdfPTable headerTable1 = new PdfPTable(1);
            PdfPTable headerTable2 = new PdfPTable(1);
            PdfPTable headerTable3 = new PdfPTable(2);
            headerTable3.SetWidths(new float[] { 40f, 70f });
            headerTable3.WidthPercentage = 50;
            PdfPTable headerTable4 = new PdfPTable(2);
            headerTable4.SetWidths(new float[] { 25f, 40f });
            headerTable4.WidthPercentage = 50;

            PdfPCell cellHeader1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeader2 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeader3 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeader4 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeaderBody = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeaderBody2 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeaderCS2 = new PdfPCell() { Border = Rectangle.NO_BORDER, Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER };

            cellHeaderBody.Phrase = new Phrase("PT. DAN LIRIS", Title_bold_font);
            headerTable1.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("Head Office : Jl. Merapi No. 23 Banaran, Grogol", normal_font);
            headerTable1.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("Sukoharjo, 57552 Central Java, Indonesia", normal_font);
            headerTable1.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("", normal_font);
            headerTable1.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("Telp  :(+62 271) 740888, 714400", normal_font);
            headerTable1.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("Fax  :(+62 271) 740777, 735222", normal_font);
            headerTable1.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("PO BOX 116 Solo, 57100", normal_font);
            headerTable1.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("Web: www.Ambassador.com", normal_font);
            headerTable1.AddCell(cellHeaderBody);

            cellHeader1.AddElement(headerTable1);
            headerTable.AddCell(cellHeader1);

            cellHeaderBody2.HorizontalAlignment = Element.ALIGN_RIGHT;
            cellHeaderBody2.Phrase = new Phrase("FM-PJ-00-03-008", bold_font);
            headerTable2.AddCell(cellHeaderBody2);

            cellHeader2.AddElement(headerTable2);
            headerTable.AddCell(cellHeader2);

            cellHeaderCS2.Phrase = new Phrase("SURAT JALAN", header_font);
            headerTable.AddCell(cellHeaderCS2);
            cellHeaderCS2.Phrase = new Phrase("No. " + viewModel.DeliveryOrderNo, bold_font);
            headerTable.AddCell(cellHeaderCS2);
            cellHeaderCS2.Phrase = new Phrase("", normal_font);
            headerTable.AddCell(cellHeaderCS2);


            cellHeaderBody.HorizontalAlignment = Element.ALIGN_LEFT;

            cellHeaderBody.Phrase = new Phrase("Kepada Yth. ", normal_font);
            headerTable3.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase(": " + viewModel.Buyer.Name, normal_font);
            headerTable3.AddCell(cellHeaderBody);

            cellHeaderBody.Phrase = new Phrase("Alamat", normal_font);
            headerTable3.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase(": " + viewModel.Buyer.Address, normal_font);
            headerTable3.AddCell(cellHeaderBody);

            cellHeaderBody.Phrase = new Phrase("", normal_font);
            headerTable3.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("", normal_font);
            headerTable3.AddCell(cellHeaderBody);

            cellHeader3.AddElement(headerTable3);
            headerTable.AddCell(cellHeader3);


            cellHeaderBody.Phrase = new Phrase("No. Fakt./Inv.", normal_font);
            headerTable4.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase($": {viewModel.SalesInvoiceType}{viewModel.AutoIncreament.ToString().PadLeft(6, '0')}", normal_font);
            headerTable4.AddCell(cellHeaderBody);

            cellHeaderBody.Phrase = new Phrase("Tgl. Fakt./Inv.", normal_font);
            headerTable4.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase(": " + viewModel.SalesInvoiceDate?.AddHours(clientTimeZoneOffset).ToString("dd MMMM yyyy", new CultureInfo("id-ID")), normal_font);
            headerTable4.AddCell(cellHeaderBody);

            cellHeaderBody.Phrase = new Phrase("", normal_font);
            headerTable4.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("", normal_font);
            headerTable4.AddCell(cellHeaderBody);

            cellHeader4.AddElement(headerTable4);
            headerTable.AddCell(cellHeader4);

            cellHeaderCS2.Phrase = new Phrase("", normal_font);
            headerTable.AddCell(cellHeaderCS2);

            document.Add(headerTable);

            #endregion Header

            int index = 1;

            #region Body

            PdfPTable bodyTable = new PdfPTable(4);
            PdfPCell bodyCell = new PdfPCell();

            float[] widthsBody = new float[] { 3f, 15f, 7f, 8f };
            bodyTable.SetWidths(widthsBody);
            bodyTable.WidthPercentage = 100;

            bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyCell.Phrase = new Phrase("No.", bold_font);
            bodyTable.AddCell(bodyCell);

            bodyCell.Phrase = new Phrase("Nama Barang", bold_font);
            bodyTable.AddCell(bodyCell);

            bodyCell.Phrase = new Phrase("Jumlah", bold_font);
            bodyTable.AddCell(bodyCell);

            bodyCell.Phrase = new Phrase("Keterangan", bold_font);
            bodyTable.AddCell(bodyCell);

            foreach (var detail in viewModel.SalesInvoiceDetails)
            {
                foreach (var item in detail.SalesInvoiceItems)
                {
                    bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    bodyCell.VerticalAlignment = Element.ALIGN_TOP;
                    bodyCell.Phrase = new Phrase((index++).ToString(), normal_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    bodyCell.Phrase = new Phrase(item.ProductName, normal_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    bodyCell.Phrase = new Phrase(item.QuantityItem.GetValueOrDefault().ToString("N2") + " " + item.ItemUom, normal_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    bodyCell.Phrase = new Phrase(item.QuantityPacking.GetValueOrDefault().ToString("N2"), normal_font);
                    bodyTable.AddCell(bodyCell);
                }
            }
            document.Add(bodyTable);

            #endregion Body

            #region Footer

            PdfPTable footerTable = new PdfPTable(1);
            PdfPCell cellFooterLeft = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            footerTable.WidthPercentage = 100; 

            cellFooterLeft.Phrase = new Phrase("", normal_font);
            footerTable.AddCell(cellFooterLeft);
            cellFooterLeft.Phrase = new Phrase("", normal_font);
            footerTable.AddCell(cellFooterLeft);

            cellFooterLeft.Phrase = new Phrase("Catatan : " + viewModel.Remark, bold_font);
            footerTable.AddCell(cellFooterLeft);

            cellFooterLeft.Phrase = new Phrase("", normal_font);
            footerTable.AddCell(cellFooterLeft);
            cellFooterLeft.Phrase = new Phrase("", normal_font);
            footerTable.AddCell(cellFooterLeft);
            cellFooterLeft.Phrase = new Phrase("", normal_font);
            footerTable.AddCell(cellFooterLeft);

            cellFooterLeft.Phrase = new Phrase("Kami harap Surat Jalan ini dikirim kembali kepada kami. Terima kasih.\n\n\n", normal_font);
            footerTable.AddCell(cellFooterLeft);

            cellFooterLeft.Phrase = new Phrase("", normal_font);
            footerTable.AddCell(cellFooterLeft);

            PdfPTable signatureTable = new PdfPTable(3);
            PdfPCell signatureCell = new PdfPCell() { HorizontalAlignment = Element.ALIGN_CENTER };

            var DateTimeNow = DateTimeOffset.Now;

            signatureCell.Phrase = new Phrase("\nPenerima", normal_font);
            signatureTable.AddCell(signatureCell);
            signatureCell.Phrase = new Phrase("\nAngkutan", normal_font);
            signatureTable.AddCell(signatureCell);
            signatureCell.Phrase = new Phrase("Sukoharjo, " + DateTimeNow.AddHours(clientTimeZoneOffset).ToString("dd MMMM yyyy", new CultureInfo("id-ID")) + "\nHormat kami\nDiv. Pemasaran Textile", normal_font);
            signatureTable.AddCell(signatureCell);

            signatureTable.AddCell(new PdfPCell()
            {
                Phrase = new Phrase("---------------------------------", normal_font),
                FixedHeight = 40,
                VerticalAlignment = Element.ALIGN_BOTTOM,
                HorizontalAlignment = Element.ALIGN_CENTER
            }); signatureTable.AddCell(new PdfPCell()
            {
                Phrase = new Phrase("---------------------------------", normal_font),
                FixedHeight = 40,
                VerticalAlignment = Element.ALIGN_BOTTOM,
                HorizontalAlignment = Element.ALIGN_CENTER
            }); signatureTable.AddCell(new PdfPCell()
            {
                Phrase = new Phrase("---------------------------------", normal_font),
                FixedHeight = 40,
                VerticalAlignment = Element.ALIGN_BOTTOM,
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            footerTable.AddCell(new PdfPCell(signatureTable));


            cellFooterLeft.Phrase = new Phrase("", bold_font);
            footerTable.AddCell(cellFooterLeft);
            cellFooterLeft.Phrase = new Phrase("Catatan :", bold_font);
            footerTable.AddCell(cellFooterLeft);
            cellFooterLeft.Phrase = new Phrase("Packing diterima dalam keadaan baik/rusak.", normal_font);
            footerTable.AddCell(cellFooterLeft);
            cellFooterLeft.Phrase = new Phrase("Jumlah barang sesuai/tidak sesuai.", normal_font);
            footerTable.AddCell(cellFooterLeft);

            cellFooterLeft.Phrase = new Phrase("", normal_font);
            footerTable.AddCell(cellFooterLeft);
            document.Add(footerTable);

            #endregion Footer

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }
    }
}
