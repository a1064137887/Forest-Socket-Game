using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
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
            DefaultController defaultController = new DefaultController();
            controllerDic.Add(defaultController.RequestCode, defaultController);
        }

        public void HandleRequest(RequestCode requestCode,ActionCode actionCode,string data)
        {
            BaseController controller;
            if(!controllerDic.TryGetValue(requestCode,out controller))
            {
                return;
            }
            string methodName = Enum.GetName(typeof(ActionCode), actionCode);
            MethodInfo methodInfo = controller.GetType().GetMethod(methodName);
            if(methodInfo == null)
            {
                return;
            }
            object[] paramaters = new object[] {data};
            object o = methodInfo.Invoke(controller,paramaters);
        }

    //class end
    }
//namespace end
}
