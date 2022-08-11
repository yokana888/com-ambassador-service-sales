using Com.Ambassador.Service.Sales.Lib.ViewModels.Weaving;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Com.Ambassador.Service.Sales.Lib.Utilities;

namespace Com.Ambassador.Service.Sales.Lib.PDFTemplates
{
    public class WeavingSalesContractModelPDFTemplate
    {
        public MemoryStream GeneratePdfTemplate(WeavingSalesContractViewModel viewModel, int timeoffset)
        {
            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 18);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);

            Document document = new Document(PageSize.A4, 40, 40, 110, 40);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            document.Open();

            #region customViewModel

            var uomLocal = "";
            if (viewModel.Uom.Unit.ToLower() == "yds")
            {
                uomLocal = "YARD";
            }
            else if (viewModel.Uom.Unit.ToLower() == "mtr")
            {
                uomLocal = "METER";
            }
            else
            {
                uomLocal = viewModel.Uom.Unit;
            }

            var ppn = viewModel.IncomeTax;
            if (ppn == "Include PPn")
            {
                ppn = "Include PPn 10%";
            }

            string QuantityToText = NumberToTextIDN.terbilang(viewModel.OrderQuantity);

            var tax = viewModel.IncomeTax == "Include PPn" ? $"Include PPn {viewModel.VatTax.Rate}%" : viewModel.IncomeTax;

            var appxLocal = "";
            var date = viewModel.DeliverySchedule.Value.Day;
            if (date >= 1 && date <= 10)
            {
                appxLocal = "AWAL";
            }
            else if (date >= 11 && date <= 20)
            {
                appxLocal = "PERTENGAHAN";
            }
            else if (date >= 21 && date <= 31)
            {
                appxLocal = "AKHIR";
            }

            #endregion

            #region Header
            
            string codeNoString = "FM-PJ-00-03-003/R1";
            Paragraph codeNo = new Paragraph(codeNoString, bold_font) { Alignment = Element.ALIGN_RIGHT };
            document.Add(codeNo);

            string titleString = "SALES CONTRACT";
            Paragraph title = new Paragraph(titleString, bold_font) { Alignment = Element.ALIGN_CENTER };
            title.SpacingAfter = 10f;
            document.Add(title);
            bold_font.SetStyle(Font.NORMAL);

            #endregion

            #region Identity

