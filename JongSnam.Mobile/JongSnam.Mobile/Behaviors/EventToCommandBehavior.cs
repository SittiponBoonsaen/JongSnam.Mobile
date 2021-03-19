using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Windows.Input;
using JongSnam.Mobile.Behaviors.Base;
using Xamarin.Forms;

namespace JongSnam.Mobile.Behaviors
{
    /// <summary>
    /// Event to command behavior class
    /// </summary>
    /// <seealso cref="Kob.Uco.Mobile.Behaviors.Base.BehaviorBase{Xamarin.Forms.VisualElement}" />
    public class EventToCommandBehavior : BehaviorBase<VisualElement>
    {
        /// <summary>
        /// The event name property
        /// </summary>
        public static readonly BindableProperty EventNameProperty = BindableProperty.Create(nameof(EventName), typeof(string), typeof(EventToCommandBehavior), propertyChanged: OnEventNameChanged);

        /// <summary>
        /// The command property
        /// </summary>
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(EventToCommandBehavior));

        /// <summary>
        /// The command parameter property
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(EventToCommandBehavior));

        /// <summary>
        /// The event arguments converter property
        /// </summary>
        public static readonly BindableProperty EventArgsConverterProperty = BindableProperty.Create(nameof(EventArgsConverter), typeof(IValueConverter), typeof(EventToCommandBehavior));

        /// <summary>
        /// The event arguments converter parameter property
        /// </summary>
        public static readonly BindableProperty EventArgsConverterParameterProperty = BindableProperty.Create(nameof(EventArgsConverterParameter), typeof(object), typeof(EventToCommandBehavior));

        private EventInfo locatedEventInfo;
        private Delegate eventHandler;

        /// <summary>
        /// Gets or sets the name of the event.
        /// </summary>
        /// <value>
        /// The name of the event.
        /// </value>
        public string EventName
        {
            get { return (string)GetValue(EventNameProperty); }
            set { SetValue(EventNameProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>
        /// The command.
        /// </value>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command parameter.
        /// </summary>
        /// <value>
        /// The command parameter.
        /// </value>
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        /// <summary>
        /// Gets or sets the event arguments converter.
        /// </summary>
        /// <value>
        /// The event arguments converter.
        /// </value>
        public IValueConverter EventArgsConverter
        {
            get { return (IValueConverter)GetValue(EventArgsConverterProperty); }
            set { SetValue(EventArgsConverterProperty, value); }
        }

        /// <summary>
        /// Gets or sets the event arguments converter parameter.
        /// </summary>
        /// <value>
        /// The event arguments converter parameter.
        /// </value>
        public object EventArgsConverterParameter
        {
            get { return GetValue(EventArgsConverterParameterProperty); }
            set { SetValue(EventArgsConverterParameterProperty, value); }
        }

        /// <summary>
        /// Attaches to the superclass and then calls the <see cref="M:Xamarin.Forms.Behavior`1.OnAttachedTo(`0)" /> method on this object.
        /// </summary>
        /// <param name="bindable">The bindable object to which the behavior was attached.</param>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnAttachedTo(VisualElement bindable)
        {
            base.OnAttachedTo(bindable);
            Subscribe(AssociatedObject, EventName);
        }

        /// <summary>
        /// Calls the <see cref="M:Xamarin.Forms.Behavior`1.OnDetachingFrom(`0)" /> method and then detaches from the superclass.
        /// </summary>
        /// <param name="bindable">The bindable object from which the behavior was detached.</param>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnDetachingFrom(VisualElement bindable)
        {
            Unsubscribe();
            base.OnDetachingFrom(bindable);
        }

        private static void OnEventNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((EventToCommandBehavior)bindable).OnEventNameChangedImpl((string)oldValue, (string)newValue);
        }

        private void Subscribe(object target, string eventName)
        {
            if (target == null || string.IsNullOrEmpty(eventName))
            {
                return;
            }

            // Lookup the named event on the associated object.
            locatedEventInfo = target.GetType().GetRuntimeEvent(eventName);
            if (locatedEventInfo == null)
            {
                throw new Exception($"Event {eventName} not found on {target}.");
            }

            // Wire up the event with reflection.
            MethodInfo methodInfo = typeof(EventToCommandBehavior).GetTypeInfo().GetDeclaredMethod("OnEventRaised");

            // for debug methodInfo should have value.
            Debug.Assert(methodInfo != null, "MethodInfo should have value");

            eventHandler = methodInfo.CreateDelegate(locatedEventInfo.EventHandlerType, this);
            locatedEventInfo.AddEventHandler(target, eventHandler);
        }

        private void Unsubscribe()
        {
            if (eventHandler == null)
            {
                return;
            }

            locatedEventInfo.RemoveEventHandler(AssociatedObject, eventHandler);
            eventHandler = null;
            locatedEventInfo = null;
        }

        private void OnEventRaised(object sender, EventArgs e)
        {
            if (Command != null)
            {
                object parameter;

                if (EventArgsConverter != null)
                {
                    parameter = EventArgsConverter.Convert(e, typeof(object), EventArgsConverterParameter, CultureInfo.CurrentUICulture);
                }
                else
                {
                    parameter = CommandParameter;
                }

                if (Command.CanExecute(parameter))
                {
                    Command.Execute(parameter);
                }
            }
#if DEBUG
            else
            {
                Debug.WriteLine($"{nameof(EventToCommandBehavior)}: missing Command on event handler, {EventName}: Sender={sender}, EventArgs={e}");
            }
#endif
        }

        private void OnEventNameChangedImpl(string oldValue, string newValue)
        {
            Unsubscribe();
            Subscribe(AssociatedObject, newValue);
        }
    }
}
