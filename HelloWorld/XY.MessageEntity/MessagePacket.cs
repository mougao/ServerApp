//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: protos/MessagePacket.proto
namespace XY.MessageEntity
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CMD_BASE_MESSAGE")]
  public partial class CMD_BASE_MESSAGE : global::ProtoBuf.IExtensible
  {
    public CMD_BASE_MESSAGE() {}
    
    private int _Cmd;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"Cmd", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int Cmd
    {
      get { return _Cmd; }
      set { _Cmd = value; }
    }
    private string _Message;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"Message", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string Message
    {
      get { return _Message; }
      set { _Message = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}