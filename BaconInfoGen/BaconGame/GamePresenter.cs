using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaconGame
{
    public class GamePresenter
    {
        private IGameMainForm _view;

        public GamePresenter(IGameMainForm view)
        {
            _view = view;
        }
    }
}
