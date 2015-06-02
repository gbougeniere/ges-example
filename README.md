# ges-example
A simple example to get started with [GetEventStore](https://geteventstore.com/) and the [.Net API](http://docs.geteventstore.com/dotnet-api/).

This example is based upon the blog post ["Getting Started: Part 2 – Implementing the CommonDomain repository interface"](https://geteventstore.com/blog/20130220/getting-started-part-2-implementing-the-commondomain-repository-interface/index.html) that presents an implementation of the Aggregate Repository from the [CommonDomain]() project by Jon Oliver.

Most of this code is based on [this repository](https://github.com/EventStore/getting-started-with-event-store) from the "getting started" series on the GetEventStore blog. Two console projects have been added : an "Emitter" and a "Subscriber". Emitter and Subscriber are using the same domain events. The Subscriber subscribes to events on a stream from the Emitter and receives plain C# events classes instances. The same Serialization/Deserialisation process is used by both the Emitter and the Subscriber.

To get started with this code sample :
1. Install and start GetEventStore
2. Activate the "$by-event-type" projection (the Subscriber example is subscribing to an event type projection for the moment)
3. Launch the Subscriber
4. Launch the Emitter and start playing with it

That's it. The Subscriber should display the Events from the Emitter.



** Disclamer: **I'm still experiencing with DDD, CQRS and Event Sourcing. Don't expect this code sample to be a reference for patterns and best practices on the subject.

** Known issues: ** 3 unit tests from the original repository are broken. I've not fixed them yet. It looks like there's an issue with the event number in the stream (the index is starting at 0 in my version of GetEventStore, but it looks like the repository implementation consider the starting point to be 1). You may have to restart the Subscriber after the first MessageSentEvent has been added to the streams. 