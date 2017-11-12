using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using System.Collections.Specialized;
using System.Linq;
using Avalonia.LogicalTree;
using AvalonStudio.Utils;
using System.Collections;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Mixins;

namespace AvalonStudio.Controls
{
    public class DocumentTabControlItem : ContentControl, ISelectable
    {
        public DocumentTabControlItem()
        {

        }

        /// <summary>
        /// Defines the <see cref="IsSelected"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsSelectedProperty =
            AvaloniaProperty.Register<DocumentTabControlItem, bool>(nameof(IsSelected));

        /// <summary>
        /// Initializes static members of the <see cref="ListBoxItem"/> class.
        /// </summary>
        static DocumentTabControlItem()
        {
            SelectableMixin.Attach<DocumentTabControlItem>(IsSelectedProperty);
            FocusableProperty.OverrideDefaultValue<DocumentTabControlItem>(true);
        }

        /// <summary>
        /// Gets or sets the selection state of the item.
        /// </summary>
        public bool IsSelected
        {
            get { return GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
    }

    public class DocumentTabControl : SelectingItemsControl
    {        
        static DocumentTabControl()
        {            
        }

        public DocumentTabControl()
        {
            SelectionMode = SelectionMode.AlwaysSelected;
        }

        /// <summary>
        /// Defines an <see cref="IMemberSelector"/> that selects the content of a <see cref="TabItem"/>.
        /// </summary>
        public static readonly IMemberSelector ContentSelector =
            new FuncMemberSelector<object, object>(SelectContent);

        public static readonly StyledProperty<object> HeaderSeperatorContentProperty = AvaloniaProperty.Register<DocumentTabControl, object>(nameof(HeaderSeperatorContent));

        public object HeaderSeperatorContent
        {
            get { return GetValue(HeaderSeperatorContentProperty); }
            set { SetValue(HeaderSeperatorContentProperty, value); }
        }

        public static readonly StyledProperty<IDataTemplate> HeaderTemplateProperty = AvaloniaProperty.Register<DocumentTabControl, IDataTemplate>(nameof(HeaderTemplate));

        public IDataTemplate HeaderTemplate
        {
            get { return GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        protected override IItemContainerGenerator CreateItemContainerGenerator()
        {
            return null;
        }

        //protected override void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    base.ItemsCollectionChanged(sender, e);

        //    if (e.NewItems != null)
        //    {
        //        foreach (var item in e.NewItems.OfType<ILogical>())
        //        {
        //            LogicalChildren.Add(item);
        //        }
        //    }

        //    if (e.OldItems != null)
        //    {
        //        foreach (var item in e.OldItems.OfType<ILogical>())
        //        {
        //            LogicalChildren.Remove(item);
        //        }
        //    }
        //}

        protected override void ItemsChanged(AvaloniaPropertyChangedEventArgs e)
        {
            base.ItemsChanged(e);
            
            //if (e.OldValue != null)
            //{
            //    foreach (var item in (e.OldValue as IEnumerable).OfType<ILogical>())
            //    {                    
            //        LogicalChildren.Remove(item);
            //    }
            //}

            //if (e.NewValue != null)
            //{
            //    foreach (var item in (e.NewValue as IEnumerable).OfType<ILogical>())
            //    {
            //        LogicalChildren.Add(item);
            //    }
            //}

            if (Items.Count() > 0)
            {
                SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Selects the content of a tab item.
        /// </summary>
        /// <param name="o">The tab item.</param>
        /// <returns>The content.</returns>
        private static object SelectContent(object o)
        {
            var content = o as IContentControl;

            if (content != null)
            {
                return content.Content;
            }
            else
            {
                return o;
            }
        }

        protected override void OnContainersMaterialized(ItemContainerEventArgs e)
        {
            base.OnContainersMaterialized(e);


        }

        

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);

            var carousel = e.NameScope.Find<Carousel>("PART_Carousel");

            if (carousel != null)
            {
                carousel.MemberSelector = ContentSelector;
            }
        }
    }
}