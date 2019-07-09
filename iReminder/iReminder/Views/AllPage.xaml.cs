using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace iReminder.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllPage 
    {
        public AllPage ()
        {
            InitializeComponent();

             On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

#pragma warning disable CS0618 // Type or member is obsolete
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetBarSelectedItemColor(Color.Black);
#pragma warning restore CS0618 // Type or member is obsolete

#pragma warning disable CS0618 // Type or member is obsolete
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetBarItemColor(Color.DarkGray);
#pragma warning restore CS0618 // Type or member is obsolete

        }
    }
}