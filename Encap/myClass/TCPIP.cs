using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
namespace Encap
{
    public class SingleClient
    {
        protected TcpClient tcpclient = null;  //全局客户端对象  
        protected NetworkStream networkstream = null;//全局数据流传输对象  
        private static Encoding encode = Encoding.Default;
        private string recData;
        public delegate void UpdateObjectDelegate(object sender);//委托
        public event UpdateObjectDelegate UpdateClientMSG;//更新MSG事件
        public event UpdateObjectDelegate UpdateClientData;//更新数据事件
        public event UpdateObjectDelegate UpdateClientErrorMSG;//更新错误MSG事件
        public string RecData
        {
            get
            {
                return recData;
            }            
        }
        public TcpClient Tcpclient
        {
            get
            {
                return tcpclient;
            }
        }
        public NetworkStream netWorkStream
        {
            get
            {
                return networkstream;
            }
        }
        /// <summary>  
        /// 进行远程服务器的连接  
        /// </summary>  
        /// <param name="ip">ip地址</param>  
        /// <param name="port">端口</param>  
        public SingleClient(string ip, int port)
        {
            networkstream = null;
            tcpclient = new TcpClient();  //对象转换成实体  
            tcpclient.BeginConnect(System.Net.IPAddress.Parse(ip), port, new AsyncCallback(Connected), tcpclient);  //开始进行尝试连接  
        }
        /// <summary>  
        /// 发送数据
        /// </summary>  
        /// <param name="data">数据</param>  
        public void SendData(string msg)
        {
            Byte[] data = encode.GetBytes(msg);
            if (networkstream != null)
                networkstream.Write(data, 0, data.Length);  //向服务器发送数据  
        }
        /// <summary>  
        /// 关闭
        /// </summary>  
        public void Close()
        {
            networkstream.Dispose(); //释放数据流传输对象  
            tcpclient.Close(); //关闭连接  
        }
        /// <summary>  
        /// 关闭
        /// </summary>  
        /// <param name="result">传入参数</param>  
        protected void Connected(IAsyncResult result)
        {   try
            {
                TcpClient tcpclt = (TcpClient)result.AsyncState;  //将传递的参数强制转换成TcpClient  
                networkstream = tcpclt.GetStream();  //获取数据流传输对象  
                byte[] data = new byte[1000];  //新建传输的缓冲  
                networkstream.BeginRead(data, 0, 1000, new AsyncCallback(DataRec), data); //挂起数据的接收等待  
            }
            catch
            {
                UpdateClientErrorMSG.Invoke((object)"服务器连接失败");
                return;
            }
            UpdateClientMSG.Invoke((object)"服务器连接成功");
        }
        /// <summary>
        /// 数据接收委托函数
        /// </summary>
        /// <param name="result">传入参数</param>
        protected void DataRec(IAsyncResult result)
        {
            try
            {
                int length = networkstream.EndRead(result);  //获取接收数据的长度  
                if (length == 0)
                {
                    //连接已经关闭
                    UpdateClientErrorMSG.Invoke((object)"服务器中断");
                    return;
                }
                List<byte> data = new List<byte>(); //新建byte数组
                data.AddRange((byte[])result.AsyncState); //获取数据
                data.RemoveRange(length, data.Count - length); //根据长度移除无效的数据
                byte[] data2 = new byte[1000]; //重新定义接收缓冲
                networkstream.BeginRead(data2, 0, 1000, new AsyncCallback(DataRec), data2);  //重新挂起数据的接收等待
                if (length == 0)
                {
                    //连接已经关闭
                    UpdateClientErrorMSG.Invoke((object)"服务器中断");
                    return;
                }                                                                          //自定义代码区域，处理数据data
                else
                {
                    if (data.Count > 0)
                    {
                        recData = encode.GetString(data.ToArray(), 0, data.Count);
                        UpdateClientMSG.Invoke(recData);
                    }
                    //int bytesread = ns.Read(bytes, 0, bytes.Length);
                    //recData = Encoding.Default.GetString(bytes, 0, bytesread);
                }
            }
            catch
            {
                UpdateClientErrorMSG.Invoke((object)"与服务器通信异常中断");
                return;
            }
        }
    }
    public class TServer
    {
        public List<TClient> Clients = new List<TClient>();  //客户端列表
        private TcpListener tcplistener = null;  //侦听对象
        public delegate void UpdateObjectDelegate(object sender);
        public event UpdateObjectDelegate UpdateServerData;
        public event UpdateObjectDelegate UpdateServerMSG;
        public event UpdateObjectDelegate UpdateServerErrorMSG;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="port">侦听端口</param>
        public TServer(string strIP,int port)
        {
            //tcplistener = new TcpListener(System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName())[0], port);  //获得本地的 启动侦听
            tcplistener = new TcpListener(IPAddress.Parse(strIP),port);//启动侦听
            tcplistener.Start(); //启动侦听
            tcplistener.BeginAcceptTcpClient(new AsyncCallback(ClientAccept), tcplistener); //开始尝试客户端的连接

        }
        private void ClientAccept(IAsyncResult result)
        {
            TcpListener tcplst = (TcpListener)result.AsyncState;
            TcpClient bak_tcpclient = tcplst.EndAcceptTcpClient(result);
            TClient bak_client = new TClient(bak_tcpclient, this);
            bak_client.UpdateTClientMSG += new TClient.UpdateObjectDelegate(TClientUpdateMSG);
            bak_client.UpdateTClientData += new TClient.UpdateObjectDelegate(TClientUpdateData);
            bak_client.UpdateTClientErrorMSG += new TClient.UpdateObjectDelegate(TClientUpdateErrorMSG);
            if(bak_client.netWorkStream !=null)
            {
                TClientUpdateMSG((object)("客户端" + bak_client.TClientIP() + "连接成功！"));
            }
            else
            {
                TClientUpdateErrorMSG((object)("客户端" + bak_client.TClientIP() + "连接失败！"));
            }
            Clients.Add(bak_client);
            tcplst.BeginAcceptTcpClient(new AsyncCallback(ClientAccept), tcplst);

        }
        private void TClientUpdateData(object sender)
        {
            UpdateServerData.Invoke(sender);
        }
        private void TClientUpdateMSG(object sender)
        {
            UpdateServerMSG.Invoke(sender);
        }
        private void TClientUpdateErrorMSG(object sender)
        {
            UpdateServerErrorMSG.Invoke(sender);
        }
        public bool SendToClient(string ip,string mes)
        {
            for(int i = 0;i<Clients.Count;i++)
            {
                ///////判断
                if(Clients[i].Tcpclient!= null&& Clients[i].netWorkStream != null)
                {
                    if(Clients[i].TClientIP().Equals(ip))
                    {
                        Clients[i].SendData(mes);
                        return true;
                    }
                }
            }
            return false;
        }
        public void Close()
        {
            for (int i = 0; i < Clients.Count; i++)
            {
                ///////判断
                if (Clients[i].Tcpclient != null && Clients[i].netWorkStream != null)
                {
                    Clients[i].netWorkStream.Dispose(); //释放数据流传输对象  
                    Clients[i].Tcpclient.Close(); //关闭连接  
                }
            }

        }
    }
    public class TClient
    {
        private TcpClient tcpclient = null;  //客户端对象
        private NetworkStream networkstream = null;  //数据发送对象
        private static Encoding encode = Encoding.Default;

