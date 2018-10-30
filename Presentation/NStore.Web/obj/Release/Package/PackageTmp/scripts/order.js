//获得配送地址列表
function getShipAddressList() {
    Ajax.get("/ucenter/ajaxshipaddresslist", false, getShipAddressListResponse);
}

//处理获得配送地址列表的反馈信息
function getShipAddressListResponse(data) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        var shipAddressList = "<ul class='orderList'>";
        for (var i = 0; i < result.content.count; i++) {
            shipAddressList += "<li><label><b><input type='radio' class='radio' name='shipAddressItem' value='" + result.content.list[i].saId + "' onclick='selectShipAddress(" + result.content.list[i].saId + ")' />" + result.content.list[i].user + "</b><i>" + result.content.list[i].address + "</i></label></li>";
        }
        shipAddressList += "<li id='newAdress'><label><input type='radio' class='radio' name='shipAddressItem' onclick='openAddShipAddressBlock()' />使用新地址</label></li></ul>";
        document.getElementById("shipAddressShowBlock").style.display = "none";
        document.getElementById("shipAddressListBlock").style.display = "";
        document.getElementById("shipAddressListBlock").innerHTML = shipAddressList;
    }
    else {
        alert(result.content);
    }
}

//选择配送地址
function selectShipAddress(saId) {
    document.getElementById("saId").value = saId;
    document.getElementById("confirmOrderForm").submit();
}

//打开添加配送地址块
function openAddShipAddressBlock() {
    document.getElementById("addShipAddressBlock").style.display = "";
}

//添加配送地址
function addShipAddress() {
    var addShipAddressForm = document.forms["addShipAddressForm"];

    var alias = addShipAddressForm.elements["alias"].value;
    var consignee = addShipAddressForm.elements["consignee"].value;
    var mobile = addShipAddressForm.elements["mobile"].value;
    var phone = addShipAddressForm.elements["phone"].value;
    var email = addShipAddressForm.elements["email"].value;
    var zipcode = addShipAddressForm.elements["zipcode"].value;
    var regionId = getSelectedOption(addShipAddressForm.elements["regionId"]).value;
    var address = addShipAddressForm.elements["address"].value;
    var isDefault = addShipAddressForm.elements["isDefault"] == undefined ? 0 : addShipAddressForm.elements["isDefault"].checked ? 1 : 0;
    isDefault = 1;

    if (!verifyAddShipAddress(alias, consignee, mobile, phone, regionId, address)) {
        return;
    }

    Ajax.post("/ucenter/addshipaddress",
            { 'alias': alias, 'consignee': consignee, 'mobile': mobile, 'phone': phone, 'email': email, 'zipcode': zipcode, 'regionId': regionId, 'address': address, 'isDefault': isDefault },
            false,
            addShipAddressResponse)
}

//验证添加的收货地址
function verifyAddShipAddress(alias, consignee, mobile, phone, regionId, address) {
    if (alias == "") {
        alert("请填写昵称");
        return false;
    }
    if (consignee == "") {
        alert("请填写收货人");
        return false;
    }
    if (mobile == "" && phone == "") {
        alert("手机号和固定电话必须填写一项");
        return false;
    }
    if (parseInt(regionId) < 1) {
        alert("请选择区域");
        return false;
    }
    if (address == "") {
        alert("请填写详细地址");
        return false;
    }
    return true;
}

//处理添加配送地址的反馈信息
function addShipAddressResponse(data) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        document.getElementById("saId").value = result.content;
        document.getElementById("confirmOrderForm").submit();
    }
    else {
        var msg = "";
        for (var i = 0; i < result.content.length; i++) {
            msg += result.content[i].msg + "\n";
        }
        alert(msg)
    }
}

//展示支付插件列表
function showPayPluginList() {
    document.getElementById("payPluginShowBlock").style.display = "none";
    document.getElementById("payPluginListBlock").style.display = "";
}

//选择支付方式
function selectPayPlugin(paySystemName) {
    document.getElementById("payName").value = paySystemName;
    document.getElementById("confirmOrderForm").submit();
}

//验证支付积分
function verifyPayCredit(hasPayCreditCount, maxUsePayCreditCount) {
    var obj = document.getElementById("payCreditCount");
    var usePayCreditCount = obj.value;
    if (isNaN(usePayCreditCount)) {
        obj.value = 0;
        alert("请输入数字");
    }
    else if (usePayCreditCount > hasPayCreditCount) {
        obj.value = hasPayCreditCount;
        alert("积分不足");
    }
    else if (usePayCreditCount > maxUsePayCreditCount) {
        obj.value = maxUsePayCreditCount;
        alert("最多只能使用" + maxUsePayCreditCount + "个");
    }
}

