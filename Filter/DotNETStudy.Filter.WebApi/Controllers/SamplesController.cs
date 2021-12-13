using Microsoft.AspNetCore.Mvc;
using DotNETStudy.Filter.WebApi.Attributes;

namespace DotNETStudy.Filter.WebApi.Controllers
{
    /// <summary>
    /// 当管道的某个特定阶段有多个筛选器时，作用域可确定筛选器执行的默认顺序。
    /// 全局筛选器涵盖类筛选器，类筛选器又涵盖方法筛选器。
    /// 在筛选器嵌套模式下，筛选器的 after 代码会按照与 before 代码相反的顺序运行。
    /// 全局筛选器的 before 代码。
    ///     控制器和页面筛选器的 before 代码。
    ///         操作方法筛选器的 before 代码。
    ///         操作方法筛选器的 after 代码。
    ///     控制器和页面筛选器的 after 代码。
    /// 全局筛选器的 after 代码。
    /// 
    /// 重写默认顺序：
    /// 可以通过实现 IOrderedFilter 来重写默认执行序列。 IOrderedFilter 公开了 Order 属性来确定执行顺序，该属性优先于作用域。
    /// 在确定筛选器的运行顺序时，Order 属性重写作用域。 先按顺序对筛选器排序，然后使用作用域消除并列问题。
    /// 所有内置筛选器实现 IOrderedFilter 并将默认 Order 值设为 0。
    /// 对于内置筛选器，作用域会确定顺序，除非将 Order 设为非零值。
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SamplesController : ControllerBase
    {
        [AddHeader("Author", "Liu Liang")]
        [HttpGet("AddHeader")]
        public IActionResult AddHeader()
        {
            return Content("Examine the headers using the F12 developer tools.");
        }

        [AddHeader("Author", "Liu Liang")]
        [ServiceFilter(typeof(MyActionFilterAttribute))]
        [HttpGet("AddHeaderByActionFilter")]
        public IActionResult AddHeaderByActionFilter()
        {
            return Content("Header values by configuration.");
        }

        /// <summary>
        /// `ShortCircuitingResourceFilter`先运行，因为它是资源筛选器且 `AddHeader` 是操作筛选器。
        /// `ShortCircuitingResourceFilter` 对管道的其余部分进行短路处理：
        /// `AddHeader` 筛选器就不会为 `SomeResource` 操作运行。
        /// </summary>
        /// <returns></returns>
        [AddHeader("Author", "Liu Liang")]
        [ShortCircuitingResourceFilter]
        [HttpGet("SomeResource")]
        public IActionResult SomeResource()
        {
            return Content("Successful access to resource - header is set.");
        }
    }
}
