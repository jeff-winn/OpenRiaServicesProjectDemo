using System.Windows;
using System.Windows.Controls;
using System.Linq;
using OpenRiaServices.DomainServices.Client;
using OpenRiaServicesProjectDemo.Services;
using OpenRiaServicesProjectDemo.Domain;
using System;

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
            MessageBox.Show(string.Format("Found: {0}", op.Entities.Count()));
        }

        private void InsertRecordButton_Click(object sender, RoutedEventArgs e)
        {
            context.AppTables.Add(new AppTable()
            {
                Name = Guid.NewGuid().ToString(),
                IsEnabled = true
            });

            context.SubmitChanges(OnSubmitChangesCompleted, null);
        }

        private void OnSubmitChangesCompleted(SubmitOperation op)
        {
            if (op.HasError)
            {
                MessageBox.Show("Operation failed.");
                op.MarkErrorAsHandled();
            }
            else if (op.IsComplete)
            {
                MessageBox.Show("Operation completed.");
            }
            else if (op.IsCanceled)
            {
                MessageBox.Show("Operation cancelled.");
            }
        }

        private void UpdateRecordButton_Click(object sender, RoutedEventArgs e)
        {
            var query = context.GetAppTablesQuery().OrderBy(o => o.Id).Take(1);
            context.Load(query, op =>
            {
                var item = op.Entities.SingleOrDefault();
                if (item != null)
                {
                    item.Name = Guid.NewGuid().ToString();
                    context.SubmitChanges(OnSubmitChangesCompleted, null);
                }
            }, null);
        }

        private void DeleteRecordButton_Click(object sender, RoutedEventArgs e)
        {
            var query = context.GetAppTablesQuery().OrderByDescending(o => o.Id).Take(1);
            context.Load(query, op =>
            {
                var item = op.Entities.SingleOrDefault();
                if (item != null)
                {
                    context.AppTables.Remove(item);
                    context.SubmitChanges(OnSubmitChangesCompleted, null);
                }
            }, null);
        }
    }
}
