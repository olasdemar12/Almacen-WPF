using MahApps.Metro.IconPacks;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Almacen_Sistema.Resources.Funcionamiento.NavItem
{
    public class MenuNavItem
    {
        public string Title { get; set; }

        public PackIconKind Icon { get; set; }

        public string TargetView { get; set; }

        public MenuNavItem(string title, PackIconKind icon, string targetView = "")
        {
            Title = title;
            Icon = icon;
            TargetView = targetView;
        }
    }
}
