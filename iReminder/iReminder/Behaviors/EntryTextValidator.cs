using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace iReminder.Behaviors
{
    public class EntryTextValidator : Behavior<View>
    {
        public int MinLenght { get; set; }

        protected override void OnAttachedTo(View view)
        {
            Entry entry = view as Entry;
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }


        protected override void OnDetachingFrom(View view)
        {
            Entry entry = view as Entry;
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        void OnEntryTextChanged (object sender, TextChangedEventArgs args)
        {
            
            bool isValid = args.NewTextValue.Length >= MinLenght;

            ((Entry)sender).TextColor = isValid ? Color.White : Color.White;
            ((Entry)sender).FontAttributes = isValid ? FontAttributes.Italic : FontAttributes.Bold;
            ((Entry)sender).BackgroundColor = isValid ? Color.Transparent : Color.Red;
            //((Entry)sender).Text = isValid ? "Your name is awesome fam!" : "One more Character and we are done!";

        }

    }
}
