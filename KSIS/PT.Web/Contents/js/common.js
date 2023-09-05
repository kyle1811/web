// 문자열 공백 체크 함수
function IsBlank(pInput) {
    if (pInput.replace(/\s/g, "") == "") {
        return true;
    }
    else {
        return false;
    }
}

// 숫자인지 체크 함수
function IsValidNum(pInput) {
    var regNum = /^[0-9]+$/;
    if (!regNum.test(pInput)) {
        return false;
    }
    else {
        return true;
    }
}

// 숫자만 추출해서 반환 함수
function GetPickUpNum(pInput) {
    var returnVal = "";
    
    returnVal = pInput.replace(/[^0-9]/g, "");

    return returnVal;
}

// 숫자(3자리콤마) 반환 함수
function GetCommaNum(pInput) {
    var num, point, len, returnVal;

    num = pInput.toString();
    point = num.length % 3;
    len = num.length;
    returnVal = num.substring(0, point);

    while (point < len) {
        if (returnVal != "") {
            returnVal = returnVal + ",";
        }
        returnVal = returnVal + num.substring(point, point + 3);
        point = point + 3;
    }

    return returnVal;
}

// 문자 및 콤마제거 후, 숫자(3자리콤마) 반환 함수
function GetStrRemoveAndCommaNum(pInput) {
    var returnVal = "";

    returnVal = pInput.replace(/[^\d]+/g, "");
    returnVal = returnVal.replace(/(\d)(?=(?:\d{3})+(?!\d))/g, "$1,")

    return returnVal;
}

// 회원정보 형식 체크 함수
function IsValidMember(pType, pInput) {
    // 이메일 형식 체크
    if (pType == "Email") {
        var regEmail = /([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
        if (!regEmail.test(pInput)) {
            return false;
        }
        else {
            return true;
        }
    }
    // 인증번호 형식 체크
    else if (pType == "CertNum") {
        var regCertNum = /^[0-9]{4}$/;
        if (!regCertNum.test(pInput)) {
            return false;
        }
        else {
            return true;
        }
    }
    // 닉네임 형식 체크
    else if (pType == "NickNm") {
        var regNickNm = /^[가-힣|a-z|A-Z|0-9]+$/;
        if (pInput.length < 2 || pInput.length > 31 || !regNickNm.test(pInput)) {
            return false;
        }
        else {
            return true;
        }
    }
    // 휴대전화 형식 체크
    else if (pType == "Mobile") {
        var regMobile = /^[0-9]{3}-[0-9]{4}-[0-9]{4}$/;
        if (!regMobile.test(pInput)) {
            return false;
        }
        else {
            return true;
        }
    }
    // 전화번호 형식 체크
    else if (pType == "Tel") {
        var regTel = /^[0-9]{2,3}-[0-9]{3,4}-[0-9]{4}$/;
        if (!regTel.test(pInput)) {
            return false;
        }
        else {
            return true;
        }
    }
    // 비밀번호 형식 체크
    else if (pType == "Pwd") {
        var regPwd = /^(?=\w{10,16}$)(?=.*?[a-z])(?=.*?[0-9]).*/;
        if (!regPwd.test(pInput)) {
            return false;
        }
        else {
            return true;
        }
    }
    // Url 형식 체크
    else if (pType == "Url") {
        var regUrl = /^((http(s?))\:\/\/)([0-9a-zA-Z\-]+\.)+[a-zA-Z]{2,6}(\:[0-9]+)?(\/\S*)?$/;
        if (!regUrl.test(pInput)) {
            return false;
        }
        else {
            return true;
        }
    }
}

// 이스케이프 변경 함수 (크롬,파폭은 인코딩 특수문자를 자동변환 하지 않음)
function ChangeEscape(pInput) {
    var returnVal = "";

    returnVal = pInput.replace(/\&amp;/g, "&").replace(/\&#39;/g, "");

    return returnVal;
}

// 항목 보이기/숨기기 제어
function displaySelect() {
    document.getElementById("setting").style.display = "none";
}

