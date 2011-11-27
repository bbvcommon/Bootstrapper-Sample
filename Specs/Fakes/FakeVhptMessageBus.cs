namespace bootstrapper.sample.Specs.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using bootstrapper.sample.Sirius;

    public class FakeVhptMessageBus : IVphtMessageBus
    {
        readonly List<Handler> handlers = new List<Handler>();

        public void Subscribe<TMessage>(IVphtHandleMessage<TMessage> handler)
        {
            this.Subscribe(handler, m => true);
        }

        public void Subscribe<TMessage>(IVphtHandleMessage<TMessage> instance, Func<TMessage, bool> selector)
        {
            lock (handlers)
            {
                if (handlers.Any(x => x.Matches(instance)))
                {
                    return;
                }

                handlers.Add(new Handler(instance));
            }
        }

        public virtual void Unsubscribe<TMessage>(IVphtHandleMessage<TMessage> instance)
        {
            lock (handlers)
            {
                var found = handlers.FirstOrDefault(x => x.Matches(instance));

                if (found != null)
                {
                    handlers.Remove(found);
                }
            }
        }

        public virtual void Publish<TMessage>(TMessage message)
        {
            Handler[] toNotify;
            lock (handlers)
            {
                toNotify = handlers.ToArray();
            }

            var messageType = typeof(TMessage);

            var dead = toNotify
                .Where(handler => !handler.Handle(messageType, message))
                .ToList();

            if (dead.Any())
            {
                lock (handlers)
                {
                    dead.ForEach(x => handlers.Remove(x));
                }
            }
        }

        protected class Handler
        {
            private readonly WeakReference reference;
            private readonly Dictionary<Type, MethodInfo> supportedHandlers = new Dictionary<Type, MethodInfo>();

            public Handler(object handler)
            {
                this.reference = new WeakReference(handler);

                var interfaces = handler.GetType().GetInterfaces()
                    .Where(x => typeof(IVphtHandle).IsAssignableFrom(x) && x.IsGenericType);

                foreach (var @interface in interfaces)
                {
                    var type = @interface.GetGenericArguments()[0];
                    var method = @interface.GetMethod("Handle");
                    this.supportedHandlers[type] = method;
                }
            }

            public bool Matches(object instance)
            {
                return this.reference.Target == instance;
            }

            public bool Handle(Type messageType, object message)
            {
                var target = this.reference.Target;
                if (target == null)
                {
                    return false;
                }

                foreach (KeyValuePair<Type, MethodInfo> pair in supportedHandlers)
                {
                    if (pair.Key.IsAssignableFrom(messageType))
                    {
                        pair.Value.Invoke(target, new[] { message });
                        return true;
                    }
                }

                return true;
            }
        }
    }
}