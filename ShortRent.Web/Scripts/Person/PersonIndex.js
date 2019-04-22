$(function () {
    var OTable = new TableInit();
    OTable.Init();
    //条件查询click事件注册
    $("#btn_query").click(function () {
        $("#AdminTable").bootstrapTable('destroy');
        OTable.Init();
        $("#sel_exportoption").val("basic");
    });
    //2.初始化select的change事件
    $("#sel_exportoption").change(function () {
        $('#AdminTable').bootstrapTable('refreshOptions', {
            exportDataType: $(this).val()
        });
    });
    //导出数据的title更改
    $(".export").children("button").attr("title", "导出数据");
});
function DoAfterSaveToFile(cell, row, col, data) {
    var OTable = new TableInit();
    $("#AdminTable").bootstrapTable('destroy');
    OTable.Init();
    $("#sel_exportoption").val("basic");
}
var TableInit = function () {
    var OTableInit = new Object();
    var $table = $("#AdminTable");
    OTableInit.Init = function () {
        $table.bootstrapTable({
            method: 'get',//用get请求
            url: '/Person/Index',
            toolbar: '#toolbar',
            showExport: true,                     //是否显示导出
            showRefresh: true,
            exportDataType: "basic",              //basic', 'all', 'selected'.
            exportOptions: {
                ignoreColumn: [2,8],  //忽略某一列的索引  
                fileName: '后台用户列表',  //文件名称设置  
                worksheetName: 'sheet1',  //表格工作区名称  
                tableName: '用户列表',
            },
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
                title: '用户名称',
                field: 'name',
                align: 'center'
            },
            {
                title: '头像',
                field: 'perImage',
                align: 'center',
                formatter: function (value, row, index) {
                    var result = "<img style=\"display:block;width:20px;height:20px;\" src=\"../Content/Images/AdminImg/"+value+"\">";
                    return result;
                }
            },
            {
                title: '职位',
                field: 'position',
                align: 'center'
            }
            ,
            {
                title: '详细描述',
                field: 'personDetail',
                align: 'center'
            },
            {
                title: 'QQ',
                field: 'qq',
                align: 'center'
            },
            {
                title: '微信',
                field: 'weChat',
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
                   var result = "<a href=" + value + ">分配角色</a>";
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
            AdminName: $("#txt_search_AdminName").val()
        }
        return temp;
    }
    return OTableInit;
};
