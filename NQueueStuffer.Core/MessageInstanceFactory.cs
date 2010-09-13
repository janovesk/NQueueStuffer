using System;
using System.Linq;
using System.Reflection;
using NServiceBus.MessageInterfaces;

namespace NQueueStuffer.Core
{
    public class MessageInstanceFactory
    {
        private readonly IMessageMapper _messageMapper;
        private readonly Type _messageType;

        public MessageInstanceFactory(IMessageMapper messageMapper, Type messageType)
        {
            _messageMapper = messageMapper;
            _messageType = messageType;
        }

        public object GetConcreteInstance(bool useReasonableDefaults)
        {
            var concreteType = _messageMapper.GetMappedTypeFor(_messageType);
            var concreteInstance = _messageMapper.CreateInstance(concreteType);
            if (useReasonableDefaults)
                SetReasonableDefaults(concreteInstance, _messageType);
            return concreteInstance;
        }

        private static void SetReasonableDefaults(object instance, Type type)
        {
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            foreach (PropertyInfo prop in type.GetProperties(bindingFlags).Where(p => p.DeclaringType == type))
            {
                if(prop.PropertyType == typeof(DateTime))
                    prop.SetValue(instance, DateTime.Now, null);
                
            }
           
            foreach (FieldInfo field in type.GetFields(bindingFlags).Where(p => p.DeclaringType == type))
            {
                if(field.FieldType == typeof(DateTime))
                    field.SetValue(instance, DateTime.Now);
            }
           
            if (type.BaseType != null)
            {
                SetReasonableDefaults(instance, type.BaseType);
            }
        }
    }
}