$(document).ready(function () {

    $(".viewOrder").on("click",
        function (e) {
            var button = $(this);
            $("#indexReceiveBtn").removeAttr('disabled');
            $.ajax({
                type: "POST",
                url: "/Receiving/Details",
                datatype: "html",
                data: { "id": button.attr("data-ponumber") },
                success: function (data) {
                    $(".details").html(data);
                    var orderButton = $(".js-Order");
                    orderButton.removeClass("invisible");
                    $.ajax({
                        type: "GET",
                        url: "/Receiving/GetUnorderedParts",
                        datatype: "html",
                        data: { "id": button.attr("data-ponumber") },
                        success: function (data) {
                            $("#UnorderedDetail").html(data);
                            var orderButton = $(".js-Order");
                            orderButton.removeClass("invisible");

                        }
                    });

                }
            });
            e.preventDefault();
        });


    $(".insertUnorderedPart").on("click",
        function (e) {
            var poNumber = $(".viewOrder").data("ponumber");
            var description = $(".js-desc");
            var vendorPart = $('.js-vendorpart');
            var quantity = $('.js-qty');
            var formdata = {};

            formdata.PurchaseOrderNumber = poNumber;
            formdata.Description = description.val();
            formdata.VendorPartNumber = vendorPart.val();
            formdata.Quantity = quantity.val();

            $.ajax({
                url: "/Receiving/CreateUnorderedPart/",
                type: "POST",
                data: '{unorderedPart: ' + JSON.stringify(formdata) + '}',
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success:
                    function (data) {
                        //$("#UnorderedDetail").html(data);
                        var orderButton = $(".js-Order");
                        orderButton.removeClass("invisible");
                        description.val("");
                        vendorPart.val("");
                        quantity.val("");
                        $("#UnorderedDetail").DataTable({
                            "searching": false,
                            "ordering": false,
                            "lengthChange": false,
                            ajax: {
                                url: "/RecevingController/GetUnorderedParts",
                                datasrc: ""
                            },
                            columns: [
                                {
                                    data: "Description"
                                },
                                {
                                    data: "VendorPartNumber",
                                    title: "Vendor Part Number"
                                },
                                {
                                    data: "Quantity"
                                }
                            ]
                        });
                    }
            });
            e.preventDefault();
        });

    $("#UnorderedDetail").on("click",
        ".js-delete",
        function (e) {
            var cartid = $(this).data('cart');
            var po = $(this).data('ponumber');
            $.ajax({
                type: "POST",
                url: '@Url.Action("Delete", "Receiving")',
                datatype: "html",
                data: {
                    "cartId": cartid,
                    "poNumber": po
                },
                success: function (data) {
                    $("#UnorderedDetail").html(data);
                    var orderButton = $(".js-Order");
                    orderButton.removeClass("invisible");

                }
            });
            e.preventDefault();
        });

    $('#indexReceiveBtn').click(function () {
        $("#frmReceiveBtn").trigger('click');
        return false;
    });

});