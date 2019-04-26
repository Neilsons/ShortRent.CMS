$(function () {
    var OTable = new TableInit();
    OTable.Init();
    //条件查询click事件注册
    $("#btn_query").click(function () {
        $("#PublishTagsTable").bootstrapTable('destroy');
        OTable.Init();
    });
});
var TableInit = function () {
    var OTableInit = new Object();
    var $table = $("#PublishTagsTable");
    OTableInit.Init = function () {
        $table.bootstrapTable({
            method: 'get',//用get请求
            url: '/CompanyPerTags/Index',
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
                title: '标签名称',
                field: 'name',
                align: 'center'
            },
            {
                title: '颜色',
                field: 'color',
                align: 'center',
                formatter: function (value, row, index) {
                    var result = "<span style='width:16px;height:16px;background-color:"+value+";display:inline-block;'></span>";
                    return result;
                }
            },
            {
                title: '序号',
                field: 'tagOrder',
                align: 'center'
            },
            {
                title: '操作',
                field: 'id',
                align: 'center',
                formatter: function (value, row, index) {
                    var result = "";
                    result += "<a href='/CompanyPerTags/Edit/" + value + "'>编辑</a><span>&nbsp;|&nbsp;</span>";
                    result += "<a href='javascript:DeleteById(" + value + ")'>删除</a>";
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
            tagName: $("#txt_search_tagName").val()
        }
        return temp;
    }
    return OTableInit;
};
//删除图标的操作
function DeleteById(id) {
    layer.alert("确认要删除吗？", {
        closeBtn: 1,
        btn: ["取消", "确认"],
        btn1: function (index, lay) {
            layer.close(index);
        },
        btn2: function (index, lay) {
            $.get("/CompanyPerTags/Delete/", { id: id }, function (data) {
                if (data.httpCodeResult == 200) {
                    layer.alert(data.message, {
                        closeBtn: 1    // 是否显示关闭按钮
                        , yes: function (index, lay) {
                            var OTable = new TableInit();
                            $("#PublishTagsTable").bootstrapTable('destroy');
                            OTable.Init();
                            layer.close(index);
                        }
                        , cancel: function () {
                            var OTable = new TableInit();
                            $("#PublishTagsTable").bootstrapTable('destroy');
                            OTable.Init();
                            layer.close(index);
                        }

                    });
                }
            });
        },
        cancel: function (index, lay) {
            layer.close(index);
        }
    });
}