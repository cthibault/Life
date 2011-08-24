using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace MVVM.Life.Common.Models
{
    public interface ISettings
    {
        int Size { get; set; }
        int SimulationSpeed { get; set; }
        bool WrapBoundaries { get; set; }
        RulesManager RulesManager { get; set; }
        HealthDisplayManager<Color> HealthDisplayManager { get; set; }
        Color AliveColor { get; set; }
        Color DeadColor { get; set; }
    }
}
