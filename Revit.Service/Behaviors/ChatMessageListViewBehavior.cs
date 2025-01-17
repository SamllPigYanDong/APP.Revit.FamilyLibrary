﻿using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;

namespace Revit.Admin.Behaviors
{
    public class ChatMessageListBoxGroupBehavior : Behavior<ListBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            ((ICollectionView)AssociatedObject.Items).GroupDescriptions.Add(new PropertyGroupDescription("CreationTime"));
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            ((ICollectionView)AssociatedObject.Items).GroupDescriptions.Remove(new PropertyGroupDescription("CreationTime"));
        }
    } 
}