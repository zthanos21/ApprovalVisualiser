using ApprovalVisualiser.Models;
using ApprovalVisualiser.ViewModels;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ApprovalVisualiser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ProcessAttributesVM vm = new ProcessAttributesVM();
        public IEnumerable<ItemSourceData> RedDataDisplaySource = MyDataContext.GetItemsFromEnum<ReferenceDataDescriptionType>();
        public MainWindow()
        {
            InitializeComponent();
            const string FILE_PATH = @"C:\Users\sales\Source\Repos\ProManager\YatchBooking.Entities\Models\Agent\Layout.cs";
            DataContext = vm;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string richText = new TextRange(rEdit.Document.ContentStart, rEdit.Document.ContentEnd).Text;
            vm.InitializeData(richText);
            //tcMain.DataContext = vm;

          //  SetMyProperty(this, vm.GetAll());
            tcMain.SelectedIndex = 1;
        }




        public static ObservableCollection<AvailableProperty> GetMyProperty(DependencyObject obj)
        {
            return (ObservableCollection<AvailableProperty>)obj.GetValue(MyPropertyProperty);
        }

        public static void SetMyProperty(DependencyObject obj, ObservableCollection<AvailableProperty> value)
        {
            obj.SetValue(MyPropertyProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.RegisterAttached("MyProperty", typeof(ObservableCollection<AvailableProperty>), typeof(MainWindow), null);


    }



}