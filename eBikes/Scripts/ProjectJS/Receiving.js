$(document).ready(function () {
    var tbl;
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
                            var orderButton = $(".js-Order");
                            orderButton.removeClass("invisible");
                            tbl = $("#UnorderedDetail").DataTable({
                                "destroy": true,
                                "searching": false,
                                "ordering": false,
                                "lengthChange": false,
                                "pagingType": "full_numbers",
                                "paging": true,
                                "lengthMenu": [10, 25, 50, 75, 100],
                                ajax: {
                                    url: "/Receiving/GetUnorderedParts",
                                    data: { "id": button.attr("data-ponumber") },
                                    datasrc: ""
                                },
                                columns: [
                                    {
                                        data: "Description"
                                    },
                                    {
                                        data: "VendorPartNumber"
                                    },
                                    {
                                        data: "Quantity"
                                    },
                                    {
                                        data: "CartID",
                                        render: function (data) {
                                            return "<button class='btn btn-link js-delete' data-cart-id=" +
                                                data +
                                                ">Delete</button>";
                                        }
                                    }
                                ]
                            });

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

            var form = $('#unorderedForm');
            $.validator.unobtrusive.parse(form);
            form.validate();
            if (form.valid()) {
                $.ajax({
                    url: "/Receiving/CreateUnorderedPart/",
                    type: "POST",
                    data: '{unorderedPart: ' + JSON.stringify(formdata) + '}',
                    contentType: "application/json; charset=utf-8",
                    datatype: "json",
                    success:
                        function (data) {
                            var orderButton = $(".js-Order");
                            orderButton.removeClass("invisible");
                            description.val("");
                            vendorPart.val("");
                            quantity.val("");
                            tbl({
                                "destroy": true,
                                "searching": false,
                                "ordering": false,
                                "lengthChange": false,
                                "pagingType": "full_numbers",
                                "paging": true,
                                "lengthMenu": [10, 25, 50, 75, 100],
                                ajax: {
                                    url: "/Receiving/GetUnorderedParts",
                                    data: { "id": button.attr("data-ponumber") },
                                    datasrc: ""

                                },
                                columns: [
                                    {
                                        data: "Description"
                                    },
                                    {
                                        data: "VendorPartNumber"
                                    },
                                    {
                                        data: "Quantity"
                                    },
                                    {
                                        data: "CartID",
                                        render: function (data) {
                                            return "<button class='btn btn-link js-delete' data-cart-id=" +
                                                data + ">Delete</button>";
                                        }
                                    }
                                ]
                            });
                        }
                });
            }
            e.preventDefault();
        });

    $("#UnorderedDetail").on("click",
        ".js-delete",
        function (e) {
            var button = $(this);
            var po = $(".viewOrder").data("ponumber");
            $.ajax({
                type: "POST",
                url: '@Url.Action("Delete", "Receiving")',
                datatype: "html",
                data: {
                    "cartId": button.attr("data-cart-id"),
                    "poNumber": po
                },
                success: function () {
                    tbl.row(button.parents("tr")).remove().draw();
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