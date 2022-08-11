using Com.Ambassador.Service.Sales.Lib.ViewModels.DOReturn;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Com.Ambassador.Service.Sales.Lib.PDFTemplates
{
    public class DOReturnPdfTemplate
    {
        public MemoryStream GeneratePdfTemplate(DOReturnViewModel viewModel, int clientTimeZoneOffset)
        {
            const int MARGIN = 15;

            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 12);
            Font header_bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 12);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);

            Document document = new Document(PageSize.A5.Rotate(), MARGIN, MARGIN, MARGIN, MARGIN);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            document.Open();

            #region Header
            PdfPTable headerTable = new PdfPTable(2);
            headerTable.SetWidths(new float[] { 10f, 10f });
            headerTable.WidthPercentage = 100;
            PdfPTable headerTable1 = new PdfPTable(1);
            PdfPTable headerTable2 = new PdfPTable(1);

            PdfPCell cellHeader1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeader2 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeaderBody = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeaderCS2 = new PdfPCell() { Border = Rectangle.NO_BORDER, Colspan = 2 };

            cellHeaderBody.Phrase = new Phrase("PT. DANLIRIS", header_font);
            headerTable1.AddCell(cellHeaderBody);

            cellHeaderBody.Phrase = new Phrase("", header_font);
            headerTable1.AddCell(cellHeaderBody);

            cellHeaderBody.Phrase = new Phrase($"No. {viewModel.DOReturnType}{viewModel.AutoIncreament.ToString().PadLeft(6, '0')}", bold_font);
            headerTable1.AddCell(cellHeaderBody);

            cellHeaderBody.Phrase = new Phrase("", header_font);
            headerTable1.AddCell(cellHeaderBody);

            cellHeaderBody.Phrase = new Phrase("Harap dikeluarkan barang tersebut di bawah ini : ", normal_font);
            headerTable1.AddCell(cellHeaderBody);

            cellHeader1.AddElement(headerTable1);
            headerTable.AddCell(cellHeader1);

            cellHeaderBody.Phrase = new Phrase("", normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHeaderBody.Phrase = new Phrase("Sukoharjo, " + viewModel.DOReturnDate?.AddHours(clientTimeZoneOffset).ToString("dd MMMM yyyy", new CultureInfo("id-ID")), normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHeaderBody.Phrase = new Phrase("Kepada", normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHeaderBody.Phrase = new Phrase("Yth. Bpk./Ibu. " + viewModel.HeadOfStorage, normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHeaderBody.Phrase = new Phrase("Bag. Gudang Packing Finishing/Printing", normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHeaderBody.Phrase = new Phrase("D.O. RETUR", bold_font);
            headerTable2.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("", normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHeaderBody.Phrase = new Phrase("Untuk melengkapi Bon No. : ......................................... ", normal_font);
            headerTable2.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("", normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeader2.AddElement(headerTable2);
            headerTable.AddCell(cellHeader2);

            cellHeaderCS2.Phrase = new Phrase("", normal_font);
            headerTable.AddCell(cellHeaderCS2);

            document.Add(headerTable);

            #endregion Header

            #region Body

            int index = 1;

            PdfPTable bodyTable = new PdfPTable(6);
            PdfPCell bodyCell = new PdfPCell();

            float[] widthsBody = new float[] { 3f, 13f, 7f, 7f, 7f, 7f };
            bodyTable.SetWidths(widthsBody);
            bodyTable.WidthPercentage = 100;

            bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyCell.Phrase = new Phrase("No.", bold_font);
            bodyTable.AddCell(bodyCell);

            bodyCell.Phrase = new Phrase("Nama", bold_font);
            bodyTable.AddCell(bodyCell);

            bodyCell.Phrase = new Phrase("Jenis/Code", bold_font);
            bodyTable.AddCell(bodyCell);

            bodyCell.Phrase = new Phrase("Pcs/Roll/Pt", bold_font);
            bodyTable.AddCell(bodyCell);

            bodyCell.Phrase = new Phrase("Mtr/Yds", bold_font);
            bodyTable.AddCell(bodyCell);

            bodyCell.Phrase = new Phrase("Kg/Bale", bold_font);
            bodyTable.AddCell(bodyCell);

            foreach (DOReturnDetailViewModel detail in viewModel.DOReturnDetails)
            {
                var noBon = detail.DOReturnItems.FirstOrDefault().BonNo;

                foreach (DOReturnDetailItemViewModel detailItem in detail.DOReturnDetailItems)
                {
                    bodyCell.Phrase = new Phrase("", normal_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Ex. Faktur : " + detail.SalesInvoice.SalesInvoiceNo + " \nEx. DO : " + detailItem.DOSalesNo + "\nEx. Bon : " + noBon, bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("", normal_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("", normal_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("", normal_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("", normal_font);
                    bodyTable.AddCell(bodyCell);
                }

                foreach (DOReturnItemViewModel item in detail.DOReturnItems)
                {
                    bodyCell.Phrase = new Phrase((index++).ToString(), normal_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase(item.ProductName, normal_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase(item.ProductCode, normal_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase(string.Format("{0:n0}", item.QuantityPacking) + " " + item.PackingUom, normal_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase(item.QuantityItem + " " + item.ItemUom, normal_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("0", normal_font);
                    bodyTable.AddCell(bodyCell);
                }
            }

            document.Add(bodyTable);

            #endregion Body

            #region Footer

            PdfPTable footerTable = new PdfPTable(1);
            PdfPCell cellFooterLeft = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };

            float[] widthsFooter = new float[] { 10f };
            footerTable.SetWidths(widthsFooter);
            footerTable.WidthPercentage = 100;

            cellFooterLeft.Phrase = new Phrase("", normal_font);
            footerTable.AddCell(cellFooterLeft);
            cellFooterLeft.Phrase = new Phrase("", normal_font);
            footerTable.AddCell(cellFooterLeft);

            cellFooterLeft.Phrase = new Phrase("No LTKP : " + viewModel.LTKPNo, normal_font);
            footerTable.AddCell(cellFooterLeft);

            cellFooterLeft.Colspan = 3;
            cellFooterLeft.Phrase = new Phrase("", bold_font);
            footerTable.AddCell(cellFooterLeft);

            cellFooterLeft.Colspan = 3;
            cellFooterLeft.Phrase = new Phrase("Dari bagian / Retur dari : " + viewModel.ReturnFrom, bold_font);
            footerTable.AddCell(cellFooterLeft);

            cellFooterLeft.Colspan = 3;
            cellFooterLeft.Phrase = new Phrase("Keterangan : " + viewModel.Remark, bold_font);
            footerTable.AddCell(cellFooterLeft);

            cellFooterLeft.Colspan = 3;
            cellFooterLeft.Phrase = new Phrase("", bold_font);
            footerTable.AddCell(cellFooterLeft);

            PdfPTable signatureTable = new PdfPTable(3) { HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell signatureCell = new PdfPCell() { HorizontalAlignment = Element.ALIGN_CENTER };

            float[] widthsSignanture = new float[] { 10f, 10f, 10f };
            signatureTable.SetWidths(widthsSignanture);
            signatureTable.WidthPercentage = 100;

            signatureCell.Phrase = new Phrase("Adm.Penjualan", normal_font);
            signatureTable.AddCell(signatureCell);
            signatureCell.Phrase = new Phrase("Gudang", normal_font);
            signatureTable.AddCell(signatureCell);
            signatureCell.Phrase = new Phrase("Terima kasih :\nBagian Penjualan", normal_font);
            signatureTable.AddCell(signatureCell);

            signatureTable.AddCell(new PdfPCell()
            {
                Phrase = new Phrase("--------------------------------", normal_font),
                FixedHeight = 40,
                VerticalAlignment = Element.ALIGN_BOTTOM,
                HorizontalAlignment = Element.ALIGN_CENTER
            }); signatureTable.AddCell(new PdfPCell()
            {
                Phrase = new Phrase("--------------------------------", normal_font),
                FixedHeight = 40,
                VerticalAlignment = Element.ALIGN_BOTTOM,
                HorizontalAlignment = Element.ALIGN_CENTER
            }); signatureTable.AddCell(new PdfPCell()
            {
                Phrase = new Phrase("--------------------------------", normal_font),
                FixedHeight = 40,
                VerticalAlignment = Element.ALIGN_BOTTOM,
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            footerTable.AddCell(new PdfPCell(signatureTable));

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
