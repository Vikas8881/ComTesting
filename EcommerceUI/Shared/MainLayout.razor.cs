using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace EcommerceWeb.Shared
{
    public sealed partial class MainLayout
    {
        private bool UseTabSet { get; set; } = true;

        private string Theme { get; set; } = "";

        private bool IsOpen { get; set; }

        private bool IsFixedHeader { get; set; } = true;

        private bool IsFixedFooter { get; set; } = false;

        private bool IsFullSide { get; set; } = true;

        private bool ShowFooter { get; set; } = true;

        private List<MenuItem>? Menus { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            base.OnInitialized();

            Menus = GetIconSideMenuItems();
        }

        private static List<MenuItem> GetIconSideMenuItems()
        {
            var menus = new List<MenuItem>
            {
                new MenuItem() { Text = "返回组件库", Icon = "fa fa-fw fa-home", Url = "https://www.blazor.zone/components"},
                new MenuItem() { Text = "Index", Icon = "fa fa-fw fa-fa", Url = "/" , Match = NavLinkMatch.All},
                new MenuItem() { Text = "Category", Icon = "fa fa-fw fa-users", Url = "/Category/category"},
                new MenuItem() { Text = "Product", Icon = "fa fa-fw fa-users", Url = "/Product/addProduct"},
                new MenuItem() { Text = "Product Image", Icon = "fa fa-fw fa-users", Url = "/Product/Product Images"}
            };

            return menus;
        }
    }
}
