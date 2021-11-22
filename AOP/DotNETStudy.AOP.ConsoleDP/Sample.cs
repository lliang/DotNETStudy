using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotNETStudy.AOP.ConsoleDP
{
    public class Sample : ISample
    {
        /// <summary>
        /// 测试值
        /// </summary>
        [Display(Name = "测试值")]
        [Required(ErrorMessage = "测试值不能为空")]
        [StringLength(20, ErrorMessage = "测试值长度不能超过20")]
        [EmailAddress]
        [Url]
        public string TestValue { get; set; }

        /// <summary>
        /// 电子邮件验证
        /// </summary>
        [EmailAddress(ErrorMessage = "电子邮件不正确")]
        public string Email { get; set; }

        /// <summary>
        /// 网址验证
        /// </summary>
        [Url(ErrorMessage = "网址不正确")]
        public string Url { get; set; }

        /// <summary>
        /// 最大长度
        /// </summary>
        [MaxLength(2, ErrorMessage = "最大长度是2")]
        public string MaxLengthValue { get; set; }

        /// <summary>
        /// 长度验证
        /// </summary>
        [StringLength(3, MinimumLength = 2, ErrorMessage = "最大长度是3,最小长度是2")]
        public string StringLengthValue { get; set; }

        /// <summary>
        /// DisplayName
        /// </summary>
        [DisplayName("DisplayNameValue值")]
        public string DisplayNameValue { get; set; }

        /// <summary>
        /// Display
        /// </summary>
        [Display(Name = "DisplayValue值")]
        public string DisplayValue { get; set; }

        /// <summary>
        /// string值
        /// </summary>
        [Display(Name = "字符串值")]
        [StringLength(20, ErrorMessage = "长度不能超过20")]
        [Required(ErrorMessage = "不能为空")]
        public string StringValue { get; set; }

        /// <summary>
        /// 验证规则集合
        /// </summary>
        private readonly List<IValidationRule> _rules = new List<IValidationRule>();

        /// <summary>
        /// 验证处理器
        /// </summary>
        private IValidationHandler _handler;

        /// <summary>
        /// 设置验证处理器
        /// </summary>
        /// <param name="handler">验证处理器</param>
        public void SetValidationHandler(IValidationHandler handler)
        {
            if (handler == null)
                return;
            _handler = handler;
        }

        public ValidationResultCollection Validate()
        {
            var result = GetValidationResults();
            HandleValidationResults(result);
            return result;
        }

        /// <summary>
        /// 获取验证结果
        /// </summary>
        private ValidationResultCollection GetValidationResults()
        {
            var result = DataAnnotationValidation.Validate(this);
            Validate(result);
            foreach (var rule in _rules)
                result.Add(rule.Validate());
            return result;
        }

        /// <summary>
        /// 验证并添加到验证结果集合
        /// </summary>
        /// <param name="results">验证结果集合</param>
        protected virtual void Validate(ValidationResultCollection results)
        {
        }

        /// <summary>
        /// 处理验证结果
        /// </summary>
        private void HandleValidationResults(ValidationResultCollection results)
        {
            if (results.IsValid)
                return;
            _handler.Handle(results);
        }
    }
}
