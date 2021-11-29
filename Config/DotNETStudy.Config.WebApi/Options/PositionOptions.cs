namespace DotNETStudy.Config.WebApi.Options
{
    /*
    选项类：
必须是包含公共无参数构造函数的非抽象类。
类型的所有公共读写属性都已绑定。
不会绑定字段。
     */
    public class PositionOptions
    {
        // 不会绑定字段
        public const string Position = "Position";

        public string Title { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
    }
}
