var nn4 = (document.layers) ? true : false;
var ie = (document.all) ? true : false;
var dom = (document.getElementById && !document.all) ? true : false;
var popups = new Array(); // keeps track of popup windows we create
var calHtml = '';
var spliter = '-';
var dateformat = 'yyyy/mm/dd'

// language and preferences
wDay = new Array('일요일', '월요일', '화요일', '수요일', '목요일', '금요일', '토요일');
wDayAbbrev = new Array('<span style="color:#CC0000">일</span>', '월', '화', '수', '목', '금', '<span style="color:#0000CC">토</span>');
wMonth = new Array('1월', '2월', '3월', '4월', '5월', '6월', '7월', '8월', '9월', '10월', '11월', '12월');
wOrder = new Array('첫번째', '두번째', '세번째', '네번째', '마지막');
wStart = 0;
var daysInMonth = new Array(0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);

var prehtml = ["<style type=\"text/css\">",
	"<!--",
	"#calendarPopup { position: absolute; visibility: visible; z-index: 100; }",
	".outline * { font-size: 12px; font-family:Verdana, Tahoma, 굴림, 돋움;}",
	".outline { border:3px solid #868a9f; text-decoration: none; }",
	".yearmonth { height:20px; font-weight:bold;font-size: 12px;  }",
	"a.arrow { font-family: 돋움,Tahoma; font-size: 11px; color: #000000; }",
	".stylecal {  padding-right:3px; color: #000000; }",
	"td.weekday { text-align:center; background: #7e899a;  }",
	"td.weekday, td.weekday span { color:#fff; font-weight:bold; font-size: 12px; font-family:Verdana, Tahoma, 맑은고딕, 돋움; }",
	"a { color:#000; text-decoration: none;  }",
	"a.mday { color:#000; text-decoration: none;  }",
	"a.mday:hover { color: #FFF; text-decoration: none; color:#000;  }",
	".row_on_a { background: #6699cc; color:#FFF;  }",
	".row_off_a {  background:none; padding:1px 2px 2px 1px; }",
	"a.gotoday { color: #0066cc; padding:0; }",
	//".notmonth {  color: #777; }", 
	"a.notmonth:link {  color: #777  }",
	"a.notmonth:hover {  color: #000000; line-height: 9pt; text-decoration: none; }",
	"a.stylecal:link {  color: #000000;  }",
	"a.stylecal:hover {  color: #000000; line-height: 9pt; text-decoration: none; }",
	"-->",
	"</style> ",
	"<div id=\"my_calender\" style=\"position: absolute; visibility: visible; z-index: 100; background-color: #99BDDF; layer-background-color: #99BDDF;\"></div> "
].join('');
document.write(prehtml);

function popup_Calendar(obj, target, format) {

    var id = 'my_calender';
    attachListener(id);
    registerPopup(id);
    dateformat = format;
    calHtml = makeCalHtml(id, null, null, null, target);
    writeLayer(id, calHtml);
    var xOffset = 0;
    var yOffset = obj.offsetHeight + 1;
    setLayerPos(obj, id, xOffset, yOffset);
    showLayer(id);
    return true;
}


function attachListener(id) {
    var layer = new pathToLayer(id)
    if (layer.obj.listening == null) {
        document.oldMouseupEvent = document.onmouseup;
        if (document.oldMouseupEvent != null) {
            document.onmouseup = new Function("document.oldMouseupEvent(); hideLayersNotClicked();");
        } else {
            document.onmouseup = hideLayersNotClicked;
        }
        layer.obj.listening = true;
    }
}

function registerPopup(id) {
    // register this popup window with the popups array
    var layer = new pathToLayer(id);
    if (layer.obj.registered == null) {
        var index = popups.length ? popups.length : 0;
        popups[index] = layer;
        layer.obj.registered = 1;
    }
}

function isDate(strDate) {
    strDate = strDate.replaceAll("/", "").replaceAll("-", "").replaceAll(".", "");

    if (strDate.length < 8) return false;
    var year = strDate.substring(0, 4);
    var month = strDate.substring(4, 6);
    var day = strDate.substring(6, 8);

    if (year < 1000) { return false; }
    var ms = makeDate(year, month, day);
    if (ms.getFullYear() != year || (ms.getMonth() + 1) != month || ms.getDate() != day) { return false; }
    return true;
}

