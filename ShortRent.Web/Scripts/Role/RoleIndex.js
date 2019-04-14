$(function () {
    var OTable =new TableInit();
    OTable.Init();
    //条件查询click事件注册
    $("#btn_query").click(function () {
        OTable.Init();
    });
    //2.初始化select的change事件
    $("#sel_exportoption").change(function () {
        $('#RolesTable').bootstrapTable('refreshOptions', {
            exportDataType: $(this).val()
        });
    });
    //导出数据的title更改
    $(".export").children("button").attr("title","导出数据");
});
//数字
function doOnMsoNumberFormat(cell, row, col) {
    var result = "";
    if (row > 0 && col == 0) {
        result = "\\@";
    }
    return result;
}

//处理导出内容,这个方法可以自定义某一行、某一列、甚至某个单元格的内容,也就是将其值设置为自己想要的内容
function DoOnCellHtmlData(cell, row, col, data) {
    if (row == 0) {
        return data;
    }

    //由于备注列超过6个字的话,通过span标签处理只显示前面6个字,如果直接导出的话会导致内容不完整,因此要将携带完整内容的span标签中title属性的值替换
    if (col == 2) {
        var spanObj = $(data);
        var title = spanObj.html();
        if (typeof (title) != 'undefined') {
            return title;
        }
    }
    return data;
}
function DoAfterSaveToFile(cell, row, col, data) {
    var OTable = new TableInit();
    $("#RolesTable").bootstrapTable('destroy');
    OTable.Init();
    $("#sel_exportoption").val("basic");
}
var TableInit = function () {
    var OTableInit = new Object();
    var $table = $("#RolesTable");
    OTableInit.Init = function () {
        $table.bootstrapTable('destroy').bootstrapTable({
            method: 'get',//用get请求
            url: '/Role/Index',
            toolbar: '#toolbar',
            showExport: true,                     //是否显示导出
            showRefresh: true,
            exportDataType: "basic",              //basic', 'all', 'selected'.
            exportOptions: {
                ignoreColumn: [3],  //忽略某一列的索引  
                fileName: '角色列表',  //文件名称设置  
                worksheetName: 'sheet1',  //表格工作区名称  
                tableName: '用户列表',
                onMsoNumberFormat: doOnMsoNumberFormat,
                onCellHtmlData: DoOnCellHtmlData,
                onAfterSaveToFile:DoAfterSaveToFile,
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
                title: '角色名称',
                field: 'name',
                align: 'center'
            },
            {
                title: '用户类型',
                field: 'type',
                align: 'center',
                formatter: function (value, row, index) {
                    var result = "";
                    if (!value) {
                        result += " <span class=\"label label-success\">前台</span>"
                    } else {
                        result += "<span class=\"label label-warning\">后台</span>"
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
                    result += "<a href='/Role/Edit/" + value + "'>编辑</a><span>&nbsp;|&nbsp;</span>";
                    result += "<a href='/Role/Edit/" + value + "'>分配权限</a>";
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
            pageList: [10,20,50],//可供选择的每页的行数（*）
            sidePagination: "server",   //分页方式：client客户端分页，server服务端分页（*）
            minimumCountColumns: 2,    //最少允许的列数
        });
    }
    OTableInit.Query = function (params) {
        var temp = {
            pageSize: params.pageSize,
            pageNumber: params.pageNumber,
            roleName: $("#txt_search_rolename").val()
        }
        return temp;
    }
    return OTableInit;
};
