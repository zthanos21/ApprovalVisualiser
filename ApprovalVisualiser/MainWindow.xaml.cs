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
        public DataVM vm = new DataVM();
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
       
            tcMain.SelectedIndex = 1;
        }

        public class DataVM
        {
            public DataVM()
            {
                RefDataDisplaySource = MyDataContext.GetItemsFromEnum<ReferenceDataDescriptionType>();
                FieldTypeSource = MyDataContext.GetItemsFromEnum<FieldType>();
                ProcessAttributesVM = new ProcessAttributesVM();
            }
            public void InitializeData(string data)
            {
                ProcessAttributesVM.InitializeData(data);
            }

            public void Preview()
            {
                SummaryTextSample = ProcessAttributesVM.GetSummaryText();
                SummaryDetailsSample = ProcessAttributesVM.GetDetailsSample();

            }
            public ObservableCollection<ItemSourceData> RefDataDisplaySource { get; set; }
            public ObservableCollection<ItemSourceData> FieldTypeSource { get; set; }
            public ObservableCollection<KeyValues> SummaryDetailsSample { get; set; }
            public ProcessAttributesVM ProcessAttributesVM { get; set; }
            public string SummaryTextSample { get; set; }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            vm.Preview();

        }
    }



}