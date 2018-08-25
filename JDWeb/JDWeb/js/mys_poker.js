$(document).ready(function () {

    var handcount = 5;

    var label0 = $('#box0Counter');
    var label1 = $('#box1Counter');
    var label2 = $('#box2Counter');
    var label3 = $('#box3Counter');
    var label4 = $('#box4Counter');
    var label5 = $('#box5Counter');
    var label6 = $('#box6Counter');
    var label7 = $('#box7Counter');
    var label8 = $('#box8Counter');
    var label9 = $('#box9Counter');

    var list0 = $('#box0View');//配置牌堆
    var list1 = $('#box1View');//玩家1 
    var list2 = $('#box2View');//玩家2 
    var list3 = $('#box3View');//玩家3 
    var list4 = $('#box4View');//玩家4 
    var list5 = $('#box5View');//玩家5
    var list6 = $('#box6View');//玩家6
    var list7 = $('#box7View');//玩家7
    var list8 = $('#box8View');//玩家8
    var list9 = $('#box9View');//牌堆 剩余N张

    var btn0 = $('#Button0');
    var btn1 = $('#Button1');
    var btn2 = $('#Button2');
    var btn3 = $('#Button3');
    var btn4 = $('#Button4');
    var btn5 = $('#Button5');
    var btn6 = $('#Button6');
    var btn7 = $('#Button7');
    var btn8 = $('#Button8');
    var btn9 = $('#Button9');


    var div1 = $('#d1');
    var div2 = $('#d2');
    var div3 = $('#d3');
    var div4 = $('#d4');
    var div5 = $('#d5');
    var div6 = $('#d6');
    var div7 = $('#d7');
    var div8 = $('#d8');

    var arraylist = new Array([9]);
    arraylist[0] = list1;
    arraylist[1] = list2;
    arraylist[2] = list3;
    arraylist[3] = list4;
    arraylist[4] = list5;
    arraylist[5] = list6;
    arraylist[6] = list7;
    arraylist[7] = list8;


    var arrayObj = new Array([8]);　

    arrayObj[0] = div1;
    arrayObj[1] = div2;
    arrayObj[2] = div3;
    arrayObj[3] = div4;
    arrayObj[4] = div5;
    arrayObj[5] = div6;
    arrayObj[6] = div7;
    arrayObj[7] = div8;

    function UpdateMember() {

        for (var i = 0; i < arrayObj.length; i++) {

            arrayObj[i].show();
        }

        var curcount = Number($('#PlayerNum').val());

        if (curcount < arrayObj.length) {

            for (var i = curcount; i < arrayObj.length; i++) {

                arrayObj[i].hide();

            }
        }

    }

    UpdateMember();


    function InitList(cards) {

        list0.empty();
        list1.empty();
        list2.empty();
        list3.empty();
        list4.empty();
        list5.empty();
        list6.empty();
        list7.empty();
        list8.empty();
        list9.empty();

        $.each(cards, function (index, value) {

            list0.append(value);
        });

        UpdateListCount();
    }


    function UpdateListCount() {

        label0.text("配置牌堆 " + list0[0].childElementCount);

        label1.text("玩家1 " + list1[0].childElementCount);
        label2.text("玩家2 " + list2[0].childElementCount);
        label3.text("玩家3 " + list3[0].childElementCount);
        label4.text("玩家4 " + list4[0].childElementCount);
        label5.text("玩家5 " + list5[0].childElementCount);
        label6.text("玩家6 " + list6[0].childElementCount);
        label7.text("玩家7 " + list7[0].childElementCount);
        label8.text("玩家8 " + list8[0].childElementCount);
        label9.text("牌堆 " + list9[0].childElementCount);

        SortList(list0);
        SortList(list1);
        SortList(list2);
        SortList(list3);
        SortList(list4);
        SortList(list5);
        SortList(list6);
        SortList(list7);
        SortList(list8);
    }

    function SortList(list) {

        xx = list.find("option").sort(function (a, b) {

            var aText = $(a).val();

            var bText = $(b).val();

            if (Number(aText) > Number(bText)) return 1;

            if (Number(aText) < Number(bText)) return -1;

            return 0;

        });

        list.empty();

        list.append(xx);
    }

    function InitConfigPokers() {

        $.ajax({
            type: "POST",
            url: "Interface/GetCardConfig_Poker.ashx",
            dataType: "json",
            data: { gametype: $("#gametype").val() },
            success: function (msg) {

                handcount = msg["HandCount"];
                InitList(msg["cards"]);

            }
        });
    }

    function GetListCurCount(list) {

        return list[0].childElementCount;
    }

    //获取牌堆的第一张牌 同时移除该张牌
    function GetListFristOption(list) {

        if (GetListCurCount(list) <= 0)
            return null;

        var option = list[0][0];

        list[0].remove(option);

        return option;
    }

    function GetListRandomOption(list) {

        if (GetListCurCount(list) <= 0)
            return null;

        var indexs = new Array();
        for (var i = 0; i < GetListCurCount(list); i++) {
            indexs[i] = i;
        }

        var arr2 = indexs.sort(randomsort);

        var option = list[0][arr2[0]];

        return option;
    }

    //获取选定的那一张牌 同时移除该张牌
    function GetListSelectedOption(list) {

        if (GetListCurCount(list) <= 0)
            return null;

        //var option = list.find("option:selected");
        var option = null;
        for (var i = 0; i < GetListCurCount(list); i++) {

            if (list[0][i].selected) {

                option = list[0][i];
                break;
            }
        }


        list.find("option:selected").get(0).remove();;

        return option;
    }

    //添加牌到指定牌堆
    function AddListOption(list, option) {

        if (option == null)
            return;

        option.selected = false;

        list[0].append(option);
    }

    function randomsort(a, b) {

        return Math.random() > .5 ? -1 : 1;

    }


    //获取剩余牌堆的随机数列
    function GetLastRandomList() {

        var curcards = $("#box0View option").map(function () {
            return $(this).val();
        }).get();

        var arr2 = curcards.sort(randomsort);

        return arr2.join(",");
    }

    //随机填充剩余卡牌
    function RandomFillList() {

        var maxcount = GetListCurCount(list0);

        for (var j = 0; j < maxcount; j++) {

            var option = GetListRandomOption(list0);

            var isadd = false;
            for (var i = 0; i < arrayObj.length; i++) {

                //未隐藏
                if (!arrayObj[i].is(':hidden')) {

                    var list = arraylist[i];

                    if (GetListCurCount(list) < handcount) {

                        AddListOption(list, option);
                        isadd = true;
                        break;
                    }

                }

            }

            if (!isadd) {
                AddListOption(list9, option);
                list9.scrollTop(list9[0].scrollHeight);
            }

        }


        list0.empty();

        UpdateListCount();
    }

    btn0.click(function () {
        //自动分配到各自牌堆
        RandomFillList();
        //var maxcount = GetListCurCount(list0);


        //for (var j = 0; j < maxcount; j++) {

        //    var option = GetListFristOption(list0);

        //    var isadd = false;
        //    for (var i = 0; i < arrayObj.length; i++) {

        //        //未隐藏
        //        if (!arrayObj[i].is(':hidden')) {

        //            var list = arraylist[i];

        //            if (GetListCurCount(list) < handcount) {

        //                AddListOption(list, option);
        //                isadd = true;
        //                break;
        //            }

        //        }

        //    }

        //    if (!isadd) {
        //        AddListOption(list9, option);
        //        list9.scrollTop(list9[0].scrollHeight);
        //    }

        //}
        

        //UpdateListCount();

    });

    btn1.click(function () {

        var maxcount = GetListCurCount(list1);

        for (var i = 0; i < maxcount; i++) {

            var option = GetListFristOption(list1);

            AddListOption(list0, option);
        }

        UpdateListCount();
    });

    btn2.click(function () {

        var maxcount = GetListCurCount(list2);

        for (var i = 0; i < maxcount; i++) {

            var option = GetListFristOption(list2);

            AddListOption(list0, option);
        }

        UpdateListCount();
    });

    btn3.click(function () {

        var maxcount = GetListCurCount(list3);

        for (var i = 0; i < maxcount; i++) {

            var option = GetListFristOption(list3);

            AddListOption(list0, option);
        }

        UpdateListCount();
    });

    btn4.click(function () {

        var maxcount = GetListCurCount(list4);

        for (var i = 0; i < maxcount; i++) {

            var option = GetListFristOption(list4);

            AddListOption(list0, option);
        }

        UpdateListCount();
    });

    btn5.click(function () {

        var maxcount = GetListCurCount(list5);

        for (var i = 0; i < maxcount; i++) {

            var option = GetListFristOption(list5);

            AddListOption(list0, option);
        }

        UpdateListCount();
    });

    btn6.click(function () {

        var maxcount = GetListCurCount(list6);

        for (var i = 0; i < maxcount; i++) {

            var option = GetListFristOption(list6);

            AddListOption(list0, option);
        }

        UpdateListCount();
    });

    btn7.click(function () {

        var maxcount = GetListCurCount(list7);

        for (var i = 0; i < maxcount; i++) {

            var option = GetListFristOption(list7);

            AddListOption(list0, option);
        }

        UpdateListCount();
    });

    btn8.click(function () {

        var maxcount = GetListCurCount(list8);

        for (var i = 0; i < maxcount; i++) {

            var option = GetListFristOption(list8);

            AddListOption(list0, option);
        }

        UpdateListCount();
    });

    btn9.click(function () {

        var maxcount = GetListCurCount(list9);

        for (var i = 0; i < maxcount; i++) {

            var option = GetListFristOption(list9);

            AddListOption(list0, option);
        }

        UpdateListCount();
    });

    list0.click(function () {

        var option = GetListSelectedOption(list0);

        var isadd = false;
        for (var i = 0; i < arrayObj.length; i++) {

            //未隐藏
            if (!arrayObj[i].is(':hidden')) {

                var list = arraylist[i];

                if (GetListCurCount(list) < handcount) {

                    AddListOption(list, option);
                    isadd = true;
                    break;
                }

            }

        }

        if (!isadd) {
            AddListOption(list9, option);
            list9.scrollTop(list9[0].scrollHeight);
        }

        UpdateListCount();

    });

    list1.click(function () {

        var option = GetListSelectedOption(list1);

        AddListOption(list0, option);

        UpdateListCount();
    });

    list2.click(function () {

        var option = GetListSelectedOption(list2);

        AddListOption(list0, option);

        UpdateListCount();
    });


    list3.click(function () {

        var option = GetListSelectedOption(list3);

        AddListOption(list0, option);

        UpdateListCount();
    });

    list4.click(function () {

        var option = GetListSelectedOption(list4);

        AddListOption(list0, option);

        UpdateListCount();
    });

    list5.click(function () {

        var option = GetListSelectedOption(list5);

        AddListOption(list0, option);

        UpdateListCount();
    });


    list6.click(function () {

        var option = GetListSelectedOption(list6);

        AddListOption(list0, option);

        UpdateListCount();
    });

    list7.click(function () {

        var option = GetListSelectedOption(list7);

        AddListOption(list0, option);

        UpdateListCount();
    });

    list8.click(function () {

        var option = GetListSelectedOption(list8);

        AddListOption(list0, option);

        UpdateListCount();
    });

    list9.click(function () {

        var option = GetListSelectedOption(list9);

        AddListOption(list0, option);

        UpdateListCount();
    });

    $.ajax({
        type: "POST",
        url: "Interface/GetGameTypes_Poker.ashx",
        dataType: "json",
        data: {},
        success: function (msg) {


            $('#gametype').empty();
            $.each(msg, function (index, value) {

                $('#gametype').append(value);
            });

            InitConfigPokers();

        }
    });

    var table = $('#table1').DataTable({

        "autoWidth": false,
        "ajax": "Interface/GetGameCards_Poker.ashx",
        "columns": [
            { "data": "index" },
            { "data": "GameType" },
            { "data": "Cards" },
            { "data": "Remarks" },
            { "data": "Status" },
            { "data": "" }
        ],
        "columnDefs": [{

            "targets": -1,
            "data": null,
            "defaultContent": "<input id=\"input_open\" type=\"button\" class=\"btn btn-small btn-primary btn-edit\" value=\"激活\"/><input id=\"input_stop\" type=\"button\" class=\"btn btn-small btn-danger btn-del\" value=\"停用\" /><input id=\"input_close\" type=\"button\" class=\"btn btn-small btn-danger btn-del\" value=\"移除\" />"
        }
        ],

    });


    $('#table1 tbody').on('click', 'input#input_open', function () {

        var data = table.row($(this).parents('tr')).data();

        var index = data["index"];
        var gametype = data["GameType"];

        $.ajax({
            type: "POST",
            url: "Interface/UseCardConfigs_Poker.ashx",
            dataType: "json",
            data: { index: index, gametype: gametype },
            success: function (msg) {

                alert(msg);
                table.ajax.reload();

            }
        });


    });

    $('#table1 tbody').on('click', 'input#input_stop', function () {

        var data = table.row($(this).parents('tr')).data();

        var index = data["index"];

        $.ajax({
            type: "POST",
            url: "Interface/StopCardConfigs_Poker.ashx",
            dataType: "json",
            data: { index: index },
            success: function (msg) {

                alert(msg);
                table.ajax.reload();

            }
        });


    });

    $('#table1 tbody').on('click', 'input#input_close', function () {

        var data = table.row($(this).parents('tr')).data();

        var index = data["index"];

        $.ajax({
            type: "POST",
            url: "Interface/RemoveCardConfigs_Poker.ashx",
            dataType: "json",
            data: { index: index },
            success: function (msg) {

                alert(msg);
                table.ajax.reload();

            }
        });


    });

    $("#PlayerNum").change(function () {

        UpdateMember();

    });

    $("#gametype").change(function () {

        InitConfigPokers();
    });

    $("#ButtonClear").click(function () {


        InitConfigPokers();

    });


    $("#button1").click(function () {

        

        //var curcards = $("#box2View option").map(function () {
        //    return $(this).val();
        //}).get().join(",");

        var curcards = null;

        for (var i = 0; i < arraylist.length;i++) {

            var list = arraylist[i];

            if (GetListCurCount(list) >0) {

                if (curcards != null) {

                    curcards += ",";
                } else {

                    curcards = "";
                }

                curcards += list.find("option").map(function () {
                    return $(this).val();
                }).get().join(",");
            }
        }

        if (GetListCurCount(list9) > 0) {

            if (curcards != null) {

                curcards += ",";
            } else {

                curcards = "";
            }

            curcards += list9.find("option").map(function () {
                return $(this).val();
            }).get().join(",");
        }

        if (GetListCurCount(list0) > 0) {

            if (curcards != null) {

                curcards += ",";
            } else {

                curcards = "";
            }

            curcards += GetLastRandomList();
        }

        
        $.ajax({
            type: "POST",
            url: "Interface/SetGameCards_Poker.ashx",
            dataType: "json",
            data: { gametype: $("#gametype").val(), cards: curcards, isuse: $("#isuse").is(':checked'), remarks: $("#remarks").val() },
            success: function (msg) {

                alert(msg);
                table.ajax.reload();

            }
        });

    });

    //鼠标移动

    var self = this;

    this.curData = null;

    this.$sorc = $('#box0View');


    $("#box0View").on("mousedown", "option", function (e) {

        self.curData = e.target;

        self.curData.selected = true;

    });

    $(document).on("mouseup", function (e) {

        var tmpDom = e.target.id;

        if (e.target.localName == "option") {

            tmpDom = e.target.parentElement.id;
        }

        if ("box0View" == tmpDom) {

            var option = GetListSelectedOption(list0);

            var isadd = false;
            for (var i = 0; i < arrayObj.length; i++) {

                //未隐藏
                if (!arrayObj[i].is(':hidden')) {

                    var list = arraylist[i];

                    if (GetListCurCount(list) < handcount) {

                        AddListOption(list, option);
                        isadd = true;
                        break;
                    }

                }

            }

            if (!isadd) {
                AddListOption(list9, option);
                list9.scrollTop(list9[0].scrollHeight);
            }

            UpdateListCount();

            var $dom = $(self.curData);
            self.curData.selected = false;
            $dom.css("position", "static");

            self.curData = null;

            return;
        }


        if ("box1View" != tmpDom && "box2View" != tmpDom && "box3View" != tmpDom && "box4View" != tmpDom && "box5View" != tmpDom && "box6View" != tmpDom && "box7View" != tmpDom && "box8View" != tmpDom && "box9View" != tmpDom) {//在放置区以外的地方就清空数据

            var $dom = $(self.curData);

            $dom.css("position", "static");

            self.curData = null;

            return;

        }

        var listcount = $("#" + tmpDom).find("option").length;

        if ("box1View" == tmpDom && listcount >= handcount) {

            var $dom = $(self.curData);
            self.curData.selected = false;
            $dom.css("position", "static");

            self.curData = null;

            return;
        }

        if ("box2View" == tmpDom && listcount >= handcount) {

            var $dom = $(self.curData);
            self.curData.selected = false;
            $dom.css("position", "static");

            self.curData = null;

            return;
        }

        if ("box3View" == tmpDom && listcount >= handcount) {

            var $dom = $(self.curData);
            self.curData.selected = false;
            $dom.css("position", "static");

            self.curData = null;

            return;
        }

        if ("box4View" == tmpDom && listcount >= handcount) {

            var $dom = $(self.curData);
            self.curData.selected = false;
            $dom.css("position", "static");

            self.curData = null;

            return;
        }

        if ("box5View" == tmpDom && listcount >= handcount) {

            var $dom = $(self.curData);
            self.curData.selected = false;
            $dom.css("position", "static");

            self.curData = null;

            return;
        }

        if ("box6View" == tmpDom && listcount >= handcount) {

            var $dom = $(self.curData);
            self.curData.selected = false;
            $dom.css("position", "static");

            self.curData = null;

            return;
        }

        if ("box7View" == tmpDom && listcount >= handcount) {

            var $dom = $(self.curData);
            self.curData.selected = false;
            $dom.css("position", "static");

            self.curData = null;

            return;
        }

        if ("box8View" == tmpDom && listcount >= handcount) {

            var $dom = $(self.curData);
            self.curData.selected = false;
            $dom.css("position", "static");

            self.curData = null;

            return;
        }

        if ("box9View" == tmpDom && listcount >= handcount) {

            var $dom = $(self.curData);
            self.curData.selected = false;
            $dom.css("position", "static");

            self.curData = null;

            return;
        }

        if (null != self.curData) {

            var $dom = $(self.curData);

            self.curData.selected = false;
            $dom.css("position", "static");

            $("#" + tmpDom).append($dom);

            //$dom.click(function () {

            //    $(this).appendTo(self.$sorc);

            //});

            UpdateListCount();

        }

        self.curData = null;

    });


    //元素跟随移动效果

    $(document).on("mousemove", function (e) {


        if (null != self.curData) {

            var sh = $(document).scrollTop();
            var sw = $(document).scrollLeft();

            var $dom = $(self.curData);

            $dom.css({ position: "fixed", 'top': e.pageY + 10 - sh, 'left': e.pageX + 10 - sw, 'z-index': 2 });

        }

    });



});
