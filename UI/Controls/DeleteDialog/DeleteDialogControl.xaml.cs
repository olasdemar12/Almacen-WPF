using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StockMasterControls
{
    /// <summary>
    /// Lógica de interacción para DeleteDialogControl.xaml
    /// </summary>
    public partial class DeleteDialogControl : UserControl
    {
        public DeleteDialogControl()
        {
            InitializeComponent();
            SystemSounds.Hand.Play();
        }

        #region Titulo y Subtitulo
        //Titulo del Dialogo
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title),
                typeof(string),
                typeof(DeleteDialogControl),
                new PropertyMetadata(string.Empty));


        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        //Subtitulo del Dialogo
        public static readonly DependencyProperty SubtitleProperty =
            DependencyProperty.Register(nameof(Subtitle),
                typeof(string),
                typeof(DeleteDialogControl),
                new PropertyMetadata(string.Empty));

        public string Subtitle
        {
            get { return (string)GetValue(SubtitleProperty); }
            set { SetValue(SubtitleProperty, value); }
        }

        public static readonly DependencyProperty Normal1Property =
            DependencyProperty.Register(nameof(Normal1),
                typeof(string),
                typeof(DeleteDialogControl),
                new PropertyMetadata(string.Empty));

        #endregion

        #region Texto Normal

        //Partes del Mensaje con texto Nomal:
        public string Normal1
        {
            get { return (string)GetValue(Normal1Property); }
            set { SetValue(Normal1Property, value); }
        }

        public static readonly DependencyProperty Normal2Property =
    DependencyProperty.Register(nameof(Normal2),
        typeof(string),
        typeof(DeleteDialogControl),
        new PropertyMetadata(string.Empty));

        public string Normal2
        {
            get { return (string)GetValue(Normal2Property); }
            set { SetValue(Normal2Property, value); }
        }

        public static readonly DependencyProperty Normal3Property =
    DependencyProperty.Register(nameof(Normal3),
        typeof(string),
        typeof(DeleteDialogControl),
        new PropertyMetadata(string.Empty));

        public string Normal3
        {
            get { return (string)GetValue(Normal3Property); }
            set { SetValue(Normal3Property, value); }
        }

        #endregion

        #region Texto en Negrita

        //Partes del Mensaje con texto Resaltado:
        public static readonly DependencyProperty Negrita1Property =
            DependencyProperty.Register(nameof(Negrita1),
                typeof(string),
                typeof(DeleteDialogControl),
                new PropertyMetadata(string.Empty));

        public string Negrita1
        {
            get { return (string)GetValue(Negrita1Property); }
            set { SetValue(Negrita1Property, value); }
        }

        public static readonly DependencyProperty Negrita2Property =
            DependencyProperty.Register(nameof(Negrita2),
                typeof(string),
                typeof(DeleteDialogControl),
                new PropertyMetadata(string.Empty));

        public string Negrita2
        {
            get { return (string)GetValue(Negrita2Property); }
            set { SetValue(Negrita2Property, value); }
        }

        public static readonly DependencyProperty Negrita3Property =
            DependencyProperty.Register(nameof(Negrita3),
                typeof(string),
                typeof(DeleteDialogControl),
                new PropertyMetadata(string.Empty));

        public string Negrita3
        {
            get { return (string)GetValue(Negrita3Property); }
            set { SetValue(Negrita3Property, value); }
        }

        #endregion


        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register(nameof(IsLoading),
                typeof(bool),
                typeof(DeleteDialogControl),
                new PropertyMetadata(true));

        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        public static readonly DependencyProperty IsEnableProperty =
            DependencyProperty.Register(nameof(IsEnable),
                typeof(bool),
                typeof(DeleteDialogControl),
                new PropertyMetadata(true));

        public bool IsEnable
        {
            get { return (bool)GetValue(IsEnableProperty); }
            set { SetValue(IsEnableProperty, value); }
        }

        //Comando para accionar el boton de "Eliminar"
        public static readonly DependencyProperty DeleteCommandProperty =
            DependencyProperty.Register(nameof(DeleteCommand),
                typeof(ICommand),
                typeof(DeleteDialogControl),
                new PropertyMetadata(null));

        public ICommand DeleteCommand
        {
            get { return (ICommand)GetValue(DeleteCommandProperty); }
            set { SetValue(DeleteCommandProperty, value); }
        }

        //Comando para accionar el boton de "Cancelar"
        public static readonly DependencyProperty CancelCommandProperty =
            DependencyProperty.Register(nameof(CancelCommand),
                typeof(ICommand),
                typeof(DeleteDialogControl),
                new PropertyMetadata(null));

        public ICommand CancelCommand
        {
            get { return (ICommand)GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }
    }
}
