$(function () {
    var OTable = new TableInit();
    OTable.Init();
    InitDateInput("#txt_search_stratTime");
    InitDateInput("#txt_search_endTime");
    //条件查询click事件注册
    $("#btn_query").click(function () {
        $("#LogTable").bootstrapTable('destroy');
        OTable.Init();
    });
    //2.初始化select的change事件
    $("#sel_exportoption").change(function () {
        $('#LogTable').bootstrapTable('refreshOptions', {
            exportDataType: $(this).val()
        });
    });
    //导出数据的title更改
    $(".export").children("button").attr("title", "导出数据");

});
function InitDateInput(value) {
    laydate.render({
        elem: value
        , format: 'yyyy-MM-dd'
    });
}
//数字
function doOnMsoNumberFormat(cell, row, col) {
    var result = "";
    if (row > 0 && col == 0) {
        result = "\\@";
    }
    return result;
}
function DoAfterSaveToFile(cell, row, col, data) {
    var OTable = new TableInit();
    $("#LogTable").bootstrapTable('destroy');
    OTable.Init();
    $("#sel_exportoption").val("basic");
}
var TableInit = function () {
    var OTableInit = new Object();
    var $table = $("#LogTable");
    OTableInit.Init = function () {
        $table.bootstrapTable({
            method: 'get',//用get请求
            url: '/LogInfo/Index',
            toolbar: '#toolbar',
            showExport: true,                     //是否显示导出
            showRefresh: true,
            exportDataType: "basic",              //basic', 'all', 'selected'.
            exportOptions: {
                ignoreColumn: [9],  //忽略某一列的索引  
                fileName: '日志列表',  //文件名称设置  
                worksheetName: 'sheet1',  //表格工作区名称  
                tableName: '日志列表',
                onAfterSaveToFile: DoAfterSaveToFile,
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
                title: '错误消息',
                field: 'message',
                align: 'center'
            },
            {
                title: '进程ID',
                field: 'processId',
                align: 'center'
            },
            {
                title: '进程名称',
                field: 'processName',
                align: 'center'
            },
            {
                title: '线程ID',
                field: 'threadId',
                align: 'center'
              
            },
            {
                title: '机器名称',
                field: 'machineName',
                align: 'center'
            },
            {
                title: '分类目录',
                field: 'catalogue',
                align: 'center'
            },      
            {
                title: '方法',
                field: 'methodFullName',
                align: 'center'
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
                    var result = "";
                    result += "<a href='/LogInfo/Detail/" + value + "'>查看详情</a><span>&nbsp;|&nbsp;</span>";
                    result += "<a href='/LogInfo/Delete/" + value + "'>删除</a>";
                    return result;
                }
            }
            ],
            striped: true,//是否显示行间隔色
            cache: false,//是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true,//是否显示分页（*）
            pageNumber: 1,//初始化加载第一页，默认第一页
            pageSize: 5, //每页的记录行数（*）
            queryParamsType: 'undefined',
            queryParams: OTableInit.Query,
            // 设置为 ''  在这种情况下传给服务器的参数为：pageSize,pageNumber
            pageList: [5,10, 20, 50],//可供选择的每页的行数（*）
            sidePagination: "server",   //分页方式：client客户端分页，server服务端分页（*）
            minimumCountColumns: 2,    //最少允许的列数
        });
    }
    OTableInit.Query = function (params) {
        var temp = {
            pageSize: params.pageSize,
            pageNumber: params.pageNumber
        }
        return temp;
    }
    return OTableInit;
};
