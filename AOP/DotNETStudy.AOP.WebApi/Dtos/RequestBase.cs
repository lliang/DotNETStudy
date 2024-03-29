﻿using DotNETStudy.AOP.WebApi.Validations;

namespace DotNETStudy.AOP.WebApi.Dtos
{
    /// <summary>
    /// 请求参数
    /// </summary>
    public abstract class RequestBase : IRequest
    {
        /// <summary>
        /// 验证
        /// </summary>
        public virtual ValidationResultCollection Validate()
        {
            var result = DataAnnotationValidation.Validate(this);
            if (result.IsValid)
                return ValidationResultCollection.Success;
            throw new Exception(result.First().ErrorMessage);
        }
    }
}