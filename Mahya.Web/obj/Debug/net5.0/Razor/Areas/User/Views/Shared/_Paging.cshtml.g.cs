#pragma checksum "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\User\Views\Shared\_Paging.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "09d2939b41167d578d64a1dc2e845b3326f044b5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_User_Views_Shared__Paging), @"mvc.1.0.view", @"/Areas/User/Views/Shared/_Paging.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"09d2939b41167d578d64a1dc2e845b3326f044b5", @"/Areas/User/Views/Shared/_Paging.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"32e05ff667d78a8bfcfd1f43cef9e523c441c5e4", @"/Areas/User/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Areas_User_Views_Shared__Paging : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n\r\n<div class=\"pagination-area\">\r\n    <nav aria-label=\"Page navigation\">\r\n        <ul class=\"pagination justify-content-center d-flex \">\r\n\r\n");
#nullable restore
#line 9 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\User\Views\Shared\_Paging.cshtml"
             if (Model.StartPage < Model.PageId)
            {


#line default
#line hidden
#nullable disable
            WriteLiteral("                <li class=\"page-item\">\r\n                    <a class=\"page-link\"");
            BeginWriteAttribute("onclick", " onclick=\"", 343, "\"", 383, 3);
            WriteAttributeValue("", 353, "FillPageId(", 353, 11, true);
#nullable restore
#line 13 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\User\Views\Shared\_Paging.cshtml"
WriteAttributeValue("", 364, Model.PageId -1, 364, 18, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 382, ")", 382, 1, true);
            EndWriteAttribute();
            WriteLiteral(" aria-label=\"Next\">\r\n                        <i class=\"icon-arrow-right\"></i>\r\n                    </a>\r\n                </li>\r\n");
#nullable restore
#line 17 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\User\Views\Shared\_Paging.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 19 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\User\Views\Shared\_Paging.cshtml"
             for (int i = Model.StartPage; i <= Model.EndPage; i++)
            {


#line default
#line hidden
#nullable disable
            WriteLiteral("                <li");
            BeginWriteAttribute("class", " class=\"", 634, "\"", 683, 2);
            WriteAttributeValue("", 642, "page-item", 642, 9, true);
#nullable restore
#line 22 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\User\Views\Shared\_Paging.cshtml"
WriteAttributeValue(" ", 651, Model.PageId ==i?"active":"", 652, 31, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("><a class=\"page-link\"");
            BeginWriteAttribute("onclick", " onclick=\"", 705, "\"", 729, 3);
            WriteAttributeValue("", 715, "FillPageId(", 715, 11, true);
#nullable restore
#line 22 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\User\Views\Shared\_Paging.cshtml"
WriteAttributeValue("", 726, i, 726, 2, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 728, ")", 728, 1, true);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 22 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\User\Views\Shared\_Paging.cshtml"
                                                                                                               Write(i);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></li>\r\n");
#nullable restore
#line 23 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\User\Views\Shared\_Paging.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n");
#nullable restore
#line 27 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\User\Views\Shared\_Paging.cshtml"
             if (Model.EndPage > Model.PageId)
            {


#line default
#line hidden
#nullable disable
            WriteLiteral("                <li class=\"page-item\">\r\n                    <a class=\"page-link\"");
            BeginWriteAttribute("onclick", " onclick=\"", 910, "\"", 950, 3);
            WriteAttributeValue("", 920, "FillPageId(", 920, 11, true);
#nullable restore
#line 31 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\User\Views\Shared\_Paging.cshtml"
WriteAttributeValue("", 931, Model.PageId +1, 931, 18, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 949, ")", 949, 1, true);
            EndWriteAttribute();
            WriteLiteral(" aria-label=\"Previous\">\r\n                        <i class=\"icon-arrow-left\"></i>\r\n                    </a>\r\n                </li>\r\n");
#nullable restore
#line 35 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\User\Views\Shared\_Paging.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </ul>\r\n    </nav>\r\n</div>\r\n");
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
