function addToCart(productId) {
    var quantity = 1;
    $.ajax({
        type: "POST",
        url: "/Shop/AddToCart",
        data: { id: productId, quantity: quantity },
        success: function (cartCount) {
            toastr.success("Sản phẩm đã được thêm vào giỏ hàng thành công.", "", {
                timeOut: 2000,
                progressBar: true,
                closeButton: true
            });

            // Update the cart count dynamically
            var cartCount = parseInt($("#CartCount").text());
            $("#CartCount").text(cartCount + 1);
        },
        error: function () {
            toastr.error("Lỗi... Thêm sản phẩm vào giỏ hàng không thành công.", "", {
                timeOut: 2000,
                progressBar: true,
                closeButton: true
            });
        }
    });
}

$(document).ready(function () {
    $(".add-to-cart, .button-cart").click(function (event) {
        var productId = $(this).data("product-id");
        addToCart(productId);
        return false;
    });
});
