$(document).ready(function () {

    var label0 = $('#box1Counter');
    var label1 = $('#box2Counter');
    var label2 = $('#box3Counter');
    var label3 = $('#box4Counter');
    var label4 = $('#box5Counter');
    var label5 = $('#box6Counter');
   
    var list0 = $('#box1View');//配置牌堆
    var list1 = $('#box2View');//庄家 14张
    var list2 = $('#box3View');//玩家1 13张
    var list3 = $('#box4View');//玩家2 13张
    var list4 = $('#box5View');//玩家3 13张
    var list5 = $('#box6View');//牌堆 剩余N张

    var btn0 = $('#Button1');
    var btn1 = $('#Button2');
    var btn2 = $('#Button3');
    var btn3 = $('#Button4');
    var btn4 = $('#Button5');
    var btn5 = $('#Button6');

    function InitList(cards)
    {
        list0.empty();
        list1.empty();
        list2.empty();
        list3.empty();
        list4.empty();
        list5.empty();

        $.each(cards, function (index, value) {

            list0.append(value);
        });

        UpdateListCount();
    }

    function UpdateListCount() {

        label0.text("配置牌堆 " + list0[0].childElementCount);

        label1.text("庄家 " + list1[0].childElementCount);
        label2.text("玩家1 " + list2[0].childElementCount);
        label3.text("玩家2 " + list3[0].childElementCount);
        label4.text("玩家3 " + list4[0].childElementCount);
        label5.text("牌堆 " + list5[0].childElementCount);


        xx = $('#box1View option').sort(function (a, b) {

            var aText = $(a).val();

            var bText = $(b).val();

            if (Number(aText) > Number(bText)) return 1;

            if (Number(aText) < Number(bText)) return -1;

            return 0;

        });

        $('#box1View').empty();

        $('#box1View').append(xx);


        xx1 = $('#box2View option').sort(function (a, b) {

            var aText = $(a).val();

            var bText = $(b).val();

            if (Number(aText) > Number(bText)) return 1;

            if (Number(aText) < Number(bText)) return -1;

            return 0;

        });

        $('#box2View').empty();

        $('#box2View').append(xx1);

        xx2 = $('#box3View option').sort(function (a, b) {

            var aText = $(a).val();

            var bText = $(b).val();

            if (Number(aText) > Number(bText)) return 1;

            if (Number(aText) < Number(bText)) return -1;

            return 0;

        });

        $('#box3View').empty();

        $('#box3View').append(xx2);


        xx3 = $('#box4View option').sort(function (a, b) {

            var aText = $(a).val();

            var bText = $(b).val();

            if (Number(aText) > Number(bText)) return 1;

            if (Number(aText) < Number(bText)) return -1;

            return 0;

        });

        $('#box4View').empty();

        $('#box4View').append(xx3);

        xx4 = $('#box5View option').sort(function (a, b) {

            var aText = $(a).val();

            var bText = $(b).val();

            if (Number(aText) > Number(bText)) return 1;

            if (Number(aText) < Number(bText)) return -1;

            return 0;

        });

        $('#box5View').empty();

        $('#box5View').append(xx4);

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

    //获取选定的那一张牌 同时移除该张牌
    function GetListSelectedOption(list) {

        if (GetListCurCount(list) <= 0)
            return null;

        //var option = list.find("option:selected");
        var option = null;
        for (var i = 0; i < GetListCurCount(list);i++) {

            if (list[0][i].selected) {

                option = list[0][i];
                break;
            }
        }


        list.find("option:selected").get(0).remove();;

        return option;
    }

    //添加牌到指定牌堆
    function AddListOption(list,option) {

        if (option == null)
            return;

        list[0].append(option);
    }


    btn0.click(function () {
        //自动分配到各自牌堆

        var maxcount = GetListCurCount(list0);

        for (var i = 0; i < maxcount;i++) {

            var option = GetListFristOption(list0);

            if (GetListCurCount(list1) < 14) {

                AddListOption(list1, option);

            } else if (GetListCurCount(list2) < 13) {

                AddListOption(list2, option);

            } else if (GetListCurCount(list3) < 13) {

                AddListOption(list3, option);

            } else if (GetListCurCount(list4) < 13) {

                AddListOption(list4, option);

            } else {

                AddListOption(list5, option);
                list5.scrollTop(list5[0].scrollHeight);
            }

        }

        UpdateListCount();

    });

    btn1.click(function () {
        //庄家牌 全部退回配置牌堆
        var maxcount = GetListCurCount(list1);

        for (var i = 0; i < maxcount; i++) {

            var option = GetListFristOption(list1);

            AddListOption(list0, option);
        }

        UpdateListCount();
    });

    btn2.click(function () {
        //玩家1牌 全部退回配置牌堆

        var maxcount = GetListCurCount(list2);

        for (var i = 0; i < maxcount; i++) {

            var option = GetListFristOption(list2);

            AddListOption(list0, option);
        }

        UpdateListCount();
    });

    btn3.click(function () {
        //玩家2牌 全部退回配置牌堆

        var maxcount = GetListCurCount(list3);

        for (var i = 0; i < maxcount; i++) {

            var option = GetListFristOption(list3);

            AddListOption(list0, option);
        }

        UpdateListCount();

    });

    btn4.click(function () {
        //玩家3牌 全部退回配置牌堆

        var maxcount = GetListCurCount(list4);

        for (var i = 0; i < maxcount; i++) {

            var option = GetListFristOption(list4);

            AddListOption(list0, option);
        }

        UpdateListCount();
    });

    btn5.click(function () {
        //牌堆 全部退回配置牌堆

        var maxcount = GetListCurCount(list5);

        for (var i = 0; i < maxcount; i++) {

            var option = GetListFristOption(list5);

            AddListOption(list0, option);
        }

        UpdateListCount();
    });


    $.ajax({
        type: "POST",
        url: "Interface/GetGameTypes.ashx",
        dataType: "json",
        data: {},
        success: function (msg) {

            $('#gametype').empty();
            $.each(msg, function (index, value) {

                $('#gametype').append(value);
            });

        }
    });


    $.ajax({
        type: "POST",
        url: "Interface/GetCardConfig.ashx",
        dataType: "json",
        data: { gametype: $("#gametype").val() },
        success: function (msg) {

            InitList(msg);


        }
    });

    $("#gametype").change(function () {

        $.ajax({
            type: "POST",
            url: "Interface/GetCardConfig.ashx",
            dataType: "json",
            data: { gametype: $("#gametype").val() },
            success: function (msg) {

                InitList(msg);

            }
        });

    });


    list0.click(function () {

        var option = GetListSelectedOption(list0);

        if (GetListCurCount(list1) < 14) {

            AddListOption(list1, option);

        } else if (GetListCurCount(list2) < 13) {

            AddListOption(list2, option);

        } else if (GetListCurCount(list3) < 13) {

            AddListOption(list3, option);

        } else if (GetListCurCount(list4) < 13) {

            AddListOption(list4, option);

        } else {

            AddListOption(list5, option);
            list5.scrollTop(list5[0].scrollHeight);
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


    var table = $('#table1').DataTable({

        "autoWidth": false,
        "ajax": "Interface/GetGameCards.ashx",
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
            url: "Interface/UseCardConfigs.ashx",
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
            url: "Interface/StopCardConfigs.ashx",
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
            url: "Interface/RemoveCardConfigs.ashx",
            dataType: "json",
            data: { index: index },
            success: function (msg) {

                alert(msg);
                table.ajax.reload();

            }
        });


    });

    


    $("#button1").click(function () {

        //var curcards = $("#box2View option").map(function () {
        //    return $(this).val();
        //}).get().join(",");

        var curcards = null;
        if (GetListCurCount(list1) > 0) {

            curcards = $("#box2View option").map(function () {
                return $(this).val();
            }).get().join(",");
        }

        if (GetListCurCount(list2) > 0) {

            curcards += ",";

            curcards += $("#box3View option").map(function () {
                return $(this).val();
            }).get().join(",");

        }
       

        if (GetListCurCount(list3) > 0) {

            curcards += ",";

            curcards += $("#box4View option").map(function () {
                return $(this).val();
            }).get().join(",");

        }
        

        if (GetListCurCount(list4) > 0) {

            curcards += ",";

            curcards += $("#box5View option").map(function () {
                return $(this).val();
            }).get().join(",");
        }
        

        if (GetListCurCount(list5) > 0) {

            curcards += ",";

            curcards += $("#box6View option").map(function () {
                return $(this).val();
            }).get().join(",");

        }
        
        $.ajax({
            type: "POST",
            url: "Interface/SetGameCards.ashx",
            dataType: "json",
            data: { gametype: $("#gametype").val(), cards: curcards, isuse: $("#isuse").is(':checked'), remarks: $("#remarks").val() },
            success: function (msg) {

                alert(msg);
                table.ajax.reload();

            }
        });

    });

   

    
});
