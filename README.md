## Development Requirements
- Microsoft Visual Studio 2015 (This is the last version supported for Silverlight development by Microsoft)
- Silverlight Development Kit

## Other Information
The first time you compile the solution you will likely see errors from the Silverlight Application project indicating files cannot be found. This is because RIA services has to link to the web application project and has not been built yet. Just build the solution again, and it should compile fully the 2nd time.

You may also need to restart the IDE to get it to compile, this is just some of the fun working with RIA services.
