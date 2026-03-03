using Almacen_Sistema.Resources.Funcionamiento.NavItem;
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

        public StockMasterViewModel()
        {
            MenuItems = new ObservableCollection<MenuNavItem>
        {
                new MenuNavItem("Inicio",PackIconKind.Home, "InicioView"),
                new MenuNavItem("Productos",PackIconKind.PackageVariant,"ProductosView"),
                new MenuNavItem("Movimientos",PackIconKind.SwapHorizontal,"MovimientosView"),
                new MenuNavItem("Inventario",PackIconKind.Warehouse,"InventarioView"),
                new MenuNavItem("Documentos",PackIconKind.FileDocumentOutline,"DocumentosView"),
                new MenuNavItem("Configuración",PackIconKind.Settings,"ConfiguracionView")
        };


            SelectedMenuItem = MenuItems.FirstOrDefault();
            TitlePage = SelectedMenuItem.Title;
        }

        partial void OnSelectedMenuItemChanged(MenuNavItem value)
        {
            if (value != null)
            {
                TitlePage = value.Title;
            }
        }

    }
}
