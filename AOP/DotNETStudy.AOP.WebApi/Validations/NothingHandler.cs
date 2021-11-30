namespace DotNETStudy.AOP.WebApi.Validations
{
    /// <summary>
    /// 验证失败，不做任何处理
    /// </summary>
    public class NothingHandler : IValidationHandler
    {
        /// <summary>
        /// 处理验证错误
        /// </summary>
        /// <param name="results">验证结果集合</param>
        public void Handle(ValidationResultCollection results)
        {
            if (results.IsValid)
                return;
            foreach (var item in results)
            {
                Console.WriteLine(item.ErrorMessage);
            }
        }
    }
}