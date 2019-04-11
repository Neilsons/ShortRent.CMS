$(function () {
    $("#RolesTable").bootstrapTable({
        method: 'get',//用get请求
        url: '/Role/Index',
        columns: [{
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
                if (value) {
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
        pageSize: 10, //每页的记录行数（*）
        queryParamsType: '', //默认值为 'limit' ,在默认情况下 传给服务端的参数为：offset,limit,sort
        // 设置为 ''  在这种情况下传给服务器的参数为：pageSize,pageNumber
        pageList: [5, 10, 15, 20],//可供选择的每页的行数（*）
        sidePagination: "server",   //分页方式：client客户端分页，server服务端分页（*）
        minimumCountColumns: 2,    //最少允许的列数
    });
});