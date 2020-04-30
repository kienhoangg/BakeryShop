var productController = function () {
    this.Initialize = function () {
        getAllCategory();
        getAllProductByPagging(false);
 
        registerEvent();
    }
    var check = false;
    function registerEvent() {
        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'vi',
            rules: {
                txtNameM: { required: true },
                ddlCategoryIdM: { required: true },
                txtPriceM: {
                    required: true,
                    number: true
                }
            }
        });
        $('#btnSelectImg').on('click', function () {
            $('#fileInputImage').click();
        });
        $('#fileInputImage').on('change', function () {
            var fileUpload = $(this).get(0);
            var files = fileUpload.files;
            var data = new FormData();
            for (var i = 0; i < files.length; i++) {
                data.append(files[i].name, files[i]);
            }
            $.ajax({
                type: "POST",
                url: "/Admin/Upload/UploadImage",
                contentType: false,
                processData: false,
                data: data,
                success: function (path) {
                    $('#txtImageM').val(path);
                    common.notify('Upload image succesful!', 'success');

                },
                error: function () {
                    common.notify('There was error uploading files!', 'error');
                }
            });
        })
        $('#ddlShowPage').on('change', function () {
            check = true;
            common.configs.pageSize = $(this).val();
            common.configs.pageIndex = 1;
            getAllProductByPagging(check);
        });
        $('#btnSearch').on('click', function () {
            common.configs.pageIndex = 1;
            getAllProductByPagging(true);
        });
        $('#txtKeyword').on('keypress', function (e) {
            if (e.which === 13) {
                common.configs.pageIndex = 1;
                getAllProductByPagging(true);
            }
        });
        $('#ddlCategorySearch').on('change', function () {
            check = true;
            common.configs.pageIndex = 1;
            getAllProductByPagging(check);
        })
        $('#btnCreate').off('click').on('click', function (e) {
            resetFormMaintainance();
            initTreeComboCategory();
            $('#modal-add-edit').modal('show');
        })
        $('body').on('click', '.btn-edit', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            $.ajax({
                type: "GET",
                url: "/Admin/Product/GetById",
                data: { id: that },
                dataType: "json",
                beforeSend: function () {
                    common.startLoading();
                },
                success: function (response) {
                    var data = response;
                    if (data.Content == null) {
                        editor.setData('');
                    }
                    else { editor.setData(data.Content); }
                    $('#hidIdM').val(data.Id);
                    $('#txtNameM').val(data.Name);
                    initTreeComboCategory(data.CategoryId);

                    $('#txtDescM').val(data.Description);
                    $('#txtUnitM').val(data.Unit);

                    $('#txtPriceM').val(data.Price);
                    $('#txtOriginalPriceM').val(data.OriginalPrice);
                    $('#txtPromotionPriceM').val(data.PromotionPrice);

                    // $('#txtImageM').val(data.ThumbnailImage);

                    $('#txtTagM').val(data.Tags);
                    $('#txtMetakeywordM').val(data.SeoKeywords);
                    $('#txtMetaDescriptionM').val(data.SeoDescription);
                    $('#txtSeoPageTitleM').val(data.SeoPageTitle);
                    $('#txtSeoAliasM').val(data.SeoAlias);

                   
                    $('#ckStatusM').prop('checked', data.Status == 1);
                    $('#ckHotM').prop('checked', data.HotFlag);
                    $('#ckShowHomeM').prop('checked', data.HomeFlag);

                    $('#modal-add-edit').modal('show');
                    common.stopLoading();

                },
                error: function (status) {
                    common.notify('Có lỗi xảy ra', 'error');
                    common.stopLoading();
                }
            });
        });
        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            common.confirm('Are you sure to delete?', function () {
                $.ajax({
                    type: "POST",
                    url: "/Admin/Product/Delete",
                    data: { id: that },
                    dataType: "json",
                    beforeSend: function () {
                        common.startLoading();
                    },
                    success: function (response) {
                        common.notify('Delete successful', 'success');
                        common.stopLoading();
                        getAllProductByPagging(true);
                    },
                    error: function (status) {
                        common.notify('Has an error in delete progress', 'error');
                        common.stopLoading();
                    }
                });
            });
        });
        $('#btnSave').off('click').on('click', function (e) {
            if ($('#frmMaintainance').valid()) {
                e.preventDefault();
                var id = $('#hidIdM').val();
                var name = $('#txtNameM').val();
                var categoryId = $('#ddlCategoryIdM').combotree('getValue');

                var description = $('#txtDescM').val();
                var unit = $('#txtUnitM').val();

                var price = $('#txtPriceM').val();
                var originalPrice = $('#txtOriginalPriceM').val();
                var promotionPrice = $('#txtPromotionPriceM').val();

                var image = $('#txtImageM').val();

                var tags = $('#txtTagM').val();
                var seoKeyword = $('#txtMetakeywordM').val();
                var seoMetaDescription = $('#txtMetaDescriptionM').val();
                var seoPageTitle = $('#txtSeoPageTitleM').val();
                var seoAlias = $('#txtSeoAliasM').val();

                var content = editor.getData();
                var status = $('#ckStatusM').prop('checked') == true ? 1 : 0;
                var hot = $('#ckHotM').prop('checked');
                var showHome = $('#ckShowHomeM').prop('checked');

                $.ajax({
                    type: 'POST',
                    dataType: 'json',
                    url: '/Admin/Product/SaveEntity',
                    data: {
                        Id: id,
                        Name: name,
                        CategoryId: categoryId,
                        Image: image,
                        Price: price,
                        OriginalPrice: originalPrice,
                        PromotionPrice: promotionPrice,
                        Description: description,
                        Content: content,
                        HomeFlag: showHome,
                        HotFlag: hot,
                        Tags: tags,
                        Unit: unit,
                        Status: status,
                        SeoPageTitle: seoPageTitle,
                        SeoAlias: seoAlias,
                        SeoKeywords: seoKeyword,
                        SeoDescription: seoMetaDescription
                    }, beforeSend: function () {
                        common.startLoading();
                    },
                    success: function (res) {
                        common.notify('Update product successful', 'success');
                        $('#modal-add-edit').modal('hide');
                        resetFormMaintainance();

                        common.stopLoading();
                        getAllProductByPagging(true);
                    },
                    error: function (status) {
                        common.notify('Has an error in save product progress', 'error');
                        common.stopLoading();
                    }
                })
            }
        })
    }

    function getAllCategory() {
  
        $.ajax({
            url: '/Admin/ProductCategory/GetAll',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                $.each(res, function (i, item) {
                    $('#ddlCategorySearch').append('<option value = "'+item.Id+'">' + item.Name + '</option>');
                })
            },
            error: function (status) {
                common.notify('Cannot loading Category', 'error');
            }
        })
    }
    function getAllProductByPagging(isPageChanged) {
        var template = $('#template-mustache').html();
        var render = '';
        $.ajax({
            url: '/Admin/Product/GetAllProductByPagging',
            type: 'GET',
            dataType: 'json',
            data: {
                categoryId: $('#ddlCategorySearch option:selected').val(),
                keyword: $('#txtKeyword').val(),
                page: common.configs.pageIndex,
                pageSize: common.configs.pageSize,
            },
            success: function (res) {
              
                $.each(res.results, function (i, item) {
                    render += Mustache.render(template, {
                        Id: item.Id,
                        ProductName: item.name,
                        CategoryName: item.productCategory.name,
                        Price: common.formatNumber(item.price, 0),
                        Image: item.image == null ? '<img src="/admin-side/images/user.png" width=25' : '<img src="' + item.image + '" width=25 />',
                        CreatedDate: common.dateTimeFormatJson(item.dateCreated),
                        Status: common.getStatus(item.status)
                    })
                })
                $('#lblTotalRecords').text(res.rowCount);
                if (render != null) {
                    $('#tbl-content').html(render);
                }
                wrapping(res.rowCount, function () {
                    getAllProductByPagging(false);
                }, isPageChanged)
            },
            error: function (status) {
                common.notify('Cannot loading data', 'error');
            }
        })
    }
       
    function wrapping(recordCount, callBack, check) {
        if ($('#paginationUL a').length === 0 || check === true) {
            $('#paginationUL').empty();
            $('#paginationUL').removeData("twbs-pagination");
            $('#paginationUL').unbind("page");
            check = false;
        }
        var totalSize = Math.ceil(recordCount / common.configs.pageSize);

        $('#paginationUL').twbsPagination({

            totalPages: totalSize,
            visiblePages: 7,
            onPageClick: function (event, page) {
                common.configs.pageIndex = page;
                setTimeout(callBack(), 200);
            }
        })
    }
    function initTreeComboCategory(selectedItem) {
        $.ajax({
            url: '/Admin/ProductCategory/GetAll',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                var arr = [];
                $.each(res, function (i, item) {                
                    arr.push({
                        id: item.Id,
                        text: item.Name,
                        parentId: item.ParentId,
                        sortOrder: item.SortOrder
                    })

                })
                var treeArray = common.unflattern(arr);
                treeArray.sort(function (a, b) {
                    return a.sortOrder - b.sortOrder;
                });
                $('#ddlCategoryIdM').combotree({
                    data: treeArray,
                });
                if (selectedItem != undefined) {
                    $('#ddlCategoryIdM').combotree('setValue', selectedItem);
                }
            }
        })
    }
    function resetFormMaintainance() {
        $('#hidIdM').val(0);
        $('#txtNameM').val('');
        initTreeComboCategory('');

        $('#txtDescM').val('');
        $('#txtUnitM').val('');

        $('#txtPriceM').val('0');
        $('#txtOriginalPriceM').val('');
        $('#txtPromotionPriceM').val('');

        //$('#txtImageM').val('');

        $('#txtTagM').val('');
        $('#txtMetakeywordM').val('');
        $('#txtMetaDescriptionM').val('');
        $('#txtSeoPageTitleM').val('');
        $('#txtSeoAliasM').val('');

        editor.setData('');
        $('#ckStatusM').prop('checked', true);
        $('#ckHotM').prop('checked', false);
        $('#ckShowHomeM').prop('checked', false);
    }
}