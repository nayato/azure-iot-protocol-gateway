// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Azure.Devices.ProtocolGateway.Mqtt
{
    using System;
    using System.Threading.Tasks;
    using DotNetty.Codecs.Mqtt.Packets;
    using DotNetty.Transport.Channels;

    public sealed class MessageAsyncProcessor<T> : MessageAsyncProcessorBase<T>, IMessageAsyncProcessor<T>
    {
        readonly Func<IChannelHandlerContext, T, Task> processFunc;

        public MessageAsyncProcessor(Func<IChannelHandlerContext, T, Task> processFunc, string scope)
            : base(scope)
        {
            this.processFunc = processFunc;
        }

        protected override Task ProcessAsync(IChannelHandlerContext context, T packet, string scope) => this.processFunc(context, packet);
    }

    public interface IMessageAsyncProcessor<T>
    {
        int BacklogSize { get; }

        Task Completion { get; }

        void Post(IChannelHandlerContext context, T message);

        void Complete();

        void Abort();
    }

    public sealed class MessageConcurrentAsyncProcessor : IMessageAsyncProcessor<PublishPacket>
    {
        Queue<int>

        public int BacklogSize => 0;

        public Task Completion => throw new NotImplementedException();

        public void Abort()
        {
        }

        public void Complete()
        {
            
        }

        public void Post(IChannelHandlerContext context, PublishPacket message)
        {
        }

        sealed class CompletionContainer
        {
            public int PacketId;

            public bool Completed;
        } 
    }
}