using Secs4Net;
using Secs4Net.Sml;
using SECSTool;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace SECS_Agent_1._0
{
    public partial class Form1 : Form
    {
        SecsGem? _secsGem;
        HsmsConnection? _connector;
        readonly ISecsGemLogger _logger;
        readonly BindingList<PrimaryMessageWrapper> recvBuffer = new BindingList<PrimaryMessageWrapper>();
        CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        SocketConnector socketConnector;
        IJInterpreter IJ_Itpr;

        public Form1()
        {
            InitializeComponent();
            _numericUpDown_TAPPort.DataBindings.Add("Enabled", _buttonEnable, "Enabled");
            _numericUpDown_EQPPort.DataBindings.Add("Enabled", _buttonEnable, "Enabled");

            Application.ThreadException += (sender, e) => MessageBox.Show(e.Exception.ToString());
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => MessageBox.Show(e.ExceptionObject.ToString());
            _logger = new SecsLogger(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IJ_Itpr = new IJInterpreter();
            socketConnector = new SocketConnector();
            socketConnector.RcvMsgEvent += new SocketConnector.RcvEQPMsgHandler(this.RcvEQPMsgHandler);
        }

        private void RcvEQPMsgHandler(object sender, MSGArgument e)
        {
            if (e.Msg_JObj == null) throw new Exception("Receieve null object!!");
            string SxFx = e.Msg_JObj["SxFx"].ToString();
            JArray jArray = e.Msg_JObj["SecsMsg"] as JArray;
            int F_idx = SxFx.IndexOf('F');
            byte s = Convert.ToByte(SxFx.Substring(1, F_idx - 1));
            byte f = Convert.ToByte(SxFx.Substring(F_idx + 1));
            if(jArray!=null)
            {
                if (jArray.Count == 0)
                {
                    SecsMessage s_msg = new SecsMessage(s, f)
                    {
                        SecsItem = Item.L()
                    };
                    _secsGem.SendAsync(s_msg);
                }
                else
                {
                    var secs_item_raw = IJ_Itpr.JArray2Item(jArray, Item.L().Items);
                    Item secs_item = secs_item_raw.Items.First();
                    SecsMessage s_msg = new SecsMessage(s, f)
                    {
                        SecsItem = secs_item
                    };
                    _secsGem.SendAsync(s_msg);
                }
            }
            else
            {
                SecsMessage s_msg = new SecsMessage(s, f);
                _secsGem.SendAsync(s_msg);
            }
        }

        private async void _buttonEnable_Click(object sender, EventArgs e)
        {
            socketConnector.AsyncConnect(_numericUpDown_EQPPort.Value.ToString());
            _secsGem?.Dispose();

            if (_connector is not null)
            {
                await _connector.DisposeAsync();
            }


            var options = Options.Create(new SecsGemOptions
            {
                IpAddress ="0.0.0.0",
                Port = (int)_numericUpDown_TAPPort.Value,
                SocketReceiveBufferSize = 65535,
                DeviceId = 0, //testVersion deviceId=0
            });

            _connector = new HsmsConnection(options, _logger);
            _secsGem = new SecsGem(options, _connector, _logger);

            _connector.ConnectionChanged += delegate
            {
                base.Invoke((MethodInvoker)delegate
                {
                    _textBoxTAPConnStatus.Text = _connector.State.ToString();
                });
            };

            _buttonEnable.Enabled = false;
            _ = _connector.StartAsync(_cancellationTokenSource.Token);
            _buttonDisable.Enabled = true;

            try
            {
                await foreach (var primaryMessage in _secsGem.GetPrimaryMessageAsync(_cancellationTokenSource.Token))
                {
                    recvBuffer.Add(primaryMessage);
                }
            }
            catch (OperationCanceledException)
            {

            }
        }

        private async void _buttonDisable_Click(object sender, EventArgs e)
        {
            socketConnector.Close();
            if (!_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
            }
            if (_connector is not null)
            {
                await _connector.DisposeAsync();
            }
            _secsGem?.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();

            _secsGem = null;
            _buttonEnable.Enabled = true;
            _buttonDisable.Enabled = false;
            _textBoxTAPConnStatus.Text = "Disconnect";
            recvBuffer.Clear();
            richTextBox1.Clear();
        }

        #region SecsLoggerClass
        class SecsLogger : ISecsGemLogger
        {
            readonly Form1 _form;
            internal SecsLogger(Form1 form)
            {
                _form = form;
            }
            public void MessageIn(SecsMessage msg, int id)
            {

                JObject jObj = new JObject(new JProperty("SxFx", "S" + msg.S.ToString() + "F" + msg.F.ToString()));
                if (msg.SecsItem != null)
                {
                    if(msg.SecsItem.Format!=SecsFormat.List)
                        jObj.Add(new JProperty("SecsMsg", _form.IJ_Itpr.Item2Prop(msg.SecsItem)));
                    else
                    {
                        JArray jArray = new JArray();
                        jObj.Add(new JProperty("SecsMsg", _form.IJ_Itpr.Item2JArray(msg.SecsItem, jArray)));
                    }
                }
                string dataToSend = JsonConvert.SerializeObject(jObj);
                byte[] dataBytes = Encoding.Default.GetBytes(dataToSend);
                _form.socketConnector.Send(dataBytes);
                _form.Invoke((MethodInvoker)delegate
                {
                    _form.richTextBox1.SelectionColor = Color.Black;
                    _form.richTextBox1.AppendText($"<-- [0x{id:X8}] {msg.ToSml()}\n");
                });
            }

            public void MessageOut(SecsMessage msg, int id)
            {
                _form.Invoke((MethodInvoker)delegate
                {
                    _form.richTextBox1.SelectionColor = Color.Black;
                    _form.richTextBox1.AppendText($"--> [0x{id:X8}] {msg.ToSml()}\n");
                });
            }

            public void Info(string msg)
            {
                _form.Invoke((MethodInvoker)delegate
                {
                    _form.richTextBox1.SelectionColor = Color.Blue;
                    _form.richTextBox1.AppendText($"{msg}\n");
                });
            }

            public void Warning(string msg)
            {
                _form.Invoke((MethodInvoker)delegate
                {
                    _form.richTextBox1.SelectionColor = Color.Green;
                    _form.richTextBox1.AppendText($"{msg}\n");
                });
            }

            public void Error(string msg, SecsMessage? message, Exception? ex)
            {
                _form.Invoke((MethodInvoker)delegate
                {
                    _form.richTextBox1.SelectionColor = Color.Red;
                    _form.richTextBox1.AppendText($"{msg}\n");
                    _form.richTextBox1.AppendText($"{message?.ToSml()}\n");
                    _form.richTextBox1.SelectionColor = Color.Gray;
                    _form.richTextBox1.AppendText($"{ex}\n");
                });
            }

            public void Debug(string msg)
            {
                _form.Invoke((MethodInvoker)delegate
                {
                    _form.richTextBox1.SelectionColor = Color.Yellow;
                    _form.richTextBox1.AppendText($"{msg}\n");
                });
            }
        }
        #endregion

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            socketConnector.Close();
        }
    }
}