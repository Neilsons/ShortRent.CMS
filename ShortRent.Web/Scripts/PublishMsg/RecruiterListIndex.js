$(function () {
    var OTable = new TableInit();
    OTable.Init();
    //初始化开始时间的输入框
    InitDateInput("#txt_search_starTime");
    //初始化结束时间的输入框
    InitDateInput("#txt_search_endTime");
    //条件查询click事件注册
    $("#btn_query").click(function () {
        $("#RecruiterTable").bootstrapTable('destroy');
        OTable.Init();
    });
});
var TableInit = function () {
    var OTableInit = new Object();
    var $table = $("#RecruiterTable");
    OTableInit.Init = function () {
        $table.bootstrapTable({
            method: 'get',//用get请求
            url: '/PublishMsg/RecruiterIndex',
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
                title: '公司名称',
                field: 'companyName',
                align: 'center'
            },
            {
                title: '姓名',
                field: 'name',
                align: 'center'
            },
            {
                title: '信用积分',
                field: 'creditScore',
                align: 'center'
            },
            {
                title: '邮箱',
                field: 'email',
                align: 'center'
            },
            {
                title: '地址',
                field: 'address',
                align: 'center'
            }
                ,
            {
                title: '联系电话',
                field: 'phone',
                align: 'center'
            },
            {
                title: '币种类型',
                field: 'currency',
                align: 'center'
            },
            {
                title: '开始区间',
                field: 'startSection',
                align: 'center'
            },
            {
                title: '结束区间',
                field: 'endSection',
                align: 'center'
            }
                ,
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
                    var result = "";
                    result += "<a href='/PublishMsg/Watch/" + value + "' style='cursor:pointer;'>查看</a><span>&nbsp;|&nbsp;</span>";
                    result += "<a onclick='LowerScore(" + value + ")' style='cursor:pointer;'>降低发布信息顺序</a>";
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
            pagedSize: params.pageSize,
            pagedIndex: params.pageNumber,
            companyName:$("#txt_search_CompanyName").val(),
            startTime: $("#txt_search_starTime").val(),
            endTime: $("#txt_search_endTime").val(),
            name: $("#txt_search_Name").val(),
            bussiness: $("#txt-search-type").val()
        }
        return temp;
    }
    return OTableInit;
};
function InitDateInput(value) {
    laydate.render({
        elem: value
        , format: 'yyyy-MM-dd'
    });
}
function LowerScore(value) {
    $.get("/PublishMsg/LowerCreditCompany/", { id: value }, function (data) {
        if (data.httpCodeResult) {
            layer.alert(data.message, {
                closeBtn: 1    // 是否显示关闭按钮
                , yes: function () {
                    window.location.href = data.url;
                }
                , cancel: function () {
                    window.location.href = data.url;
                }
            });
        }
        else {
            window.location.href = data.url;
        }
    });
}