// 페이지 생성 함수
function CreatePager(pPager, pShowCnt, pFullCnt, pCurPage) {

    $("#" + pPager).empty();                            // 페이지 자식 개체 삭제

    var showCnt = Number(pShowCnt);                     // 기본 페이지 개수
    var fullCnt = Number(pFullCnt);                     // 전체 페이지 개수
    var curPage = Number(pCurPage);                     // 선택 페이지

    var pageGroup = Math.ceil(curPage / showCnt);       // 소속된 페이지 그룹 구하기
    var endPage = (pageGroup * showCnt);                // 소속된 페이지 그룹의 마지막 페이지 번호 구하기

    // (소속된 페이지 그룹의 마지막 페이지 번호 > 전체 페이지 개수) 이면 루프의 시작이 전체 페이지 개수
    if (endPage > fullCnt) {
        for (i = fullCnt; i > (endPage - showCnt) ; i--) {
            $("#" + pPager).prepend("<a>" + i + "</a>");
        }
    }
    else {
        // 아니면 소속된 페이지 그룹의 마지막 페이지 번호
        for (i = endPage; i > (endPage - showCnt) ; i--) {
            $("#" + pPager).prepend("<a>" + i + "</a>");
        }
    }

    // 선택한 페이지가 첫 번째 그룹이 아니면 생성
    if (pageGroup > 1) {
        $("#" + pPager).prepend("<a id='prev'>" + "<" + "</a>");
        $("#" + pPager).prepend("<a id='start_prev'>" + "<<" + "</a>");
    }

    // 선택한 페이지가 마지막 번째 그룹이 아니면 생성
    if (endPage < fullCnt) {
        $("#" + pPager).append("<a id='next'>" + ">" + "</a>");
        $("#" + pPager).append("<a id='finish_next'>" + ">>" + "</a>");
    }

    $("#" + pPager).append("<a id='show_cnt' style='display:none;'>" + showCnt + "</a>");
    $("#" + pPager).append("<a id='full_cnt' style='display:none;'>" + fullCnt + "</a>");
    $("#" + pPager).append("<a id='cur_page' style='display:none;'>" + curPage + "</a>");

    // 선택된 페이지 번호에 언더라인/볼드 처리 및 포인터 해제
    $("#" + pPager + " a").each(function () {
        if ($(this).text() == curPage) {
            $(this).css("text-decoration", "underline");
            $(this).css("font-weight", "bold");
            $(this).css("cursor", "default");
            return false;
        };
    });
}

// 페이지(상세) 생성 함수
function CreateDynamicPager(pPager, pShowCnt, pFullCnt, pCurPage, pGubun) {

    $("#" + pPager).empty();                            // 페이지 자식 개체 삭제

    var showCnt = Number(pShowCnt);                     // 기본 페이지 개수
    var fullCnt = Number(pFullCnt);                     // 전체 페이지 개수
    var curPage = Number(pCurPage);                     // 선택 페이지

    var pageGroup = Math.ceil(curPage / showCnt);       // 소속된 페이지 그룹 구하기
    var endPage = (pageGroup * showCnt);                // 소속된 페이지 그룹의 마지막 페이지 번호 구하기

    // (소속된 페이지 그룹의 마지막 페이지 번호 > 전체 페이지 개수) 이면 루프의 시작이 전체 페이지 개수
    if (endPage > fullCnt) {
        for (i = fullCnt; i > (endPage - showCnt); i--) {
            $("#" + pPager).prepend("<a>" + i + "</a>");
        }
    }
    else {
        // 아니면 소속된 페이지 그룹의 마지막 페이지 번호
        for (i = endPage; i > (endPage - showCnt); i--) {
            $("#" + pPager).prepend("<a>" + i + "</a>");
        }
    }

    // 선택한 페이지가 첫 번째 그룹이 아니면 생성
    if (pageGroup > 1) {
        $("#" + pPager).prepend("<a id='prev_" + pGubun + "'>" + "<" + "</a>");
        $("#" + pPager).prepend("<a id='start_prev_" + pGubun + "'>" + "<<" + "</a>");
    }

    // 선택한 페이지가 마지막 번째 그룹이 아니면 생성
    if (endPage < fullCnt) {
        $("#" + pPager).append("<a id='next_" + pGubun + "'>" + ">" + "</a>");
        $("#" + pPager).append("<a id='finish_next_" + pGubun + "'>" + ">>" + "</a>");
    }

    $("#" + pPager).append("<a id='show_cnt_" + pGubun + "' style='display:none;'>" + showCnt + "</a>");
    $("#" + pPager).append("<a id='full_cnt_" + pGubun + "' style='display:none;'>" + fullCnt + "</a>");
    $("#" + pPager).append("<a id='cur_page_" + pGubun + "' style='display:none;'>" + curPage + "</a>");

    // 선택된 페이지 번호에 언더라인/볼드 처리 및 포인터 해제
    $("#" + pPager + " a").each(function () {
        if ($(this).text() == curPage) {
            $(this).css("text-decoration", "underline");
            $(this).css("font-weight", "bold");
            $(this).css("cursor", "default");
            return false;
        };
    });
}

// 페이지 번호 반환
function GetPagerValue(pPageId, pPageNo) {

    var pageNo = Number($("#cur_page").text());
    var fullpage_cnt = Number($("#full_cnt").text());

    if (pPageId == "next") {
        pageNo = pageNo + 1;
    }
    else if (pPageId == "finish_next") {
        pageNo = fullpage_cnt;
    }
    else if (pPageId == "prev") {
        pageNo = pageNo - 1;
    }
    else if (pPageId == "start_prev") {
        pageNo = 1;
    }
    else {
        pageNo = pPageNo;
    }

    return pageNo;
}

// 페이지(상세) 번호 반환
function GetDynamicPagerValue(pPageId, pPageNo, pGubun) {

    var pageNo = Number($("#cur_page_" + pGubun).text());
    var fullpage_cnt = Number($("#full_cnt_" + pGubun).text());

    if (pPageId == "next_" + pGubun) {
        pageNo = pageNo + 1;
    }
    else if (pPageId == "finish_next_" + pGubun) {
        pageNo = fullpage_cnt;
    }
    else if (pPageId == "prev_" + pGubun) {
        pageNo = pageNo - 1;
    }
    else if (pPageId == "start_prev_" + pGubun) {
        pageNo = 1;
    }
    else {
        pageNo = pPageNo;
    }

    return pageNo;
}

// 그리드 컬럼값 반환
function GetGridTrValue(pGridNm, pId, pChildObj) {
    var returnVal = "";
    var val = "";

    $("#" + pGridNm + " tr:first-child").children().each(function () {
        if ($(this).attr("id") == pId) {
            returnVal = $(this).index();
        }
    });

    val = pChildObj.eq(returnVal).text();

    return val;
}

