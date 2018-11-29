$(document)
    .ready(function () {
        var tbl;
        var poNumber;
        $(".viewOrder")
            .on("click",
                function (e) {
                    poNumber = $(this).attr("data-ponumber");
                    $("#message").text("");
                    $.ajax({
                        type: "POST",
                        url: "/Receiving/Details",
                        datatype: "html",
                        data: { "id": poNumber },
                        success: function (data) {
                            $(".details").html(data);
                            var orderButton = $(".js-Order");
                            orderButton.removeClass("invisible");
                            $.ajax({
                                type: "POST",
                                url: "/Receiving/GetUnorderedParts",
                                datatype: "html",
                                data: { "id": poNumber },
                                success: function (data) {
                                    tbl = $("#UnorderedDetail")
                                        .DataTable({
                                            "destroy": true,
                                            "searching": false,
                                            "ordering": false,
                                            "lengthChange": false,
                                            "pagingType": "full_numbers",
                                            "paging": true,
                                            "lengthMenu": [10, 25, 50, 75, 100],
                                            ajax: {
                                                url: "/Receiving/GetUnorderedParts",
                                                data: { "id": poNumber },
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
                                                        return "<button class='btn btn-danger js-delete' data-cart-id=" +
                                                            data +
                                                            ">Delete</button>";
                                                    }
                                                }
                                            ] //EOColumns
                                        }); //EODataTable

                                } //EOSuccess
                            }); //EOInnerAjax

                        } //EOOuterSuccess
                    }); //EOInitialAjax
                    e.preventDefault();
                });


        $(".insertUnorderedPart")
            .on("click",
                function (e) {
                    var description = $(".js-desc");
                    var vendorPart = $('.js-vendorpart');
                    var quantity = $('.js-qty');
                    var formdata = {};

                    formdata.PurchaseOrderNumber = poNumber;
                    formdata.Description = description.val();
                    formdata.VendorPartNumber = vendorPart.val();
                    formdata.Quantity = quantity.val();

                    if ($('#unorderedForm').valid()) {
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
                                    tbl.ajax.reload();
                                } //EOSuccess
                        }); //EOAjax
                    }
                    e.preventDefault();
                });

        $("#UnorderedDetail")
            .on("click",
                ".js-delete",
                function (e) {
                    var button = $(this);
                    $.ajax({
                        type: "POST",
                        url: "/Receiving/Delete/",
                        datatype: "html",
                        data: {
                            "cartId": button.attr("data-cart-id"),
                            "poNumber": poNumber
                        },
                        success: function () {
                            tbl.row(button.parents("tr")).remove().draw();
                            var orderButton = $(".js-Order");
                            orderButton.removeClass("invisible");
                        }
                    });
                    e.preventDefault();
                });

        //Since this click will take place in partial view and is being
        //loaded into page dynamically, select element on main form and then
        //specify button id
        $(".details").on("click",
            '#indexReceiveBtn',
            function () {
                $.validator.unobtrusive.parse($("form"));
                if (!$('#unorderedForm').valid())
                    return false;
                $("#frmReceiveBtn").trigger('click');
                return false;
            });

        //Since this click will take place in partial view and is being
        //loaded into page dynamically, select element on main form and then
        //specify button id
        $(".details").on("click",
            "#forceCloserBtn",
            function () {
                var closeForm = $("#OrderForm");
                $.ajax({
                    type: "POST",
                    url: "/Receiving/ForceCloser/",
                    //data: JSON.stringify({ viewModel: model }),
                    dataType: "json",
                    data: closeForm.serialize(),
                    success: function (data) {
                        $.ajax({
                            type: "GET",
                            url: "/Receiving/Index/",
                            datatype: "html",
                            success: function (data) {
                                $("#wrapper").html(data);
                                $("#message").text("Order closed successfully.");
                            }
                        });
                    }
                });
            });

        $("#viewOrderAuto")
            .click(function () {
                AutoRunthrough.viewOrder();
            });

        $("#addRecItmAuto")
            .click(function () {
                AutoRunthrough.addReceiveData();
            });

        $("#receiveAuto")
            .click(function () {
                AutoRunthrough.receiveItem();
            });
        $("#unorderTabAuto")
            .click(function () {
                AutoRunthrough.unorderedTab();
            });
        $("#addUnorderedAuto")
            .click(function () {
                AutoRunthrough.addUnordered();
            });
        $("#insertUnorderedAuto")
            .click(function () {
                AutoRunthrough.insertUOP();
            });
        $("#deleteUnorderAuto")
            .click(function () {
                if (!tbl.data().count()) {
                    bootbox.alert("Can not demo this function, there are no records to delete");
                } else {
                    AutoRunthrough.deleteUOP();
                }
            });
        $("#finish").click(function () {
            AutoRunthrough.finish();
        });

    });

var AutoRunthrough = (function () {
    return {
        viewOrder: function () {
            $('.nav-tabs a[href="#receiving"]').tab('show');
            AutoRunthrough.removeDisable($("#addRecItmAuto"));
            var btnRow = $("#OrderList tbody").find("a:first");
            if (typeof btnRow != 'undefined')
                btnRow.trigger('click');
        },
        addReceiveData: function () {
            var outstanding = $("#OrderDetail tbody tr:first td:nth-child(4)").text();
            var receiving = $("#OrderDetail tbody tr:first td:nth-child(5) input");
            var returning = $("#OrderDetail tbody tr:first td:nth-child(6) input");
            var reason = $("#OrderDetail tbody tr:first td:nth-child(7) input");

            receiving.val(2);
            returning.val(20);
            reason.val("Defective parts");
            AutoRunthrough.removeDisable($("#receiveAuto"));
        },
        receiveItem: function () {
            AutoRunthrough.removeDisable($("#unorderTabAuto"));
            $("#frmReceiveBtn").trigger('click');
        },
        unorderedTab: function () {
            $('.nav-tabs a[href="#unordered"]').tab('show');
            AutoRunthrough.removeDisable($("#addUnorderedAuto"));
        },
        addUnordered: function () {
            var description = $("#UnorderedPart_Description");
            var partNumber = $("#UnorderedPart_VendorPartNumber");
            var quanity = $("#UnorderedPart_Quantity");

            description.val("Home speaker system");
            partNumber.val("HS2341-P");
            quanity.val(1);

            AutoRunthrough.removeDisable($("#insertUnorderedAuto"));
        },
        insertUOP: function () {
            $("#insertUOP").trigger("click");
            AutoRunthrough.removeDisable($("#deleteUnorderAuto"));
        },
        deleteUOP: function () {
            var btnRow = $("#UnorderedDetail tbody").find("button:first");
            if (typeof btnRow != 'undefined')
                btnRow.trigger('click');
            AutoRunthrough.removeDisable($("#finish"));
        },
        finish: function () {
            $("#addRecItmAuto").addClass("disabled");
            $("#receiveAuto").addClass("disabled");
            $("#unorderTabAuto").addClass("disabled");
            $("#addUnorderedAuto").addClass("disabled");
            $("#insertUnorderedAuto").addClass("disabled");
            $("#deleteUnorderAuto").addClass("disabled");
            $("#finish").addClass("disabled");
            $('.nav-tabs a[href="#receiving"]').tab('show');
        },
        removeDisable: function (element) {
            element.removeClass("disabled");
        }
    }
})();