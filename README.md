# xorPacker
A simple packer working with all PE files which cipher your exe with a XOR implementation

# Packer
<img src="https://image.prntscr.com/image/PoGtGhKQS7KBwFyDKj802g.png"/>

PE is encoded to Base64 then XOR-ed with a random key with a length of 25 chars, re-Base64 encoded & injected to a PE Dotnet stub which will do the reverse operation.

# Todo list
- Implementation of Process Hollowing with public code (which will make the tool work only with .NET PE)
