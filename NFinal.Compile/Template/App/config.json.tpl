{
  //本模块所有的URL前缀
  "urlPrefix": "/@{@app}",
  //本模块所有的Cookie前缀
  "cookiePrefix": "@{@app}_",
  //本模块所有的Session前缀
  "sessionPrefix": "@{@app}_",
  //默认样式
  "defaultStyle": "Default",
  //默认后缀
  "urlExtension": ".html",
  //URL模式
  "urlMode": "1",
  //版本号
  "version": "1.0",
  //自动版本号
  "autoVersion": false,
  //是否压缩HTML
  "compressHTML": true,
  //数据库配置
  "connectionStrings": [
    {
      "name": "Common",
      "connectionString": "Data Source=|ModuleDataDirectory|Common.db;Pooling=true;FailIfMissing=false",
      "providerName": "System.Data.SQLite"
    }
  ],
  //静态路由重写配置
  "rewriteDirectory": [
  ],
  "rewriteFile": [
    {"from": "/@{@app}/Index.html","to": "/@{@app}/IndexController/Index.html"}
  ],
  //缓存服务器
  "redis": {
    "autoStart": true,
    "maxReadPoolSize": 60,
    "maxWritePoolSize": 60,
    "readWriteHosts": [ "127.0.0.1:6379" ],
    "readOnlyHosts": [ "127.0.0.1:6379" ]
  }
}