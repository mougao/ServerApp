//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: protos/MessagePacket.proto
namespace MsEntity
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CMD_LG_CTL_REGIST")]
  public partial class CMD_LG_CTL_REGIST : global::ProtoBuf.IExtensible
  {
    public CMD_LG_CTL_REGIST() {}
    
    private string _account;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"account", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string account
    {
      get { return _account; }
      set { _account = value; }
    }
    private string _psw;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"psw", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string psw
    {
      get { return _psw; }
      set { _psw = value; }
    }
    private string _code;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"code", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string code
    {
      get { return _code; }
      set { _code = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}