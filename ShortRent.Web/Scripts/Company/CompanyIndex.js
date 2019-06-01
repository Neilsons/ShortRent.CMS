$(function () {
    var OTable = new TableInit();
    OTable.Init();
    //条件查询click事件注册
    $("#btn_query").click(function () {
        $("#CompanyTable").bootstrapTable('destroy');
        OTable.Init();
    });
});
var TableInit = function () {
    var OTableInit = new Object();
    var $table = $("#CompanyTable");
    OTableInit.Init = function () {
        $table.bootstrapTable({
            method: 'get',//用get请求
            url: '/Company/Index',
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
                field: 'name',
                align: 'center'
            },
            {
                title: '信用积分',
                field: 'score',
                align: 'center'
            },
            {
                title: '公司LOGO',
                field: 'companyImg',
                align: 'center',
                formatter: function (value, row, index) {
                    var result = "<img style=\"display:block;width:20px;height:20px;\" src=\"../Content/Images/WebImage/Company/" + value + "\">";
                    return result;
                }
            },
            {
                title: '员工数量',
                field: 'employeesCount',
                align: 'center'
            } ,
            {
                title: '公司成立时间',
                field: 'establishTime',
                align: 'center'
            },
            {
                title: '创建时间',
                field: 'createTime',
                align: 'center'
            }
            ,
            {
                title: '创建公司人',
                field: 'userTypeName',
                align: 'center'
            }
            ,
            {
                title: '状态',
                field: 'companyStatus',
                align: 'center',
                formatter: function (value, row, index) {
                    var result = "";
                    if (value == "0") {
                        result += " <span class=\"label label-warning\">未审核</span>"
                    } else if (value == "1") {
                        result += "<span class=\"label label-success\">审核通过</span>"
                    }
                    else {
                        result += "<span class=\"label label-danger\">审核未通过</span>"
                    }
                    return result;
                }
            },
            {
                title: '操作',
                field: 'id',
                align: 'center',
                formatter: function (value, row, index) {
                    var result = "";
                    result += "<a href='/Company/Audit/" + value + "'>审核</a>";
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
            Name: $("#txt_search_Name").val()
        }
        return temp;
    }
    return OTableInit;
};