$(function () {
    var OTable = new TableInit();
    OTable.Init();
    //条件查询click事件注册
    $("#btn_query").click(function () {
        $("#IntroTable").bootstrapTable('destroy');
        OTable.Init();
    });
});
var TableInit = function () {
    var OTableInit = new Object();
    var $table = $("#IntroTable");
    OTableInit.Init = function () {
        $table.bootstrapTable({
            method: 'get',//用get请求
            url: '/PerOrComIntroGuidance/Index',
            toolbar: '#toolbar',
            showRefresh: true,
            columns: [{
                title: '编号',
                align: 'center',
                valign: 'bottom',
                formatter: function (value, row, index) {
                    var pageNumber = $table.bootstrapTable('getOptions').pageNumber;
                    var pageSize = $table.bootstrapTable('getOptions').pageSize;
                    return (pageNumber - 1) * pageSize + index + 1;
                }
            },
            {
                title: '问题内容',
                field: 'questionMsg',
                align: 'center'
            },
            {
                title: '问题类型',
                field: 'type',
                align: 'center',
                formatter: function (value, row, index) {
                    var result = "";
                    if (value) {
                        result += " <span class=\"label label-success\">招聘者</span>"
                    } else {
                        result += "<span class=\"label label-warning\">被招聘者</span>"
                    }
                    return result;
                }
            },
            {
                title: '创建时间',
                field: 'createTime',
                align: 'center'
            },
            {
                title: '操作',
                field: 'id',
                align: 'center',
                formatter: function (value, row, index) {
                    var result = "<a href='/PerOrComIntroGuidance/Edit/" + value + "'>编辑</a>";
                    return result;
                }
            }],
            striped: true,//是否显示行间隔色
            cache: false,//是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true,//是否显示分页（*）
            pageNumber: 1,//初始化加载第一页，默认第一页
            pageSize: 5, //每页的记录行数（*）
            queryParamsType: 'undefined',
            queryParams: OTableInit.Query,
            // 设置为 ''  在这种情况下传给服务器的参数为：pageSize,pageNumber
            pageList: [5, 10, 20, 50],//可供选择的每页的行数（*）
            sidePagination: "server",   //分页方式：client客户端分页，server服务端分页（*）
            minimumCountColumns: 2,    //最少允许的列数
        });
    }
    OTableInit.Query = function (params) {
        var temp = {
            pageSize: params.pageSize,
            pageNumber: params.pageNumber,
            Type: $("#txt_search_Type").val()
        }
        return temp;
    }
    return OTableInit;
};