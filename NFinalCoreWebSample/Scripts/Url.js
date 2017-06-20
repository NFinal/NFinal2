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

    "NFinalCorePlug_Controllers_IndexController":{
            "UpdateA":function()
            {
            return "/Index/UpdateA.html";
            }
            ,
            "Default":function()
            {
            return "/Index/Default.html";
            }
            ,
            "Ajax":function()
            {
            return "/Index/Ajax.html";
            }
            ,
            "SetSession":function()
            {
            return "/Index/SetSession.html";
            }
            ,
            "WriteSession":function()
            {
            return "/Index/WriteSession.html";
            }
    }
        ,
    "NFinalCorePlug_BaseController":{
            "UpdateA":function()
            {
            return "/Base/UpdateA.html";
            }
    }
        ,
    "NFinalCorePlug_Controllers_UserController":{
            "Insert":function()
            {
            return "/User/Insert.html";
            }
    }
};