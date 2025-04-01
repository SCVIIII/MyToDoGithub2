using MyToDo.Common;
using MyToDo.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Extensions
{
    public static class MT1_DialogExtensions
    {


        /// <summary>
        /// 询问窗口
        /// </summary>
        /// <param name="dialogHost">指定的DialogHost会话主机</param>
        /// <param name="title">标题</param>
        /// <param name="content">询问内容</param>
        /// <param name="dialogHostName">会话主机名称(唯一)</param>
        /// <returns></returns>
        public static async Task<IDialogResult> MT1_Question(this MT1_IDialogHostService dialogHost,
            string title, string content, string dialogHostName = "Root"
            )
        {
            DialogParameters param = new DialogParameters();
            param.Add("Title", title);
            param.Add("Content", content);
            param.Add("dialogHostName", dialogHostName);
            IDialogResult dialogResult = await dialogHost.MT1_ShowDialog("MT1_MsgView", param, dialogHostName);

            return dialogResult;
        }

        /// <summary>
        /// 推送等待消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="updateModel"></param>
        public static void MT1_UpdateLoading(this IEventAggregator aggregator,MT1_UpdateModel updateModel)
        {
            aggregator.GetEvent<MT1_UpdateLoadingEvent>().Publish(updateModel);

        }

        /// <summary>
        /// 注册d等待消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="action"></param>
        public static void MT1_Register(this IEventAggregator aggregator, Action<MT1_UpdateModel>  action)
        {
            aggregator.GetEvent<MT1_UpdateLoadingEvent>().Subscribe(action);

        }


        /// <summary>
        /// 注册提示消息 
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="action"></param>
        public static void MT1_RegisterMessage(this IEventAggregator aggregator,
            Action<MT1_MessageModel> action, string filterName = "Main")
        {
            aggregator.GetEvent<MessageEvent>().Subscribe(action,
                ThreadOption.PublisherThread, true, (m) =>
                {
                    return m.Filter.Equals(filterName);
                });
        }

        /// <summary>
        /// 发送提示消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="message"></param>
        public static void MT1_SendMessage(this IEventAggregator aggregator, string message, string filterName = "Main")
        {
            aggregator.GetEvent<MessageEvent>().Publish(new MT1_MessageModel()
            {
                Filter = filterName,
                Message = message,
            });
        }
    }
}
