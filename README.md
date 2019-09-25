# qualitybook-aspdonetcore

关于部署的注意
. 注释掉　Startup.cs->CreateUserRoles(services).Wait();
. 上传代码到云端
. 远程连接云端数据库，添加Ａｄｄｒｅｓｓ字段到ａｓｐｎｅｔｕｓｅｒ
．执行程序，注册admin用户
. 打开Startup.cs->CreateUserRoles(services).Wait();
。重新上传代码到云端
。之后便可正常使用
