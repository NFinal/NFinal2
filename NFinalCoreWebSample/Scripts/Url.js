function StringFormat() {
    if (arguments.length == 0)
        return null;
    var str = arguments[0];
    for (var i = 1; i < arguments.length; i++) {
        var re = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
        str = str.replace(re, arguments[i]);
    }
    return str;
}
var Url={

    "NFinalCoreWebSample_Controllers_IndexController":{
            "Index":function()
            {
            return "/Index/Index.html";
            }
    }
        ,
    "NFinalCoreWebSample_BaseController":{
    }
};