function makeCalHtml(id, calYear, calMonth, calDay, target) {
    var strDate, targetId;

    if (typeof target == "object") {
        strDate = target.value;
        targetId = target.id;
    }
    else {
        strDate = getObject(target).value;
        targetId = target;
    }

    strDate = strDate.replaceAll("/", "").replaceAll("-", "").replaceAll(".", "");
    if (isDate(strDate)) {

        if (calYear == null || calYear == "") calYear = strDate.substring(0, 4);
        if (calMonth == null || calMonth == "") calMonth = strDate.substring(4, 6);
        if (calDay == null || calDay == "") calDay = strDate.substring(6, 8);
        if ((calYear % 4 == 0 && calYear % 100 != 0) || calYear % 400 == 0) {
            daysInMonth[2] = 29;
        }
    }
    else {
        var today = new Date();
        if (calYear == null || calYear == "") calYear = today.getFullYear();
        if (calMonth == null || calMonth == "") calMonth = today.getMonth() + 1;
        if (calDay == null || calDay == "") calDay = today.getDate();
    }

    var calDate = new Date(calYear, calMonth - 1, calDay);
    //-----------------------------------------
    // check if the currently selected day is
    // more than what our target month has
    //-----------------------------------------
    if (calMonth < calDate.getMonth() + 1) {
        calDay = calDay - calDate.getDate();
        calDate = new Date(calYear, calMonth - 1, calDay);
    }

    var calNextYear = calDate.getMonth() == 11 ? calDate.getFullYear() + 1 : calDate.getFullYear();
    var calNextMonth = calDate.getMonth() == 11 ? 1 : calDate.getMonth() + 2;
    var calLastYear = calDate.getMonth() == 0 ? calDate.getFullYear() - 1 : calDate.getFullYear();
    var calLastMonth = calDate.getMonth() == 0 ? 12 : calDate.getMonth();

    var todayDate = new Date();

    //---------------------------------------------------------
    // this relies on the javascript bug-feature of offsetting
    // values over 31 days properly. Negative day offsets do NOT
    // work with Netscape 4.x, and negative months do not work
    // in Safari. This works everywhere.
    //---------------------------------------------------------
    var calStartOfThisMonthDate = new Date(calYear, calMonth - 1, 1);
    var calOffsetToFirstDayOfLastMonth = calStartOfThisMonthDate.getDay() >= wStart ? calStartOfThisMonthDate.getDay() - wStart : 7 - wStart - calStartOfThisMonthDate.getDay()
    if (calOffsetToFirstDayOfLastMonth > 0) {
        var calStartDate = new Date(calLastYear, calLastMonth - 1, 1); // we start in last month
    } else {
        var calStartDate = new Date(calYear, calMonth - 1, 1); // we start in this month
    }
    var calStartYear = calStartDate.getFullYear();
    var calStartMonth = calStartDate.getMonth();
    var calCurrentDay = calOffsetToFirstDayOfLastMonth ? daysInMonth[calStartMonth + 1] - (calOffsetToFirstDayOfLastMonth - 1) : 1;

    var html = '';
    // writing the <html><head><body> causes some browsers (Konquerer) to fail

    html += '<table border=0 cellpadding=0 cellspacing=0 bgcolor="#EEEEFF" class="outline">\n';
    html += '<tr>\n';
    html += '<td>\n';
    // inner
    html += '<table width=100% cellpadding="0" cellspacing="1" border="0" bgcolor="#FFFFFF">\n';
    html += '<tr>\n';
    html += '<td valign="top">\n';
    html += '<table width=100% cellpadding="3" cellspacing="1" border="0">\n';
    html += '<tr>\n';
    html += '<td><a class="arrow" href="#" onClick="updateCal(\'' + id + '\',' + calLastYear + ',' + calLastMonth + ',' + calDay + ',\'' + targetId + '\'); return false;">◀</a></td>\n';
    html += '<td class="yearmonth" align="center" colspan="5">&nbsp;' + calDate.getFullYear() + '년 ' + wMonth[calDate.getMonth()] + '&nbsp;</td>\n';
    html += '<td align="right"><a class="arrow" href="#" onClick="updateCal(\'' + id + '\',' + calNextYear + ',' + calNextMonth + ',' + calDay + ',\'' + targetId + '\'); return false;">▶</a></td>\n';
    html += '</tr>\n';
    for (var row = 1; row <= 7; row++) {
        // check if we started a new month at the beginning of this row
        upcomingDate = new Date(calStartYear, calStartMonth, calCurrentDay);
        if (upcomingDate.getDate() <= 8 && row > 5) {
            continue; // skip this row
        }
        html += '<tr>\n';

        for (var col = 0; col < 7; col++) {
            //var tdColor = col % 2 ? '"#DAEDFE"' : '"#F8F8F8"';
            var tdColor = '"#F3F3F3"';
            if (row == 1) {
                tdColor = '"#EEEEFF"';
                html += '<td align="center" width="20" class="weekday">' + wDayAbbrev[(wStart + col) % 7] + '</td>\n';
            } else {
                var hereDate = new Date(calStartYear, calStartMonth, calCurrentDay);
                var hereDay = hereDate.getDate();
                var aClass = '"mday"';

                if (hereDate.getYear() == todayDate.getYear() && hereDate.getMonth() == todayDate.getMonth() && hereDate.getDate() == todayDate.getDate()) {
                    tdColor = '"#cccc66"';
                }
                else if (todayDate.format('yyyymm') != hereDate.format('yyyymm') && hereDate.getMonth() == calDate.getMonth() && hereDate.getDate() == calDate.getDate()) {
                    tdColor = '"#99ccff"';
                }
                if (hereDate.getMonth() != calDate.getMonth()) {
                    tdColor = '"#dddddd"';
                    var aClass = '"notmonth"';
                }
                html += '<td bgcolor=' + tdColor + ' align="right" onClick="changeFormDate(' + hereDate.getFullYear() + ',' + (hereDate.getMonth() + 1) + ',' + hereDate.getDate() + ',\'' + targetId + '\'); hideLayer(\'' + id + '\'); return false;" onMouseOver="this.className=\'row_on_a\'" onMouseOut="this.className=\'row_off\'"><a href="#" class=' + aClass + ' onClick="changeFormDate(' + hereDate.getFullYear() + ',' + (hereDate.getMonth() + 1) + ',' + hereDate.getDate() + ',\'' + targetId + '\'); hideLayer(\'' + id + '\'); return false;">' + hereDay + '</a></td>\n';
                calCurrentDay++;
            }
        }
        html += '</tr>\n';
    }
    var today = new Date().format('yyyy/mm/dd');
    html += '<tr>\n';
    html += '<td align="center" valign="bottom" colspan="7" height="20" bgcolor="#ffffff" onClick="updateCal(\'' + id + '\',' + todayDate.getFullYear() + ',' + (todayDate.getMonth() + 1) + ',' + todayDate.getDate() + ',\'' + targetId + '\'); return false;"><button class="gotoday" onClick="updateCal(\'' + id + '\',' + todayDate.getFullYear() + ',' + (todayDate.getMonth() + 1) + ',' + todayDate.getDate() + ',\'' + targetId + '\'); return false;">오늘로 이동 (' + today + ')</button></td>\n';
    html += '</tr>\n';
    html += '</table>\n';
    html += '</td></tr>\n';
    html += '</table>\n';
    // end of inner
    html += '</td>\n';
    html += '</tr>\n';
    html += '</table>\n';
    return html;
}