        private string recData;

        public delegate void UpdateObjectDelegate(object sender);
        public event UpdateObjectDelegate UpdateTClientMSG;
        public event UpdateObjectDelegate UpdateTClientData;
        public event UpdateObjectDelegate UpdateTClientErrorMSG;
        public string RecData
        {
            get
            {
                return recData;
            }
        }
        public TcpClient Tcpclient
        {
            get
            {
                return tcpclient;
            }
        }
        public NetworkStream netWorkStream
        {
            get
            {
                return networkstream;
            }
        }
        private TServer m_Parent = null;  

        //父级类

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tcpclt">客户端对象</param>
        /// <param name="parent">父级</param>
        public TClient(TcpClient tcpclt, TServer parent)
        {
            try
            {
                this.tcpclient = tcpclt;
                this.m_Parent = parent;
                string ip = ((IPEndPoint)tcpclient.Client.RemoteEndPoint).Address.ToString(); //获取客户端IP
                string port = ((IPEndPoint)tcpclient.Client.RemoteEndPoint).Port.ToString();  //获取客户端端口
                this.networkstream = tcpclt.GetStream();  //获取数据传输对象
                byte[] data = new byte[10240];
                this.networkstream.BeginRead(data, 0, 10240, new AsyncCallback(DataRec), data);//启动数据侦听
            }
            catch
            {
                m_Parent.Clients.Remove(this);  //告知父类删除此客户端
                return;
            }            
        }
        public void Close()
        {
            networkstream.Dispose(); //释放数据流传输对象  
            tcpclient.Close(); //关闭连接  
        }
        public string TClientIP()
        {
            string strIP = "";
            if(tcpclient!=null)
            {
                try
                {
                    strIP = ((IPEndPoint)tcpclient.Client.RemoteEndPoint).Address.ToString(); //获取客户端IP
                }
                catch (Exception ex)
                {
                }
            }
            return strIP;
        }
        /// <summary>
        /// 数据接收
        /// </summary>
        /// <param name="result"></param>
        private void DataRec(IAsyncResult result)
        {
            try
            {
                int length = networkstream.EndRead(result);
                if (length == 0)
                {
                    m_Parent.Clients.Remove(this);  //告知父类删除此客户端
                    UpdateTClientErrorMSG.Invoke((object)"客户端" + TClientIP() + ":掉线了");
                    return;
                }
                List<byte> data = new List<byte>();
                data.AddRange((byte[])result.AsyncState);
                byte[] data2 = new byte[10240];
                networkstream.BeginRead(data2, 0, 10240, new AsyncCallback(DataRec), data2);
                if (length == 0)
                {
                    m_Parent.Clients.Remove(this);  //告知父类删除此客户端
                    UpdateTClientErrorMSG.Invoke((object)"客户端" + TClientIP() + ":掉线了");
                    return;
                }
                else
                {
                    data.RemoveRange(length, data.Count - length);
                    //数据处理代码data
                    if (data.Count > 0)
                    {
                        recData = encode.GetString(data.ToArray(), 0, data.Count);
                        UpdateTClientData.Invoke(recData);
                        bool resultSend = SendData("OK");
                        if (!resultSend)
                        {
                            UpdateTClientErrorMSG.Invoke((object)"向客户端" + TClientIP() + "反馈失败");
                        }
                    }
                }
            }
            catch
            {
                m_Parent.Clients.Remove(this);  //告知父类删除此客户端
                UpdateTClientErrorMSG.Invoke((object)"客户端" + TClientIP() + ":掉线了");
                return;
            }
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public bool SendData(string msg)
        {
            Byte[] data = encode.GetBytes(msg);
            if (networkstream != null)
                try
                {
                    networkstream.Write(data, 0, data.Length);
                }
                catch (Exception )
                {
                    MessageBox.Show("发送数据失败[networkstream.Write]：" + msg);
                    return false;
                }
                
            return (true);
        }
    }
    /*
    #region 通讯

    /// <summary>
    /// 创建服务器
    /// </summary>
    public void CreatServe()
    {
        try
        {
            tServer = new TServer("127.0.0.1", 10086);
            Log("[CreatServe]", "创建服务器...", LogLevel.Info);
            tServer.UpdateServerMSG += new TServer.UpdateObjectDelegate(ServerUpdateMSG);
            tServer.UpdateServerErrorMSG += new TServer.UpdateObjectDelegate(ServerUpdateErrorMSG);
            tServer.UpdateServerData += new TServer.UpdateObjectDelegate(ServerUpdateData);
        }
        catch (Exception ex)
        {
            MessageBox.Show("[CreatServe]Err：" + ex.Message);
        }
    }

    /// <summary>
    /// 通讯MSG更新
    /// </summary>
    /// <param name="sender"></param>
    private void ServerUpdateMSG(object sender)
    {
        string msg = sender as string;
        Log("[ServerUpdateMSG]", msg, LogLevel.Info);
    }

    /// <summary>
    /// 通讯报错
    /// </summary>
    /// <param name="sender"></param>
    public void ServerUpdateErrorMSG(object sender)
    {
        string msg = sender as string;
        Log("[ServerUpdateErrorMSG]", msg, LogLevel.Error);
    }

    object obj = new object();
    /// <summary>
    /// 接收上位机数据
    /// </summary>
    /// <param name="sender"></param>
    public void ServerUpdateData(object sender)
    {
        lock (obj)
        {
            try
            {
                string msg = sender as string;
                Log("[ServerUpdateData]", "创建服务器...", LogLevel.Info);
                string[] revCMD = msg.Split(',');
                if (revCMD.Count() <= 1)
                {
                    return;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Log("[ServerUpdateData]", ex.Message, LogLevel.Error);
            }
        }
    }
    #endregion
    */
}
