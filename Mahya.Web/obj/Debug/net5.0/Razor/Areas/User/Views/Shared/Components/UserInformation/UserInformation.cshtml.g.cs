#pragma checksum "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\User\Views\Shared\Components\UserInformation\UserInformation.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a5f2b3a0418989096ae1e9120416d0e52743b88f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_User_Views_Shared_Components_UserInformation_UserInformation), @"mvc.1.0.view", @"/Areas/User/Views/Shared/Components/UserInformation/UserInformation.cshtml")]
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
#nullable restore
#line 1 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\User\Views\Shared\Components\UserInformation\UserInformation.cshtml"
using Mahya.App.Extenstion;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\User\Views\Shared\Components\UserInformation\UserInformation.cshtml"
using Mahya.App.Utils;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a5f2b3a0418989096ae1e9120416d0e52743b88f", @"/Areas/User/Views/Shared/Components/UserInformation/UserInformation.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"32e05ff667d78a8bfcfd1f43cef9e523c441c5e4", @"/Areas/User/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Areas_User_Views_Shared_Components_UserInformation_UserInformation : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Mahya.Domain.Models.Account.User>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"

<section class=""user-account-content box-shadow"">
    <header>
        <h1> داشبورد </h1>
    </header>
    <div class=""inner"">
        <div class=""account-information"">
            <h3> اطلاعات کاربری </h3>
            <ul>
                <li> <i class=""zmdi zmdi-account""></i> نام و نام خانوادگی : ");
#nullable restore
#line 14 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\User\Views\Shared\Components\UserInformation\UserInformation.cshtml"
                                                                       Write(Model.GetUserName());

#line default
#line hidden
#nullable disable
            WriteLiteral(" </li>\r\n                <li> <i class=\"zmdi zmdi-calendar-check\"></i> تاریخ عضویت : ");
#nullable restore
#line 15 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\User\Views\Shared\Components\UserInformation\UserInformation.cshtml"
                                                                       Write(Model.CreateDate.ToStringShamsiDate());

#line default
#line hidden
#nullable disable
            WriteLiteral(" </li>\r\n                <li> <i class=\"zmdi zmdi-smartphone-android\"></i> شماره تماس : ");
#nullable restore
#line 16 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\User\Views\Shared\Components\UserInformation\UserInformation.cshtml"
                                                                          Write(Model.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </li>\r\n                <li> <i class=\"zmdi zmdi-smartphone-android\"></i> آدرس : ");
#nullable restore
#line 17 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\User\Views\Shared\Components\UserInformation\UserInformation.cshtml"
                                                                    Write(Model.Address);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </li>\r\n                <li> <i class=\"zmdi zmdi-smartphone-android\"></i> کدپستی : ");
#nullable restore
#line 18 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\User\Views\Shared\Components\UserInformation\UserInformation.cshtml"
                                                                      Write(Model.PostAddress);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </li>\r\n            </ul>\r\n        </div>\r\n    </div>\r\n</section>");
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