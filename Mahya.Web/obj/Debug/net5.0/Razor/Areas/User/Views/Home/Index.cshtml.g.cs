#pragma checksum "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\User\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fd2ece77e2db9928e287b0707c2ff463e0a81c05"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_User_Views_Home_Index), @"mvc.1.0.view", @"/Areas/User/Views/Home/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\User\Views\_ViewImports.cshtml"
using Mahya.Web;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fd2ece77e2db9928e287b0707c2ff463e0a81c05", @"/Areas/User/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"32e05ff667d78a8bfcfd1f43cef9e523c441c5e4", @"/Areas/User/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Areas_User_Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\User\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "پنل کاربری";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container text-right mt-5 mb-5\">\r\n    <div class=\"user-account\">\r\n        <div class=\"row\">\r\n            <div class=\"col-md-3 col-sm-4 col-xs-12\">\r\n                <aside>\r\n                    ");
#nullable restore
#line 11 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\User\Views\Home\Index.cshtml"
               Write(await Component.InvokeAsync("UserSideBar"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                  \r\n                </aside>\r\n            </div>\r\n            <div class=\"col-md-9 col-sm-8 col-xs-12 \">\r\n               ");
#nullable restore
#line 16 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\User\Views\Home\Index.cshtml"
          Write(await Component.InvokeAsync("UserInformation"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
