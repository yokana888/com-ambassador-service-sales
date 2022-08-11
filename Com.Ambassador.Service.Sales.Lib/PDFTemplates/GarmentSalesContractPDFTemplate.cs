using Com.Ambassador.Service.Sales.Lib.BusinessLogic.Interface.GarmentSalesContractInterface;
using Com.Ambassador.Service.Sales.Lib.Helpers;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Com.Ambassador.Service.Sales.Lib.ViewModels.GarmentSalesContractViewModels;
using Com.Ambassador.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Com.Ambassador.Service.Sales.Lib.PDFTemplates
{
    public class GarmentSalesContractPDFTemplate
    {
        public MemoryStream GeneratePdfTemplate(GarmentSalesContractViewModel viewModel, IGarmentSalesContract facade, int timeoffset, Dictionary<string, object> buyer, Dictionary<string, object> bank)
        {
            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 18);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 7);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 7);

            Document document = new Document(PageSize.A4, 40, 40, 120, 40);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            writer.PageEvent = new GarmentSalesContractPDFTemplatePageEvent();


            document.Open();


            #region Header

            string codeNoString = "FM-00-PJ-02-001/R1";
            Paragraph codeNo = new Paragraph(codeNoString, bold_font) { Alignment = Element.ALIGN_RIGHT };
            codeNo.SpacingAfter = 10f;
            document.Add(codeNo);

            PdfPTable tableHeader = new PdfPTable(4);
            tableHeader.SetWidths(new float[] { 1f,0.2f, 3f, 3f });

            PdfPCell cellHeaderContentLeft = new PdfPCell() { Border = Rectangle.NO_BORDER , HorizontalAlignment = Element.ALIGN_LEFT };
            cellHeaderContentLeft.Phrase=new Phrase("R/O NO", normal_font);
            tableHeader.AddCell(cellHeaderContentLeft);
            cellHeaderContentLeft.Phrase = new Phrase(": ", normal_font);
            tableHeader.AddCell(cellHeaderContentLeft);
            cellHeaderContentLeft.Phrase=new Phrase(viewModel.RONumber, normal_font);
            tableHeader.AddCell(cellHeaderContentLeft);

            DateTime date = viewModel.CreatedUtc.AddHours(timeoffset);
            PdfPCell cellHeaderContentRight = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            cellHeaderContentRight.Phrase=new Phrase("DATE, " + date.ToString("MMMM dd, yyyy"), normal_font);
            tableHeader.AddCell(cellHeaderContentRight);

            cellHeaderContentLeft.Phrase=new Phrase("MESSR.", normal_font);
            tableHeader.AddCell(cellHeaderContentLeft);
            cellHeaderContentLeft.Phrase = new Phrase(": ", normal_font);
            tableHeader.AddCell(cellHeaderContentLeft);
            cellHeaderContentLeft.Phrase=new Phrase( viewModel.BuyerBrandName, normal_font);
            tableHeader.AddCell(cellHeaderContentLeft);


            cellHeaderContentLeft.Phrase=new Phrase("", normal_font);
            tableHeader.AddCell(cellHeaderContentLeft);
            cellHeaderContentLeft.Phrase=new Phrase("", normal_font);
            tableHeader.AddCell(cellHeaderContentLeft);
            cellHeaderContentLeft.Phrase = new Phrase("", normal_font);
            tableHeader.AddCell(cellHeaderContentLeft);
            string buyerAddress = buyer["Address"] !=null? buyer["Address"].ToString() : "";
            cellHeaderContentLeft.Phrase=new Phrase( buyerAddress, normal_font);
            tableHeader.AddCell(cellHeaderContentLeft);

            cellHeaderContentLeft.Phrase = new Phrase("", normal_font);
            tableHeader.AddCell(cellHeaderContentLeft);

            PdfPCell cellHeader = new PdfPCell(tableHeader);
            tableHeader.ExtendLastRow = false;
            tableHeader.SpacingAfter = 15f;
            document.Add(tableHeader);

            string titleString = "SALES CONTRACT";
            bold_font.SetStyle(Font.UNDERLINE);
            Paragraph title = new Paragraph(titleString, bold_font) { Alignment = Element.ALIGN_CENTER };
           // title.SpacingAfter = 20f;
            document.Add(title);
            bold_font.SetStyle(Font.NORMAL);

            //string codeNoString = "FM-00-PJ-02-001/R1";
            Paragraph scNo = new Paragraph(viewModel.SalesContractNo, bold_font) { Alignment = Element.ALIGN_CENTER };
            scNo.SpacingAfter = 10f;
            document.Add(scNo);
            #endregion


            #region body
            PdfPTable tableBody = new PdfPTable(3);
            tableBody.SetWidths(new float[] { 0.75f,0.1f, 2f });
            PdfPCell bodyContentCenter = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            PdfPCell bodyContentLeft = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell bodyContentRight = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            bodyContentLeft.Phrase = new Phrase("Description of Goods", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": ");
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(viewModel.Description, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Material", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": ");
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase( viewModel.Material, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Packing", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": ");
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase( viewModel.Packing, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Style/Order/Article No.", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " );
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase( viewModel.Article, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Quantity", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " , normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase( viewModel.Quantity + " " + viewModel.Uom.Unit, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Unit Price", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": ", normal_font);
            tableBody.AddCell(bodyContentLeft);
            if(viewModel.Items.Count > 0)
            {
                bodyContentLeft.Phrase = new Phrase(viewModel.FOB, normal_font);
                tableBody.AddCell(bodyContentLeft);
            }
            else
            {
                bodyContentLeft.Phrase = new Phrase(viewModel.FOB + " US$ " + string.Format("{0:n2}", viewModel.Price) + " /" + viewModel.Uom.Unit, normal_font);
                tableBody.AddCell(bodyContentLeft);
            }
            

            if (viewModel.Items.Count > 0)
            {
                foreach(var item in viewModel.Items)
                {
                    bodyContentLeft.Phrase = new Phrase("", normal_font);
                    tableBody.AddCell(bodyContentLeft);
                    bodyContentLeft.Phrase = new Phrase("", normal_font);
                    tableBody.AddCell(bodyContentLeft);
                    bodyContentLeft.Phrase = new Phrase( item.Description + "  : " + item.Quantity + " "+ viewModel.Uom.Unit + " | US$ " + string.Format("{0:n2}", item.Price) + " / " + viewModel.Uom.Unit, normal_font);
                    tableBody.AddCell(bodyContentLeft);

                }
            }

            bodyContentLeft.Phrase = new Phrase("Total Amount", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": ", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("US$ " + string.Format("{0:n2}",viewModel.Amount) , normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Shipment/Delivery", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": " , normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase( viewModel.DeliveryDate.ToOffset(new TimeSpan(timeoffset, 0, 0)).ToString("dd/MM/yyyy") + " " + viewModel.Delivery, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Destination", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": ", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(viewModel.Country, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("H.S.No.", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": ", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(viewModel.NoHS, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Payment", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": ", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase( viewModel.PaymentDetail, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Manufacturer", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": ", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("PT.DAN LIRIS", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("ADDRESS : JL. MERAPI NO. 23, KELURAHAN BANARAN, KECAMATAN GROGOL \n                    KABUPATEN SUKOHARJO, JAWA TENGAH 57552, INDONESIA", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("Bank Detail", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase(": ", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase( viewModel.AccountBank.BankName, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            string bankAddress = bank["BankAddress"] != null ? bank["BankAddress"].ToString() : "";
            bodyContentLeft.Phrase = new Phrase("ADDRESS : " + bankAddress, normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("", normal_font);
            tableBody.AddCell(bodyContentLeft);
            bodyContentLeft.Phrase = new Phrase("SWIFTCODE : " + bank["SwiftCode"], normal_font);
            tableBody.AddCell(bodyContentLeft);

            if(!viewModel.IsTTPayment)
            {
                bodyContentLeft.Phrase = new Phrase("DOCUMENT PRESENTED", normal_font);
                tableBody.AddCell(bodyContentLeft);
                bodyContentLeft.Phrase = new Phrase(": ", normal_font);
                tableBody.AddCell(bodyContentLeft);
                bodyContentLeft.Phrase = new Phrase(viewModel.DocPresented, normal_font);
                tableBody.AddCell(bodyContentLeft);
            }

            PdfPCell cellBody = new PdfPCell(tableBody); // dont remove
            tableBody.ExtendLastRow = false;
            tableBody.SpacingAfter = 10f;
            document.Add(tableBody);

            #endregion

            #region REMARKS

            Paragraph remark = new Paragraph("REMARKS  : ", bold_font) { Alignment = Element.ALIGN_LEFT };
            remark.SpacingAfter = 5f;
            document.Add(remark);

            PdfPTable remarks = new PdfPTable(2);
            remarks.SetWidths(new float[] { 0.5f, 10f });
            PdfPCell cell_remark = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_TOP, Padding = 2 };

            cell_remark.Phrase = new Phrase("1)", normal_font);
            remarks.AddCell(cell_remark);
            cell_remark.Phrase = new Phrase("FULL ORDER DETAILS REGARDING SIZE/COLOUR BREAKDOWN, LABELLING, PACKING, ALL ACCESSORIES, AND ANY OTHER DETAILS TO BE RECEIVED BY SELLER MINIMUM FOR SOLID 3 MONTHS, PRINTED 3,5 MONTHS AND YARN DYED 4 MONTHS PRIOR TO DELIVERY DATE.", normal_font);
            remarks.AddCell(cell_remark);
            cell_remark.Phrase = new Phrase("2)", normal_font);
            remarks.AddCell(cell_remark);
            cell_remark.Phrase = new Phrase("SELLER IS NOT TO BE LIABLE FOR ANY EXTRA COST INCURRED DUE TO ANY EXTRA ATTRIBUTES/ACCESSORIES SENT BY BUYER OR COMING IN TO INDONESIA AS REQUIRED BY BUYER. SUCH EXPENSES, IF ANY, ARE TO BE CHARGED FOR BUYER'S ACCOUNT.", normal_font);
            remarks.AddCell(cell_remark);
            cell_remark.Phrase = new Phrase("3)", normal_font);
            remarks.AddCell(cell_remark);
            cell_remark.Phrase = new Phrase("SELLER WILL NOT BE HELD RESPONSIBLE FOR LATE DELIVERY CAUSED BY BUYER ASKING FOR ANY AND OR ACCESSORIES THAT HAS TO BE IMPORTED INTO INDONESIA FOR THEIR ORDER.", normal_font);
            remarks.AddCell(cell_remark);
            cell_remark.Phrase = new Phrase("4)", normal_font);
            remarks.AddCell(cell_remark);
            cell_remark.Phrase = new Phrase("L/C MUST BE OPENED MINIMUM THREE MONTHS PRIOR TO DELIVERY DATE.", normal_font);
            remarks.AddCell(cell_remark);
            cell_remark.Phrase = new Phrase("5)", normal_font);
            remarks.AddCell(cell_remark);
            cell_remark.Phrase = new Phrase("IN CASE L/C AMENDMENT (S) REQUIRED DUE TO MISTAKE OR NEGLECT ON BUYER'S SIDE, ANY CHARGES INCURRED IS TO BE BORNE BY BUYER. ", normal_font);
            remarks.AddCell(cell_remark);
            cell_remark.Phrase = new Phrase("6)", normal_font);
            remarks.AddCell(cell_remark);
            cell_remark.Phrase = new Phrase("CONDITION OF LETTER OF CREDIT SHOULD CONFORM TO THE INDONESIAN BANKING AND GOVERNMENT EXPORT REGULATIONS AS PER OUR ENCLOSURE, OF WHICH IT FORMS AN INTEGRAL PART.", normal_font);
            remarks.AddCell(cell_remark);

            PdfPCell remarkCell = new PdfPCell(remarks); // dont remove
            remarks.ExtendLastRow = false;
            remarks.SpacingAfter = 10f;
            document.Add(remarks);
            #endregion

            #region signature

            string LineSeparator = "=============================================================================================================================";
            Paragraph separator = new Paragraph(LineSeparator, bold_font) { Alignment = Element.ALIGN_CENTER };
            separator.SpacingAfter = 10f;
            document.Add(separator);

            PdfPTable sign = new PdfPTable(2);
            sign.SetWidths(new float[] { 10f, 3f });
            PdfPCell cell_signatureLeft = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_TOP, Padding = 2 };
            PdfPCell cell_signatureRight = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_TOP, Padding = 2 };

            cell_signatureLeft.Phrase = new Phrase("CONFIRMED BY BUYER :", normal_font);
            sign.AddCell(cell_signatureLeft);
            cell_signatureRight.Phrase = new Phrase("SELLER", normal_font);
            sign.AddCell(cell_signatureRight);


            cell_signatureLeft.Phrase = new Phrase("", normal_font);
            sign.AddCell(cell_signatureLeft);
            cell_signatureRight.Phrase = new Phrase("PT. DAN LIRIS", normal_font);
            sign.AddCell(cell_signatureRight);

            cell_signatureLeft.Phrase = new Phrase("", normal_font);
            sign.AddCell(cell_signatureLeft);
            cell_signatureRight.Phrase = new Phrase("", normal_font);
            sign.AddCell(cell_signatureRight);

            cell_signatureLeft.Phrase = new Phrase("", normal_font);
            sign.AddCell(cell_signatureLeft);
            cell_signatureRight.Phrase = new Phrase("", normal_font);
            sign.AddCell(cell_signatureRight);

            cell_signatureLeft.Phrase = new Phrase("", normal_font);
            sign.AddCell(cell_signatureLeft);
            cell_signatureRight.Phrase = new Phrase("", normal_font);
            sign.AddCell(cell_signatureRight);

            cell_signatureLeft.Phrase = new Phrase("", normal_font);
            sign.AddCell(cell_signatureLeft);
            cell_signatureRight.Phrase = new Phrase("", normal_font);
            sign.AddCell(cell_signatureRight);

            cell_signatureLeft.Phrase = new Phrase("", normal_font);
            sign.AddCell(cell_signatureLeft);
            cell_signatureRight.Phrase = new Phrase("_______________", normal_font);
            sign.AddCell(cell_signatureRight);

            cell_signatureLeft.Phrase = new Phrase(" ", normal_font);
            sign.AddCell(cell_signatureLeft);
            cell_signatureRight.Phrase = new Phrase("GENERAL MANAGER", normal_font);
            sign.AddCell(cell_signatureRight);


            PdfPCell signCell = new PdfPCell(sign); // dont remove
            sign.ExtendLastRow = false;
            sign.SpacingAfter = 10f;
            document.Add(sign);
            #endregion

            #region LOC
            if (!viewModel.IsTTPayment)
            {
                document.NewPage();
                //document.Add(bankSpace);

                Paragraph LocTitle = new Paragraph("CONDITION OF LETTER OF CREDIT (L/C)", bold_font) { Alignment = Element.ALIGN_CENTER };
                LocTitle.SpacingAfter = 5f;
                document.Add(LocTitle);

                Paragraph loc = new Paragraph("THE FOLLOWING CONDITION ARE REQUESTED TO BE INCLUDED IN YOUR LETTER OF CREDIT. OTHERWISE THERE WILL BE DELAYED IN OUR SHIPMENT BECAUSE OF AMENDMENT WE REQUIRE TO MEET THE INDONESIAN BANKING AND GOVERNMENTAL EXPORT REGULATION. \n THEREFORE PLEASE INSTRUCT YOUR BANK TO INCLUDE THE BELOW MENTIONED CONDITIONS IN YOUR LETTER OF CREDIT TO US : ", normal_font) { Alignment = Element.ALIGN_LEFT };
                loc.SpacingAfter = 5f;
                document.Add(loc);

                PdfPTable LocList = new PdfPTable(2);
                LocList.SetWidths(new float[] { 0.5f, 10f });
                PdfPCell cell_loc= new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_TOP, Padding = 2 };

                cell_loc.Phrase = new Phrase("A.", normal_font);
                LocList.AddCell(cell_loc);
                cell_loc.Phrase = new Phrase(" IRREVOCABLE", normal_font);
                LocList.AddCell(cell_loc);

                cell_loc.Phrase = new Phrase("B.", normal_font);
                LocList.AddCell(cell_loc);
                cell_loc.Phrase = new Phrase(" TRANSFERABLE OR NEGOTIABLE THROUGH ANY BANK IN INDONESIA", normal_font);
                LocList.AddCell(cell_loc);

                cell_loc.Phrase = new Phrase("C.", normal_font);
                LocList.AddCell(cell_loc);
                cell_loc.Phrase = new Phrase(" DRAWN AT SIGHT", normal_font);
                LocList.AddCell(cell_loc);



                cell_loc.Phrase = new Phrase("D.", normal_font);
                LocList.AddCell(cell_loc);
                cell_loc.Phrase = new Phrase(" BENEFICIARY      : PT. DAN LIRIS \n                                 ADDRESS    : JL. MERAPI No. 23, KELURAHAN BANARAN, KECAMATAN GROGOL \n                                                        KABUPATEN SUKOHARJO, JAWA TENGAH 57552, INDONESIA", normal_font);
                LocList.AddCell(cell_loc);


                cell_loc.Phrase = new Phrase("E.", normal_font);
                LocList.AddCell(cell_loc);
                cell_loc.Phrase = new Phrase("ADVISING BANK     : " + bank["BankName"], normal_font);
                LocList.AddCell(cell_loc);


                cell_loc.Phrase = new Phrase("F.", normal_font);
                LocList.AddCell(cell_loc);
                cell_loc.Phrase = new Phrase("CLEARLY STATE     : PARTIAL SHIPMENT ALLOWED / PROHIBITED", normal_font);
                LocList.AddCell(cell_loc);


                cell_loc.Phrase = new Phrase("G.", normal_font);
                LocList.AddCell(cell_loc);
                cell_loc.Phrase = new Phrase("TRANSHIPMENT ALLOWED", normal_font);
                LocList.AddCell(cell_loc);

                cell_loc.Phrase = new Phrase("H.", normal_font);
                LocList.AddCell(cell_loc);
                cell_loc.Phrase = new Phrase("BILL OF LANDING SHOWING BENEFICIARY AS SHIPPER AND MADE OUT TO THE ORDER OF NEGOTIATING BANK AND TO BE ENDORSED TO THE ORDER OF THE CREDIT OPENING BANK MARKED 'FREIGHT COLLECT' AND NOTIFY ..........................", normal_font);
                LocList.AddCell(cell_loc);

                cell_loc.Phrase = new Phrase("I.", normal_font);
                LocList.AddCell(cell_loc);
                cell_loc.Phrase = new Phrase("AIRWAY BILL ISSUED AND SIGNED BY THE CARRIER OF HIS AGENT REQUIRED MARKED 'FREIGHT COLLECT' AND NOTIFY.........................", normal_font);
                LocList.AddCell(cell_loc);

                cell_loc.Phrase = new Phrase("J.", normal_font);
                LocList.AddCell(cell_loc);
                cell_loc.Phrase = new Phrase("FORWARDED BILL OF LANDING", normal_font);
                LocList.AddCell(cell_loc);

                cell_loc.Phrase = new Phrase("K.", normal_font);
                LocList.AddCell(cell_loc);
                cell_loc.Phrase = new Phrase("EXPIRY DATE: 21 DAYS AFTER LATEST DATE OF SHIPMENT", normal_font);
                LocList.AddCell(cell_loc);

                cell_loc.Phrase = new Phrase("L.", normal_font);
                LocList.AddCell(cell_loc);
                cell_loc.Phrase = new Phrase("PLUS OR MINUS 5 PERCENT BOTH IN QUANTITY AND AMOUNT ACCEPTABLE", normal_font);
                LocList.AddCell(cell_loc);

                cell_loc.Phrase = new Phrase("M.", normal_font);
                LocList.AddCell(cell_loc);
                cell_loc.Phrase = new Phrase("DOCUMENTS TO BE PRESENTED WITHIN 21 DAYS AFTER THE DATE OF ISSUANCE BILL OF LANDING / AIRWAY BILL OR OTHER SHIPPING DOCUMENTS (S)", normal_font);
                LocList.AddCell(cell_loc);

                cell_loc.Phrase = new Phrase("N.", normal_font);
                LocList.AddCell(cell_loc);
                cell_loc.Phrase = new Phrase("SHIPMENT EXECUTED FROM ANY INDONESIA PORT / AIRPORT", normal_font);
                LocList.AddCell(cell_loc);

                cell_loc.Phrase = new Phrase("O.", normal_font);
                LocList.AddCell(cell_loc);
                cell_loc.Phrase = new Phrase("THIRD PARTY SHIPPER AND DOCUMENTS ARE ACCEPTABLE", normal_font);
                LocList.AddCell(cell_loc);

                cell_loc.Phrase = new Phrase("P.", normal_font);
                LocList.AddCell(cell_loc);
                cell_loc.Phrase = new Phrase("TELEGRAPHIC TRANSFER REIMBURSEMENT IS ALLOWED", normal_font);
                LocList.AddCell(cell_loc);

                #region
                ////another table
                //PdfPTable ALocList = new PdfPTable(2);
                //ALocList.SetWidths(new float[] {1f, 5f });
                //PdfPCell acell_loc = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_TOP, Padding = 2 };


                //    acell_loc.Phrase = new Phrase(" BENEFICIARY ", normal_font);
                //    ALocList.AddCell(acell_loc);

                //    acell_loc.Phrase = new Phrase(": PT. DAN LIRIS", normal_font);
                //    ALocList.AddCell(acell_loc);

                //    acell_loc.Phrase = new Phrase("  ", normal_font);
                //    ALocList.AddCell(acell_loc);

                //    acell_loc.Phrase = new Phrase("  ADDRESS    : KELURAHAN BANARAN, KECAMATAN GROGOL", normal_font);
                //    ALocList.AddCell(acell_loc);

                //    acell_loc.Phrase = new Phrase("  ", normal_font);
                //    ALocList.AddCell(acell_loc);

                //    acell_loc.Phrase = new Phrase("               KELURAHAN BANARAN, KECAMATAN GROGOL", normal_font);
                //    ALocList.AddCell(acell_loc);

                //    acell_loc.Phrase = new Phrase("ADVISING BANK", normal_font);
                //    ALocList.AddCell(acell_loc);

                //    acell_loc.Phrase = new Phrase(": "+ bank["BankName"], normal_font);
                //    ALocList.AddCell(acell_loc);


                //    acell_loc.Phrase = new Phrase("CLEARLY STATE", normal_font);
                //    ALocList.AddCell(acell_loc);

                //    acell_loc.Phrase = new Phrase(": PARTIAL SHIPMENT ALLOWED / PROHIBITED" , normal_font);
                //    ALocList.AddCell(acell_loc);

                //PdfPCell AlocCell = new PdfPCell(LocList); // dont remove
                //ALocList.ExtendLastRow = false;
                //LocList.AddCell(ALocList);
                #endregion
                PdfPCell locCell = new PdfPCell(LocList); // dont remove
                LocList.ExtendLastRow = false;
                LocList.SpacingAfter = 10f;
                document.Add(LocList);
            }
            #endregion

            #region printedON
            string printed = "PRINTED ON : " + DateTime.Now.ToString("MMMM dd, yyyy / HH:mm: ss");
            Paragraph printedOn = new Paragraph(printed, normal_font) { Alignment = Element.ALIGN_LEFT };
            printedOn.SpacingBefore = 20f;
            printedOn.SpacingAfter = 10f;
            document.Add(printedOn);
            #endregion

            //string pages = document.PageNumber.ToString();
            //Paragraph page = new Paragraph(pages, normal_font) { Alignment = Element.ALIGN_CENTER };
            //document.Add(page);
            
            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }

    }

    class GarmentSalesContractPDFTemplatePageEvent : PDFPages
    {
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            PdfContentByte cb = writer.DirectContent;
            cb.BeginText();

            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);

            float height = writer.PageSize.Height, width = writer.PageSize.Width;
            float marginLeft = document.LeftMargin - 10, marginTop = document.TopMargin, marginRight = document.RightMargin - 10;

            cb.SetFontAndSize(bf, 6);

            #region LEFT

            var branchOfficeY = height - marginTop + 50;
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "BRANCH OFFICE :", marginLeft, branchOfficeY, 0);
            string[] branchOffices = {
                "Equity Tower 15th Floor Suite C",
                "Sudirman Central Business District (SCBD) Lot 9",
                "Jl. Jend. Sudirman Kav 52-53 Jakarta 12190",
                "Tel. : (62-21) 2903-5388. 2903-5389 (Hunting)",
                "Fax. : (62-21) 2903-5398",
            };
            for (int i = 0; i < branchOffices.Length; i++)
            {
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, branchOffices[i], marginLeft, branchOfficeY - 10 - (i * 8), 0);
            }

            #endregion

            #region CENTER

            var headOfficeX = width / 2 + 30;
            var headOfficeY = height - marginTop + 45;

            byte[] imageByte = Convert.FromBase64String(Base64ImageStrings.LOGO_NAME);
            Image image = Image.GetInstance(imageByte);
            if (image.Width > 160)
            {
                float percentage = 0.0f;
                percentage = 160 / image.Width;
                image.ScalePercent(percentage * 100);
            }
            image.SetAbsolutePosition(headOfficeX - (image.ScaledWidth / 2), headOfficeY);
            cb.AddImage(image, inlineImage: true);

            string[] headOffices = {
                "Head Office : JL. MERAPI NO. 23, BANARAN, GROGOL, SUKOHARJO JAWA TENGAH 57552 - INDONESIA",
                "TELP. 0271-740888, 714400 (HUNTING), FAX. : 0271-735222, 740777, PO BOX 166 SOLO 57100",
                "Website : www.Ambassador.com",
            };
            for (int i = 0; i < headOffices.Length; i++)
            {
                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, headOffices[i], headOfficeX, headOfficeY - image.ScaledHeight - (i * 10), 0);
            }

            #endregion

            #region RIGHT

            byte[] imageByteIso = Convert.FromBase64String(Base64ImageStrings.ISO);
            Image imageIso = Image.GetInstance(imageByteIso);
            if (imageIso.Width > 80)
            {
                float percentage = 0.0f;
                percentage = 80 / imageIso.Width;
                imageIso.ScalePercent(percentage * 100);
            }
            imageIso.SetAbsolutePosition(width - imageIso.ScaledWidth - marginRight, height - imageIso.ScaledHeight - marginTop + 60);
            cb.AddImage(imageIso, inlineImage: true);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "CERTIFICATE ID09 / 01238", width - (imageIso.ScaledWidth / 2) - marginRight, height - imageIso.ScaledHeight - marginTop + 60 - 5, 0);

            #endregion

            #region LINE

            cb.MoveTo(marginLeft, height - marginTop);
            cb.LineTo(width - marginRight, height - marginTop - 1);
            cb.Stroke();

            #endregion

            #region WATERMARK

            byte[] imageByteCenter = Convert.FromBase64String(Base64ImageStrings.LOGO_WATERMARK);
            Image imageCenter = Image.GetInstance(imageByteCenter);
            float percentageImageCenter = 0.0f;
            percentageImageCenter = 400 / imageCenter.Width;
            imageCenter.ScalePercent(percentageImageCenter * 100);
            imageCenter.SetAbsolutePosition((width / 2) - imageCenter.ScaledWidth / 2, (height / 2) - imageCenter.ScaledHeight / 2);
            //cb.AddImage(imageCenter);
            document.Add(imageCenter);

            #endregion

            cb.EndText();
        }
    }
}

