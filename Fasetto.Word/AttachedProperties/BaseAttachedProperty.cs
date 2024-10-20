﻿using System.Windows;

namespace Fasetto.Word
{
    public abstract class BaseAttachedProperty<Parent, Property>
        where Parent : BaseAttachedProperty<Parent, Property>, new()
    {
        public event Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged
            = (sender, EntryPointNotFoundException) => { };

        public static Parent Instance { get; private set; } = new Parent();

        public static readonly DependencyProperty ValueProperty = 
            DependencyProperty.RegisterAttached("Value", typeof(Property), typeof(BaseAttachedProperty<Parent, Property>), new PropertyMetadata(new PropertyChangedCallback(onValuePropertyChanged)));
        private static void onValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Instance.OnValueChanged(d, e);

            Instance.ValueChanged(d, e);
        }

        public static Property GetValue(DependencyObject d) => (Property) d.GetValue(ValueProperty);

        public static void SetValue(DependencyObject d, Property value) => d.SetValue(ValueProperty, value);

        public virtual void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { }
    }
}
