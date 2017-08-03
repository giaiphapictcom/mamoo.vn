﻿/// <reference path="jquery-1.10.2.min.js" />

function Admin_ProductType_HienThiPopUp(noidung, pId, pPage) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_ProductType_Xoa(' + pId + ',' + pPage + ');">OK</a><a class="controlno" href="javascript:ClosePopup();">NO</a></div></div>')
}

function Admin_ProductType_PopUpKetQuaTaoMoi(noidung) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup"><div class="mpopup_header"><div class="mpopup_title">Thông báo</div><div class="mpopup_close" onclick="javascript:ClosePopup();"></div></div><div class="mpopup_content">' + noidung + '</div><div class="mcontrols"><a class="controlok" href="javascript:Admin_ProductType_Reset();">Tiếp tục thêm</a><a class="controlno" href="javascript:Admin_ProductType_ComeHome();">Về trang danh sách</a></div></div>')
}
function Admin_ProductType_PopUpKetQuaSua(noidung, pId) {
    $("#popupId").css('display', 'block');
    $("#popupId").empty();
    $("#popupId").append('<div class="mpopup">' +
        '<div class="mpopup_header">' +
        '<div class="mpopup_title">Thông báo</div>' +
        '<div class="mpopup_close" onclick="javascript:ClosePopup();"></div>' +
        '</div>' +
        '<div class="mpopup_content">' + noidung + '</div>' +
        '<div class="mcontrols"><a class="controlok" href="javascript:Admin_ProductType_ComeSua(' + pId + ');">Tiếp tục</a><a class="controlno" href="javascript:Admin_ProductType_ComeHome();">Về trang danh sách</a></div></div>')
}
function Admin_ProductType_Xoa(pId, pPage) {

    $("#wait").css("display", "block");
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': pId },
        dataType: 'json',
        url: "/ProductType/OnDelete",
        timeout: 60000,
        success: function (data) {
            if (data.code == 1) {
                window.location = "/ProductType/Index?pPage=" + pPage + "";
            }
            else {
                $("#wait").css("display", "none");
                HienThiPopUp(data.message);
            }
        },
        error: function (x, t, m) {
            $("#wait").css("display", "none");
        }
    });
}

function Admin_ProductType_ThucHienLuuMoi() {

    $("#wait").css("display", "block");
    var mTieuDe = $('#pTitleId').val();
    var mThuTu = $('#pOrderId').val();
    var mpActive= $('#pActiveId').val();
    var mpGroup = $('#pGroupId').val();
    var mpSummary = $('#pSummaryId').val();
    var mAnh = $('#ImageUrl').val();
    var mAnhBanner = $('#ImageUrlBanner').val();
    var mpDescription = $('#pDescriptionId').val();
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pDescription':mpDescription,'pTieuDe': mTieuDe, 'pGroupId': mpGroup, 'pSummary': mpSummary, 'pKichHoat': mpActive, 'pUuTien': mThuTu, 'pImageUrl': mAnh, 'pImageBanner': mAnhBanner },
        dataType: 'json',
        url: "/ProductType/OnCreate/",
        timeout: 60000,
        success: function (data) {
            $("#wait").css("display", "none");
            if (data.code == 1) {
                Admin_ProductType_PopUpKetQuaTaoMoi(data.message);
                scrollWindow(10);
            }
            else {
                HienThiPopUp(data.message);
                scrollWindow(10);
            }
        },
        error: function (x, t, m) {
            $("#wait").css("display", "none");
        }
    });
}

function Admin_ProductType_ThucHienLuuSua() {

    $("#wait").css("display", "block");
    var mId = $('#pId').val();
    var mTieuDe = $('#pTitleId').val();
    var mThuTu = $('#pOrderId').val();
    var mpActive = $('#pActiveId').val();
    var mpGroup = $('#pGroupId').val();
    var mpSummary = $('#pSummaryId').val();
    var mAnh = $('#ImageUrl').val();
    var mAnhBanner = $('#ImageUrlBanner').val();
    var mpDescription = $('#pDescriptionId').val();
    //gui thong tin dang ky len may chu
    $.ajax({
        type: 'POST',
        data: { 'pId': mId, 'pDescription': mpDescription, 'pTieuDe': mTieuDe, 'pGroupId': mpGroup, 'pSummary': mpSummary, 'pKichHoat': mpActive, 'pUuTien': mThuTu, 'pImageUrl': mAnh, 'pImageBanner': mAnhBanner },
        dataType: 'json',
        url: "/ProductType/OnEdit/",
        timeout: 60000,
        success: function (data) {
            $("#wait").css("display", "none");
            if (data.code == 1) {
                Admin_ProductType_PopUpKetQuaSua(data.message, mId);
                scrollWindow(10);
            }
            else {
                HienThiPopUp(data.message);
                scrollWindow(10);
            }
        },
        error: function (x, t, m) {
            $("#wait").css("display", "none");
        }
    });
}


function Admin_ProductType_Reset() {

    $('#pTitleId').val("");
    ClosePopup();
}

function Admin_ProductType_ComeHome() {

    window.location = "/ProductType/Index";
}

function Admin_ProductType_ComeSua(pId) {

    window.location = "/ProductType/Edit?pId=" + pId + "";
}
function ThayDoiKieuSanPham2(pPage) {
    var pType = $("#pGroupIdHome").val();
    window.location = "/ProductType/Index?pPage=1&pType=" + pType + "";
}
function Admin_ProductType_ChangeRooId() {
    window.location = "/ProductType/Index?pPage=1&rootId=" + $("#RootId").val() + "";
}
function Admin_ProductType_ChangeParentId() {
    var rootId = $("#RootId").val();
    var parentId = $("#ParentId").val();
    var baseUrl = "/ProductType/Index?pPage=1";
    if (rootId)
        baseUrl += "&rootId=" + rootId;
    if (parentId)
        baseUrl += "&parentId=" + parentId;

    window.location = baseUrl;

}
function Admin_ProductType_ChangeChildId() {
    var rootId = $("#RootId").val();
    var parentId = $("#ParentId").val();
    var childId = $("#ChildId").val();
    var baseUrl = "/ProductType/Index?pPage=1";
    if (rootId)
        baseUrl += "&rootId=" + rootId;
    if (parentId)
        baseUrl += "&parentId=" + parentId;
    if (childId)
        baseUrl += "&childId=" + childId;
    window.location = baseUrl;

}
