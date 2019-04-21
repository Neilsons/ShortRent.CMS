$(function () {
    var OTable = new TableInit();
    OTable.Init();
});

var TableInit = function () {
    var OTableInit = new Object();
    var $table = $("#ManagerTable");
    OTableInit.Init = function () {
        $table.bootstrapTable({
            url: '/Manager/Index',
            striped: true,
            sidePagination: 'server',
            idField: 'id',
            columns: [
                {
                    field: 'iconInfo',
                    title: '分类图标',
                    formatter: "IconShow",
                    align: 'center'
                },
                {
                    field: 'name',
                    title: '菜单名称',
                    align: 'center',
                },
                {
                    field: 'controllerName',
                    title: '控制器',
                    align: 'center',
                    formatter:"Controller"
                },
                {
                    field: 'actionName',
                    title: '方法',
                    align: 'center',
                    formatter: "Action"
                },
                {
                    field: 'activity',
                    title: '状态',
                    align: 'center',
                    formatter:"IsActivity"
                },
                {
                    field: 'createTime',
                    title: '创建时间',
                    align: 'center',
                },
                {
                    field: 'id',
                    title: '操作',
                    formatter: 'Editor'

                }
            ],
            treeShowField: 'name',
            parentIdField: 'pid',
            onLoadSuccess: function (data) {
                $table.treegrid({
                    initialState: 'collapsed',
                    treeColumn: 1,
                    expanderExpandedClass: 'glyphicon glyphicon-minus',
                    expanderCollapsedClass: 'glyphicon glyphicon-plus',
                    onChange: function () {
                        $table.bootstrapTable('resetWidth');
                    }
                });
            }
        });
    }
    return OTableInit;  
};
function IconShow(value,row,index) {
    var list = value.split('|');
    return "<i class='" + list[0] + "' style=\"color:" + list[1] + "\"></i>";
}
function IsActivity(value, row, index) {
    var result = "";
    if (value) {
        result += " <span class=\"label label-success\">已启用</span>"
    } else {
        result += "<span class=\"label label-danger\">已禁用</span>"
    }
    return result;
}
function Editor(value, row, index) {
    var result = "";
    result += "<a href='/Manager/Edit/" + value + "'>编辑</a><span>&nbsp;|&nbsp;</span>";
    return result;
}
function Controller(value,row,index) {
    if (value == "") {
       return "--"
    }
    return value;
}
function Action(value, row, index) {
    if (value == "") {
        return "--"
    }
    return value;
}