$(function () {
    GetTypeName();
    events();

});
function GetTypeName() {
    var i = 0;
    $.ajax({
        url: "http://10.1.156.58:8089/api/Zrw/GetGreensType",
        type: "get",
        success: function (data) {

            $("#u").empty();
            $(data).each(function () {
                var line = '<li id="a' + i + '" ><span id="b' + i + '" >' + this.GreensType + '</span></li>'
                $("#u").append(line);
                i++;
            })
            GetGreens("蛋及三明治");
            $('.left-menu ul li').each(function (ind) {
                $("#left li:first-child").addClass("active");
                $(this).on('click', function () {
                    $(this).addClass("active").siblings().removeClass("active");
                    var n = $(".left-menu ul li").index(this);
                    $(".left-menu ul li").index(this);
                    $(".con>div:eq(" + n + ")").show();
                    GetGreens($("#b" + ind).text());
                });

            });
        }
    })


}
function GetGreens(TypeName) {
    $.ajax({
        url: "http://10.1.156.58:8089/api/Zrw/GetGreensInType?TypeName=" + TypeName,
        type: "get",
        success: function (data) {
            $("#uu").empty();
            $(data).each(function () {
                var line = '<li>'
                    + '<div class="menu-img"><img src="' + this.GreensImgUrl + '"></div>'
                    + '<div class="menu-txt">'
                    + '<h4 data-icon="00">' + this.GreensName + '</h4>'
                    + '<h5 style="display:none">' + this.GreensId + '</h5>'
                    + '<p class="list1">家常菜</p>'
                    + '<p class="list2">'
                    + '<b>￥</b><b>' + this.GreensPrice + '</b>'
                    + '</p>'
                    + '<div class="btn">'
                    + '<button class="minus"></button>'
                    + '<i>0</i>'
                    + '<button class="add"></button>'
                    + '<i class="price">2.2</i>'
                    + '</div>'
                    + '</div>'
                    + '</li>'
                $("#uu").append(line);
            })
            InsertOrderTable();
        }
    })
}
function events() {
    var greensNum = -1;
    //弹框 - 加
    $(".ad").click(function () {
        var n = parseFloat($(this).prev().text()) + 1;
        if (n == 0) { return; }
        $(this).prev().text(n);
        var danjia = $(this).next().text();   //获取单价
        var a = $("#totalpriceshow").html();  //获取当前所选总价
        var b = a * 1 + danjia * 1;
        $("#totalpriceshow").html(b);         //计算当前所选总价
        var nm = $("#totalcountshow").html(); //获取数量

        $("#totalcountshow").html(nm * 1 + 1);
    });

    //弹框 - 减
    $(".ms").click(function () {
        var n = $(this).next().text();
        if (n > 1) {
            var num = parseFloat(n) - 1;
            $(this).next().text(num);//减1

            var danjia = $(this).nextAll(".price").text();//获取单价
            var a = $("#totalpriceshow").html();//获取当前所选总价
            var b = (a * 3 - danjia * 3) / 3;
            $("#totalpriceshow").html(b);//计算当前所选总价

            var nm = $("#totalcountshow").html();//获取数量
            $("#totalcountshow").html(nm * 1 - 1);
        }

        //如果数量小于或等于0则隐藏减号和数量
        if (num <= 0) {
            $(this).next().css("display", "none");
            $(this).css("display", "none");
            jss();//改变按钮样式
            return
        }
    });

    //点击遮罩，隐藏商品详情弹框
    $(".up").click(function () {
        $(".subFly").hide();
    });

    //点击加入购物车 - 按钮
    var flag = false;
    $(".foot").click(function () {
        greensNum = greensNum + 1;
        var n = $('.ad').prev().text();
        var num = parseFloat(n) + 1;
        if (num == 0) { return; }
        $('.ad').prev().text(num);
        var danjia = $('.ad').next().text();  //获取单价
        var a = $("#totalpriceshow").html();  //获取当前所选总价
        $("#totalpriceshow").html((a * 1 + danjia * 1).toFixed(2));//计算当前所选总价
        var nm = $("#totalcountshow").html(); //获取数量
        $("#totalcountshow").html(nm * 1 + 1);
        jss();   //改变按钮样式
        $(".subFly").hide();
        var ms = e.text(num - 1);
        if (ms != 0) {      //判断是否显示减号及数量
            e.css("display", "inline-block");
            e.prev().css("display", "inline-block")
        }
        var m = $(".subName dd:nth-child(2) p:nth-child(1)").text();    //当前商品名称
        var ids = $(".subName dd:nth-child(2) h5:nth-child(2)").text();//获取当前Id
        var taste = $(".subChose .m-active").text();
        var acount = n;
        var sum = parseFloat($(".subName dd p:nth-child(3) span:nth-child(2)").text()) * acount;
        var price = parseFloat($(".subName dd p:nth-child(3) span:nth-child(2)").text());

        var dataIconN = $(this).parent().children(".subName").children("dd").children("p:first-child").attr("data-icon");

        //判断购物车里是否有商品，是否有相同规格的商品
        if ($(".list-content ul li").length <= 0) {
            var addtr = '<li class="food">';
            addtr += '<div><span class="accountName" id="Gname' + greensNum + '" data-icon="' + dataIconN + '">' + m + '</span><span class="taste">' + taste + '</span><span class="ids" style="display:none" id="Gid' + greensNum + '">' + ids + '</span></div>';
            addtr += '<div><span>￥</span><span class="accountPrice">' + sum + '</span></div>';
            addtr += '<div class="btn">';
            addtr += '<button class="ms2" style="display: inline-block;"></button>';
            addtr += '<span class="li_acount" id="num' + greensNum + '">' + acount + '</span>';
            addtr += '<button class="ad2"></button>';
            addtr += '<i class="price" style="display: none;" id="Gprice' + greensNum + '">' + price + '</i>';
            addtr += '</div>';
            addtr += '</li>';
            $(".list-content ul").append(addtr);
            return;

        } else {
            $(".list-content ul li").each(function () {
                if ($(this).find("span.accountName").html() == m && $(this).find(".taste").html() == taste) {
                    var count = parseInt($(this).find(".li_acount").html());
                    count += parseInt(n);
                    $(this).find(".li_acount").html(count); //对商品的数量进行重新赋值
                    flag = true;
                    return false;
                } else {
                    flag = false;
                }
            })
        }
        //如果为默认值也就是说里面没有此商品，所以添加此商品。
        if (flag == false) {
            var addtr = '<li class="food">';
            addtr += '<div><span class="accountName" id="Gname' + greensNum + '"  data-icon="' + dataIconN + '">' + m + '</span><span class="taste">' + taste + '</span><span class="ids" style="display:none" id="Gid' + greensNum + '">' + ids + '</span></div>';
            addtr += '<div><span>￥</span><span class="accountPrice">' + sum + '</span></div>';
            addtr += '<div class="btn">';
            addtr += '<button class="ms2" style="display: inline-block;"></button><span class="li_acount" id="num' + greensNum + '">' + acount + '</span><button class="ad2"></button><i class="price" style="display: none;" id="Gprice' + greensNum + '">' + price + '</i>';
            addtr += '</div>';
            addtr += '</li>';
            $(".list-content ul").append(addtr);

        }

		/*$(".list-content>ul").append('<li class="food"><div><span class="accountName" data-icon="'+dataIconN+'">'+m+'</span><span class="taste">'+taste+'</span></div><div><span>￥</span><span class="accountPrice">'+sum+'</span></div><div class="btn"><button class="ms2" style="display: inline-block;"></button><i style="display: inline-block;">'+acount+'</i><button class="ad2"></button><i class="price" style="display: none;">'+price+'</i></div></li>');
		var display = $(".shopcart-list.fold-transition").css('display');
		if(display=="block"){
			$("document").click(function(){
				$(".shopcart-list.fold-transition").hide();
			})
		}*/
    });

    //购物车 - 加
    $(document).on('click', '.ad2', function () {
        var n = parseInt($(this).prev().text()) + 1;
        $(this).prev().text(n);    //当前商品数量+1
        e.text(n);    //赋值给商品列表的数量
        var p = parseFloat($(this).next().text());    //隐藏的价格
        $(this).parent().prev().children("span.accountPrice").text((p * n).toFixed(2));  //计算该商品规格的总价值
        $("#totalcountshow").text(parseFloat($("#totalcountshow").text()) + 1);   //总数量＋1
        $("#totalpriceshow").text(parseFloat($("#totalpriceshow").text()));   //总价加上该商品价格

        sum = $("#totalpriceshow").text(parseFloat($("#totalpriceshow").text()) + p);
    });

    //购物车 - 减
    $('.list-content').on('click', '.ms2', function () {
        var a = parseFloat($(this).siblings(".price").text());  //当前商品单价
        var n = parseInt($(this).next().text()) - 1;  //当前商品数量
        var s = parseFloat($("#totalpriceshow").text());  //总金额
        if (n == 0) {
            greensNum = greensNum - 1;
            $(this).parent().parent().remove();
            $(".up1").hide();
            e.css("display", "none");
            e.prev().css("display", "none")
        }
        $(this).next().text(n);
        e.text(n);    //赋值给商品列表的数量
        $(this).parent().prev().children("span:nth-child(2)").text(a * n);

        $("#totalcountshow").text(parseInt($("#totalcountshow").text()) - 1);
        $("#totalpriceshow").text(s - a);
        sum = $("#totalpriceshow").text(s - a);
        if (parseFloat($("#totalcountshow").text()) == 0) {
            $(".shopcart-list").hide();
        }
    });

    function jss() {
        var m = $("#totalcountshow").html();
        if (m > 0) {
            $(".right").find("a").removeClass("disable");
        } else {
            $(".right").find("a").addClass("disable");
        }
    };

    //选项卡
    $(".con>div").hide();
    $(".con>div:eq(0)").show();
    $(".subFly").hide();
    $(".close").click(function () {
        $(".subFly").hide();
    });
    $(".footer>.left").click(function () {
        var content = $(".list-content>ul").html();
        if (content != "") {
            $(".shopcart-list.fold-transition").toggle();
            $(".up1").toggle();
        }
    });

    $(".up1").click(function () {
        $(".up1").hide();
        $(".shopcart-list.fold-transition").hide();
    })

    //清空购物车
    $(".empty").click(function () {
        $(".list-content>ul").html("");
        $("#totalcountshow").text("0");
        $("#totalpriceshow").text("0");
        $(".minus").next().text("0");
        $(".minus").hide();
        $(".minus").next().hide();
        $(".shopcart-list").hide();
        $(".up1").hide();
        jss();//改变按钮样式
    });
    //去结算
    $("#btnselect").click(function () {
        //定义一个明细表数组
        var obj = [];
        //定义一个菜品Id数组
        var GreensIds = [];
        //添加订单的方法
        var obj1 =
        {
            Oen: CurentTime(),
            OrderStatic: 0,
            OrderTime: Date.now().toLocaleString(),
            OrderRemark: "",
            OrderPrice: 294,
            RepastWay: "",
            Uid: $("#UserId").val(),
            Sid: "92E2E1F1-0CA8-476A-B376-4EF6D0677DE5"
        };
        $.ajax({
            url: "http://10.1.156.58:8089/api/Zrw/InsertOrderTable",
            type: "Post",
            data: obj1,
            success: function (data) {
                if (data > 0) {
                    //查询最新订单Id
                    $.ajax({
                        url: "http://10.1.156.58:8089/api/Zrw/GetOrderFirst",
                        type: "get",
                        success: function (data) {
                            $(data).each(function () {
                                Oen = this.Oen;
                                Oid = this.OrderId;
                                $(".food").each(function (i, v) {
                                    Gnum = $("#num" + i).text();
                                    Gid = $("#Gid" + i).text();
                                    Gprice = $("#Gprice" + i).text();
                                    obj.push({ Gid: Gid, Gprice: Gprice, Gnum: Gnum, Oid: Oid });
                                    GreensIds.push(Gid);
                                })
                                DetailTable(obj, GreensIds);
                            })
                        }
                    })
                }
            }
        })
    })
}
function DetailTable(obj, GreensIds) {
    $.ajax({
        url: "http://10.1.156.58:8089/api/Zrw/InsertDetailTable",
        type: "Post",
        data: JSON.stringify(obj),
        contentType: "application/json",
        success: function (data) {
            if (data > 0) {
                location.href = "/Default/Order_notes?Oen=" + Oen + "&GreensIds=" + GreensIds;
            }
        }
    })
}
function InsertOrderTable() {
    //商品点击增加
    $(".add").click(function () {
        $(".subFly").show();
        var n = $(this).prev().text();
        var num = parseFloat(n);
        if (n == 0) { num = 1 }
        $(".ad").prev().text(num);
        e = $(this).prev();

        var parent = $(this).parent();
        var name = parent.parent().children("h4").text();
        var ids = parent.parent().children("h5").text();

        var price = parseFloat(parent.prev().children("b:nth-child(2)").text());

        var src = $(this).parent().parent().prev().children()[0].src;

        $(".subName dd p:nth-child(1)").html(name);
        $(".subName dd h5:nth-child(2)").html(ids);
        $(".pce").text(price);
        $(".imgPhoto").attr('src', src);
        $(this).siblings(".price").text(price);
        $(".mydd .price").html(price);
        $(".choseValue").text($(".subChose .m-active").text());
        var dataIcon = $(this).parent().parent().children("h4").attr("data-icon");
        $(".subName dd p:first-child").attr("data-icon", dataIcon);
    });
    $(".minus").click(function () {
        $('.shopcart-list,.up1').show();
    });
    //口味选择
    $(".subChose dd").click(function () {
        $(this).addClass("m-active").siblings().removeClass("m-active");
        $(".choseValue").text($(".subChose .m-active").text());
    })

}

function CurentTime() {
    let now = new Date();
    let year = now.getFullYear();       //年
    let month = now.getMonth() + 1;     //月
    let day = now.getDate();            //日
    let hh = now.getHours();            //时
    let mm = now.getMinutes();          //分
    let ss = now.getSeconds();           //秒
    let clock = year + "";
    if (month < 10)
        clock += "0";

    clock += month + "";

    if (day < 10)
        clock += "0";

    clock += day + "";

    if (hh < 10)
        clock += "0";

    clock += hh + "";
    if (mm < 10) clock += '0';
    clock += mm + "";

    if (ss < 10) clock += '0';
    clock += ss;
    return (clock);
}