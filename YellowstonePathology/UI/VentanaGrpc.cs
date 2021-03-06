// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: ventana.proto
#region Designer generated code

using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;

namespace Ventana {
  public static partial class VentanaService
  {
    static readonly string __ServiceName = "ventana.VentanaService";

    static readonly Marshaller<global::Ventana.OrderRequest> __Marshaller_OrderRequest = Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Ventana.OrderRequest.Parser.ParseFrom);
    static readonly Marshaller<global::Ventana.OrderReply> __Marshaller_OrderReply = Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Ventana.OrderReply.Parser.ParseFrom);

    static readonly Method<global::Ventana.OrderRequest, global::Ventana.OrderReply> __Method_buildOrder = new Method<global::Ventana.OrderRequest, global::Ventana.OrderReply>(
        MethodType.Unary,
        __ServiceName,
        "buildOrder",
        __Marshaller_OrderRequest,
        __Marshaller_OrderReply);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Ventana.VentanaReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of VentanaService</summary>
    public abstract partial class VentanaServiceBase
    {
      public virtual global::System.Threading.Tasks.Task<global::Ventana.OrderReply> buildOrder(global::Ventana.OrderRequest request, ServerCallContext context)
      {
        throw new RpcException(new Status(StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for VentanaService</summary>
    public partial class VentanaServiceClient : ClientBase<VentanaServiceClient>
    {
      /// <summary>Creates a new client for VentanaService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public VentanaServiceClient(Channel channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for VentanaService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public VentanaServiceClient(CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected VentanaServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected VentanaServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::Ventana.OrderReply buildOrder(global::Ventana.OrderRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return buildOrder(request, new CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Ventana.OrderReply buildOrder(global::Ventana.OrderRequest request, CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_buildOrder, null, options, request);
      }
      public virtual AsyncUnaryCall<global::Ventana.OrderReply> buildOrderAsync(global::Ventana.OrderRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return buildOrderAsync(request, new CallOptions(headers, deadline, cancellationToken));
      }
      public virtual AsyncUnaryCall<global::Ventana.OrderReply> buildOrderAsync(global::Ventana.OrderRequest request, CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_buildOrder, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override VentanaServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new VentanaServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static ServerServiceDefinition BindService(VentanaServiceBase serviceImpl)
    {
      return ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_buildOrder, serviceImpl.buildOrder).Build();
    }

  }
}
#endregion