function updateCal(id, calYear, calMonth, calDay, targetId) {
    calHtml = makeCalHtml(id, calYear, calMonth, calDay, targetId);
    writeLayer(id, calHtml);
}

function writeLayer(id, html) {
    var layer = new pathToLayer(id);
    if (nn4) {
        layer.obj.document.open();
        layer.obj.document.write(html);
        layer.obj.document.close();
    } else {
        layer.obj.innerHTML = '';
        layer.obj.innerHTML = html;
    }
}

function setLayerPos(obj, id, xOffset, yOffset) {
    var newX = 0;
    var newY = 0;
    if (obj.offsetParent) {
        // if called from href="setLayerPos(this,'example')" then obj will
        // have no offsetParent properties. Use onClick= instead.
        while (obj.offsetParent) {
            newX += obj.offsetLeft;
            newY += obj.offsetTop;
            obj = obj.offsetParent;
        }
    } else if (obj.x) {
        // nn4 - only works with "a" tags
        newX += obj.x;
        newY += obj.y;
    }

    // apply the offsets
    newX += xOffset;
    newY += yOffset;

    // apply the new positions to our layer
    //   var layer = new pathToLayer(id);
    var layer = document.getElementById(id);
    if (nn4) {
        layer.style.left = newX;
        layer.style.top = newY;
    } else {
        // the px avoids errors with doctype strict modes
        layer.style.left = newX + 'px';
        layer.style.top = newY + 'px';
    }
}

