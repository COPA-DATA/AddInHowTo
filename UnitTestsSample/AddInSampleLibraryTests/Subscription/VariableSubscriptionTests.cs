using System;
using System.Collections.Generic;
using AddInSampleLibrary.Subscription;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Scada.AddIn.Contracts;
using Scada.AddIn.Contracts.Variable;

namespace AddInSampleLibraryTests.Subscription
{
    [TestClass]
    public class VariableSubscriptionTests
    {
        [TestMethod]
        public void Start_OK()
        {
            // For usage of Moq, see
            // https://github.com/Moq/moq4/wiki/Quickstart

            var onlineContainerMock = new Mock<IOnlineVariableContainer>(MockBehavior.Strict);
            onlineContainerMock.Setup(x => x.AddVariable(It.IsAny<string[]>())).Returns(true);
            onlineContainerMock.Setup(x => x.ActivateBulkMode()).Returns(true);
            onlineContainerMock.Setup(x => x.Activate()).Returns(true);

            var projectMock = new Mock<IProject>(MockBehavior.Strict);
            projectMock.Setup(x => x.OnlineVariableContainerCollection.Delete(It.IsAny<string>())).Returns(false);
            projectMock.Setup(x => x.OnlineVariableContainerCollection.Create(It.IsAny<string>())).Returns(onlineContainerMock.Object);

            Action<IEnumerable<IVariable>> action = variables =>
            {

            };

            var testee = new VariableSubscription(action);
            testee.Start(projectMock.Object, new[] { "TestVariable" });

            // Verify all verifiable expectations on all mocks created through the factory
            onlineContainerMock.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Stop_NoContainer_Fail()
        {
            // For usage of Moq, see
            // https://github.com/Moq/moq4/wiki/Quickstart

            Action<IEnumerable<IVariable>> action = variables =>
            {
            };

            var testee = new VariableSubscription(action);
            testee.Stop();

            // What else can be verified here?
            // Which drawbacks you see here?
        }
    }
}
