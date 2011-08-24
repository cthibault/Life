using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MVVM.Life.ViewModels;

namespace MVVM.Life.Behaviors
{
    public static class ToggleLifeBehaviors
    {
        #region Dependency Properties
        public static DependencyProperty ToggleLifeWhenClickedProperty =
            DependencyProperty.RegisterAttached
            (
                "ToggleLifeWhenClicked",
                typeof(bool),
                typeof(ToggleLifeBehaviors),
                new UIPropertyMetadata(false, OnToggleLifeWhenClickedChanged)
            );
        public static DependencyProperty ToggleLifeWhenEnteredWithMousePressedProperty =
            DependencyProperty.RegisterAttached
            (
                "ToggleLifeWhenEnteredWithMousePressed",
                typeof(bool),
                typeof(ToggleLifeBehaviors),
                new UIPropertyMetadata(false, OnToggleLifeWhenEnteredWithMousePressedChanged)
            );
        #endregion Dependency Properties

        #region Getters and Setters
        //GETTERS
        public static bool GetToggleLifeWhenClicked(DependencyObject dependencyObject)
        {
            return (bool)dependencyObject.GetValue(ToggleLifeWhenClickedProperty);
        }
        public static bool GetToggleLifeWhenEnteredWithMousePressed(DependencyObject dependencyObject)
        {
            return (bool)dependencyObject.GetValue(ToggleLifeWhenEnteredWithMousePressedProperty);
        }

        //SETTERS
        public static void SetToggleLifeWhenClicked(DependencyObject dependencyObject, bool value)
        {
            dependencyObject.SetValue(ToggleLifeWhenClickedProperty, value);
        }
        public static void SetToggleLifeWhenEnteredWithMousePressed(DependencyObject dependencyObject, bool value)
        {
            dependencyObject.SetValue(ToggleLifeWhenEnteredWithMousePressedProperty, value);
        }
        #endregion Getters and Setters

        #region Subscription Management
        private static void OnToggleLifeWhenClickedChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            Border border = dependencyObject as Border;
            if (border != null)
            {
                if ((bool)args.NewValue)
                    border.MouseDown += border_MouseDown;
                else
                    border.MouseDown -= border_MouseDown;
            }
        }
        private static void OnToggleLifeWhenEnteredWithMousePressedChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            Border border = dependencyObject as Border;
            if (border != null)
            {
                if ((bool)args.NewValue)
                    border.MouseEnter += border_MouseEnter;
                else
                    border.MouseEnter -= border_MouseEnter;
            }
        }
        #endregion Subscription Management

        #region Event Handlers
        private static void border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Border border = sender as Border;
            if (border != null)
                _toggleLife(border);
        }
        private static void border_MouseEnter(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Border border = sender as Border;
                if (border != null)
                    _toggleLife(border);
            }
        }
        #endregion Event Handlers

        #region Methods
        private static void _toggleLife(Border border)
        {
            CellViewModel dataContext = border.DataContext as CellViewModel;
            if (dataContext != null)
                dataContext.ToggleLife();
        }
        #endregion Methods
    }
}
