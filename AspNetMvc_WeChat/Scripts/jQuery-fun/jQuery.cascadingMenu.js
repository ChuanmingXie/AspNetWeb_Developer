/* @by chuanmingxie */
/* 在所有的添加页中 引入 创建级联菜单 */

; (function ($, window, document, undefined) {
    var defaults = [{
        "text": "IT科技",
        "value": "1",
        "subType": [
            { "text": "互联网/电子商务", "value": "1" },
            { "text": "IT软件与服务", "value": "2" },
            { "text": "IT硬件与设备", "value": "3" },
            { "text": "电子技术", "value": "4" },
            { "text": "通信与运营商", "value": "5" },
            { "text": "网络游戏", "value": "6" }]
    }, {
        "text": "金融业",
        "value": "7",
        "subType": [
            { "text": "银行", "value": "7" },
            { "text": "基金理财信托", "value": "8" },
            { "text": "保险", "value": "9" }]
    }, {
        "text": "餐饮",
        "value": "10",
        "subType": [
            { "text": "	餐饮", "value": "10" }]
    }, {
        "text": "酒店旅游",
        "value": "11",
        "subType": [
            { "text": "	酒店", "value": "11" },
            { "text": "	旅游", "value": "12" }]
    }, {
        "text": "运输与仓储",
        "value": "13",
        "subType": [
            { "text": "	快递", "value": "13" },
            { "text": "	物流", "value": "14" },
            { "text": "	仓储", "value": "15" }]
    }, {
        "text": "教育",
        "value": "16",
        "subType": [
            { "text": "	培训", "value": "16" },
            { "text": "	院校", "value": "17" }]
    }, {
        "text": "政府与公共事业",
        "value": "18",
        "subType": [
            { "text": "	学术科研", "value": "18" },
            { "text": "	交警", "value": "19" },
            { "text": "	博物馆", "value": "20" },
            { "text": "	公共事业非盈利机构", "value": "21" }]
    }, {
        "text": "医药护理",
        "value": "16",
        "subType": [
            { "text": "	医药医疗", "value": "22" },
            { "text": "	护理美容", "value": "23" },
            { "text": "	保健与卫生", "value": "24" }]
    }, {
        "text": "交通工具",
        "value": "25",
        "subType": [
            { "text": "	汽车", "value": "25" },
            { "text": "	摩托车", "value": "26" },
            { "text": "	火车", "value": "27" },
            { "text": "	飞机", "value": "28" }]
    }, {
        "text": "房地产",
        "value": "29",
        "subType": [
            { "text": "	建筑", "value": "29" },
            { "text": "	物业", "value": "30" }]
    }, {
        "text": "消费品",
        "value": "31",
        "subType": [
            { "text": "	消费品", "value": "31" }]
    }, {
        "text": "商业服务",
        "value": "32",
        "subType": [
            { "text": "	法律", "value": "32" },
            { "text": "	会展", "value": "33" },
            { "text": "	中介服务", "value": "34" },
            { "text": "	认证", "value": "35" },
            { "text": "	审计", "value": "36" }]
    }, {
        "text": "文体娱乐",
        "value": "37",
        "subType": [
            { "text": "	传媒", "value": "37" },
            { "text": "	体育", "value": "38" },
            { "text": "	娱乐休闲", "value": "39" }]
    }, {
        "text": "印刷",
        "value": "40",
        "subType": [
            { "text": "	印刷", "value": "40" }]
    }, {
        "text": "其他",
        "value": "41",
        "subType": [
            { "text": "	其他", "value": "41" }]
    },
    ];

    function CascadingMenu(element, options) {
        this.$element = element,
            this.settings = $.extend(defaults, options);
        this.firstMenu = null;
        this.secondMenu = null;
    }
    CascadingMenu.prototype = {
        initMenu: function () {
            this.initFirstMenu();
            this.bindSelectChangeEvent();
            return $(this.$element).append(this.firstMenu).append(this.secondMenu);
        },
        initFirstMenu: function () {
            // 创建级联菜单第一项
            this.firstMenu = $("<select name='industry_id1' class='form-control col-md-5'></select>");
            this.firstMenu.append($("<option value='请选择'>--请选择--</option>"))
            for (var i = 0; i < this.settings.length; i++) {
                var option = $("<option></option>");
                option.append(this.settings[i].text);
                option.val(this.settings[i].value);
                this.firstMenu.append(option);
            }
            return this.firstMenu;
        },
        bindSelectChangeEvent: function () {
            // 保存this对象
            var that = this;
            that.secondMenu = $("<select name='industry_id2' class='form-control col-md-5'></select>");
            that.secondMenu.append($("<option value='请选择'>--请选择--</option>"));
            that.firstMenu.on("change", function () {
                that.secondMenu.empty();
                that.secondMenu.append($("<option value='请选择'>--请选择--</option>"));
                var index = this.selectedIndex - 1;
                var subType = that.settings[index].subType;
                for (var i = 0; i < subType.length; i++) {
                    var option = $("<option></option>");
                    option.append(subType[i].text);
                    option.val(subType[i].value);
                    that.secondMenu.append(option);
                }
            });
            return that.secondMenu;
        }
    };

    /* 在自定义插件cascadingMenuPlugin 中创建cascadingMenu对象 */
    $.fn.cascadingMenuPlugins = function (opts) {
        console.log(opts);
        var cascadingMenu = new CascadingMenu(this, opts);
        return cascadingMenu.initMenu();
    }
})(jQuery, window, document);