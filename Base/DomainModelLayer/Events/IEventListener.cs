﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DomainModelLayer.Events
{
    public interface IEventListener
    {
    }

    public interface IEventListener<TEvent>: IEventListener where TEvent: IDomainEvent
    {
        void Handle(TEvent eventData);
    }

}
