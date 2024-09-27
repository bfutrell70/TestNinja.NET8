# TestNinja.NET8

This is a copy of the TestNinja solution from Mosh Hamedani's
Unit Testing for C# Developers course from the Udemy site. The
code in this solution was not upgraded to .NET 8 through Visual 
Studio. A new solution was made along with new projects, then 
the files were copied from the original solution and updated
as needed.

The original code uses the .NET Classic 4.5 framework. This
code uses .NET 8, with all libraries using the latest version
of libraries compatible with .NET 8. This should make this
code multi-platform. Verified this by opening the solution on
a Raspberry Pi 400 running Raspberry Pi OS (Debian) with .NET 8 
and Visual Studio Code installed.

I have attempted to add comments to explain code changes made 
to fix errors or warnings reported by Visual Studio.

## Branches
The branches in this repository have the following structure:

- master
- section1 (based on master)
- section2 (based on section1)
- section3 (based on section2)

This allows someone else to start with the master branch to
work through the course. The code in the master branch should
be the equivalent to source-code-starter folder within the
source code provided by Mosh.