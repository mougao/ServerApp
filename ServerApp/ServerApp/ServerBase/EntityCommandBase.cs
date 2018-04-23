using log4net;
using ProtoBuf;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ServerApp
{
    public abstract class CommandBase<TAppSession> : CommandBase<TAppSession,MSRequestInfo>
        where TAppSession : IAppSession, IAppSession<TAppSession, MSRequestInfo>, new()
    {
    }

    public abstract class EntityCommandBase<TAppSession, TRequestInfo, TRequestEntity> : CommandBase<TAppSession, TRequestInfo>
        where TAppSession : IAppSession, IAppSession<TAppSession, TRequestInfo>, new()
        where TRequestInfo : MSRequestInfo
        where TRequestEntity : new()
    {
    }

    public abstract class EntityCommandBase<TAppSession,TRequestEntity> : EntityCommandBase<TAppSession,MSRequestInfo,TRequestEntity>
        where TAppSession : IAppSession, IAppSession<TAppSession, MSRequestInfo>, new()
        where TRequestEntity : new()
    {
        public override void ExecuteCommand(TAppSession session, MSRequestInfo requestInfo)
        {
            MemoryStream stream = new MemoryStream();
            stream.Write(requestInfo.Data.Array,requestInfo.Data.Offset,requestInfo.Data.Count);
            stream.Position = 0;
            ExcuteEntityCommand(session, Serializer.Deserialize<TRequestEntity>(stream));
        }

        protected abstract void ExcuteEntityCommand(TAppSession session, TRequestEntity entity);

       // public abstract string Name { get; }
    }

    public abstract class EntityCommandBase<TRequestEntity> : EntityCommandBase<PlayerSession, TRequestEntity>
        where TRequestEntity : new()
    {
    }
}
