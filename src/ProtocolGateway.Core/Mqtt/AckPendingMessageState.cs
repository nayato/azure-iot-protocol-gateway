// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Azure.Devices.ProtocolGateway.Mqtt
{
    using System;
    using System.Threading.Tasks;
    using DotNetty.Codecs.Mqtt.Packets;
    using DotNetty.Common;
    using Microsoft.Azure.Devices.ProtocolGateway.Messaging;

    sealed class AckPendingMessageState : IPacketReference, ISupportRetransmission // todo: recycle?
    {
        public AckPendingMessageState(IMessage message, MessageFeedbackChannel feedback, PublishPacket packet)
        {
            this.SequenceNumber = message.SequenceNumber;
            this.PacketId = packet.PacketId;
            this.QualityOfService = packet.QualityOfService;
            this.Feedback = feedback;
            this.SentTime = DateTime.UtcNow;
            this.StartTimestamp = PreciseTimeSpan.FromStart;
        }

        public PreciseTimeSpan StartTimestamp { get; set; }

        public ulong SequenceNumber { get; }

        public int PacketId { get; }

        public DateTime SentTime { get; private set; }

        public QualityOfService QualityOfService { get; private set; }

        public MessageFeedbackChannel Feedback { get; private set; }

        public void ResetMessage(IMessage message, MessageFeedbackChannel feedback)
        {
            if (message.SequenceNumber != this.SequenceNumber)
            {
                throw new InvalidOperationException($"Expected to receive message with id of {this.SequenceNumber.ToString()} but saw a message " +
                    $"with id of {message.SequenceNumber.ToString()}. Protocol Gateway only supports exclusive connection to IoT Hub.");
            }

            this.Feedback = feedback;
        }

        public void ResetSentTime()
        {
            this.SentTime = DateTime.UtcNow;
        }
    }
}