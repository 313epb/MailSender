using MailSender.Domain.Entities;
using Mail_Sender.Model;
using Mail_Sender.ViewModel;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace MailSender.UnitTests
{
    public class MainViewModelTests
    {
        #region DeleteIPairItem

        [Fact]
        public void AddPairItem_Straight_ItemAdded()
        {
            //Arrange
            var msContext = new Mock<MailSenderContext>();
            MainViewModel mvm = new MainViewModel(msContext.Object);
            var item = new Sender(){Key = "testKey",Value = "testValue"};
            //Act
            mvm.AddPairCommand(item);
        }
        
        #endregion
    }   
}