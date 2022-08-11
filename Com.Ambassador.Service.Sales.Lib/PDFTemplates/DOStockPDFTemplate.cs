using Com.Ambassador.Service.Sales.Lib.ViewModels.DOStock;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.PDFTemplates
{
    public class DOStockPDFTemplate
    {
        public MemoryStream GeneratePdfTemplate(DOStockViewModel viewModel, int clientTimeZoneOffset)
        {
            const int MARGIN_LEFT_RIGHT = 15;
            const int MARGIN_TOP_BOTTOM = 20;

            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 12);
            Font header_bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 12);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);

            Document document = new Document(PageSize.A4, MARGIN_LEFT_RIGHT, MARGIN_LEFT_RIGHT, MARGIN_TOP_BOTTOM, MARGIN_TOP_BOTTOM);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            document.Open();
            GenerateTemplate(viewModel, clientTimeZoneOffset, document, header_font, header_bold_font, normal_font, bold_font);
           

            //====================================
            #region Divider

            PdfPTable dividerTable = new PdfPTable(1);
            PdfPCell cellDivider = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };

            float[] widthsDivider = new float[] { 10f };
            dividerTable.SetWidths(widthsDivider);
            dividerTable.WidthPercentage = 100;

            cellDivider.Phrase = new Phrase(" ", normal_font);
            dividerTable.AddCell(cellDivider);
            cellDivider.Phrase = new Phrase(" ", normal_font);
            dividerTable.AddCell(cellDivider);
            cellDivider.Phrase = new Phrase(" ", normal_font);
            dividerTable.AddCell(cellDivider);
            cellDivider.Phrase = new Phrase("Gunting disini ..........................................................................................................................................................................................................", normal_font);
            dividerTable.AddCell(cellDivider);
            cellDivider.Phrase = new Phrase(" ", normal_font);
            dividerTable.AddCell(cellDivider);
            cellDivider.Phrase = new Phrase(" ", normal_font);
            dividerTable.AddCell(cellDivider);
            cellDivider.Phrase = new Phrase(" ", normal_font);
            dividerTable.AddCell(cellDivider);
            document.Add(dividerTable);

            #endregion Divider
            //====================================
            GenerateTemplate(viewModel, clientTimeZoneOffset, document, header_font, header_bold_font, normal_font, bold_font);
            

            document.Close();

            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }

        //private void GenerateTemplateExport(DOStockViewModel viewModel, int clientTimeZoneOffset, Document document, Font header_font, Font header_bold_font, Font normal_font, Font bold_font)
        //{
        //    #region Header
        //    PdfPTable headerTable_A = new PdfPTable(2);
        //    PdfPTable headerTable_B = new PdfPTable(1);
        //    PdfPTable headerTable1 = new PdfPTable(1);
        //    PdfPTable headerTable2 = new PdfPTable(1);
        //    PdfPTable headerTable3 = new PdfPTable(3);
        //    PdfPTable headerTable4 = new PdfPTable(2);
        //    headerTable_A.SetWidths(new float[] { 10f, 10f });
        //    headerTable_A.WidthPercentage = 100;
        //    headerTable3.SetWidths(new float[] { 40f, 4f, 100f });
        //    headerTable3.WidthPercentage = 100;
        //    headerTable4.SetWidths(new float[] { 10f, 40f });
        //    headerTable4.WidthPercentage = 100;

        //    PdfPCell cellHeader1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
        //    PdfPCell cellHeader2 = new PdfPCell() { Border = Rectangle.NO_BORDER };
        //    PdfPCell cellHeader3 = new PdfPCell() { Border = Rectangle.NO_BORDER };
        //    PdfPCell cellHeader4 = new PdfPCell() { Border = Rectangle.NO_BORDER };
        //    PdfPCell cellHeaderBody = new PdfPCell() { Border = Rectangle.NO_BORDER };

        //    cellHeaderBody.Phrase = new Phrase("", normal_font);
        //    headerTable1.AddCell(cellHeaderBody);
        //    cellHeaderBody.Phrase = new Phrase($"No. {viewModel.Type}{viewModel.AutoIncreament.ToString().PadLeft(6, '0')}", header_font);
        //    headerTable1.AddCell(cellHeaderBody);
        //    cellHeaderBody.Phrase = new Phrase("", normal_font);
        //    headerTable1.AddCell(cellHeaderBody);
        //    cellHeaderBody.Phrase = new Phrase("", normal_font);
        //    headerTable1.AddCell(cellHeaderBody);

        //    cellHeader1.AddElement(headerTable1);
        //    headerTable_A.AddCell(cellHeader1);

        //    cellHeaderBody.HorizontalAlignment = Element.ALIGN_LEFT;

        //    cellHeaderBody.Phrase = new Phrase("", header_font);
        //    headerTable2.AddCell(cellHeaderBody);
        //    cellHeaderBody.Phrase = new Phrase("KERTAS KERJA EKSPORT", header_bold_font);
        //    headerTable2.AddCell(cellHeaderBody);
        //    cellHeaderBody.Phrase = new Phrase("", header_font);
        //    headerTable2.AddCell(cellHeaderBody);
        //    cellHeaderBody.Phrase = new Phrase("", header_font);
        //    headerTable2.AddCell(cellHeaderBody);

        //    cellHeader2.AddElement(headerTable2);
        //    headerTable_A.AddCell(cellHeader2);

        //    document.Add(headerTable_A);

        //    cellHeaderBody.HorizontalAlignment = Element.ALIGN_LEFT;

        //    cellHeaderBody.Phrase = new Phrase("", bold_font);
        //    headerTable3.AddCell(cellHeaderBody);
        //    cellHeaderBody.Phrase = new Phrase("", bold_font);
        //    headerTable3.AddCell(cellHeaderBody);
        //    cellHeaderBody.Phrase = new Phrase("", bold_font);
        //    headerTable3.AddCell(cellHeaderBody);

        //    cellHeaderBody.Phrase = new Phrase("", bold_font);
        //    headerTable3.AddCell(cellHeaderBody);
        //    cellHeaderBody.Phrase = new Phrase("", bold_font);
        //    headerTable3.AddCell(cellHeaderBody);
        //    cellHeaderBody.Phrase = new Phrase("", bold_font);
        //    headerTable3.AddCell(cellHeaderBody);

        //    cellHeaderBody.Phrase = new Phrase("1. Jenis Untuk ", bold_font);
        //    headerTable3.AddCell(cellHeaderBody);
        //    cellHeaderBody.Phrase = new Phrase(":", bold_font);
        //    headerTable3.AddCell(cellHeaderBody);
        //    cellHeaderBody.Phrase = new Phrase(viewModel.Buyer.Name, normal_font);
        //    headerTable3.AddCell(cellHeaderBody);

        //    double total_length = 0;

        //    foreach (var item in viewModel.DOStockItems)
        //    {
        //        total_length += item.Length;
        //    }

        //    cellHeaderBody.Phrase = new Phrase("2. Jumlah Order ", bold_font);
        //    headerTable3.AddCell(cellHeaderBody);
        //    cellHeaderBody.Phrase = new Phrase(":", bold_font);
        //    headerTable3.AddCell(cellHeaderBody);
        //    cellHeaderBody.Phrase = new Phrase(total_length + " " + viewModel.LengthUom, normal_font);
        //    headerTable3.AddCell(cellHeaderBody);

            

        //    cellHeaderBody.Phrase = new Phrase("CATATAN KETERANGAN ", bold_font);
        //    headerTable3.AddCell(cellHeaderBody);
        //    cellHeaderBody.Phrase = new Phrase(":", bold_font);
        //    headerTable3.AddCell(cellHeaderBody);
        //    cellHeaderBody.Phrase = new Phrase("", bold_font);
        //    headerTable3.AddCell(cellHeaderBody);
        //    //foreach (DOSalesDetailViewModel item in viewModel.DOSalesDetailItems)
        //    //{
        //    //    cellHeaderBody.Phrase = new Phrase("\n" + item.ProductionOrder.OrderNo + " (" + item.ColorRequest + ")", normal_font);
        //    //    //headerTable3.AddCell(cellHeaderBody);
        //    //}
        //    headerTable3.AddCell(cellHeaderBody);
        //    cellHeaderBody.Phrase = new Phrase(" ", normal_font);
        //    headerTable3.AddCell(cellHeaderBody);

        //    cellHeader3.AddElement(headerTable3);
        //    headerTable_B.AddCell(cellHeader3);

        //    cellHeader4.AddElement(headerTable4);
        //    headerTable_B.AddCell(cellHeader4);

        //    document.Add(headerTable_B);

        //    #endregion Header

        //    #region Body

        //    PdfPTable bodyTable = new PdfPTable(1);
        //    PdfPCell bodyCell = new PdfPCell() { Border = Rectangle.NO_BORDER };

        //    float[] widthsBody = new float[] { 1f };
        //    bodyTable.SetWidths(widthsBody);
        //    bodyTable.WidthPercentage = 80;

        //    foreach (DOSalesDetailViewModel item in viewModel.DOSalesDetailItems)
        //    {
        //        bodyCell.HorizontalAlignment = Element.ALIGN_LEFT;
        //        bodyCell.VerticalAlignment = Element.ALIGN_MIDDLE;

        //        bodyCell.Phrase = new Phrase(item.ProductionOrder.OrderNo + " " + item.ColorRequest, bold_font);
        //        bodyTable.AddCell(bodyCell);
        //    }
        //    bodyCell.Phrase = new Phrase("", bold_font);
        //    bodyTable.AddCell(bodyCell);
        //    bodyCell.Phrase = new Phrase("", bold_font);
        //    bodyTable.AddCell(bodyCell);
        //    bodyCell.Phrase = new Phrase("", bold_font);
        //    bodyTable.AddCell(bodyCell);
        //    bodyCell.Phrase = new Phrase(viewModel.Remark, bold_font);
        //    bodyTable.AddCell(bodyCell);
        //    bodyCell.Phrase = new Phrase("", bold_font);
        //    bodyTable.AddCell(bodyCell);
        //    bodyCell.Phrase = new Phrase("", bold_font);
        //    bodyTable.AddCell(bodyCell);
        //    bodyCell.Phrase = new Phrase("", bold_font);
        //    bodyTable.AddCell(bodyCell);
        //    bodyCell.Phrase = new Phrase("", bold_font);
        //    bodyTable.AddCell(bodyCell);
        //    bodyCell.Phrase = new Phrase("", bold_font);
        //    bodyTable.AddCell(bodyCell);

        //    document.Add(bodyTable);

        //    #endregion Body

        //    #region Footer

        //    PdfPTable footerTable = new PdfPTable(1);

        //    PdfPTable signatureTable = new PdfPTable(2) { HorizontalAlignment = Element.ALIGN_CENTER };
        //    PdfPCell signatureCell = new PdfPCell() { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };

        //    float[] widthsSignanture = new float[] { 10f, 10f };
        //    signatureTable.SetWidths(widthsSignanture);
        //    signatureTable.WidthPercentage = 80;

        //    signatureCell.Phrase = new Phrase("Diterima Tgl. ......................", normal_font);
        //    signatureTable.AddCell(signatureCell);
        //    signatureCell.Phrase = new Phrase("Sukoharjo, " + viewModel.Date?.AddHours(clientTimeZoneOffset).ToString("dd MMMM yyyy", new CultureInfo("id-ID")) + "\n\nParaf Pembuat,", normal_font);
        //    signatureTable.AddCell(signatureCell);

        //    signatureTable.AddCell(new PdfPCell()
        //    {
        //        Phrase = new Phrase("--------------------------------", normal_font),
        //        FixedHeight = 50,
        //        VerticalAlignment = Element.ALIGN_BOTTOM,
        //        HorizontalAlignment = Element.ALIGN_CENTER
        //    });
        //    signatureTable.AddCell(new PdfPCell()
        //    {
        //        Phrase = new Phrase("--------------------------------", normal_font),
        //        FixedHeight = 50,
        //        VerticalAlignment = Element.ALIGN_BOTTOM,
        //        HorizontalAlignment = Element.ALIGN_CENTER
        //    });

        //    footerTable.AddCell(new PdfPCell(signatureTable));
        //    document.Add(footerTable);

        //    #endregion Footer
        //}

        private void GenerateTemplate(DOStockViewModel viewModel, int clientTimeZoneOffset, Document document, Font header_font, Font header_bold_font, Font normal_font, Font bold_font)
        {
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

            cellHeaderBody.Phrase = new Phrase($"No. {viewModel.Type}{viewModel.AutoIncreament.ToString().PadLeft(6, '0')}", bold_font);
            headerTable1.AddCell(cellHeaderBody);

            cellHeaderBody.Phrase = new Phrase(" ", header_font);
            headerTable1.AddCell(cellHeaderBody);

            cellHeaderBody.Phrase = new Phrase(" ", header_font);
            headerTable1.AddCell(cellHeaderBody);

            cellHeaderBody.Phrase = new Phrase(" ", header_font);
            headerTable1.AddCell(cellHeaderBody);

            cellHeaderBody.Phrase = new Phrase("Harap dikeluarkan barang tersebut di bawah ini : ", normal_font);
            headerTable1.AddCell(cellHeaderBody);

            cellHeader1.AddElement(headerTable1);
            headerTable.AddCell(cellHeader1);

            cellHeaderBody.Phrase = new Phrase("", normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHeaderBody.Phrase = new Phrase("Sukoharjo, " + viewModel.Date?.AddHours(clientTimeZoneOffset).ToString("dd MMMM yyyy", new CultureInfo("id-ID")), normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHeaderBody.Phrase = new Phrase("Kepada", normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHeaderBody.Phrase = new Phrase("Yth. Bpk./Ibu. " + viewModel.HeadOfStorage, normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHeaderBody.Phrase = new Phrase("Bag. Gudang Stock Finishing/Printing", normal_font);
            headerTable2.AddCell(cellHeaderBody);


            cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHeaderBody.Phrase = new Phrase("D.O. Stock", bold_font);
            headerTable2.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("", normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHeaderBody.Phrase = new Phrase("Order dari " + viewModel.Buyer.Name, normal_font);
            headerTable2.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("Sukoharjo", normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeader2.AddElement(headerTable2);
            headerTable.AddCell(cellHeader2);

            cellHeaderCS2.Phrase = new Phrase("", normal_font);
            headerTable.AddCell(cellHeaderCS2);

            document.Add(headerTable);

            #endregion Header

            #region Custom
            int index = 1;
            double totalPackingQuantity = 0;
            double totalLengthQuantity = 0;
            #endregion

            #region Body

            PdfPTable bodyTable = new PdfPTable(6);
            PdfPCell bodyCell = new PdfPCell();

            float[] widthsBody = new float[] { 3f, 8f, 10f, 5f, 5f, 5f };
            bodyTable.SetWidths(widthsBody);
            bodyTable.WidthPercentage = 100;

            bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyCell.Phrase = new Phrase("No.", bold_font);
            bodyTable.AddCell(bodyCell);

            bodyCell.Phrase = new Phrase("No. SPP", bold_font);
            bodyTable.AddCell(bodyCell);

            bodyCell.Phrase = new Phrase("Material Konstruksi", bold_font);
            bodyTable.AddCell(bodyCell);

            bodyCell.Phrase = new Phrase("Jenis/Code", bold_font);
            bodyTable.AddCell(bodyCell);

            bodyCell.Phrase = new Phrase("Jumlah Packing", bold_font);
            bodyTable.AddCell(bodyCell);

            bodyCell.Phrase = new Phrase("Panjang", bold_font);
            bodyTable.AddCell(bodyCell);

            foreach (var item in viewModel.DOStockItems)
            {
                bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                bodyCell.Phrase = new Phrase((index++).ToString(), normal_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Phrase = new Phrase(item.ProductionOrder.OrderNo, normal_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Phrase = new Phrase(item.ConstructionName, normal_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Phrase = new Phrase(item.UnitOrCode, normal_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Phrase = new Phrase(string.Format("{0:n0}", item.Packing) + " " + viewModel.PackingUom, normal_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Phrase = new Phrase(string.Format("{0:n0}", item.Length) + " " + viewModel.LengthUom, normal_font);
                bodyTable.AddCell(bodyCell);

                totalPackingQuantity += item.Packing;
                totalLengthQuantity += item.Length;
            }

            bodyCell.Border = Rectangle.NO_BORDER;
            bodyCell.Phrase = new Phrase("", normal_font);
            bodyTable.AddCell(bodyCell);

            bodyCell.Border = Rectangle.NO_BORDER;
            bodyCell.Phrase = new Phrase("", normal_font);
            bodyTable.AddCell(bodyCell);

            bodyCell.Border = Rectangle.NO_BORDER;
            bodyCell.Phrase = new Phrase("", normal_font);
            bodyTable.AddCell(bodyCell);

            bodyCell.Border = Rectangle.BOX;
            bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyCell.Phrase = new Phrase("Total", bold_font);
            bodyTable.AddCell(bodyCell);

            bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyCell.Phrase = new Phrase(string.Format("{0:n2}", totalPackingQuantity), bold_font);
            bodyTable.AddCell(bodyCell);

            bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
            bodyCell.Phrase = new Phrase(string.Format("{0:n2}", totalLengthQuantity), bold_font);
            bodyTable.AddCell(bodyCell);

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

            cellFooterLeft.Phrase = new Phrase("Disp : " + viewModel.Disp.GetValueOrDefault(), normal_font);
            footerTable.AddCell(cellFooterLeft);

            cellFooterLeft.Colspan = 3;
            cellFooterLeft.Phrase = new Phrase("", bold_font);
            footerTable.AddCell(cellFooterLeft);

            cellFooterLeft.Colspan = 3;
            cellFooterLeft.Phrase = new Phrase("Dikirim Kepada : " + viewModel.DestinationBuyerName, bold_font);
            footerTable.AddCell(cellFooterLeft);


            cellFooterLeft.Colspan = 3;
            cellFooterLeft.Phrase = new Phrase("Catatan : " + viewModel.Remark, bold_font);
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
        }
    }
}
