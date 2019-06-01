$(function () {
    var OTable = new TableInit();
    OTable.Init();
    //初始化开始时间的输入框
    InitDateInput("#txt_search_starTime");
    //初始化结束时间的输入框
    InitDateInput("#txt_search_endTime");
    //条件查询click事件注册
    $("#btn_query").click(function () {
        $("#ContactTable").bootstrapTable('destroy');
        OTable.Init();
    });
});
var TableInit = function () {
    var OTableInit = new Object();
    var $table = $("#ContactTable");
    OTableInit.Init = function () {
        $table.bootstrapTable({
            method: 'get',//用get请求
            url: '/Contact/Index',
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
                title: '姓名',
                field: 'name',
                align: 'center'
            },
            {
                title: '邮箱',
                field: 'email',
                align: 'center'
            },
            {
                title: '简介',
                field: 'brief',
                align: 'center'
            },
            {
                title: '创建时间',
                field: 'createTime',
                align: 'center'
            },
            {
                title: '操作',
                field: 'content',
                align: 'center',
                formatter: function (value, row, index) {
                    var result = "";
                    var value = value + "";
                    result += "<a style='cursor:pointer;' onclick=showMessage('"+value+ "')>查看</a>";
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
            startTime: $("#txt_search_starTime").val(),
            endTime: $("#txt_search_endTime").val()
        }
        return temp;
    }
    return OTableInit;
};
function showMessage(data) {
    //示范一个公告层
    layer.open({
        type: 1
        , title: false //不显示标题栏
        , closeBtn: false
        , area: '300px;'
        , shade: 0.8
        , id: 'LAY_layuipro' //设定一个id，防止重复弹出
        , btn: ['确认']
        , btnAlign: 'c'
        , moveType: 1 //拖拽模式，0或者1
        , content: '<div style="padding: 50px; line-height: 22px; background-color: #393D49; color: #fff; font-weight: 300;"><h3 align="center">具体内容</h3></br></br>'+data+'</div>'
    });
}
function InitDateInput(value) {
    laydate.render({
        elem: value
        , format: 'yyyy-MM-dd'
    });
}