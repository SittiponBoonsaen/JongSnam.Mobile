using Xamarin.Forms;

namespace JongSnam.Mobile.Behaviors
{
    /// <summary>
    /// Effect behavior class
    /// </summary>
    /// <seealso cref="Xamarin.Forms.Behavior{Xamarin.Forms.VisualElement}" />
    public class EffectBehavior : Behavior<VisualElement>
    {
        /// <summary>
        /// The group property
        /// </summary>
        public static readonly BindableProperty GroupProperty = BindableProperty.Create(nameof(Group), typeof(string), typeof(EffectBehavior), null);

        /// <summary>
        /// The name property
        /// </summary>
        public static readonly BindableProperty NameProperty = BindableProperty.Create(nameof(Name), typeof(string), typeof(EffectBehavior), null);

        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        /// <value>
        /// The group of effect behavior.
        /// </value>
        public string Group
        {
            get { return (string)GetValue(GroupProperty); }
            set { SetValue(GroupProperty, value); }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name of effect behavior.
        /// </value>
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
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
            AddEffect(bindable as View);
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
            RemoveEffect(bindable as View);
            base.OnDetachingFrom(bindable);
        }

        private void AddEffect(View view)
        {
            var effect = GetEffect();
            if (effect != null)
            {
                view.Effects.Add(GetEffect());
            }
        }

        private void RemoveEffect(View view)
        {
            var effect = GetEffect();
            if (effect != null)
            {
                view.Effects.Remove(GetEffect());
            }
        }

        private Effect GetEffect()
        {
            if (!string.IsNullOrWhiteSpace(Group) && !string.IsNullOrWhiteSpace(Name))
            {
                return Effect.Resolve(string.Format("{0}.{1}", Group, Name));
            }

            return null;
        }
    }
}