            PdfPTable tableIdentity = new PdfPTable(3);
            tableIdentity.SetWidths(new float[] { 0.5f, 4.5f, 2.5f });
            PdfPCell cellIdentityContentLeft = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellIdentityContentRight = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            cellIdentityContentLeft.Phrase = new Phrase("No", normal_font);
            tableIdentity.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.SalesContractNo, normal_font);
            tableIdentity.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase($"Sukoharjo, {viewModel.CreatedUtc.AddHours(timeoffset).ToString("dd MMMM yyyy", new CultureInfo("id-ID"))}", normal_font);
            tableIdentity.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase("Hal", normal_font);
            tableIdentity.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(": " + "KONFIRMASI ORDER GREY", normal_font);
            tableIdentity.AddCell(cellIdentityContentLeft);
            cellIdentityContentRight.Phrase = new Phrase("");
            tableIdentity.AddCell(cellIdentityContentRight);
            PdfPCell cellIdentity = new PdfPCell(tableIdentity); // dont remove
            tableIdentity.ExtendLastRow = false;
            tableIdentity.SpacingAfter = 10f;
            document.Add(tableIdentity);

            PdfPTable tableIdentityOpeningLetter = new PdfPTable(3);
            tableIdentity.SetWidths(new float[] { 2f, 4.5f, 2.5f });
            cellIdentityContentRight.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentRight);
            cellIdentityContentRight.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentRight);
            cellIdentityContentLeft.Phrase = new Phrase("Kepada Yth :", normal_font);
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentRight.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentRight);
            cellIdentityContentRight.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentRight);
            cellIdentityContentLeft.Phrase = new Phrase(viewModel.Buyer.Name, normal_font);
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentRight.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentRight);
            cellIdentityContentRight.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentRight);
            cellIdentityContentLeft.Phrase = new Phrase(viewModel.Buyer.Address, normal_font);
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            cellIdentityContentRight.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentRight);
            cellIdentityContentRight.Phrase = new Phrase("");
            tableIdentityOpeningLetter.AddCell(cellIdentityContentRight);
            cellIdentityContentLeft.Phrase = new Phrase(viewModel.Buyer.City, normal_font);
            tableIdentityOpeningLetter.AddCell(cellIdentityContentLeft);
            PdfPCell cellIdentityOpeningLetter = new PdfPCell(tableIdentityOpeningLetter); // dont remove
            tableIdentityOpeningLetter.ExtendLastRow = false;
            tableIdentityOpeningLetter.SpacingAfter = 10f;
            document.Add(tableIdentityOpeningLetter);

            #endregion

            string HeaderParagraphString = "Dengan Hormat,";
            Paragraph HeaderParagraph = new Paragraph(HeaderParagraphString, normal_font) { Alignment = Element.ALIGN_LEFT };
            document.Add(HeaderParagraph);

            string firstParagraphString = "Sesuai dengan pesanan / order Bapak / Ibu kepada kami, maka bersama ini kami kirimkan surat persetujuan pesanan dengan ketentuan dan syarat - syarat di bawah ini: ";
            Paragraph firstParagraph = new Paragraph(firstParagraphString, normal_font) { Alignment = Element.ALIGN_JUSTIFIED };
            firstParagraph.SpacingAfter = 10f;
            document.Add(firstParagraph);

            #region body
            PdfPTable tableBody = new PdfPTable(2);
            tableBody.SetWidths(new float[] { 0.75f, 1f });
            PdfPCell bodyContentLeft = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell bodyContentRight = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            bodyContentLeft.Phrase = new Phrase("Jenis", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.Comodity.Name + " " + viewModel.ComodityDescription, normal_font);
            tableBody.AddCell(bodyContentLeft);
            if (!string.IsNullOrEmpty(viewModel.ComodityDescription) && !string.IsNullOrWhiteSpace(viewModel.ComodityDescription))
            {
                bodyContentLeft.Phrase = new Phrase(" ", normal_font);
                tableBody.AddCell(bodyContentLeft);
                bodyContentLeft.Phrase = new Phrase("  " + viewModel.ComodityDescription, normal_font);
                tableBody.AddCell(bodyContentLeft);
            }
            bodyContentLeft.Phrase = new Phrase("Material / Konstruksi", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.Product.Name + " " + viewModel.MaterialConstruction.Name + " " + viewModel.YarnMaterial.Name + " " + viewModel.MaterialWidth, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Jumlah", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.OrderQuantity.ToString("N2") + " (" + QuantityToText + ") " + uomLocal, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Kualitas", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.Quality.Name, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Harga", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.AccountBank.Currency.Symbol + " " + string.Format("{0:n}", viewModel.Price) + " / " + uomLocal + " " + tax, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Syarat Pembayaran", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.TermOfPayment.Name, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Pembayaran Ke Alamat", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": BANK " + viewModel.AccountBank.BankName, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("  " + viewModel.AccountBank.BankAddress, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("  A/C. " + viewModel.AccountBank.AccountNumber, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("  A/N. " + viewModel.AccountBank.AccountName, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Ongkos Angkut", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + string.Format("{0:n2}",viewModel.TransportFee), normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Dikirim Ke", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.DeliveredTo, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Jadwal Pengiriman", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + appxLocal + " " + (viewModel.DeliverySchedule.Value.AddHours(timeoffset).ToString("MMMM yyyy", new CultureInfo("id-ID")))?.ToUpper(), normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Piece length", normal_font);
            tableBody.AddCell(bodyContentLeft);
            string pieceLength = !string.IsNullOrEmpty(viewModel.PieceLength) ? viewModel.PieceLength.Replace("\n", "\n  ") : viewModel.PieceLength;
            bodyContentLeft.Phrase = new Phrase(": " + pieceLength, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Kondisi", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.Condition, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Keterangan", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " + viewModel.Remark, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentRight.Phrase = new Phrase("");
            tableBody.AddCell(bodyContentRight);
            PdfPCell cellBody = new PdfPCell(tableBody); // dont remove
            tableBody.ExtendLastRow = false;
            tableBody.SpacingAfter = 10f;
            document.Add(tableBody);
            #endregion

            string ClosingParagraphString = "Demikian konfirmasi order ini kami sampaikan untuk diketahui dan dipergunakan seperlunya. Tembusan surat ini mohon dikirim kembali setelah ditanda tangani dan dibubuhi cap perusahaan.";
            Paragraph ClosingParagraph = new Paragraph(ClosingParagraphString, normal_font) { Alignment = Element.ALIGN_JUSTIFIED };
            ClosingParagraph.SpacingBefore = 10f;
            ClosingParagraph.SpacingAfter = 10f;
            document.Add(ClosingParagraph);

            #region signature
            PdfPTable signature = new PdfPTable(2);
            signature.SetWidths(new float[] { 1f, 1f });
            PdfPCell cell_signature = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 2 };
            signature.SetWidths(new float[] { 1f, 1f });
            cell_signature.Phrase = new Phrase("Pembeli,", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("Hormat Kami, ", normal_font);
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
            cell_signature.Phrase = new Phrase(" ", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase(" ", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase(" ", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase(" ", normal_font);
            signature.AddCell(cell_signature);

            cell_signature.Phrase = new Phrase("(...........................)", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("( SRI HENDRATNO )", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("", normal_font);
            signature.AddCell(cell_signature);
            cell_signature.Phrase = new Phrase("Penjualan Tekstil", normal_font);
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
            
            string ConditionString = "Kondisi";
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
            bodyContentJustify.Phrase = new Phrase("Keterlambatan pembayaran dikenakan denda 3.00 % per bulan.", normal_font);
            conditionList.AddCell(bodyContentJustify);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(bulletListSymbol, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            bodyContentJustify.Phrase = new Phrase("Pembayaran maju mendapat potongan 00.00 % per bulan, potongan pembayaran maju tersebut dapat berubah sewaktu - waktu baik dengan atau tanpa pemberitahuan terlebih dahulu dari pihak PT.Ambassador.", normal_font);
            conditionList.AddCell(bodyContentJustify);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(bulletListSymbol, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            bodyContentJustify.Phrase = new Phrase("Bila terjadi kebijaksanaan pemerintah dalam bidang moneter, untuk barang yang belum terkirim harga akan dibicarakan lagi.", normal_font);
            conditionList.AddCell(bodyContentJustify);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(bulletListSymbol, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            bodyContentJustify.Phrase = new Phrase("Kain/Benang yang telah diproses/dipotong tidak dapat dikembalikan kecuali ada persetujuan tertulis dari kedua belah pihak sebelumnya.", normal_font);
            conditionList.AddCell(bodyContentJustify);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(bulletListSymbol, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            bodyContentJustify.Phrase = new Phrase("Semua klaim atas cacat Kain / Benang harus diinformasikan kepada penjual secara tertulis, berikut contoh atau bukti yang menunjang(memadai), maksimum 2 minggu setelah tanggal penerimaan barang.", normal_font);
            conditionList.AddCell(bodyContentJustify);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(bulletListSymbol, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            bodyContentJustify.Phrase = new Phrase("Klaim yang diajukan akan diselesaikan secara terpisah dan tidak dapat dihubungkan atau dikompensasikan dengan pembayaran Kain Grey / Benang.", normal_font);
            conditionList.AddCell(bodyContentJustify);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(bulletListSymbol, normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            bodyContentJustify.Phrase = new Phrase("Penjual mempunyai hak dengan pemberitahuan sebelumnya untuk membatalkan Konfrmasi ini seluruhnya atau sebagian bilamana:", normal_font);
            conditionList.AddCell(bodyContentJustify);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionList.AddCell(cellIdentityContentLeft);
            PdfPCell conditionListData = new PdfPCell(conditionList); // dont remove
            conditionList.ExtendLastRow = false;
            document.Add(conditionList);

            PdfPTable conditionListChild = new PdfPTable(2);
            conditionListChild.SetWidths(new float[] { 0.04f, 1f });
            cellIdentityContentRight.Phrase = new Phrase("1. ", normal_font);
            conditionListChild.AddCell(cellIdentityContentRight);
            bodyContentJustify.Phrase = new Phrase("Pembeli tidak dapat memenuhi / menyelesaikan jadwal pengiriman/pengambilan barang yang telah ditetapkan dan disetujui kedua belah pihak.", normal_font);
            conditionListChild.AddCell(bodyContentJustify);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionListChild.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionListChild.AddCell(cellIdentityContentLeft);
            cellIdentityContentRight.Phrase = new Phrase("2. ", normal_font);
            conditionListChild.AddCell(cellIdentityContentRight);
            bodyContentJustify.Phrase = new Phrase("Pembeli belum / tidak dapat menyelesaikan pembayaran yang sudah jatuh tempo dari pengambilan / order yang telah terkirim sebelumnya.", normal_font);
            conditionListChild.AddCell(bodyContentJustify);
            cellIdentityContentLeft.Phrase = new Phrase(" ", normal_font);
            conditionListChild.AddCell(cellIdentityContentLeft);

            PdfPCell conditionListChildData = new PdfPCell(conditionListChild); // dont remove
            conditionListChild.ExtendLastRow = false;
            document.Add(conditionListChild);

            #endregion

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;
            return stream;
        }
    }
}
