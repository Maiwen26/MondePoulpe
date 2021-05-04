using MondePoulpe.Core;
using MondePoulpe.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MondePoulpe.Interfaces
{
    public interface IBehavior
    {
        bool Act(Monster monster, CommandSystem commandSystem);
    }
}
