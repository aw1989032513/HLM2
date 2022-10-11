namespace ET
{
    public static partial class ErrorCode
    {
        public const int ERR_Success = 0;


        // 1-11004 是SocketError请看SocketError定义
        //-----------------------------------
        // 100000-109999是Core层的错误

        // 110000以下的错误请看ErrorCore.cs

        // 这里配置逻辑层的错误码
        // 110000 - 200000是抛异常的错误
        // 200001以上不抛异常
        public const int ERR_NetWorkError        = 200002;
        public const int ERR_LoginInfoError      = 200003;// 登录信息错误
        public const int ERR_AccountNameFromError= 200004;//登录账号格式错误
        public const int ERR_PasswordFromError   = 200005;//登录密码格式错误
        public const int ERR_LoginBlackListError = 200006;//黑户
        public const int ERR_PassWordError       = 200007;//密码错误
        public const int ERR_RequestRepeatedly   = 200008;//重复请求
        public const int ERR_TokenError          = 200009;//Token错误

        public const int ERR_RoleNameIsNull      = 200010;//创建Role的名字是空的
        public const int ERR_RoleNameSame        = 200011;//该区已经有相同名字
        public const int ERR_RoleNotExist        = 200012;//该区查无此人

        public const int ERR_ConnectGateKeyError = 200013; //连接Gate的令牌错误

        public const int ERR_RequestSceneTypeError = 200014; //请求的Scene错误
        public const int ERR_OtherAccountLogin     = 200015; //其他账号在线
        public const int ERR_SessionPlayerError    = 200016;
        public const int ERR_NonePlayerError       = 200017;
        public const int ERR_SessionStateError     = 200018;
        public const int ERR_EnterGameError        = 200019;
        public const int ERR_NumericTypeNotExist = 200020;
        public const int ERR_NumericTypeNotAddPoint = 200021;
        public const int ERR_AddPointNotEnough = 200022;





    }
}