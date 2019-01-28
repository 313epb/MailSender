using System.Collections.Generic;
using Mail_Sender.View;

namespace Mail_Sender.Model
{
    public class MessagesClass
    {
        private List<string> _msgList;
        public List<string> MsgList { get=>_msgList; set=>_msgList=value; }

        private string _title; 

        public MessagesClass(string title)
        {
            _title = title;
            _msgList= new List<string>();
        }

        public void ShowMessages()
        {
            if (_msgList.Count!=0)
            {
                MyMessageBoxWindow msgBox=new MyMessageBoxWindow(_msgList,_title);
                msgBox.ShowDialog();
            }
        }

    }
}
