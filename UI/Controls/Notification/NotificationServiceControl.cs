using CommunityToolkit.Mvvm.ComponentModel;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace StockMasterControls
{
    public enum NotificationType
    {
        Success,
        Error,
    }
    public partial class NotificationServiceControl:ObservableObject
    {
        private static NotificationServiceControl _instance;
        public static NotificationServiceControl Instance => _instance ??= new NotificationServiceControl();

        //Propiedades para Tipo,Mensaje y Visibilidad de la Notificacion
        [ObservableProperty]
        private PackIconKind _type;
        [ObservableProperty]
        private Brush _colorIcon;
        [ObservableProperty]
        private string _message;
        [ObservableProperty]
        private bool _isVisible;
        [ObservableProperty]
        private List<string> _saleType = new List<string>() { "Unidad", "Pieza" ,"Peso(Kg)"};

        //Metodo para mostrar la notificacion
        public async Task ShowNotification(string message, NotificationType type, int duration = 1500)
        {
            switch(type)
            {
                case NotificationType.Success:
                    Type = PackIconKind.CheckCircle;
                    ColorIcon = Brushes.LimeGreen;
                    SystemSounds.Asterisk.Play();
                    break; 
                case NotificationType.Error:
                    Type = PackIconKind.CloseCircle;
                    ColorIcon = Brushes.Red;
                    SystemSounds.Hand.Play();
                    break;
            }
            Message = message;
            IsVisible = true;
            await Task.Delay(duration);
            IsVisible = false;
        }
    }
}
