﻿using System.Collections.Generic;

namespace Gwent.NET.Events
{
    public class HandChangedEvent : Event
    {
        public List<int> HandCards { get; set; }

        public HandChangedEvent(int eventRecipient)
            : base(eventRecipient)
        {

        }
    }
}
