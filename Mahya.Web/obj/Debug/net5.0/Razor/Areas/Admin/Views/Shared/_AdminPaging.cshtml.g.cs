#pragma checksum "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\Admin\Views\Shared\_AdminPaging.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0bc37bc4038dfca1f7a358b8d551296af2b185d1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Shared__AdminPaging), @"mvc.1.0.view", @"/Areas/Admin/Views/Shared/_AdminPaging.cshtml")]
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
#line 1 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\Admin\Views\_ViewImports.cshtml"
using Mahya.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\Admin\Views\_ViewImports.cshtml"
using Mahya.Domain.Models.Account;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\Admin\Views\_ViewImports.cshtml"
using Mahya.App.Extenstion;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\Admin\Views\_ViewImports.cshtml"
using Mahya.App.Utils;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0bc37bc4038dfca1f7a358b8d551296af2b185d1", @"/Areas/Admin/Views/Shared/_AdminPaging.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"297b2bf5eb3adb5e60455653bc039a8b109897c9", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Areas_Admin_Views_Shared__AdminPaging : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Mahya.Domain.ViewModels.Paging.BasePaging>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n<nav aria-label=\"Page navigation example\">\r\n    <ul class=\"pagination\">\r\n");
#nullable restore
#line 6 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\Admin\Views\Shared\_AdminPaging.cshtml"
         if (Model.StartPage < Model.PageId)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li class=\"page-item\">\r\n                <a class=\"page-link\"");
            BeginWriteAttribute("onclick", " onclick=\"", 256, "\"", 296, 3);
            WriteAttributeValue("", 266, "FillPageId(", 266, 11, true);
#nullable restore
#line 9 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\Admin\Views\Shared\_AdminPaging.cshtml"
WriteAttributeValue("", 277, Model.PageId -1, 277, 18, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 295, ")", 295, 1, true);
            EndWriteAttribute();
            WriteLiteral(" aria-label=\"Previous\">\r\n                    <span aria-hidden=\"true\">&laquo;</span>\r\n                    <span class=\"sr-only\">Previous</span>\r\n                </a>\r\n            </li>\r\n");
#nullable restore
#line 14 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\Admin\Views\Shared\_AdminPaging.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 16 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\Admin\Views\Shared\_AdminPaging.cshtml"
         for (int i = Model.StartPage; i <= Model.EndPage; i++)
        {


#line default
#line hidden
#nullable disable
            WriteLiteral("            <li");
            BeginWriteAttribute("class", " class=\"", 589, "\"", 638, 2);
            WriteAttributeValue("", 597, "page-item", 597, 9, true);
#nullable restore
#line 19 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\Admin\Views\Shared\_AdminPaging.cshtml"
WriteAttributeValue(" ", 606, Model.PageId ==i?"active":"", 607, 31, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("><a class=\"page-link\"");
            BeginWriteAttribute("onclick", " onclick=\"", 660, "\"", 684, 3);
            WriteAttributeValue("", 670, "FillPageId(", 670, 11, true);
#nullable restore
#line 19 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\Admin\Views\Shared\_AdminPaging.cshtml"
WriteAttributeValue("", 681, i, 681, 2, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 683, ")", 683, 1, true);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 19 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\Admin\Views\Shared\_AdminPaging.cshtml"
                                                                                                           Write(i);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></li>\r\n");
#nullable restore
#line 20 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\Admin\Views\Shared\_AdminPaging.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 22 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\Admin\Views\Shared\_AdminPaging.cshtml"
         if (Model.EndPage > Model.PageId)
        {


#line default
#line hidden
#nullable disable
            WriteLiteral("            <li class=\"page-item\">\r\n                <a class=\"page-link\"");
            BeginWriteAttribute("onclick", " onclick=\"", 841, "\"", 881, 3);
            WriteAttributeValue("", 851, "FillPageId(", 851, 11, true);
#nullable restore
#line 26 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\Admin\Views\Shared\_AdminPaging.cshtml"
WriteAttributeValue("", 862, Model.PageId +1, 862, 18, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 880, ")", 880, 1, true);
            EndWriteAttribute();
            WriteLiteral(" aria-label=\"Previous\">\r\n                    <span aria-hidden=\"true\">&raquo;</span>\r\n                    <span class=\"sr-only\">Next</span>\r\n                </a>\r\n            </li>\r\n");
#nullable restore
#line 31 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\Admin\Views\Shared\_AdminPaging.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </ul>\r\n</nav>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Mahya.Domain.ViewModels.Paging.BasePaging> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591