using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVVM.Life.Common.Models;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace MVVM.Life.Business.Services
{
    public class BasicHealthDisplayManager : HealthDisplayManager<Color>
    {
        public BasicHealthDisplayManager()
        {
            ObservableCollection<IHealthDisplay<Color>> newItems = new ObservableCollection<IHealthDisplay<Color>>()
            {
                new HealthDisplay<Color>(100, Colors.Blue),
                new HealthDisplay<Color>(0, Colors.White)
            };

            Items = newItems;
            Default = Colors.Black;
        }
    }
}
