using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Discord.Core.UserControls
{
    /// <summary>
    /// Interaction logic for ValidateEntryControl.xaml
    /// </summary>
    public partial class ValidateEntryControl : UserControl
    {
        public static readonly DependencyProperty ErrorMessageDependencyProperty;
        public static readonly DependencyProperty HeaderDependencyProperty;
        public static readonly DependencyProperty InputDependencyProperty;

        public string ErrorMessage
        {
            get => (string)GetValue(ErrorMessageDependencyProperty);
            set
            {
                // Set red color if there's error message
                HeaderTextBlock.Foreground = value == "" ?
                    (Brush)Application.Current.Resources["GrayMiddle"] :
                    (Brush)Application.Current.Resources["Red"];

                SetValue(ErrorMessageDependencyProperty, value);
            }
        }

        public string Header
        {
            get => (string)GetValue(HeaderDependencyProperty);
            set => SetValue(HeaderDependencyProperty, value);
        }

        public string Input
        {
            get => (string)GetValue(InputDependencyProperty);
            set => SetValue(InputDependencyProperty, value);
        }

        static ValidateEntryControl()
        {
            ErrorMessageDependencyProperty = DependencyProperty.Register(
                nameof(ErrorMessage),
                typeof(string),
                typeof(ValidateEntryControl));
            HeaderDependencyProperty = DependencyProperty.Register(
                nameof(Header),
                typeof(string),
                typeof(ValidateEntryControl));
            InputDependencyProperty = DependencyProperty.Register(
                nameof(Input),
                typeof(string),
                typeof(ValidateEntryControl));
        }

        public ValidateEntryControl()
        {
            InitializeComponent();

            DataContext = this;
        }
    }
}