//获得有效的优惠劵列表
function getValidCouponList() {
    Ajax.get("/order/getvalidcouponlist?selectedCartItemKeyList=" + document.getElementById("selectedCartItemKeyList").value, false, getValidCouponListResponse);
}

//处理获得有效的优惠劵列表的反馈信息
function getValidCouponListResponse(data) {
    var result = eval("(" + data + ")");
    if (result.state == "success") {
        if (result.content.length < 1) {
            document.getElementById("validCouponList").innerHTML = "<p>此订单暂无可用的优惠券</p>";
        }
        else {
            var itemList = "<p class='chooseYH'>";
            for (var i = 0; i < result.content.length; i++) {
                itemList += "<label><input type='checkbox' name='couponId' value='" + result.content[i].couponId + "' useMode='" + result.content[i].usemode + "' onclick='checkCouponUseMode(this)'/>" + result.content[i].name + "</label>";
            }
            itemList += "</p>";
            document.getElementById("validCouponList").innerHTML = itemList;
        }
        document.getElementById("validCouponCount").innerHTML = result.content.length;
    }
    else {
        alert(result.content);
    }
}

//检查优惠劵的使用模式
function checkCouponUseMode(obj) {
    if (!obj.checked) {
        return;
    }
    var useMode = obj.getAttribute("useMode");
    if (useMode == "0") {
        return;
    }
    var checkboxList = document.getElementById("validCouponList").getElementsByTagName("input");
    for (var i = 0; i < checkboxList.length; i++) {
        checkboxList[i].checked = false;
    }
    obj.checked = true;
}

//验证优惠劵编号
function verifyCouponSN(couponSN) {
    if (couponSN == undefined || couponSN == null || couponSN.length == 0) {
        alert("请输入优惠劵编号");
    }
    else if (couponSN.length != 16) {
        alert("优惠劵编号不正确");
    }
    else {
        Ajax.get("/order/verifycouponsn?couponSN=" + couponSN, false, verifyCouponSNResponse);
    }
}

//处理验证优惠劵编号的反馈信息
function verifyCouponSNResponse(data) {
    var result = eval("(" + data + ")");
    alert(result.content);
}

//提交订单
function submitOrder() {
    var selectedCartItemKeyList = document.getElementById("selectedCartItemKeyList").value
    var saId = document.getElementById("saId").value;
    var payName = document.getElementById("payName").value;
    var payCreditCount = document.getElementById("payCreditCount") ? document.getElementById("payCreditCount").value : 0;

    var couponIdList = "";
    var couponIdCheckboxList = document.getElementById("validCouponList").getElementsByTagName("input");
    for (var i = 0; i < couponIdCheckboxList.length; i++) {
        if (couponIdCheckboxList[i].checked == true) {
            couponIdList += couponIdCheckboxList[i].value + ",";
        }
    }
    if (couponIdCheckboxList.length > 0)
        couponIdList = couponIdList.substring(0, couponIdCheckboxList.length - 1)

    var couponSN = document.getElementById("couponSN") ? document.getElementById("couponSN").value : "";
    var allFullCut = document.getElementById("allFullCut") ? document.getElementById("allFullCut").value : 0;
    var bestTime = document.getElementById("bestTime") ? document.getElementById("bestTime").value : "";
    var buyerRemark = document.getElementById("buyerRemark") ? document.getElementById("buyerRemark").value : "";
    var verifyCode = document.getElementById("verifyCode") ? document.getElementById("verifyCode").value : "";

    if (!verifySubmitOrder(saId, payName, buyerRemark)) {
        return;
    }

    Ajax.post("/order/submitorder",
            { 'selectedCartItemKeyList': selectedCartItemKeyList, 'saId': saId, 'payName': payName, 'payCreditCount': payCreditCount, 'couponIdList': couponIdList, 'couponSNList': couponSN, 'fullCut': allFullCut, 'bestTime': bestTime, 'buyerRemark': buyerRemark, 'verifyCode': verifyCode },
            false,
            submitOrderResponse)
}

//验证提交订单
function verifySubmitOrder(saId, payName, buyerRemark) {
    if (saId < 1) {
        alert("请填写收货人信息");
        return false;
    }
    if (payName.length < 1) {
        alert("配送方式不能为空");
        return false;
    }
    if (buyerRemark.length > 125) {
        alert("最多只能输入125个字");
        return false;
    }
    return true;
}

//处理提交订单的反馈信息
function submitOrderResponse(data) {
    var result = eval("(" + data + ")");
    if (result.state != "success") {
        alert(result.content);
    }
    else {
        window.location.href = result.content;
    }
}