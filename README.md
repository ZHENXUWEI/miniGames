# miniGames
# 数据库配置说明

## 首次运行设置

1. 将 `App.config.example` 文件复制一份，重命名为 `App.config`
2. 编辑 `App.config` 文件，修改以下配置：
   ```xml
   <add name="MyDatabase" 
        connectionString="Server=你的服务器;Database=你的数据库;Uid=用户名;Pwd=密码;Port=3306;" />
