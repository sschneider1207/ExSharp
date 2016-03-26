// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: ExSharp.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace ExSharp.Protobuf {

  /// <summary>Holder for reflection information generated from ExSharp.proto</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public static partial class ExSharpReflection {

    #region Descriptor
    /// <summary>File descriptor for ExSharp.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ExSharpReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg1FeFNoYXJwLnByb3RvEghleF9zaGFycCJ1CgpNb2R1bGVTcGVjEgwKBG5h",
            "bWUYASABKAkSMAoJZnVuY3Rpb25zGAIgAygLMh0uZXhfc2hhcnAuTW9kdWxl",
            "U3BlYy5GdW5jdGlvbhonCghGdW5jdGlvbhIMCgRuYW1lGAEgASgJEg0KBWFy",
            "aXR5GAIgASgFIjMKCk1vZHVsZUxpc3QSJQoHbW9kdWxlcxgBIAMoCzIULmV4",
            "X3NoYXJwLk1vZHVsZVNwZWMiVAoMRnVuY3Rpb25DYWxsEhIKCm1vZHVsZU5h",
            "bWUYASABKAkSFAoMZnVuY3Rpb25OYW1lGAIgASgJEgwKBGFyZ2MYAyABKAUS",
            "DAoEYXJndhgEIAMoDCIfCg5GdW5jdGlvblJlc3VsdBINCgV2YWx1ZRgBIAEo",
            "DEITqgIQRXhTaGFycC5Qcm90b2J1ZmIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedCodeInfo(null, new pbr::GeneratedCodeInfo[] {
            new pbr::GeneratedCodeInfo(typeof(global::ExSharp.Protobuf.ModuleSpec), global::ExSharp.Protobuf.ModuleSpec.Parser, new[]{ "Name", "Functions" }, null, null, new pbr::GeneratedCodeInfo[] { new pbr::GeneratedCodeInfo(typeof(global::ExSharp.Protobuf.ModuleSpec.Types.Function), global::ExSharp.Protobuf.ModuleSpec.Types.Function.Parser, new[]{ "Name", "Arity" }, null, null, null)}),
            new pbr::GeneratedCodeInfo(typeof(global::ExSharp.Protobuf.ModuleList), global::ExSharp.Protobuf.ModuleList.Parser, new[]{ "Modules" }, null, null, null),
            new pbr::GeneratedCodeInfo(typeof(global::ExSharp.Protobuf.FunctionCall), global::ExSharp.Protobuf.FunctionCall.Parser, new[]{ "ModuleName", "FunctionName", "Argc", "Argv" }, null, null, null),
            new pbr::GeneratedCodeInfo(typeof(global::ExSharp.Protobuf.FunctionResult), global::ExSharp.Protobuf.FunctionResult.Parser, new[]{ "Value" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class ModuleSpec : pb::IMessage<ModuleSpec> {
    private static readonly pb::MessageParser<ModuleSpec> _parser = new pb::MessageParser<ModuleSpec>(() => new ModuleSpec());
    public static pb::MessageParser<ModuleSpec> Parser { get { return _parser; } }

    public static pbr::MessageDescriptor Descriptor {
      get { return global::ExSharp.Protobuf.ExSharpReflection.Descriptor.MessageTypes[0]; }
    }

    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    public ModuleSpec() {
      OnConstruction();
    }

    partial void OnConstruction();

    public ModuleSpec(ModuleSpec other) : this() {
      name_ = other.name_;
      functions_ = other.functions_.Clone();
    }

    public ModuleSpec Clone() {
      return new ModuleSpec(this);
    }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 1;
    private string name_ = "";
    public string Name {
      get { return name_; }
      set {
        name_ = pb::Preconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "functions" field.</summary>
    public const int FunctionsFieldNumber = 2;
    private static readonly pb::FieldCodec<global::ExSharp.Protobuf.ModuleSpec.Types.Function> _repeated_functions_codec
        = pb::FieldCodec.ForMessage(18, global::ExSharp.Protobuf.ModuleSpec.Types.Function.Parser);
    private readonly pbc::RepeatedField<global::ExSharp.Protobuf.ModuleSpec.Types.Function> functions_ = new pbc::RepeatedField<global::ExSharp.Protobuf.ModuleSpec.Types.Function>();
    public pbc::RepeatedField<global::ExSharp.Protobuf.ModuleSpec.Types.Function> Functions {
      get { return functions_; }
    }

    public override bool Equals(object other) {
      return Equals(other as ModuleSpec);
    }

    public bool Equals(ModuleSpec other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Name != other.Name) return false;
      if(!functions_.Equals(other.functions_)) return false;
      return true;
    }

    public override int GetHashCode() {
      int hash = 1;
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      hash ^= functions_.GetHashCode();
      return hash;
    }

    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (Name.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Name);
      }
      functions_.WriteTo(output, _repeated_functions_codec);
    }

    public int CalculateSize() {
      int size = 0;
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      size += functions_.CalculateSize(_repeated_functions_codec);
      return size;
    }

    public void MergeFrom(ModuleSpec other) {
      if (other == null) {
        return;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      functions_.Add(other.functions_);
    }

    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            Name = input.ReadString();
            break;
          }
          case 18: {
            functions_.AddEntriesFrom(input, _repeated_functions_codec);
            break;
          }
        }
      }
    }

    #region Nested types
    /// <summary>Container for nested types declared in the ModuleSpec message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public static partial class Types {
      [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
      public sealed partial class Function : pb::IMessage<Function> {
        private static readonly pb::MessageParser<Function> _parser = new pb::MessageParser<Function>(() => new Function());
        public static pb::MessageParser<Function> Parser { get { return _parser; } }

        public static pbr::MessageDescriptor Descriptor {
          get { return global::ExSharp.Protobuf.ModuleSpec.Descriptor.NestedTypes[0]; }
        }

        pbr::MessageDescriptor pb::IMessage.Descriptor {
          get { return Descriptor; }
        }

        public Function() {
          OnConstruction();
        }

        partial void OnConstruction();

        public Function(Function other) : this() {
          name_ = other.name_;
          arity_ = other.arity_;
        }

        public Function Clone() {
          return new Function(this);
        }

        /// <summary>Field number for the "name" field.</summary>
        public const int NameFieldNumber = 1;
        private string name_ = "";
        public string Name {
          get { return name_; }
          set {
            name_ = pb::Preconditions.CheckNotNull(value, "value");
          }
        }

        /// <summary>Field number for the "arity" field.</summary>
        public const int ArityFieldNumber = 2;
        private int arity_;
        public int Arity {
          get { return arity_; }
          set {
            arity_ = value;
          }
        }

        public override bool Equals(object other) {
          return Equals(other as Function);
        }

        public bool Equals(Function other) {
          if (ReferenceEquals(other, null)) {
            return false;
          }
          if (ReferenceEquals(other, this)) {
            return true;
          }
          if (Name != other.Name) return false;
          if (Arity != other.Arity) return false;
          return true;
        }

        public override int GetHashCode() {
          int hash = 1;
          if (Name.Length != 0) hash ^= Name.GetHashCode();
          if (Arity != 0) hash ^= Arity.GetHashCode();
          return hash;
        }

        public override string ToString() {
          return pb::JsonFormatter.ToDiagnosticString(this);
        }

        public void WriteTo(pb::CodedOutputStream output) {
          if (Name.Length != 0) {
            output.WriteRawTag(10);
            output.WriteString(Name);
          }
          if (Arity != 0) {
            output.WriteRawTag(16);
            output.WriteInt32(Arity);
          }
        }

        public int CalculateSize() {
          int size = 0;
          if (Name.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
          }
          if (Arity != 0) {
            size += 1 + pb::CodedOutputStream.ComputeInt32Size(Arity);
          }
          return size;
        }

        public void MergeFrom(Function other) {
          if (other == null) {
            return;
          }
          if (other.Name.Length != 0) {
            Name = other.Name;
          }
          if (other.Arity != 0) {
            Arity = other.Arity;
          }
        }

        public void MergeFrom(pb::CodedInputStream input) {
          uint tag;
          while ((tag = input.ReadTag()) != 0) {
            switch(tag) {
              default:
                input.SkipLastField();
                break;
              case 10: {
                Name = input.ReadString();
                break;
              }
              case 16: {
                Arity = input.ReadInt32();
                break;
              }
            }
          }
        }

      }

    }
    #endregion

  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class ModuleList : pb::IMessage<ModuleList> {
    private static readonly pb::MessageParser<ModuleList> _parser = new pb::MessageParser<ModuleList>(() => new ModuleList());
    public static pb::MessageParser<ModuleList> Parser { get { return _parser; } }

    public static pbr::MessageDescriptor Descriptor {
      get { return global::ExSharp.Protobuf.ExSharpReflection.Descriptor.MessageTypes[1]; }
    }

    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    public ModuleList() {
      OnConstruction();
    }

    partial void OnConstruction();

    public ModuleList(ModuleList other) : this() {
      modules_ = other.modules_.Clone();
    }

    public ModuleList Clone() {
      return new ModuleList(this);
    }

    /// <summary>Field number for the "modules" field.</summary>
    public const int ModulesFieldNumber = 1;
    private static readonly pb::FieldCodec<global::ExSharp.Protobuf.ModuleSpec> _repeated_modules_codec
        = pb::FieldCodec.ForMessage(10, global::ExSharp.Protobuf.ModuleSpec.Parser);
    private readonly pbc::RepeatedField<global::ExSharp.Protobuf.ModuleSpec> modules_ = new pbc::RepeatedField<global::ExSharp.Protobuf.ModuleSpec>();
    public pbc::RepeatedField<global::ExSharp.Protobuf.ModuleSpec> Modules {
      get { return modules_; }
    }

    public override bool Equals(object other) {
      return Equals(other as ModuleList);
    }

    public bool Equals(ModuleList other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!modules_.Equals(other.modules_)) return false;
      return true;
    }

    public override int GetHashCode() {
      int hash = 1;
      hash ^= modules_.GetHashCode();
      return hash;
    }

    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    public void WriteTo(pb::CodedOutputStream output) {
      modules_.WriteTo(output, _repeated_modules_codec);
    }

    public int CalculateSize() {
      int size = 0;
      size += modules_.CalculateSize(_repeated_modules_codec);
      return size;
    }

    public void MergeFrom(ModuleList other) {
      if (other == null) {
        return;
      }
      modules_.Add(other.modules_);
    }

    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            modules_.AddEntriesFrom(input, _repeated_modules_codec);
            break;
          }
        }
      }
    }

  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class FunctionCall : pb::IMessage<FunctionCall> {
    private static readonly pb::MessageParser<FunctionCall> _parser = new pb::MessageParser<FunctionCall>(() => new FunctionCall());
    public static pb::MessageParser<FunctionCall> Parser { get { return _parser; } }

    public static pbr::MessageDescriptor Descriptor {
      get { return global::ExSharp.Protobuf.ExSharpReflection.Descriptor.MessageTypes[2]; }
    }

    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    public FunctionCall() {
      OnConstruction();
    }

    partial void OnConstruction();

    public FunctionCall(FunctionCall other) : this() {
      moduleName_ = other.moduleName_;
      functionName_ = other.functionName_;
      argc_ = other.argc_;
      argv_ = other.argv_.Clone();
    }

    public FunctionCall Clone() {
      return new FunctionCall(this);
    }

    /// <summary>Field number for the "moduleName" field.</summary>
    public const int ModuleNameFieldNumber = 1;
    private string moduleName_ = "";
    public string ModuleName {
      get { return moduleName_; }
      set {
        moduleName_ = pb::Preconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "functionName" field.</summary>
    public const int FunctionNameFieldNumber = 2;
    private string functionName_ = "";
    public string FunctionName {
      get { return functionName_; }
      set {
        functionName_ = pb::Preconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "argc" field.</summary>
    public const int ArgcFieldNumber = 3;
    private int argc_;
    public int Argc {
      get { return argc_; }
      set {
        argc_ = value;
      }
    }

    /// <summary>Field number for the "argv" field.</summary>
    public const int ArgvFieldNumber = 4;
    private static readonly pb::FieldCodec<pb::ByteString> _repeated_argv_codec
        = pb::FieldCodec.ForBytes(34);
    private readonly pbc::RepeatedField<pb::ByteString> argv_ = new pbc::RepeatedField<pb::ByteString>();
    public pbc::RepeatedField<pb::ByteString> Argv {
      get { return argv_; }
    }

    public override bool Equals(object other) {
      return Equals(other as FunctionCall);
    }

    public bool Equals(FunctionCall other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (ModuleName != other.ModuleName) return false;
      if (FunctionName != other.FunctionName) return false;
      if (Argc != other.Argc) return false;
      if(!argv_.Equals(other.argv_)) return false;
      return true;
    }

    public override int GetHashCode() {
      int hash = 1;
      if (ModuleName.Length != 0) hash ^= ModuleName.GetHashCode();
      if (FunctionName.Length != 0) hash ^= FunctionName.GetHashCode();
      if (Argc != 0) hash ^= Argc.GetHashCode();
      hash ^= argv_.GetHashCode();
      return hash;
    }

    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (ModuleName.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(ModuleName);
      }
      if (FunctionName.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(FunctionName);
      }
      if (Argc != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(Argc);
      }
      argv_.WriteTo(output, _repeated_argv_codec);
    }

    public int CalculateSize() {
      int size = 0;
      if (ModuleName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(ModuleName);
      }
      if (FunctionName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(FunctionName);
      }
      if (Argc != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Argc);
      }
      size += argv_.CalculateSize(_repeated_argv_codec);
      return size;
    }

    public void MergeFrom(FunctionCall other) {
      if (other == null) {
        return;
      }
      if (other.ModuleName.Length != 0) {
        ModuleName = other.ModuleName;
      }
      if (other.FunctionName.Length != 0) {
        FunctionName = other.FunctionName;
      }
      if (other.Argc != 0) {
        Argc = other.Argc;
      }
      argv_.Add(other.argv_);
    }

    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            ModuleName = input.ReadString();
            break;
          }
          case 18: {
            FunctionName = input.ReadString();
            break;
          }
          case 24: {
            Argc = input.ReadInt32();
            break;
          }
          case 34: {
            argv_.AddEntriesFrom(input, _repeated_argv_codec);
            break;
          }
        }
      }
    }

  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class FunctionResult : pb::IMessage<FunctionResult> {
    private static readonly pb::MessageParser<FunctionResult> _parser = new pb::MessageParser<FunctionResult>(() => new FunctionResult());
    public static pb::MessageParser<FunctionResult> Parser { get { return _parser; } }

    public static pbr::MessageDescriptor Descriptor {
      get { return global::ExSharp.Protobuf.ExSharpReflection.Descriptor.MessageTypes[3]; }
    }

    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    public FunctionResult() {
      OnConstruction();
    }

    partial void OnConstruction();

    public FunctionResult(FunctionResult other) : this() {
      value_ = other.value_;
    }

    public FunctionResult Clone() {
      return new FunctionResult(this);
    }

    /// <summary>Field number for the "value" field.</summary>
    public const int ValueFieldNumber = 1;
    private pb::ByteString value_ = pb::ByteString.Empty;
    public pb::ByteString Value {
      get { return value_; }
      set {
        value_ = pb::Preconditions.CheckNotNull(value, "value");
      }
    }

    public override bool Equals(object other) {
      return Equals(other as FunctionResult);
    }

    public bool Equals(FunctionResult other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Value != other.Value) return false;
      return true;
    }

    public override int GetHashCode() {
      int hash = 1;
      if (Value.Length != 0) hash ^= Value.GetHashCode();
      return hash;
    }

    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    public void WriteTo(pb::CodedOutputStream output) {
      if (Value.Length != 0) {
        output.WriteRawTag(10);
        output.WriteBytes(Value);
      }
    }

    public int CalculateSize() {
      int size = 0;
      if (Value.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(Value);
      }
      return size;
    }

    public void MergeFrom(FunctionResult other) {
      if (other == null) {
        return;
      }
      if (other.Value.Length != 0) {
        Value = other.Value;
      }
    }

    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            Value = input.ReadBytes();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code