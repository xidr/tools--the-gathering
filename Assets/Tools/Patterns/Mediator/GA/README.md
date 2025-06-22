https://youtu.be/rRsfikzuobQ?si=gjFlckvQ2dfPE-He

It exists to act as a central point of access for information so that the others components don't have to know anything about each other: UI don't have to know anything about the hero to get the hero stats - So this is why using the `ServiceLocator` is such a great idea here???? Instead of making them to communicate through Events????????!!!!!!!!!!!

Mediator is the only thing parts of code need to know about to make some job done yas!

Single reference point. It forces objects to collaborate only through self instead of doing it directly. It becomes the central point of collaboration

Often used in building UI systems

- Broadcast to do that for multiples ones (with predicate)
- Message to do that for one specific target

Mediators have a tendency to become god objects