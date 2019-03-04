using System.Windows;
using System.Windows.Controls;
using System.Linq;
using OpenRiaServices.DomainServices.Client;
using OpenRiaServicesProjectDemo.Services;
using OpenRiaServicesProjectDemo.Domain;

namespace OpenRiaServicesProjectDemo.SilverlightApp
{
    public partial class MainPage : UserControl
    {
        private readonly AppDomainContext context;

        public MainPage()
        {
            InitializeComponent();

            context = new AppDomainContext();
        }        

        private void ExecuteCustomMethodButton_Click(object sender, RoutedEventArgs e)
        {
            context.ExecuteCustomMethod("My Value!", OnExecuteCustomMethodCompleted, null);
        }

        private void OnExecuteCustomMethodCompleted(InvokeOperation<bool> op)
        {
            MessageBox.Show(string.Format("Result: {0}", op.Value));
        }

        private void ExecuteQueryButton_Click(object sender, RoutedEventArgs e)
        {
            var query = context.GetAppTablesQuery().Where(o => o.IsEnabled);
            context.Load(query, OnQueryCompleted, null);
        }

        private void OnQueryCompleted(LoadOperation<AppTable> op)
        {
            MessageBox.Show(string.Format("Found: {0}", op.TotalEntityCount));
        }
    }
}
