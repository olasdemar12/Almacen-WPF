using Almacen_Sistema.Resources.Funcionamiento.NavItem;
using Almacen_Sistema.UI.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Almacen_Sistema.MVVM.ViewModels.Window
{
    public partial class StockMasterViewModel:ObservableObject
    {
        public ObservableCollection<MenuNavItem> MenuItems { get; set; }

        [ObservableProperty]
        private MenuNavItem _selectedMenuItem;

        [ObservableProperty]
        private string _titlePage;

        [ObservableProperty]
        private object _contentPage;

        public StockMasterViewModel()
        {
            MenuItems = new ObservableCollection<MenuNavItem>
        {
                new MenuNavItem("Inicio",PackIconKind.Home, new InicioView()),
                new MenuNavItem("Productos",PackIconKind.PackageVariant,new ProductosView()),
                new MenuNavItem("Movimientos",PackIconKind.SwapHorizontal, new MovimientosView()),
                new MenuNavItem("Inventario",PackIconKind.Warehouse,new InvetarioView()),
                new MenuNavItem("Documentos",PackIconKind.FileDocumentOutline,new DocumentosView()),
                new MenuNavItem("Configuración",PackIconKind.Settings,new ConfiguracionView())
        };


            SelectedMenuItem = MenuItems.FirstOrDefault();
            TitlePage = SelectedMenuItem.Title;
        }

        partial void OnSelectedMenuItemChanged(MenuNavItem value)
        {
            if (value != null)
            {
                TitlePage = value.Title;
                switch (TitlePage)
                {
                    case "Inicio":
                        ContentPage = Activator.CreateInstance(typeof(InicioView));
                        break;
                    case "Productos":
                        ContentPage = Activator.CreateInstance(typeof(ProductosView));
                        break;
                    case "Movimientos":
                        ContentPage = Activator.CreateInstance(typeof(MovimientosView));
                        break;
                    case "Inventario":
                       ContentPage = Activator.CreateInstance(typeof(InvetarioView));
                        break;
                    case "Documentos":
                        ContentPage = Activator.CreateInstance(typeof(DocumentosView));
                        break;
                    case "Configuración":
                        ContentPage = Activator.CreateInstance(typeof(ConfiguracionView));
                        break;
                }
            }
        }
        

    }
}
