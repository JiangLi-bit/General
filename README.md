# General
.Net Core 2.0
Web使用了HttpContextAccessor 认证方式，Api是使用的JWT认证方式

核心类/公共类（Core/COmmon）:一些共用类及通用的操作方法
实体（Entity）:实体代表业务领域的数据和操作，在实践中，通过用来映射成数据库表。
仓储接口（IRepositoryBase）:仓储用来操作数据库进行数据存取。
服务接口（IService、Service）：用来实现对应的业务逻辑。
数据传输对象（DTO）：提供数据的校验或检索
接口（WebApi）：Api
界面（Web）：界面（UI）
