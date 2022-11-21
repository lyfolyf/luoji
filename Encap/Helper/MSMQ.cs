using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Encap
{
    public delegate void MsmqRcvMsg(string msg);
    public class MSMQ
    {
        public event MsmqRcvMsg OnMsmqRcvedEvent;
        MessageQueue _sendQueue;
        MessageQueue _rcvQueue;
        private void CreatQueue(string sendName, string rcvName, bool cycleEn)
        {
            var sendPath = $".\\Private$\\{sendName}";
            var rcvPath = $".\\Private$\\{rcvName}";
            if (MessageQueue.Exists(sendPath))
                _sendQueue = new MessageQueue(sendPath);
            else
                _sendQueue = MessageQueue.Create(sendPath);
            if (MessageQueue.Exists(rcvPath))
                _rcvQueue = new MessageQueue(rcvPath);
            else
                _rcvQueue = MessageQueue.Create(rcvPath);

            if (cycleEn)
            {
                Task task = new Task(RcvCycle);
                task.Start();
            }

        }
        public int SendQueueNum => _sendQueue.GetAllMessages().Length;
        public void SendMsg<T>(T msg)
        {
            var message = new System.Messaging.Message();
            message.Formatter = new XmlMessageFormatter(new[] { typeof(T) });
            message.Body = msg;
            _sendQueue.Send(message);
        }
        public MSMQ(string sendQueueName, string rcvQueueName, bool enableCycleMode = true)
        {
            CreatQueue(sendQueueName, rcvQueueName, enableCycleMode);
        }
        public int RcvObj<T>(ref T rcvObj)
        {
            var message = _sendQueue.Receive();
            message.Formatter = new XmlMessageFormatter(new[] { typeof(T) });
            try
            {
                rcvObj = (T)message.Body;
            }
            catch (Exception ex)
            {
                return -2;
            }
            return 0;
        }
        public string RcvMsg()
        {
            var message = _rcvQueue.Receive();
            message.Formatter = new XmlMessageFormatter(new[] { typeof(string) });
            return message.Body.ToString();
        }
        public void ClrRcvQueue()
        {
            while (_rcvQueue.GetAllMessages().Length > 0) _rcvQueue.Receive();
        }

        private void RcvCycle()
        {
            while (true)
            {
                try
                {
                    string msg = RcvMsg();
                    OnMsmqRcvedEvent?.BeginInvoke(msg, null, null);
                }
                catch
                {

                }
            }
        }
    }
}
