#pragma checksum "C:\Users\user\OneDrive\Documents\Year 2 (2022), Semester 3\Web Development\Assignment\WEB_T04_Team6\WEB_T04_Team6\Views\SaleTransaction\_ViewTransactions.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "24fd1db4d9daee84fc2e7c975ecf48a176cb666a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_SaleTransaction__ViewTransactions), @"mvc.1.0.view", @"/Views/SaleTransaction/_ViewTransactions.cshtml")]
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
#line 1 "C:\Users\user\OneDrive\Documents\Year 2 (2022), Semester 3\Web Development\Assignment\WEB_T04_Team6\WEB_T04_Team6\Views\_ViewImports.cshtml"
using WEB_T04_Team6;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\user\OneDrive\Documents\Year 2 (2022), Semester 3\Web Development\Assignment\WEB_T04_Team6\WEB_T04_Team6\Views\_ViewImports.cshtml"
using WEB_T04_Team6.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\user\OneDrive\Documents\Year 2 (2022), Semester 3\Web Development\Assignment\WEB_T04_Team6\WEB_T04_Team6\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"24fd1db4d9daee84fc2e7c975ecf48a176cb666a", @"/Views/SaleTransaction/_ViewTransactions.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d7d2fc06f50f13f00459bb9a5ebfcb51dd2dc2d5", @"/Views/_ViewImports.cshtml")]
    public class Views_SaleTransaction__ViewTransactions : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<WEB_T04_Team6.Models.SaleTransaction>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Issues", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "SaleTransaction", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\user\OneDrive\Documents\Year 2 (2022), Semester 3\Web Development\Assignment\WEB_T04_Team6\WEB_T04_Team6\Views\SaleTransaction\_ViewTransactions.cshtml"
 if (Model.ToList().Count > 0)
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""table-responsive"">
        <table id=""viewStaff"" class=""table table-striped table-bordered"">
            <thead class=""thead-dark"">
                <tr>
                    <th>Member ID</th>
                    <th>Total Amount Spent</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 15 "C:\Users\user\OneDrive\Documents\Year 2 (2022), Semester 3\Web Development\Assignment\WEB_T04_Team6\WEB_T04_Team6\Views\SaleTransaction\_ViewTransactions.cshtml"
                 foreach (var item in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>");
#nullable restore
#line 18 "C:\Users\user\OneDrive\Documents\Year 2 (2022), Semester 3\Web Development\Assignment\WEB_T04_Team6\WEB_T04_Team6\Views\SaleTransaction\_ViewTransactions.cshtml"
                       Write(item.MemberID);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 19 "C:\Users\user\OneDrive\Documents\Year 2 (2022), Semester 3\Web Development\Assignment\WEB_T04_Team6\WEB_T04_Team6\Views\SaleTransaction\_ViewTransactions.cshtml"
                       Write(item.Total);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "24fd1db4d9daee84fc2e7c975ecf48a176cb666a6083", async() => {
                WriteLiteral("Issue Cash Voucher");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 22 "C:\Users\user\OneDrive\Documents\Year 2 (2022), Semester 3\Web Development\Assignment\WEB_T04_Team6\WEB_T04_Team6\Views\SaleTransaction\_ViewTransactions.cshtml"
                                 WriteLiteral(item.MemberID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" \r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 25 "C:\Users\user\OneDrive\Documents\Year 2 (2022), Semester 3\Web Development\Assignment\WEB_T04_Team6\WEB_T04_Team6\Views\SaleTransaction\_ViewTransactions.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n    </div>\r\n");
#nullable restore
#line 29 "C:\Users\user\OneDrive\Documents\Year 2 (2022), Semester 3\Web Development\Assignment\WEB_T04_Team6\WEB_T04_Team6\Views\SaleTransaction\_ViewTransactions.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <span style=\"color:red\">No record found!</span>\r\n");
#nullable restore
#line 33 "C:\Users\user\OneDrive\Documents\Year 2 (2022), Semester 3\Web Development\Assignment\WEB_T04_Team6\WEB_T04_Team6\Views\SaleTransaction\_ViewTransactions.cshtml"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<WEB_T04_Team6.Models.SaleTransaction>> Html { get; private set; }
    }
}
#pragma warning restore 1591
