using Unigram.Charts.Data;
using Unigram.Charts.DataView;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Data.Json;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Unigram.Charts
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        public BlankPage1()
        {
            this.InitializeComponent();
        }

        private async void test_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(1000);

            var file = await Package.Current.InstalledLocation.GetFileAsync("test.json");
            var text = await FileIO.ReadTextAsync(file);
            var json = JsonConvert.DeserializeObject<TLStatsBroadcastStats>(text);

            var obj = JsonObject.Parse(json.FollowersGraph.Json.Data);
            var data = new StackBarChartData(obj);

            test.setData(data);
            test.onCheckChanged();

            Checks.ItemsSource = test.lines;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox check && check.DataContext is LineViewData column)
            {
                column.enabled = check.IsChecked == true;
                test.onCheckChanged();
            }
        }
    }
}
