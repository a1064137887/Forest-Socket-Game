using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Common;
using GameServer.Servers;

namespace GameServer.Controller
{
    class ControllerManager
    {
        private Dictionary<RequestCode, BaseController> controllerDic = new Dictionary<RequestCode, BaseController>();
        private Server server;
        public ControllerManager(Server server)
        {
            this.server = server;
            Init();
        }

        private void Init()
        {
            DefaultController defaultController = new DefaultController();
            controllerDic.Add(defaultController.RequestCode, defaultController);
            controllerDic.Add(RequestCode.User, new UserController());
        }

        public void HandleRequest(RequestCode requestCode,ActionCode actionCode,string data,Client client)
        {
            BaseController controller;
            if(!controllerDic.TryGetValue(requestCode,out controller))
            {
                return;
            }

            //通过映射的方法调用 controller 中的方法
            string methodName = Enum.GetName(typeof(ActionCode), actionCode);
            MethodInfo methodInfo = controller.GetType().GetMethod(methodName);
            if(methodInfo == null)
            {
                return;
            }
            object[] paramaters = new object[] {data,client,server};
            object o = methodInfo.Invoke(controller, paramaters);//o为返回给客户端的信息
            if (o == null || string.IsNullOrEmpty(o as string))
            {
                return;
            }
            server.SendResponse(client, actionCode, o as string); 
        }



    //class end
    }
//namespace end
}
