#pragma checksum "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Views\Shared\_ForgotPassword.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f8f71c881359b741706dc8e675e16d5cc408a5da"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__ForgotPassword), @"mvc.1.0.view", @"/Views/Shared/_ForgotPassword.cshtml")]
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
#line 1 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Views\_ViewImports.cshtml"
using Mahya.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Views\_ViewImports.cshtml"
using Mahya.App.Utils;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f8f71c881359b741706dc8e675e16d5cc408a5da", @"/Views/Shared/_ForgotPassword.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9d9d9d3c3000f8b7436e6275a1d035241d8e111c", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared__ForgotPassword : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Mahya.Domain.Models.Account.User>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div style=\"direction: rtl; padding: 20px\">\r\n    <h2>");
#nullable restore
#line 4 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Views\Shared\_ForgotPassword.cshtml"
   Write(Model.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral(" عزیز !</h2>\r\n    <p>\r\n       جهت بازیابی حساب کاربری خود روی لینک زیر کلیک کنید\r\n    </p>\r\n    <p>\r\n        <a");
            BeginWriteAttribute("href", " href=\"", 219, "\"", 295, 2);
            WriteAttributeValue("", 226, "https://localhost:44352/Account/ResetPassword/", 226, 46, true);
#nullable restore
#line 9 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Views\Shared\_ForgotPassword.cshtml"
WriteAttributeValue("", 272, Model.MobileActiveCode, 272, 23, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">بازیابی کلمه عبور</a>\r\n    </p>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Mahya.Domain.Models.Account.User> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
