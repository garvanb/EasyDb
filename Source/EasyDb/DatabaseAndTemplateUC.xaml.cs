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

namespace EasyDb
{
    /// <summary>
    /// Interaction logic for DatabaseAndTemplateUC.xaml
    /// </summary>
    public partial class DatabaseAndTemplateUC : UserControl
    {
        public DatabaseAndTemplateUC()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This prevent's an annoying behaviour when
        /// an item in the Treeview is missed by the mouse click
        /// it displays a thin grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeViewTables_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        /// <summary>
        /// Forces the command to execute before setting the selected item
        /// of the TreeView to the last item, which will now be the table
        /// just added by the command. Finally sets the focus to the new
        /// column text box control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetTableAddFocus_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItemTables.IsExpanded = true;
            var button = sender as Button;
            button.Command.Execute(button.CommandParameter);
            
            foreach (Object item in TreeViewTables.Items)
            {
                TreeViewItem treeviewItem = null;
                treeviewItem =  TreeViewTables.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                if (treeviewItem != null)
                { 
                    treeviewItem.IsSelected = true;
                    treeviewItem.IsExpanded = true;
                }
            }
            TextBoxColumnName.Focus();
        }

        private void SetColumnAddFocus_Click(object sender, RoutedEventArgs e)
        {
            TextBoxColumnName.Focus();
        }
    }
}
