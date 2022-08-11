using Com.Ambassador.Service.Sales.Lib.ViewModels.DOSales;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Globalization;
using System.IO;

namespace Com.Ambassador.Service.Sales.Lib.PDFTemplates
{
    public class DOSalesPdfTemplate
    {
        public MemoryStream GeneratePdfTemplate(DOSalesViewModel viewModel, int clientTimeZoneOffset)
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

            if (viewModel.DOSalesType == "Lokal" || viewModel.DOSalesCategory == "SPINNING")
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

                if(viewModel.DOSalesType == "Lokal" && viewModel.DOSalesCategory == "SPINNING")
                {
                    cellHeaderBody.Phrase = new Phrase("FM-PJ-00-03-005 / R2", bold_font);
                    headerTable1.AddCell(cellHeaderBody);
                }                

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

                if (viewModel.DOSalesCategory.Equals("SPINNING") || viewModel.DOSalesCategory.Equals("WEAVING"))
                {
                    cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
                    cellHeaderBody.Phrase = new Phrase("Bag. " + viewModel.Storage.name, normal_font);
                    headerTable2.AddCell(cellHeaderBody);
                }

                if (viewModel.DOSalesCategory.Equals("DYEINGPRINTING"))
                {
                    cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
                    cellHeaderBody.Phrase = new Phrase("Bag. Gudang Packing Finishing/Printing", normal_font);
                    headerTable2.AddCell(cellHeaderBody);
                }

                cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
                cellHeaderBody.Phrase = new Phrase("D.O. PENJUALAN", bold_font);
                headerTable2.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", normal_font);
                headerTable2.AddCell(cellHeaderBody);

                cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
                cellHeaderBody.Phrase = new Phrase("Order dari " + viewModel.SalesContract.Buyer.Name, normal_font);
                headerTable2.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(" ", normal_font);
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

                #region Body

                PdfPTable bodyTable = new PdfPTable(7);
                PdfPCell bodyCell = new PdfPCell();

                float[] widthsBody = new float[] { 3f, 7f, 13f, 7f, 7f, 7f, 7f };
                bodyTable.SetWidths(widthsBody);
                bodyTable.WidthPercentage = 100;

