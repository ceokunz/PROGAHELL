using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public class GameEventArgs : EventArgs
    {
        public object Target { get; }
        public string Message { get; }

        public GameEventArgs(object target, string message = "")
        {
            Target = target;
            Message = message;
        }
    }

    public delegate void GameEventHandler(object sender, GameEventArgs e);
}
