﻿using System;
using EventStore.Projections.Core.Messages.ParallelQueryProcessingMessages;
using EventStore.Projections.Core.Messages.Persisted.Responses.Slave;
using NUnit.Framework;

namespace EventStore.Projections.Core.Tests.Services.slave_projection_response_writer
{
    [TestFixture]
    class when_handling_partition_measured_message : specification_with_slave_projection_response_writer
    {
        private Guid _workerId;
        private Guid _subscriptionId;
        private string _partition;
        private int _size;

        protected override void Given()
        {
            _workerId = Guid.NewGuid();
            _subscriptionId = Guid.NewGuid();
            _partition = "partition";
            _size = 123;
        }

        protected override void When()
        {
            _sut.Handle(new PartitionMeasured(_workerId, _subscriptionId, _partition, _size));
        }

        [Test]
        public void publishes_partition_measured_response()
        {
            var body = AssertParsedSingleResponse<PartitionMeasuredResponse>("$measured", _subscriptionId);
            Assert.AreEqual(_partition, body.Partition);
            Assert.AreEqual(_size, body.Size);
        }

    }
}