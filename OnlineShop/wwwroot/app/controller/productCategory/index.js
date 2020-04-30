var productCategory = function () {
    this.Initialize = function () {
        getAllCategory();
        registerEvent();
    }
    function registerEvent() {
        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules: {
                txtNameM: { required: true },
                txtOrderM: { number: true },
                txtHomeOrderM: { number: true }
            }
        });
        $('#btnCreate').off('click').on('click', function () {
            initTreeComboCategory();
            $('#modal-add-edit').modal('show');
        })
        $('body').on('click', '#btnDelete', function (e) {
            e.preventDefault();
            var that = $('#hidIdM').val();
            common.confirm('Are you sure to delete?', function () {
                $.ajax({
                    type: "POST",
                    url: "/Admin/ProductCategory/Delete",
                    data: { id: that },
                    dataType: "json",
                    beforeSend: function () {
                        common.startLoading();
                    },
                    success: function (response) {
                        common.notify('Deleted success', 'success');
                        common.stopLoading();
                        loadData();
                    },
                    error: function (status) {
                        common.notify('Has an error in deleting progress', 'error');
                        common.stopLoading();
                    }
                });
            });
        });
        $('body').on('click', '#btnEdit', function () {
            $.ajax({
                url: '/Admin/ProductCategory/GetById',
                data: {
                    id: parseInt($('#hidIdM').val())
                },
                dataType: 'json',
                type: 'Post',
                success: function (res) {
                    var data = res;
                    $('#hidIdM').val(data.Id);
                    $('#txtNameM').val(data.Name);
                    initTreeComboCategory(data.ParentId, parseInt($('#hidIdM').val()));

                    $('#txtDescM').val(data.Description);

                    $('#txtImageM').val(data.ThumbnailImage);

                    $('#txtSeoKeywordM').val(data.SeoKeywords);
                    $('#txtSeoDescriptionM').val(data.SeoDescription);
                    $('#txtSeoPageTitleM').val(data.SeoPageTitle);
                    $('#txtSeoAliasM').val(data.SeoAlias);

                    $('#ckStatusM').prop('checked', data.Status == 1);
                    $('#ckShowHomeM').prop('checked', data.HomeFlag);
                    $('#txtOrderM').val(data.SortOrder);
                    $('#txtHomeOrderM').val(data.HomeOrder);
                    $('#modal-add-edit').modal('show');
                    common.stopLoading();
                    
                },
                error: function (status) {
                    common.notify('Có lỗi xảy ra', 'error');
                    common.stopLoading();
                }
            })
        })
        $('#btnSave').off('click').on('click', function (e) {
            if ($('#frmMaintainance').valid()) {
                e.preventDefault(); 
                var id = parseInt($('#hidIdM').val());
                var name = $('#txtNameM').val();
                var parentId = $('#ddlCategoryIdM').combotree('getValue');
                var description = $('#txtDescM').val();

                var image = $('#txtImageM').val();
                var order = parseInt($('#txtOrderM').val());
                var homeOrder = $('#txtHomeOrderM').val();

                var seoKeyword = $('#txtSeoKeywordM').val();
                var seoMetaDescription = $('#txtSeoDescriptionM').val();
                var seoPageTitle = $('#txtSeoPageTitleM').val();
                var seoAlias = $('#txtSeoAliasM').val();
                var status = $('#ckStatusM').prop('checked') == true ? 1 : 0;
                var showHome = $('#ckShowHomeM').prop('checked');

                $.ajax({
                    type: "POST",
                    url: "/Admin/ProductCategory/SaveEntity",
                    data: {
                        Id: id,
                        Name: name,
                        Description: description,
                        ParentId: parentId,
                        HomeOrder: homeOrder,
                        SortOrder: order,
                        HomeFlag: showHome,
                        Image: image,
                        Status: status,
                        SeoPageTitle: seoPageTitle,
                        SeoAlias: seoAlias,
                        SeoKeywords: seoKeyword,
                        SeoDescription: seoMetaDescription
                    },
                    dataType: "json",
                    beforeSend: function () {
                        common.startLoading();
                    },
                    success: function (response) {
                        common.notify('Update success', 'success');
                        $('#modal-add-edit').modal('hide');

                        resetFormMaintainance();

                        common.stopLoading();
                        loadData(true);
                    },
                    error: function () {
                        common.notify('Has an error in update progress', 'error');
                        common.stopLoading();
                    }
                });
            }
            return false;
        })  
    }
    function initTreeComboCategory(selectedItem, thisId) {
        $.ajax({
            url: '/Admin/ProductCategory/GetAll',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                var arr = [];
                $.each(res, function (i, item) {
                    if (item.id === thisId || item.parentId === thisId) {
                        return true;
                    }
                    arr.push({
                        id: item.id,
                        text: item.name,
                        parentId: item.parentId,
                        sortOrder: item.sortOrder
                    })

                })  
                var treeArray = common.unflattern(arr,thisId);
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
        initTreeDropDownCategory('');

        $('#txtDescM').val('');
        $('#txtOrderM').val('');
        $('#txtHomeOrderM').val('');
        $('#txtImageM').val('');

        $('#txtMetakeywordM').val('');
        $('#txtMetaDescriptionM').val('');
        $('#txtSeoPageTitleM').val('');
        $('#txtSeoAliasM').val('');

        $('#ckStatusM').prop('checked', true);
        $('#ckShowHomeM').prop('checked', false);
    }
    function getAllCategory() {
        $.ajax({
            url: '/Admin/ProductCategory/GetAll',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                var arr = [];
                $.each(res, function (i, item) {
                    arr.push({
                        id: item.id,
                        text: item.name,
                        parentId: item.parentId,
                        sortOrder: item.sortOrder
                    })

                })
               
                //arr.sort((a, b) => a.sortOrder.localeCompare(b.sortOrder));
                var treeArray = common.unflattern(arr);
                treeArray.sort(function (a, b) {
                    return a.sortOrder - b.sortOrder;
                });
                $('#treeCategory').tree({
                    data: treeArray,
                    dnd: true,
                    onDrop: function (target, source, point) {
                        var targetNode = $(this).tree('getNode', target);
                       
                        var children = [];
                        $.each(targetNode.children, function (i, item) {
                            children.push({
                                key: item.id,
                                value: i
                            })
                        })
                        if (point === 'append') {
                            $.ajax({
                                url: '/Admin/ProductCategory/UpdateParentId',
                                type: 'POST',
                                dataType: 'json',
                                data: {
                                    sourceId: source.id,
                                    targetId: targetNode.id,
                                    lstChildren: children
                                },
                                success: function (res) {
                                    getAllCategory();
                                }
                            })
                        }
                        else if (point === 'top' || point === 'bottom') {
                            if (targetNode.parentId == null || targetNode.parentId == source.parentId) {
                                $.ajax({
                                    url: '/Admin/ProductCategory/ReOrder',
                                    dataType: 'json',
                                    type: 'POST',
                                    data: {
                                        sourceId: source.id,
                                        targetId: targetNode.id,
                                        lstChildren: null
                                    },
                                    success: function (res) {
                                        getAllCategory();
                                    }
                                })
                            }
                            else {
                                var parentNode = $(this).tree('getParent', target);
                             
                                var children = [];
                                $.each(parentNode.children, function (i, item) {
                                    children.push({
                                        key: item.id,
                                        value: i
                                    })
                                })
                                $.ajax({
                                    url: '/Admin/ProductCategory/UpdateParentId',
                                    type: 'POST',
                                    dataType: 'json',
                                    data: {
                                        sourceId: source.id,
                                        targetId: targetNode.parentId,
                                        lstChildren: children
                                    },
                                    success: function (res) {
                                        getAllCategory();
                                    }
                                })
                              
                            }
                           
                        }
                    },
                    onContextMenu: function (e, node) {
                        e.preventDefault();
                        $('#hidIdM').val(node.id);
                        $('#contextMenu').menu('show', {
                            left: e.pageX,
                            top: e.pageY
                        })
                    }
                })
            },
            error: function (status) {
                common.notify('Cannot loading Category', 'error');
            }
        })
    }
}