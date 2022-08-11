using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Globalization;
using System.IO;

namespace Com.Ambassador.Service.Sales.Lib
{
    public class DeliveryNoteProductionPdfTemplate
    {
        public MemoryStream GeneratePdfTemplate(DeliveryNoteProductionViewModel viewModel, int clientTimeZoneOffset)
        {
            const int MARGIN_LEFT_RIGHT = 15;
            const int MARGIN_TOP_BOTTOM = 20;

            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 12);
            Font header_bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 12);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
            Font normal_font_underline = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
            normal_font_underline.SetStyle(Font.UNDERLINE);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 9);
            Font bold_font_title = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 12);

            Document document = new Document(PageSize.A4, MARGIN_LEFT_RIGHT, MARGIN_LEFT_RIGHT, MARGIN_TOP_BOTTOM, MARGIN_TOP_BOTTOM);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            document.Open();
            //================

            #region Header

            PdfPTable headerTable = new PdfPTable(2);
            headerTable.SetWidths(new float[] { 10f, 10f });
            headerTable.WidthPercentage = 100;
            PdfPTable headerTable1 = new PdfPTable(1);
            PdfPTable headerTable2 = new PdfPTable(1);
            headerTable2.WidthPercentage = 35;

            PdfPCell cellHeader1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeader2 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeaderBody = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeaderCS2 = new PdfPCell() { Border = Rectangle.NO_BORDER, Colspan = 2 };

            cellHeaderBody.Phrase = new Phrase("BAGIAN PENJUALAN BENANG", header_font);
            headerTable1.AddCell(cellHeaderBody);

            cellHeaderBody.Phrase = new Phrase("PT. DANLIRIS - SUKOHARJO", header_font);
            headerTable1.AddCell(cellHeaderBody);

            cellHeaderBody.Phrase = new Phrase("", header_font);
            headerTable1.AddCell(cellHeaderBody);

            

            cellHeaderBody.Phrase = new Phrase("", header_font);
            headerTable1.AddCell(cellHeaderBody);

            cellHeaderBody.Phrase = new Phrase($"NO               : {viewModel.SalesContract.SalesContractNo}", normal_font);
            headerTable1.AddCell(cellHeaderBody);

            if(viewModel.Subject == "Lainnya")
            {
                cellHeaderBody.Phrase = new Phrase($"PERIHAL     : {viewModel.OtherSubject}", normal_font);
                headerTable1.AddCell(cellHeaderBody);
            }
            else
            {
                cellHeaderBody.Phrase = new Phrase($"PERIHAL     : {viewModel.Subject}", normal_font);
                headerTable1.AddCell(cellHeaderBody);
            }

            

            cellHeaderBody.Phrase = new Phrase($"PEMBELI     : {viewModel.SalesContract.Buyer.Name}", normal_font);
            headerTable1.AddCell(cellHeaderBody);

            cellHeader1.AddElement(headerTable1);
            headerTable.AddCell(cellHeader1);

            cellHeaderBody.HorizontalAlignment = Element.ALIGN_LEFT;
            cellHeaderBody.Phrase = new Phrase("FM-PJ-00-03-014/R1", bold_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeaderBody.Phrase = new Phrase("", normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHeaderBody.Phrase = new Phrase("", normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHeaderBody.Phrase = new Phrase("", normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHeaderBody.Phrase = new Phrase("", normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHeaderBody.Phrase = new Phrase("", normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHeaderBody.Phrase = new Phrase("", normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeaderBody.HorizontalAlignment = Element.ALIGN_LEFT;
            cellHeaderBody.Phrase = new Phrase("Kepada Yth.  :", normal_font);
            headerTable2.AddCell(cellHeaderBody);

            var Unit_split = viewModel.Unit.Split("Bp.");

            cellHeaderBody.HorizontalAlignment = Element.ALIGN_LEFT;
            cellHeaderBody.Phrase = new Phrase("Bpk." + Unit_split[1], normal_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeaderBody.HorizontalAlignment = Element.ALIGN_LEFT;
            cellHeaderBody.Phrase = new Phrase("Kabag " + Unit_split[0], normal_font);
            headerTable2.AddCell(cellHeaderBody);
            
            cellHeaderBody.HorizontalAlignment = Element.ALIGN_LEFT;
            cellHeaderBody.Phrase = new Phrase("PT. Danliris", normal_font);
            headerTable2.AddCell(cellHeaderBody);
            
            cellHeaderBody.HorizontalAlignment = Element.ALIGN_LEFT;
            cellHeaderBody.Phrase = new Phrase("Sukoharjo", normal_font_underline);
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
            double totalWeightQuantity = 0;
            #endregion

            #region Title
            

            string titleString = "SURAT ORDER PRODUKSI";
            Paragraph title = new Paragraph(titleString, bold_font_title) { Alignment = Element.ALIGN_CENTER };
            title.SpacingBefore = 30f;
            title.SpacingAfter = 20f;
            document.Add(title);
            bold_font.SetStyle(Font.NORMAL);

            PdfPTable bodyTable = new PdfPTable(7);
            PdfPCell bodyCell = new PdfPCell();

            float[] widthsBody = new float[] { 3f, 7f, 13f, 7f, 7f, 7f, 7f };
            bodyTable.SetWidths(widthsBody);
            bodyTable.WidthPercentage = 100;

            #endregion

            #region HeaderParagraphString
            string HeaderParagraphString = "Dengan hormat,";
            Paragraph HeaderParagraph = new Paragraph(HeaderParagraphString, normal_font) { Alignment = Element.ALIGN_LEFT };
            document.Add(HeaderParagraph);

            string firstParagraphString = "Mohon diproduksikan benang untuk eksport / lokal / dipakai sendiri dengan spesifikasi sbb : ";
            Paragraph firstParagraph = new Paragraph(firstParagraphString, normal_font) { Alignment = Element.ALIGN_JUSTIFIED };
            firstParagraph.SpacingAfter = 10f;
            document.Add(firstParagraph);
            #endregion

            #region Body
            //PdfPCell cellIdentityContentLeft = new PdfPCell() { HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPTable tableBody = new PdfPTable(3);
            tableBody.SetWidths(new float[] { 0.2f,1f, 2f });
            tableBody.SpacingAfter = 20f;
            PdfPCell bodyContentLeft = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellIdentityContentLeft = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellIdentityContentRight = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            bodyContentLeft.Phrase = new Phrase("1.", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Jenis dan Nomor Benang", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.SalesContract.Comodity.Name, normal_font);
            tableBody.AddCell(bodyContentLeft);

            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);

            bodyContentLeft.Phrase = new Phrase("2.", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Blended", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.SalesContract.ComodityDescription, normal_font);
            tableBody.AddCell(bodyContentLeft);

            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);

            bodyContentLeft.Phrase = new Phrase("3.", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Net Weight", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": 1.89 KG / cone", normal_font);
            tableBody.AddCell(bodyContentLeft);

            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);

            bodyContentLeft.Phrase = new Phrase("4.", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Lot", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": 1 (SATU) lot", normal_font);
            tableBody.AddCell(bodyContentLeft);

            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);

            bodyContentLeft.Phrase = new Phrase("5.", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Jumlah", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.SalesContract.OrderQuantity.ToString() + " " + viewModel.SalesContract.UomUnit, normal_font);
            tableBody.AddCell(bodyContentLeft);

            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);

            bodyContentLeft.Phrase = new Phrase("6.", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Packing", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": Dos", normal_font);
            tableBody.AddCell(bodyContentLeft);

            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);

            bodyContentLeft.Phrase = new Phrase("7.", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Barang siap kirim", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.MonthandYear, normal_font);
            tableBody.AddCell(bodyContentLeft);

            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);

            bodyContentLeft.Phrase = new Phrase("8.", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Bale Mark", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.BallMark, normal_font);
            tableBody.AddCell(bodyContentLeft);

            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);

            bodyContentLeft.Phrase = new Phrase("9.", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Sample", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.Sample, normal_font);
            tableBody.AddCell(bodyContentLeft);

            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);

            bodyContentLeft.Phrase = new Phrase("10.", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Catatan", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.Remark, normal_font);
            tableBody.AddCell(bodyContentLeft);

            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);

            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);

            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);

            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);

            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Atas bantuannya kami ucapkan terima kasih.", normal_font);
            tableBody.AddCell(new PdfPCell(bodyContentLeft) { Colspan = 2 });
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);

            PdfPCell cellBody = new PdfPCell(tableBody); // dont remove
            tableBody.ExtendLastRow = false;
            document.Add(tableBody);

            //PdfPTable conditionListBody = new PdfPTable(3);
            //conditionListBody.SetWidths(new float[] { 0.4f, 0.025f, 1f });

            //PdfPCell cellConditionList = new PdfPCell(conditionListBody); // dont remove
            //conditionListBody.ExtendLastRow = false;
            //conditionListBody.SpacingAfter = 20f;
            //document.Add(conditionListBody);

            #endregion

            PdfPTable tableIdentity = new PdfPTable(3);
            tableIdentity.SetWidths(new float[] { 0.5f, 5.2f, 2.5f });

            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            tableIdentity.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            tableIdentity.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase($"Sukoharjo, {viewModel.CreatedUtc.AddHours(clientTimeZoneOffset).ToString("dd MMMM yyyy", new CultureInfo("id-ID"))}", normal_font);
            tableIdentity.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            tableIdentity.AddCell(cellIdentityContentLeft);
            tableIdentity.AddCell(cellIdentityContentRight);
            PdfPCell cellIdentity = new PdfPCell(tableIdentity); // dont remove
            tableIdentity.ExtendLastRow = false;
            document.Add(tableIdentity);

            #region signature1
            PdfPTable signature = new PdfPTable(2);
            signature.SetWidths(new float[] { 1f, 1f });
            PdfPCell cell_signature = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 2 };
            //PdfPCell cellIdentityContentRight = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            signature.SetWidths(new float[] { 1f, 1f });
            cell_signature.Phrase = new Phrase("Mengetahui, ", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("Hormat kami,", normal_font);
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

            cell_signature.Phrase = new Phrase("Sri Hendratno", normal_font_underline);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase(viewModel.YarnSales, normal_font_underline);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("Kasie Penjualan Benang & Grey", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("Penjualan Benang", normal_font);
            signature.AddCell(cell_signature);
            cellIdentityContentRight.Phrase = new Phrase("");
            signature.AddCell(cellIdentityContentRight);

            PdfPCell signatureCell = new PdfPCell(signature); // dont remove
            signature.ExtendLastRow = false;
            signature.SpacingAfter = 30f;
            document.Add(signature);
            #endregion



            #region subsignature

            PdfPTable sub_signature = new PdfPTable(1);
            sub_signature.WidthPercentage = 10;
            //sub_signature.SetWidths(new float[] { 1f, 1f });
            PdfPCell cell_signature_sub = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 2 };
            //sub_signature.SetWidths(new float[] { 1f, 1f });
            cell_signature_sub.Phrase = new Phrase("Bagian Spinning sanggup memenuhi permintaan pelanggan tersebut di atas", normal_font);
            sub_signature.AddCell(cell_signature_sub);

            PdfPCell signatureCell_sub = new PdfPCell(sub_signature); // dont remove
            sub_signature.ExtendLastRow = false;
            sub_signature.SpacingAfter = 30f;
            document.Add(sub_signature);
            #endregion



            #region signature2

            PdfPTable signature2 = new PdfPTable(2);
            signature2.SetWidths(new float[] { 1f, 1f });
            PdfPCell cell_signature2 = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 2 };
            PdfPCell cellIdentityContentRight2 = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            signature2.SetWidths(new float[] { 1f, 1f });
            cell_signature2.Phrase = new Phrase("Diterima, ", normal_font);
            signature2.AddCell(cell_signature2);
            cell_signature2.Phrase = new Phrase("Mengetahui,", normal_font);
            signature2.AddCell(cell_signature2);

            cell_signature2.Phrase = new Phrase("", normal_font);
            signature2.AddCell(cell_signature2);
            cell_signature2.Phrase = new Phrase("", normal_font);
            signature2.AddCell(cell_signature2);

            string signature2Area = string.Empty;
            for (int i = 0; i < 5; i++)
            {
                signature2Area += Environment.NewLine;
            }

            cell_signature2.Phrase = new Phrase(signature2Area, normal_font);
            signature2.AddCell(cell_signature2);
            signature2.AddCell(cell_signature2);

            cell_signature2.Phrase = new Phrase(Unit_split[1], normal_font_underline);
            signature2.AddCell(cell_signature2);
            cell_signature2.Phrase = new Phrase("Heru Sudarno", normal_font_underline);
            signature2.AddCell(cell_signature2);
            cell_signature2.Phrase = new Phrase("Kabag " + Unit_split[0], normal_font);
            signature2.AddCell(cell_signature2);
            cell_signature2.Phrase = new Phrase("Direktur. Prod", normal_font);
            signature2.AddCell(cell_signature2);
            cellIdentityContentRight2.Phrase = new Phrase("");
            signature2.AddCell(cellIdentityContentRight2);

            PdfPCell signature2Cell = new PdfPCell(signature2); // dont remove
            signature2.ExtendLastRow = false;
            signature2.SpacingAfter = 10f;
            document.Add(signature2);

            #endregion

            document.Close();

            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }
    }
}