using CF_HOATUOIBASANH.Models;
using MailKit.Security;
using MimeKit;
using System.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CF_HOATUOIBASANH.Interface;
using CF_HOATUOIBASANH.Repositorys;

namespace CF_HOATUOIBASANH.Interfaces
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IProductRepository _productRepository;
        public EmailService(IConfiguration configuration, IProductRepository productRepository)
        {
            _configuration = configuration;
            _productRepository = productRepository;
        }

        public async Task SendEmailAsync(string to, Order order, List<DetailOrder> orderDetails, string recipientType)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Hoa Tươi Ba Sanh", "hutechdonationfundation@gmail.com"));

            if (recipientType == "KH") // Nếu là khách hàng
            {
                message.To.Add(new MailboxAddress("", to));
                message.Subject = $"Xác nhận đơn hàng {order.OrderID} - Cửa hàng hoa tươi ba sanh";

                // Build the email body for the customer
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = BuildHtmlBody(order, orderDetails);

                message.Body = bodyBuilder.ToMessageBody();
            }
            else if (recipientType == "QL") // Nếu là quản lý
            {
                message.To.Add(new MailboxAddress("", "phucprodl.3@gmail.com"));
                message.Subject = "Thông báo có đơn hàng cần thanh toán";

                // Build the email body for the manager
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = BuildHtmlBody2(order, orderDetails);

                message.Body = bodyBuilder.ToMessageBody();
            }

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_configuration["MailSettings:SmtpServer"], int.Parse(_configuration["MailSettings:Port"]), SecureSocketOptions.Auto);
                await client.AuthenticateAsync(_configuration["MailSettings:Username"], _configuration["MailSettings:Password"]);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        private string BuildHtmlBody(Order order, List<DetailOrder> orderDetails)
        {
            var sb = new StringBuilder();

            // Build HTML content for the email body
            sb.Append($"<h1>Đơn hàng {order.OrderID}</h1>");
            sb.Append($"<p><strong>Mã đơn hàng:</strong> {order.OrderID}</p>");
            sb.Append($"<p><strong>Tên khách hàng:</strong> {order.Customer.FirstName} {order.Customer.LastName}</p>");
            sb.Append($"<p><strong>Địa chỉ giao hàng:</strong> {order.ShipAddress}</p>");
            sb.Append($"<p><strong>Hình thức thanh toán:</strong> {order.PayMethod}</p>");
            sb.Append($"<p><strong>Trạng thái thanh toán:</strong> {order.PayStatus}</p>");
            sb.Append($"<p><strong>Trạng thái giao hàng:</strong> {order.ShipStatus}</p>");
            sb.Append($"<p><strong>Ngày tạo:</strong> {order.CreateDate.ToString("dd/MM/yyyy")}</p>");

            sb.Append("<h2>Chi tiết đơn hàng</h2>");
            sb.Append("<table border='1'>");
            sb.Append("<tr><th>Mã sản phẩm</th><th>Tên sản phẩm</th><th>Số lượng</th><th>Giá</th><th>Thành tiền</th></tr>");
            decimal totalAmount = 0;
            decimal shipCost = Convert.ToDecimal(order.ShipCost);

            foreach (var detail in orderDetails)
            {
                var product = _productRepository.GetById(detail.ProductID);
                decimal totalPrice = (product.Price ?? 0) * (detail.Quantity ?? 0);
                totalAmount += totalPrice;
                sb.Append($"<tr><td>{detail.ProductID}</td><td>{product.ProductName}</td><td>{detail.Quantity}</td><td>{product.Price}</td><td>{totalPrice.ToString()}</td></tr>");
            }
            sb.Append("</table>");

            sb.Append($"<p><strong>Tổng tiền hàng:</strong> {totalAmount.ToString("N0")}</p>");
            sb.Append($"<p><strong>Tổng tiền ship:</strong> {shipCost.ToString("N0")}</p>");
            sb.Append($"<p><strong>Thành tiền:</strong> {totalAmount.ToString("N0")}</p>");

            // Thêm lời cảm ơn và xác nhận
            sb.Append("<p>Xin cảm ơn quý khách đã đặt hàng từ chúng tôi. Đơn hàng của bạn đã được tiếp nhận và đang được xử lý. Vui lòng chờ trong thời gian ngắn. Chúng tôi sẽ thông báo cho bạn khi đơn hàng được gửi đi. Trân trọng!</p>");
            sb.Append("<p>Đây là email tự động, vui lòng không trả lời.</p>");

            return sb.ToString();
        }
        private string BuildHtmlBody2(Order order, List<DetailOrder> orderDetails)
        {
            var sb = new StringBuilder();
            sb.Append("<p>Bạn có đơn hàng cần xác nhận !!!!.</p>");

            sb.Append($"<h1>Đơn hàng {order.OrderID}</h1>");
            sb.Append($"<p><strong>Mã đơn hàng:</strong> {order.OrderID}</p>");
            sb.Append($"<p><strong>Tên khách hàng:</strong> {order.Customer.FirstName} {order.Customer.LastName}</p>");
            sb.Append($"<p><strong>Địa chỉ giao hàng:</strong> {order.ShipAddress}</p>");
            sb.Append($"<p><strong>Hình thức thanh toán:</strong> {order.PayMethod}</p>");
            sb.Append($"<p><strong>Trạng thái thanh toán:</strong> {order.PayStatus}</p>");
            sb.Append($"<p><strong>Trạng thái giao hàng:</strong> {order.ShipStatus}</p>");
            sb.Append($"<p><strong>Ngày tạo:</strong> {order.CreateDate.ToString("dd/MM/yyyy")}</p>");

            sb.Append("<h2>Chi tiết đơn hàng</h2>");
            sb.Append("<table border='1'>");
            sb.Append("<tr><th>Mã sản phẩm</th><th>Tên sản phẩm</th><th>Số lượng</th><th>Giá</th><th>Thành tiền</th></tr>");
            decimal totalAmount = 0;
            decimal shipCost = Convert.ToDecimal(order.ShipCost);

            foreach (var detail in orderDetails)
            {
                var product = _productRepository.GetById(detail.ProductID);
                decimal totalPrice = (product.Price ?? 0) * (detail.Quantity ?? 0);
                totalAmount += totalPrice;
                sb.Append($"<tr><td>{detail.ProductID}</td><td>{product.ProductName}</td><td>{detail.Quantity}</td><td>{product.Price}</td><td>{totalPrice.ToString()}</td></tr>");
            }
            sb.Append("</table>");

            sb.Append($"<p><strong>Tổng tiền hàng:</strong> {totalAmount.ToString("N0")}</p>");
            sb.Append($"<p><strong>Tổng tiền ship:</strong> {shipCost.ToString("N0")}</p>");
            sb.Append($"<p><strong>Thành tiền:</strong> {totalAmount.ToString("N0")}</p>");


            return sb.ToString();
        }


    }
}
