using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;
using CF_HOATUOIBASANH.Interface;
using CF_HOATUOIBASANH.Models;

namespace CF_HOATUOIBASANH.Interfaces
{
    public class PdfService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;

        public PdfService(IProductRepository productRepository, ICustomerRepository customerRepository)
        {
            _productRepository = productRepository;
            _customerRepository = customerRepository;
        }

        public async Task<Document> GenerateInvoicePdf(Order order, List<DetailOrder> orderDetails)
        {
            Document pdfDocument = new Document();
            Page page = pdfDocument.Pages.Add();
            Table infoTable = new Table();

            // Add order information
            TextFragment text = new TextFragment();
            TextFragment title = new TextFragment("HÓA ĐƠN BÁN HÀNG");

            title.TextState.FontStyle = FontStyles.Bold;
            title.TextState.FontSize = 24;

            title.HorizontalAlignment = HorizontalAlignment.Center;

            page.Paragraphs.Add(title);
            page.Paragraphs.Add(text);
            Customer customer = await _customerRepository.GetCustomerByIdAsync(order.CustomerID);
            text = new TextFragment($"Tên khách hàng: {customer.FirstName} {customer.LastName}\n");
            page.Paragraphs.Add(text);
            text = new TextFragment($"Mã đơn hàng: {order.OrderID}\n");
            page.Paragraphs.Add(text);
            text = new TextFragment($"Địa chỉ giao hàng: {order.ShipAddress}\n");
            page.Paragraphs.Add(text);
            text = new TextFragment($"Hình thức thanh toán: {order.PayMethod}\n");
            page.Paragraphs.Add(text);
            text = new TextFragment($"Trạng thái thanh toán: {order.PayStatus}\n");
            page.Paragraphs.Add(text);
            text = new TextFragment($"Trạng thái giao hàng: {order.ShipStatus}\n");
            page.Paragraphs.Add(text);
            text = new TextFragment($"Ngày tạo: {order.CreateDate.ToString("dd/MM/yyyy")}\n");
            page.Paragraphs.Add(text);

            Table table = new Table();
            table.ColumnWidths = "45 195 40 75 75";

            Row headerRow = table.Rows.Add();
            headerRow.Cells.Add("Mã SP");
            headerRow.Cells.Add("Tên sản phẩm");
            headerRow.Cells.Add("Số lượng");
            headerRow.Cells.Add("Giá");
            headerRow.Cells.Add("Thành tiền");
            headerRow.FixedRowHeight = 20; 

            foreach (Cell cell in headerRow.Cells)
            {
                cell.Border = new BorderInfo(BorderSide.All, 0.1f);
            }

            foreach (Cell cell in headerRow.Cells)
            {
                cell.Alignment = HorizontalAlignment.Center;
            }

            decimal? totalAmount = 0;

            foreach (var detail in orderDetails)
            {
                var product = _productRepository.GetById(detail.ProductID);
                Row dataRow = table.Rows.Add();
                dataRow.Cells.Add(detail.ProductID.ToString());
                dataRow.Cells.Add(product.ProductName);
                dataRow.Cells.Add(detail.Quantity.ToString());
                dataRow.Cells.Add($"{product.Price:N0}đ");
                dataRow.Cells.Add($"{(product.Price * detail.Quantity):N0}đ");
                dataRow.FixedRowHeight = 20;

                decimal? price = (decimal?)(product.Price ?? 0); // Chuyển đổi decimal? sang decimal với giá trị mặc định là 0 nếu null
                totalAmount += price * detail.Quantity;
                foreach (Cell cell in dataRow.Cells)
                {
                    cell.Border = new BorderInfo(BorderSide.All, 0.1f);
                    cell.Alignment = HorizontalAlignment.Center;

                }
            }


            page.Paragraphs.Add(table);
            page.Paragraphs.Add(new TextFragment("\n"));

            // Add total amounts
            text = new TextFragment($"Tổng tiền hàng: {totalAmount:N0}đ\n");
            text.HorizontalAlignment = HorizontalAlignment.Right;
            page.Paragraphs.Add(text);

            text = new TextFragment($"Tổng tiền ship: {order.ShipCost:N0}đ\n");
            text.HorizontalAlignment = HorizontalAlignment.Right;
            page.Paragraphs.Add(text);

            text = new TextFragment($"Thành tiền: {order.TotalAmount:N0}đ\n");
            text.HorizontalAlignment = HorizontalAlignment.Right;
            page.Paragraphs.Add(text);


            return pdfDocument;
        }


    }
}
