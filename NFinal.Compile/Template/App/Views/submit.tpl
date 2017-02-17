<!DOCTYPE html>
<html>
<head>
    <script src="/NFinal/Content/js/jquery-1.11.2.min.js"></script>
    <script src="/Url.js"></script>
</head>
<body>
    <form action="<%=Url.${};%>" method="${method.type}">
        <input type="text" name="name"/>
        <input type="text" name="pwd"/>
        <input type="submit" value="submit"/>
    </form>
    <div>
        <input type="text" id="name" />
        <input type="text" id="pwd" />
        <input type="button" value="submit" onclick="${method.name}"/>
    </div>
    <script>
        function ${method.name}()
        {
            ${foreach(parameter in parameters)}
                var ${parameter.name}=$("${parameter.name}").val();
                if(${parameter.name}.length<20)
                {
                    return;
                }
            }
            $.${method.type}(${method.name},{
                ${foreach(parmeter in parameters)}
                    "${parameter.name}":$("${parameter.name}").val();
                }
            },function(data){
                if(data.code==1)
                {
                    alert(data.msg);
                }
            });
        }
    </script>
</body>
</html>