function hideLayersNotClicked(e) {
    if (!e) var e = window.event;
    e.cancelBubble = true;
    if (e.stopPropagation) e.stopPropagation();
    if (e.target) {
        var clicked = e.target;
    } else if (e.srcElement) {
        var clicked = e.srcElement;
    }

    // go through each popup window,
    // checking if it has been clicked
    for (var i = 0; i < popups.length; i++) {
        if (nn4) {
            if ((popups[i].style.left < e.pageX) &&
                (popups[i].style.left + popups[i].style.clip.width > e.pageX) &&
                (popups[i].style.top < e.pageY) &&
                (popups[i].style.top + popups[i].style.clip.height > e.pageY)) {
                return true;
            } else {
                hideLayer(popups[i].obj.id);
                return true;
            }
        } else if (ie) {
            while (clicked.parentElement != null) {
                if (popups[i].obj.id == clicked.id) {
                    return true;
                }
                clicked = clicked.parentElement;
            }
            hideLayer(popups[i].obj.id);
            return true;
        } else if (dom) {
            while (clicked.parentNode != null) {
                if (popups[i].obj.id == clicked.id) {
                    return true;
                }
                clicked = clicked.parentNode;
            }
            hideLayer(popups[i].obj.id);
            return true;
        }
        return true;
    }
    return true;
}

function pathToLayer(id) {
    if (nn4) {
        this.obj = document.layers[id];
        this.style = document.layers[id];
    } else if (ie) {
        this.obj = document.all[id];
        this.style = document.all[id].style;
    } else {
        this.obj = document.getElementById(id);
        this.style = document.getElementById(id).style;
    }
}

function getObject(id) {
    if (nn4) {
        return document.layers[id];
    } else if (ie) {
        return document.all[id];
    } else {
        return document.getElementById(id);
    }
}

function showLayer(id) {
    var layer = new pathToLayer(id)
    layer.style.visibility = "visible";
    //hideSelectList();
}

function hideLayer(id) {
    var layer = new pathToLayer(id);
    layer.style.visibility = "hidden";
    //showSelectList();
}

function makeDate(year, month, day) {
    return new Date(year, month - 1, day);
}

function changeFormDate(changeYear, changeMonth, changeDay, targetId) {
    if (changeMonth < 10) changeMonth = '0' + changeMonth;
    if (changeDay.length < 10) changeDay = '0' + changeDay;

    var date = makeDate(changeYear, changeMonth, changeDay);
    eval(getObject(targetId)).value = date.format(dateformat);
}

// the date format prototype
//SomeDiv.innerText = (new Date()).format('dddd, mmmm dd, yyyy.');
Date.prototype.format = function (f) {
    if (!this.valueOf())
        return '';
    var d = this;
    return f.replace(/(yyyy|mmmm|mmm|mm|dddd|ddd|dd|hh|nn|ss|a\/p)/gi,

        function ($1) {
            switch ($1) {
                case 'yyyy': return d.getFullYear();
                case 'mmmm': return wMonth[d.getMonth() + 1];
                    //case 'mmm':  return wMonth[d.getMonth()+1].substr(0, 3);
                case 'mm': return (d.getMonth() + 1).zf(2);
                case 'dddd': return wDay[d.getDay()];  //월요일, 화요일...
                case 'ddd': return wDay[d.getDay()].substr(0, 1); //월, 화, 수...
                case 'dd': return d.getDate().zf(2);  // 01, 02, 03..
                case 'hh': return ((h = d.getHours() % 12) ? h : 12).zf(2); //시간
                case 'nn': return d.getMinutes().zf(2);  //분
                case 'ss': return d.getSeconds().zf(2);  //초
                case 'a/p': return d.getHours() < 12 ? '오전' : '오후';
            }
        }
    );
};

// Zero-Fill
String.prototype.zf = function (l) { return '0'.string(l - this.length) + this; };
String.prototype.string = function (l) { var s = '', i = 0; while (i++ < l) { s += this; } return s; };
Number.prototype.zf = function (l) { return this.toString().zf(l); };
String.prototype.replaceAll = function (regex, replacement) {
    return this.split(regex).join(replacement);
};
