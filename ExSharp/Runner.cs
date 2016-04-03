using ExSharp.Protobuf;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExSharp
{
    public class Runner
    {
        private IReadOnlyDictionary<ExSharpModuleAttribute, ExSharpFunctionAttribute[]> _moduleToFunctions;
        private IReadOnlyDictionary<string, IReadOnlyDictionary<string, MethodInfo>> _moduleNameToFunctionNameToMethod;

        private static readonly byte[] _startSignal = new byte[] { 37, 10, 246, 113 };
        private static readonly byte[] _receiveModList = new byte[] {203, 61, 10, 114};
        private static readonly byte[] _protoHeader = new byte[] { 112, 198, 7, 27 };        

        public Runner()
        {
            var typesToMethods = Assembly.GetCallingAssembly()
                .GetTypes()
                .Where(t => t.GetCustomAttribute<ExSharpModuleAttribute>(false) != null)
                .ToDictionary(t => t, t => t.GetMethods()
                                            .Where(m => m.GetCustomAttribute<ExSharpFunctionAttribute>(false) != null)
                                            .Where(m => m.ReturnType == typeof(ElixirTerm) || m.ReturnType == typeof(void))
                                            .ToArray());

            var functionsToMethods = typesToMethods.Values.SelectMany(ms =>
                    ms.Select(m => new KeyValuePair<ExSharpFunctionAttribute, MethodInfo>(m.GetCustomAttribute<ExSharpFunctionAttribute>(false), m)))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            _moduleToFunctions = typesToMethods.Select(kvp => 
                new KeyValuePair<ExSharpModuleAttribute, ExSharpFunctionAttribute[]>(
                    kvp.Key.GetCustomAttribute<ExSharpModuleAttribute>(), 
                    kvp.Value.Select(m => m.GetCustomAttribute<ExSharpFunctionAttribute>())
                .ToArray()))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);            

            _moduleNameToFunctionNameToMethod = _moduleToFunctions.Select(kvp =>
                new KeyValuePair<string, IReadOnlyDictionary<string, MethodInfo>>(kvp.Key.ToString(), kvp.Value.Select(fun =>
                        new KeyValuePair<string, MethodInfo>(fun.ToString(), functionsToMethods[fun]))
                    .ToDictionary(innerKvp => innerKvp.Key, innerKvp => innerKvp.Value)))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public void Run()
        {
            SignalStarted();
            WaitForReadySignal();
            Loop();
        }

        private void SignalStarted()
        {
            using(var @out = Console.OpenStandardOutput(4))
            {
                @out.Write(_startSignal, 0, 4);
            }
        }

        private void WaitForReadySignal()
        {
            var modList = BuildModuleList();

            while (true)
            {
                var buf = ReadFromStdIn(4);
                
                if (buf.SequenceEqual(_receiveModList))
                {
                    modList.WriteToStdOut();
                    return;
                }
                System.Threading.Thread.Sleep(50);
            }
        }

        private void Loop()
        {
            while (true)
            {
                var buf = ReadFromStdIn(4);

                if (buf.SequenceEqual(_protoHeader))
                {
                    var lenBuf = ReadFromStdIn(4);
                    if(BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(lenBuf);
                    }                   
                    var len = BitConverter.ToInt32(lenBuf, 0);

                    var msgBuf = ReadFromStdIn(len);
                    var funCall = FunctionCall.Parser.ParseFrom(msgBuf);

                    var method = GetMethod(funCall.ModuleName, funCall.FunctionName, funCall.Argc);
                    var argv = funCall.Argv.Select(ElixirTerm.FromByteString)
                        .ToArray();

                    ElixirTerm termResult;
                    try
                    {
                        termResult = (ElixirTerm)method.Invoke(null, new object[] { argv, funCall.Argc });
                    }
                    catch (Exception e) when (e.InnerException != null)
                    {
                        termResult = ElixirTerm.MakeTuple(new ElixirTerm[]
                        {
                            ElixirTerm.MakeAtom("exception"),
                            ElixirTerm.MakeUTF8String(e.InnerException.Message)
                        });
                    }
                    catch (Exception e)
                    {
                        termResult = ElixirTerm.MakeTuple(new ElixirTerm[]
                        {
                            ElixirTerm.MakeAtom("exception"),
                            ElixirTerm.MakeUTF8String(e.Message)
                        });
                    }

                    if(termResult == null)
                    {
                        termResult = ElixirTerm.MakeAtom("ok");
                    }

                    ElixirTerm.ToFunctionResult(termResult)
                        .WriteToStdOut();
                }
                System.Threading.Thread.Sleep(50);
            }
        }

        private byte[] ReadFromStdIn(int length)
        {
            var buf = new byte[length];
            using (var @in = Console.OpenStandardInput(length))
            {
                @in.Read(buf, 0, length);
                return buf;
            }
        }

        private ModuleList BuildModuleList()
        {
            var list = new ModuleList();
            list.Modules.Add(_moduleToFunctions.Select(kvp =>
            {
                var mspec = new ModuleSpec()
                {
                    Name = kvp.Key.ToString()
                };
                mspec.Functions.Add(kvp.Value.Select(f => f.ToProto()));
                return mspec;
            }));
            return list;
        }        
        
        private MethodInfo GetMethod(string moduleName, string functionName, int arity)
        {
            IReadOnlyDictionary<string, MethodInfo> functions;
            if(!_moduleNameToFunctionNameToMethod.TryGetValue(moduleName, out functions))
            {
                return null;
            }

            MethodInfo function;
            return functions.TryGetValue(ExSharpFunctionAttribute.GenFullName(functionName, arity), out function) ? function : null;
        }       
    }
}
