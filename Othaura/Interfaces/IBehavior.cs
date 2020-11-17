using Othaura.Core;
using Othaura.Systems;

namespace Othaura.Interfaces {
    public interface IBehavior {
        bool Act(Monster monster, CommandSystem commandSystem);
    }
}
