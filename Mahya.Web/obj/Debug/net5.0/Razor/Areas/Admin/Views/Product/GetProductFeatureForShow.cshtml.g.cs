#pragma checksum "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\Admin\Views\Product\GetProductFeatureForShow.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a268f346587537c291252cd432e9099f102c362d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Product_GetProductFeatureForShow), @"mvc.1.0.view", @"/Areas/Admin/Views/Product/GetProductFeatureForShow.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a268f346587537c291252cd432e9099f102c362d", @"/Areas/Admin/Views/Product/GetProductFeatureForShow.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"297b2bf5eb3adb5e60455653bc039a8b109897c9", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Areas_Admin_Views_Product_GetProductFeatureForShow : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Mahya.Domain.Models.ProductEntity.ProductFeature>>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "Admin", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Product", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DeleteFeature", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-app"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\Admin\Views\Product\GetProductFeatureForShow.cshtml"
  
    ViewData["Title"] = "نمایش همه ویژگی ها";
    var index = 1;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"card\">\r\n    <div class=\"card-header\">\r\n        <h3 class=\"card-title\">");
#nullable restore
#line 9 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\Admin\Views\Product\GetProductFeatureForShow.cshtml"
                          Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h3>
    </div>
    <!-- /.card-header -->
    
    <div class=""card-body"">
       
        <div id=""example1_wrapper"" class=""dataTables_wrapper container-fluid dt-bootstrap4"">

            <div class=""row"">
                <div class=""col-sm-12"">
");
#nullable restore
#line 19 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\Admin\Views\Product\GetProductFeatureForShow.cshtml"
                     if (Model != null && Model.Any())
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        <table id=""example1"" class=""table table-bordered table-striped dataTable"" role=""grid"">
                            <thead>
                            <tr role=""row"">
                                <th class=""sorting"" tabindex=""0"" aria-controls=""example1"" rowspan=""1"" colspan=""1"" aria-label=""#: activate to sort column ascending"">#</th>
                                <th class=""sorting"" tabindex=""0"" aria-controls=""example1"" rowspan=""1"" colspan=""1"" aria-label=""عنوان: activate to sort column ascending"">عنوان ویژگی</th>
                                <th class=""sorting"" tabindex=""0"" aria-controls=""example1"" rowspan=""1"" colspan=""1"" aria-label=""محتوا: activate to sort column ascending"">محتوای ویژگی</th>
                                <th class=""sorting_desc"" tabindex=""0"" aria-controls=""example1"" rowspan=""1"" colspan=""1"" aria-label=""دستورات: activate to sort column ascending""aria-sort=""descending"">دستورات</th>
                            </tr>
                            </thead>
 ");
            WriteLiteral("                           <tbody>\r\n");
#nullable restore
#line 31 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\Admin\Views\Product\GetProductFeatureForShow.cshtml"
                             foreach (var feature in Model)
                            {


#line default
#line hidden
#nullable disable
            WriteLiteral("                                <tr role=\"row\" class=\"odd\">\r\n                                    <td");
            BeginWriteAttribute("class", " class=\"", 1831, "\"", 1839, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 35 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\Admin\Views\Product\GetProductFeatureForShow.cshtml"
                                            Write(index);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td >\r\n                                        ");
#nullable restore
#line 37 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\Admin\Views\Product\GetProductFeatureForShow.cshtml"
                                   Write(feature.FeatuerTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </td>\r\n                                    <td >\r\n                                        ");
#nullable restore
#line 40 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\Admin\Views\Product\GetProductFeatureForShow.cshtml"
                                   Write(feature.FeatureValue);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </td>\r\n\r\n                                    <td class=\"center sorting_1\">\r\n                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a268f346587537c291252cd432e9099f102c362d9133", async() => {
                WriteLiteral("\r\n                                            <i class=\"fa fa-remove\"></i> حذف\r\n                                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-featureId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 44 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\Admin\Views\Product\GetProductFeatureForShow.cshtml"
                                                                                                                        WriteLiteral(feature.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["featureId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-featureId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["featureId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                       \r\n                                    </td>\r\n                                </tr>\r\n");
#nullable restore
#line 50 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\Admin\Views\Product\GetProductFeatureForShow.cshtml"
                                index++;
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            </tbody>\r\n");
            WriteLiteral("                        </table>\r\n");
#nullable restore
#line 57 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\Admin\Views\Product\GetProductFeatureForShow.cshtml"
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <div class=\"alert alert-danger\">\r\n                            <b>ایتمی وجود ندارد</b>\r\n                        </div>\r\n");
#nullable restore
#line 63 "C:\Users\yahya\source\repos\MahyaShop\Mahya.Web\Areas\Admin\Views\Product\GetProductFeatureForShow.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </div>\r\n            </div>\r\n");
            WriteLiteral("\r\n");
            WriteLiteral("        </div>\r\n    </div>\r\n    <!-- /.card-body -->\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Mahya.Domain.Models.ProductEntity.ProductFeature>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591