                if (viewModel.DOSalesCategory.Equals("SPINNING"))
                {
                    bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    bodyCell.Phrase = new Phrase("No.", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("No. SOP", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Jenis dan Nomor Benang", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Jenis / Code", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Pcs/Roll/Pt", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Mtr/Yds", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Kg/Bale", bold_font);
                    bodyTable.AddCell(bodyCell);
                }
                else if (viewModel.DOSalesCategory.Equals("WEAVING"))
                {
                    bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    bodyCell.Phrase = new Phrase("No.", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("No. SOP", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Jenis dan Nomor Benang", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Grade", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Pcs/Roll/Pt", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Mtr/Yds", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Kg/Bale", bold_font);
                    bodyTable.AddCell(bodyCell);
                }
                else
                {
                    bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    bodyCell.Phrase = new Phrase("No.", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("No. SPP", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Material Konstruksi", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Jenis / Code", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Pcs/Roll/Pt", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Yds/Bale", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Mtr/Kg", bold_font);
                    bodyTable.AddCell(bodyCell);
                }



                //bodyCell.Phrase = new Phrase("Total Packing\n(" + viewModel.PackingUom + ")", bold_font);
                //bodyTable.AddCell(bodyCell);

                //bodyCell.Phrase = new Phrase("Total Panjang\n(" + viewModel.LengthUom + ")", bold_font);
                //bodyTable.AddCell(bodyCell);

                //bodyCell.Phrase = new Phrase("Total Berat\n(" + viewModel.WeightUom + ")", bold_font);
                //bodyTable.AddCell(bodyCell);
                if (viewModel.DOSalesCategory.Equals("SPINNING"))
                {

                    foreach (DOSalesDetailViewModel item in viewModel.DOSalesDetailItems)
                    {
                        bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        bodyCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                        bodyCell.Phrase = new Phrase((index++).ToString(), normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(item.NoSOP, normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(item.ThreadNumber, normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(viewModel.PackingUom, normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(string.Format("{0:n0}", item.Packing) + " " + viewModel.PackingUom, normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(string.Format("{0:n0}", item.Length) + " " + viewModel.LengthUom, normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(string.Format("{0:n0}", item.Weight) + " " + viewModel.WeightUom, normal_font);
                        bodyTable.AddCell(bodyCell);
                    }
                }
                else if (viewModel.DOSalesCategory.Equals("WEAVING"))
                {

                    foreach (DOSalesDetailViewModel item in viewModel.DOSalesDetailItems)
                    {
                        bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        bodyCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                        bodyCell.Phrase = new Phrase((index++).ToString(), normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(item.NoSOP, normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(item.ThreadNumber, normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(item.Grade, normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(string.Format("{0:n0}", item.Packing) + " " + viewModel.PackingUom, normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(string.Format("{0:n0}", item.Length) + " " + viewModel.LengthUom, normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(string.Format("{0:n0}", item.Weight) + " " + viewModel.WeightUom, normal_font);
                        bodyTable.AddCell(bodyCell);
                    }
                }
                else
                {

                    foreach (DOSalesDetailViewModel item in viewModel.DOSalesDetailItems)
                    {
                        bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        bodyCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                        bodyCell.Phrase = new Phrase((index++).ToString(), normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(item.ProductionOrder.OrderNo, normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(item.ConstructionName.Replace("\n",string.Empty).Replace(" ", string.Empty), normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(item.UnitOrCode, normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(string.Format("{0:n0}", item.Packing) + " " + viewModel.PackingUom, normal_font);
                        bodyTable.AddCell(bodyCell);

                        if(viewModel.LengthUom == "YDS")
                        {
                            bodyCell.Phrase = new Phrase(string.Format("{0:n0}", item.Length) + " " + viewModel.LengthUom, normal_font);
                            bodyTable.AddCell(bodyCell);

                            bodyCell.Phrase = new Phrase(string.Format("{0:n0}", item.ConvertionValue) + " MTR", normal_font);
                            bodyTable.AddCell(bodyCell);
                        }
                        else
                        {
                            bodyCell.Phrase = new Phrase(string.Format("{0:n0}", item.ConvertionValue) + " " + viewModel.LengthUom, normal_font);
                            bodyTable.AddCell(bodyCell);

                            bodyCell.Phrase = new Phrase(string.Format("{0:n0}", item.Length) + " YDS", normal_font);
                            bodyTable.AddCell(bodyCell);
                        }

                        
                    }

                }

                if(viewModel.DOSalesCategory.Equals("DYEINGPRINTING"))
                {
                    if (viewModel.LengthUom == "YDS")
                    {
                        foreach (DOSalesDetailViewModel total in viewModel.DOSalesDetailItems)
                        {
                            totalPackingQuantity += total.Packing;
                            totalLengthQuantity += total.Length;
                            totalWeightQuantity += total.ConvertionValue;
                        }
                    }
                    else
                    {
                        foreach (DOSalesDetailViewModel total in viewModel.DOSalesDetailItems)
                        {
                            totalPackingQuantity += total.Packing;
                            totalLengthQuantity += total.ConvertionValue;
                            totalWeightQuantity += total.Length;
                        }
                    }

                        
                }
                else
                {
                    foreach (DOSalesDetailViewModel total in viewModel.DOSalesDetailItems)
                    {
                        totalPackingQuantity += total.Packing;
                        totalLengthQuantity += total.Length;
                        totalWeightQuantity += total.Weight;
                    }
                }                

                //bodyCell.Colspan = 2;
                bodyCell.Border = Rectangle.NO_BORDER;
                bodyCell.Phrase = new Phrase("", normal_font);
                bodyTable.AddCell(bodyCell);

                //bodyCell.Colspan = 2;
                bodyCell.Border = Rectangle.NO_BORDER;
                bodyCell.Phrase = new Phrase("", normal_font);
                bodyTable.AddCell(bodyCell);

                //bodyCell.Colspan = 2;
                bodyCell.Border = Rectangle.NO_BORDER;
                bodyCell.Phrase = new Phrase("", normal_font);
                bodyTable.AddCell(bodyCell);

                //bodyCell.Colspan = 1;
                bodyCell.Border = Rectangle.BOX;
                bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyCell.Phrase = new Phrase("Total", bold_font);
                bodyTable.AddCell(bodyCell);

                //bodyCell.Colspan = 1;
                bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyCell.Phrase = new Phrase(string.Format("{0:n2}", totalPackingQuantity), bold_font);
                bodyTable.AddCell(bodyCell);

                //bodyCell.Colspan = 1;
                bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyCell.Phrase = new Phrase(string.Format("{0:n2}", totalLengthQuantity), bold_font);
                bodyTable.AddCell(bodyCell);

                //bodyCell.Colspan = 1;
                bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyCell.Phrase = new Phrase(string.Format("{0:n2}", totalWeightQuantity), bold_font);
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

                cellFooterLeft.Phrase = new Phrase("Disp : " + viewModel?.Disp + "       Op : " + viewModel?.Op + "       Sc : " + viewModel?.Sc, normal_font);
                footerTable.AddCell(cellFooterLeft);

                cellFooterLeft.Colspan = 3;
                cellFooterLeft.Phrase = new Phrase("", bold_font);
                footerTable.AddCell(cellFooterLeft);

                cellFooterLeft.Colspan = 3;
                cellFooterLeft.Phrase = new Phrase("Dikirim Kepada : " + viewModel.DestinationBuyerName, bold_font);
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

            }
            else if (viewModel.DOSalesType == "Ekspor")
            {
                #region Header
                PdfPTable headerTable_A = new PdfPTable(2);
                PdfPTable headerTable_B = new PdfPTable(1);
                PdfPTable headerTable1 = new PdfPTable(1);
                PdfPTable headerTable2 = new PdfPTable(1);
                PdfPTable headerTable3 = new PdfPTable(3);
                PdfPTable headerTable4 = new PdfPTable(2);
                headerTable_A.SetWidths(new float[] { 10f, 10f });
                headerTable_A.WidthPercentage = 100;
                headerTable3.SetWidths(new float[] { 40f, 4f, 100f });
                headerTable3.WidthPercentage = 100;
                headerTable4.SetWidths(new float[] { 10f, 40f });
                headerTable4.WidthPercentage = 100;

                PdfPCell cellHeader1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellHeader2 = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellHeader3 = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellHeader4 = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellHeaderBody = new PdfPCell() { Border = Rectangle.NO_BORDER };

                cellHeaderBody.Phrase = new Phrase("", normal_font);
                headerTable1.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase($"No. {viewModel.Type}{viewModel.AutoIncreament.ToString().PadLeft(6, '0')}", header_font);
                headerTable1.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", normal_font);
                headerTable1.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", normal_font);
                headerTable1.AddCell(cellHeaderBody);

                cellHeader1.AddElement(headerTable1);
                headerTable_A.AddCell(cellHeader1);

                cellHeaderBody.HorizontalAlignment = Element.ALIGN_LEFT;

                cellHeaderBody.Phrase = new Phrase("", header_font);
                headerTable2.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("KERTAS KERJA EKSPORT", header_bold_font);
                headerTable2.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", header_font);
                headerTable2.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", header_font);
                headerTable2.AddCell(cellHeaderBody);

                cellHeader2.AddElement(headerTable2);
                headerTable_A.AddCell(cellHeader2);

                document.Add(headerTable_A);

                cellHeaderBody.HorizontalAlignment = Element.ALIGN_LEFT;

                cellHeaderBody.Phrase = new Phrase("", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", bold_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", bold_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("Dikerjakan Oleh ", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(":", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(viewModel.DoneBy, normal_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("1. Order Untuk ", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(":", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(viewModel.SalesContract.Material.Name + " / " + viewModel.SalesContract.MaterialConstruction.Name + " / " + viewModel.SalesContract.MaterialWidth + "            " + viewModel.SalesContract.SalesContractNo, normal_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("2. Jenis Untuk ", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(":", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(viewModel.SalesContract.Buyer.Name, normal_font);
                headerTable3.AddCell(cellHeaderBody);

                double total_weight = 0;

                foreach (DOSalesDetailViewModel item in viewModel.DOSalesDetailItems)
                {
                    total_weight += item.Packing;
                }

                cellHeaderBody.Phrase = new Phrase("3. Jumlah Kirim ", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(":", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(total_weight + " " + viewModel.PackingUom, normal_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("4. Piece Length ", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(":", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(viewModel.SalesContract.PieceLength, normal_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("5. Cap Komposisi Persen ", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(":", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(viewModel.SalesContract.Commodity.Name, normal_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("6. Isi tiap Bale", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(":", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(viewModel.FillEachBale.GetValueOrDefault().ToString("N2") + " " + viewModel.BaleUom, normal_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("CATATAN KETERANGAN ", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(":", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                //foreach (DOSalesDetailViewModel item in viewModel.DOSalesDetailItems)
                //{
                //    cellHeaderBody.Phrase = new Phrase("\n" + item.ProductionOrder.OrderNo + " (" + item.ColorRequest + ")", normal_font);
                //    //headerTable3.AddCell(cellHeaderBody);
                //}
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(" ", normal_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeader3.AddElement(headerTable3);
                headerTable_B.AddCell(cellHeader3);

                cellHeader4.AddElement(headerTable4);
                headerTable_B.AddCell(cellHeader4);

                document.Add(headerTable_B);

                #endregion Header

                #region Body

                PdfPTable bodyTable = new PdfPTable(1);
                PdfPCell bodyCell = new PdfPCell() { Border = Rectangle.NO_BORDER };

                float[] widthsBody = new float[] { 1f };
                bodyTable.SetWidths(widthsBody);
                bodyTable.WidthPercentage = 80;

                foreach (DOSalesDetailViewModel item in viewModel.DOSalesDetailItems)
                {
                    bodyCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    bodyCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                    bodyCell.Phrase = new Phrase(item.ProductionOrder.OrderNo + " " + item.ColorRequest, bold_font);
                    bodyTable.AddCell(bodyCell);
                }
                bodyCell.Phrase = new Phrase("", bold_font);
                bodyTable.AddCell(bodyCell);
                bodyCell.Phrase = new Phrase("", bold_font);
                bodyTable.AddCell(bodyCell);
                bodyCell.Phrase = new Phrase("", bold_font);
                bodyTable.AddCell(bodyCell);
                bodyCell.Phrase = new Phrase(viewModel.Remark, bold_font);
                bodyTable.AddCell(bodyCell);
                bodyCell.Phrase = new Phrase("", bold_font);
                bodyTable.AddCell(bodyCell);
                bodyCell.Phrase = new Phrase("", bold_font);
                bodyTable.AddCell(bodyCell);
                bodyCell.Phrase = new Phrase("", bold_font);
                bodyTable.AddCell(bodyCell);
                bodyCell.Phrase = new Phrase("", bold_font);
                bodyTable.AddCell(bodyCell);
                bodyCell.Phrase = new Phrase("", bold_font);
                bodyTable.AddCell(bodyCell);

                document.Add(bodyTable);

                #endregion Body

                #region Footer

                PdfPTable footerTable = new PdfPTable(1);

                PdfPTable signatureTable = new PdfPTable(2) { HorizontalAlignment = Element.ALIGN_CENTER };
                PdfPCell signatureCell = new PdfPCell() { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };

                float[] widthsSignanture = new float[] { 10f, 10f };
                signatureTable.SetWidths(widthsSignanture);
                signatureTable.WidthPercentage = 80;

                signatureCell.Phrase = new Phrase("Diterima Tgl. ......................", normal_font);
                signatureTable.AddCell(signatureCell);
                signatureCell.Phrase = new Phrase("Sukoharjo, " + viewModel.Date?.AddHours(clientTimeZoneOffset).ToString("dd MMMM yyyy", new CultureInfo("id-ID")) + "\n\nParaf Pembuat,", normal_font);
                signatureTable.AddCell(signatureCell);

                signatureTable.AddCell(new PdfPCell()
                {
                    Phrase = new Phrase("--------------------------------", normal_font),
                    FixedHeight = 50,
                    VerticalAlignment = Element.ALIGN_BOTTOM,
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                signatureTable.AddCell(new PdfPCell()
                {
                    Phrase = new Phrase("--------------------------------", normal_font),
                    FixedHeight = 50,
                    VerticalAlignment = Element.ALIGN_BOTTOM,
                    HorizontalAlignment = Element.ALIGN_CENTER
                });

                footerTable.AddCell(new PdfPCell(signatureTable));
                document.Add(footerTable);

                #endregion Footer
            }

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

            if (viewModel.DOSalesType == "Lokal" || viewModel.DOSalesCategory == "SPINNING")
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

                if (viewModel.DOSalesType == "Lokal" && viewModel.DOSalesCategory == "SPINNING")
                {
                    cellHeaderBody.Phrase = new Phrase("FM-PJ-00-03-005 / R2", bold_font);
                    headerTable1.AddCell(cellHeaderBody);
                }

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

                if (viewModel.DOSalesCategory.Equals("SPINNING") || viewModel.DOSalesCategory.Equals("WEAVING"))
                {
                    cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
                    cellHeaderBody.Phrase = new Phrase("Bag. " + viewModel.Storage.name, normal_font);
                    headerTable2.AddCell(cellHeaderBody);
                }
                else if (viewModel.DOSalesCategory.Equals("DYEINGPRINTING"))
                {
                    cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
                    cellHeaderBody.Phrase = new Phrase("Bag. Gudang Packing Finishing/Printing", normal_font);
                    headerTable2.AddCell(cellHeaderBody);
                }

                cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
                cellHeaderBody.Phrase = new Phrase("D.O. PENJUALAN", bold_font);
                headerTable2.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", normal_font);
                headerTable2.AddCell(cellHeaderBody);

                cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;
                cellHeaderBody.Phrase = new Phrase("Order dari " + viewModel.SalesContract.Buyer.Name, normal_font);
                headerTable2.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(" ", normal_font);
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

                #region Body

                PdfPTable bodyTable = new PdfPTable(7);
                PdfPCell bodyCell = new PdfPCell();

                float[] widthsBody = new float[] { 3f, 7f, 13f, 7f, 7f, 7f, 7f };
                bodyTable.SetWidths(widthsBody);
                bodyTable.WidthPercentage = 100;

                if (viewModel.DOSalesCategory.Equals("SPINNING"))
                {
                    bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    bodyCell.Phrase = new Phrase("No.", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("No. SOP", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Jenis dan Nomor Benang", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Jenis / Code", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Pcs/Roll/Pt", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Mtr/Yds", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Kg/Bale", bold_font);
                    bodyTable.AddCell(bodyCell);
                }
                else if (viewModel.DOSalesCategory.Equals("WEAVING"))
                {
                    bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    bodyCell.Phrase = new Phrase("No.", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("No. SOP", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Jenis dan Nomor Benang", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Grade", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Pcs/Roll/Pt", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Mtr/Yds", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Kg/Bale", bold_font);
                    bodyTable.AddCell(bodyCell);
                }
                else
                {
                    bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    bodyCell.Phrase = new Phrase("No.", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("No. SPP", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Material Konstruksi", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Jenis / Code", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Pcs/Roll/Pt", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Yds/Bale", bold_font);
                    bodyTable.AddCell(bodyCell);

                    bodyCell.Phrase = new Phrase("Mtr/Kg", bold_font);
                    bodyTable.AddCell(bodyCell);
                }



                //bodyCell.Phrase = new Phrase("Total Packing\n(" + viewModel.PackingUom + ")", bold_font);
                //bodyTable.AddCell(bodyCell);

                //bodyCell.Phrase = new Phrase("Total Panjang\n(" + viewModel.LengthUom + ")", bold_font);
                //bodyTable.AddCell(bodyCell);

                //bodyCell.Phrase = new Phrase("Total Berat\n(" + viewModel.WeightUom + ")", bold_font);
                //bodyTable.AddCell(bodyCell);
                if (viewModel.DOSalesCategory.Equals("SPINNING"))
                {

                    foreach (DOSalesDetailViewModel item in viewModel.DOSalesDetailItems)
                    {
                        bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        bodyCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                        bodyCell.Phrase = new Phrase((index++).ToString(), normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(item.NoSOP, normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(item.ThreadNumber, normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(viewModel.PackingUom, normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(string.Format("{0:n0}", item.Packing) + " " + viewModel.PackingUom, normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(string.Format("{0:n0}", item.Length) + " " + viewModel.LengthUom, normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(string.Format("{0:n0}", item.Weight) + " " + viewModel.WeightUom, normal_font);
                        bodyTable.AddCell(bodyCell);
                    }
                }
                else if (viewModel.DOSalesCategory.Equals("WEAVING"))
                {

                    foreach (DOSalesDetailViewModel item in viewModel.DOSalesDetailItems)
                    {
                        bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        bodyCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                        bodyCell.Phrase = new Phrase((index++).ToString(), normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(item.NoSOP, normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(item.ThreadNumber, normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(item.Grade, normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(string.Format("{0:n0}", item.Packing) + " " + viewModel.PackingUom, normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(string.Format("{0:n0}", item.Length) + " " + viewModel.LengthUom, normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(string.Format("{0:n0}", item.Weight) + " " + viewModel.WeightUom, normal_font);
                        bodyTable.AddCell(bodyCell);
                    }
                }
                else
                {

                    foreach (DOSalesDetailViewModel item in viewModel.DOSalesDetailItems)
                    {
                        bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        bodyCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                        bodyCell.Phrase = new Phrase((index++).ToString(), normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(item.ProductionOrder.OrderNo, normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(item.ConstructionName.Replace("\n", string.Empty).Replace(" ", string.Empty), normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(item.UnitOrCode, normal_font);
                        bodyTable.AddCell(bodyCell);

                        bodyCell.Phrase = new Phrase(string.Format("{0:n0}", item.Packing) + " " + viewModel.PackingUom, normal_font);
                        bodyTable.AddCell(bodyCell);

                        if (viewModel.LengthUom == "YDS")
                        {
                            bodyCell.Phrase = new Phrase(string.Format("{0:n0}", item.Length) + " " + viewModel.LengthUom, normal_font);
                            bodyTable.AddCell(bodyCell);

                            bodyCell.Phrase = new Phrase(string.Format("{0:n0}", item.ConvertionValue) + " MTR", normal_font);
                            bodyTable.AddCell(bodyCell);
                        }
                        else
                        {
                            bodyCell.Phrase = new Phrase(string.Format("{0:n0}", item.ConvertionValue) + " " + viewModel.LengthUom, normal_font);
                            bodyTable.AddCell(bodyCell);

                            bodyCell.Phrase = new Phrase(string.Format("{0:n0}", item.Length) + " YDS", normal_font);
                            bodyTable.AddCell(bodyCell);
                        }
                    }
                }

                if (viewModel.DOSalesCategory.Equals("DYEINGPRINTING"))
                {
                    if (viewModel.LengthUom == "YDS")
                    {
                        foreach (DOSalesDetailViewModel total in viewModel.DOSalesDetailItems)
                        {
                            totalPackingQuantity += total.Packing;
                            totalLengthQuantity += total.Length;
                            totalWeightQuantity += total.ConvertionValue;
                        }
                    }
                    else
                    {
                        foreach (DOSalesDetailViewModel total in viewModel.DOSalesDetailItems)
                        {
                            totalPackingQuantity += total.Packing;
                            totalLengthQuantity += total.ConvertionValue;
                            totalWeightQuantity += total.Length;
                        }
                    }


                }
                else
                {
                    foreach (DOSalesDetailViewModel total in viewModel.DOSalesDetailItems)
                    {
                        totalPackingQuantity += total.Packing;
                        totalLengthQuantity += total.Length;
                        totalWeightQuantity += total.Weight;
                    }
                }

                bodyCell.Border = Rectangle.NO_BORDER;
                bodyCell.Phrase = new Phrase("", normal_font);
                bodyTable.AddCell(bodyCell);

                bodyCell.Border = Rectangle.NO_BORDER;
                bodyCell.Phrase = new Phrase("", normal_font);
                bodyTable.AddCell(bodyCell);

                //bodyCell.Colspan = 2;
                bodyCell.Border = Rectangle.NO_BORDER;
                bodyCell.Phrase = new Phrase("", normal_font);
                bodyTable.AddCell(bodyCell);

                //bodyCell.Colspan = 1;
                bodyCell.Border = Rectangle.BOX;
                bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyCell.Phrase = new Phrase("Total", bold_font);
                bodyTable.AddCell(bodyCell);

                //bodyCell.Colspan = 1;
                bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyCell.Phrase = new Phrase(string.Format("{0:n2}", totalPackingQuantity), bold_font);
                bodyTable.AddCell(bodyCell);

                //bodyCell.Colspan = 1;
                bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyCell.Phrase = new Phrase(string.Format("{0:n2}", totalLengthQuantity), bold_font);
                bodyTable.AddCell(bodyCell);

                //bodyCell.Colspan = 1;
                bodyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                bodyCell.Phrase = new Phrase(string.Format("{0:n2}", totalWeightQuantity), bold_font);
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

                cellFooterLeft.Phrase = new Phrase("Disp : " + viewModel?.Disp + "       Op : " + viewModel?.Op + "       Sc : " + viewModel?.Sc, normal_font);
                footerTable.AddCell(cellFooterLeft);

                cellFooterLeft.Colspan = 3;
                cellFooterLeft.Phrase = new Phrase("", bold_font);
                footerTable.AddCell(cellFooterLeft);

                cellFooterLeft.Colspan = 3;
                cellFooterLeft.Phrase = new Phrase("Dikirim Kepada : " + viewModel.DestinationBuyerName, bold_font);
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

            }
            else if (viewModel.DOSalesType == "Ekspor")
            {
                #region Header
                PdfPTable headerTable_A = new PdfPTable(2);
                PdfPTable headerTable_B = new PdfPTable(1);
                PdfPTable headerTable1 = new PdfPTable(1);
                PdfPTable headerTable2 = new PdfPTable(1);
                PdfPTable headerTable3 = new PdfPTable(3);
                PdfPTable headerTable4 = new PdfPTable(2);
                headerTable_A.SetWidths(new float[] { 10f, 10f });
                headerTable_A.WidthPercentage = 100;
                headerTable3.SetWidths(new float[] { 40f, 4f, 100f });
                headerTable3.WidthPercentage = 100;
                headerTable4.SetWidths(new float[] { 10f, 40f });
                headerTable4.WidthPercentage = 100;

                PdfPCell cellHeader1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellHeader2 = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellHeader3 = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellHeader4 = new PdfPCell() { Border = Rectangle.NO_BORDER };
                PdfPCell cellHeaderBody = new PdfPCell() { Border = Rectangle.NO_BORDER };

                cellHeaderBody.Phrase = new Phrase("", normal_font);
                headerTable1.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase($"{viewModel.Type}{viewModel.AutoIncreament.ToString().PadLeft(6, '0')}", header_font);
                headerTable1.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", normal_font);
                headerTable1.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", normal_font);
                headerTable1.AddCell(cellHeaderBody);

                cellHeader1.AddElement(headerTable1);
                headerTable_A.AddCell(cellHeader1);

                cellHeaderBody.HorizontalAlignment = Element.ALIGN_LEFT;

                cellHeaderBody.Phrase = new Phrase("", header_font);
                headerTable2.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("KERTAS KERJA EKSPORT", header_bold_font);
                headerTable2.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", header_font);
                headerTable2.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", header_font);
                headerTable2.AddCell(cellHeaderBody);

                cellHeader2.AddElement(headerTable2);
                headerTable_A.AddCell(cellHeader2);

                document.Add(headerTable_A);

                cellHeaderBody.HorizontalAlignment = Element.ALIGN_LEFT;

                cellHeaderBody.Phrase = new Phrase("", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", bold_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", bold_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("Dikerjakan Oleh ", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(":", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(viewModel.DoneBy, normal_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("1. Order Untuk ", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(":", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(viewModel.SalesContract.Material.Name + " / " + viewModel.SalesContract.MaterialConstruction.Name + " / " + viewModel.SalesContract.MaterialWidth + "            " + viewModel.SalesContract.SalesContractNo, normal_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("2. Jenis Untuk ", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(":", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(viewModel.SalesContract.Buyer.Name, normal_font);
                headerTable3.AddCell(cellHeaderBody);

                double total_weight = 0;

                foreach (DOSalesDetailViewModel item in viewModel.DOSalesDetailItems)
                {
                    total_weight += item.Packing;
                }

                cellHeaderBody.Phrase = new Phrase("3. Jumlah Kirim ", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(":", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(total_weight + " " + viewModel.PackingUom, normal_font);
                headerTable3.AddCell(cellHeaderBody);

                //cellHeaderBody.Phrase = new Phrase("3. Jumlah Order ", bold_font);
                //headerTable3.AddCell(cellHeaderBody);
                //cellHeaderBody.Phrase = new Phrase(":", bold_font);
                //headerTable3.AddCell(cellHeaderBody);
                //cellHeaderBody.Phrase = new Phrase(viewModel.SalesContract.OrderQuantity + " METER", normal_font);
                //headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("4. Piece Length ", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(":", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(viewModel.SalesContract.PieceLength, normal_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("5. Cap Komposisi Persen ", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(":", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(viewModel.SalesContract.Commodity.Name, normal_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("6. Isi tiap Bale", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(":", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(viewModel.FillEachBale.GetValueOrDefault().ToString("N2") + " " + viewModel.BaleUom, normal_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeaderBody.Phrase = new Phrase("CATATAN KETERANGAN ", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(":", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase("", bold_font);
                headerTable3.AddCell(cellHeaderBody);
                //foreach (DOSalesDetailViewModel item in viewModel.DOSalesDetailItems)
                //{
                //    cellHeaderBody.Phrase = new Phrase("\n" + item.ProductionOrder.OrderNo + " (" + item.ColorRequest + ")", normal_font);
                //    //headerTable3.AddCell(cellHeaderBody);
                //}
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(" ", normal_font);
                headerTable3.AddCell(cellHeaderBody);

                cellHeader3.AddElement(headerTable3);
                headerTable_B.AddCell(cellHeader3);

                cellHeader4.AddElement(headerTable4);
                headerTable_B.AddCell(cellHeader4);

                document.Add(headerTable_B);

                #endregion Header

                #region Body

                PdfPTable bodyTable = new PdfPTable(1);
                PdfPCell bodyCell = new PdfPCell() { Border = Rectangle.NO_BORDER };

                float[] widthsBody = new float[] { 1f };
                bodyTable.SetWidths(widthsBody);
                bodyTable.WidthPercentage = 80;

                foreach (DOSalesDetailViewModel item in viewModel.DOSalesDetailItems)
                {
                    bodyCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    bodyCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                    bodyCell.Phrase = new Phrase(item.ProductionOrder.OrderNo + " " + item.ColorRequest, bold_font);
                    bodyTable.AddCell(bodyCell);
                }
                bodyCell.Phrase = new Phrase("", bold_font);
                bodyTable.AddCell(bodyCell);
                bodyCell.Phrase = new Phrase("", bold_font);
                bodyTable.AddCell(bodyCell);
                bodyCell.Phrase = new Phrase("", bold_font);
                bodyTable.AddCell(bodyCell);
                bodyCell.Phrase = new Phrase(viewModel.Remark, bold_font);
                bodyTable.AddCell(bodyCell);
                bodyCell.Phrase = new Phrase("", bold_font);
                bodyTable.AddCell(bodyCell);
                bodyCell.Phrase = new Phrase("", bold_font);
                bodyTable.AddCell(bodyCell);
                bodyCell.Phrase = new Phrase("", bold_font);
                bodyTable.AddCell(bodyCell);
                bodyCell.Phrase = new Phrase("", bold_font);
                bodyTable.AddCell(bodyCell);
                bodyCell.Phrase = new Phrase("", bold_font);
                bodyTable.AddCell(bodyCell);

                document.Add(bodyTable);

                #endregion Body

                #region Footer

                PdfPTable footerTable = new PdfPTable(1);

                PdfPTable signatureTable = new PdfPTable(2) { HorizontalAlignment = Element.ALIGN_CENTER };
                PdfPCell signatureCell = new PdfPCell() { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };

                float[] widthsSignanture = new float[] { 10f, 10f };
                signatureTable.SetWidths(widthsSignanture);
                signatureTable.WidthPercentage = 80;

                signatureCell.Phrase = new Phrase("Diterima Tgl. ......................", normal_font);
                signatureTable.AddCell(signatureCell);
                signatureCell.Phrase = new Phrase("Sukoharjo, " + viewModel.Date?.AddHours(clientTimeZoneOffset).ToString("dd MMMM yyyy", new CultureInfo("id-ID")) + "\n\nParaf Pembuat,", normal_font);
                signatureTable.AddCell(signatureCell);

                signatureTable.AddCell(new PdfPCell()
                {
                    Phrase = new Phrase("--------------------------------", normal_font),
                    FixedHeight = 50,
                    VerticalAlignment = Element.ALIGN_BOTTOM,
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                signatureTable.AddCell(new PdfPCell()
                {
                    Phrase = new Phrase("--------------------------------", normal_font),
                    FixedHeight = 50,
                    VerticalAlignment = Element.ALIGN_BOTTOM,
                    HorizontalAlignment = Element.ALIGN_CENTER
                });

                footerTable.AddCell(new PdfPCell(signatureTable));
                document.Add(footerTable);

                #endregion Footer
            }

            document.Close();

            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }
    }
}
