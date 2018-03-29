using System;
using System.Collections.Generic;
using System.Reflection;

namespace DOH2015.ComponentModel
{
    public sealed class WeakEvent<TDelegate>
    {
        static WeakEvent()
        {
            if (!typeof(TDelegate).IsSubclassOf(typeof(Delegate)))
                throw new InvalidOperationException(typeof(TDelegate).Name + " is not a delegate type");
        }

        LinkedList<WeakDelegate> invokationList = new LinkedList<WeakDelegate>();
        public void Add(TDelegate handle)
        {
            this.invokationList.AddLast(new WeakDelegate((Delegate)(object)handle));
        }

        public void Remove(TDelegate handle)
        {
            for (var node = this.invokationList.First; node != null; node = node.Next)
            {
                if (node.Value.Equals(handle))
                    this.invokationList.Remove(node);
            }
        }

        public object Invoke(params object[] param)
        {
            object result = null;
            var node = this.invokationList.First;
            while (node != null)
            {
                var nextNode = node.Next;
                if (!node.Value.KeepAlive(() => result = node.Value.Invoke(param)))
                    this.invokationList.Remove(node);

                node = nextNode;
            }
            return result;
        }

        private class WeakDelegate : IEquatable<TDelegate>
        {
            private WeakReference targetReference;
            private MethodInfo method;

            public WeakDelegate(Delegate @delegate)
            {
                if (@delegate.Target != null)
                    targetReference = new WeakReference(@delegate.Target);

                method = @delegate.Method;
            }

            private Delegate GetDelegate()
            {
                if (targetReference != null)
                    return Delegate.CreateDelegate(typeof(TDelegate), targetReference.Target, method);
                else
                    return Delegate.CreateDelegate(typeof(TDelegate), method);
            }

            public bool IsAlive
            {
                get { return targetReference == null || targetReference.IsAlive; }
            }

            public bool KeepAlive(Action scope)
            {
                var targetRef = this.targetReference.Target;
                if (targetRef != null)
                    scope();

                return targetRef != null;
            }

            public object Invoke(params object[] args)
            {
                Delegate handler = GetDelegate();
                return handler.DynamicInvoke(args);
            }

            #region IEquatable<TDelegate> Members
            public bool Equals(TDelegate other)
            {
                Delegate d = (Delegate)(object)other;
                return d != null
                    && (d.Target == null || d.Target == targetReference.Target)
                    && d.Method.Equals(method);
            }
            #endregion
        }
    }

}