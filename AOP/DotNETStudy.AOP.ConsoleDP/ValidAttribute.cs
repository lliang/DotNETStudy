using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AspectCore.DynamicProxy.Parameters;

namespace DotNETStudy.AOP.ConsoleDP
{
    internal class ValidAttribute : ParameterInterceptorAttribute
    {
        public override async Task Invoke(ParameterAspectContext context, ParameterAspectDelegate next)
        {
            Validate(context.Parameter);
            await next(context);
        }

        /// <summary>
        /// 验证
        /// </summary>
        private void Validate(Parameter parameter)
        {
            if (IsGenericCollection(parameter.RawType))
            {
                ValidateCollection(parameter);
                return;
            }
            IValidation validation = parameter.Value as IValidation;
            validation?.Validate();
        }

        /// <summary>
        /// 验证集合
        /// </summary>
        private void ValidateCollection(Parameter parameter)
        {
            if (!(parameter.Value is IEnumerable<IValidation> validations))
                return;
            foreach (var validation in validations)
                validation.Validate();
        }

        /// <summary>
        /// 是否泛型集合
        /// </summary>
        /// <param name="type">类型</param>
        private bool IsGenericCollection(Type type)
        {
            if (!type.IsGenericType)
                return false;
            var typeDefinition = type.GetGenericTypeDefinition();
            return typeDefinition == typeof(IEnumerable<>)
                   || typeDefinition == typeof(IReadOnlyCollection<>)
                   || typeDefinition == typeof(IReadOnlyList<>)
                   || typeDefinition == typeof(ICollection<>)
                   || typeDefinition == typeof(IList<>)
                   || typeDefinition == typeof(List<>);
        }
    }
}
