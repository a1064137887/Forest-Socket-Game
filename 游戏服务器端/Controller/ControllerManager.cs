using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace GameServer.Controller
{
    class ControllerManager
    {
        private Dictionary<RequestCode, BaseController> controllerDic = new Dictionary<RequestCode, BaseController>();
        public ControllerManager()
        {

        }

        private void Init()
        {
            //TODO
        }

    //class end
    }
//namespace end
}
